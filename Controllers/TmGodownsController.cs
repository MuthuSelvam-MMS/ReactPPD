using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactPPD.Model;

namespace ReactPPD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TmGodownsController : ControllerBase
    {
        private readonly reactppdContext _context;

        public TmGodownsController(reactppdContext context)
        {
            _context = context;
        }

        // GET: api/TmGodowns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TmGodown>>> GetTmGodown()
        {
            return await _context.TmGodown.ToListAsync();
        }

        // GET: api/TmGodowns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TmGodown>> GetTmGodown(string id)
        {
            var tmGodown = await _context.TmGodown.FindAsync(id);

            if (tmGodown == null)
            {
                return NotFound();
            }

            return tmGodown;
        }

        // PUT: api/TmGodowns/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTmGodown(string id, TmGodown tmGodown)
        {
            if (id != tmGodown.GoDownCode)
            {
                return BadRequest();
            }

            _context.Entry(tmGodown).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TmGodownExists(id))
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

        // POST: api/TmGodowns
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TmGodown>> PostTmGodown(TmGodown tmGodown)
        {
            _context.TmGodown.Add(tmGodown);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TmGodownExists(tmGodown.GoDownCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTmGodown", new { id = tmGodown.GoDownCode }, tmGodown);
        }

        // DELETE: api/TmGodowns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TmGodown>> DeleteTmGodown(string id)
        {
            var tmGodown = await _context.TmGodown.FindAsync(id);
            if (tmGodown == null)
            {
                return NotFound();
            }

            _context.TmGodown.Remove(tmGodown);
            await _context.SaveChangesAsync();

            return tmGodown;
        }

        private bool TmGodownExists(string id)
        {
            return _context.TmGodown.Any(e => e.GoDownCode == id);
        }
    }
}
