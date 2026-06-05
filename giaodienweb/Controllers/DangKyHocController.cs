using Microsoft.AspNetCore.Mvc;
using QuanLyTrungTam.DAL.Interfaces;
using QuanLyTrungTam.Models;

namespace QuanLyTrungTam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DangKyHocController : ControllerBase
    {
        private readonly IDangKyHocDAL _dal;
        public DangKyHocController(IDangKyHocDAL dal) { _dal = dal; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try { return Ok(await _dal.GetAllDetailsAsync()); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DangKyHoc dangKy)
        {
            try { await _dal.AddAsync(dangKy); return Ok(new { message = "Đăng ký thành công!" }); }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try { await _dal.DeleteAsync(id); return Ok(new { message = "Hủy thành công!" }); }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }
    }
}
