using Client.Models;
using Client.Shareds;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Views
{
    public partial class frm_Mon : Form
    {
        // enum variable used for select option
        private Option option = Option.Nodata;

        // uses for save data from all textboxs
        private QLKhoa_ServiceReference.Mon_ett mon = new QLKhoa_ServiceReference.Mon_ett();

        //  set "mon" variable value
        private void get_infor_mon()
        {
            mon.mamon = txt_mamon.Text;
            mon.tenmon = txt_tenmon.Text;
        }

        // update data for datagridview
        private void load_data()
        {
            dtgv_khoa.ForeColor = System.Drawing.Color.Black;
            var temp = cl.selectAllMon();
            switch (temp.errorCode)
            {
                case QLKhoa_ServiceReference.ErrorCode.Sucess:
                    dtgv_khoa.DataSource = temp.data;
                    used_for_headerclick_dtgv = temp.data;
                    Utils.chang_title_datagridViewCellMon(dtgv_khoa);
                    break;
                case QLKhoa_ServiceReference.ErrorCode.False:
                    dtgv_khoa.DataSource = temp.data;
                    if (Utils.switch_false())
                    {
                        MessageBox.Show(temp.errorInfor);
                    }
                    break;
                case QLKhoa_ServiceReference.ErrorCode.NaN:
                    dtgv_khoa.DataSource = null;
                    MessageBox.Show(temp.errorInfor);
                    break;
                default:
                    break;
            }
        }

        // variable connect to webservice
        QLKhoa_ServiceReference.WebServiceSoapClient cl = new QLKhoa_ServiceReference.WebServiceSoapClient();

        QLKhoa_ServiceReference.Mon_ett[] used_for_headerclick_dtgv;

        public frm_Mon()
        {
            InitializeComponent();
            Utils.readOnly_text_khoa(txt_mamon, txt_tenmon, txt_mamon, txt_tenmon);

        }

        private void frm_Mon_Load(object sender, EventArgs e)
        {
            load_data();

            List<how_to_search> dt_source = new List<how_to_search>();
            dt_source.Add(new how_to_search("Mã môn", "mamon"));
            dt_source.Add(new how_to_search("Tên môn", "tenmon"));

            cbx_option_search.DataSource = dt_source;
            cbx_option_search.DisplayMember = "value";
            cbx_option_search.ValueMember = "key";
            cbx_option_search.SelectedIndex = 1;
        }


        private void button6_Click(object sender, EventArgs e)
        {
            if (Utils.confirm_exit())
            {
                Application.Exit();
            }
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            Utils.erase_text_khoa(txt_mamon, txt_tenmon, txt_mamon, txt_tenmon);
            Utils.readOnly_text_khoa(txt_mamon, txt_tenmon, txt_mamon, txt_tenmon);
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            Utils.erase_text_khoa(txt_mamon, txt_tenmon, txt_mamon, txt_tenmon);
            Utils.not_readOnly_text_khoa(txt_mamon, txt_tenmon, txt_mamon, txt_tenmon);
            txt_mamon.Focus();
            option = Option.Insert;
        }

        private void dtgv_khoa_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow temp = dtgv_khoa.Rows[e.RowIndex];
                if (temp.Cells[0].Value.ToString() != null)
                {
                    txt_mamon.Text = temp.Cells[0].Value.ToString();
                }
                else txt_mamon.Text = "";
                if (temp.Cells[1].Value.ToString() != null)
                {
                    txt_tenmon.Text = temp.Cells[1].Value.ToString();
                }
                else
                    txt_tenmon.Text = "";
            }
        }

        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {

            var select_cbx = cbx_option_search.SelectedValue.ToString();
            var temp = cl.selectByFieldsMon(txt_timkiem.Text, select_cbx);
            switch (temp.errorCode)
            {
                case QLKhoa_ServiceReference.ErrorCode.Sucess:
                    dtgv_khoa.DataSource = temp.data;
                    used_for_headerclick_dtgv = temp.data;
                    Utils.chang_title_datagridViewCellMon(dtgv_khoa);
                    break;
                case QLKhoa_ServiceReference.ErrorCode.False:
                    dtgv_khoa.DataSource = temp.data;
                    if (Utils.switch_false())
                    {
                        MessageBox.Show(temp.errorInfor);
                    }
                    break;
                case QLKhoa_ServiceReference.ErrorCode.NaN:
                    dtgv_khoa.DataSource = temp.data;
                    break;
                default:
                    break;
            }
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            switch (option)
            {
                case Option.Nodata:
                    get_infor_mon();
                    if (mon.mamon == "")
                    {
                        MessageBox.Show(Constants.error_insert_nodata_mamon);
                        txt_mamon.Focus();
                    }

                    break;

                case Option.Insert:
                    get_infor_mon();
                    if (mon.mamon == "")
                    {
                        MessageBox.Show(Constants.error_null_mamon);
                        txt_mamon.Focus();
                        break;
                    }

                    bool check = true; // check if existing data
                    var data = dtgv_khoa.Rows;
                    foreach (DataGridViewRow item in data)
                    {
                        if (mon.mamon == item.Cells[0].Value.ToString())
                        {
                            check = false;
                        }
                    }
                    if (!check)
                    {
                        MessageBox.Show(Constants.error_duplicate_mamon); //if existing return error
                        txt_mamon.Focus();
                        break;
                    }

                    var temp = cl.insertMon(mon.mamon, mon.tenmon);
                    switch (temp.errorCode)
                    {
                        case QLKhoa_ServiceReference.ErrorCode.Sucess:
                            MessageBox.Show(Constants.success_insert);
                            load_data();
                            Utils.erase_text_khoa(txt_tenmon, txt_mamon,txt_tenmon, txt_mamon);
                            // option still is Insert , so we can insert in sequences
                            break;
                        case QLKhoa_ServiceReference.ErrorCode.False:
                            if (Utils.switch_false())
                            {
                                MessageBox.Show(temp.errorInfor);
                            }
                            break;
                        default:
                            break;
                    }
                    break;

                case Option.Edit:
                    get_infor_mon();
                    bool check1 = true; // check if existing data
                    var data1 = dtgv_khoa.Rows;
                    foreach (DataGridViewRow item in data1)
                    {

                        if (item.Cells[0].Value.ToString() == mon.mamon)
                        {
                            check1 = false;
                        }
                    }
                    if (check1)
                    {
                        MessageBox.Show(Constants.error_edit_mamon); // if not existing return error
                        txt_mamon.Focus();
                        break;
                    }
                    else
                    {
                        var temp1 = cl.editMon(mon.mamon, mon.tenmon);
                        switch (temp1.errorCode)
                        {
                            case QLKhoa_ServiceReference.ErrorCode.Sucess:
                                MessageBox.Show(Constants.success_edit);
                                load_data();
                                Utils.erase_text_khoa(txt_mamon,txt_tenmon,txt_tenmon,txt_mamon);
                                Utils.readOnly_text_khoa(txt_mamon,txt_tenmon,txt_tenmon,txt_mamon);
                                option = Option.Nodata; // edit one by one then return to option default
                                break;
                            case QLKhoa_ServiceReference.ErrorCode.False:
                                if (Utils.switch_false())
                                {
                                    MessageBox.Show(temp1.errorInfor);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            option = Option.Edit;
            Utils.not_readOnly_text_khoa(txt_mamon, txt_tenmon,txt_mamon, txt_tenmon);
        }

        private void txt_makhoa_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar); //makhoa only type uppercase
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            // get data user selected;
            var selected_r = dtgv_khoa.SelectedRows;
            if (selected_r.Count > 0)
            {
                if (Utils.confirm_delete())
                {
                    bool check = true;
                    string error_infor = "";
                    foreach (DataGridViewRow item in selected_r)
                    {
                        var temp = cl.deleteMon(item.Cells[0].Value.ToString());
                        switch (temp.errorCode)
                        {
                            case QLKhoa_ServiceReference.ErrorCode.Sucess:
                                Utils.erase_text_khoa(txt_mamon, txt_tenmon, txt_mamon, txt_tenmon);
                                Utils.readOnly_text_khoa(txt_mamon, txt_tenmon, txt_mamon, txt_tenmon);
                                option = Option.Nodata;
                                break;
                            case QLKhoa_ServiceReference.ErrorCode.False:
                                check = false;
                                error_infor = temp.errorInfor;
                                break;
                            default:
                                break;
                        }
                    }

                    if (check)
                    {
                        MessageBox.Show(Constants.success_delete);
                        load_data();
                    }
                    else
                    {
                        MessageBox.Show(error_infor);
                    }
                }
            }
        }

        private bool[] how_to_sort = new bool[2] { true, true };
        private void dtgv_khoa_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int which_to_sort = e.ColumnIndex;
            switch (which_to_sort)
            {
                case 0:
                    if (how_to_sort[0])
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderBy(o => o.mamon).ToArray();
                        how_to_sort[0] = false;
                    }
                    else
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderByDescending(o => o.mamon).ToArray();
                        how_to_sort[0] = true;
                    }
                    break;
                case 1:
                    if (how_to_sort[1])
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderBy(o => o.tenmon).ToArray();
                        how_to_sort[1] = false;
                    }
                    else
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderByDescending(o => o.tenmon).ToArray();
                        how_to_sort[1] = true;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
