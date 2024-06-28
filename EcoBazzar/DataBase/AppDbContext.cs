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

    }
}
