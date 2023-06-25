using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartClassAPI.Data;
using SmartClassAPI.Model;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartClassAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MyDbContext _context;

        public LoginController(MyDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<User>> Login(LoginMD user)
        {
            var us = _context.Users.Where(u => u.UserName == user.UserName && u.MatKhau == user.MatKhau).FirstOrDefault();
            if (us == null)
            {
                return BadRequest();
            }
            else
            {
                var Role = _context.LoaiUserData.Where(l => l.IdLoai == us.IdLoai).FirstOrDefault();
                HttpContext.Session.SetString("Username", us.UserName);
                HttpContext.Session.SetInt32("IdUser", us.IdUser);
                HttpContext.Session.SetString("HoTen", us.HoTen);
                HttpContext.Session.SetInt32("IdLoai", (int)us.IdLoai);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, us.UserName),
                    new Claim(ClaimTypes.Name, us.HoTen),
                    new Claim(ClaimTypes.Role, Role.TenLoai),
                };
                return Ok(us);
            }
        }
    }
}
