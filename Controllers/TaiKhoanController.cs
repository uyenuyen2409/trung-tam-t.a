using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyTrungTam.Data;
using QuanLyTrungTam.Models;

namespace QuanLyTrungTam.Controllers
{
    /// <summary>
    /// API Controller xử lý đăng nhập - Sử dụng truy vấn tham số (parameterized query)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaiKhoanController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// POST: api/TaiKhoan/login - Xác thực đăng nhập
        /// Sử dụng LINQ (parameterized query an toàn, chống SQL Injection)
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TaiKhoan loginInfo)
        {
            try
            {
                // LINQ tự động sử dụng parameterized query
                var taiKhoan = await _context.TaiKhoans
                    .FirstOrDefaultAsync(tk =>
                        tk.TenDangNhap == loginInfo.TenDangNhap &&
                        tk.MatKhau == loginInfo.MatKhau);

                if (taiKhoan == null)
                {
                    return Unauthorized(new { message = "Tên đăng nhập hoặc mật khẩu không đúng!" });
                }

                return Ok(new { message = "Đăng nhập thành công!", tenDangNhap = taiKhoan.TenDangNhap });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống: " + ex.Message });
            }
        }
    }
}
