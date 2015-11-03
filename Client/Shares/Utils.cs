using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace ServerClient.Shares
{
    class Utils
    {
        public static void add_form_to_panel(Form f, Panel p)
        {
            p.Controls.Clear();
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            p.Controls.Add(f);
            f.Show();
        }
    }
}