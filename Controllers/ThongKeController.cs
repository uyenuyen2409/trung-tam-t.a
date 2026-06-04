using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyTrungTam.Data;

namespace QuanLyTrungTam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ThongKeController(AppDbContext context) { _context = context; }

        [HttpGet]
        public async Task<IActionResult> GetThongKe()
        {
            // COUNT DISTINCT - Đếm số học viên không trùng lặp
            var tongHocVien = await _context.HocViens.CountAsync();
            var tongKhoaHoc = await _context.KhoaHocs.CountAsync();
            var tongLopHoc = await _context.LopHocs.CountAsync();
            var tongGiangVien = await _context.GiangViens.CountAsync();
            var tongDangKy = await _context.DangKyHocs.CountAsync();

            // COUNT DISTINCT - Học viên đang học (đã đăng ký)
            var hocVienDangHoc = await _context.DangKyHocs
                .Select(d => d.MaHocVien).Distinct().CountAsync();

            // SUM - Tổng doanh thu
            var tongDoanhThu = await _context.ThanhToans.SumAsync(t => t.SoTienThu);

            return Ok(new {
                tongHocVien, tongKhoaHoc, tongLopHoc, tongGiangVien,
                tongDangKy, hocVienDangHoc, tongDoanhThu
            });
        }
    }
}
