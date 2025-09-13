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
    public partial class FQuanLyLop : Form
    {
        public FQuanLyLop()
        {
            InitializeComponent();
        }

        // Load dữ liệu khi Form mở
        private void FQuanLyLop_Load(object sender, EventArgs e)
        {
            LoadTableLop();
            LoadComboBoxKhoa();
        }

        // Hàm load dữ liệu lớp
        private void LoadTableLop()
        {
            string query = @"
                SELECT l.MaLop, l.TenLop, k.TenKhoa
                FROM Lop l
                LEFT JOIN Khoa k ON l.MaKhoa = k.MaKhoa";
            DataTable dt = DataProvider.LoadCSDL(query);
            dvgInfoLop.DataSource = dt;
        }

        // Hàm load danh sách khoa cho ComboBox
        private void LoadComboBoxKhoa()
        {
            string query = "SELECT MaKhoa, TenKhoa FROM Khoa";
            DataTable dt = DataProvider.LoadCSDL(query);
            cbKhoa.DataSource = dt;
            cbKhoa.DisplayMember = "TenKhoa";
            cbKhoa.ValueMember = "MaKhoa";
        }

        // Nút thêm lớp
        private void btnAddLop_Click(object sender, EventArgs e)
        {
            txbMaLop.Enabled = true;
            txbTenLop.Enabled = true;
            cbKhoa.Enabled = true;
            btnSaveLop.Enabled = true;

            txbMaLop.Clear();
            txbTenLop.Clear();
            cbKhoa.SelectedIndex = -1;
            txbMaLop.Focus();
        }

        // Nút lưu lớp
        private void btnSaveLop_Click(object sender, EventArgs e)
        {
            string maLop = txbMaLop.Text.Trim();
            string tenLop = txbTenLop.Text.Trim();
            string maKhoa = cbKhoa.SelectedValue?.ToString();

            if (string.IsNullOrWhiteSpace(maLop) || string.IsNullOrWhiteSpace(tenLop) || string.IsNullOrWhiteSpace(maKhoa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            string query = $@"
                INSERT INTO Lop(MaLop, TenLop, MaKhoa)
                VALUES ('{maLop}', N'{tenLop}', '{maKhoa}')";

            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Thêm lớp thành công!");
                LoadTableLop();
            }
            else
            {
                MessageBox.Show("Thêm lớp thất bại!");
            }
        }

        // Nút sửa lớp
        private void btnEditLop_Click(object sender, EventArgs e)
        {
            string maLop = txbMaLop.Text.Trim();
            string tenLop = txbTenLop.Text.Trim();
            string maKhoa = cbKhoa.SelectedValue?.ToString();

            if (string.IsNullOrWhiteSpace(maLop))
            {
                MessageBox.Show("Vui lòng chọn lớp cần sửa!");
                return;
            }

            string query = $@"
                UPDATE Lop
                SET TenLop = N'{tenLop}', MaKhoa = '{maKhoa}'
                WHERE MaLop = '{maLop}'";

            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Cập nhật lớp thành công!");
                LoadTableLop();
            }
            else
            {
                MessageBox.Show("Cập nhật lớp thất bại!");
            }
        }

        // Nút xóa lớp
        private void btnDeleteLop_Click(object sender, EventArgs e)
        {
            string maLop = txbMaLop.Text.Trim();
            if (string.IsNullOrWhiteSpace(maLop))
            {
                MessageBox.Show("Vui lòng chọn lớp cần xóa!");
                return;
            }

            DialogResult dr = MessageBox.Show(
                $"Bạn có chắc muốn xóa lớp {maLop} không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dr == DialogResult.No) return;

            string query = $"DELETE FROM Lop WHERE MaLop = '{maLop}'";
            int kq = DataProvider.ThaoTacCSDL(query);

            if (kq > 0)
            {
                MessageBox.Show("Xóa lớp thành công!");
                LoadTableLop();
            }
            else
            {
                MessageBox.Show("Xóa lớp thất bại!");
            }
        }

        // Nút hiển thị tất cả lớp
        private void btnShowAllLop_Click(object sender, EventArgs e)
        {
            LoadTableLop();
        }

        // Hiển thị thông tin lớp khi click DataGridView
        private void dvgInfoLop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dvgInfoLop.Rows[e.RowIndex];
                txbMaLop.Text = row.Cells["MaLop"].Value.ToString();
                txbTenLop.Text = row.Cells["TenLop"].Value.ToString();
                cbKhoa.Text = row.Cells["TenKhoa"].Value.ToString();
            }
        }


    }
}
