using Microsoft.AspNetCore.Identity;

namespace VHECIntershipMain.Models
{
    public class UserModel : IdentityUser
    {
        //public int Id { get; set; }
        //public required string UserName { get; set; }
        //public required string Email { get; set; }   
        //public required string Password { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
