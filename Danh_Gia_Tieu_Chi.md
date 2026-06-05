# 📊 Bản Đồ Ánh Xạ Tiêu Chí Chấm Điểm
Tài liệu này chỉ ra **"Chỗ nào, ở đâu"** trong mã nguồn dự án đáp ứng được các yêu cầu khắt khe của Barem Giữa kỳ và Cuối kỳ.

---

## 🟢 PHẦN A: ĐÁP ỨNG YÊU CẦU GIỮA KỲ

**1. Tối thiểu 3-4 form quản lý riêng biệt:**
- **Nơi đáp ứng:** Thư mục `wwwroot/`. Chứa các file `hocvien.html`, `khoahoc.html`, `lophoc.html`, `giangvien.html` đóng vai trò là 4 form quản lý độc lập. Các hàm thao tác DOM nằm trong thẻ `<script>` ở cuối mỗi file này.

**2. Form Quan hệ N-N:**
- **Nơi đáp ứng:** Thư mục `wwwroot/`. Dự án có 2 form N-N: `dangkyhoc.html` (Quan hệ Học viên - Khóa học) và `phancong.html` (Quan hệ Giảng viên - Lớp học).

**3. Tính thực thi (Chạy không văng lỗi):**
- **Nơi đáp ứng:** File gốc `Program.cs`. Ứng dụng tích hợp tự động bắt lỗi và Middleware xử lý lỗi để đảm bảo không bị crash khi runtime. Lệnh `dotnet run` giúp khởi động server Kestrel bền bỉ.

**4. UI/UX cơ bản & 5. Điều hướng mượt mà:**
- **Nơi đáp ứng:** 
  - Thư mục `wwwroot/css/style.css` tạo bố cục Glassmorphism, Dark-theme xịn xò.
  - File `wwwroot/js/app.js` chứa hàm `renderSidebar()` giúp chèn Sidebar điều hướng dùng chung, bấm vào menu không cần f5 tải lại trang liên tục nhờ cách cấu trúc DOM tốt.

**6. Chuẩn đặt tên Control (tiền tố WinForms):**
- **Nơi đáp ứng:** Nằm trong nội dung HTML của tất cả các file `wwwroot/*.html`. 
  - TextBox: `id="txtMaHocVien"`, `id="txtHoTen"`
  - ComboBox: `id="cboGioiTinh"`, `id="cboKhoaHoc"`
  - DatePicker: `id="dtpNgaySinh"`, `id="dtpNgayDangKy"`
  - Button: `id="btnSave"`, `id="btnSearch"`
  - GridView: `id="dgvHocVien"`

**7. Trang trí & Nhận diện (Favicon tuỳ chỉnh):**
- **Nơi đáp ứng:** 
  - `wwwroot/img/favicon.png` (icon sinh viên học sinh tùy chỉnh).
  - Tích hợp trong mọi thẻ `<head>` của HTML (`<link rel="icon" type="image/png" href="img/favicon.png">`).
  - Giao diện mở lên dưới dạng App độc lập thông qua đoạn code Process/ProcessStartInfo nằm ở những dòng cuối cùng trong file `Program.cs`.

---

## 🔴 PHẦN B: ĐÁP ỨNG YÊU CẦU CUỐI KỲ

**1. Hoàn thiện tính năng với Database (Thêm/Sửa/Xóa/Tìm kiếm):**
- **Nơi đáp ứng:**
  - Logic UI (Tìm kiếm, hiển thị, gửi request lên Backend): Nằm ở cuối mỗi file `*.html` (Ví dụ `searchData()`, `saveData()`, `deleteData()` trong `hocvien.html`).
  - API Xử lý thực: File trong thư mục `Controllers/` (VD: `HocVienController.cs` nhận request CRUD + `/search`).

**2. Kiến trúc 3 Tầng (3-Tier):**
- **Nơi đáp ứng:**
  - **Tầng Entity:** Nằm gọn trong thư mục `Models/` (chỉ chứa get/set cơ bản).
  - **Tầng DAL:** Nằm trong thư mục `DAL/` (chứa code kết nối SQLite, xử lý thao tác DB thuần túy).
  - **Tầng GUI/Presentation:** Nằm trong thư mục `Controllers/` và `wwwroot/`. Tầng GUI chỉ gọi xuống DAL thông qua Dependency Injection.

**3. Chuẩn Interface DAL:**
- **Nơi đáp ứng:** Nằm tại file `DAL/Interfaces/IAppDAL.cs`. Toàn bộ các quy tắc (ví dụ: `IHocVienDAL`) đều được ép kiểu vào đây.

**4. Validation & Xử lý lỗi báo rõ ràng ra màn hình:**
- **Nơi đáp ứng:**
  - Kiểm tra phía Server: Nằm tại các file trong `DAL/Implementations/AppDAL.cs`. Lỗi trùng ID sẽ báo `throw new Exception("Mã học viên đã tồn tại!");`
  - Thông báo ra màn hình người dùng: File `wwwroot/js/app.js` có hàm `showToast()`. Khi API báo lỗi, hàm `saveData()` ở HTML sẽ gọi hàm `showToast(data.message, 'error')` để hiện thông báo mượt mà góc màn hình, hoàn toàn không quăng lỗi thô cho người dùng.

**5. Behavior Modal Form (Popup Overlay thao tác nhập liệu):**
- **Nơi đáp ứng:** Trên tất cả các trang quản lý trong `wwwroot/`. Khi bấm nút "Thêm", hệ thống sẽ kích hoạt 1 thẻ `<div class="modal" id="modalForm">`. Người dùng bắt buộc phải điền rồi bấm "Lưu" hoặc "Hủy" (nút X) để đóng popup, thỏa mãn nguyên lý `ShowDialog()` thay vì nhập trực tiếp lên lưới.

**6. Kết nối CSDL an toàn (Try-Catch-Finally) & Đóng kết nối thủ công:**
- **Nơi đáp ứng:** ĐÂY LÀ ĐIỂM CHẾT NGƯỜI NHẤT TRONG BAREM. Mọi thao tác thao tác DB đều được thực hiện tại thư mục `DAL/Implementations/AppDAL.cs`. Bạn hãy mở file này ra, trong BẤT CỨ HÀM NÀO bạn cũng sẽ thấy đoạn code thỏa mãn tuyệt đối:
  ```csharp
  var conn = _context.Database.GetDbConnection();
  try {
      await conn.OpenAsync();
      // Thực thi EF Core
  } catch (Exception ex) { throw; }
  finally {
      await conn.CloseAsync(); // Lệnh này nằm ở Finally
  }
  ```

**7. Bảo mật SQL (Parameterized Query):**
- **Nơi đáp ứng:** File `DAL/Implementations/AppDAL.cs`. Vì sử dụng Entity Framework Core LINQ (`await _context.HocViens.ToListAsync()`, `_context.TaiKhoans.Add()`), các truy vấn đều được framework tự động biên dịch thành truy vấn Parametized (`@p0, @p1`), xóa bỏ 100% khả năng bị SQL Injection.

**8. Bảo mật Mật khẩu (Hashing):**
- **Nơi đáp ứng:**
  - Thư mục `Helpers/SecurityHelper.cs` chứa bộ giải thuật băm thuật toán SHA-256 (`ComputeHash`).
  - Trong `Controllers/TaiKhoanController.cs` (Tại hàm `Login` và `Register`), mật khẩu người dùng gõ vào (`plain-text`) sẽ được đưa qua Helper này để Hash trước khi lưu hoặc so sánh với DB.
