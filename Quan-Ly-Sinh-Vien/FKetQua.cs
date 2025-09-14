using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Quan_Ly_Sinh_Vien
{
    public partial class FKetQua : Form
    {
        public FKetQua()
        {
            InitializeComponent();
        }

        // Bật control
        private void EnableControls(List<Control> controls)
        {
            foreach (var control in controls) control.Enabled = true;
        }

        // Tắt control
        private void UnEnableControls(List<Control> controls)
        {
            foreach (var control in controls) control.Enabled = false;
        }

        // Reset text
        private void ResetText(List<Control> controls)
        {
            foreach (var control in controls) control.Text = string.Empty;
        }

        private void FKetQua_Load(object sender, EventArgs e)
        {
            LoadTableKetQua();

            // Load combobox lớp
            string query = "SELECT MaLop FROM Lop";
            DataTable dt = DataProvider.LoadCSDL(query);
            cbSearchLop.DataSource = dt;
            cbSearchLop.DisplayMember = "MaLop";
            cbSearchLop.ValueMember = "MaLop";
            cbSearchLop.SelectedIndex = -1;
        }

        private void LoadTableKetQua()
        {
            string query = "SELECT * FROM KetQua";
            DataTable dt = DataProvider.LoadCSDL(query);
            dgvDanhmucketqua.DataSource = dt;
        }

        // Hiển thị dữ liệu khi chọn kết quả
        private void dgvDanhmucketqua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvDanhmucketqua.Rows[e.RowIndex];
                txbIdDiemSINHVIEN.Text = row.Cells["MaSV"].Value?.ToString() ?? "";
                txbNamekqMH.Text = row.Cells["MaMH"].Value?.ToString() ?? "";
                txbDiemlan1.Text = row.Cells["DiemLan1"].Value == DBNull.Value ? "" : row.Cells["DiemLan1"].Value.ToString();
                txbDiemThiLai.Text = row.Cells["DiemThiLai"].Value == DBNull.Value ? "" : row.Cells["DiemThiLai"].Value.ToString();
            }
        }

        // Thêm điểm
        private void btnAddDiem_Click(object sender, EventArgs e)
        {
            string masv = txbIdDiemSINHVIEN.Text.Trim();
            string mamh = txbNamekqMH.Text.Trim();

            if (!float.TryParse(txbDiemlan1.Text, out float diemlan1) ||
                !float.TryParse(txbDiemThiLai.Text, out float diemthilai))
            {
                MessageBox.Show("Điểm nhập không hợp lệ!");
                return;
            }

            string query = $"INSERT INTO KetQua (MaSV, MaMH, DiemLan1, DiemThiLai) " +
                           $"VALUES ('{masv}', '{mamh}', {diemlan1}, {diemthilai})";
            int kq = DataProvider.ThaoTacCSDL(query);

            if (kq > 0)
            {
                MessageBox.Show("Thêm điểm thành công!");
                LoadTableKetQua();
                ResetText(new List<Control> { txbIdDiemSINHVIEN, txbNamekqMH, txbDiemlan1, txbDiemThiLai });
            }
            else MessageBox.Show("Thêm điểm thất bại!");
        }

        // Sửa điểm
        private void btnEditDiem_Click(object sender, EventArgs e)
        {
            string masv = txbIdDiemSINHVIEN.Text.Trim();
            string mamh = txbNamekqMH.Text.Trim();

            if (!float.TryParse(txbDiemlan1.Text, out float diemlan1) ||
                !float.TryParse(txbDiemThiLai.Text, out float diemthilai))
            {
                MessageBox.Show("Điểm nhập không hợp lệ!");
                return;
            }

            string query = $"UPDATE KetQua SET DiemLan1 = {diemlan1}, DiemThiLai = {diemthilai} " +
                           $"WHERE MaSV = '{masv}' AND MaMH = '{mamh}'";
            int kq = DataProvider.ThaoTacCSDL(query);

            if (kq > 0)
            {
                MessageBox.Show("Cập nhật điểm thành công!");
                LoadTableKetQua();
            }
            else MessageBox.Show("Cập nhật thất bại!");
        }

        // Xóa điểm
        private void btnDeleteDiem_Click(object sender, EventArgs e)
        {
            string masv = txbIdDiemSINHVIEN.Text.Trim();
            string mamh = txbNamekqMH.Text.Trim();

            if (MessageBox.Show("Bạn có chắc muốn xóa điểm này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            string query = $"DELETE FROM KetQua WHERE MaSV = '{masv}' AND MaMH = '{mamh}'";
            int kq = DataProvider.ThaoTacCSDL(query);

            if (kq > 0)
            {
                MessageBox.Show("Xóa điểm thành công!");
                LoadTableKetQua();
                ResetText(new List<Control> { txbIdDiemSINHVIEN, txbNamekqMH, txbDiemlan1, txbDiemThiLai });
            }
            else MessageBox.Show("Xóa thất bại!");
        }

        // Tìm kiếm lớp
        private void cbSearchLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchLop.SelectedIndex < 0) return;
            string malop = cbSearchLop.SelectedValue.ToString();

            string query = $"SELECT sv.MaSV, sv.HoTen, l.TenLop, mh.TenMH, kq.DiemLan1, kq.DiemThiLai " +
                           $"FROM SinhVien sv " +
                           $"JOIN Lop l ON sv.MaLop = l.MaLop " +
                           $"JOIN KetQua kq ON sv.MaSV = kq.MaSV " +
                           $"JOIN MonHoc mh ON kq.MaMH = mh.MaMH " +
                           $"WHERE l.MaLop = '{malop}'";
            DataTable dt = DataProvider.LoadCSDL(query);
            dgvDiemSinhVien.DataSource = dt;
        }

        // Xuất Excel
        private void btnBaoCaoKetqua_Click(object sender, EventArgs e)
        {
            if (dgvDiemSinhVien.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = "KetQua.xlsx"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var wb = new XLWorkbook())
                        {
                            var ws = wb.Worksheets.Add("KetQua");

                            // Header
                            for (int i = 1; i <= dgvDiemSinhVien.Columns.Count; i++)
                            {
                                ws.Cell(1, i).Value = dgvDiemSinhVien.Columns[i - 1].HeaderText;
                                ws.Cell(1, i).Style.Font.Bold = true;
                            }

                            // Data
                            for (int i = 0; i < dgvDiemSinhVien.Rows.Count; i++)
                            {
                                for (int j = 0; j < dgvDiemSinhVien.Columns.Count; j++)
                                {
                                    ws.Cell(i + 2, j + 1).Value =
                                        dgvDiemSinhVien.Rows[i].Cells[j].Value?.ToString() ?? "";
                                }
                            }

                            ws.Columns().AdjustToContents();
                            wb.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Xuất Excel thành công!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
                    }
                }
            }
        }
    }
}
