using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyTrungTam.Models
{
    /// <summary>
    /// Bảng Thanh Toán - Lưu thông tin hóa đơn thu tiền
    /// </summary>
    public class ThanhToan
    {
        [Key]
        [StringLength(20)]
        public string MaHoaDon { get; set; } = string.Empty;

        public decimal SoTienThu { get; set; }

        public DateTime NgayThanhToan { get; set; }

        // Foreign key
        [StringLength(20)]
        public string MaDangKy { get; set; } = string.Empty;

        [ForeignKey("MaDangKy")]
        public DangKyHoc? DangKyHoc { get; set; }
    }
}
