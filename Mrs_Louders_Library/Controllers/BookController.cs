using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mrs_Louders_Library.Models;

namespace Mrs_Louders_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LouderLibraryDbContext _dbContext;

        // DbContext is injected via the constructor
        public BookController(LouderLibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            List<Book> books = _dbContext.Books.OrderBy(b=> b.Title).ToList();
            return Ok(books);
        }
        [HttpGet("{id}")]
        public IActionResult GetBooksById(int id)
        {
            List<Book> Books = _dbContext.Books.ToList();
            if (Books == null) { return NotFound(); }
            return Ok(Books);

        }
        [HttpPost]
        public IActionResult AddBooks(Book newBook)
        {

            _dbContext.Books.Add(newBook);
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]

        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            if (updatedBook.Id != id)
            {
                return BadRequest();
            }
            else if (!_dbContext.Books.Any(a => a.Id == id))
            {
                return NotFound();
            }
            else
            {
                _dbContext.Books.Update(updatedBook);
                _dbContext.SaveChanges();
                return NoContent();
            }
        }
    }
}
