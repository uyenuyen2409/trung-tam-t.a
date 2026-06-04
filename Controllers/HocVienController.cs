using Microsoft.AspNetCore.Mvc;
using QuanLyTrungTam.DAL.Interfaces;
using QuanLyTrungTam.Models;

namespace QuanLyTrungTam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocVienController : ControllerBase
    {
        private readonly IHocVienDAL _dal;
        public HocVienController(IHocVienDAL dal) { _dal = dal; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try { return Ok(await _dal.GetAllAsync()); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try { return Ok(await _dal.GetByIdAsync(id)); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            try { return Ok(await _dal.SearchAsync(keyword ?? "")); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HocVien hocVien)
        {
            try { await _dal.AddAsync(hocVien); return Ok(new { message = "Thêm thành công!" }); }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] HocVien hocVien)
        {
            try { await _dal.UpdateAsync(hocVien); return Ok(new { message = "Cập nhật thành công!" }); }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try { await _dal.DeleteAsync(id); return Ok(new { message = "Xóa thành công!" }); }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }
    }
}
