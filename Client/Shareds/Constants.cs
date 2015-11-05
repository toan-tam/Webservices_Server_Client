using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Shareds
{
    class Constants
    {
        public static string  confirm_delete = "Do you really want to delete this record ?";
        public static string  confirm_exit = "Do you really want to exit ?";
        public static string  empty_data = "Empty data !";
        public static string  error = "Error occurs ! Do you want to see the problem ?";
        public static string  error_insert_nodata_makhoa = "You must input value for Ma khoa !";
        public static string  error_insert_nodata_masv = "You must input value for Ma khoa !";
        public static string  error_insert_nodata_mamon = "You must input value for Ma khoa !";
        public static string  error_duplicate_makhoa = "Duplicate data! you should change the Ma khoa field";
        public static string  error_duplicate_masv = "Duplicate data! you should change the Ma khoa field";
        public static string  error_duplicate_mamon = "Duplicate data! you should change the Ma khoa field";
        public static string  error_duplicate_masv_mamon = "Duplicate data! you should change the Ma khoa field or Ma SV field";
        public static string  error_null_makhoa = "Ma khoa must be typed !";
        public static string  error_null_masv = "Ma sinh vien must be typed !";
        public static string  error_null_mamon = "Ma mon must be typed !";
        public static string  error_edit_makhoa = "Ma khoa must have in table !";
        public static string  error_edit_masv = "Ma sinh vien must have in table !";
        public static string  error_edit_mamon = "Ma mon must have in table !";
        public static string  error_edit_masv_mamon = "Ma mon and Ma SV must have in table !";
        public static string  error_delete_nothing_makhoa = "Ma khoa must have in table !";
        public static string  error_delete_nothing_masv = "Ma khoa must have in table !";
        public static string  error_delete_nothing_mamon = "Ma khoa must have in table !";
        public static string  error_delete_nothing_masv_mamon = "Ma khoa and Ma SV must have in table !";
        public static string  success_insert = "Success insert !";
        public static string  success_edit = "Success edit !";
        public static string  success_delete = "Success delete !";
        public static string  warning_caption = "Warning !";
        public static string error_not_list_khoa = "Error ! you have to input the right data!";

        public static int btn_width = 35;
        public static int btn_height = 30;

    }
}
