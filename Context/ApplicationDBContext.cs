using crudSample.Entities;
using Microsoft.EntityFrameworkCore;

namespace crudSample.Data
{
    public class ApplicationDBContext:DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
#pragma warning disable CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente
        public DbSet<Autor> Autor { get; set; }
#pragma warning restore CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente
        public DbSet<Libro> Libro { get; set; }
    }
}
