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
    public partial class FDangNhap : Form
    {
        public FDangNhap()
        {
            InitializeComponent();

            // Enter = Đăng nhập
            this.AcceptButton = btnLogin;
            // Esc = Thoát
            this.CancelButton = btnExit;
        }

        private void FDangNhap_Load(object sender, EventArgs e)
        {
            // Load dữ liệu đăng nhập
            DataProvider.GetAllDangNhap();
        }

        // Sự kiện nút đăng nhập
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txbUserName.Text.Trim();
            string matKhau = txbPassWord.Text.Trim();
            // Kiểm tra tài khoản và mật khẩu
            var dn = DataProvider.dangNhaps
                .FirstOrDefault(x => x.TenDangNhap == tenDangNhap && x.MatKhau == matKhau);

            if (dn != null)
            {
                // Đăng nhập thành công
                MessageBox.Show("Đăng nhập thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mở form chính
                FMain fMain = new FMain(dn);
                this.Hide();//Ẩn form đăng nhập
                fMain.ShowDialog();
                this.Close();//Đóng form đăng nhập khi form chính đóng

                // Reset textbox
                txbUserName.Text = "";
                txbPassWord.Text = "";
                txbUserName.Focus();
            }
            else
            {
                // Sai tài khoản hoặc mật khẩu
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txbPassWord.Text = "";
                txbUserName.Focus();
            }
        }

        // Sự kiện nút thoát
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // Sự kiện checkbox hiện mật khẩu
        private void chekShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chekShowPass.Checked)
                txbPassWord.UseSystemPasswordChar = false;
            else
                txbPassWord.UseSystemPasswordChar = true;
        }

        // Sự kiện link quên mật khẩu
        private void linkQuenPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Chức năng quên mật khẩu chưa được hỗ trợ!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}
