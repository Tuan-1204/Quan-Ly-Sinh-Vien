using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Sinh_Vien.DTO__DATA_TRANSFER_OBJECT_
{
    public class KetQua
    {
     public string  MaSV { get; set; }
        public string MaMH { get; set; }
     
        public float DiemLan1 { get; set; }
        public float DiemThiLai { get; set; }
        public KetQua() { }
        public KetQua( string maSV, string maMH,  float diemLan1, float diemThiLai)
        {
            MaSV = maSV;
            MaMH = maMH;
            DiemLan1 = diemLan1;
            DiemThiLai = diemThiLai;
        }

    }
}
