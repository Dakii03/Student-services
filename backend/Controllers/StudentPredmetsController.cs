using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentPredmetsController : ControllerBase
    {
        private readonly StudentskaContext _context;

        public StudentPredmetsController(StudentskaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentPredmet>>> GetAll()
        {
            return await _context.StudentPredmets
                .Select(sp => new StudentPredmet
                {
                    IdStudenta = sp.IdStudenta,
                    IdPredmeta = sp.IdPredmeta,
                    SkolskaGodina = sp.SkolskaGodina
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<StudentPredmet>>> GetByStudent(int id)
        {
            return await _context.StudentPredmets
                .Where(sp => sp.IdStudenta == id)
                .Select(sp => new StudentPredmet
                {
                    IdStudenta = sp.IdStudenta,
                    IdPredmeta = sp.IdPredmeta,
                    SkolskaGodina = sp.SkolskaGodina
                })
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Create(StudentPredmet sp)
        {
            if (sp.IdStudenta == null || sp.IdPredmeta == null || string.IsNullOrEmpty(sp.SkolskaGodina))
            {
                return BadRequest(new { message = "Sva polja su obavezna" });
            }

            _context.StudentPredmets.Add(sp);
            await _context.SaveChangesAsync();

            return StatusCode(201, new { message = "StudentPredmet uspesno dodat" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, StudentPredmet updated)
        {
            if (updated.IdPredmeta == null || string.IsNullOrEmpty(updated.SkolskaGodina))
            {
                return BadRequest(new
                {
                    message = "idPredmeta i skolskaGodina su obavezni"
                });
            }

            var sp = await _context.StudentPredmets.FirstOrDefaultAsync(x =>
                x.IdStudenta == id &&
                x.IdPredmeta == updated.IdPredmeta);

            if (sp == null)
                return NotFound(new { message = "Zapis o student_predmet nije pronadjen" });

            sp.SkolskaGodina = updated.SkolskaGodina;
            await _context.SaveChangesAsync();

            return Ok(new { message = "StudentPredmet uspesno izmenjen" });
        }

        [HttpDelete("{idStudent}/{idPredmet}")]
        public async Task<ActionResult> Delete(int idStudent, int idPredmet)
        {
            var sp = await _context.StudentPredmets.FirstOrDefaultAsync(x =>
                x.IdStudenta == idStudent &&
                x.IdPredmeta == idPredmet);

            if (sp == null)
                return NotFound(new { message = "StudentPredmet nije pronadjen" });

            _context.StudentPredmets.Remove(sp);
            await _context.SaveChangesAsync();

            return Ok(new { message = "StudentPredmet uspesno obrisan" });
        }
    }
}
