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
        //Mở kết nối
        public static void OpenConnection()
        {
            connection = new SqlConnection(connString); //khởi tạo data base
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }
        //Đóng kết nối
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
                while (reader.Read())
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

        //Hàm cập nhật mật khẩu
        public static bool UpdateMatKhau(string tenDangNhap, string matKhauMoi)
        {
            try
            {
                OpenConnection();
                string query = "UPDATE DangNhap SET MatKhau = @MatKhauMoi WHERE TenDangNhap = @TenDangNhap";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MatKhauMoi", matKhauMoi);
                command.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0; // Trả về true nếu có ít nhất một hàng bị ảnh hưởng
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }

        public static DataTable LoadCSDL(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand(query, connection); // tạo lệnh
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
               

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
            return dt;
        }
    }
}
