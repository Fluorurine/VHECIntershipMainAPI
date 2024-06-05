using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class SushiDbContext : DbContext
    {
        public SushiDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().Property(u => u.CreatedDate).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Product>().Property(u => u.UpdatedDate).HasDefaultValueSql("GETDATE()");
        }


    }
}
