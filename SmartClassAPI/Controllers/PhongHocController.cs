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
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var phongHoc = _phonghocRepo.GetById(id);
            if (phongHoc == null)
            {
                return NotFound();
            }
            else
            {
                if (phongHoc.TenPhongHoc == null)
                {
                    // Xử lý khi TenPhongHoc là null
                    phongHoc.TenPhongHoc = "Tên phòng học không khả dụng";
                }
                return Ok(phongHoc);
            }
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
                return new JsonResult("Hãy điền đầy đủ thông tin!!");
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
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
