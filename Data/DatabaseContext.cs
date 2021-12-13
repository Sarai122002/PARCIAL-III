using Microsoft.EntityFrameworkCore;
using ParcialComputo3.Models;

namespace ParcialComputo3.Data
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext() {}
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options) { }

        public DbSet<shoe> shoes { get; set;}
        public DbSet<Brand> Brands {get; set;}
    }
}