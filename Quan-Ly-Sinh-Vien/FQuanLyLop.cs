using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Quan_Ly_Sinh_Vien
{
    public partial class FQuanLyLop : Form
    {
        DataTable dt = new DataTable();

        public FQuanLyLop()
        {
            InitializeComponent();
        }

        // Load dữ liệu khi mở form
        private void FQuanLyLop_Load(object sender, EventArgs e)
        {
            LoadKhoa();   // chỉ gọi 1 lần khi load form
            LoadLop();

            UnEnableControls(new List<Control> { txbMalop, txbTenLop, cbKhoa, btnSaveLop, btnEditLop, btnDeleteLop });
            EnableControls(new List<Control> { btnAddLop, btnShowAllLop });
        }

        private void EnableControls(List<Control> controls)
        {
            foreach (var control in controls)
                control.Enabled = true;
        }

        private void UnEnableControls(List<Control> controls)
        {
            foreach (var control in controls)
                control.Enabled = false;
        }

        private void ResetText(List<Control> controls)
        {
            foreach (var control in controls)
                control.Text = string.Empty;
        }

        private void LoadLop()
        {
            string query = "SELECT MaLop, TenLop, MaKhoa FROM Lop";
            dt = DataProvider.LoadCSDL(query);
            dvgInfoLop.DataSource = dt;
        }

        // Hiển thị danh sách Khoa vào combobox
        private void LoadKhoa()
        {
            string query = "SELECT MaKhoa, TenKhoa FROM Khoa";
            DataTable dt = DataProvider.LoadCSDL(query);

            // thêm dòng mặc định
            DataRow dr = dt.NewRow();
            dr["MaKhoa"] = "";
            dr["TenKhoa"] = "-- Chọn Khoa --";
            dt.Rows.InsertAt(dr, 0);

            cbKhoa.DataSource = dt;
            cbKhoa.DisplayMember = "TenKhoa";
            cbKhoa.ValueMember = "MaKhoa";
            cbKhoa.SelectedIndex = 0;             // chưa chọn mặc định
        }


        private void btnShowAllLop_Click(object sender, EventArgs e)
        {
            LoadLop();
        }

        private void btnAddLop_Click(object sender, EventArgs e)
        {
            EnableControls(new List<Control> { txbMalop, txbTenLop, cbKhoa, btnSaveLop });
            UnEnableControls(new List<Control> { btnEditLop, btnDeleteLop });
            ResetText(new List<Control> { txbMalop, txbTenLop });
            cbKhoa.SelectedIndex = -1;
            txbMalop.Focus();
        }

        private void btnSaveLop_Click(object sender, EventArgs e)
        {
            string maLop = txbMalop.Text.Trim();
            string tenLop = txbTenLop.Text.Trim();
            string maKhoa = cbKhoa.SelectedValue?.ToString();

            if (string.IsNullOrWhiteSpace(maLop) || string.IsNullOrWhiteSpace(tenLop) || string.IsNullOrWhiteSpace(maKhoa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin lớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = $"INSERT INTO Lop(MaLop, TenLop, MaKhoa) VALUES ('{maLop}', N'{tenLop}', '{maKhoa}')";
            int kq = DataProvider.ThaoTacCSDL(query);

            if (kq > 0)
            {
                MessageBox.Show("Thêm lớp thành công!");
                LoadLop();
                UnEnableControls(new List<Control> { txbMalop, txbTenLop, cbKhoa, btnSaveLop });
                EnableControls(new List<Control> { btnAddLop, btnShowAllLop });
                ResetText(new List<Control> { txbMalop, txbTenLop });
            }
            else
            {
                MessageBox.Show("Thêm lớp thất bại!");
            }
        }

        private void dvgInfoLop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dvgInfoLop.SelectedRows.Count > 0)
            {
                var row = dvgInfoLop.SelectedRows[0];
                txbMalop.Text = row.Cells["MaLop"].Value.ToString();
                txbTenLop.Text = row.Cells["TenLop"].Value.ToString();
                cbKhoa.SelectedValue = row.Cells["MaKhoa"].Value.ToString();

                EnableControls(new List<Control> { txbTenLop, cbKhoa, btnEditLop, btnDeleteLop });
                txbMalop.Enabled = false; // Không cho sửa mã lớp
            }
        }

        private void btnEditLop_Click(object sender, EventArgs e)
        {
            string maLop = txbMalop.Text.Trim();
            string tenLop = txbTenLop.Text.Trim();
            string maKhoa = cbKhoa.SelectedValue?.ToString();

            if (string.IsNullOrWhiteSpace(maLop))
            {
                MessageBox.Show("Vui lòng chọn lớp để sửa!");
                return;
            }

            string query = $"UPDATE Lop SET TenLop = N'{tenLop}', MaKhoa = '{maKhoa}' WHERE MaLop = '{maLop}'";
            int kq = DataProvider.ThaoTacCSDL(query);

            if (kq > 0)
            {
                MessageBox.Show("Cập nhật lớp thành công!");
                LoadLop();
                ResetText(new List<Control> { txbMalop, txbTenLop });
                cbKhoa.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }

        private void btnDeleteLop_Click(object sender, EventArgs e)
        {
            string maLop = txbMalop.Text.Trim();

            if (string.IsNullOrWhiteSpace(maLop))
            {
                MessageBox.Show("Vui lòng chọn lớp để xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa lớp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string query = $"DELETE FROM Lop WHERE MaLop = '{maLop}'";
            int kq = DataProvider.ThaoTacCSDL(query);

            if (kq > 0)
            {
                MessageBox.Show("Xóa lớp thành công!");
                LoadLop();
                ResetText(new List<Control> { txbMalop, txbTenLop });
                cbKhoa.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Xóa thất bại!");
            }
        }

       
            // Sự kiện chọn combobox (nếu cần xử lý thêm)
                private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
                    {
                        if (cbKhoa.SelectedIndex >= 0)
                        {
                            string maKhoa = cbKhoa.SelectedValue.ToString();
                            Console.WriteLine("Mã khoa đang chọn: " + maKhoa);
                        }
                    }
    
    }
}
