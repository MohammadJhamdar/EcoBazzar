using EcoBazzar.DataModel;
using Microsoft.EntityFrameworkCore;

namespace EcoBazzar.DataBase
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<SubCategory> subcategories { get; set; }
        public DbSet<Product> products { get; set; }

    }
}
