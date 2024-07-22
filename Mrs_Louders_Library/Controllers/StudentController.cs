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
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
    [HttpPost]
    public IActionResult AddStudent(StudentDTO newStudent)
    {
        if (dbContext.Student.Any(u => u.Email.ToLower() == newStudent.Email.ToLower()))
        {
            return Ok(dbContext.Student.FirstOrDefault(u => u.Email.ToLower() == newStudent.Email.ToLower()));
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

            dbContext.Student.Add(s);
            dbContext.SaveChanges();
            return Created($"/Api/Student/{u.StudentId}", s);
        }
    }
}
