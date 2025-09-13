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
   public class LopDAO
    {
        public static List<Lop> GetAll()
        {
            List<Lop> list = new List<Lop>();
            try
            {
                DataTable dt = DataProvider.LoadCSDL("SELECT * FROM Lop");
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Lop
                    {
                        MaLop = row["MaLop"].ToString(),
                        TenLop = row["TenLop"].ToString(),
                        MaKhoa = row["MaKhoa"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi LopDAO.GetAll: " + ex.Message);
            }
            return list;
        }
    }
}
