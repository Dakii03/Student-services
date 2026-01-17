using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IspitniRoksController : ControllerBase
    {
        private readonly StudentskaContext _context;

        public IspitniRoksController(StudentskaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IspitniRok>>> GetAll()
        {
            return await _context.IspitniRoks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IspitniRok>> GetById(int id)
        {
            var rok = await _context.IspitniRoks.FindAsync(id);

            if (rok == null)
                return NotFound(new { message = "Ispitni rok nije pronadjen" });

            return rok;
        }

        [HttpPost]
        public async Task<ActionResult> Create(IspitniRok rok)
        {
            if (string.IsNullOrEmpty(rok.Naziv) ||
                string.IsNullOrEmpty(rok.SkolskaGod) ||
                string.IsNullOrEmpty(rok.StatusRoka))
            {
                return BadRequest(new
                {
                    message = "NAZIV, SKOLSKA_GOD i STATUS_ROKA su obavezni"
                });
            }

            _context.IspitniRoks.Add(rok);
            await _context.SaveChangesAsync();

            return StatusCode(201, new
            {
                message = "Ispitni rok uspesno dodat",
                ID_ROKA = rok.IdRoka
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, IspitniRok updated)
        {
            var rok = await _context.IspitniRoks.FindAsync(id);

            if (rok == null)
                return NotFound(new { message = "Ispitni rok nije pronadjen" });

            rok.Naziv = updated.Naziv;
            rok.SkolskaGod = updated.SkolskaGod;
            rok.StatusRoka = updated.StatusRoka;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Ispitni rok uspesno izmenjen" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var rok = await _context.IspitniRoks.FindAsync(id);

            if (rok == null)
                return NotFound(new { message = "Ispitni rok nije pronadjen" });

            _context.IspitniRoks.Remove(rok);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Ispitni rok uspesno obrisan" });
        }
    }
}
