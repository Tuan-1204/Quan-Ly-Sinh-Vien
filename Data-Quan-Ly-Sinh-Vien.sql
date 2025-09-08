use [Quan-Ly-Sinh-Vien]

Go
create Table DangNhap(
  TenDangNhap nvarchar(50)  primary key,
  HoTen nvarchar(100),
  MatKhau varchar(50) not null,
  Quyen nvarchar(50) 
);

Go
Create table Khoa(
	MaKhoa varchar(50) primary key,
	TenKhoa nvarchar(100)
);

Go
Create Table SinhVien(
	MaSo int primary key,
	HoTen nvarchar(100),
	NgaySinh DateTime,
	GioiTinh bit,
	DiaChi nvarchar(100),
	DienThoai varchar(20) unique,
	MaKhoa varchar(50) references Khoa(MaKhoa)
);

Go
create table MonHoc(
	MaMH varchar(50) primary key,
	TenMH nvarchar(100),
	SoTiet int 
);

Go
create table KetQua(
MaSo int,
MaMH varchar(50) references MonHoc(MaMH),
Diem decimal,
primary key (MaSo, MaMH)
);


--Thêm dữ liệu admin và user
Go
insert into DangNhap ( TenDangNhap, HoTen, MatKhau, Quyen) 
values (N'Admin', N'NguyenAnhTuan', N'admin' , 1)


insert into DangNhap ( TenDangNhap, HoTen, MatKhau, Quyen)
values (N'uSer',N'User',N'user', 0)



--Thêm dữ liệu Khoa
Go
insert into Khoa (MaKhoa , TenKhoa) 
values (N'CNTT', N'Công Nghệ Thông Tin')


insert into Khoa (MaKhoa , TenKhoa) 
values (N'DL', N'Du Lịch')

insert into Khoa (MaKhoa , TenKhoa) 
values (N'NN', N'Ngôn Ngữ')

insert into Khoa (MaKhoa , TenKhoa) 
values (N'KD', N'Quản Trị Kinh Doanh')

--Thêm dữ liệu sinh viên
Go
insert into SinhVien(MaSo ,HoTen, NgaySinh, GioiTinh, DiaChi, DienThoai, MaKhoa)
values (1,N'Nguyễn Anh Tuấn', '2002-04-12', 0, N'Hưng Yên', '0989681864',N'CNTT')

INSERT INTO SinhVien (MaSo, HoTen, NgaySinh, GioiTinh, DiaChi, DienThoai, MaKhoa)
VALUES (2, N'Trương Đức Tâm', '2005-05-12', 0, N'Hà Nam', '0989456987', N'Dl');


insert into SinhVien(MaSo ,HoTen, NgaySinh, GioiTinh, DiaChi, DienThoai, MaKhoa)
	values(3,N'Nguyễn Ngọc Lan', '2006-09-07', 1 , N'Hưng Yên', '0333567984',N'NN')

	insert into SinhVien(MaSo ,HoTen, NgaySinh, GioiTinh, DiaChi, DienThoai, MaKhoa)
	values(4,N'Nguyễn Mạnh Hùng', '2001-04-12', 0 , N'Hưng Yên', '0916581524',N'KD')


		--Thêm dữ liệu môn học
		Go
		insert into MonHoc(MaMH, TenMH ,SoTiet) 
		values (N'CSDL', N'Cơ sở dữ liệu', 12)

		insert into MonHoc(MaMH, TenMH ,SoTiet) 
		values (N'GTCB', N'Giao Tiếp Cơ Bản', 6)

		insert into MonHoc(MaMH, TenMH ,SoTiet) 
		values (N'LTC#', N'Lập Trình .Net', 2)

		insert into MonHoc(MaMH, TenMH ,SoTiet) 
		values (N'LTHDT', N'Lập Trình Hướng Đối Tượng', 10)

		insert into MonHoc(MaMH, TenMH ,SoTiet) 
		values (N'TACN', N'Tiếng Anh Chuyên Ngành', 8)

		insert into MonHoc(MaMH, TenMH ,SoTiet) 
		values (N'TCC', N'Toán Cao Cấp', 12)


		--Thêm dữ liệu kết quả
		Go
		insert into KetQua(MaSo,MaMH, Diem)
		values (1,N'CSDL',7)

		insert into KetQua(MaSo,MaMH, Diem)
		values (1,N'LTHDT',7)

		insert into KetQua(MaSo,MaMH, Diem)
		values (2,N'GTCB',5)

		insert into KetQua(MaSo,MaMH, Diem)
		values (3,N'TACN',7)

		insert into KetQua(MaSo,MaMH, Diem)
		values (3,N'CSDL',7)

--Update khóa ngoại cho bảng Kết Qủa và Sinh Viên
Go
ALTER TABLE KetQua
ADD CONSTRAINT FK_KetQua_SinhVien
FOREIGN KEY (MaSo) REFERENCES SinhVien(MaSo);


--Hiển thị thông tin các bảng
Go
Select * from Khoa

Go
select * from SinhVien

Go
Select * from DangNhap

Go
 SELECT
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
from SinhVien sv
INNER JOIN Khoa k ON sv.MaKhoa = k.MaKhoa
 ORDER BY k.MaKhoa;

Go
SELECT 
 k.TenKhoa 
FROM SinhVien sv
INNER JOIN Khoa k ON sv.MaKhoa = k.MaKhoa
 ORDER BY k.MaKhoa;





