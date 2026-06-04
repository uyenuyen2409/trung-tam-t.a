using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyTrungTam.Data;
using QuanLyTrungTam.Models;

namespace QuanLyTrungTam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DangKyHocController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DangKyHocController(AppDbContext context) { _context = context; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.DangKyHocs
                .Include(d => d.HocVien).Include(d => d.KhoaHoc)
                .Select(d => new {
                    d.MaDangKy, d.NgayDangKy, d.MaHocVien,
                    TenHocVien = d.HocVien != null ? d.HocVien.HoTen : "",
                    d.MaKhoaHoc,
                    TenKhoaHoc = d.KhoaHoc != null ? d.KhoaHoc.TenKhoaHoc : "",
                    HocPhi = d.KhoaHoc != null ? d.KhoaHoc.HocPhi : 0
                }).ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DangKyHoc dk)
        {
            if (await _context.DangKyHocs.AnyAsync(d => d.MaDangKy == dk.MaDangKy))
                return BadRequest(new { message = "Mã đăng ký đã tồn tại!" });
            _context.DangKyHocs.Add(dk);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Đăng ký thành công!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var dk = await _context.DangKyHocs.FindAsync(id);
            if (dk == null) return NotFound();
            _context.DangKyHocs.Remove(dk);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Xóa thành công!" });
        }
    }
}
