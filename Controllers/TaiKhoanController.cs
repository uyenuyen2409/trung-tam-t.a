using Microsoft.AspNetCore.Mvc;
using QuanLyTrungTam.DAL.Interfaces;
using QuanLyTrungTam.Helpers;
using QuanLyTrungTam.Models;

namespace QuanLyTrungTam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly ITaiKhoanDAL _dal;
        public TaiKhoanController(ITaiKhoanDAL dal) { _dal = dal; }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TaiKhoan loginInfo)
        {
            try
            {
                var hash = SecurityHelper.HashPassword(loginInfo.MatKhau);
                var tk = await _dal.LoginAsync(loginInfo.TenDangNhap, hash);
                if (tk == null) return Unauthorized(new { message = "Sai tên đăng nhập hoặc mật khẩu!" });
                return Ok(new { message = "Đăng nhập thành công!", tenDangNhap = tk.TenDangNhap });
            }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }
    }
}
