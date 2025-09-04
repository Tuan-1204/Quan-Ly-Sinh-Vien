using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Sinh_Vien
{
   
    public partial class FMain : Form
    {
        DataTable dt = new DataTable();
        public FMain(DangNhap dn)
        {
            InitializeComponent();
        }


        //nút hiển thị tất cả 
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            //câu lệnh query -> lấy dữ liệu từ database ->  table ->hiển thị lên datagridview
            string query = "SELECT * FROM MonHoc";
            //hàm xóa 
            dt.Clear();
            dt = DataProvider.LoadCSDL(query);
            dvgShow.DataSource = dt;
        }
    }
}
