using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerClient.Models
{
    public class Diem_ett
    {
        public string masv { get; set; }
        public string mamon { get; set; }
        public string diem { get; set; }

        public Diem_ett() { }

        public Diem_ett(tbl_diem diem)
        {
            masv = diem.masv;
            mamon = diem.mamon;
            this.diem = diem.diem;
        }

        public Diem_ett(string masv, string mamon, string diem)
        {
            this.masv = masv;
            this.mamon = mamon;
            this.diem = diem;
        }
    }
}