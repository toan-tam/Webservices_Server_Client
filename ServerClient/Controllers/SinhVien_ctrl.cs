using ServerClient.Models;
using ServerClient.Shareds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerClient.Controllers
{
    public class SinhVien_ctrl
    {
        public SinhVien_ctrl() { }

        QLKhoaDataContext db = new QLKhoaDataContext();
        // handle manipulations
        public Result<List<SinhVien_ett>> select_all_sinhvien()
        {
            Result<List<SinhVien_ett>> rs = new Result<List<SinhVien_ett>>();
            try
            {
                List<SinhVien_ett> lst = new List<SinhVien_ett>();
                // remember : linq select all table "tbl_sinhvien"
                var dt = db.tbl_sinhviens;

                if (dt.Count() > 0)
                {
                    foreach (tbl_sinhvien item in dt)
                    {
                        SinhVien_ett temp = new SinhVien_ett(item);
                        lst.Add(temp);
                    }
                    rs.data = lst;
                    rs.errorCode = ErrorCode.Sucess;

                }
                else
                {
                    rs.data = null;
                    rs.errorCode = ErrorCode.NaN;
                    rs.errorInfor = Constants.empty_data;
                }
                return rs;
            }
            catch (Exception ex)
            {
                rs.data = null;
                rs.errorInfor = ex.ToString();
                rs.errorCode = ErrorCode.False;
                return rs;
            }

        }

        public Result<List<SinhVienCbx_ett>> select_all_sinhvien_cbx()
        {
            Result<List<SinhVienCbx_ett>> rs = new Result<List<SinhVienCbx_ett>>();
            try
            {
                List<SinhVienCbx_ett> lst = new List<SinhVienCbx_ett>();
                var dt = from o in db.tbl_sinhviens
                         let matensv = o.masv + " | " + o.hoten
                         select new { o, matensv };

                if (dt.Count() > 0)
                {
                    foreach (var item in dt)
                    {
                        SinhVienCbx_ett temp = new SinhVienCbx_ett(item.o, item.matensv);
                        lst.Add(temp);
                    }
                    rs.data = lst;
                    rs.errorCode = ErrorCode.Sucess;

                }
                else
                {
                    rs.data = null;
                    rs.errorCode = ErrorCode.NaN;
                    rs.errorInfor = Constants.empty_data;
                }
                return rs;
            }
            catch (Exception ex)
            {
                rs.data = null;
                rs.errorInfor = ex.ToString();
                rs.errorCode = ErrorCode.False;
                return rs;
            }

        }

        public Result<List<SinhVien_ett>> select_by_fields(string nameInput, string how_to_search)
        {
            Result<List<SinhVien_ett>> rs = new Result<List<SinhVien_ett>>();
            try
            {
                IQueryable<tbl_sinhvien> dt = null;
                List<SinhVien_ett> lst = new List<SinhVien_ett>();
                switch (how_to_search)
                {
                    case "masv":
                        dt = db.tbl_sinhviens.Where(o => o.masv.Contains(nameInput));
                        break;
                    case "hoten":
                        dt = db.tbl_sinhviens.Where(o => o.hoten.Contains(nameInput));
                        break;
                    case "noisinh":
                        dt = db.tbl_sinhviens.Where(o => o.noisinh.Contains(nameInput));
                        break;
                    case "makhoa":
                        dt = db.tbl_sinhviens.Where(o => o.makhoa.Contains(nameInput));
                        break;
                    default:
                        break;
                }

                if (dt.Count() > 0)
                {
                    foreach (tbl_sinhvien item in dt)
                    {
                        SinhVien_ett temp = new SinhVien_ett(item);
                        lst.Add(temp);
                    }
                    rs.errorCode = ErrorCode.Sucess;
                    rs.data = lst;
                }
                else
                {
                    rs.errorCode = ErrorCode.NaN;
                    rs.data = null;
                }
                return rs;
            }
            catch (Exception e)
            {
                rs.data = null;
                rs.errorCode = ErrorCode.False;
                rs.errorInfor = e.ToString();
                return rs;
            }
        }
        public Result<bool> insert_sinhvien(SinhVien_ett sinhvien)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                // create new tbl_khoa to insert to datacontext
                tbl_sinhvien temp = new tbl_sinhvien();
                temp.masv = sinhvien.masv;
                temp.hoten = sinhvien.hoten;
                temp.noisinh = sinhvien.noisinh;
                temp.makhoa = sinhvien.makhoa;

                // remember : linq insert data 
                db.tbl_sinhviens.InsertOnSubmit(temp);
                // remember : all manipulations such as insert, update, delete have to use this command
                db.SubmitChanges();

                rs.data = true;
                rs.errorCode = ErrorCode.Sucess;
                return rs;
            }
            catch (Exception e)
            {
                rs.data = false;
                rs.errorCode = ErrorCode.False;
                rs.errorInfor = e.ToString();
                return rs;
            }
        }

        public Result<bool> delete_sinhvien(string maInput)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                // LINQ : select + where
                var dt = db.tbl_sinhviens.Where(o => o.masv == maInput);
                // delete diem
                // delete sinhvien
                if (dt.Count() > 0)
                {
                    foreach (var i in dt)
                    {
                        var dt_diem = db.tbl_diems.Where(o => o.masv == i.masv);
                        if (dt_diem.Count() > 0)
                        {
                            foreach (tbl_diem item in dt_diem)
                            {
                                db.tbl_diems.DeleteOnSubmit(item);
                            }
                        }
                        db.tbl_sinhviens.DeleteOnSubmit(i);
                        db.SubmitChanges();
                        rs.data = true;
                        rs.errorCode = ErrorCode.Sucess;
                    }
                }
                else
                {
                    rs.data = false;
                    rs.errorCode = ErrorCode.False;
                    rs.errorInfor = Constants.empty_data;
                }
                return rs;
            }
            catch (Exception e)
            {
                rs.data = false;
                rs.errorCode = ErrorCode.False;
                rs.errorInfor = e.ToString();
                return rs;
            }
        }

        public Result<bool> edit_sinhvien(string ma, string ten, string ns, string mk)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                // find the only row to edit
                var dt = db.tbl_sinhviens.Where(o => o.masv == ma).SingleOrDefault();

                // if fields is null or "" then maintain the old data
                if (ten != "" && ten != null)
                {
                    dt.hoten = ten;
                }
                if (ns != "" && ns != null)
                {
                    dt.noisinh = ns;
                }
                if (mk != "" && mk != null)
                {
                    dt.makhoa = mk;
                }

                db.SubmitChanges();
                rs.data = true;
                rs.errorCode = ErrorCode.Sucess;

                return rs;

            }
            catch (Exception e)
            {
                rs.data = false;
                rs.errorCode = ErrorCode.False;
                rs.errorInfor = e.ToString();
                return rs;
            }
        }
    }
}