using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mrs_Louders_Library.Models;

namespace Mrs_Louders_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase

    {
        private readonly LouderLibraryDbContext _dbContext;

        // DbContext is injected via the constructor
        public AuthorController(LouderLibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            List<Author> authors = _dbContext.Authors.ToList();
            return Ok(authors);
        }
        [HttpGet("{id}")]
        public IActionResult GetAuthorsById(int id)
        {
            List<Author> authors = _dbContext.Authors.ToList();
            if (authors == null) { return NotFound(); }
            return Ok(authors);

        }
        [HttpPost]
        public IActionResult AddAuthors( Author newAuthor)
        {
           
            _dbContext.Authors.Add(newAuthor);
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]

        public IActionResult UpdateAuthor(int id, [FromBody] Author updatedAuthor)
        {
            if(updatedAuthor.Id != id)
            {
                return BadRequest();
            }
            else if(!_dbContext.Authors.Any(a=> a.Id == id))
            {
                return NotFound();  
            }
            else
            {
                _dbContext.Authors.Update(updatedAuthor);
                _dbContext.SaveChanges();
                return NoContent(); 
            }
        }
    }
}
