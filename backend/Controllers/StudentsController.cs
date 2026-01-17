using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentskaContext _context;

        public StudentsController(StudentskaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.IdStudenta == id);

            if (student == null)
            {
                return NotFound(new { message = "Student nije pronadjen" });
            }

            return student;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            if (student == null ||
                string.IsNullOrEmpty(student.Ime) ||
                string.IsNullOrEmpty(student.Prezime) ||
                string.IsNullOrEmpty(student.Smer) ||
                student.Broj == null ||
                string.IsNullOrEmpty(student.GodinaUpisa))
            {
                return BadRequest(new
                {
                    message = "IME, PREZIME, SMER, BROJ i GODINA_UPISA su obavezni"
                });
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetStudent),
                new { id = student.IdStudenta },
                new
                {
                    message = "Student uspesno dodat",
                    ID_STUDENTA = student.IdStudenta
                }
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, [FromBody] StudentUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.IdStudenta == id);

            if (student == null)
                return NotFound();

            student.Ime = dto.Ime;
            student.Prezime = dto.Prezime;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.IdStudenta == id);

            if (student == null)
            {
                return NotFound(new { message = "Student nije pronađen" });
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Student uspešno obrisan" });
        }
    }
}
