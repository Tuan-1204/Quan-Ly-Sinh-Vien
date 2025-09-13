using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Sinh_Vien.DTO__DATA_TRANSFER_OBJECT_
{
    public class Khoa
    {
        public string MaKhoa { get; set; }
        public string TenKhoa { get; set; }
        public Khoa() { }
        public Khoa( string maKhoa, string tenKhoa)
        {
            MaKhoa = maKhoa;
            TenKhoa = tenKhoa;
        }
    }
}
