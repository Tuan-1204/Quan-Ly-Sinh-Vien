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


        //tạo control
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
        //hàm xóa trắng control
        private void ResetText(List<Control> controls)
        {
            foreach (var control in controls)
            {
                control.Text = string.Empty;
            }
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
            string masv = dgvDiemSinhVien.CurrentRow.Cells["MaSV"].Value.ToString();
            string query = $"SELECT sv.MaSV, sv.HoTen, sv.GioiTinh, sv.NgaySinh, k.TenKhoa, l.TenLop, mh.TenMH, dk.DiemGK, dk.DiemCK, dk.DiemKhac, dk.DiemTong " +
                           $"FROM SinhVien sv " +
                           $"JOIN Lop l ON sv.MaLop = l.MaLop " +
                           $"JOIN Khoa k ON l.MaKhoa = k.MaKhoa " +
                           $"JOIN DangKyMonHoc dk ON sv.MaSV = dk.MaSV " +
                           $"JOIN MonHoc mh ON dk.MaMH = mh.MaMH " +
                           $"WHERE sv.MaSV = '{masv}'";
            DataTable dt = DataProvider.LoadCSDL(query);
        }
        //hiển thị kết quả môn học
        private void dgvDanhmucketqua_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string mamh = dgvDanhmucketqua.CurrentRow.Cells["MaMH"].Value.ToString();
            string query = $"SELECT sv.MaSV, sv.HoTen, sv.GioiTinh, sv.NgaySinh, k.TenKhoa, l.TenLop, mh.TenMH, dk.DiemGK, dk.DiemCK, dk.DiemKhac, dk.DiemTong " +
                           $"FROM SinhVien sv " +
                           $"JOIN Lop l ON sv.MaLop = l.MaLop " +
                           $"JOIN Khoa k ON l.MaKhoa = k.MaKhoa " +
                           $"JOIN DangKyMonHoc dk ON sv.MaSV = dk.MaSV " +
                           $"JOIN MonHoc mh ON dk.MaMH = mh.MaMH " +
                           $"WHERE mh.MaMH = '{mamh}'";
            DataTable dt = DataProvider.LoadCSDL(query);

        }

        //thêm điểm
        private void btnAddDiem_Click(object sender, EventArgs e)
        {
            string masv = txbIdDiemSINHVIEN.Text;
            string mamh = txbNamekqMH.Text;
            float diemgk = float.Parse(txbDiemlan1.Text);
            float diemck = float.Parse(txbDiemThiLai.Text);
            float diemkhac = float.Parse(btnAddDiem.Text);
            
            string query = $"insert into DangKyMonHoc(MaSV, MaMH, DiemGK, DiemCK, DiemKhac, DiemTong) values ('{masv}','{mamh}','{diemgk}','{diemck}','{diemkhac}')";
            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            
            {
                MessageBox.Show("Thêm mới điểm thành công");
                LoadTableKetQua();
                ResetText(new List<Control> { txbIdDiemSINHVIEN, txbNamekqMH, txbDiemlan1, txbDiemThiLai, btnAddDiem });
            }
            else
            {
                MessageBox.Show("Thêm mới điểm thất bại");
            }


        }
        //lưu điểm
        private void btnSaveDiem_Click(object sender, EventArgs e)
        {
            string masv = txbIdDiemSINHVIEN.Text;
            string mamh = txbNamekqMH.Text;
            float diemgk = float.Parse(txbDiemlan1.Text);
            float diemck = float.Parse(txbDiemThiLai.Text);
            float diemkhac = float.Parse(btnAddDiem.Text);
            
            string query = $"UPDATE DangKyMonHoc SET DiemGK = '{diemgk}', DiemCK = '{diemck}', DiemKhac = '{diemkhac}',' WHERE MaSV = '{masv}' AND MaMH = '{mamh}'";
            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Lưu điểm thành công");
                LoadTableKetQua();
                ResetText(new List<Control> { txbIdDiemSINHVIEN, txbNamekqMH, txbDiemlan1, txbDiemThiLai, btnAddDiem });
            }
            else
            {
                MessageBox.Show("Lưu điểm thất bại");
            }
        }
        //sửa điểm
        private void btnEditDiem_Click(object sender, EventArgs e)
        {
            string masv = txbIdDiemSINHVIEN.Text;
            string mamh = txbNamekqMH.Text;
            float diemgk = float.Parse(txbDiemlan1.Text);
            float diemck = float.Parse(txbDiemThiLai.Text);
            float diemkhac = float.Parse(btnAddDiem.Text);
     
            string query =$"UPDATE Dang KyMonHoc SET DiemGK = '{diemgk}', DiemCK = '{diemck}', DiemKhac = '{diemkhac}',' WHERE MaSV = '{masv}' AND MaMH = '{mamh}'";
            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Sửa điểm thành công");
                LoadTableKetQua();
                ResetText(new List<Control> { txbIdDiemSINHVIEN, txbNamekqMH, txbDiemlan1, txbDiemThiLai, btnAddDiem });
            }
            else
            {
                MessageBox.Show("Sửa điểm thất bại");
            }

        }
        //xóa điểm
        private void btnDeleteDiem_Click(object sender, EventArgs e)
        {
            string masv = txbIdDiemSINHVIEN.Text;
            string mamh = txbNamekqMH.Text;
            string query = $"DELETE FROM DangKyMonHoc WHERE MaSV = '{masv}' AND MaMH = '{mamh}'";
            int kq = DataProvider.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Xóa điểm thành công");
                LoadTableKetQua();
                ResetText(new List<Control> { txbIdDiemSINHVIEN, txbNamekqMH, txbDiemlan1, txbDiemThiLai, btnAddDiem });
            }
            else
            {
                MessageBox.Show("Xóa điểm thất bại");
            }

           }
        //nút thêm điểm từ excel
        private void btnADDtoEXEL_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    // Thực hiện các thao tác với file Excel tại đây
                }
            }

        }
        //nút in điểm ra excel
        private void btnPrintToExel_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog.FileName;
                    // Thực hiện các thao tác lưu dữ liệu ra file Excel tại đây
                }
            }

        }
    }
}
