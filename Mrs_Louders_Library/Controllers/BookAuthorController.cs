using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mrs_Louders_Library.Models;

namespace Mrs_Louders_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAuthorController : ControllerBase
    {
        private readonly LouderLibraryDbContext dbContext;
        public BookAuthorController (LouderLibraryDbContext dbContext)
        {
            dbContext = dbContext;
        }
        
        [HttpGet()]
        public IActionResult GetAll()
        {
            List<Book> result = dbContext.Books.ToList();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Book result = dbContext.Books.Find(id);
            if (result == null) { return NotFound(); }
            return Ok(result);  
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Book book)
        {
            Book newBook = new Book
            {

            };
            book.Id = 0;
            dbContext.Books.Add(book);
            dbContext.SaveChanges();
            return Created($"/Api/Books/{book.Id}", newBook);
        }
    }
}






    

