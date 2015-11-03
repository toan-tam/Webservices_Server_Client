using ServerClient.Shares;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ContentForm : Form
    {
        public ContentForm()
        {
            InitializeComponent();
        }

        ServiceReference.WebServiceSoapClient sr = new ServiceReference.WebServiceSoapClient();

        private void ContentForm_Load(object sender, EventArgs e)
        {
            var temp = sr.selectAllData();
            switch (temp.errorCode)
            {
                case ServiceReference.ErrorCode.NaN:
                    break;
                case ServiceReference.ErrorCode.Success:
                    dtgv_content.DataSource = temp.data;
                    dtgv_content.ColumnHeadersVisible = true;
                    dtgv_content.Columns[0].HeaderText = "Mã khoa";
                    dtgv_content.Columns[1].HeaderText = "Tên khoa";
                    dtgv_content.Columns[2].HeaderText = "Mô tả";
                    dtgv_content.Columns[3].HeaderText = "Địa chỉ";
                    dtgv_content.Columns[4].HeaderText = "Địa chỉ";
                    dtgv_content.Columns[5].HeaderText = "Email";

                    break;
                case ServiceReference.ErrorCode.False:
                    if (MessageBox.Show(Constants.error_connect, Constants.warning_caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        MessageBox.Show(temp.errorInfor);
                    }
                    break;
                case ServiceReference.ErrorCode.FakeData:
                    MessageBox.Show(Constants.empty_data);
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var temp = sr.selectByName(txt_name.Text);
            switch (temp.errorCode)
            {
                case ServiceReference.ErrorCode.NaN:
                    break;
                case ServiceReference.ErrorCode.Success:
                    dtgv_content.DataSource = temp.data;
                    break;
                case ServiceReference.ErrorCode.False:
                    if (MessageBox.Show(Constants.error_connect, Constants.warning_caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        MessageBox.Show(temp.errorInfor);
                    }
                    break;
                case ServiceReference.ErrorCode.FakeData:
                  //  MessageBox.Show(Constants.empty_data);
                    break;
                default:
                    break;
            }
        }

        private void dtgv_content_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgv_content.Rows[e.RowIndex];

                Form1 ctrl = (Form1)dtgv_content.FindForm().Parent.FindForm();
                ctrl.txt_makhoa.Text = row.Cells[0].Value.ToString();
                ctrl.txt_tenkhoa.Text = row.Cells[1].Value.ToString();
                ctrl.txt_mota.Text = row.Cells[2].Value.ToString();
                ctrl.txt_diachi.Text = row.Cells[3].Value.ToString();
                ctrl.txt_dienthoai.Text = row.Cells[4].Value.ToString();
                ctrl.txt_email.Text = row.Cells[5].Value.ToString();

                ctrl.Show();
                
            }
        }
    }
}
