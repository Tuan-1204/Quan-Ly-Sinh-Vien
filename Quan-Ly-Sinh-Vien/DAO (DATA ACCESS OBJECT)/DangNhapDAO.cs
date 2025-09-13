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
    public class DangNhapDAO
    {
        public static List<Dangnhap> GetAll()
        {
            List<Dangnhap> list = new List<Dangnhap>();
            try
            {
                DataTable dt = DataProvider.LoadCSDL("SELECT * FROM DangNhap");
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Dangnhap(row));
                    {
                        string TenDangNhap = row["TenDangNhap"].ToString();
                        string HoTen = row["HoTen"].ToString(),
                         MatKhau = row["MatKhau"].ToString(),
                         Quyen = row["Quyen"].ToString();
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi DangNhapDAO.GetAll: " + ex.Message);
            }
            return list;
        }
    }

    
    
}
