using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyTrungTam.Data;
using QuanLyTrungTam.Models;

namespace QuanLyTrungTam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThanhToanController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ThanhToanController(AppDbContext context) { _context = context; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.ThanhToans
                .Include(t => t.DangKyHoc)
                    .ThenInclude(d => d!.HocVien)
                .Include(t => t.DangKyHoc)
                    .ThenInclude(d => d!.KhoaHoc)
                .Select(t => new {
                    t.MaHoaDon, t.SoTienThu, t.NgayThanhToan, t.MaDangKy,
                    TenHocVien = t.DangKyHoc != null && t.DangKyHoc.HocVien != null ? t.DangKyHoc.HocVien.HoTen : "",
                    TenKhoaHoc = t.DangKyHoc != null && t.DangKyHoc.KhoaHoc != null ? t.DangKyHoc.KhoaHoc.TenKhoaHoc : ""
                }).ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ThanhToan tt)
        {
            if (await _context.ThanhToans.AnyAsync(t => t.MaHoaDon == tt.MaHoaDon))
                return BadRequest(new { message = "Mã hóa đơn đã tồn tại!" });
            _context.ThanhToans.Add(tt);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Thanh toán thành công!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var tt = await _context.ThanhToans.FindAsync(id);
            if (tt == null) return NotFound();
            _context.ThanhToans.Remove(tt);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Xóa thành công!" });
        }
    }
}
