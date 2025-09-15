using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Quan_Ly_Sinh_Vien
{
    public partial class FQuanLySinhVien : Form
    {
        DataTable dt = new DataTable();

        public FQuanLySinhVien()
        {
            InitializeComponent();
        }

        // bật control
        private void EnableControls(List<Control> controls)
        {
            foreach (var c in controls) c.Enabled = true;
        }

        // tắt control
        private void UnEnableControls(List<Control> controls)
        {
            foreach (var c in controls) c.Enabled = false;
        }

        // reset control
        private void ResetText(List<Control> controls)
        {
            foreach (var c in controls) c.ResetText();
        }

        // Load toàn bộ sinh viên
        private void LoadTableSinhVien()
        {
            string query = @"
                SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.SDT, sv.DiaChi, l.TenLop
                FROM SinhVien sv
                LEFT JOIN Lop l ON sv.Lop = l.MaLop";

            dt = DataProvider.LoadCSDL(query);
            dvgInfoSinhVien.DataSource = dt;

            // căn chỉnh DataGridView
            dvgInfoSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dvgInfoSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dvgInfoSinhVien.MultiSelect = false;
            dvgInfoSinhVien.RowHeadersVisible = false;
        }

        // thêm mới
        private void btnAddSinhvien_Click(object sender, EventArgs e)
        {
            EnableControls(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, chekMen, chekWomen, txbPhone, txbAdress, cbLop, btnSaveSinhvien });
            UnEnableControls(new List<Control> { btnEditSinhVien, btnDeleteSinhVien });
            ResetText(new List<Control> { txbIDSinhvien, txbNameSv, txbPhone, txbAdress });
            txbIDSinhvien.Focus();
        }

        // lưu mới
        private void btnSaveSinhvien_Click(object sender, EventArgs e)
        {
            string idSv = txbIDSinhvien.Text.Trim();
            string nameSv = txbNameSv.Text.Trim();
            DateTime dateSv = dateSinhVien.Value;
            string phoneSv = txbPhone.Text.Trim();
            string adressSv = txbAdress.Text.Trim();
            string gioiTinh = chekMen.Checked ? "Nam" : "Nữ";
            string lop = cbLop.SelectedValue?.ToString();

            string query = $@"
                INSERT INTO SinhVien(MaSV, HoTen, NgaySinh, GioiTinh, SDT, DiaChi, Lop)
                VALUES ('{idSv}', N'{nameSv}', '{dateSv:yyyy-MM-dd}', N'{gioiTinh}', '{phoneSv}', N'{adressSv}', '{lop}')";

            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Thêm mới sinh viên thành công");
                LoadTableSinhVien();
                UnEnableControls(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, txbPhone, txbAdress, cbLop, btnSaveSinhvien });
                ResetText(new List<Control> { txbIDSinhvien, txbNameSv, txbPhone, txbAdress });
            }
        }

        // hiển thị lên textbox khi chọn dòng
        private void dvgInfoSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dvgInfoSinhVien.SelectedRows.Count > 0)
            {
                var row = dvgInfoSinhVien.SelectedRows[0];
                txbIDSinhvien.Text = row.Cells["MaSV"].Value.ToString();
                txbNameSv.Text = row.Cells["HoTen"].Value.ToString();
                dateSinhVien.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);

                string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                chekMen.Checked = gioiTinh == "Nam";
                chekWomen.Checked = gioiTinh == "Nữ";

                txbPhone.Text = row.Cells["SDT"].Value.ToString();
                txbAdress.Text = row.Cells["DiaChi"].Value.ToString();
                cbLop.Text = row.Cells["TenLop"].Value.ToString();

                EnableControls(new List<Control> { txbNameSv, dateSinhVien, txbPhone, txbAdress, cbLop, btnEditSinhVien, btnDeleteSinhVien });
                txbIDSinhvien.Enabled = false; // không cho sửa mã sinh viên
            }
        }

        // sửa
        private void btnEditSinhVien_Click(object sender, EventArgs e)
        {
            string idSv = txbIDSinhvien.Text.Trim();
            string nameSv = txbNameSv.Text.Trim();
            DateTime dateSv = dateSinhVien.Value;
            string phoneSv = txbPhone.Text.Trim();
            string adressSv = txbAdress.Text.Trim();
            string gioiTinh = chekMen.Checked ? "Nam" : "Nữ";
            string lop = cbLop.SelectedValue?.ToString();

            string query = $@"
                UPDATE SinhVien
                SET HoTen = N'{nameSv}',
                    NgaySinh = '{dateSv:yyyy-MM-dd}',
                    GioiTinh = N'{gioiTinh}',
                    SDT = '{phoneSv}',
                    DiaChi = N'{adressSv}',
                    Lop = '{lop}'
                WHERE MaSV = '{idSv}'";

            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Cập nhật sinh viên thành công");
                LoadTableSinhVien();
            }
        }

        // xóa
        private void btnDeleteSinhVien_Click(object sender, EventArgs e)
        {
            string idSv = txbIDSinhvien.Text;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string query = $"DELETE FROM SinhVien WHERE MaSV = '{idSv}'";
                int kq = DataProvider.ThaoTacCSDL(query);
                if (kq > 0)
                {
                    MessageBox.Show("Xóa sinh viên thành công");
                    LoadTableSinhVien();
                }
            }
        }

    
     

     
        //hiển thị thông tin sinh viên lên datagrid view
        private void btnShowAllInfoSv_Click(object sender, EventArgs e)
        {

            try
            {
                LoadTableSinhVien();  // gọi hàm load dữ liệu
                MessageBox.Show("Đã tải danh sách sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu có lựa chọn hợp lệ
            if (cbLop.SelectedValue != null && !string.IsNullOrEmpty(cbLop.SelectedValue.ToString()))
            {
                string maLop = cbLop.SelectedValue.ToString();
                string tenLop = cbLop.Text;
                Console.WriteLine($"Đã chọn lớp: {tenLop} (Mã: {maLop})");
            }
        }
        

        private void LoadComboBoxLop()
        {
            try
            {
                string query = "SELECT MaLop, TenLop FROM Lop ORDER BY TenLop";
                DataTable dtLop = DataProvider.LoadCSDL(query);

                // Thêm dòng trống để người dùng có thể chọn "không chọn lớp"
                DataRow emptyRow = dtLop.NewRow();
                emptyRow["MaLop"] = "";
                emptyRow["TenLop"] = "-- Chọn lớp --";
                dtLop.Rows.InsertAt(emptyRow, 0);

                cbLop.DataSource = dtLop;
                cbLop.DisplayMember = "TenLop";  // Hiển thị tên lớp
                cbLop.ValueMember = "MaLop";    // Giá trị là mã lớp
                cbLop.SelectedIndex = 0;        // Chọn dòng đầu tiên (dòng trống)
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FQuanLySinhVien_Load(object sender, EventArgs e)
        {
            LoadComboBoxLop();
            LoadTableSinhVien();
            
        }
    }
}
