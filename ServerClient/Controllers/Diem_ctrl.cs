using ServerClient.Models;
using ServerClient.Shareds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerClient.Controllers
{
    public class Diem_ctrl
    {
        public Diem_ctrl() { }

        QLKhoaDataContext db = new QLKhoaDataContext();

        public Result<List<Diem_ett>> select_all_diem()
        {
            Result<List<Diem_ett>> rs = new Result<List<Diem_ett>>();
            try
            {
                List<Diem_ett> lst = new List<Diem_ett>();
                var dt = db.tbl_diems;
                if (dt.Count() > 0)
                {
                    foreach (tbl_diem item in dt)
                    {
                        Diem_ett temp = new Diem_ett(item);
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
            catch (Exception e)
            {
                rs.data = null;
                rs.errorCode = ErrorCode.False;
                rs.errorInfor = e.ToString();
                return rs;
            }
        }

        public Result<List<Diem_ett>> select_by_fields(string input, string how)
        {
            Result<List<Diem_ett>> rs = new Result<List<Diem_ett>>();
            try
            {
                List<Diem_ett> lst = new List<Diem_ett>();
                IQueryable<tbl_diem> dt = null;
                switch (how)
                {
                    case "masv":
                        dt = db.tbl_diems.Where(o => o.masv.Contains(input));
                        break;
                    case "tensv":
                        break;
                    case "mamon":
                        dt = db.tbl_diems.Where(o => o.mamon.Contains(input));
                        break;
                    case "tenmon":
                        break;
                    case "diem":
                        dt = db.tbl_diems.Where(o => o.diem.Contains(input));
                        break;
                    default:
                        break;
                }

                if (dt.Count() > 0)
                {
                    foreach (tbl_diem item in dt)
                    {
                        Diem_ett temp = new Diem_ett(item);
                        lst.Add(temp);
                    }

                    rs.data = lst;
                    rs.errorCode = ErrorCode.Sucess;
                } else
                {
                    rs.data = null;
                    rs.errorCode = ErrorCode.NaN;
                    rs.errorInfor = Constants.empty_data;
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

        public Result<bool> delete_diem(string input_masv,string input_mamon)
        {
            Result<bool> rs = new Result<bool>();

            try
            {
                var dt = db.tbl_diems.Where(o => o.masv == input_masv && o.mamon == input_mamon);
                if (dt.Count() > 0)
                {
                    foreach (tbl_diem item in dt)
                    {
                        db.tbl_diems.DeleteOnSubmit(item);
                    }
                    db.SubmitChanges();
                    rs.data = true;
                    rs.errorCode = ErrorCode.Sucess;
                } else
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

        public Result<bool> insert_diem(string masv, string mamon, string diem)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                tbl_diem temp = new tbl_diem();
                temp.masv = masv;
                temp.mamon = mamon;
                temp.diem = diem;

                db.tbl_diems.InsertOnSubmit(temp);
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

        public Result<bool> edit_diem(string masv, string mamon, string diem)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                var dt = db.tbl_diems.Where(o => o.masv == masv && o.mamon == mamon).SingleOrDefault();
                if (diem != "" && diem != null)
                {
                    dt.diem = diem;
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