using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartClassAPI.Model;
using SmartClassAPI.Repository.PhongHocRepo;

namespace SmartClassAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongHocController : ControllerBase
    {
        private readonly IPhongHocRepository _phonghocRepo;

        public PhongHocController(IPhongHocRepository phongHocRepo)
        {
            _phonghocRepo = phongHocRepo;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_phonghocRepo.GetAll());
        }
        [HttpPost]
        public IActionResult Add(PhongHocModel phonghoc)
        {
            try
            {
                _phonghocRepo.Add(phonghoc);
                return new JsonResult("Thêm Thành Công");
            }
            catch
            {
                return new JsonResult("Thêm Thất Bại");
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, PhongHocVM phongHoc)
        {
            try
            {
                _phonghocRepo.Update(id, phongHoc);
                return new JsonResult("Đã Cập Nhật");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0) return BadRequest("Không có dữ liệu");
                _phonghocRepo.Delete(id);
                return new JsonResult("Đã xóa phòng Học");
            }
            catch
            {
                return new JsonResult("Đã có lỗi xảy ra");
            }

        }
    }
}