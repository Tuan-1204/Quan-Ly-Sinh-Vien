using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Quan_Ly_Sinh_Vien
{
    public class DangNhap
    {
        public string  TenDangNhap { get; set; }    
        public string MatKhau { get; set; }

        public string HoTen { get; set; }
        public string Quyen { get; set; }

      //tạo constructor
        public DangNhap(string tenDangNhap, string matKhau)
        {
            this.TenDangNhap = tenDangNhap;
            this.MatKhau = matKhau;
        }
        public DangNhap(string tenDangNhap, string matKhau, string hoTen, string quyen)
        {
            this.TenDangNhap = tenDangNhap;
            this.MatKhau = matKhau;
            this.HoTen = hoTen;
            this.Quyen = quyen;
        }

        public DangNhap()
        {
        }
    }
}
