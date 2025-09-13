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
   public class MonHocDAO
    {
        public static List<MonHoc> GetAll()
        {
            List<MonHoc> list = new List<MonHoc>();
            try
            {
                DataTable dt = DataProvider.LoadCSDL("SELECT * FROM MonHoc");
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new MonHoc
                    {
                        MaMH = row["MaMH"].ToString(),
                        TenMH = row["TenMH"].ToString(),
                        SoTiet = Convert.ToInt32(row["SoTiet"])
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi MonHocDAO.GetAll: " + ex.Message);
            }
            return list;
        }
    }
}
