using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactPPD.Model;
using ReactPPD.VM;
namespace ReactPPD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TmUsersController : ControllerBase
    {
        private readonly reactppdContext _context;

        public TmUsersController(reactppdContext context)
        {
            _context = context;
        }

        // GET: api/TmUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TmUser>>> GetTmUser()
        {
            return await _context.TmUser.ToListAsync();
        }

        // GET: api/TmUsers/5

        [EnableCors("*", "*", "*")]
        [HttpPost("Login")]
        public async Task<ActionResult<Response>> GetTmUser(TmUser user)
        {
            var tmUser = await _context.TmUser.FindAsync(user.UserId);

            if (tmUser != null)
            {
                // var passw = _context.TmUser.Where(i => i.PassWord != password).ToString();
                if (user.PassWord != tmUser.PassWord)
                {
                    // return BadRequest(new { Message = "InValid Password" });
                    return new Response { Status = "Invalid", Message = "Invalid Password" };
                }


            }
            if (tmUser == null)
            {
                // return NotFound();
                //return BadRequest(new { Message = "InValid UserId" });
               return new Response { Status = "Invalid", Message = "Invalid UserId" };
            }

            //return tmUser;
            return new Response { Status = "Success", Message = "Login Successfully of user id " + " " + user.UserId.ToString() };
           
        }

        // PUT: api/TmUsers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTmUser(string id, TmUser tmUser)
        {
            if (id != tmUser.UserId)
            {
                return BadRequest();
            }

            _context.Entry(tmUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TmUserExists(id))
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

        // POST: api/TmUsers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TmUser>> PostTmUser(TmUser tmUser)
        {
            _context.TmUser.Add(tmUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TmUserExists(tmUser.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTmUser", new { id = tmUser.UserId }, tmUser);
        }

        // DELETE: api/TmUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TmUser>> DeleteTmUser(string id)
        {
            var tmUser = await _context.TmUser.FindAsync(id);
            if (tmUser == null)
            {
                return NotFound();
            }

            _context.TmUser.Remove(tmUser);
            await _context.SaveChangesAsync();

            return tmUser;
        }

        private bool TmUserExists(string id)
        {
            return _context.TmUser.Any(e => e.UserId == id);
        }
    }
}
