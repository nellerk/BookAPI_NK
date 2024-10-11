using BookAPI_NK.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookAPI_NK.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
    }
}
