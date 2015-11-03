Create database QLKhoa
go
use QLKhoa

Create table tbl_khoa(	
	 makhoa varChar(20) not null,
	 tenkhoa nvarChar(50),
	 diachi nvarChar(50),
	 dienthoai varChar(12)
)


Alter table tbl_khoa 
add constraint PK_khoa primary key(makhoa)

Create table tbl_sinhvien(
	masv varchar(20) primary key,
	hoten nvarchar(50),
	noisinh nvarchar(50),
	makhoa varchar(20),
	constraint FR_sinhvien foreign key(makhoa) references tbl_khoa(makhoa)
)

Create table tbl_mon(
	mamon varchar(20) primary key,
	tenmon nvarchar(50),
)
Create table tbl_diem(
	masv varchar(20),
	mamon varchar(20),
	diem varchar(2),
	constraint FR_diem foreign key(masv) references tbl_sinhvien(masv),
	constraint FR_diem1 foreign key(mamon) references tbl_mon(mamon),
	constraint PK_diem primary key(masv, mamon)
)

Insert into tbl_khoa values('CNTT', N'Công nghệ thông tin', N'ĐH xây dựng', '098584378');
Insert into tbl_khoa values('CD', N'Cầu đường', N'ĐH xây dựng', '098584378');
Insert into tbl_khoa values('XD', N'Xây dựng dân dụng', N'ĐH xây dựng', '098584378');
Insert into tbl_khoa values('CB', N'Cảng biển', N'ĐH xây dựng', '098584378');
Insert into tbl_khoa values('KT', N'Kinh tế', N'ĐH xây dựng', '098584378');

Insert into tbl_mon values('CNPM', N'Cong nghe phan mem');
Insert into tbl_mon values('CNW', N'Cong nghe web');

Insert into tbl_sinhvien values('1544357', N'Nguyen Van Toan', N'Ha Noi', 'CNTT');
Insert into tbl_sinhvien values('1544359', N'Toan Tam', N'Ha Noi', 'CD');
Insert into tbl_sinhvien values('1544358', N'Tam Toan', N'Ha Noi', 'XD');
Insert into tbl_sinhvien values('1544355', N'Toan NV', N'Ha Noi', 'KT');

Insert tbl_diem values('1544357', 'CNPM', '10');
Insert tbl_diem values('1544359', 'CNPM', '9');
Insert tbl_diem values('1544358', 'CNW', '8');
Insert tbl_diem values('1544355', 'CNW', '7');