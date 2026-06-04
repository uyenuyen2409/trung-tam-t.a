using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyTrungTam.Data;
using QuanLyTrungTam.Models;

namespace QuanLyTrungTam.Controllers
{
    /// <summary>
    /// API Controller quản lý Học Viên - CRUD operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HocVienController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HocVienController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/HocVien - Lấy danh sách tất cả học viên
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HocVien>>> GetAll()
        {
            try
            {
                var list = await _context.HocViens.ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi: " + ex.Message });
            }
        }

        /// <summary>
        /// GET: api/HocVien/{id} - Lấy thông tin 1 học viên
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<HocVien>> GetById(string id)
        {
            try
            {
                var hv = await _context.HocViens.FindAsync(id);
                if (hv == null) return NotFound(new { message = "Không tìm thấy học viên!" });
                return Ok(hv);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi: " + ex.Message });
            }
        }

        /// <summary>
        /// POST: api/HocVien - Thêm mới học viên (parameterized query qua EF Core)
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<HocVien>> Create([FromBody] HocVien hocVien)
        {
            try
            {
                // Kiểm tra trùng mã
                if (await _context.HocViens.AnyAsync(h => h.MaHocVien == hocVien.MaHocVien))
                {
                    return BadRequest(new { message = "Mã học viên đã tồn tại!" });
                }

                _context.HocViens.Add(hocVien);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = hocVien.MaHocVien }, hocVien);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi: " + ex.Message });
            }
        }

        /// <summary>
        /// PUT: api/HocVien/{id} - Cập nhật thông tin học viên
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] HocVien hocVien)
        {
            try
            {
                if (id != hocVien.MaHocVien)
                    return BadRequest(new { message = "Mã học viên không khớp!" });

                var existing = await _context.HocViens.FindAsync(id);
                if (existing == null) return NotFound(new { message = "Không tìm thấy học viên!" });

                existing.HoTen = hocVien.HoTen;
                existing.NgaySinh = hocVien.NgaySinh;
                existing.GioiTinh = hocVien.GioiTinh;
                existing.SoDienThoai = hocVien.SoDienThoai;
                existing.Email = hocVien.Email;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Cập nhật thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi: " + ex.Message });
            }
        }

        /// <summary>
        /// DELETE: api/HocVien/{id} - Xóa học viên
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var hv = await _context.HocViens.FindAsync(id);
                if (hv == null) return NotFound(new { message = "Không tìm thấy học viên!" });

                _context.HocViens.Remove(hv);
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
