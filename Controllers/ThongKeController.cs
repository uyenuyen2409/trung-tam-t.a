using Microsoft.AspNetCore.Mvc;
using QuanLyTrungTam.DAL.Interfaces;

namespace QuanLyTrungTam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly IThongKeDAL _dal;
        public ThongKeController(IThongKeDAL dal) { _dal = dal; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try { return Ok(await _dal.GetThongKeTongHopAsync()); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }
    }
}
