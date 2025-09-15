using ClosedXML.Excel;
using Quan_Ly_Sinh_Vien.DTO__DATA_TRANSFER_OBJECT_;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
          
            LoadTableLop();

            // Disable tất cả controls khi load form
            UnEnableControls(new List<Control> { txbMalop, txbTenLop,  btnSaveLop, btnEditLop, btnDeleteLop });
        }

        // Load dữ liệu lớp vào DataGridView 
        private void LoadTableLop()
        {
            string query = @"SELECT * from Lop";
            dt = DataProvider.LoadCSDL(query);
            dvgInfoLop.DataSource = dt;

            // Cấu hình DataGridView
            dvgInfoLop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dvgInfoLop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dvgInfoLop.MultiSelect = false;
            dvgInfoLop.AllowUserToAddRows = false;
            dvgInfoLop.ReadOnly = true;
            dvgInfoLop.RowHeadersVisible = false;
        }

     

        // Nút hiển thị tất cả
        private void btnShowAllLop_Click(object sender, EventArgs e)
        {
            LoadTableLop();
            ResetText(new List<Control> { txbMalop, txbTenLop });
         
            UnEnableControls(new List<Control> { txbMalop, txbTenLop,  btnSaveLop, btnEditLop, btnDeleteLop });
        }

        // Thêm mới dữ liệu 
        private void btnAddLop_Click(object sender, EventArgs e)
        {
            EnableControls(new List<Control> { txbMalop, txbTenLop, btnSaveLop });
            UnEnableControls(new List<Control> { btnEditLop, btnDeleteLop });
            ResetText(new List<Control> { txbMalop, txbTenLop });
          
            txbMalop.Focus();
        }

        // Hàm khởi tạo control
        private void EnableControls(List<Control> controls)
        {
            foreach (var control in controls)
            {
                if (control != null) control.Enabled = true;
            }
        }

        // Hàm vô hiệu hóa control
        private void UnEnableControls(List<Control> controls)
        {
            foreach (var control in controls)
            {
                if (control != null) control.Enabled = false;
            }
        }

        // Hàm reset control
        private void ResetText(List<Control> controls)
        {
            foreach (var control in controls)
            {
                if (control != null) control.Text = string.Empty;
            }
        }

        // Lưu dữ liệu vào database 
        private void btnSaveLop_Click(object sender, EventArgs e)
        {
            string maLop = txbMalop.Text.Trim();
            string tenLop = txbTenLop.Text.Trim();
          

            // Validation
            if (string.IsNullOrWhiteSpace(maLop))
            {
                MessageBox.Show("Vui lòng nhập mã lớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbMalop.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tenLop))
            {
                MessageBox.Show("Vui lòng nhập tên lớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbTenLop.Focus();
                return;
            }

           

            // Kiểm tra trùng mã lớp
            string checkQuery = $"SELECT COUNT(*) FROM Lop WHERE MaLop = '{maLop}'";
            DataTable checkResult = DataProvider.LoadCSDL(checkQuery);
            if (checkResult != null && checkResult.Rows.Count > 0)
            {
                int count = Convert.ToInt32(checkResult.Rows[0][0]);
                if (count > 0)
                {
                    MessageBox.Show("Mã lớp đã tồn tại. Vui lòng sử dụng mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txbMalop.Focus();
                    return;
                }
            }

            string query = $"INSERT INTO Lop(MaLop, TenLop) VALUES ('{maLop}', N'{tenLop}')";
            int kq = DataProvider.ThaoTacCSDL(query);

            if (kq > 0)
            {
                MessageBox.Show("Thêm mới lớp thành công");
                LoadTableLop();
                UnEnableControls(new List<Control> { txbMalop, txbTenLop, btnSaveLop, btnEditLop, btnDeleteLop });
                ResetText(new List<Control> { txbMalop, txbTenLop });
            }
            else
            {
                MessageBox.Show("Thêm mới lớp thất bại. Vui lòng xem lại!");
            }
        }

        // Hiển thị dữ liệu lên textbox khi chọn dòng 
        private void dvgInfoLop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dvgInfoLop.SelectedRows.Count > 0)
            {
                // Lấy dòng dữ liệu được chọn
                var row = dvgInfoLop.SelectedRows[0];

                // Truyền giá trị dữ liệu lên textbox
                txbMalop.Text = row.Cells["MaLop"].Value?.ToString() ;
                txbTenLop.Text = row.Cells["TenLop"].Value?.ToString() ;

              

                // Hiển thị các textbox và button
                EnableControls(new List<Control> { txbTenLop,  btnDeleteLop, btnEditLop });

                // Ẩn textbox mã lớp
                txbMalop.Enabled = false;
            }
        }

        // Sửa dữ liệu
        private void btnEditLop_Click(object sender, EventArgs e)
        {
            string maLop = txbMalop.Text.Trim();
            string tenLop = txbTenLop.Text.Trim();
          

            if (string.IsNullOrWhiteSpace(maLop))
            {
                MessageBox.Show("Vui lòng chọn lớp để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(tenLop))
            {
                MessageBox.Show("Vui lòng nhập tên lớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbTenLop.Focus();
                return;
            }

         

            if (MessageBox.Show("Bạn có muốn cập nhật thông tin lớp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string query = $"UPDATE Lop SET TenLop = N'{tenLop}' WHERE MaLop = '{maLop}'";
            int kq = DataProvider.ThaoTacCSDL(query);

            if (kq > 0)
            {
                MessageBox.Show("Cập nhật lớp thành công");
                LoadTableLop();
                UnEnableControls(new List<Control> { txbMalop, txbTenLop,  btnSaveLop, btnEditLop, btnDeleteLop });
                ResetText(new List<Control> { txbMalop, txbTenLop });
               
            }
            else
            {
                MessageBox.Show("Cập nhật lớp thất bại. Vui lòng xem lại!");
            }
        }

        // Xóa dữ liệu
        private void btnDeleteLop_Click(object sender, EventArgs e)
        {
            string maLop = txbMalop.Text.Trim();

            if (string.IsNullOrWhiteSpace(maLop))
            {
                MessageBox.Show("Vui lòng chọn lớp để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem lớp có sinh viên hay không
            string checkStudentQuery = $"SELECT COUNT(*) FROM SinhVien WHERE Lop = '{maLop}'";
            DataTable checkResult = DataProvider.LoadCSDL(checkStudentQuery);
            if (checkResult != null && checkResult.Rows.Count > 0)
            {
                int studentCount = Convert.ToInt32(checkResult.Rows[0][0]);
                if (studentCount > 0)
                {
                    MessageBox.Show($"Không thể xóa lớp này vì có {studentCount} sinh viên đang thuộc lớp này!",
                                  "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa lớp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            string query = $"DELETE FROM Lop WHERE MaLop = '{maLop}'";
            int kq = DataProvider.ThaoTacCSDL(query);

            if (kq > 0)
            {
                MessageBox.Show("Xóa lớp thành công");
                LoadTableLop();
                UnEnableControls(new List<Control> { txbMalop, txbTenLop, btnSaveLop, btnEditLop, btnDeleteLop });
                ResetText(new List<Control> { txbMalop, txbTenLop });
            
            }
            else
            {
                MessageBox.Show("Xóa lớp thất bại. Vui lòng xem lại!");
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
                FileName = $"DanhSachLop_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var wb = new XLWorkbook())
                        {
                            var ws = wb.Worksheets.Add("DanhSachLop");

                            // Tiêu đề báo cáo
                            ws.Cell(1, 1).Value = "DANH SÁCH LỚP";
                            ws.Range(1, 1, 1, 4).Merge().Style.Font.Bold = true;
                            ws.Range(1, 1, 1, 4).Style.Font.FontSize = 16;
                            ws.Range(1, 1, 1, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            // Header bảng
                            int headerRow = 3;
                            string[] headers = { "Mã Lớp", "Tên Lớp" };
                            for (int i = 0; i < headers.Length; i++)
                            {
                                ws.Cell(headerRow, i + 1).Value = headers[i];
                                ws.Cell(headerRow, i + 1).Style.Font.Bold = true;
                                ws.Cell(headerRow, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            }

                            // Dữ liệu
                            for (int i = 0; i < dvgInfoLop.Rows.Count; i++)
                            {
                                if (dvgInfoLop.Rows[i].Cells["MaLop"].Value != null)
                                {
                                    int dataRow = headerRow + 1 + i;
                                    ws.Cell(dataRow, 1).Value = dvgInfoLop.Rows[i].Cells["MaLop"].Value.ToString()  ;
                                    ws.Cell(dataRow, 2).Value = dvgInfoLop.Rows[i].Cells["TenLop"].Value.ToString()  ;
                                   
                                }
                            }

                            ws.Columns().AdjustToContents();
                            wb.SaveAs(sfd.FileName);
                            MessageBox.Show("Xuất Excel thành công!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message);
                    }
                }
            }
        }

        
    }
}
