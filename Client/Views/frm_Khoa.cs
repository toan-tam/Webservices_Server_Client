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
using Client.Models;

namespace Client.Views
{
    public partial class frm_Khoa : Form
    {
        // enum variable used for select option
        private Option option = Option.Nodata;

        // uses for save data from all textboxs
        private QLKhoa_ServiceReference.Khoa_ett khoa = new QLKhoa_ServiceReference.Khoa_ett();

        //  set "khoa" variable value
        private void get_infor_khoa()
        {
            khoa.diachi = txt_diachi.Text;
            khoa.dienthoai = txt_dienthoai.Text;
            khoa.makhoa = txt_makhoa.Text;
            khoa.tenkhoa = txt_tenkhoa.Text;
        }

        // update data for datagridview
        private void load_data()
        {
            dtgv_khoa.ForeColor = System.Drawing.Color.Black;
            var temp = cl.selectAllKhoa();
            switch (temp.errorCode)
            {
                case QLKhoa_ServiceReference.ErrorCode.Sucess:
                    dtgv_khoa.DataSource = temp.data;
                    used_for_headerclick_dtgv = temp.data;
                    Utils.chang_title_datagridViewCellKhoa(dtgv_khoa);
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

        QLKhoa_ServiceReference.Khoa_ett[] used_for_headerclick_dtgv;

        public frm_Khoa()
        {
            InitializeComponent();
            Utils.readOnly_text_khoa(txt_diachi, txt_dienthoai, txt_makhoa, txt_tenkhoa);
        }

        private void frm_Khoa_Load(object sender, EventArgs e)
        {
            load_data();
             
            List<how_to_search> dt_source = new List<how_to_search>();
            dt_source.Add(new how_to_search("Mã khoa", "makhoa"));
            dt_source.Add(new how_to_search("Tên khoa", "tenkhoa"));
            dt_source.Add(new how_to_search("Địa chỉ", "diachi"));
            dt_source.Add(new how_to_search("Điện thoại", "dienthoai"));

            cbx_option_search.DataSource = dt_source;
            cbx_option_search.DisplayMember = "value";
            cbx_option_search.ValueMember = "key";
            cbx_option_search.SelectedIndex = 1;

            current_page = 1; // constructed value for current_pages
            set_btn_paging();
            Update_dtg_data();
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
            Utils.erase_text_khoa(txt_diachi, txt_dienthoai, txt_makhoa, txt_tenkhoa);
            Utils.readOnly_text_khoa(txt_diachi, txt_dienthoai, txt_makhoa, txt_tenkhoa);
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            Utils.erase_text_khoa(txt_diachi, txt_dienthoai, txt_makhoa, txt_tenkhoa);
            Utils.not_readOnly_text_khoa(txt_diachi, txt_dienthoai, txt_makhoa, txt_tenkhoa);
            txt_makhoa.Focus();
            option = Option.Insert;
        }

        private void dtgv_khoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow temp = dtgv_khoa.Rows[e.RowIndex];
                if (temp.Cells[0].Value.ToString() != null)
                {
                    txt_makhoa.Text = temp.Cells[0].Value.ToString();
                }
                else txt_makhoa.Text = "";
                if (temp.Cells[1].Value.ToString() != null)
                {
                    txt_tenkhoa.Text = temp.Cells[1].Value.ToString();
                }
                else
                    txt_tenkhoa.Text = "";
                if (temp.Cells[2].Value.ToString() != null)
                {
                    txt_diachi.Text = temp.Cells[2].Value.ToString();
                }
                else
                    txt_diachi.Text = "";

                if (temp.Cells[3].Value.ToString() != null)
                {
                    txt_dienthoai.Text = temp.Cells[3].Value.ToString();
                }
                else
                    txt_dienthoai.Text = "";
            }
        }

        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {

            var select_cbx = cbx_option_search.SelectedValue.ToString();
            var temp = cl.selectByFieldsKhoa(txt_timkiem.Text, select_cbx);
            switch (temp.errorCode)
            {
                case QLKhoa_ServiceReference.ErrorCode.Sucess:
                    dtgv_khoa.DataSource = temp.data;
                    used_for_headerclick_dtgv = temp.data;
                    Utils.chang_title_datagridViewCellKhoa(dtgv_khoa);
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
                    get_infor_khoa();
                    if (khoa.makhoa == "")
                    {
                        MessageBox.Show(Constants.error_insert_nodata_makhoa);
                        txt_makhoa.Focus();
                    }

                    break;

                case Option.Insert:
                    get_infor_khoa();
                    if (khoa.makhoa == "")
                    {
                        MessageBox.Show(Constants.error_null_makhoa);
                        txt_makhoa.Focus();
                        break;
                    }

                    bool check = true; // check if existing data
                    var data = dtgv_khoa.Rows;
                    foreach (DataGridViewRow item in data)
                    {
                        if (khoa.makhoa == item.Cells[0].Value.ToString())
                        {
                            check = false;
                        }
                    }
                    if (!check)
                    {
                        MessageBox.Show(Constants.error_duplicate_makhoa); //if existing return error
                        txt_makhoa.Focus();
                        break;
                    }

                    var temp = cl.insertKhoa(khoa.makhoa, khoa.tenkhoa, khoa.diachi, khoa.dienthoai);
                    switch (temp.errorCode)
                    {
                        case QLKhoa_ServiceReference.ErrorCode.Sucess:
                            MessageBox.Show(Constants.success_insert);
                            load_data();
                            Utils.erase_text_khoa(txt_makhoa, txt_tenkhoa, txt_diachi, txt_dienthoai);
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
                    get_infor_khoa();
                    bool check1 = true; // check if existing data
                    var data1 = dtgv_khoa.Rows;
                    foreach (DataGridViewRow item in data1)
                    {

                        if (item.Cells[0].Value.ToString() == khoa.makhoa)
                        {
                            check1 = false;
                        }
                    }
                    if (check1)
                    {
                        MessageBox.Show(Constants.error_edit_makhoa); // if not existing return error
                        txt_makhoa.Focus();
                        break;
                    }
                    else
                    {
                        var temp1 = cl.editKhoa(khoa.makhoa, khoa.tenkhoa, khoa.diachi, khoa.dienthoai);
                        switch (temp1.errorCode)
                        {
                            case QLKhoa_ServiceReference.ErrorCode.Sucess:
                                MessageBox.Show(Constants.success_edit);
                                load_data();
                                Utils.erase_text_khoa(txt_makhoa, txt_tenkhoa, txt_diachi, txt_dienthoai);
                                Utils.readOnly_text_khoa(txt_makhoa, txt_tenkhoa, txt_diachi, txt_dienthoai);
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
            Utils.not_readOnly_text_khoa(txt_diachi, txt_dienthoai, txt_makhoa, txt_tenkhoa);
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
                        var temp = cl.deleteKhoa(item.Cells[0].Value.ToString());                      
                        switch (temp.errorCode)
                        {
                            case QLKhoa_ServiceReference.ErrorCode.Sucess:
                                Utils.erase_text_khoa(txt_makhoa, txt_tenkhoa, txt_diachi, txt_dienthoai);
                                Utils.readOnly_text_khoa(txt_makhoa, txt_tenkhoa, txt_diachi, txt_dienthoai);
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

        private bool[] how_to_sort = new bool[4] { true, true, true, true };
        private void dtgv_khoa_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int which_to_sort = e.ColumnIndex;
            switch (which_to_sort)
            {
                case 0:
                    if (how_to_sort[0])
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderBy(o => o.makhoa).ToArray();
                        how_to_sort[0] = false;
                    }
                    else
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderByDescending(o => o.makhoa).ToArray();
                        how_to_sort[0] = true;
                    }
                    break;
                case 1:
                    if (how_to_sort[1])
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderBy(o => o.tenkhoa).ToArray();
                        how_to_sort[1] = false;
                    }
                    else
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderByDescending(o => o.tenkhoa).ToArray();
                        how_to_sort[1] = true;
                    }
                    break;
                case 2:
                    if (how_to_sort[2])
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderBy(o => o.diachi).ToArray();
                        how_to_sort[2] = false;
                    }
                    else
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderByDescending(o => o.diachi).ToArray();
                        how_to_sort[2] = true;
                    }
                    break;
                case 3:
                    if (how_to_sort[3])
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderBy(o => o.dienthoai).ToArray();
                        how_to_sort[3] = false;
                    }
                    else
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderByDescending(o => o.dienthoai).ToArray();
                        how_to_sort[3] = true;
                    }
                    break;
                default:
                    break;
            }
        }

        #region "paging"
        public int current_page = -1;  //giá trị lưu trữ trang hiện tại đang xem
        public int page_total = -1;    //lưu trữ tổng số của phân trang
        // update data for dtgv_khoa;  
        public void Update_dtg_data()
        {
            
        }

        public void set_btn_paging()
        {
            // xóa trắng groupbox trước khi chèn mới button
            groupBox2.Controls.Clear();

            // get page_total
            page_total = cl.get_page_total_of_khoa();
        }
        #endregion
    }
}
