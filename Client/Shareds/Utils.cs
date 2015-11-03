using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Client.Shareds
{
    public class Utils
    {
        public static void add_form_to_panel(Form f, Panel p)
        {
            p.Controls.Clear();
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            p.Controls.Add(f);
            f.Show();     
        }

        public static DataTable toDatatable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            // get all properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                // setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    // inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item);
                }
                dataTable.Rows.Add(values);
            }
            // put a breakpoint here and check datatable
            return dataTable ;
        }
        #region "textbox of khoa"
        public static void erase_text_khoa(TextBox t1, TextBox t2, TextBox t3, TextBox t4)
        {
            t1.Text = t2.Text = t3.Text = t4.Text = "";
        }

        public static void readOnly_text_khoa(TextBox t1, TextBox t2, TextBox t3, TextBox t4)
        {
            t1.ReadOnly = t2.ReadOnly = t3.ReadOnly = t4.ReadOnly = true;
        }

        public static void not_readOnly_text_khoa(TextBox t1, TextBox t2, TextBox t3, TextBox t4)
        {
            t1.ReadOnly = t2.ReadOnly = t3.ReadOnly = t4.ReadOnly = false;
        }
        #endregion
        #region "textbox of sinhvien"
        public static void erase_text_sinhvien(TextBox t1, TextBox t2, TextBox t3, ComboBox t4)
        {
            t1.Text = t2.Text = t3.Text = t4.Text = "";
        }

        public static void readOnly_text_sinhvien(TextBox t1, TextBox t2, TextBox t3)
        {
            t1.ReadOnly = t2.ReadOnly = t3.ReadOnly = true;
        }

        public static void not_readOnly_text_sinhvien(TextBox t1, TextBox t2, TextBox t3)
        {
            t1.ReadOnly = t2.ReadOnly = t3.ReadOnly = false;
        }
        #endregion
        #region "textbox of diem"
        public static void erase_text_diem(ComboBox t1, ComboBox t2, TextBox t3)
        {
            t1.Text = t2.Text = t3.Text = "";
        }

        public static void readOnly_text_diem(ComboBox t1, ComboBox t2, TextBox t3)
        {
            t3.ReadOnly = true;
        }

        public static void not_readOnly_text_diem(ComboBox t1, ComboBox t2, TextBox t3)
        {
            t3.ReadOnly = false;
        }
        #endregion
        #region "chang headertext_datagridview"
        public static void chang_title_datagridViewCellKhoa(DataGridView dtgvIn)
        {
            dtgvIn.ColumnHeadersVisible = true;
            dtgvIn.Columns[0].HeaderText = "Mã khoa";
            dtgvIn.Columns[1].HeaderText = "Tên khoa";
            dtgvIn.Columns[2].HeaderText = "Địa chỉ";
            dtgvIn.Columns[3].HeaderText = "Điện thoại";
        }

        public static void chang_title_datagridViewCellSinhVien(DataGridView dtgvIn)
        {
            dtgvIn.ColumnHeadersVisible = true;
            dtgvIn.Columns[0].HeaderText = "Mã SV";
            dtgvIn.Columns[1].HeaderText = "Tên SV";
            dtgvIn.Columns[2].HeaderText = "Nơi sinh";
            dtgvIn.Columns[3].HeaderText = "Khoa";
        }
        public static void chang_title_datagridViewCellMon(DataGridView dtgvIn)
        {
            dtgvIn.ColumnHeadersVisible = true;
            dtgvIn.Columns[0].HeaderText = "Mã môn";
            dtgvIn.Columns[1].HeaderText = "Tên môn";
        }
        public static void chang_title_datagridViewCellDiem(DataGridView dtgvIn)
        {
            dtgvIn.ColumnHeadersVisible = true;
            dtgvIn.Columns[0].HeaderText = "Sinh viên";
            dtgvIn.Columns[1].HeaderText = "Môn";
            dtgvIn.Columns[2].HeaderText = "Điểm";

        }
        #endregion
        #region "dialog"
        public static bool switch_false()
        {
            return MessageBox.Show(Constants.error, Constants.warning_caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }
        public static bool confirm_delete()
        {
            return MessageBox.Show(Constants.confirm_delete, Constants.warning_caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }
        public static bool confirm_exit()
        {
            return MessageBox.Show(Constants.confirm_exit, Constants.warning_caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }
        #endregion
    }
}
