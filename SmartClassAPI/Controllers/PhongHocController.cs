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
                return Ok(_phonghocRepo.Add(phonghoc));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Khong co du lieu");
            _phonghocRepo?.Delete(id);
            return new JsonResult("Xoa thanh cong");
        }
    }
}
