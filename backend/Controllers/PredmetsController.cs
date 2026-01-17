using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredmetsController : ControllerBase
    {
        private readonly StudentskaContext _context;

        public PredmetsController(StudentskaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Predmet>>> GetAll()
        {
            return await _context.Predmets.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Predmet>> GetById(int id)
        {
            var predmet = await _context.Predmets
                .FirstOrDefaultAsync(p => p.IdPredmeta == id);

            if (predmet == null)
                return NotFound(new { message = "Predmet nije pronadjen" });

            return predmet;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Predmet predmet)
        {
            if (predmet.IdProfesora == null ||
                string.IsNullOrEmpty(predmet.Naziv) ||
                predmet.Espb == null ||
                string.IsNullOrEmpty(predmet.Status))
            {
                return BadRequest(new
                {
                    message = "ID_PROFESORA, NAZIV, ESPB i STATUS su obavezni"
                });
            }

            _context.Predmets.Add(predmet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = predmet.IdPredmeta },
                new
                {
                    message = "Predmet uspesno dodat",
                    ID_PREDMETA = predmet.IdPredmeta
                }
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Predmet updated)
        {
            var predmet = await _context.Predmets
                .FirstOrDefaultAsync(p => p.IdPredmeta == id);

            if (predmet == null)
                return NotFound(new { message = "Predmet nije pronadjen" });

            predmet.IdProfesora = updated.IdProfesora;
            predmet.Naziv = updated.Naziv;
            predmet.Espb = updated.Espb;
            predmet.Status = updated.Status;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Predmet uspesno izmenjen" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var predmet = await _context.Predmets
                .FirstOrDefaultAsync(p => p.IdPredmeta == id);

            if (predmet == null)
                return NotFound(new { message = "Predmet nije pronadjen" });

            _context.Predmets.Remove(predmet);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Predmet uspesno obrisan" });
        }
    }
}
