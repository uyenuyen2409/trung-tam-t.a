# 🗄️ Mô Tả Cấu Trúc Cơ Sở Dữ Liệu - Hệ Thống Quản Lý Trung Tâm Tiếng Anh

Dưới đây là chi tiết mô tả 8 bảng trong CSDL của dự án. Bạn có thể chép nguyên văn nội dung này gửi cho Gemini hoặc ChatGPT và yêu cầu: *"Dựa vào cấu trúc bảng này, hãy viết cho tôi mã SQL để nạp vào Microsoft Access (hoặc SQL Server) nhằm vẽ được Sơ đồ Thực thể Liên kết (ERD) hoàn chỉnh."*

---

## 1. Bảng: TaiKhoans (Tài khoản người dùng)
*Quản lý tài khoản đăng nhập vào hệ thống.*
- `TenDangNhap` (Short Text): Tên đăng nhập **(Khóa chính - PK)**.
- `MatKhau` (Short Text): Mật khẩu truy cập (Đã băm SHA-256).

## 2. Bảng: HocViens (Học viên)
*Quản lý thông tin học viên của trung tâm.*
- `MaHocVien` (Short Text): Mã học viên **(Khóa chính - PK)**.
- `HoTen` (Short Text): Họ và tên đầy đủ.
- `NgaySinh` (Date/Time): Ngày sinh.
- `GioiTinh` (Short Text): Giới tính (Nam/Nữ).
- `SoDienThoai` (Short Text): Số điện thoại liên hệ.
- `Email` (Short Text): Địa chỉ email.

## 3. Bảng: KhoaHocs (Khóa học)
*Danh mục các khóa học trung tâm đang kinh doanh.*
- `MaKhoaHoc` (Short Text): Mã khóa học **(Khóa chính - PK)**.
- `TenKhoaHoc` (Short Text): Tên khóa học.
- `HocPhi` (Number/Double): Mức học phí.
- `SoBuoiHoc` (Number/Integer): Tổng số buổi học.

## 4. Bảng: LopHocs (Lớp học)
*Mỗi khóa học sẽ được mở ra nhiều lớp khác nhau (Quan hệ 1-N).*
- `MaLopHoc` (Short Text): Mã lớp học **(Khóa chính - PK)**.
- `TenLopHoc` (Short Text): Tên lớp.
- `PhongHoc` (Short Text): Phòng học được bố trí.
- `MaKhoaHoc` (Short Text): **(Khóa ngoại - FK)** trỏ về `KhoaHocs(MaKhoaHoc)`. (Quan hệ: Một khóa học có nhiều lớp).

## 5. Bảng: GiangViens (Giảng viên)
*Danh mục các giảng viên làm việc tại trung tâm.*
- `MaGiangVien` (Short Text): Mã giảng viên **(Khóa chính - PK)**.
- `HoTen` (Short Text): Tên giảng viên.
- `TrinhDo` (Short Text): Trình độ chuyên môn (Đại học/Thạc sĩ/IELTS 8.0+...).
- `SoDienThoai` (Short Text): Số điện thoại giảng viên.

## 6. Bảng: DangKyHocs (Đăng ký học - Quan hệ N-N)
*Bảng liên kết giải quyết quan hệ Nhiều-Nhiều: Một học viên có thể đăng ký nhiều khóa học, một khóa học có nhiều học viên.*
- `MaDangKy` (Short Text): Mã số phiếu đăng ký **(Khóa chính - PK)**.
- `NgayDangKy` (Date/Time): Ngày lập phiếu.
- `MaHocVien` (Short Text): **(Khóa ngoại - FK)** trỏ về `HocViens(MaHocVien)`.
- `MaKhoaHoc` (Short Text): **(Khóa ngoại - FK)** trỏ về `KhoaHocs(MaKhoaHoc)`.

## 7. Bảng: PhanCongGiangViens (Phân công Giảng viên - Quan hệ N-N)
*Bảng liên kết giải quyết quan hệ Nhiều-Nhiều: Một giảng viên có thể dạy nhiều lớp, một lớp có thể do nhiều giảng viên (giáo viên chính/trợ giảng) đứng lớp.*
- `MaPhanCong` (Short Text): Mã phân công **(Khóa chính - PK)**.
- `VaiTro` (Short Text): Vai trò đảm nhận (Giáo viên chính / Trợ giảng).
- `MaGiangVien` (Short Text): **(Khóa ngoại - FK)** trỏ về `GiangViens(MaGiangVien)`.
- `MaLopHoc` (Short Text): **(Khóa ngoại - FK)** trỏ về `LopHocs(MaLopHoc)`.

## 8. Bảng: ThanhToans (Hóa đơn thanh toán)
*Lưu vết các khoản đóng tiền học phí của từng phiếu Đăng ký học (Quan hệ 1-N).*
- `MaHoaDon` (Short Text): Mã hóa đơn **(Khóa chính - PK)**.
- `SoTienThu` (Number/Double): Số tiền đã thu.
- `NgayThanhToan` (Date/Time): Ngày thanh toán.
- `MaDangKy` (Short Text): **(Khóa ngoại - FK)** trỏ về `DangKyHocs(MaDangKy)`.
