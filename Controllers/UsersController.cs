using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VHECIntershipMain.Models;

namespace VHECIntershipMain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = new List<UserModel> {
                new UserModel { 
                    Id = 1,
                    Name = "TruongVM", 
                    Email = "truong@gmail.com",
                    Password = "123456789"}
            };
            return Ok(users);
        }
    }
}
