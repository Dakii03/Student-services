using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesorsController : ControllerBase
    {
        private readonly StudentskaContext _context;

        public ProfesorsController(StudentskaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesor>>> GetAll()
        {
            return await _context.Profesors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Profesor>> GetById(int id)
        {
            var profesor = await _context.Profesors.FindAsync(id);

            if (profesor == null)
                return NotFound(new { message = "Profesor nije pronadjen" });

            return profesor;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Profesor profesor)
        {
            if (string.IsNullOrEmpty(profesor.Ime) ||
                string.IsNullOrEmpty(profesor.Prezime) ||
                string.IsNullOrEmpty(profesor.Zvanje) ||
                profesor.DatumZap == null)
            {
                return BadRequest(new
                {
                    message = "IME, PREZIME, ZVANJE i DATUM_ZAP su obavezni"
                });
            }

            _context.Profesors.Add(profesor);
            await _context.SaveChangesAsync();

            return StatusCode(201, new
            {
                message = "Profesor uspesno dodat",
                ID_PROFESORA = profesor.IdProfesora
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Profesor updated)
        {
            var profesor = await _context.Profesors.FindAsync(id);

            if (profesor == null)
                return NotFound(new { message = "Profesor nije pronadjen" });

            profesor.Ime = updated.Ime;
            profesor.Prezime = updated.Prezime;
            profesor.Zvanje = updated.Zvanje;
            profesor.DatumZap = updated.DatumZap;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Profesor uspesno izmenjen" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var profesor = await _context.Profesors.FindAsync(id);

            if (profesor == null)
                return NotFound(new { message = "Profesor nije pronadjen" });

            _context.Profesors.Remove(profesor);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Profesor uspesno obrisan" });
        }
    }
}
