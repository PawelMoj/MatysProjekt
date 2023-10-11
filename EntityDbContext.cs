using MatysProjekt.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MatysProjekt
{
    public class EntityDbContext : DbContext
    {
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<UserModel> Users { get; set; }

        public EntityDbContext() : base()
        {
        }

        public EntityDbContext(DbContextOptions<EntityDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserModel>().HasKey(x => x.Id);
            builder.Entity<UserModel>().ToTable("Users");
            builder.Entity<ProductModel>().HasKey(x => x.Id);
            builder.Entity<ProductModel>().ToTable("Products");
        }


    }
}
