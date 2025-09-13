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
    public class KhoaDAO
    {
        public static List<Khoa> GetAll()
        {
            List<Khoa> list = new List<Khoa>();
            try
            {
                DataTable dt = DataProvider.LoadCSDL("SELECT * FROM Khoa");
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Khoa
                    {
                        MaKhoa = row["MaKhoa"].ToString(),
                        TenKhoa = row["TenKhoa"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi KhoaDAO.GetAll: " + ex.Message);
            }
            return list;
        }
    }
}
