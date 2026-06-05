using System.ComponentModel.DataAnnotations;

namespace QuanLyTrungTam.Models
{
    /// <summary>
    /// Bảng Học Viên - Lưu thông tin học viên của trung tâm
    /// </summary>
    public class HocVien
    {
        [Key]
        [StringLength(20)]
        public string MaHocVien { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; } = string.Empty;

        public DateTime NgaySinh { get; set; }

        [StringLength(10)]
        public string GioiTinh { get; set; } = string.Empty;

        [StringLength(15)]
        public string SoDienThoai { get; set; } = string.Empty;

        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        // Navigation property
        public ICollection<DangKyHoc>? DangKyHocs { get; set; }
    }
}
