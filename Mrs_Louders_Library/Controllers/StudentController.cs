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
        LouderLibraryDbContext dbContext = new LouderLibraryDbContext();
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            Student result = dbContext.Students.Find(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddStudent(StudentDTO newStudent)
        {
            if (dbContext.Students.Any(u => u.Email.ToLower() == newStudent.Email.ToLower()))
            {
                return Ok(dbContext.Students.FirstOrDefault(u => u.Email.ToLower() == newStudent.Email.ToLower()));

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

                dbContext.Students.Add(s);
                dbContext.SaveChanges();
                return Created($"/Api/Student/{s.Id}", s);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, StudentDTO updatedStudent)
        {
            Student s = dbContext.Students.Find(id);
            if(s == null) { return NotFound(); }
            s.FirstName = updatedStudent.FirstName;
            s.LastName = updatedStudent.LastName;
            s.Email = updatedStudent.Email; 
            s.Id = updatedStudent.Id;
            dbContext.Students.Update(s);
            dbContext.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]   
        
        public IActionResult DeleteStudent(int id)
        {
            Student s = dbContext.Students.Find(id);
            if(s == null ) { return NotFound(); }
            dbContext.Students.Remove(s);
            dbContext.SaveChanges();
            return NoContent() ;
        }
        
                
            
            
                
          

    }
}
