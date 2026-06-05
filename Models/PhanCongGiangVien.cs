using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyTrungTam.Models
{
    /// <summary>
    /// Bảng Phân Công Giảng Viên-Bảng trung gian giữa GiangVien và LopHoc (Nhiều-Nhiều)
    /// </summary>
    public class PhanCongGiangVien
    {
        [Key]
        [StringLength(20)]
        public string MaPhanCong { get; set; } = string.Empty;

        [StringLength(50)]
        public string VaiTro { get; set; } = string.Empty;

        // Foreign keys
        [StringLength(20)]
        public string MaGiangVien { get; set; } = string.Empty;

        [StringLength(20)]
        public string MaLopHoc { get; set; } = string.Empty;

        [ForeignKey("MaGiangVien")]
        public GiangVien? GiangVien { get; set; }

        [ForeignKey("MaLopHoc")]
        public LopHoc? LopHoc { get; set; }
    }
}
