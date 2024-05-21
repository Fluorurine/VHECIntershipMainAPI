using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
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
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User not exist in database");
            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> createNewUser(UserModel user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }
        [HttpPut]
        public async Task<IActionResult> editUser(UserModel user)
        {
            var editingUser = await _context.Users.FindAsync(user.Id);
            if (editingUser == null)
            {
                return BadRequest("User not found on Database");
            }
            user.Name = editingUser.Name;
            user.Email = editingUser.Email;
            user.Password = editingUser.Password;
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
    }
    }

