using crudSample.Data;
using crudSample.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace crudSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly ApplicationDBContext context;

        public AutorController(ApplicationDBContext context)
        {
            this.context = context; 
        }

        /// <summary>
        /// Obtiene un listado de todos los autores
        /// </summary>
        /// <returns>Devuelve una estructura json scon todos los elementos encontrados</returns>
        /// <response code="200">Autores devuelto con exito</response>
        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            return context.Autor.Include(x => x.Libros).ToList();
        }

        /// <summary>
        /// Obtiene un solo autor a partir de su id numerico
        /// </summary>
        /// <remarks>
        /// Indique el id correspondiente al autor deseado
        /// </remarks>
        /// <param name="id">Id numerico del autor</param>
        /// <returns>Devuelve una estructura json</returns>
        /// <response code="200">Autor encontrado es devuelto</response>
        /// <response code="404">El autor correspondiente al id no pudo ser encontrado</response>
        [HttpGet("{id}", Name = "ObtenerAutor")]
        public ActionResult<Autor> Get(int id)
        {
            var autor = context.Autor.Include(x => x.Libros).FirstOrDefault(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }

        /// <summary>
        /// Crea un nuevo autor
        /// </summary>
        /// <remarks>
        /// Solo es necesario el nombre del autor para dar de alta con la sig estructura:
        /// { "nombre": "string",   "libros": [ ] }}
        /// </remarks>
        /// <returns>Devuelve una estructura json de Autor correspondiente al usuario agregado</returns>
        /// <param name="autor">Estructura json de un autor a dar de alta</param>
        /// <response code="201">Autor dado de alta</response>
        /// <response code="500">Error al dar de alta</response>
        /// <response code="400">Error al dar de alta</response>
        [HttpPost]
        public ActionResult Post([FromBody] Autor autor)
        {
            try 
            {
                context.Autor.Add(autor);
                context.SaveChanges();

                return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autor);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error: " + e.Message.ToString());
            }
        }

        /// <summary>
        /// Actualiza informacion de un autor
        /// </summary>
        /// <remarks>
        /// Con la estructura recibida, se cambiaran los datos del autor correspondiente al id dado
        /// </remarks>
        /// <returns>Devuelve una estructura json de Autor correspondiente al usuario agregado</returns>
        /// <param name="autor">Estructura json del autor a ser actualizada</param>
        /// <param name="id">Id del autor a actualizar</param>
        /// <response code="200">Autor actualizado con exito</response>
        /// <response code="500">Error al actualizar</response>
        /// <response code="404">Id de autor no encontrado</response>
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Autor autor, int id)
        {
            if (id != autor.Id)
            {
                return BadRequest();
            }

            context.Entry(autor).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }


        /// <summary>
        /// Borra un autor especifico
        /// </summary>
        /// <param name="id">El id del autor a borrar</param>
        /// <returns>Devuelve la estructura json del autor borrado</returns>
        /// <response code="200">Autor borrado con exito</response>
        /// <response code="404">Autor correspondiente al id no encontrado</response>
        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            var autor = context.Autor.FirstOrDefault(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }
            context.Autor.Remove(autor); 
            context.SaveChanges();
            return autor;
        }
    }
}
