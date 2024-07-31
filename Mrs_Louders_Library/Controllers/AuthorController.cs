using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mrs_Louders_Library.Models;

namespace Mrs_Louders_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        LouderLibraryDbContext dbContext = new LouderLibraryDbContext();
        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            List<Author> authors = dbContext.Authors.ToList();
            return Ok(authors);
        }
        [HttpGet("{id}")]
        public IActionResult GetAuthorsById(int id)
        {
            List<Author> authors = dbContext.Authors.ToList();
            if (authors == null) { return NotFound(); }
            return Ok(authors);

        }
        [HttpPost]
        public IActionResult AddAuthors( Author newAuthor)
        {
           
            dbContext.Authors.Add(newAuthor);
            dbContext.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]

        public IActionResult UpdateAuthor(int id, [FromBody] Author updatedAuthor)
        {
            if(updatedAuthor.Id != id)
            {
                return BadRequest();
            }
            else if(!dbContext.Authors.Any(a=> a.Id == id))
            {
                return NotFound();  
            }
            else
            {
                dbContext.Authors.Update(updatedAuthor);
                dbContext.SaveChanges();
                return NoContent(); 
            }
        }
    }
}
