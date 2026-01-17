using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IspitsController : ControllerBase
    {
        private readonly StudentskaContext _context;

        public IspitsController(StudentskaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ispit>>> GetAll()
        {
            return await _context.Ispits.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ispit>> GetById(int id)
        {
            var ispit = await _context.Ispits.FindAsync(id);

            if (ispit == null)
                return NotFound(new { message = "Ispit nije pronadjen" });

            return ispit;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Ispit ispit)
        {
            if (ispit.IdRoka == null ||
                ispit.IdPredmeta == null ||
                ispit.Datum == null)
            {
                return BadRequest(new
                {
                    message = "ID_ROKA, ID_PREDMETA i DATUM su obavezni"
                });
            }

            _context.Ispits.Add(ispit);
            await _context.SaveChangesAsync();

            return StatusCode(201, new
            {
                message = "Ispit uspesno dodat",
                ID_ISPITA = ispit.IdIspita
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Ispit updated)
        {
            var ispit = await _context.Ispits.FindAsync(id);

            if (ispit == null)
                return NotFound(new { message = "Ispit nije pronadjen" });

            ispit.IdRoka = updated.IdRoka;
            ispit.IdPredmeta = updated.IdPredmeta;
            ispit.Datum = updated.Datum;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Ispit uspesno izmenjen" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ispit = await _context.Ispits.FindAsync(id);

            if (ispit == null)
                return NotFound(new { message = "Ispit nije pronadjen" });

            _context.Ispits.Remove(ispit);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Ispit uspesno obrisan" });
        }
    }
}
