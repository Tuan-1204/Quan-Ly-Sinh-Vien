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
    public partial class FQuanLyKhoa : Form
    {


        public FQuanLyKhoa()
        {
            InitializeComponent();
        }

        private void txbIdKhoa_TextChanged(object sender, EventArgs e)
        {
            // thêm dữ liệu cơ sở dữ liệu
        }

        private void btnAddInffoSv_Click(object sender, EventArgs e)
        {
            // thêm dữ liệu cơ sở dữ liệu

            string maKhoa = txbIdKhoa.Text;
            string tenKhoa = txbTenKhoa.Text;
            string query = $"insert into Khoa(Makhoa,TenKhoa) values ('{maKhoa}', N'{tenKhoa}')";
            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Thêm mới dữ liệu thành công");
                LoadTableKhoa();

            }
            else
            {
                MessageBox.Show("Thêm mới dữ liệu thất bại");


            }
        }



        private void btnShowALLKhoa_Click(object sender, EventArgs e)
        {
            LoadTableKhoa();
        }

        private void LoadTableKhoa()
        {
            throw new NotImplementedException();
        }
    }

}
