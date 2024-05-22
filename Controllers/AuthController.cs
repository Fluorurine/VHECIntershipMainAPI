using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using VHECIntershipMain.Data;
using VHECIntershipMain.Models;

namespace VHECIntershipMain.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        //THis can be future improve with user manager in identity framework.
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration, DataContext context)
        {
            _context = context;
            _configuration = configuration;
        }
        //private string CreateToken(UserModel user)
        //{
        //    List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName),
        //    new Claim("Username2","TruongVM")};
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:SigningKey").Value!));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        //    var token = new JwtSecurityToken(claims:claims,expires:DateTime.UtcNow.AddMinutes(5),signingCredentials:creds);
        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);  
        //    return jwt+"o------o"+ _configuration.GetSection("JWT:SigningKey").Value!;
        //}
        private string GenerateJwtToken(UserModel model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWT:SigningKey").Value!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, model.UserName)
            }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                //Issuer = _jwtSettings.Issuer,
                //Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        [HttpGet]
        public string testa()
        {
            UserModel user = new UserModel { Email = "aaa@gmail.com", PasswordHash = "aaaaa123", UserName = "TruongVMTestUser" };
            string data = GenerateJwtToken(user);
            return data;
        }
        //[HttpPost("register")]
        //public async Task<IActionResult> Register()
        //{

        //}


    }
}
