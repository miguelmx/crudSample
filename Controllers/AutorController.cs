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

        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            return context.Autor.Include(x => x.Libros).ToList();
        }

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

        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            var autor = context.Autor.FirstOrDefault(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            context.Autor.Remove(autor); //context.Entry(autor).State = EntityState.Deleted;
            context.SaveChanges();
            return autor;
        }
    }
}
