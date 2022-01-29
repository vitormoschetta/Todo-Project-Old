using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
                    : base(options) { }

        public DbSet<TodoItem> TodoItem { get; set; }        
    }
}