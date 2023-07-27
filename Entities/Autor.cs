using System.ComponentModel.DataAnnotations;

namespace crudSample.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public List<Libro>? Libros { get; set; } 
    }
}
