using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerClient.Models
{
    public class SinhVien_ett
    {
        public string masv { get; set; }
        public string hoten { get; set; }
        public string noisinh { get; set; }
        public string makhoa { get; set; }

        public SinhVien_ett() { }

        public SinhVien_ett(tbl_sinhvien sv)
        {
            masv = sv.masv;
            hoten = sv.hoten;
            noisinh = sv.noisinh;
            makhoa = sv.makhoa;
        }

        public SinhVien_ett(string ma, string ht, string ns, string mk)
        {
            masv = ma;
            hoten = ht;
            noisinh = ns;
            makhoa = mk;
        }
    }
}