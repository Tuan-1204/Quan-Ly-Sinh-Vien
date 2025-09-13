using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Sinh_Vien
{
    public partial class FQuanLySinhVien : Form
    {
        public FQuanLySinhVien()
        {
            InitializeComponent();
        }

        //Khoi tao ham control
        private void EnabledControls(List<Control> controls)
        {
            foreach (var control in controls)
            {
                control.Enabled = true;
            }
        }


        //hàm vô hiệu hóa control
        private void UnEnabledControls(List<Control> controls)
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

        // Load dữ liệu SinhVien
        private void LoadTableSinhVien()
        {
            string query = @"
        SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.SDT, sv.DiaChi, l.TenLop
        FROM SinhVien sv
        LEFT JOIN Lop l ON sv.Lop = l.MaLop";
            DataTable dt = DataProvider.LoadCSDL(query);
            dvgInfoSinhVien.DataSource = dt;
        }

        //nút thêm mới sinh viên
        private void btnAddSinhvien_Click(object sender, EventArgs e)
        {
            EnabledControls(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, chekMen, chekWomen, txbPhone, txbAdress, btnSaveSinhvien });
            UnEnabledControls(new List<Control> { btnEditSinhVien, btnDeleteSinhVien });
            ResetText(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, txbPhone, txbAdress });
            txbIDSinhvien.Focus();
        }


        //nút lưu thông tin sinh viên
        private void btnSaveSinhvien_Click(object sender, EventArgs e)
        {
            string idSv = txbIDSinhvien.Text.Trim();
            string nameSv = txbNameSv.Text.Trim();
            DateTime dateSv = dateSinhVien.Value;
            string phoneSv = txbPhone.Text.Trim();
            string adressSv = txbAdress.Text.Trim();
            string gioiTinh = chekMen.Checked ? "Nam" : "Nữ";
            string lop = cbLop.SelectedValue?.ToString();   // lấy MaLop từ combobox

            if (string.IsNullOrWhiteSpace(lop))
            {
                MessageBox.Show("Vui lòng chọn lớp cho sinh viên!");
                return;
            }

            string query = $@"
        INSERT INTO SinhVien(MaSV, HoTen, NgaySinh, GioiTinh, SDT, DiaChi, Lop)
        VALUES ('{idSv}', N'{nameSv}', '{dateSv:yyyy-MM-dd}', N'{gioiTinh}', '{phoneSv}', N'{adressSv}', '{lop}')";

            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Thêm mới sinh viên thành công");
                LoadTableSinhVien();
                UnEnabledControls(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, txbPhone, txbAdress, cbLop, btnSaveSinhvien });
                ResetText(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, txbPhone, txbAdress });
            }
            else
            {
                MessageBox.Show("Thêm mới sinh viên thất bại. Vui lòng xem lại!");
            }
        }



        //nút sửa thông tin sinh viên
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
                MessageBox.Show("Cập nhật thông tin sinh viên thành công");
                LoadTableSinhVien();
                UnEnabledControls(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, txbPhone, txbAdress, cbLop, btnEditSinhVien, btnDeleteSinhVien });
                ResetText(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, txbPhone, txbAdress });
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin sinh viên thất bại. Vui lòng xem lại!");
            }
        }


        //nút xóa thông tin sinh viên
        private void btnDeleteSinhVien_Click(object sender, EventArgs e)
        {
            string idSv = txbIDSinhvien.Text;

            if (string.IsNullOrWhiteSpace(idSv))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!");
                return;
            }

            // Hỏi lại trước khi xoá
            DialogResult dr = MessageBox.Show(
                $"Bạn có chắc muốn xoá sinh viên {idSv} không?",
                "Xác nhận xoá",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dr == DialogResult.No) return;

            // Truy vấn xoá 
            string query = $"DELETE FROM SinhVien WHERE MaSV = '{idSv}'";
            int kq = DataProvider.ThaoTacCSDL(query);

            if (kq > 0)
            {
                MessageBox.Show("Xóa thông tin sinh viên thành công");
                LoadTableSinhVien();
                UnEnabledControls(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, txbPhone, txbAdress,  btnEditSinhVien, btnDeleteSinhVien });
                ResetText(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, txbPhone, txbAdress });
            }
            else
            {
                MessageBox.Show("Xóa thông tin sinh viên thất bại.");
            }
        }
      
        

        //nust hi tất cả thông tin sinh viên
        private void btnShowAllInfoSv_Click(object sender, EventArgs e)
        {
            LoadTableSinhVien();
        }

        //hiển thị thông tin sinh viên khi chọn vào datagridview
        private void dvgInfoSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dvgInfoSinhVien.Rows[e.RowIndex];
                txbIDSinhvien.Text = row.Cells["MaSV"].Value.ToString();
                txbNameSv.Text = row.Cells["HoTen"].Value.ToString();
                dateSinhVien.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);

                string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                chekMen.Checked = gioiTinh == "Nam";
                chekWomen.Checked = gioiTinh == "Nữ";

                txbPhone.Text = row.Cells["SDT"].Value.ToString();
                txbAdress.Text = row.Cells["DiaChi"].Value.ToString();

                // Gán lớp
                cbLop.Text = row.Cells["TenLop"].Value.ToString();

                EnabledControls(new List<Control> { btnEditSinhVien, btnDeleteSinhVien });
                UnEnabledControls(new List<Control> { btnSaveSinhvien, txbIDSinhvien });
            }
        }

       
        
            // Load danh sách lớp cho ComboBox
private void LoadComboBoxLop()
        {
            string query = "SELECT MaLop, TenLop FROM Lop";
            DataTable dt = DataProvider.LoadCSDL(query);
            cbLop.DataSource = dt;
            cbLop.DisplayMember = "TenLop";
            cbLop.ValueMember = "MaLop";
        }
    }
    
}
    
    
