using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerClient.Models
{
    public class SinhVienCbx_ett
    {
        public string masv { get; set; }
        public string hoten { get; set; }
        public string noisinh { get; set; }
        public string makhoa { get; set; }
        public string matensv { get; set; }

        public SinhVienCbx_ett() { }

        public SinhVienCbx_ett(tbl_sinhvien o, string matensv)
        {
            masv = o.masv;
            hoten = o.hoten;
            noisinh = o.noisinh;
            makhoa = o.makhoa;
            this.matensv = matensv;
        }

    }
}