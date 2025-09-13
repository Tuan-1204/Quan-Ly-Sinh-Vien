using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Sinh_Vien
{
    public partial class FKetQua : Form
    {
        public FKetQua()
        {
            InitializeComponent();
        }


        private void LoadTableKetQua()
        {
            string query = "select * from KetQua";
            DataTable dt = DataProvider.LoadCSDL(query);
            dgvDanhmucketqua.DataSource = dt;

        }
        //tìm kiếm kết quả theo lớp
        private void FKetQua_Load(object sender, EventArgs e)
        {
            string query = "SELECT MaLop FROM Lop";
            DataTable dt = DataProvider.LoadCSDL(query);
            cbSearchMaMH.DataSource = dt;
            cbSearchMaMH.DisplayMember = "MaLop";
            cbSearchMaMH.ValueMember = "MaLop";
            cbSearchMaMH.SelectedIndex = -1; // Không chọn mục nào ban đầu
        }


        // cau lệnh query -> lấy dữ liệu từ database ->  table ->hiển thị lên datagridview

        private void cbSearchMaMH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string malop = cbSearchMaMH.SelectedItem.ToString();
            string query = $"SELECT sv.MaSV, sv.HoTen, sv.GioiTinh, sv.NgaySinh, k.TenKhoa, l.TenLop, mh.TenMH, dk.DiemGK, dk.DiemCK, dk.DiemKhac, dk.DiemTong " +
                           $"FROM SinhVien sv " +
                           $"JOIN Lop l ON sv.MaLop = l.MaLop " +
                           $"JOIN Khoa k ON l.MaKhoa = k.MaKhoa " +
                           $"JOIN DangKyMonHoc dk ON sv.MaSV = dk.MaSV " +
                           $"JOIN MonHoc mh ON dk.MaMH = mh.MaMH " +
                           $"WHERE l.MaLop = '{malop}'";
            DataTable dt = DataProvider.LoadCSDL(query);
            dgvDiemSinhVien.DataSource = dt;

        }

        //hiển thị kết quả điểm sinh viên
        private void dgvDiemSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //hiển thị kết quả môn học
        private void dgvDanhmucketqua_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //thêm điểm
        private void btnAddDiem_Click(object sender, EventArgs e)
        {

        }
        //lưu điểm
        private void btnSaveDiem_Click(object sender, EventArgs e)
        {

        }
        //sửa điểm
        private void btnEditDiem_Click(object sender, EventArgs e)
        {

        }
        //xóa điểm
        private void btnDeleteDiem_Click(object sender, EventArgs e)
        {

        }
        //nút thêm điểm từ excel
        private void btnADDtoEXEL_Click(object sender, EventArgs e)
        {

        }
        //nút in điểm ra excel
        private void btnPrintToExel_Click(object sender, EventArgs e)
        {

        }
    }
}
