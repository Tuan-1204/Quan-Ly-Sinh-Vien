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



        public FQuanLyKhoa()
        {
            InitializeComponent();

        }
        // Show all Khoa
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            LoadTableKhoa();
        }
        // Load Khoa data into DataGridView
        private void LoadTableKhoa()
        {
            string query = "select * from Khoa";
            DataTable dt = DataProvider.LoadCSDL(query);
            dvgInKhoa.DataSource = dt;
        }

        // Add new Khoa
        private void btnAdd_Click(object sender, EventArgs e)
        {
            EnableControls(new List<Control> { txbIdKhoa, txbNameKhoa, btnSaveInfoKhoa });
            UnEnableControls(new List<Control> { btnEditInfoKhoa, btnDeleteInfoKhoa });
            ResetText(new List<Control> { txbIdKhoa, txbNameKhoa });
            txbIdKhoa.Focus();
        }

        // Save new Khoa
        private void btnSave_Click(object sender, EventArgs e)
        {
            string maKhoa = txbIdKhoa.Text;
            string tenKhoa = txbNameKhoa.Text;

            string query = $"insert into Khoa(MaKhoa, TenKhoa) values ('{maKhoa}',N'{tenKhoa}')";
            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Thêm mới khoa thành công");
                LoadTableKhoa();
                UnEnableControls(new List<Control> { txbIdKhoa, txbNameKhoa, btnDeleteInfoKhoa });
                ResetText(new List<Control> { txbIdKhoa, txbNameKhoa });
            }
            else
            {
                MessageBox.Show("Thêm mới khoa thất bại. Vui lòng xem lại !");
            }
        }

        // Show selected row in TextBoxes
        private void dvgInKhoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dvgInKhoa.SelectedRows.Count > 0)
            {
                var row = dvgInKhoa.SelectedRows[0];
                txbIdKhoa.Text = row.Cells["MaKhoa"].Value.ToString();
                txbNameKhoa.Text = row.Cells["TenKhoa"].Value.ToString();

                EnableControls(new List<Control> { txbIdKhoa, btnDeleteInfoKhoa });
                txbIdKhoa.Enabled = false;
            }
        }

        // Edit Khoa
        private void btnEdit_Click(object sender, EventArgs e)
        {
            string maKhoa = txbIdKhoa.Text;
            string tenKhoa = txbTenKhoa.Text;
            string query = $"UPDATE Khoa SET TenKhoa = N'{tenKhoa}' WHERE MaKhoa = '{maKhoa}'";
            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Cập nhật khoa thành công");
                LoadTableKhoa();
                UnEnableControls(new List<Control> { txbIdKhoa, txbNameKhoa, btnEditInfoKhoa, btnDeleteInfoKhoa});
                ResetText(new List<Control> { txbIdKhoa, txbNameKhoa });
            }
            else
            {
                MessageBox.Show("Cập nhật khoa thất bại. Vui lòng xem lại !");
            }
        }

        // Delete Khoa
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string maKhoa = txbIdKhoa.Text;
            string query = $"DELETE FROM Khoa WHERE MaKhoa = '{maKhoa}'";
            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Xóa khoa thành công");
                LoadTableKhoa();
                UnEnableControls(new List<Control> { txbIdKhoa, txbNameKhoa, btnEditInfoKhoa, btnDeleteInfoKhoa });
                ResetText(new List<Control> { txbIdKhoa , txbNameKhoa });
            }
            else
            {
                MessageBox.Show("Xóa khoa thất bại. Vui lòng xem lại !");
            }
        }

        // hàm khởi tạo control 
        private void EnableControls(List<Control> controls)
        {
            foreach (var control in controls)
            {
                control.Enabled = true;
            }
        }

        // hàm vô hiệu hóa control
        private void UnEnableControls(List<Control> controls)
        {
            foreach (var control in controls)
            {
                control.Enabled = false;
            }
        }
        // hàm reset control
        private void ResetText(List<Control> controls)
        {
            foreach (var control in controls)
            {
                control.Text = string.Empty;
            }
        }


        //nút lưu thông tin khoa
        private void btnSaveInfoKhoa_Click(object sender, EventArgs e)
        {

        }
        //nút sửa thông tin khoa
        private void btnEditInfoKhoa_Click(object sender, EventArgs e)
        {

        }
        //nút xóa thông tin khoa
        private void btnDeleteInfoKhoa_Click(object sender, EventArgs e)
        {

        }
        //nút thêm mới khoa
        private void btnAddKhoa_Click(object sender, EventArgs e)
        {

        }
        //nút hiển thị tất cả khoa
        private void btnShowALLKhoa_Click(object sender, EventArgs e)
        {

        }
        //hiển thị dữ liệu khoa lên datagridview
        private void dgvInKhoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
}
