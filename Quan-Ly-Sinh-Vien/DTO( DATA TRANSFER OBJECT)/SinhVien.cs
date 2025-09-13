using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Sinh_Vien.DTO__DATA_TRANSFER_OBJECT_;

namespace Quan_Ly_Sinh_Vien.DTO__DATA_TRANSFER_OBJECT_
{
    public class SinhVien
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public string Lop { get; set; }
        public SinhVien() { }
        public SinhVien(string maSV, string hoTen, DateTime ngaySinh, string gioiTinh, string sDT, string diaChi, string lop)
        {
            MaSV = maSV;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            SDT = sDT;
            DiaChi = diaChi;
            Lop = lop;
        }
    }
}
