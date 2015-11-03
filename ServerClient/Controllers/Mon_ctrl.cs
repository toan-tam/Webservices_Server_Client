using ServerClient.Models;
using ServerClient.Shareds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerClient.Controllers
{
    public class Mon_ctrl
    {
        public Mon_ctrl() { }

        QLKhoaDataContext db = new QLKhoaDataContext();

        // handle manipulations
        public Result<List<Mon_ett>> select_all_mon()
        {
            Result<List<Mon_ett>> rs = new Result<List<Mon_ett>>();
            try
            {
                List<Mon_ett> lst = new List<Mon_ett>();
                // remember : linq select all table "tbl_mon"
                var dt = db.tbl_mons;

                if (dt.Count() > 0)
                {
                    foreach (tbl_mon item in dt)
                    {
                        Mon_ett temp = new Mon_ett(item);
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

        public Result<List<Mon_ett>> select_by_fields(string nameInput, string how_to_search)
        {
            Result<List<Mon_ett>> rs = new Result<List<Mon_ett>>();
            try
            {
                IQueryable<tbl_mon> dt = null;
                List<Mon_ett> lst = new List<Mon_ett>();
                switch (how_to_search)
                {
                    case "mamon":
                        dt = db.tbl_mons.Where(o => o.mamon.Contains(nameInput));
                        break;
                    case "tenmon":
                        dt = db.tbl_mons.Where(o => o.tenmon.Contains(nameInput));
                        break;
                    default:
                        break;
                }

                if (dt.Count() > 0)
                {
                    foreach (tbl_mon item in dt)
                    {
                        Mon_ett temp = new Mon_ett(item);
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
        public Result<bool> insert_mon(Mon_ett mon)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                // create new tbl_khoa to insert to datacontext
                tbl_mon temp = new tbl_mon();
                temp.mamon = mon.mamon;
                temp.tenmon = mon.tenmon;

                // remember : linq insert data 
                db.tbl_mons.InsertOnSubmit(temp);
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

        public Result<bool> delete_mon(string maInput)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                // LINQ : select + where
                var dt = db.tbl_mons.Where(o => o.mamon == maInput);
                // delete diem
                // delete mon
                if (dt.Count() > 0)
                {
                    foreach (tbl_mon i in dt)
                    {
                        var dt_diem = db.tbl_diems.Where(o => o.mamon == i.mamon);
                        if (dt_diem.Count() > 0)
                        {
                            foreach (tbl_diem item in dt_diem)
                            {
                                db.tbl_diems.DeleteOnSubmit(item);
                            }
                        }
                        db.tbl_mons.DeleteOnSubmit(i);
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

        public Result<bool> edit_mon(string ma, string ten)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                // find the only row to edit
                var dt = db.tbl_mons.Where(o => o.mamon == ma).SingleOrDefault();

                // if fields is null or "" then maintain the old data
                if (ten != "" && ten != null)
                {
                    dt.tenmon = ten;
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