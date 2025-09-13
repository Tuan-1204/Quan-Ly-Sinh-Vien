using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Sinh_Vien.DTO__DATA_TRANSFER_OBJECT_;

namespace Quan_Ly_Sinh_Vien.DTO__DATA_TRANSFER_OBJECT_
{
    public class MonHoc
    {
        public string MaMH { get; set; }
        public string TenMH { get; set; }
        public int SoTiet { get; set; }
        public MonHoc() { }
        public MonHoc(string maMH, string tenMH, int soTiet)
        {
            MaMH = maMH;
            TenMH = tenMH;
           soTiet = SoTiet;
        }
    }
}
