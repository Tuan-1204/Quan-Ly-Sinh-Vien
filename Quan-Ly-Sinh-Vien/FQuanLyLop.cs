using ClosedXML.Excel;
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
        // Load form
        private void FQuanLyLop_Load(object sender, EventArgs e)
        {
            LoadKhoa();
            LoadLop();

            UnEnableControls(new List<Control> { txbMalop, txbTenLop, cbKhoa, btnSaveLop, btnEditLop, btnDeleteLop });
            EnableControls(new List<Control> { btnAddLop, btnShowAllLop, btnExportLop });
        }
        // Hàm hỗ trợ
        private void EnableControls(List<Control> controls)
        {
            foreach (var control in controls)
                control.Enabled = true;
        }
        // Hàm hỗ trợ
        private void UnEnableControls(List<Control> controls)
        {
            foreach (var control in controls)
                control.Enabled = false;
        }
        // Hàm hỗ trợ
        private void ResetText(List<Control> controls)
        {
            foreach (var control in controls)
                control.Text = string.Empty;
        }
        // Load dữ liệu lớp vào DataGridView
        private void LoadLop()
        {
            string query = "SELECT MaLop, TenLop, MaKhoa FROM Lop";
            dt = DataProvider.LoadCSDL(query);
            dvgInfoLop.DataSource = dt;

            // Căn chỉnh hiển thị
            dvgInfoLop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dvgInfoLop.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dvgInfoLop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dvgInfoLop.MultiSelect = false;
            dvgInfoLop.AllowUserToAddRows = false;
            dvgInfoLop.ReadOnly = true;
            dvgInfoLop.RowHeadersVisible = false;
        }
        // Load dữ liệu khoa vào ComboBox
        private void LoadKhoa()
        {
            string query = "SELECT MaKhoa, TenKhoa FROM Khoa";
            DataTable dtKhoa = DataProvider.LoadCSDL(query);

            // Thêm dòng mặc định
            DataRow dr = dtKhoa.NewRow();
            dr["MaKhoa"] = "";
            dr["TenKhoa"] = "-- Chọn Khoa --";
            dtKhoa.Rows.InsertAt(dr, 0);

            cbKhoa.DataSource = dtKhoa;
            cbKhoa.DisplayMember = "TenKhoa";
            cbKhoa.ValueMember = "MaKhoa";

            // Kiểm tra trước khi set SelectedIndex
            if (cbKhoa.Items.Count > 0)
                cbKhoa.SelectedIndex = 0;
        }
        // Hiển thị tất cả lớp
        private void btnShowAllLop_Click(object sender, EventArgs e)
        {
            LoadLop();
        }
        // Thêm lớp mới
        private void btnAddLop_Click(object sender, EventArgs e)
        {
            EnableControls(new List<Control> { txbMalop, txbTenLop, cbKhoa, btnSaveLop });
            UnEnableControls(new List<Control> { btnEditLop, btnDeleteLop });
            ResetText(new List<Control> { txbMalop, txbTenLop });
            if (cbKhoa.Items.Count > 0)
                cbKhoa.SelectedIndex = 0;
            txbMalop.Focus();
        }
        // Lưu lớp mới
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

            // Kiểm tra trùng mã lớp
            string checkQuery = $"SELECT COUNT(*) FROM Lop WHERE MaLop = '{maLop}'";
            int count = (int)DataProvider.LoadCSDL(checkQuery).Rows[0][0];
            if (count > 0)
            {
                MessageBox.Show("Mã lớp đã tồn tại. Vui lòng sử dụng mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = $"INSERT INTO Lop(MaLop, TenLop, MaKhoa) VALUES ('{maLop}', N'{tenLop}', '{maKhoa}')";
            int kq = DataProvider.ThaoTacCSDL(query);

            if (kq > 0)
            {
                MessageBox.Show("Thêm lớp thành công!");
                LoadLop();
                UnEnableControls(new List<Control> { txbMalop, txbTenLop, cbKhoa, btnSaveLop });
                EnableControls(new List<Control> { btnAddLop, btnShowAllLop, btnExportLop });
                ResetText(new List<Control> { txbMalop, txbTenLop });
                if (cbKhoa.Items.Count > 0)
                    cbKhoa.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Thêm lớp thất bại!");
            }
        }
        // Xử lý sự kiện khi chọn một dòng trong DataGridView
        private void dvgInfoLop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dvgInfoLop.SelectedRows.Count > 0)
            {
                var row = dvgInfoLop.SelectedRows[0];
                txbMalop.Text = row.Cells["MaLop"].Value.ToString();
                txbTenLop.Text = row.Cells["TenLop"].Value.ToString();
                cbKhoa.SelectedValue = row.Cells["MaKhoa"].Value.ToString();

                EnableControls(new List<Control> { txbTenLop, cbKhoa, btnEditLop, btnDeleteLop });
                txbMalop.Enabled = false;
            }
        }
        // Sửa lớp
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

            if (MessageBox.Show("Bạn có muốn cập nhật thông tin lớp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string query = $"UPDATE Lop SET TenLop = N'{tenLop}', MaKhoa = '{maKhoa}' WHERE MaLop = '{maLop}'";
            int kq = DataProvider.ThaoTacCSDL(query);

            if (kq > 0)
            {
                MessageBox.Show("Cập nhật lớp thành công!");
                LoadLop();
                ResetText(new List<Control> { txbMalop, txbTenLop });
                if (cbKhoa.Items.Count > 0)
                    cbKhoa.SelectedIndex = 0;
                UnEnableControls(new List<Control> { txbMalop, txbTenLop, cbKhoa, btnEditLop, btnDeleteLop });
                EnableControls(new List<Control> { btnAddLop, btnShowAllLop, btnExportLop });
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }
        // Xóa lớp
        private void btnDeleteLop_Click(object sender, EventArgs e)
        {
            string maLop = txbMalop.Text.Trim();

            if (string.IsNullOrWhiteSpace(maLop))
            {
                MessageBox.Show("Vui lòng chọn lớp để xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa lớp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            string query = $"DELETE FROM Lop WHERE MaLop = '{maLop}'";
            int kq = DataProvider.ThaoTacCSDL(query);

            if (kq > 0)
            {
                MessageBox.Show("Xóa lớp thành công!");
                LoadLop();
                ResetText(new List<Control> { txbMalop, txbTenLop });
                if (cbKhoa.Items.Count > 0)
                    cbKhoa.SelectedIndex = 0;
                UnEnableControls(new List<Control> { txbMalop, txbTenLop, cbKhoa, btnEditLop, btnDeleteLop });
                EnableControls(new List<Control> { btnAddLop, btnShowAllLop, btnExportLop });
            }
            else
            {
                MessageBox.Show("Xóa thất bại!");
            }
        }

        // Xử lý sự kiện khi chọn khoa
        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbKhoa.SelectedIndex > 0) // Chỉ khi chọn khoa thực sự
            {
                string maKhoa = cbKhoa.SelectedValue.ToString();
                Console.WriteLine("Mã khoa đang chọn: " + maKhoa);
            }
        }

        // Xuất Excel danh sách lớp
        private void btnExportLop_Click(object sender, EventArgs e)
        {
            if (dvgInfoLop.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Excel Workbook|*.xlsx",
                ValidateNames = true,
                FileName = "DanhSachLop.xlsx"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var wb = new XLWorkbook())
                        {
                            var ws = wb.Worksheets.Add("DanhSachLop");

                            // Xác định các dòng cần xuất
                            List<DataGridViewRow> rowsToExport = new List<DataGridViewRow>();
                            if (dvgInfoLop.SelectedRows.Count > 0)
                                rowsToExport.Add(dvgInfoLop.SelectedRows[0]);
                            else
                                foreach (DataGridViewRow r in dvgInfoLop.Rows)
                                    rowsToExport.Add(r);

                            // Header
                            for (int i = 1; i <= dvgInfoLop.Columns.Count; i++)
                            {
                                ws.Cell(1, i).Value = dvgInfoLop.Columns[i - 1].HeaderText;
                                ws.Cell(1, i).Style.Font.Bold = true;
                                ws.Cell(1, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell(1, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            }

                            // Dữ liệu
                            for (int i = 0; i < rowsToExport.Count; i++)
                            {
                                for (int j = 0; j < dvgInfoLop.Columns.Count; j++)
                                {
                                    ws.Cell(i + 2, j + 1).Value = rowsToExport[i].Cells[j].Value?.ToString() ?? "";
                                    ws.Cell(i + 2, j + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    ws.Cell(i + 2, j + 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                }
                            }

                            // Border toàn bảng
                            var range = ws.Range(1, 1, rowsToExport.Count + 1, dvgInfoLop.Columns.Count);
                            range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                            // Auto-fit cột
                            ws.Columns().AdjustToContents();

                            wb.SaveAs(sfd.FileName);
                            MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

    }
}
