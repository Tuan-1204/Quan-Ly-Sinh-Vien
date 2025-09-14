﻿
using ClosedXML.Excel;
using Quan_Ly_Sinh_Vien.DTO__DATA_TRANSFER_OBJECT_;
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

    public partial class FMain : Form
    {
        DataTable dt = new DataTable();
        public FMain(Dangnhap dn)
        {
            InitializeComponent();
        }





        //nút hiển thị tất cả 
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            //câu lệnh query -> lấy dữ liệu từ database ->  table ->hiển thị lên datagridview
            LoadTableMonHoc();
        }

        private void LoadTableMonHoc()
        {
            string query = "select * from MonHoc";
            dt = DataProvider.LoadCSDL(query);
            dvgShowMh.DataSource = dt;
        }
        //thêm mới dữ liệu
        private void btnAdd_Click(object sender, EventArgs e)
        {
            EnableControls(new List<Control> { txbMaMH, txbTenMH, txbTinChi, btnSave });
            UnEnableControls(new List<Control> { btnEdit, btnDelete });
            ResetText(new List<Control> { txbMaMH, txbTenMH, txbTinChi });
            txbMaMH.Focus();


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
                control.Text = string.Empty;
            }
        }

        //lưu dữ liệu vào database
        private void btnSave_Click(object sender, EventArgs e)
        {
            string maMH = txbMaMH.Text;
            string tenMH = txbTenMH.Text;
            string tinChi = txbTinChi.Text;

            string query = $"insert into MonHoc(MaMH, TenMH ,SoTiet) values ('{maMH}',N'{tenMH}','{tinChi}') ";
            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Thêm mới môn học thành công");
                LoadTableMonHoc();
                UnEnableControls(new List<Control> { txbMaMH, txbTenMH, txbTinChi, btnSave });
                ResetText(new List<Control> { txbMaMH, txbTenMH, txbTinChi });
            }
            else
            {
                MessageBox.Show("Thêm mới môn học thất bại. Vui lòng xem lại !");
            }
        }
        //hiển thị dữ liệu lên textbox khi chọn dòng trong datagridview
        private void dvgShow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dvgShowMh.SelectedRows.Count > 0)
            {
                //lấy dòng dữ liệu được chọn
                var row = dvgShowMh.SelectedRows[0];
                //truyền giá trị dữ liệu lên textbox
                txbMaMH.Text = row.Cells["MaMH"].Value.ToString();
                txbTenMH.Text = row.Cells["TenMH"].Value.ToString();
                txbTinChi.Text = row.Cells["SoTiet"].Value.ToString();

                //hiển thị các textbox 
                EnableControls(new List<Control> { txbTenMH, txbTinChi, btnDelete, btnEdit });

                //ẩn textbox mã môn học
                txbMaMH.Enabled = false;

            }
        }

        //sửa dữ liệu
        private void btnEdit_Click(object sender, EventArgs e)
        {
            string maMH = txbMaMH.Text;
            string tenMH = txbTenMH.Text;
            string tinChi = txbTinChi.Text;
            string query = $"UPDATE MonHoc SET TenMH = N'{tenMH}', SoTiet = '{tinChi}'  WHERE MaMH = '{maMH}'";
            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Cập nhật môn học thành công");
                LoadTableMonHoc();
                UnEnableControls(new List<Control> { txbMaMH, txbTenMH, txbTinChi, btnSave, btnEdit, btnDelete });
                ResetText(new List<Control> { txbMaMH, txbTenMH, txbTinChi });
            }
            else
            {
                MessageBox.Show("Cập nhật môn học thất bại. Vui lòng xem lại !");
            }
        }

        //xóa dữ liệu
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string maMH = txbMaMH.Text;
            string query = $"DELETE FROM MonHoc WHERE MaMH = '{maMH}'";
            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Xóa môn học thành công");
                LoadTableMonHoc();
                UnEnableControls(new List<Control> { txbMaMH, txbTenMH, txbTinChi, btnSave, btnEdit, btnDelete });
                ResetText(new List<Control> { txbMaMH, txbTenMH, txbTinChi });
            }
            else
            {
                MessageBox.Show("Xóa môn học thất bại. Vui lòng xem lại !");
            }
        }

        private void cbSearchMaMH_Click(object sender, EventArgs e)
        {
            LoadcbSearchMaMH();
        }

        //hàm load combo box tìm kiếm mã môn học
        private void LoadcbSearchMaMH()
        {
            string query = "select MaMH, TenMH from MonHoc";
            cbSearchMaMH.DataSource = DataProvider.LoadCSDL(query);
            cbSearchMaMH.DisplayMember = "TenMH"; //hiển thị mã môn học
            cbSearchMaMH.ValueMember = "MaMH"; //giá trị mã môn họcs
        }

        private void btnSearchMaMh_Click(object sender, EventArgs e)
        {
            string maMH = cbSearchMaMH.SelectedValue.ToString();
            string query = $"select * from MonHoc where MaMH = '{maMH}'";
            dt.Clear();
            dt = DataProvider.LoadCSDL(query);
            dvgShowMh.DataSource = dt;
        }
        //tìm kiếm theo tên môn học
        private void btnSearchTT_Click(object sender, EventArgs e)
        {
            string tenMH = txbSearchTT.Text;
            string query = $"select * from MonHoc where TenMH like N'%{tenMH}%'";
            dt.Clear();
            dt = DataProvider.LoadCSDL(query);
            dvgShowMh.DataSource = dt;

        }
        //nút xem danh sách sinh viên
        private void btnCategorySinhVien_Click(object sender, EventArgs e)
        {
            this.Hide();
            FQuanLySinhVien f = new FQuanLySinhVien();
            f.ShowDialog();
            this.Show();

        }

        //nút xem danh sách sinh viên theo khoa
        private void btnCategorySinhVienKhoa_Click(object sender, EventArgs e)
        {
            this.Hide();
            FQuanLyKhoa f = new FQuanLyKhoa();
            f.ShowDialog();
            this.Show();

        }

        //xem điểm sinh viên
        private void btnXemDiem_Click(object sender, EventArgs e)
        {
            this.Hide();
            FKetQua fKetQua = new FKetQua();
            fKetQua.ShowDialog();
            this.Show();
        }


        private void btnDsLop_Click(object sender, EventArgs e)
        {
            this.Hide();
            FQuanLyLop f = new FQuanLyLop();
            f.ShowDialog();
            this.Show();

        }

       
            private void bbtnXuatExelMH_Click(object sender, EventArgs e)
        {
            if (dvgShowMh.Rows.Count > 0)
            {
                using (SaveFileDialog sfd = new SaveFileDialog()
                {
                    Filter = "Excel Workbook|*.xlsx",
                    ValidateNames = true,
                    FileName = $"DanhSachMonHoc_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                var ws = wb.Worksheets.Add("DanhSachMonHoc");

                                // Ghi tiêu đề
                                ws.Cell(1, 1).Value = "DANH SÁCH MÔN HỌC";
                                ws.Range(1, 1, 1, dvgShowMh.Columns.Count).Merge().Style.Font.Bold = true;
                                ws.Range(1, 1, 1, dvgShowMh.Columns.Count).Style.Font.FontSize = 16;
                                ws.Range(1, 1, 1, dvgShowMh.Columns.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                                // Ghi header
                                for (int i = 0; i < dvgShowMh.Columns.Count; i++)
                                {
                                    ws.Cell(3, i + 1).Value = dvgShowMh.Columns[i].HeaderText;
                                    ws.Cell(3, i + 1).Style.Font.Bold = true;
                                    ws.Cell(3, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    ws.Cell(3, i + 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                }

                                // Ghi dữ liệu
                                for (int i = 0; i < dvgShowMh.Rows.Count; i++)
                                {
                                    for (int j = 0; j < dvgShowMh.Columns.Count; j++)
                                    {
                                        ws.Cell(i + 4, j + 1).Value = dvgShowMh.Rows[i].Cells[j].Value?.ToString() ?? "";
                                        ws.Cell(i + 4, j + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        ws.Cell(i + 4, j + 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                    }
                                }

                                // Thêm border
                                var tableRange = ws.Range(3, 1, dvgShowMh.Rows.Count + 3, dvgShowMh.Columns.Count);
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
    



