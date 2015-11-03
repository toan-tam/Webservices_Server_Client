using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerClient.Models
{
    public class Mon_ett
    {
        public string mamon { get; set; }
        public string tenmon { get; set; }

        public Mon_ett() { }
        public Mon_ett(tbl_mon mon)
        {
            mamon = mon.mamon;
            tenmon = mon.tenmon;
        }
        public Mon_ett(string ma, string ten)
        {
            mamon = ma;
            tenmon = ten;
        }
    }
}