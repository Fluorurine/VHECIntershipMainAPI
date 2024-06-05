

using System.ComponentModel.DataAnnotations;

namespace VHECIntershipMain.Models
{
    public class UserModel
    {
        public int Id { get; set; }
     
        public required string UserName { get; set; }
       
        public required string UserEmail { get; set; }
      
        public required string PasswordHash { get; set; }

        public  string UserRole
        {
            get; set;
        } = "Member";
        public string Address { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
