using Quan_Ly_Sinh_Vien.DTO__DATA_TRANSFER_OBJECT_;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Quan_Ly_Sinh_Vien
{
    public class DataProvider
    {
        // Chuỗi kết nối
        private const string connString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Quan-Ly-Sinh-Vien;Integrated Security=True;";

        // Tạo list đăng nhập
        public static List<Dangnhap> dangNhaps = new List<Dangnhap>();

        // Hàm lấy danh sách đăng nhập và truyền vào list 
        public static void GetAllDangNhap()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    string query = "SELECT * FROM DangNhap";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Dangnhap dangNhap = new Dangnhap
                        {
                            TenDangNhap = reader["TenDangNhap"].ToString(),
                            MatKhau = reader["MatKhau"].ToString(),
                            HoTen = reader["HoTen"].ToString(),
                            Quyen = reader["Quyen"].ToString()
                        };
                        dangNhaps.Add(dangNhap);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // Hàm load dữ liệu từ database (SELECT)
        public static DataTable LoadCSDL(string query, object[] parameters = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains("@"))
                                {
                                    command.Parameters.AddWithValue(item, parameters[i]);
                                    i++;
                                }
                            }
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            return dt;
        }

        // Hàm thêm/sửa/xóa (INSERT, UPDATE, DELETE)
        public static int ThaoTacCSDL(string query, object[] parameters = null)
        {
            int kq = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains("@"))
                                {
                                    command.Parameters.AddWithValue(item, parameters[i]);
                                    i++;
                                }
                            }
                        }

                        kq = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            return kq;
        }
    }
}
