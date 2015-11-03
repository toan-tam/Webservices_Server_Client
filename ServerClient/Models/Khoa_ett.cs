using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerClient.Models
{
    public class Khoa_ett
    {
        public string makhoa { get; set; }
        public string tenkhoa { get; set; }
        public string diachi { get; set; }
        public string dienthoai { get; set; }

        public Khoa_ett()
        {

        }

        public Khoa_ett(string ma, string ten, string dc, string dt)
        {
            makhoa = ma;
            tenkhoa = ten;
            diachi = dc;
            dienthoai = dt;
        }

        public Khoa_ett(tbl_khoa khoa)
        {
            makhoa = khoa.makhoa;
            tenkhoa = khoa.tenkhoa;
            diachi = khoa.diachi;
            dienthoai = khoa.dienthoai;
        }
    }
}