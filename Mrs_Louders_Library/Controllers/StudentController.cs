using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mrs_Louders_Library.Models;

namespace Mrs_Louders_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly LouderLibraryDbContext _dbContext;

        // DbContext is injected via the constructor
        public StudentController(LouderLibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            Student result = _dbContext.Students.Find(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost()]
        public IActionResult AddStudent(StudentDTO newStudent)
        {
            if (_dbContext.Students.Any(u => u.Email.ToLower() == newStudent.Email.ToLower()))
            {
                return Ok(_dbContext.Students.FirstOrDefault(u => u.Email.ToLower() == newStudent.Email.ToLower()));

            }
            else
            {
                Student s = new Student

                {
                    FirstName = newStudent.FirstName,
                    LastName = newStudent.LastName,
                    Email = newStudent.Email,
                    Id = newStudent.Id,

                };

                _dbContext.Students.Add(s);
                _dbContext.SaveChanges();
                return Created($"/Api/Student/{s.Id}", s);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, StudentDTO updatedStudent)
        {
            Student s = _dbContext.Students.Find(id);
            if(s == null) { return NotFound(); }
            s.FirstName = updatedStudent.FirstName;
            s.LastName = updatedStudent.LastName;
            s.Email = updatedStudent.Email; 
            s.Id = updatedStudent.Id;
            _dbContext.Students.Update(s);
            _dbContext.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]   
        
        public IActionResult DeleteStudent(int id)
        {
            Student s = _dbContext.Students.Find(id);
            if(s == null ) { return NotFound(); }
            _dbContext.Students.Remove(s);
            _dbContext.SaveChanges();
            return NoContent() ;
        }
        
                
            
            
                
          

    }
}
