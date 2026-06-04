using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyTrungTam.Data;
using QuanLyTrungTam.Models;

namespace QuanLyTrungTam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanCongGiangVienController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PhanCongGiangVienController(AppDbContext context) { _context = context; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.PhanCongGiangViens
                .Include(p => p.GiangVien).Include(p => p.LopHoc)
                .Select(p => new {
                    p.MaPhanCong, p.VaiTro, p.MaGiangVien,
                    TenGiangVien = p.GiangVien != null ? p.GiangVien.HoTen : "",
                    p.MaLopHoc,
                    TenLopHoc = p.LopHoc != null ? p.LopHoc.TenLopHoc : ""
                }).ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PhanCongGiangVien pc)
        {
            if (await _context.PhanCongGiangViens.AnyAsync(p => p.MaPhanCong == pc.MaPhanCong))
                return BadRequest(new { message = "Mã phân công đã tồn tại!" });
            _context.PhanCongGiangViens.Add(pc);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Phân công thành công!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var pc = await _context.PhanCongGiangViens.FindAsync(id);
            if (pc == null) return NotFound();
            _context.PhanCongGiangViens.Remove(pc);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Xóa thành công!" });
        }
    }
}
