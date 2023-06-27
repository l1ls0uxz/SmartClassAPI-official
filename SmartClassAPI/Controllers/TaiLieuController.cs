using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartClassAPI.Model;
using SmartClassAPI.Repository.TaiLieuRepo;

namespace SmartClassAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiLieuController : ControllerBase
    {
        private readonly ITaiLieuRepository _taiLieuRepo;

        public TaiLieuController(ITaiLieuRepository taiLieuRepo)
        {
            _taiLieuRepo = taiLieuRepo;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_taiLieuRepo.GetAll());
        }
        [HttpPost]
        public IActionResult Add(TaiLieuModel taiLieu)
        {
            try
            {
                _taiLieuRepo.Add(taiLieu);
                return new JsonResult("Thêm Thành Công");
            }
            catch
            {
                return new JsonResult("Hãy điền đầy đủ thông tin!!");
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, TaiLieuVM taiLieu)
        {
            if (taiLieu.IdTaiLieu == id)
            {
                _taiLieuRepo.Update(id, taiLieu);
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0) return BadRequest("Không có dữ liệu");
                _taiLieuRepo.Delete(id);
                return new JsonResult("Đã xóa phòng Học");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}