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

        private void LoadTableSinhVien()
        {
            string query = "select * from SinhVien";
            DataTable dt = DataProvider.LoadCSDL(query);
            dvgInfoSinhVien.DataSource = dt;
        }
        
        //nút thêm mới sinh viên
        private void btnAddSinhvien_Click(object sender, EventArgs e)
        {
            EnabledControls(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, chekMen, chekWomen, txbPhone, txbAdress, txbNameClass, btnSaveSinhvien });
            UnEnabledControls(new List<Control> { btnEditSinhVien, btnDeleteSinhVien });
            ResetText(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, txbPhone, txbAdress, txbNameClass });
            txbIDSinhvien.Focus();
        }
        //nút lưu thông tin sinh viên
        private void btnSaveSinhvien_Click(object sender, EventArgs e)
        {
            string idSv = txbIDSinhvien.Text;
            string nameSv = txbNameSv.Text;
            DateTime dateSv = dateSinhVien.Value;
            string phoneSv = txbPhone.Text;
            string adressSv = txbAdress.Text;
            string nameClass = txbNameClass.Text;
            string gioiTinh = chekMen.Checked ? "Nam" : "Nữ";
            string query = $"insert into SinhVien(MaSV, TenSV, NgaySinh, GioiTinh, SDT, DiaChi, TenLop) values ('{idSv}',N'{nameSv}','{dateSv}','{gioiTinh}','{phoneSv}',N'{adressSv}',N'{nameClass}')";
            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Thêm mới sinh viên thành công");
                LoadTableSinhVien();
                UnEnabledControls(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, txbPhone, txbAdress, txbNameClass, btnSaveSinhvien });
                ResetText(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, txbPhone, txbAdress, txbNameClass });
            }
            else
            {
                MessageBox.Show("Thêm mới sinh viên thất bại. Vui lòng xem lại !");
            }
        }
        //nút sửa thông tin sinh viên
        private void btnEditSinhVien_Click(object sender, EventArgs e)
        {
            String idSv = txbIDSinhvien.Text;
            String nameSv = txbNameSv.Text;
            DateTime dateSv = dateSinhVien.Value;
            String phoneSv = txbPhone.Text;
            String adressSv = txbAdress.Text;
            String nameClass = txbNameClass.Text;
            String gioiTinh = chekMen.Checked ? "Nam" : "Nữ";
            String query = $"update SinhVien set TenSV=N'{nameSv}', NgaySinh='{dateSv}', GioiTinh=N'{gioiTinh}', SDT='{phoneSv}', DiaChi=N'{adressSv}', TenLop=N'{nameClass}' where MaSV='{idSv}'";
            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Cập nhật thông tin sinh viên thành công");
                LoadTableSinhVien();
                UnEnabledControls(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, txbPhone, txbAdress, txbNameClass, btnEditSinhVien, btnDeleteSinhVien });
                ResetText(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, txbPhone, txbAdress, txbNameClass });
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin sinh viên thất bại. Vui lòng xem lại !");
            }
        }
        //nút xóa thông tin sinh viên
        private void btnDeleteSinhVien_Click(object sender, EventArgs e)
        {
            string idSv = txbIDSinhvien.Text;
            string query = $"delete from SinhVien where MaSV='{idSv}'";
            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Xóa thông tin sinh viên thành công");
                LoadTableSinhVien();
                UnEnabledControls(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, txbPhone, txbAdress, txbNameClass, btnEditSinhVien, btnDeleteSinhVien });
                ResetText(new List<Control> { txbIDSinhvien, txbNameSv, dateSinhVien, txbPhone, txbAdress, txbNameClass });

            }
            else
            {
                MessageBox.Show("Xóa thông tin sinh viên thất bại. Vui lòng xem lại !");
            }
        }
        //nút tìm kiếm  lớp của sinh viên
        private void cbSelectionClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedClass = cbSelectionClass.SelectedItem.ToString();
            string query = $"select * from SinhVien where TenLop=N'{selectedClass}'";
            DataTable dt = DataProvider.LoadCSDL(query);
            dvgInfoSinhVien.DataSource = dt;

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
                txbNameSv.Text = row.Cells["TenSV"].Value.ToString();
                dateSinhVien.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                if (gioiTinh == "Nam")
                {
                    chekMen.Checked = true;
                    chekWomen.Checked = false;
                }
                else
                {
                    chekMen.Checked = false;
                    chekWomen.Checked = true;
                }
                txbPhone.Text = row.Cells["SDT"].Value.ToString();
                txbAdress.Text = row.Cells["DiaChi"].Value.ToString();
                txbNameClass.Text = row.Cells["TenLop"].Value.ToString();
                EnabledControls(new List<Control> { btnEditSinhVien, btnDeleteSinhVien });
                UnEnabledControls(new List<Control> { btnSaveSinhvien, txbIDSinhvien });

            }
        }
    }
}
    
    
