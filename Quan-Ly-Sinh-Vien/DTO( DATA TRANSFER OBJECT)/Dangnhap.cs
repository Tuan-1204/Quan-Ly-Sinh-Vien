using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Sinh_Vien.DAO__DATA_ACCESS_OBJECT_;

namespace Quan_Ly_Sinh_Vien.DTO__DATA_TRANSFER_OBJECT_
{
   public class Dangnhap
    {
        private DataRow row;

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

        public Dangnhap(DataRow row)
        {
            this.row = row;
        }
    }
}
