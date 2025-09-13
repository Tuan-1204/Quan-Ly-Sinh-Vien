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




        // Load dữ liệu khi mở form

        private void FQuanLyLop_Load(object sender, EventArgs e)
        {
            LoadKhoa();
            LoadLop();
          
            UnEnableControls(new List<Control> { txbMalop, txbTenLop, cbKhoa, btnSaveLop, btnEditLop, btnDeleteLop });
            EnableControls(new List<Control> { btnAddLop, btnShowAllLop });

        }
        // Hiển thị danh sách Khoa vào combobox
        private void LoadKhoa()
        {
            string query = "SELECT * FROM Khoa";
            DataTable dt = DataProvider.LoadCSDL(query);
            cbKhoa.DataSource = dt;
            cbKhoa.DisplayMember = "TenKhoa";
            cbKhoa.ValueMember = "MaKhoa";
        }

        // Hiển thị tất cả lớp
        private void LoadLop()
        {
            string query = "SELECT MaLop, TenLop, MaKhoa FROM Lop";
            dvgInfoLop.DataSource = DataProvider.LoadCSDL(query);
        }

        // Thêm mới: reset các textbox
        private void btnAddLop_Click(object sender, EventArgs e)
        {
            EnableControls(new List<Control> { txbMalop, txbTenLop, cbKhoa, btnSaveLop });
            UnEnableControls(new List<Control> { btnEditLop, btnDeleteLop });
            ResetText(new List<Control> { txbMalop, txbTenLop });
            cbKhoa.SelectedIndex = -1;
            txbMalop.Focus();
        }

        // Lưu (INSERT)
        private void btnSaveLop_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbMalop.Text) ||
               string.IsNullOrWhiteSpace(txbTenLop.Text) ||
               cbKhoa.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "INSERT INTO Lop(MaLop, TenLop, MaKhoa) VALUES(@MaLop, @TenLop, @MaKhoa)";
            int result = DataProvider.ThaoTacCSDL(query,
                new object[] { txbMalop.Text.Trim(), txbTenLop.Text.Trim(), cbKhoa.SelectedValue.ToString() });

            if (result > 0)
            {
                MessageBox.Show("Thêm lớp thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLop();

                // reset trạng thái sau khi lưu
                UnEnableControls(new List<Control> { txbMalop, txbTenLop, cbKhoa, btnSaveLop, btnEditLop, btnDeleteLop });
                EnableControls(new List<Control> { btnAddLop, btnShowAllLop });
            }
            else
            {
                MessageBox.Show("Thêm lớp thất bại!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Sửa (UPDATE)
        private void btnEditLop_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbMalop.Text))
            {
                MessageBox.Show("Vui lòng chọn lớp để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "UPDATE Lop SET TenLop = @TenLop, MaKhoa = @MaKhoa WHERE MaLop = @MaLop";
            int result = DataProvider.ThaoTacCSDL(query,
                new object[] { txbTenLop.Text.Trim(), cbKhoa.SelectedValue.ToString(), txbMalop.Text.Trim() });

            if (result > 0)
            {
                MessageBox.Show("Cập nhật lớp thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLop();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa (DELETE)
        private void btnDeleteLop_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbMalop.Text))
            {
                MessageBox.Show("Vui lòng chọn lớp để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa lớp này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string query = "DELETE FROM Lop WHERE MaLop = @MaLop";
            int result = DataProvider.ThaoTacCSDL(query, new object[] { txbMalop.Text.Trim() });

            if (result > 0)
            {
                MessageBox.Show("Xóa lớp thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLop();

                // reset trạng thái sau khi xóa
                ResetText(new List<Control> { txbMalop, txbTenLop });
                cbKhoa.SelectedIndex = -1;
                UnEnableControls(new List<Control> { txbMalop, txbTenLop, cbKhoa, btnSaveLop, btnEditLop, btnDeleteLop });
                EnableControls(new List<Control> { btnAddLop, btnShowAllLop });
            }
            else
            {
                MessageBox.Show("Xóa thất bại!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hiển thị tất cả
        private void btnShowAllLop_Click(object sender, EventArgs e)
        {
            LoadLop();
        }

        // Khi click vào DataGridView, load dữ liệu ra textbox
        private void dvgInfoLop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dvgInfoLop.Rows[e.RowIndex];
                txbMalop.Text = row.Cells["MaLop"].Value.ToString();
                txbTenLop.Text = row.Cells["TenLop"].Value.ToString();
                cbKhoa.SelectedValue = row.Cells["MaKhoa"].Value.ToString();

                EnableControls(new List<Control> { txbTenLop, cbKhoa, btnEditLop, btnDeleteLop });
                UnEnableControls(new List<Control> { btnSaveLop });
            }
        }

       
    }
}
