using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactPPD.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace ReactPPD.Controllers
{
    [EnableCors("*", "*", "*")]
    [Route("api/[controller]")]
    [ApiController]
    public class PricelistController : ControllerBase
    {
        private readonly reactppdContext _context;

        public PricelistController(reactppdContext context)
        {
            _context = context;
        }

        // GET: api/Pricelist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TmCompany>>> GetTmCompany()
        {
            return await _context.TmCompany.ToListAsync();
        }

        // GET: api/Pricelist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TmCompany>> GetTmCompany(string id)
        {
            var tmCompany = await _context.TmCompany.FindAsync(id);

            if (tmCompany == null)
            {
                return NotFound();
            }

            return tmCompany;
        }

        // PUT: api/Pricelist/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTmCompany(string id, TmCompany tmCompany)
        {
            if (id != tmCompany.CompanyCode)
            {
                return BadRequest();
            }

            _context.Entry(tmCompany).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TmCompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pricelist
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TmCompany>> PostTmCompany(TmCompany tmCompany)
        {
            _context.TmCompany.Add(tmCompany);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TmCompanyExists(tmCompany.CompanyCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTmCompany", new { id = tmCompany.CompanyCode }, tmCompany);
        }

        // DELETE: api/Pricelist/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TmCompany>> DeleteTmCompany(string id)
        {
            var tmCompany = await _context.TmCompany.FindAsync(id);
            if (tmCompany == null)
            {
                return NotFound();
            }

            _context.TmCompany.Remove(tmCompany);
            await _context.SaveChangesAsync();

            return tmCompany;
        }

        private bool TmCompanyExists(string id)
        {
            return _context.TmCompany.Any(e => e.CompanyCode == id);
        }
    }
}
