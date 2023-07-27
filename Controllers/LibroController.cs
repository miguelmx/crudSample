using crudSample.Data;
using crudSample.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crudSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibroController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        public LibroController(ApplicationDBContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Obtiene un listado de todos los libros
        /// </summary>
        /// <returns>Devuelve una estructura json scon todos los elementos encontrados</returns>
        /// <response code="200">Libros devuelto con exito</response>
        [HttpGet]
        public ActionResult<List<Libro>> Get()
        {
            var libros = context.Libro.ToList();
            return libros;
        }

        /// <summary>
        /// Obtiene un solo libro a partir de su id numerico
        /// </summary>
        /// <remarks>
        /// Indique el id correspondiente al libro deseado
        /// </remarks>
        /// <param name="id">Id numerico del libro</param>
        /// <returns>Devuelve una estructura json</returns>
        /// <response code="200">libro encontrado es devuelto</response>
        /// <response code="404">El libro correspondiente al id no pudo ser encontrado</response>
        [HttpGet("{id}", Name = "ObtenerLibro")]
        public ActionResult<Libro> Get(int id)
        {
            var libro = context.Libro.FirstOrDefault(x => x.Id == id);

            if (libro == null)
            {
                return NotFound();
            }
            return libro;
        }

        /// <summary>
        /// Crea un nuevo libro
        /// </summary>
        /// <remarks>
        /// Solo es necesario el nombre del libro para dar de alta con la sig estructura:
        /// </remarks>
        /// <returns>Devuelve una estructura json de el libro correspondiente al agregado</returns>
        /// <param name="titulo">Nombre del libro a agregar</param>
        /// <param name="idAutor">Id del autor</param>
        /// <response code="200">Libro dado de alta</response>
        /// <response code="500">Error al dar de alta</response>
        /// <response code="404">El autor correspondiente al id dado no se encontro</response>
        [HttpPost()]
        public ActionResult Post(string titulo, int idAutor)
        {
            try
            {
                var autor = context.Autor.FirstOrDefault(x => x.Id == idAutor);
                if (autor == null)
                {
                    return NotFound();
                }
                Libro templibro = new Libro();
                templibro.Titulo = titulo;
                templibro.AutorId = idAutor;

                context.Libro.Add(templibro);
                context.SaveChanges();

                //return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autor);
                //return new CreatedAtRouteResult("ObtenerLibro", new { id = templibro.Id }, templibro);
                return Ok();
            }
            catch (Exception e )
            {
                throw new ApplicationException("Error: " + e.Message.ToString());
            }
            
            
        }

        /// <summary>
        /// Actualiza informacion de un libro
        /// </summary>
        /// <remarks>
        /// Con la estructura recibida, se cambiaran los datos del libro correspondiente al id dado
        /// </remarks>
        /// <returns>Devuelve una estructura json de el libro correspondiente al agregado</returns>
        /// <param name="autor">Estructura json del libro a ser actualizado</param>
        /// <param name="id">Id del libro a actualizar</param>
        /// <response code="200">libro actualizado con exito</response>
        /// <response code="500">Error al actualizar</response>
        /// <response code="404">Id de libro no encontrado</response>
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Libro libro, int id)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }
            context.Entry(libro).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Borra un libro especifico
        /// </summary>
        /// <param name="id">El id del libro a borrar</param>
        /// <returns>Devuelve la estructura json del libro borrado</returns>
        /// <response code="200">Libro borrado con exito</response>
        /// <response code="404">Libro correspondiente al id no encontrado</response>
        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {
            var libro = context.Libro.FirstOrDefault(x => x.Id == id);

            if (libro == null)
            {
                return NotFound(); 
            }
            context.Libro.Remove(libro);
            context.SaveChanges();
            return libro;
        }

    }
}
