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
    public partial class frm_Diem : Form
    {
        // enum variable used for select option
        private Option option = Option.Nodata;

        QLKhoa_ServiceReference.WebServiceSoapClient cl = new QLKhoa_ServiceReference.WebServiceSoapClient();

        private QLKhoa_ServiceReference.Diem_ett diem = new QLKhoa_ServiceReference.Diem_ett();

        private QLKhoa_ServiceReference.Diem_ett[] used_for_headerclick_dtgv;

        private void get_infor_diem()
        {
            diem.masv = cbx_masv.SelectedValue.ToString();
            diem.mamon = cbx_mamon.SelectedValue.ToString();
            diem.diem = txt_diem.Text;
        }

        public frm_Diem()
        {
            InitializeComponent();
            Utils.readOnly_text_diem(cbx_masv, cbx_mamon, txt_diem);
            cbx_mamon.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbx_mamon.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbx_masv.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbx_masv.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void load_data()
        {
            dtgv_khoa.ForeColor = System.Drawing.Color.Black;
            var temp = cl.selectAllDiem();
            switch (temp.errorCode)
            {
                case QLKhoa_ServiceReference.ErrorCode.Sucess:
                    dtgv_khoa.DataSource = temp.data;
                    used_for_headerclick_dtgv = temp.data;
                    Utils.chang_title_datagridViewCellDiem(dtgv_khoa);
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

        private void frm_Diem_Load(object sender, EventArgs e)
        {
            load_data();

            List<how_to_search> temp = new List<how_to_search>();
            temp.Add(new how_to_search("Mã SV", "masv"));
            temp.Add(new how_to_search("Tên SV", "tensv"));
            temp.Add(new how_to_search("Mã môn", "mamon"));
            temp.Add(new how_to_search("Tên môn", "tenmon"));
            temp.Add(new how_to_search("Điểm", "diem"));

            cbx_option_search.ValueMember = "key";
            cbx_option_search.DisplayMember = "value";
            cbx_option_search.DataSource = temp;

            var dt = cl.selectAllSinhVienCbx();
            switch (dt.errorCode)
            {
                case QLKhoa_ServiceReference.ErrorCode.Sucess:
                    cbx_masv.DataSource = dt.data;
                    cbx_masv.DisplayMember = "matensv";
                    cbx_masv.ValueMember = "masv";
                    cbx_masv.SelectedIndex = -1;
                    break;
                case QLKhoa_ServiceReference.ErrorCode.False:
                    MessageBox.Show(dt.errorInfor);
                    break;
                case QLKhoa_ServiceReference.ErrorCode.NaN:
                    break;
                default:
                    break;
            }

            var dt1 = cl.selectAllMon();
            switch (dt1.errorCode)
            {
                case QLKhoa_ServiceReference.ErrorCode.Sucess:
                    cbx_mamon.DataSource = dt1.data;
                    cbx_mamon.DisplayMember = "tenmon";
                    cbx_mamon.ValueMember = "mamon";
                    cbx_mamon.SelectedIndex = -1;
                    break;
                case QLKhoa_ServiceReference.ErrorCode.False:
                    break;
                case QLKhoa_ServiceReference.ErrorCode.NaN:
                    break;
                default:
                    break;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Utils.confirm_exit())
            {
                Application.Exit();
            }
        }

        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {

            var select_cbx = cbx_option_search.SelectedValue.ToString();
            var temp = cl.selectByFieldsDiem(txt_timkiem.Text, select_cbx);
            switch (temp.errorCode)
            {
                case QLKhoa_ServiceReference.ErrorCode.Sucess:
                    dtgv_khoa.DataSource = temp.data;
                    used_for_headerclick_dtgv = temp.data;
                    Utils.chang_title_datagridViewCellDiem(dtgv_khoa);
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

        private void btn_huy_Click(object sender, EventArgs e)
        {
            Utils.erase_text_diem(cbx_masv, cbx_mamon, txt_diem);
            Utils.readOnly_text_diem(cbx_masv, cbx_mamon, txt_diem);
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            Utils.erase_text_diem(cbx_masv, cbx_mamon, txt_diem);
            Utils.not_readOnly_text_diem(cbx_masv, cbx_mamon, txt_diem);
            cbx_masv.Focus();
            option = Option.Insert;
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            option = Option.Edit;
            Utils.not_readOnly_text_diem(cbx_masv, cbx_mamon, txt_diem);
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
                        var temp = cl.deleteDiem(item.Cells[0].Value.ToString(), item.Cells[1].Value.ToString());
                        switch (temp.errorCode)
                        {
                            case QLKhoa_ServiceReference.ErrorCode.Sucess:
                                Utils.erase_text_diem(cbx_masv, cbx_mamon, txt_diem);
                                Utils.readOnly_text_diem(cbx_masv, cbx_mamon, txt_diem);
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

        private void btn_luu_Click(object sender, EventArgs e)
        {
            switch (option)
            {
                case Option.Nodata:
                    break;

                case Option.Insert:
                    get_infor_diem();

                    bool check = true; // check if existing data
                    var data = dtgv_khoa.Rows;
                    foreach (DataGridViewRow item in data)
                    {
                        if (diem.masv == item.Cells[0].Value.ToString() && diem.mamon == item.Cells[1].Value.ToString())
                        {
                            check = false;
                        }
                    }
                    if (!check)
                    {
                        MessageBox.Show(Constants.error_duplicate_masv_mamon); //if existing return error
                        cbx_masv.Focus();
                        break;
                    }

                    var temp = cl.insertDiem(diem.masv, diem.mamon, diem.diem);
                    switch (temp.errorCode)
                    {
                        case QLKhoa_ServiceReference.ErrorCode.Sucess:
                            MessageBox.Show(Constants.success_insert);
                            load_data();
                            Utils.erase_text_diem(cbx_masv, cbx_mamon, txt_diem);
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
                    get_infor_diem();
                    bool check1 = true; // check if existing data
                    var data1 = dtgv_khoa.Rows;
                    foreach (DataGridViewRow item in data1)
                    {

                        if (diem.masv == item.Cells[0].Value.ToString() && diem.mamon == item.Cells[1].Value.ToString())
                        {
                            check1 = false;
                        }
                    }
                    if (check1)
                    {
                        MessageBox.Show(Constants.error_edit_masv_mamon); // if not existing return error
                        cbx_masv.Focus();
                        break;
                    }
                    else
                    {
                        var temp1 = cl.editDiem(diem.masv, diem.mamon, diem.diem);
                        switch (temp1.errorCode)
                        {
                            case QLKhoa_ServiceReference.ErrorCode.Sucess:
                                MessageBox.Show(Constants.success_edit);
                                load_data();
                                Utils.erase_text_diem(cbx_masv, cbx_mamon, txt_diem);
                                Utils.readOnly_text_diem(cbx_masv, cbx_mamon, txt_diem);
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
        private void dtgv_khoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow temp = dtgv_khoa.Rows[e.RowIndex];

                // display data from datagridview to combobox
                var local_dt = cl.selectAllSinhVienCbx();
                switch (local_dt.errorCode)
                {
                    case QLKhoa_ServiceReference.ErrorCode.Sucess:
                        foreach (var item in local_dt.data)
                        {
                            if (item.matensv.Contains(temp.Cells[0].Value.ToString()))
                            {
                                cbx_masv.Text = item.matensv;
                                cbx_mamon.Text = temp.Cells[1].Value.ToString();
                                txt_diem.Text = temp.Cells[2].Value.ToString();
                            }
                        }
                        break;
                    case QLKhoa_ServiceReference.ErrorCode.False:
                        MessageBox.Show(local_dt.errorInfor);
                        break;
                    case QLKhoa_ServiceReference.ErrorCode.NaN:
                        break;
                    default:
                        break;
                }             
            }
        }

        private bool[] how_to_sort = new bool[3] { true, true, true};
        private void dtgv_khoa_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int which_to_sort = e.ColumnIndex;
            switch (which_to_sort)
            {
                case 0:
                    if (how_to_sort[0])
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderBy(o => o.masv).ToArray();
                        how_to_sort[0] = false;
                    }
                    else
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderByDescending(o => o.masv).ToArray();
                        how_to_sort[0] = true;
                    }
                    break;
                case 1:
                    if (how_to_sort[1])
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderBy(o => o.mamon).ToArray();
                        how_to_sort[1] = false;
                    }
                    else
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderByDescending(o => o.mamon).ToArray();
                        how_to_sort[1] = true;
                    }
                    break;
                case 2:
                    if (how_to_sort[2])
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderBy(o => o.diem).ToArray();
                        how_to_sort[2] = false;
                    }
                    else
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderByDescending(o => o.diem).ToArray();
                        how_to_sort[2] = true;
                    }
                    break;
                default:
                    break;
            }
        }

        private void cbx_masv_Leave(object sender, EventArgs e)
        {
            List<QLKhoa_ServiceReference.SinhVienCbx_ett> temp = cbx_masv.Items.OfType<QLKhoa_ServiceReference.SinhVienCbx_ett>().ToList();

            var x = temp.Where(o => o.matensv == cbx_masv.Text);
            if (x.Count() == 0)
            {
                MessageBox.Show(Constants.error_not_list_khoa);
                cbx_masv.Focus();
            }
        }

        private void cbx_mamon_Leave(object sender, EventArgs e)
        {
            List<QLKhoa_ServiceReference.Mon_ett> temp = cbx_mamon.Items.OfType<QLKhoa_ServiceReference.Mon_ett>().ToList();

            var x = temp.Where(o => o.tenmon == cbx_mamon.Text);
            if (x.Count() == 0)
            {
                MessageBox.Show(Constants.error_not_list_khoa);
                cbx_mamon.Focus();
            }
        }
    }
}
