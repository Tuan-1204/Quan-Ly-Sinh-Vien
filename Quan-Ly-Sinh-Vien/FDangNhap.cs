using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Quan_Ly_Sinh_Vien
{
    public partial class FDangNhap : Form
    {
        private DangNhap dn;

        public FDangNhap()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin;
        }
        //nút hiện mật khẩu
        private void chkPassWord_CheckedChanged(object sender, EventArgs e)
        {
            // Nếu được chọn, hiển thị mật khẩu; ngược lại, ẩn mật khẩu
            txbPassWord.UseSystemPasswordChar = !chkPassWord.Checked;
        }


        //link quên mật khẩu
        private void LnkQuenPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Mở form đổi mật khẩu (ChangePass)
            ChangePass changePassForm = new ChangePass();
            changePassForm.ShowDialog();
        }


        //nút đăng nhập
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //lấy tên đăng nhập và mật khẩu
            string tenDangNhap = txbUserName.Text;
            string matKhau = txbPassWord.Text;
            bool isFound = false;
            foreach (DangNhap dn in DataProvider.dangNhaps)
            {
                if (dn.TenDangNhap == tenDangNhap && dn.MatKhau == matKhau)
                {
                    isFound = true;
                    break;

                }
               
            }
            if (isFound)
            {
                //hiện form chính
              this.Hide();
                //FMain fMain = new FMain(dn);
                FHeThong fHeThong = new FHeThong(dn);
                //fMain.ShowDialog();
                fHeThong.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
            }
        }

        //nút thoát
        private void btnExit_Click(object sender, EventArgs e)
        {
           //hiện hộp thoại xác nhận thoát
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                Application.Exit();
            }
        }

        //Hàm load form
        private void FDangNhap_Load(object sender, EventArgs e)
        {
            DataProvider.GetAllDangNhap();
        }
    }
}
