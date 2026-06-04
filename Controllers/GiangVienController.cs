using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyTrungTam.Data;
using QuanLyTrungTam.Models;

namespace QuanLyTrungTam.Controllers
{
    /// <summary>
    /// API Controller quản lý Giảng Viên - CRUD operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GiangVienController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GiangVienController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/GiangVien - Lấy danh sách tất cả giảng viên
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiangVien>>> GetAll()
        {
            try
            {
                var list = await _context.GiangViens.ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi: " + ex.Message });
            }
        }

        /// <summary>
        /// POST: api/GiangVien - Thêm mới giảng viên
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GiangVien giangVien)
        {
            try
            {
                if (await _context.GiangViens.AnyAsync(g => g.MaGiangVien == giangVien.MaGiangVien))
                {
                    return BadRequest(new { message = "Mã giảng viên đã tồn tại!" });
                }

                _context.GiangViens.Add(giangVien);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Thêm giảng viên thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi: " + ex.Message });
            }
        }

        /// <summary>
        /// PUT: api/GiangVien/{id} - Cập nhật giảng viên
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] GiangVien giangVien)
        {
            try
            {
                var existing = await _context.GiangViens.FindAsync(id);
                if (existing == null) return NotFound(new { message = "Không tìm thấy giảng viên!" });

                existing.HoTen = giangVien.HoTen;
                existing.TrinhDo = giangVien.TrinhDo;
                existing.SoDienThoai = giangVien.SoDienThoai;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Cập nhật thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi: " + ex.Message });
            }
        }

        /// <summary>
        /// DELETE: api/GiangVien/{id} - Xóa giảng viên
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var gv = await _context.GiangViens.FindAsync(id);
                if (gv == null) return NotFound(new { message = "Không tìm thấy giảng viên!" });

                _context.GiangViens.Remove(gv);
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
