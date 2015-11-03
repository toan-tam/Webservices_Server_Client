using ServerClient.Models;
using ServerClient.Shareds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerClient.Controllers
{
    public class Khoa_ctrl
    {
        public int page_total_khoa = -1;


        public Khoa_ctrl(){
            int record_total = db.tbl_khoas.Count();
            if (record_total % Constants.num_record_on_page == 0)
            {
                page_total_khoa = record_total / Constants.num_record_on_page;
            }
            else
            {
                page_total_khoa = record_total / Constants.num_record_on_page + 1;
            }
        }

        QLKhoaDataContext db = new QLKhoaDataContext();

        // handle manipulations
        public Result<List<Khoa_ett>> select_all_khoa()
        {
            Result<List<Khoa_ett>> rs = new Result<List<Khoa_ett>>();
            try
            {
                List<Khoa_ett> lst = new List<Khoa_ett>();
                // remember : linq select all table "tbl_khoa"
                var dt = db.tbl_khoas;

                if (dt.Count() > 0)
                {
                    foreach (tbl_khoa item in dt)
                    {
                        Khoa_ett temp = new Khoa_ett(item);
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

        public Result<List<Khoa_ett>> select_by_fields(string nameInput, string how_to_search)
        {
            Result<List<Khoa_ett>> rs = new Result<List<Khoa_ett>>();
            try
            {
                IQueryable<tbl_khoa> dt = null;
                List<Khoa_ett> lst = new List<Khoa_ett>();
                switch (how_to_search)
                {
                    case "makhoa":
                        dt = db.tbl_khoas.Where(o => o.makhoa.Contains(nameInput));
                        break;
                    case "tenkhoa":
                        dt = db.tbl_khoas.Where(o => o.tenkhoa.Contains(nameInput));
                        break;
                    case "diachi":
                        dt = db.tbl_khoas.Where(o => o.diachi.Contains(nameInput));
                        break;
                    case "dienthoai":
                        dt = db.tbl_khoas.Where(o => o.dienthoai.Contains(nameInput));
                        break;
                    default:
                        break;
                }
 
                if (dt.Count() > 0)
                {
                    foreach (tbl_khoa item in dt)
                    {
                        Khoa_ett temp = new Khoa_ett(item);
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
        public Result<bool> insert_khoa(Khoa_ett khoa)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                // create new tbl_khoa to insert to datacontext
                tbl_khoa temp = new tbl_khoa();
                temp.makhoa = khoa.makhoa;
                temp.tenkhoa = khoa.tenkhoa;
                temp.diachi = khoa.diachi;
                temp.dienthoai = khoa.dienthoai;

                // remember : linq insert data 
                db.tbl_khoas.InsertOnSubmit(temp);
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

        public Result<bool> delete_khoa(string maInput)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                // LINQ : select + where
                var dt = db.tbl_khoas.Where(o => o.makhoa == maInput);
                //LINQ : delete
                //delete tbl_diem
                //delete tbl_sinhvien
                //delete tbl_khoa
                if (dt.Count() > 0)
                {
                    foreach (var i in dt)
                    {
                        var dt_sv = db.tbl_sinhviens.Where(o => o.makhoa == i.makhoa);
                        if (dt_sv.Count() > 0)
                        {
                            foreach (tbl_sinhvien item in dt_sv)
                            {
                                var dt_diem = db.tbl_diems.Where(o => o.masv == item.masv);
                                if (dt_diem.Count() > 0)
                                {
                                    foreach (var item1 in dt_diem)
                                    {
                                        db.tbl_diems.DeleteOnSubmit(item1);
                                    }
                                }
                                db.tbl_sinhviens.DeleteOnSubmit(item);
                            }
                        }
                        db.tbl_khoas.DeleteOnSubmit(i);
                        db.SubmitChanges();
                        rs.data = true;
                        rs.errorCode = ErrorCode.Sucess;
                    }
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

        public Result<bool> edit_khoa(string ma, string ten, string dc, string dthoai)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                // find the only row to edit
                var dt = db.tbl_khoas.Where(o => o.makhoa == ma).SingleOrDefault();

                // if fields is null or "" then maintain the old data
                    if (ten != "" && ten != null)
                    {
                        dt.tenkhoa = ten;
                    }
                    if (dc != "" && dc != null)
                    {
                        dt.diachi = dc;
                    }
                    if (dthoai != "" && dthoai != null)
                    {
                        dt.dienthoai = dthoai;
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

        public Result<List<Khoa_ett>> select_by_paging_khoa(int current_page)
        {
            Result<List<Khoa_ett>> rs = new Result<List<Khoa_ett>>();
            try
            {
                List<Khoa_ett> lst = new List<Khoa_ett>();
                //check whether curren_page is out of range or not
                if (current_page <= 0)
                {
                    current_page = 1;
                }
                if (current_page > page_total_khoa)
                {
                    current_page = page_total_khoa;
                }
                // index of beginning record
                int num_skip_record = Constants.num_record_on_page * (current_page - 1);
                // get all the record correspondly
                var dt = db.tbl_khoas.Skip(num_skip_record).Take(Constants.num_record_on_page);

                if (dt.Count() > 0)
                {
                    foreach (tbl_khoa item in dt)
                    {
                        Khoa_ett temp = new Khoa_ett(item);
                        lst.Add(temp);
                    }
                    rs.data = lst;
                    rs.errorCode = ErrorCode.Sucess;
                }
                else
                {
                    rs.data = null;
                    rs.errorInfor = Constants.empty_data;
                }
            }
            catch (Exception e)
            {
                rs.data = null;
                rs.errorCode = ErrorCode.False;
                rs.errorInfor = e.ToString();
            }
            return rs;
        }
    }
}