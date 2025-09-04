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
        public FMain(DangNhap dn)
        {
            InitializeComponent();
        }


        //nút hiển thị tất cả 
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            //câu lệnh query -> lấy dữ liệu từ database ->  table ->hiển thị lên datagridview
            LoadTableMonHoc();
        }

        private  void LoadTableMonHoc()
        {
          string query = "select * from MonHoc";
            dt = DataProvider.LoadCSDL(query);
            dvgShow.DataSource = dt;
        }
        //thêm mới dữ liệu
        private void btnAdd_Click(object sender, EventArgs e)
        {
            EnableControls(new List<Control> { txbMaMH, txbTenMH, txbTinChi , btnSave});
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

        private void dvgShow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dvgShow.SelectedRows.Count > 0)
            {
                //lấy dòng dữ liệu được chọn
                var row = dvgShow.SelectedRows[0];
                //truyền giá trị dữ liệu lên textbox
                txbMaMH.Text = row.Cells["MaMH"].Value.ToString();
                txbTenMH.Text = row.Cells["TenMH"].Value.ToString();
                txbTinChi.Text = row.Cells["SoTiet"].Value.ToString();

                //hiển thị các textbox 
                EnableControls(new List<Control> { txbTenMH, txbTinChi, btnDelete, btnEdit });
              
            }
        }
    }
}
