using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Sinh_Vien.DTO__DATA_TRANSFER_OBJECT_
{
   public class Lop
    {
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public string MaKhoa { get; set; }
        public Lop() { }
        public Lop(string maLop, string tenLop, string maKhoa)
        {
            MaLop = maLop;
            TenLop = tenLop;
            MaKhoa = maKhoa;
        }
    }
}
