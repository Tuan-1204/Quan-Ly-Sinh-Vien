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


        DataTable dt = new DataTable();
        public FQuanLyKhoa()
        {
            InitializeComponent();
         
        }

        //hàm khởi tạo load dữ liệu khoa
        private void LoadTableKhoa()
        {
            string query = "select * from Khoa";
             dt = DataProvider.LoadCSDL(query);
            dgvInKhoa.DataSource = dt;
        }


        //hàm khởi tạo control
        private void EnableControls(List<Control> controls)
        {
            foreach (var control in controls)
            {
                control.Enabled = true;
            }
        }
        //hàm vô hiệu hóa control
        private void UnEnableControls(List<Control> controls)
        {
            foreach (var control in controls)
            {
                control.Enabled = false;
            }
        }
        //hàm reset control
        private void ResetText(List<Control> controls)
        {
            foreach (var control in controls)
            {
                control.ResetText();
            }
        }
        //nút thêm mới khoa
        private void btnAddKhoa_Click(object sender, EventArgs e)
        {
            ResetText(new List<Control> { txbIdKhoa, txbQLNameKhoa });
            EnableControls(new List<Control> { txbIdKhoa, txbQLNameKhoa, btnSaveInfoKhoa });
            UnEnableControls(new List<Control> { btnEditInfoKhoa, btnDeleteInfoKhoa });
            txbIdKhoa.Focus();
        }
        //nút lưu thông tin khoa
        private void btnSaveInfoKhoa_Click(object sender, EventArgs e)
        {
            string maKhoa = txbIdKhoa.Text;
            string tenKhoa = txbQLNameKhoa.Text;
            if (string.IsNullOrWhiteSpace(maKhoa) || string.IsNullOrWhiteSpace(tenKhoa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khoa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //kiểm tra mã khoa đã tồn tại chưa
            string checkQuery = $"SELECT COUNT(*) FROM Khoa WHERE MaKhoa = '{maKhoa}'";
            int count = (int)DataProvider.LoadCSDL(checkQuery).Rows[0][0];
            if (count > 0)
            {
                MessageBox.Show("Mã khoa đã tồn tại. Vui lòng sử dụng mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //thêm mới khoa
            string query = $"insert into Khoa(MaKhoa, TenKhoa) values ('{maKhoa}',N'{tenKhoa}') ";
            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Thêm mới khoa thành công");
                LoadTableKhoa();
                UnEnableControls(new List<Control> { txbIdKhoa, txbQLNameKhoa, btnSaveInfoKhoa });
                ResetText(new List<Control> { txbIdKhoa, txbQLNameKhoa });
            }
            else
            {
                MessageBox.Show("Thêm mới khoa thất bại. Vui lòng xem lại !");
            }
        }
        //nút sửa thông tin khoa
        private void btnEditInfoKhoa_Click(object sender, EventArgs e)
        {
            string maKhoa = txbIdKhoa.Text;
            string tenKhoa = txbQLNameKhoa.Text;
            if (string.IsNullOrWhiteSpace(maKhoa) || string.IsNullOrWhiteSpace(tenKhoa))
            {
                MessageBox.Show("Vui lòng chọn mã khoa để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string query = $"update Khoa set TenKhoa = N'{tenKhoa}' where MaKhoa = '{maKhoa}'";
            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Cập nhật thông tin khoa thành công");
                LoadTableKhoa();
                UnEnableControls(new List<Control> { txbIdKhoa, txbQLNameKhoa, btnEditInfoKhoa, btnDeleteInfoKhoa });
                ResetText(new List<Control> { txbIdKhoa, txbQLNameKhoa });
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin khoa thất bại. Vui lòng xem lại !");
            }
        }
        //nút xóa thông tin khoa
        private void btnDeleteInfoKhoa_Click(object sender, EventArgs e)
        {
            string maKhoa = txbIdKhoa.Text;
            if (string.IsNullOrWhiteSpace(maKhoa))
            {
                MessageBox.Show("Vui lòng chọn mã khoa để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //hỏi lại trước khi xóa
            DialogResult dr = MessageBox.Show($"Bạn có chắc muốn xóa khoa {maKhoa} không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.No) return;
            string query = $"delete from Khoa where MaKhoa = '{maKhoa}'";
            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Xóa khoa thành công");
                LoadTableKhoa();
                UnEnableControls(new List<Control> { txbIdKhoa, txbQLNameKhoa, btnEditInfoKhoa, btnDeleteInfoKhoa });
                ResetText(new List<Control> { txbIdKhoa, txbQLNameKhoa });
            }
            else
            {
                MessageBox.Show("Xóa khoa thất bại. Vui lòng xem lại !");
            }

        }
        //nút hiển thị tất cả khoa Lên datagridview
        private void btnShowALLKhoa_Click(object sender, EventArgs e)
        {
            LoadTableKhoa();
        }

        private void dgvInKhoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvInKhoa.SelectedRows.Count > 0)
            {
                txbIdKhoa.Text = dgvInKhoa.SelectedRows[0].Cells["MaKhoa"].Value.ToString();
                txbQLNameKhoa.Text = dgvInKhoa.SelectedRows[0].Cells["TenKhoa"].Value.ToString();
                EnableControls(new List<Control> { txbIdKhoa, txbQLNameKhoa, btnEditInfoKhoa, btnDeleteInfoKhoa });
                UnEnableControls(new List<Control> { btnSaveInfoKhoa });
            }
        }
    }
}
      

