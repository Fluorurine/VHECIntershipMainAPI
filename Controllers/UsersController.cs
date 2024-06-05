using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using VHECIntershipMain.Data;
using VHECIntershipMain.Models;

namespace VHECIntershipMain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> getAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(new { status = 200, message = "Request Completed Sucessfully", data = users });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User not exist in database");
            }
            return Ok(new { status = 200, message = "Request Completed Sucessfully", data = user });
        }
        [HttpPost]
        public async Task<IActionResult> createNewUser(UserModel user)
        {
            var errors = await ValidateUserModelAsync(user);
            if (errors.Count > 0)
            {
                return BadRequest(new { status = 400, message = "Validation Failed", data = errors, errorText = true });
            }

            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();

            return Ok(new { status = 200, message = "Request Completed Successfully", data = await _context.Users.ToListAsync() });
        }
        [HttpPut]
        public async Task<IActionResult> editUser(UserModel user)
        {
            var editingUser = await _context.Users.FindAsync(user.Id);
            if (editingUser == null)
            {
                return BadRequest("User not found on Database");
            }
            user.UserName = editingUser.UserName;
            user.UserEmail = editingUser.UserEmail;
            user.PasswordHash = editingUser.PasswordHash;
            user.Address = editingUser.Address;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpDelete]
        public async Task<IActionResult> deleteUser(int Id)
        {
            var deletingUser = await _context.Users.FindAsync(Id);
            if (deletingUser == null)
            {
                return BadRequest("User not found on Database");
            }
            _context.Users.Remove(deletingUser);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

        private async Task<List<object>> ValidateUserModelAsync(UserModel user)
        {
            var errors = new List<object>();

            // Validate Username
            if (string.IsNullOrEmpty(user.UserName))
            {
                errors.Add(new { field = "UserName", message = "Username is required" });
            }
            else if (!Regex.IsMatch(user.UserName, @"^[a-zA-Z0-9]*$"))
            {
                errors.Add(new { field = "UserName", message = "Username contains unsuitable characters" });
            }

            // Validate Email
            if (string.IsNullOrEmpty(user.UserEmail))
            {
                errors.Add(new { field = "UserEmail", message = "Email is required" });
            }
            else if (!Regex.IsMatch(user.UserEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errors.Add(new { field = "UserEmail", message = "Invalid email format" });
            }
            else
            {
                var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.UserEmail == user.UserEmail);
                if (existingUser != null)
                {
                    errors.Add(new { field = "Emailexisted", message = "Email already taken" });
                }
            }

            // Validate Password
            if (string.IsNullOrEmpty(user.PasswordHash))
            {
                errors.Add(new { field = "PasswordHash", message = "Password is required" });
            }
            else if (!Regex.IsMatch(user.PasswordHash, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"))
            {
                errors.Add(new { field = "PasswordHash", message = "Password must be at least 8 characters long and contain both letters and numbers" });
            }

           

            return errors;
        }
    }
}

