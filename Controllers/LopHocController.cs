using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyTrungTam.Data;
using QuanLyTrungTam.Models;

namespace QuanLyTrungTam.Controllers
{
    /// <summary>
    /// API Controller quản lý Lớp Học - CRUD operations với INNER JOIN
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LopHocController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LopHocController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/LopHoc - Lấy danh sách lớp học (INNER JOIN với KhoaHoc để hiển thị tên khóa học)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _context.LopHocs
                    .Include(l => l.KhoaHoc)  // INNER JOIN
                    .Select(l => new
                    {
                        l.MaLopHoc,
                        l.TenLopHoc,
                        l.PhongHoc,
                        l.MaKhoaHoc,
                        TenKhoaHoc = l.KhoaHoc != null ? l.KhoaHoc.TenKhoaHoc : ""
                    })
                    .ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi: " + ex.Message });
            }
        }

        /// <summary>
        /// POST: api/LopHoc - Thêm mới lớp học
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LopHoc lopHoc)
        {
            try
            {
                if (await _context.LopHocs.AnyAsync(l => l.MaLopHoc == lopHoc.MaLopHoc))
                {
                    return BadRequest(new { message = "Mã lớp học đã tồn tại!" });
                }

                _context.LopHocs.Add(lopHoc);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Thêm lớp học thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi: " + ex.Message });
            }
        }

        /// <summary>
        /// PUT: api/LopHoc/{id} - Cập nhật lớp học
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] LopHoc lopHoc)
        {
            try
            {
                var existing = await _context.LopHocs.FindAsync(id);
                if (existing == null) return NotFound(new { message = "Không tìm thấy lớp học!" });

                existing.TenLopHoc = lopHoc.TenLopHoc;
                existing.PhongHoc = lopHoc.PhongHoc;
                existing.MaKhoaHoc = lopHoc.MaKhoaHoc;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Cập nhật thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi: " + ex.Message });
            }
        }

        /// <summary>
        /// DELETE: api/LopHoc/{id} - Xóa lớp học
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var lh = await _context.LopHocs.FindAsync(id);
                if (lh == null) return NotFound(new { message = "Không tìm thấy lớp học!" });

                _context.LopHocs.Remove(lh);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Xóa thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi: " + ex.Message });
            }
        }
    }
}
