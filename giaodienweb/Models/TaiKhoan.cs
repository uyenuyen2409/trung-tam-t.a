using System.ComponentModel.DataAnnotations;

namespace QuanLyTrungTam.Models
{
    /// <summary>
    /// Bảng Tài Khoản - Lưu thông tin đăng nhập hệ thống
    /// </summary>
    public class TaiKhoan
    {
        [Key]
        [StringLength(50)]
        public string TenDangNhap { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string MatKhau { get; set; } = string.Empty;
    }
}
