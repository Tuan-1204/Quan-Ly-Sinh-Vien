using Microsoft.Reporting.WinForms;
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
    public partial class FReport : Form
    {

        private string _option;
        public FReport(string option)
        {
            InitializeComponent();
            _option = option;
        }

        private void FReport_Load(object sender, EventArgs e)
        {
            if (_option == "XemDSSV")
            {
                try
                {
                    reportViewer1.LocalReport.ReportEmbeddedResource = "Quan_Ly_Sinh_Vien.ReportSinhVien.rdlc";
                    string query = @" SELECT 
                                            MaSo,
                                            HoTen,
                                            CONVERT(VARCHAR(10), NgaySinh, 103) AS NgaySinh,
                                            CASE 
                                                WHEN GioiTinh = 1 THEN N'Nữ'
                                                WHEN GioiTinh = 0 THEN N'Nam'
                                                ELSE N'Không xác định'
                                            END AS GioiTinh,
                                            DiaChi,
                                            DienThoai,
                                            MaKhoa
                                        FROM SinhVien
                                        ORDER BY MaSo";
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSetSV";
                    reportDataSource.Value = DataProvider.LoadCSDL(query);
                    this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if (_option == "XemDSSVTheoKhoa")
            {

                try
                {
                    reportViewer1.LocalReport.ReportEmbeddedResource = "Quan_Ly_Sinh_Vien.ReportSVTheoKhoa.rdlc";
                    string querySV = @"SELECT
                                    sv.MaSo,
                                    sv.HoTen,
                                    FORMAT(sv.NgaySinh, 'dd/MM/yyyy') AS NgaySinh,
                                    CASE
                                        WHEN sv.GioiTinh = 1 THEN N'Nữ'
                                        WHEN sv.GioiTinh = 0 THEN N'Nam'
                                        ELSE N'Không xác định'
                                    END AS GioiTinh,
                                    sv.DiaChi,
                                    sv.DienThoai
                                FROM SinhVien sv
                                INNER JOIN Khoa k ON sv.MaKhoa = k.MaKhoa
                                ORDER BY k.MaKhoa;";
                     
                    string queryKhoa = @"SELECT 
                                 k.TenKhoa 
                                FROM SinhVien sv
                                INNER JOIN Khoa k ON sv.MaKhoa = k.MaKhoa
                                ORDER BY k.MaKhoa;";

                    var dtSV = DataProvider.LoadCSDL(querySV);
                    var dtKhoa = DataProvider.LoadCSDL(queryKhoa);

                    var reportDataSource1 = new ReportDataSource
                    {
                        Name = "DataSetSVTheoKhoa",
                        Value = dtSV
                    };

                    var reportDataSource2 = new ReportDataSource
                    {
                        Name = "DataSetKhoa",
                        Value = dtKhoa
                    };

                 
                    reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                    reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
              
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if(_option == "XemDiem")
            {

            }
            else if(_option == "XemDiemTheoMon")
            {

            }
            this.reportViewer1.RefreshReport();
        }
    }
}
