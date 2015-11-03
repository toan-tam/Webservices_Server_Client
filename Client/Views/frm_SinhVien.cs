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
using System.Data.Linq.SqlClient;

namespace Client.Views
{
    public partial class frm_SinhVien : Form
    {
        // enum variable used for select option
        private Option option = Option.Nodata;

        // uses for save data from all textboxs
        private QLKhoa_ServiceReference.SinhVien_ett sinhvien = new QLKhoa_ServiceReference.SinhVien_ett();

        //  set "sinhvien" variable value
        private void get_infor_sinhvien()
        {
            sinhvien.masv = txt_maSV.Text;
            sinhvien.hoten = txt_tenSV.Text;
            sinhvien.noisinh = txt_noisinh.Text;
            sinhvien.makhoa = cbx_makhoa.SelectedValue.ToString();
        }

        private List<QLKhoa_ServiceReference.SinhVien_ett> used_for_headerclick_dtgv;

        // update data for datagridview
        private void load_data()
        {
            dtgv_khoa.ForeColor = System.Drawing.Color.Black;
            var temp = cl.selectAllSinhVien();
            switch (temp.errorCode)
            {
                case QLKhoa_ServiceReference.ErrorCode.Sucess:
                    dtgv_khoa.DataSource = temp.data;
                    used_for_headerclick_dtgv = temp.data.ToList();

                    // change value of makhoa to tenkhoa
                    var dtgv = dtgv_khoa.Rows;
                    var dt_khoa = cl.selectAllKhoa();
                    foreach (DataGridViewRow item in dtgv)
                    {
                        foreach (var item1 in dt_khoa.data)
                        {
                            if (item.Cells[3].Value.ToString() == item1.makhoa)
                            {
                                item.Cells[3].Value = item1.tenkhoa;
                            }
                        }
                    }

                    Utils.chang_title_datagridViewCellSinhVien(dtgv_khoa);
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
        public frm_SinhVien()
        {
            InitializeComponent();
            Utils.readOnly_text_sinhvien(txt_maSV, txt_tenSV, txt_noisinh);
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
            Utils.erase_text_sinhvien(txt_maSV, txt_tenSV, txt_noisinh, cbx_makhoa);
            Utils.readOnly_text_sinhvien(txt_maSV, txt_tenSV, txt_noisinh);
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            Utils.erase_text_sinhvien(txt_maSV, txt_tenSV, txt_noisinh, cbx_makhoa);
            Utils.not_readOnly_text_sinhvien(txt_maSV, txt_tenSV, txt_noisinh);
            txt_maSV.Focus();
            option = Option.Insert;
        }

        private void dtgv_khoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow temp = dtgv_khoa.Rows[e.RowIndex];
                if (temp.Cells[0].Value.ToString() != null)
                {
                    txt_maSV.Text = temp.Cells[0].Value.ToString();
                }
                else txt_maSV.Text = "";
                if (temp.Cells[1].Value.ToString() != null)
                {
                    txt_tenSV.Text = temp.Cells[1].Value.ToString();
                }
                else
                    txt_tenSV.Text = "";
                if (temp.Cells[2].Value.ToString() != null)
                {
                    txt_noisinh.Text = temp.Cells[2].Value.ToString();
                }
                else
                    txt_noisinh.Text = "";


                // display data from datagridview to combobox
                var x = cbx_makhoa.Items.Count;
                for (int i = 0; i < x; i++)
                {
                    cbx_makhoa.SelectedIndex = i;
                    if (temp.Cells[3].Value.ToString() == cbx_makhoa.Text)
                    {
                        cbx_makhoa.Text = temp.Cells[3].Value.ToString();
                        break;
                    }
                }

            }
        }

        private void frm_SinhVien_Load(object sender, EventArgs e)
        {
            load_data();

            cbx_makhoa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbx_makhoa.AutoCompleteSource = AutoCompleteSource.ListItems;

            List<how_to_search> dt_source = new List<how_to_search>();
            dt_source.Add(new how_to_search("Mã SV", "masv"));
            dt_source.Add(new how_to_search("Tên SV", "hoten"));
            dt_source.Add(new how_to_search("Nơi sinh", "noisinh"));
            dt_source.Add(new how_to_search("Mã khoa", "makhoa"));

            cbx_option_search.DataSource = dt_source;
            cbx_option_search.DisplayMember = "value";
            cbx_option_search.ValueMember = "key";
            cbx_option_search.SelectedIndex = 1;

            var rs = cl.selectAllKhoa();
            switch (rs.errorCode)
            {
                case QLKhoa_ServiceReference.ErrorCode.Sucess:
                    cbx_makhoa.DataSource = cl.selectAllKhoa().data.ToList();
                    cbx_makhoa.DisplayMember = "tenkhoa";
                    cbx_makhoa.ValueMember = "makhoa";
                    cbx_makhoa.SelectedIndex = -1;

                    break;
                case QLKhoa_ServiceReference.ErrorCode.False:
                    break;
                case QLKhoa_ServiceReference.ErrorCode.NaN:
                    break;
                default:
                    break;
            }                    
        }

        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {

            var select_cbx = cbx_option_search.SelectedValue.ToString();
            var temp = cl.selectByFieldsSinhVien(txt_timkiem.Text, select_cbx);
            switch (temp.errorCode)
            {
                case QLKhoa_ServiceReference.ErrorCode.Sucess:
                    dtgv_khoa.DataSource = temp.data;
                    used_for_headerclick_dtgv = temp.data.ToList();
                    Utils.chang_title_datagridViewCellSinhVien(dtgv_khoa);
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
                    get_infor_sinhvien();
                    if (sinhvien.masv == "")
                    {
                        MessageBox.Show(Constants.error_insert_nodata_masv);
                        txt_maSV.Focus();
                    }

                    break;

                case Option.Insert:
                    get_infor_sinhvien();
                    if (sinhvien.masv == "")
                    {
                        MessageBox.Show(Constants.error_null_masv);
                        txt_maSV.Focus();
                        break;
                    }

                    bool check = true; // check if existing data
                    var data = dtgv_khoa.Rows;
                    foreach (DataGridViewRow item in data)
                    {
                        if (sinhvien.masv == item.Cells[0].Value.ToString())
                        {
                            check = false;
                        }
                    }
                    if (!check)
                    {
                        MessageBox.Show(Constants.error_duplicate_masv); //if existing return error
                        txt_maSV.Focus();
                        break;
                    }

                    var temp = cl.insertSinhVien(sinhvien.masv, sinhvien.hoten, sinhvien.noisinh, sinhvien.makhoa);
                    switch (temp.errorCode)
                    {
                        case QLKhoa_ServiceReference.ErrorCode.Sucess:
                            MessageBox.Show(Constants.success_insert);
                            load_data();
                            Utils.erase_text_sinhvien(txt_maSV, txt_noisinh, txt_tenSV, cbx_makhoa);
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
                    get_infor_sinhvien();
                    bool check1 = true; // check if existing data
                    var data1 = dtgv_khoa.Rows;
                    foreach (DataGridViewRow item in data1)
                    {

                        if (item.Cells[0].Value.ToString() == sinhvien.masv)
                        {
                            check1 = false;
                        }
                    }
                    if (check1)
                    {
                        MessageBox.Show(Constants.error_edit_masv); // if not existing return error
                        txt_maSV.Focus();
                        break;
                    }
                    else
                    {
                        var temp1 = cl.editSinhVien(sinhvien.masv, sinhvien.hoten, sinhvien.noisinh, sinhvien.makhoa);
                        switch (temp1.errorCode)
                        {
                            case QLKhoa_ServiceReference.ErrorCode.Sucess:
                                MessageBox.Show(Constants.success_edit);
                                load_data();
                                Utils.erase_text_sinhvien(txt_maSV, txt_noisinh, txt_tenSV, cbx_makhoa);
                                Utils.readOnly_text_sinhvien(txt_maSV, txt_tenSV, txt_noisinh);
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
            Utils.not_readOnly_text_khoa(txt_maSV, txt_tenSV, txt_maSV, txt_noisinh);
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
                        var temp = cl.deleteSinhVien(item.Cells[0].Value.ToString());
                        switch (temp.errorCode)
                        {
                            case QLKhoa_ServiceReference.ErrorCode.Sucess:
                                Utils.erase_text_sinhvien(txt_maSV, txt_noisinh, txt_tenSV, cbx_makhoa);
                                Utils.readOnly_text_khoa(txt_maSV, txt_tenSV, txt_maSV, txt_noisinh);
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

        private bool[] how_to_sort = new bool[4] { true, true, true, true};

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
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderBy(o => o.hoten).ToArray();
                        how_to_sort[1] = false;
                    }
                    else
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderByDescending(o => o.hoten).ToArray();
                        how_to_sort[1] = true;
                    }
                    break;
                case 2:
                    if (how_to_sort[2])
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderBy(o => o.noisinh).ToArray();
                        how_to_sort[2] = false;
                    }
                    else
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderByDescending(o => o.noisinh).ToArray();
                        how_to_sort[2] = true;
                    }
                    break;
                case 3:
                    if (how_to_sort[3])
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderBy(o => o.makhoa).ToArray();
                        how_to_sort[3] = false;
                    }
                    else
                    {
                        dtgv_khoa.DataSource = used_for_headerclick_dtgv.OrderByDescending(o => o.makhoa).ToArray();
                        how_to_sort[3] = true;
                    }
                    break;
                default:
                    break;
            }
           
        }

        private void cbx_makhoa_Leave(object sender, EventArgs e)
        {
            List<QLKhoa_ServiceReference.Khoa_ett> temp = cbx_makhoa.Items.OfType<QLKhoa_ServiceReference.Khoa_ett>().ToList();

            var x = temp.Where(o => o.tenkhoa == cbx_makhoa.Text);
            if (x.Count() == 0)
            {
                MessageBox.Show(Constants.error_not_list_khoa);
                cbx_makhoa.Focus();
            }
        }
    }
}
