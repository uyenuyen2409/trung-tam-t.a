using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyTrungTam.Data;
using QuanLyTrungTam.Models;

namespace QuanLyTrungTam.Controllers
{
    /// <summary>
    /// API Controller quản lý Khóa Học - CRUD operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class KhoaHocController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KhoaHocController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/KhoaHoc - Lấy danh sách tất cả khóa học
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhoaHoc>>> GetAll()
        {
            try
            {
                var list = await _context.KhoaHocs.ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi: " + ex.Message });
            }
        }

        /// <summary>
        /// GET: api/KhoaHoc/{id} - Lấy thông tin 1 khóa học
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<KhoaHoc>> GetById(string id)
        {
            try
            {
                var kh = await _context.KhoaHocs.FindAsync(id);
                if (kh == null) return NotFound(new { message = "Không tìm thấy khóa học!" });
                return Ok(kh);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi: " + ex.Message });
            }
        }

        /// <summary>
        /// POST: api/KhoaHoc - Thêm mới khóa học
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<KhoaHoc>> Create([FromBody] KhoaHoc khoaHoc)
        {
            try
            {
                if (await _context.KhoaHocs.AnyAsync(k => k.MaKhoaHoc == khoaHoc.MaKhoaHoc))
                {
                    return BadRequest(new { message = "Mã khóa học đã tồn tại!" });
                }

                _context.KhoaHocs.Add(khoaHoc);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = khoaHoc.MaKhoaHoc }, khoaHoc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi: " + ex.Message });
            }
        }

        /// <summary>
        /// PUT: api/KhoaHoc/{id} - Cập nhật khóa học
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] KhoaHoc khoaHoc)
        {
            try
            {
                if (id != khoaHoc.MaKhoaHoc)
                    return BadRequest(new { message = "Mã khóa học không khớp!" });

                var existing = await _context.KhoaHocs.FindAsync(id);
                if (existing == null) return NotFound(new { message = "Không tìm thấy khóa học!" });

                existing.TenKhoaHoc = khoaHoc.TenKhoaHoc;
                existing.HocPhi = khoaHoc.HocPhi;
                existing.SoBuoiHoc = khoaHoc.SoBuoiHoc;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Cập nhật thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi: " + ex.Message });
            }
        }

        /// <summary>
        /// DELETE: api/KhoaHoc/{id} - Xóa khóa học
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var kh = await _context.KhoaHocs.FindAsync(id);
                if (kh == null) return NotFound(new { message = "Không tìm thấy khóa học!" });

                _context.KhoaHocs.Remove(kh);
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
