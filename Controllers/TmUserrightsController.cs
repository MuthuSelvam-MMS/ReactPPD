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
    public class TmUserrightsController : ControllerBase
    {
        private readonly reactppdContext _context;

        public TmUserrightsController(reactppdContext context)
        {
            _context = context;
        }

        // GET: api/TmUserrights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TmUserright>>> GetTmUserright()
        {
            return await _context.TmUserright.ToListAsync();
        }

        // GET: api/TmUserrights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TmUserright>> GetTmUserright(string id)
        {

            var tmUser = _context.TmUserright.Where(x => x.UserId == id).FirstOrDefault();
            if (tmUser == null)
            {
                return BadRequest(new { Message = "No UserRights for User Assign" }); ;
            }
            else
            {
                var tmUserright = await _context.TmUserright.FindAsync(tmUser.UserId, tmUser.BranchCode, tmUser.MenuId);                            
                return tmUserright;
            }
        }
        // PUT: api/TmUserrights/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTmUserright(string id, TmUserright tmUserright)
        {
            if (id != tmUserright.UserId)
            {
                return BadRequest();
            }

            _context.Entry(tmUserright).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TmUserrightExists(id))
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

        // POST: api/TmUserrights
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TmUserright>> PostTmUserright(TmUserright tmUserright)
        {
            _context.TmUserright.Add(tmUserright);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TmUserrightExists(tmUserright.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTmUserright", new { id = tmUserright.UserId }, tmUserright);
        }

        // DELETE: api/TmUserrights/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TmUserright>> DeleteTmUserright(string id)
        {
            var tmUserright = await _context.TmUserright.FindAsync(id);
            if (tmUserright == null)
            {
                return NotFound();
            }

            _context.TmUserright.Remove(tmUserright);
            await _context.SaveChangesAsync();

            return tmUserright;
        }

        private bool TmUserrightExists(string id)
        {
            return _context.TmUserright.Any(e => e.UserId == id);
        }
    }
}
