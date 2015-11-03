using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ServerClient.Controllers;
using ServerClient.Models;

namespace ServerClient
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        #region "khoa"
        [WebMethod]
        public Result<List<Khoa_ett>> selectAllKhoa()
        {
            Khoa_ctrl temp = new Khoa_ctrl();
            return temp.select_all_khoa();
        }

        [WebMethod]
        public Result<List<Khoa_ett>> selectByFieldsKhoa(string inputName, string how)
        {
            Khoa_ctrl temp = new Khoa_ctrl();
            return temp.select_by_fields(inputName, how);
        }
        [WebMethod]
        public Result<bool> insertKhoa(string ma, string ten, string dc, string dthoai)
        {
            Khoa_ctrl temp = new Khoa_ctrl();
            return temp.insert_khoa(new Khoa_ett(ma, ten, dc, dthoai));
        }
        [WebMethod]
        public Result<bool> deleteKhoa(string ma)
        {
            Khoa_ctrl temp = new Khoa_ctrl();
            return temp.delete_khoa(ma);
        }
        [WebMethod]
        public Result<bool> editKhoa(string ma, string ten, string dc, string dthoai)
        {
            Khoa_ctrl temp = new Khoa_ctrl();
            return temp.edit_khoa(ma, ten, dc, dthoai);
        }
        [WebMethod]
        public Result<List<Khoa_ett>> selectByPagingKhoa(int current_page)
        {
            Khoa_ctrl temp = new Khoa_ctrl();
            return temp.select_by_paging_khoa(current_page);
        }
        [WebMethod]
        public int get_page_total_of_khoa()
        {
            Khoa_ctrl temp = new Khoa_ctrl();
            return temp.page_total_khoa;
        }
        #endregion
        #region "mon"
        [WebMethod]
        public Result<List<Mon_ett>> selectAllMon()
        {
            Mon_ctrl temp = new Mon_ctrl();
            return temp.select_all_mon();
        }
        [WebMethod]
        public Result<List<Mon_ett>> selectByFieldsMon(string inputName, string how)
        {
            Mon_ctrl temp = new Mon_ctrl();
            return temp.select_by_fields(inputName, how);
        }
        [WebMethod]
        public Result<bool> insertMon(string ma, string ten)
        {
            Mon_ctrl temp = new Mon_ctrl();
            return temp.insert_mon(new Mon_ett(ma, ten));
        }
        [WebMethod]
        public Result<bool> deleteMon(string ma)
        {
            Mon_ctrl temp = new Mon_ctrl();
            return temp.delete_mon(ma);
        }
        [WebMethod]
        public Result<bool> editMon(string ma, string ten)
        {
            Mon_ctrl temp = new Mon_ctrl();
            return temp.edit_mon(ma, ten);
        }
        #endregion
        #region "sinhvien"
        [WebMethod]
        public Result<List<SinhVien_ett>> selectAllSinhVien()
        {
            SinhVien_ctrl temp = new SinhVien_ctrl();
            return temp.select_all_sinhvien();
        }

        [WebMethod]
        public Result<List<SinhVien_ett>> selectByFieldsSinhVien(string inputName, string how)
        {
            SinhVien_ctrl temp = new SinhVien_ctrl();
            return temp.select_by_fields(inputName, how);
        }
        [WebMethod]
        public Result<bool> insertSinhVien(string ma, string ten, string ns, string mk)
        {
            SinhVien_ctrl temp = new SinhVien_ctrl();
            return temp.insert_sinhvien(new SinhVien_ett(ma, ten, ns, mk));
        }
        [WebMethod]
        public Result<bool> deleteSinhVien(string ma)
        {
            SinhVien_ctrl temp = new SinhVien_ctrl();
            return temp.delete_sinhvien(ma);
        }
        [WebMethod]
        public Result<bool> editSinhVien(string ma, string ten, string ns, string mk)
        {
            SinhVien_ctrl temp = new SinhVien_ctrl();
            return temp.edit_sinhvien(ma, ten, ns, mk);
        }
        [WebMethod]
        public Result<List<SinhVienCbx_ett>> selectAllSinhVienCbx()
        {
            SinhVien_ctrl temp = new SinhVien_ctrl();
            return temp.select_all_sinhvien_cbx();
        }
        #endregion
        #region "diem"
        [WebMethod]
        public Result<List<Diem_ett>> selectAllDiem()
        {
            Diem_ctrl temp = new Diem_ctrl();
            return temp.select_all_diem();
        }

        [WebMethod]
        public Result<List<Diem_ett>> selectByFieldsDiem(string inputName, string how)
        {
            Diem_ctrl temp = new Diem_ctrl();
            return temp.select_by_fields(inputName, how);
        }
        [WebMethod]
        public Result<bool> insertDiem(string masv, string mamon, string diem)
        {
            Diem_ctrl temp = new Diem_ctrl();
            return temp.insert_diem(masv, mamon, diem);
        }
        [WebMethod]
        public Result<bool> deleteDiem(string masv, string mamon)
        {
            Diem_ctrl temp = new Diem_ctrl();
            return temp.delete_diem(masv, mamon);
        }
        [WebMethod]
        public Result<bool> editDiem(string masv, string mamon, string diem)
        {
            Diem_ctrl temp = new Diem_ctrl();
            return temp.edit_diem(masv, mamon, diem);
        }
        #endregion
    }
}
