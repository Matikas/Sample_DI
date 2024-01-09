using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Databases
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
