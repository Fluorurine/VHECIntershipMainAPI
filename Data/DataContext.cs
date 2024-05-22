using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VHECIntershipMain.Models;

namespace VHECIntershipMain.Data
{
    //This is the class for implemetn the context for many database.
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) 
        {
            
        }
        public DbSet<UserModel> Users { get; set; }
    }
}
    