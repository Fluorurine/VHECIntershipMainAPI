namespace VHECIntershipMain.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }   
        public required string Password { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
