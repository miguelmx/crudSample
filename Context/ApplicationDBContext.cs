using crudSample.Entities;
using Microsoft.EntityFrameworkCore;

namespace crudSample.Data
{
    public class ApplicationDBContext:DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Autor> Autor { get; set; }
        public DbSet<Libro> Libro { get; set; }
    }
}
