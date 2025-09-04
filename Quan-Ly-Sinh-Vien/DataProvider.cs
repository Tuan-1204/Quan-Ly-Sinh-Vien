using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Quan_Ly_Sinh_Vien
{
    public class DataProvider
    {

        //Khởi tạo kết nối
        const string connString = "Data Source=.\\Sqlexpress;" +
            "Initial Catalog=Quan-Ly-Sinh-Vien;" +
            "Integrated Security=True;";
           

        //Tạo đối tượng kết nối
        private static SqlConnection connection;

        //Tạo list đăng nhập
        public static List<DangNhap> dangNhaps = new List<DangNhap>();
        public static void OpenConnection()
        {
            connection = new SqlConnection(connString); //khởi tạo data base
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public static void CloseConnection()
        {
                if (connection != null && connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }

       //Hàm lấy danh sách đăng nhập và truyền vào list 
       public static void GetAllDangNhap()
        {
            try
            {
                OpenConnection();
                string query = "SELECT * FROM DangNhap";
                SqlCommand command = new SqlCommand(query, connection);
                //đọc dữ liệu
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    DangNhap dangNhap = new DangNhap();
                    dangNhap.TenDangNhap = reader["TenDangNhap"].ToString();
                    dangNhap.MatKhau = reader["MatKhau"].ToString();
                    dangNhap.HoTen = reader["HoTen"].ToString();
                    dangNhap.Quyen = reader["Quyen"].ToString();
                   dangNhaps.Add(dangNhap);
                }
            }
            //bắt lỗi
            catch (Exception ex)
            {
              MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }








    }
}
