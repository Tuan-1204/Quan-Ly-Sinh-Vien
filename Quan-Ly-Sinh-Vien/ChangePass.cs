using System;
using System.Linq;
using System.Windows.Forms;

namespace Quan_Ly_Sinh_Vien
{
    public partial class ChangePass : Form
    {
        public ChangePass()
        {
            InitializeComponent();
        }

        // Sự kiện nút đổi mật khẩu
        private void btnChangePass_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txbChangePassName.Text;
            string matKhauCu = txbPassCurrently.Text;
            string matKhauMoi = txbPassNew.Text;
            string xacNhanMatKhauMoi = txbPassConfirm.Text;

            // Kiểm tra nhập liệu
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhauCu) ||
                string.IsNullOrEmpty(matKhauMoi) || string.IsNullOrEmpty(xacNhanMatKhauMoi))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            // Kiểm tra xác nhận mật khẩu mới
            if (matKhauMoi != xacNhanMatKhauMoi)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp.");
                return;
            }

            // Kiểm tra mật khẩu cũ
            var dn = DataProvider.dangNhaps.FirstOrDefault(d => d.TenDangNhap == tenDangNhap && d.MatKhau == matKhauCu);
            if (dn == null)
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu cũ không đúng.");
                return;
            }

            // Cập nhật mật khẩu
            bool success = DataProvider.UpdateMatKhau(tenDangNhap, matKhauMoi);
            if (success)
            {
                MessageBox.Show("Đổi mật khẩu thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại.");
            }
        }

        //nút hiển thị mật khẩu
        private void chekPassCurrently_CheckedChanged(object sender, EventArgs e)
        {
            if (chekPassCurrently.Checked)
            {
                txbPassCurrently.UseSystemPasswordChar = false; // hiển thị
            }
            else
            {
                txbPassCurrently.UseSystemPasswordChar = true; // ẩn
            }
        }

        //nút đổi mật khẩu
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            txbChangePassName.Clear();
            txbPassCurrently.Clear();
            txbPassNew.Clear();
            txbPassConfirm.Clear();

            txbChangePassName.Focus(); // đưa con trỏ về ô nhập đầu tiên
        }

        private void btnChangePasswordExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

    }
}
