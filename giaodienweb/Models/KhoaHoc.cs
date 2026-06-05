using System.ComponentModel.DataAnnotations;

namespace QuanLyTrungTam.Models
{
    /// <summary>
    /// Bảng Khóa Học - Lưu thông tin các khóa đào tạo
    /// </summary>
    public class KhoaHoc
    {
        [Key]
        [StringLength(20)]
        public string MaKhoaHoc { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string TenKhoaHoc { get; set; } = string.Empty;

        public decimal HocPhi { get; set; }

        public int SoBuoiHoc { get; set; }

        // Navigation properties
        public ICollection<LopHoc>? LopHocs { get; set; }
        public ICollection<DangKyHoc>? DangKyHocs { get; set; }
    }
}
