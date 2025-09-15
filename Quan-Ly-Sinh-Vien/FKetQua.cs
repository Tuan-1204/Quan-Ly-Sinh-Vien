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
            LoadComboBoxLop();      // Load ComboBox lớp
            LoadComboBoxSinhVien(); // Load ComboBox sinh viên 
            LoadComboBoxMonHoc();   // Load ComboBox môn học 
        }

        // Load dữ liệu lớp vào ComboBox với tính năng reload
        private void LoadComboBoxLop()
        {
            try
            {
                string query = "SELECT MaLop, TenLop FROM Lop ORDER BY TenLop";
                DataTable dtLop = DataProvider.LoadCSDL(query);

                // Thêm dòng trống để hiển thị tất cả
                DataRow emptyRow = dtLop.NewRow();
                emptyRow["MaLop"] = "";
                emptyRow["TenLop"] = "-- Tất cả lớp --";
                dtLop.Rows.InsertAt(emptyRow, 0);

                // Lưu lại lựa chọn hiện tại
                string currentValue = cbSearchLop.SelectedValue?.ToString() ?? "";

                cbSearchLop.DataSource = dtLop;
                cbSearchLop.DisplayMember = "TenLop";
                cbSearchLop.ValueMember = "MaLop";

                
                if (!string.IsNullOrEmpty(currentValue))
                {
                    cbSearchLop.SelectedValue = currentValue;
                }
                else
                {
                    cbSearchLop.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load dữ liệu sinh viên vào ComboBox 
        private void LoadComboBoxSinhVien()
        {
            try
            {
                string query = "SELECT MaSV, HoTen FROM SinhVien ORDER BY HoTen";
                DataTable dtSV = DataProvider.LoadCSDL(query);

               
                DataRow emptyRow = dtSV.NewRow();
                emptyRow["MaSV"] = "";
                emptyRow["HoTen"] = "-- Chọn sinh viên --";
                dtSV.Rows.InsertAt(emptyRow, 0);

           
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load dữ liệu môn học vào ComboBox 
        private void LoadComboBoxMonHoc()
        {
            try
            {
                string query = "SELECT MaMH, TenMH FROM MonHoc ORDER BY TenMH";
                DataTable dtMH = DataProvider.LoadCSDL(query);

               
                DataRow emptyRow = dtMH.NewRow();
                emptyRow["MaMH"] = "";
                emptyRow["TenMH"] = "-- Chọn môn học --";
                dtMH.Rows.InsertAt(emptyRow, 0);

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTableKetQua()
        {
            try
            {
                string query = @"
                    SELECT kq.MaSV, sv.HoTen as TenSinhVien, kq.MaMH, mh.TenMH, 
                           kq.DiemLan1, kq.DiemThiLai, l.TenLop
                    FROM KetQua kq
                    LEFT JOIN SinhVien sv ON kq.MaSV = sv.MaSV
                    LEFT JOIN MonHoc mh ON kq.MaMH = mh.MaMH
                    LEFT JOIN Lop l ON sv.Lop = l.MaLop
                    ORDER BY sv.HoTen, mh.TenMH";

                DataTable dt = DataProvider.LoadCSDL(query);
                dgvDanhmucketqua.DataSource = dt;

                // Cấu hình DataGridView
                dgvDanhmucketqua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDanhmucketqua.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvDanhmucketqua.MultiSelect = false;
                dgvDanhmucketqua.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu kết quả: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                // Enable các nút chỉnh sửa và xóa
                EnableControls(new List<Control> { btnEditDiem, btnDeleteDiem });
            }
        }

        // Thêm điểm
        private void btnAddDiem_Click(object sender, EventArgs e)
        {
            try
            {
                string masv = txbIdDiemSINHVIEN.Text.Trim();
                string mamh = txbNamekqMH.Text.Trim();

                // Validation
                if (string.IsNullOrEmpty(masv))
                {
                    MessageBox.Show("Vui lòng nhập mã sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txbIdDiemSINHVIEN.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(mamh))
                {
                    MessageBox.Show("Vui lòng nhập mã môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txbNamekqMH.Focus();
                    return;
                }

                // Kiểm tra sinh viên có tồn tại
                string checkSVQuery = $"SELECT COUNT(*) FROM SinhVien WHERE MaSV = '{masv}'";
                DataTable dtCheckSV = DataProvider.LoadCSDL(checkSVQuery);
                if (dtCheckSV.Rows[0][0].ToString() == "0")
                {
                    MessageBox.Show("Mã sinh viên không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra môn học có tồn tại
                string checkMHQuery = $"SELECT COUNT(*) FROM MonHoc WHERE MaMH = '{mamh}'";
                DataTable dtCheckMH = DataProvider.LoadCSDL(checkMHQuery);
                if (dtCheckMH.Rows[0][0].ToString() == "0")
                {
                    MessageBox.Show("Mã môn học không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra trường hợp đã tồn tại điểm
                string checkExistQuery = $"SELECT COUNT(*) FROM KetQua WHERE MaSV = '{masv}' AND MaMH = '{mamh}'";
                DataTable dtCheckExist = DataProvider.LoadCSDL(checkExistQuery);
                if (dtCheckExist.Rows[0][0].ToString() != "0")
                {
                    MessageBox.Show("Điểm sinh viên cho môn học này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                float diemlan1 = 0, diemthilai = 0;

                // Kiểm tra điểm lần 1
                if (!string.IsNullOrEmpty(txbDiemlan1.Text))
                {
                    if (!float.TryParse(txbDiemlan1.Text, out diemlan1) || diemlan1 < 0 || diemlan1 > 10)
                    {
                        MessageBox.Show("Điểm lần 1 không hợp lệ! (0-10)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txbDiemlan1.Focus();
                        return;
                    }
                }

                // Kiểm tra điểm thi lại
                if (!string.IsNullOrEmpty(txbDiemThiLai.Text))
                {
                    if (!float.TryParse(txbDiemThiLai.Text, out diemthilai) || diemthilai < 0 || diemthilai > 10)
                    {
                        MessageBox.Show("Điểm thi lại không hợp lệ! (0-10)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txbDiemThiLai.Focus();
                        return;
                    }
                }

                string diemLan1Value = string.IsNullOrEmpty(txbDiemlan1.Text) ? "NULL" : diemlan1.ToString().Replace(",", ".");
                string diemThiLaiValue = string.IsNullOrEmpty(txbDiemThiLai.Text) ? "NULL" : diemthilai.ToString().Replace(",", ".");

                string query = $"INSERT INTO KetQua (MaSV, MaMH, DiemLan1, DiemThiLai) " +
                               $"VALUES ('{masv}', '{mamh}', {diemLan1Value}, {diemThiLaiValue})";

                int kq = DataProvider.ThaoTacCSDL(query);

                if (kq > 0)
                {
                    MessageBox.Show("Thêm điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTableKetQua();
                    ResetText(new List<Control> { txbIdDiemSINHVIEN, txbNamekqMH, txbDiemlan1, txbDiemThiLai });
                    UnEnableControls(new List<Control> { btnEditDiem, btnDeleteDiem });
                }
                else
                {
                    MessageBox.Show("Thêm điểm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sửa điểm với validation
        private void btnEditDiem_Click(object sender, EventArgs e)
        {
            try
            {
                string masv = txbIdDiemSINHVIEN.Text.Trim();
                string mamh = txbNamekqMH.Text.Trim();

                if (string.IsNullOrEmpty(masv) || string.IsNullOrEmpty(mamh))
                {
                    MessageBox.Show("Vui lòng chọn một dòng dữ liệu để chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                float diemlan1 = 0, diemthilai = 0;

                // Kiểm tra điểm lần 1
                if (!string.IsNullOrEmpty(txbDiemlan1.Text))
                {
                    if (!float.TryParse(txbDiemlan1.Text, out diemlan1) || diemlan1 < 0 || diemlan1 > 10)
                    {
                        MessageBox.Show("Điểm lần 1 không hợp lệ! (0-10)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txbDiemlan1.Focus();
                        return;
                    }
                }

                // Kiểm tra điểm thi lại
                if (!string.IsNullOrEmpty(txbDiemThiLai.Text))
                {
                    if (!float.TryParse(txbDiemThiLai.Text, out diemthilai) || diemthilai < 0 || diemthilai > 10)
                    {
                        MessageBox.Show("Điểm thi lại không hợp lệ! (0-10)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txbDiemThiLai.Focus();
                        return;
                    }
                }

                string diemLan1Value = string.IsNullOrEmpty(txbDiemlan1.Text) ? "NULL" : diemlan1.ToString().Replace(",", ".");
                string diemThiLaiValue = string.IsNullOrEmpty(txbDiemThiLai.Text) ? "NULL" : diemthilai.ToString().Replace(",", ".");

                string query = $"UPDATE KetQua SET DiemLan1 = {diemLan1Value}, DiemThiLai = {diemThiLaiValue} " +
                               $"WHERE MaSV = '{masv}' AND MaMH = '{mamh}'";

                int kq = DataProvider.ThaoTacCSDL(query);

                if (kq > 0)
                {
                    MessageBox.Show("Cập nhật điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTableKetQua();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa điểm
        private void btnDeleteDiem_Click(object sender, EventArgs e)
        {
            try
            {
                string masv = txbIdDiemSINHVIEN.Text.Trim();
                string mamh = txbNamekqMH.Text.Trim();

                if (string.IsNullOrEmpty(masv) || string.IsNullOrEmpty(mamh))
                {
                    MessageBox.Show("Vui lòng chọn một dòng dữ liệu để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc muốn xóa điểm này?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                string query = $"DELETE FROM KetQua WHERE MaSV = '{masv}' AND MaMH = '{mamh}'";
                int kq = DataProvider.ThaoTacCSDL(query);

                if (kq > 0)
                {
                    MessageBox.Show("Xóa điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTableKetQua();
                    ResetText(new List<Control> { txbIdDiemSINHVIEN, txbNamekqMH, txbDiemlan1, txbDiemThiLai });
                    UnEnableControls(new List<Control> { btnEditDiem, btnDeleteDiem });
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tìm kiếm lớp được cải thiện
        private void cbSearchLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbSearchLop.SelectedIndex < 0) return;

                string malop = cbSearchLop.SelectedValue?.ToString() ?? "";

                string query;
                if (string.IsNullOrEmpty(malop))
                {
                    // Hiển thị tất cả sinh viên
                    query = @"
                        SELECT sv.MaSV, sv.HoTen, l.TenLop, mh.TenMH, kq.DiemLan1, kq.DiemThiLai
                        FROM SinhVien sv
                        LEFT JOIN Lop l ON sv.Lop = l.MaLop
                        LEFT JOIN KetQua kq ON sv.MaSV = kq.MaSV
                        LEFT JOIN MonHoc mh ON kq.MaMH = mh.MaMH
                        ORDER BY sv.HoTen, mh.TenMH";
                }
                else
                {
                    // Lọc theo lớp
                    query = $@"
                        SELECT sv.MaSV, sv.HoTen, l.TenLop, mh.TenMH, kq.DiemLan1, kq.DiemThiLai
                        FROM SinhVien sv
                        INNER JOIN Lop l ON sv.Lop = l.MaLop
                        LEFT JOIN KetQua kq ON sv.MaSV = kq.MaSV
                        LEFT JOIN MonHoc mh ON kq.MaMH = mh.MaMH
                        WHERE l.MaLop = '{malop}'
                        ORDER BY sv.HoTen, mh.TenMH";
                }

                DataTable dt = DataProvider.LoadCSDL(query);
                dgvDiemSinhVien.DataSource = dt;

                // Cấu hình DataGridView
                dgvDiemSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDiemSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvDiemSinhVien.MultiSelect = false;
                dgvDiemSinhVien.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm theo lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút refresh để reload dữ liệu
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTableKetQua();
            LoadComboBoxLop();
            LoadComboBoxSinhVien();
            LoadComboBoxMonHoc();
            MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Xuất Excel được cải thiện
        private void btnBaoCaoKetqua_Click(object sender, EventArgs e)
        {
            if (dgvDiemSinhVien.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = $"KetQua_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var wb = new XLWorkbook())
                        {
                            var ws = wb.Worksheets.Add("KetQua");

                            // Tiêu đề báo cáo
                            ws.Cell(1, 1).Value = "BÁO CÁO KẾT QUÁ HỌC TẬP";
                            ws.Cell(1, 1).Style.Font.Bold = true;
                            ws.Cell(1, 1).Style.Font.FontSize = 16;
                            ws.Range(1, 1, 1, dgvDiemSinhVien.Columns.Count).Merge();
                            ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            ws.Cell(2, 1).Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
                            ws.Range(2, 1, 2, dgvDiemSinhVien.Columns.Count).Merge();

                            // Header
                            int startRow = 4;
                            for (int i = 1; i <= dgvDiemSinhVien.Columns.Count; i++)
                            {
                                ws.Cell(startRow, i).Value = dgvDiemSinhVien.Columns[i - 1].HeaderText;
                                ws.Cell(startRow, i).Style.Font.Bold = true;
                                ws.Cell(startRow, i).Style.Fill.BackgroundColor = XLColor.LightGray;
                            }

                            // Data
                            for (int i = 0; i < dgvDiemSinhVien.Rows.Count; i++)
                            {
                                for (int j = 0; j < dgvDiemSinhVien.Columns.Count; j++)
                                {
                                    ws.Cell(i + startRow + 1, j + 1).Value =
                                        dgvDiemSinhVien.Rows[i].Cells[j].Value?.ToString() ?? "";
                                }
                            }

                            // Định dạng
                            ws.Columns().AdjustToContents();
                            var tableRange = ws.Range(startRow, 1, startRow + dgvDiemSinhVien.Rows.Count, dgvDiemSinhVien.Columns.Count);
                            tableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            tableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                            wb.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}