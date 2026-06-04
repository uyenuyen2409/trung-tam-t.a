using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyTrungTam.Models
{
    /// <summary>
    /// Bảng Lớp Học - Lưu thông tin lớp học thực tế
    /// </summary>
    public class LopHoc
    {
        [Key]
        [StringLength(20)]
        public string MaLopHoc { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string TenLopHoc { get; set; } = string.Empty;

        [StringLength(50)]
        public string PhongHoc { get; set; } = string.Empty;

        // Foreign key
        [StringLength(20)]
        public string MaKhoaHoc { get; set; } = string.Empty;

        [ForeignKey("MaKhoaHoc")]
        public KhoaHoc? KhoaHoc { get; set; }

        // Navigation property
        public ICollection<PhanCongGiangVien>? PhanCongGiangViens { get; set; }
    }
}
