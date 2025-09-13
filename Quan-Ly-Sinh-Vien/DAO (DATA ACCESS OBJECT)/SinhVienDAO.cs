using Quan_Ly_Sinh_Vien.DTO__DATA_TRANSFER_OBJECT_;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Sinh_Vien.DAO__DATA_ACCESS_OBJECT_
{
    public class SinhVienDAO
    {
        public static List<SinhVien> GetAll()
        {
            List<SinhVien> list = new List<SinhVien>();
            try
            {
                DataTable dt = DataProvider.LoadCSDL("SELECT * FROM SinhVien");
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new SinhVien
                    {
                        MaSV = row["MaSV"].ToString(),
                        HoTen = row["HoTen"].ToString(),
                        NgaySinh = Convert.ToDateTime(row["NgaySinh"]),
                        GioiTinh = row["GioiTinh"].ToString(),
                        DiaChi = row["DiaChi"].ToString(),
                        SDT = row["SDT"].ToString(),
                        Lop = row["Lop"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SinhVienDAO.GetAll: " + ex.Message);
            }
            return list;
        }

        public static bool Insert(SinhVien sv)
        {
            string query = "INSERT INTO SinhVien(MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, SDT, Lop) " +
                           "VALUES(@MaSV, @HoTen, @NgaySinh, @GioiTinh, @DiaChi, @SDT, @Lop)";
            int result = DataProvider.ThaoTacCSDL(query, new object[] { sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.DiaChi, sv.SDT, sv.Lop });
            return result > 0;
        }

        public static bool Update(SinhVien sv)
        {
            string query = "UPDATE SinhVien SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, " +
                           "DiaChi=@DiaChi, SDT=@SDT, Lop=@Lop WHERE MaSV=@MaSV";
            int result = DataProvider.ThaoTacCSDL(query,
                new object[] { sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.DiaChi, sv.SDT, sv.Lop, sv.MaSV });
            return result > 0;
        }

        public static bool Delete(string maSV)
        {
            string query = "DELETE FROM SinhVien WHERE MaSV=@MaSV";
            int result = DataProvider.ThaoTacCSDL(query, new object[] { maSV });
            return result > 0;
        }
    }
}
