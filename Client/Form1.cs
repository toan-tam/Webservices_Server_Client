using Client.Shareds;
using Client.Views;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void khoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Khoa temp = new frm_Khoa();
            Utils.add_form_to_panel(temp, panel1);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utils.confirm_exit())
            {
                Application.Exit();
            }
        }

        private void sinhVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_SinhVien temp = new frm_SinhVien();
            Utils.add_form_to_panel(temp, panel1);
        }

        private void mônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Mon temp = new frm_Mon();
            Utils.add_form_to_panel(temp, panel1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frm_Diem temp = new frm_Diem();
            Utils.add_form_to_panel(temp, panel1);
        }
    }
}
