using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyTrungTam.Models
{
    /// <summary>
    /// Bảng Đăng Ký Học - Bảng trung gian giữa HocVien và KhoaHoc (Nhiều-Nhiều)
    /// </summary>
    public class DangKyHoc
    {
        [Key]
        [StringLength(20)]
        public string MaDangKy { get; set; } = string.Empty;

        public DateTime NgayDangKy { get; set; }

        // Foreign keys
        [StringLength(20)]
        public string MaHocVien { get; set; } = string.Empty;

        [StringLength(20)]
        public string MaKhoaHoc { get; set; } = string.Empty;

        [ForeignKey("MaHocVien")]
        public HocVien? HocVien { get; set; }

        [ForeignKey("MaKhoaHoc")]
        public KhoaHoc? KhoaHoc { get; set; }

        // Navigation property
        public ICollection<ThanhToan>? ThanhToans { get; set; }
    }
}
