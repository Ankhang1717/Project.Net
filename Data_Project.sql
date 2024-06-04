use Project_C#
go


-- Tạo bảng dữ liệu
create table Phong (
	MaPhong nvarchar(10) primary key,
	TenPhong nvarchar(50) not null,
	SoLuongMay int not null,
	DonGiaTheoGio float not null,
	MaLoaiTB nvarchar(10)
)

create table NhaCungCap (
	MaNCC nvarchar(10) primary key,
	TenNCC nvarchar(100) not null,
	DienThoai nvarchar(10) not null,
	DiaChi nvarchar(100) not null
)

create table HoaDonBan (
	MaHDB nvarchar(10) primary key,
	MaMay nvarchar(10),
	MaPhong nvarchar(10),
	NgayThue date,
	GioVao time,
	GioRa time,
	MaNV nvarchar(10),
	TongTien float,
	GhiChu nvarchar(100)
)


create table HoaDonNhap(
	MaHDN nvarchar(10) primary key,
	MaNCC nvarchar(10),
	NgayNhap date,
	TongTien float
)


create table ChiTietHDN(
	MaHDN nvarchar(10) not null,
	MaTB nvarchar(10) not null,
	MaLoaiTB nvarchar(10) not null,
	MaNhomTB nvarchar(10) not null
)

create table NhanVien (
	MaNV nvarchar(10) primary key,
	TenNV nvarchar(50) not null,
	MaCa nvarchar(10),
	NamSinh date not null,
	GioiTinh nvarchar(10) not null,
	DiaChi nvarchar(100) not null,
	DienThoai nvarchar(10) not null
)

create table CaLam (
	MaCa nvarchar(10) primary key,
	TenCa nvarchar(20),
	ThoiGian nvarchar(20)
)

create table NhaBaoTri (
	MaNBT nvarchar(10) primary key,
	TenNBT nvarchar(50) not null,
	DiaChi nvarchar(100) not null,
	DienThoai nvarchar(10) not null
)

create table BaoTri (
	MaBT nvarchar(10) primary key,
	MaMay nvarchar(10),
	MaNBT nvarchar(10),
	TinhTrang nvarchar(50),
	NguyenNhan nvarchar(200),
	GiaiPhap nvarchar(200),
	ThanhTien float
)

create table May(
	MaMay nvarchar(10),
	MaPhong nvarchar(10),
	TrangThai int
)

create table ThietBi (
	MaTB nvarchar(10) primary key,
	TenTB nvarchar(50) not null,
	MaLoaiTB nvarchar(10),
	MaNhomTB nvarchar(10),
	MaNCC nvarchar(10),
	Gia float,
	BaoHanh float,
	SoLuong int
)

create table LoaiThietBi(
	MaLoaiTB nvarchar(10) primary key,
	TenLoaiTB nvarchar(50) not null
)

create table NhomThietBi (
	MaNhomTB nvarchar(10) primary key,
	TenNhomTB nvarchar(50)
)


-- Thêm Khóa
-- Thêm Khóa Chính
alter table ChiTietHDN
ADD PRIMARY KEY (MaHDN, MaTB, MaLoaiTB, MaNhomTB);

-- Thêm khóa chính mới bao gồm cả MaMay và MaPhong
ALTER TABLE May
ADD PRIMARY KEY (MaMay, MaPhong);

ALTER TABLE May
ALTER COLUMN MaMay NVARCHAR(10) NOT NULL;

ALTER TABLE May
ALTER COLUMN MaPhong NVARCHAR(10) NOT NULL;

ALTER TABLE BaoTri
ADD MaPhong NVARCHAR(10); 


alter table May
drop constraint PK__May__3A5BBB4172FE1C18

alter table Baotri
drop constraint PK_BT_May

alter table Hoadonban
drop constraint PK_HDB_May

-- Thêm Khóa Ngoại
alter table Phong
add constraint PK_Phong_LTB
foreign key (MaLoaiTB) references LoaiThietBi (MaLoaiTB)

alter table HoaDonBan
add constraint PK_HDB_May
foreign key (MaMay, MaPhong) references May (MaMay, MaPhong)

alter table HoaDonBan
add constraint PK_HDB_Phong
foreign key (MaPhong) references Phong (MaPhong)

alter table HoaDonBan
add constraint PK_HDB_NV
foreign key (MaNV) references NhanVien (MaNV)

alter table HoaDonNhap
add constraint PK_HDN_NCC
foreign key (MaNCC) references NhaCungCap (MaNCC)

alter table ChiTietHDN
add constraint PK_CTHDN_HDN
foreign key (MaHDN) references HoaDonNhap (MaHDN)

alter table ChiTietHDN
add constraint PK_CTHDN_TB
foreign key (MaTB) references ThietBi (MaTB)

alter table ChiTietHDN
add constraint PK_CTHDN_LTB
foreign key (MaLoaiTB) references LoaiThietBi (MaLoaiTB)

alter table ChiTietHDN
add constraint PK_CTHDN_NTB
foreign key (MaNhomTB) references NhomThietBi (MaNhomTB)

alter table NhanVien
add constraint PK_NV_CL
foreign key (MaCa) references CaLam (MaCa)

alter table BaoTri
add constraint PK_BT_May
foreign key (MaMay, MaPhong) references May (MaMay, MaPhong)

alter table BaoTri
add constraint PK_BT_NBT
foreign key (MaNBT) references NhaBaoTri (MaNBT)

alter table May
add constraint PK_May_Phong
foreign key (MaPhong) references Phong (MaPhong)

alter table ThietBi
add constraint PK_TB_LTB
foreign key (MaLoaiTB) references LoaiThietBi (MaLoaiTB)

alter table ThietBi
add constraint PK_TB_NTB
foreign key (MaNhomTB) references NhomThietBi (MaNhomTB)

alter table ThietBi
add constraint PK_TB_NCC
foreign key (MaNCC) references NhaCungCap (MaNCC)

-- Insert dữ liệu

insert into NhaCungCap (MaNCC, TenNCC, DienThoai, DiaChi)
values ('NCC01', N'Phong Vũ Computer', '18006867', N'Tầng 2, Số 47 Phố Thái Hà, Phường Trung Liệt, Quận Đống Đa, Thành phố Hà Nội')
insert into NhaCungCap (MaNCC, TenNCC, DienThoai, DiaChi)
values ('NCC02', N'An Phát Computer', '19000323', N'Tầng 5, Số 49 phố Thái Hà, Phường Trung Liệt, Quận Đống Đa, Thành phố Hà Nội, Việt Nam')
insert into NhaCungCap (MaNCC, TenNCC, DienThoai, DiaChi)
values ('NCC03', N'GEARVN', '18006975', N'162 - 164 Thái Hà, Phường Trung Liệt, Đống Đa, Hà Nội')
insert into NhaCungCap (MaNCC, TenNCC, DienThoai, DiaChi)
values ('NCC04', N'Phúc Anh', '19002164', N'Tầng 4, 89 Lê Duẩn, Hoàn Kiếm, Hà Nội')
insert into NhaCungCap (MaNCC, TenNCC, DienThoai, DiaChi)
values ('NCC05', N'HACOM', '19001903', N'Số 131 Lê Thanh Nghị - Hai Bà Trưng - Hà Nội')

insert into CaLam(MaCa, TenCa, ThoiGian)
values ('C01', N'Ca Sáng', '6:00 - 12:00')
insert into CaLam(MaCa, TenCa, ThoiGian)
values ('C02', N'Ca Chiều', '12:00 - 18:00')
insert into CaLam(MaCa, TenCa, ThoiGian)
values ('C03', N'Ca Tối', '18:00 - 24:00')
insert into CaLam(MaCa, TenCa, ThoiGian)
values ('C04', N'Ca Đêm', '24:00 - 6:00')

insert into NhaBaoTri(MaNBT, TenNBT, DiaChi, DienThoai)
values('NBT01', N'DỊCH VỤ CNTT & SỬA CHỮA MÁY TÍNH ACER', N'9 Đào Duy Anh, Phương Liên, Đống Đa, Hà Nội', '0190096960')
insert into NhaBaoTri(MaNBT, TenNBT, DiaChi, DienThoai)
values('NBT02', N'CHI NHÁNH ASUS HÀ NỘI', N'131 Thái Hà, Trung Liệt, Đống Đa, Hà Nội', '18006588')
insert into NhaBaoTri(MaNBT, TenNBT, DiaChi, DienThoai)
values('NBT03', N'CÔNG TY DỊCH VỤ KỸ THUẬT CHẤT LƯỢNG CAO - HQTS', N'165 Xã Đàn, Nam Đồng, Đống Đa, Hà Nội', '0243573855')
insert into NhaBaoTri(MaNBT, TenNBT, DiaChi, DienThoai)
values('NBT04', N'CÔNG TY CÔNG NGHỆ HP', N'55 Đại La, Phường Trương Định, Hà Nội', '0249348233')
insert into NhaBaoTri(MaNBT, TenNBT, DiaChi, DienThoai)
values('NBT05', N'CÔNG TY NTHH IT SYSTEMS VIETNAM', N'321/7 Phan Đình Phùng, Thành phố Hồ Chí Minh', '0909104579')

insert into LoaiThietBi(MaLoaiTB, TenLoaiTB)
values('L01', N'VIP')
insert into LoaiThietBi(MaLoaiTB, TenLoaiTB)
values('L02', N'Thường')

insert into NhomThietBi(MaNhomTB, TenNhomTB)
values('N01', N'Bàn phím')
insert into NhomThietBi(MaNhomTB, TenNhomTB)
values('N02', N'Chuột')
insert into NhomThietBi(MaNhomTB, TenNhomTB)
values('N03', N'Màn hình')
insert into NhomThietBi(MaNhomTB, TenNhomTB)
values('N04', N'Tai nghe')
insert into NhomThietBi(MaNhomTB, TenNhomTB)
values('N05', N'Ổ cứng')

insert into ThietBi(MaTB, TenTB, MaLoaiTB, MaNhomTB, MaNCC, Gia, BaoHanh, SoLuong)
values('TB001', N'Bàn phím cơ DAREU EK810 104KEY Black', 'L01', 'N01', 'NCC01', '500000', '12', '10')
insert into ThietBi(MaTB, TenTB, MaLoaiTB, MaNhomTB, MaNCC, Gia, BaoHanh, SoLuong)
values('TB002', N'Chuột Gaming LOGITECH G102 LightSync Black RGB', 'L01', 'N02', 'NCC02', '400000', '12', '10')
insert into ThietBi(MaTB, TenTB, MaLoaiTB, MaNhomTB, MaNCC, Gia, BaoHanh, SoLuong)
values('TB003', N'Màn hình Gaming MSI G255F 24.5 inch FHD IPS 180Hz', 'L01', 'N03', 'NCC03', '3000000', '36', '10')
insert into ThietBi(MaTB, TenTB, MaLoaiTB, MaNhomTB, MaNCC, Gia, BaoHanh, SoLuong)
values('TB004', N'Tai nghe Gaming DAREU EH416', 'L02', 'N04', 'NCC04', '300000', '12', '10')
insert into ThietBi(MaTB, TenTB, MaLoaiTB, MaNhomTB, MaNCC, Gia, BaoHanh, SoLuong)
values('TB005', N'Ổ cứng HDD 1TB Dell Enterpriser', 'L01', 'N05', 'NCC05', '2000000', '24', '10')
INSERT INTO ThietBi (MaTB, TenTB, MaLoaiTB, MaNhomTB, MaNCC, Gia, BaoHanh, SoLuong)
VALUES ('TB006', N'Ổ cứng HDD 500GB Dell Enterpriser', 'L02', 'N05', 'NCC05', '1200000', '24', '10');
INSERT INTO ThietBi (MaTB, TenTB, MaLoaiTB, MaNhomTB, MaNCC, Gia, BaoHanh, SoLuong)
VALUES ('TB007', N'Bàn phím cơ DAREU LK145', 'L02', 'N01', 'NCC01', '320000', '12', '10');
INSERT INTO ThietBi (MaTB, TenTB, MaLoaiTB, MaNhomTB, MaNCC, Gia, BaoHanh, SoLuong)
VALUES ('TB008', N'Chuột Gaming DAREU EM908', 'L02', 'N02', 'NCC01', '295000', '12', '10');
INSERT INTO ThietBi (MaTB, TenTB, MaLoaiTB, MaNhomTB, MaNCC, Gia, BaoHanh, SoLuong)
VALUES ('TB009', N'Màn hình BenQ XL2411 24 inch', 'L02', 'N03', 'NCC03', '2050000', '36', '10');
INSERT INTO ThietBi (MaTB, TenTB, MaLoaiTB, MaNhomTB, MaNCC, Gia, BaoHanh, SoLuong)
VALUES ('TB010', N'Tai nghe Gaming DAREU EH925S RGB', 'L01', 'N04', 'NCC04', '765000', '12', '10');


insert into Phong(MaPhong, TenPhong, SoLuongMay, DonGiaTheoGio, MaLoaiTB)
values('P01', N'Phòng VIP', 10, '20000', N'L01')
insert into Phong(MaPhong, TenPhong, SoLuongMay, DonGiaTheoGio, MaLoaiTB)
values('P02', N'Phòng không hút thuốc', 5, '10000', N'L02')
insert into Phong(MaPhong, TenPhong, SoLuongMay, DonGiaTheoGio, MaLoaiTB)
values('P03', N'Phòng hút thuốc', 5, '10000', N'L02')

insert into May(MaPhong, TrangThai)
values ('P01', 1)
insert into May(MaPhong, TrangThai)
values ('P01', 0)
insert into May(MaPhong, TrangThai)
values ('P01', 1)
insert into May(MaPhong, TrangThai)
values ('P01', 1)
insert into May(MaPhong, TrangThai)
values ('P01', 0)
insert into May(MaPhong, TrangThai)
values ('P01', 0)
insert into May(MaPhong, TrangThai)
values ('P01', 1)
insert into May(MaPhong, TrangThai)
values ('P01', 0)
insert into May(MaPhong, TrangThai)
values ('P01', 1)
insert into May(MaPhong, TrangThai)
values ('P01', 0)
insert into May(MaPhong, TrangThai)
values ('P02', 1)
insert into May(MaPhong, TrangThai)
values ('P02', 0)
insert into May(MaPhong, TrangThai)
values ('P02', 1)
insert into May(MaPhong, TrangThai)
values ('P02', 1)
insert into May(MaPhong, TrangThai)
values ('P02', 0)
insert into May(MaPhong, TrangThai)
values ('P03', 0)
insert into May(MaPhong, TrangThai)
values ('P03', 1)
insert into May(MaPhong, TrangThai)
values ('P03', 0)
insert into May(MaPhong, TrangThai)
values ('P03', 1)
insert into May(MaPhong, TrangThai)
values ('P03', 1)



insert into BaoTri(MaBT, MaMay, TinhTrang, NguyenNhan, GiaiPhap, ThanhTien)
values('MBT001', 'M003', N'Không hoạt động', N'Hỏng chuột', N'Mua chuột mới', '400000')
insert into BaoTri(MaBT, MaMay, TinhTrang, NguyenNhan, GiaiPhap, ThanhTien)
values('MBT002', 'M015', N'Không hoạt động', N'Lỗi ổ cứng', N'Mua ổ cứng mới', '1200000')
insert into BaoTri(MaBT, MaMay, TinhTrang, NguyenNhan, GiaiPhap, ThanhTien)
values('MBT003', 'M007', N'Không hoạt động', N'Lỗi màn hình', N'Sửa màn hình', '300000')

insert into NhanVien(MaNV, TenNV, MaCa, NamSinh, GioiTinh, DiaChi, DienThoai)
values('A1',N'Trịnh Thị Phi Yến','C01','2002-09-05',N'Nữ',N'Hà Nội','0236579765')
insert into NhanVien(MaNV, TenNV, MaCa, NamSinh, GioiTinh, DiaChi, DienThoai)
values('A2',N'Nguyễn Thị Hải My','C02','2003-03-02',N'Nữ',N'Hà Nội','0976543987')
insert into NhanVien(MaNV, TenNV, MaCa, NamSinh, GioiTinh, DiaChi, DienThoai)
values('A3',N'Lê Phú An Khang','C03','2004-08-05',N'Nam',N'Hà Nội','0865487643')
insert into NhanVien(MaNV, TenNV, MaCa, NamSinh, GioiTinh, DiaChi, DienThoai)
values('A4',N'Nguyễn Tiến Dũng','C04','2003-04-05',N'Nam',N'Bắc Ninh','0983576771')
insert into NhanVien(MaNV, TenNV, MaCa, NamSinh, GioiTinh, DiaChi, DienThoai)
values('A5',N'Nguyễn Minh Đức','C02','2001-07-01',N'Nam',N'Hải Phòng','0978612462')

insert into HoaDonBan(MaHDB, MaMay, MaPhong, NgayThue, GioVao, GioRa, MaNV, TongTien, GhiChu)
values('HDB0001', 'M005', 'P01', '2024-05-20', '08:30:00', '10:30:00', 'A1', '40000', '')
insert into HoaDonBan(MaHDB, MaMay, MaPhong, NgayThue, GioVao, GioRa, MaNV, TongTien, GhiChu)
values('HDB0002', 'M003', 'P01', '2024-04-30', '15:20', '18:20', 'A4', '60000', '')
insert into HoaDonBan(MaHDB, MaMay, MaPhong, NgayThue, GioVao, GioRa, MaNV, TongTien, GhiChu)
values('HDB0003', 'M007', 'P01', '2024-05-01', '8:00', '10:00', 'A1', '40000', '')

insert into HoaDonNhap(MaHDN, MaNCC, NgayNhap, TongTien)
values('HDN0001', 'NCC01', '2024-04-01', '11150000')
insert into HoaDonNhap(MaHDN, MaNCC, NgayNhap, TongTien)
values('HDN0002', 'NCC02', '2024-04-03', '4000000')
insert into HoaDonNhap(MaHDN, MaNCC, NgayNhap, TongTien)
values('HDN0003', 'NCC03', '2024-03-29', '50500000')

insert into ChiTietHDN(MaHDN, MaTB, MaLoaiTB, MaNhomTB)
values('HDN0001', 'TB001', 'L01', 'N01')
insert into ChiTietHDN(MaHDN, MaTB, MaLoaiTB, MaNhomTB)
values('HDN0002', 'TB002', 'L01', 'N02')
insert into ChiTietHDN(MaHDN, MaTB, MaLoaiTB, MaNhomTB)
values('HDN0003', 'TB009', 'L02', 'N03')



-- Tạo trigger sinh mã máy tự động
CREATE TRIGGER trg_InsertMay
ON May
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @NewMaMay NVARCHAR(10);
    DECLARE @MaxID INT;

    -- Lấy giá trị lớn nhất hiện tại của MaMay, nếu không có thì đặt là 0
    SELECT @MaxID = ISNULL(MAX(CAST(SUBSTRING(MaMay, 2, LEN(MaMay) - 1) AS INT)), 0) FROM May;

    -- Tăng giá trị lớn nhất lên 1 để tạo MaMay mới
    SET @NewMaMay = 'M' + RIGHT('000' + CAST(@MaxID + 1 AS NVARCHAR(10)), 3);

    -- Chèn bản ghi mới với giá trị MaMay đã sinh
    INSERT INTO May (MaMay, MaPhong, TrangThai)
    SELECT @NewMaMay, MaPhong, TrangThai
    FROM inserted;
END;

drop trigger trg_InsertMay
