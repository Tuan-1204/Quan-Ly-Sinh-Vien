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
    public class KetQuaDAO
    {
        public static List<KetQua> GetAll()
        {
            List<KetQua> list = new List<KetQua>();
            try
            {
                DataTable dt = DataProvider.LoadCSDL("SELECT * FROM KetQua");
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new KetQua
                    {
                        MaSV = row["MaSV"].ToString(),
                        MaMH = row["MaMH"].ToString(),
                        DiemLan1 = (float)(row["DiemLan1"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(row["DiemLan1"])),
                        DiemThiLai = (float)(row["DiemThiLai"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(row["DiemThiLai"]))
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi KetQuaDAO.GetAll: " + ex.Message);
            }
            return list;
        }
    }
}
