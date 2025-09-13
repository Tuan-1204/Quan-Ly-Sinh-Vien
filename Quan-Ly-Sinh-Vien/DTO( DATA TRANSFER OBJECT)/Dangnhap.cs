using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Sinh_Vien.DTO__DATA_TRANSFER_OBJECT_
{
   public class Dangnhap
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string Quyen { get; set; }
        public Dangnhap() { }
        public Dangnhap(string tenDangNhap, string matKhau, string hoTen, string quyen)
        {
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            HoTen = hoTen;
            Quyen = quyen;
        }



    }
}
