# 🎓 English Center Management System

Dự án này là hệ thống Quản lý Trung tâm Tiếng Anh, được xây dựng với kiến trúc 3-Tier (3 Tầng), sử dụng **ASP.NET Core Web API**, **SQLite**, và **Vanilla HTML/CSS/JS**.

## 📂 Cấu trúc thư mục (Folder Tree)

```text
quanlyhcvien_uyen/
├── Controllers/
│   ├── DangKyHocController.cs
│   ├── GiangVienController.cs
│   ├── HocVienController.cs
│   ├── KhoaHocController.cs
│   ├── LopHocController.cs
│   ├── PhanCongGiangVienController.cs
│   ├── TaiKhoanController.cs
│   ├── ThanhToanController.cs
│   └── ThongKeController.cs
├── DAL/
│   ├── Implementations/
│   │   └── AppDAL.cs
│   └── Interfaces/
│       └── IAppDAL.cs
├── Data/
│   └── AppDbContext.cs
├── Helpers/
│   └── SecurityHelper.cs
├── Models/
│   ├── DangKyHoc.cs
│   ├── GiangVien.cs
│   ├── HocVien.cs
│   ├── KhoaHoc.cs
│   ├── LopHoc.cs
│   ├── PhanCongGiangVien.cs
│   ├── TaiKhoan.cs
│   └── ThanhToan.cs
├── wwwroot/
│   ├── css/
│   │   └── style.css
│   ├── img/
│   │   └── favicon.png
│   ├── js/
│   │   └── app.js
│   ├── dangkyhoc.html
│   ├── giangvien.html
│   ├── hocvien.html
│   ├── index.html
│   ├── khoahoc.html
│   ├── login.html
│   ├── lophoc.html
│   ├── phancong.html
│   ├── thanhtoan.html
│   └── thongke.html
├── appsettings.json
├── Program.cs
├── QuanLyTrungTam.csproj
└── QuanLyTrungTam.db
```

---

## 📝 Giải thích chi tiết từng phần

### 1. Thư mục `Models/` (Tầng Entity)
- Chứa cấu trúc định nghĩa các bảng trong Cơ sở dữ liệu.
- Các file trong này chỉ thuần túy khai báo Properties (thuộc tính `get; set;`), Khóa chính (Primary Key), và Mối quan hệ Khóa ngoại (Foreign Key) như `HocVien.cs`, `KhoaHoc.cs`, `DangKyHoc.cs`...

### 2. Thư mục `DAL/` (Tầng Data Access Layer)
Đảm nhiệm toàn bộ logic giao tiếp với Cơ sở dữ liệu (Database). Tầng này được thiết kế theo cấu trúc Interface-driven chuẩn xác để đáp ứng yêu cầu khắt khe:
- **`DAL/Interfaces/IAppDAL.cs`**: Chứa toàn bộ các *Khuôn mẫu (Interface)* như `IHocVienDAL`, `IKhoaHocDAL`. Nó quy định DAL phải có các hàm gì (Thêm, Sửa, Xóa, Tìm kiếm).
- **`DAL/Implementations/AppDAL.cs`**: Code thực thi các Interface trên. Tất cả mọi hàm trong file này đều BẮT BUỘC có cấu trúc: `var conn = GetDbConnection(); try { Mở_Kết_Nối; Xử_Lý_DB; } catch { Bắt_Lỗi; } finally { conn.Close(); Đóng_Kết_Nối; }`.

### 3. Thư mục `Data/`
- **`AppDbContext.cs`**: Lớp trung tâm cấu hình Entity Framework Core. Nó giúp "ánh xạ" các file trong `Models/` vào CSDL SQLite (`QuanLyTrungTam.db`). Nó cũng chịu trách nhiệm nạp dữ liệu mẫu ban đầu (Seed Data).

### 4. Thư mục `Controllers/` (Tầng Logic / API endpoints)
- Nhận dữ liệu từ giao diện (Frontend), chuyển xuống tầng `DAL` để xử lý, sau đó trả kết quả lại dưới định dạng JSON. Không có lệnh tương tác DB trực tiếp nào ở đây.
- Ví dụ: `HocVienController.cs` có các hàm `Get()`, `Post()`, `Put()`, `Delete()` gọi trực tiếp `_dal.GetAllAsync()`, `_dal.AddAsync()`...

### 5. Thư mục `Helpers/`
- **`SecurityHelper.cs`**: Cung cấp công cụ mã hóa. Chứa hàm băm mật khẩu ra mã hóa SHA-256 (hỗ trợ bảo mật khi Đăng ký và Đăng nhập).

### 6. Thư mục `wwwroot/` (Tầng Presentation / GUI)
Chứa toàn bộ giao diện Frontend. Hoạt động tương đương với WinForms UI nhưng hiện đại hơn:
- **`.html` (Các trang Web):** Mỗi file tương đương 1 Form. Ví dụ `login.html` (Form Đăng Nhập/Đăng ký), `hocvien.html` (Form Quản lý Sinh viên)... Mọi ô nhập liệu đều có prefix Winforms chuẩn (`txt`, `cbo`, `dtp`, `btn`).
- **`css/style.css`**: Code tùy chỉnh làm đẹp form, tạo Dark Mode, Glassmorphism, Pop-up Modal...
- **`js/app.js`**: Chứa các hàm dùng chung để điều hướng trang (sidebar), hiển thị thông báo góc màn hình (toast), và xác thực bảo mật.
- **`img/favicon.png`**: Icon tùy chỉnh của chương trình (thay cho icon mặc định).

### 7. Các file Hệ thống gốc
- **`Program.cs`**: File chạy chính. Ở đây diễn ra việc: Cấu hình Web API, "Tiêm" (Dependency Injection) các class DAL vào Controllers, thiết lập tự tạo CSDL nếu chưa có, và tự động gọi hệ điều hành mở trình duyệt dưới dạng cửa sổ độc lập (Desktop Wrapper `--app mode`).
- **`QuanLyTrungTam.db`**: File Database SQLite sinh ra khi chạy chương trình (chứa toàn bộ dữ liệu).
- **`appsettings.json`**: File cấu hình chuỗi kết nối Database.
- **`QuanLyTrungTam.csproj`**: File liệt kê cấu hình thư viện C# đang dùng.