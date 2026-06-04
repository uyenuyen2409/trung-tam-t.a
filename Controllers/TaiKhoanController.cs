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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] TaiKhoan registerInfo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(registerInfo.TenDangNhap) || string.IsNullOrWhiteSpace(registerInfo.MatKhau))
                    return BadRequest(new { message = "Tên đăng nhập và mật khẩu không được để trống!" });

                var hash = SecurityHelper.HashPassword(registerInfo.MatKhau);
                var tk = new TaiKhoan { TenDangNhap = registerInfo.TenDangNhap, MatKhau = hash };
                await _dal.RegisterAsync(tk);
                
                return Ok(new { message = "Đăng ký thành công!", tenDangNhap = tk.TenDangNhap });
            }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }
    }
}
