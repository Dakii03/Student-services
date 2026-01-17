using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZapisniksController : ControllerBase
    {
        private readonly StudentskaContext _context;

        public ZapisniksController(StudentskaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zapisnik>>> GetAll()
        {
            return await _context.Zapisniks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Zapisnik>>> GetByIspit(int id)
        {
            var rows = await _context.Zapisniks
                .Where(z => z.IdIspita == id)
                .ToListAsync();

            return rows;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Zapisnik zapisnik)
        {
            if (zapisnik.IdStudenta == null ||
                zapisnik.IdIspita == null ||
                zapisnik.Ocena == null ||
                zapisnik.Bodovi == null)
            {
                return BadRequest(new { message = "Sva polja su obavezna" });
            }

            _context.Zapisniks.Add(zapisnik);
            await _context.SaveChangesAsync();

            return StatusCode(201, new
            {
                message = "Zapisnik uspesno dodat"
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Zapisnik updated)
        {
            var zapisnik = await _context.Zapisniks.FirstOrDefaultAsync(z =>
                z.IdIspita == id &&
                z.IdStudenta == updated.IdStudenta
            );

            if (zapisnik == null)
                return NotFound(new { message = "Zapisnik nije pronadjen" });

            zapisnik.Ocena = updated.Ocena;
            zapisnik.Bodovi = updated.Bodovi;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Zapisnik uspesno izmenjen" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, [FromBody] int idStudenta)
        {
            var zapisnik = await _context.Zapisniks.FirstOrDefaultAsync(z =>
                z.IdIspita == id &&
                z.IdStudenta == idStudenta
            );

            if (zapisnik == null)
                return NotFound(new { message = "Zapisnik nije pronadjen" });

            _context.Zapisniks.Remove(zapisnik);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Zapisnik uspesno obrisan" });
        }
    }
}
