using ClosedXML.Excel;
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

            // căn chỉnh hiển thị
            dgvInKhoa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInKhoa.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvInKhoa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInKhoa.MultiSelect = false;
            dgvInKhoa.AllowUserToAddRows = false; // ẩn dòng trống cuối
            dgvInKhoa.ReadOnly = true; // không cho sửa trực tiếp trên grid
            dgvInKhoa.RowHeadersVisible = false; // ẩn cột STT mặc định bên trái
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
            txbIdKhoa.Enabled = true; // khi thêm mới phải cho nhập lại MãKhoa
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

            string query = $"UPDATE Khoa SET TenKhoa = N'{tenKhoa}' WHERE MaKhoa = '{maKhoa}'";
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

            DialogResult dr = MessageBox.Show($"Bạn có chắc muốn xóa khoa {maKhoa} không?",
                                              "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.No) return;

            string query = $"DELETE FROM Khoa WHERE MaKhoa = '{maKhoa}'";
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

        //hiển thị thông tin khoa khi chọn vào datagridview
        private void dgvInKhoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvInKhoa.SelectedRows.Count > 0)
            {
                // Lấy dòng dữ liệu được chọn
                var row = dgvInKhoa.SelectedRows[0];

                // Đổ dữ liệu vào textbox
                txbIdKhoa.Text = row.Cells["MaKhoa"].Value.ToString();
                txbQLNameKhoa.Text = row.Cells["TenKhoa"].Value.ToString();

                // Hiển thị textbox tên khoa + bật nút Edit, Delete
                EnableControls(new List<Control> { txbQLNameKhoa, btnEditInfoKhoa, btnDeleteInfoKhoa });

                // Khóa textbox mã khoa (không cho sửa)
                txbIdKhoa.Enabled = false;

                // Tắt nút Save để tránh lưu nhầm
                UnEnableControls(new List<Control> { btnSaveInfoKhoa });
            }
        }
        // nút xuất excel danh sách khoa
        private void btnExelKhoa_Click(object sender, EventArgs e)
        {
            if (dgvInKhoa.Rows.Count > 0) // kiểm tra có dữ liệu
            {
                using (SaveFileDialog sfd = new SaveFileDialog()
                {
                    Filter = "Excel Workbook|*.xlsx",
                    ValidateNames = true,
                    FileName = "DanhSachKhoa.xlsx"
                })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                var ws = wb.Worksheets.Add("DanhSachKhoa");

                                // Ghi header và in đậm
                                for (int i = 1; i <= dgvInKhoa.Columns.Count; i++)
                                {
                                    ws.Cell(1, i).Value = dgvInKhoa.Columns[i - 1].HeaderText;
                                    ws.Cell(1, i).Style.Font.Bold = true;
                                    ws.Cell(1, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    ws.Cell(1, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                }

                                // Ghi dữ liệu
                                for (int i = 0; i < dgvInKhoa.Rows.Count; i++)
                                {
                                    for (int j = 0; j < dgvInKhoa.Columns.Count; j++)
                                    {
                                        ws.Cell(i + 2, j + 1).Value = dgvInKhoa.Rows[i].Cells[j].Value?.ToString() ?? "";
                                        ws.Cell(i + 2, j + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        ws.Cell(i + 2, j + 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                    }
                                }

                                // Thêm border cho toàn bảng
                                var tableRange = ws.Range(1, 1, dgvInKhoa.Rows.Count + 1, dgvInKhoa.Columns.Count);
                                tableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                tableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                                // Tự động co giãn cột
                                ws.Columns().AdjustToContents();

                                // Lưu file
                                wb.SaveAs(sfd.FileName);
                            }

                            MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Xuất Excel thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


    }



}

      

