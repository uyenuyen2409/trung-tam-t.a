using System.ComponentModel.DataAnnotations;

namespace QuanLyTrungTam.Models
{
    /// <summary>
    /// Bảng Giảng Viên - Lưu thông tin giảng viên
    /// </summary>
    public class GiangVien
    {
        [Key]
        [StringLength(20)]
        public string MaGiangVien { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; } = string.Empty;

        [StringLength(100)]
        public string TrinhDo { get; set; } = string.Empty;

        [StringLength(15)]
        public string SoDienThoai { get; set; } = string.Empty;

        // Navigation property
        public ICollection<PhanCongGiangVien>? PhanCongGiangViens { get; set; }
    }
}
