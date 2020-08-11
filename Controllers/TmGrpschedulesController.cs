using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactPPD.Model;
using ReactPPD.VM;
namespace ReactPPD.Controllers
{
    [EnableCors("*", "*", "*")]
    [Route("api/[controller]")]
    [ApiController]
    public class TmGrpschedulesController : ControllerBase
    {
        private readonly reactppdContext _context;

        public TmGrpschedulesController(reactppdContext context)
        {
            _context = context;
        }

        // GET: api/TmGrpschedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TmGrpschedule>>> GetTmGrpschedule()
        {
            return await _context.TmGrpschedule.ToListAsync();
        }

        // GET: api/TmGrpschedules/5
        [HttpPost("GrpSchedule")]
        public async Task<ActionResult<IEnumerable<TmGrpschedule>>> GetTmGrpschedule(string schname)
        {
            try
            {
                if (schname == null)
                {
                    var tmGrpschedule = await _context.TmGrpschedule
                                       .OrderBy(i => i.SchName)
                                       .Select(i => new TmGrpschedule { SchNo = i.SchNo, SchName = i.SchName })
                                       .ToListAsync();


                    if (tmGrpschedule.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Group Schedule with Name = {0}", schname)),
                            ReasonPhrase = "Group Schedule Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmGrpschedule;
                }
                else if (schname != null)
                {
                    var tmGrpschedule = await _context.TmGrpschedule
                                        .Where(i => i.SchName.StartsWith(schname))
                                        .OrderBy(i => i.SchName)
                                        .Select(i => new TmGrpschedule { SchNo = i.SchNo, SchName = i.SchName })
                                        .ToListAsync();



                    if (tmGrpschedule.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Group Schedule with Name = {0}", schname)),
                            ReasonPhrase = "Group Schedule Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmGrpschedule;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("ParentSchedule")]
        public async Task<ActionResult<IEnumerable<TmGrpschedule>>> GetTmGrpParentschedule(string schname)
        {
            try
            {
                if (schname == null)
                {
                    var tmGrpschedule = await _context.TmGrpschedule
                                       .Where(i =>i.IsActive == "A" )
                                       .OrderBy(i => i.SchName)
                                       .Select(i => new TmGrpschedule { SchNo = i.SchNo, SchName = i.SchName })
                                       .ToListAsync();


                    if (tmGrpschedule.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Parent Group Schedule with Name = {0}", schname)),
                            ReasonPhrase = "Parent Group Schedule Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmGrpschedule;
                }
                else if (schname != null)
                {
                    var tmGrpschedule = await _context.TmGrpschedule
                                        .Where(i => i.IsActive == "A")
                                        .Where(i => i.SchName.StartsWith(schname))
                                        .OrderBy(i => i.SchName)
                                        .Select(i => new TmGrpschedule { SchNo = i.SchNo, SchName = i.SchName })
                                        .ToListAsync();



                    if (tmGrpschedule.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Parent Group Schedule with Name = {0}", schname)),
                            ReasonPhrase = "Parent Group Schedule Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmGrpschedule;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }
        [HttpPost("ViewGrpSchedule")]
        public async Task<ActionResult<IEnumerable<TmGrpschedule>>> ViewTmGrpschedule(int schno)
        {
            try
            {
                var tmGrpschedule = await _context.TmGrpschedule
                            .Where(i => i.SchNo == schno)
                            .Select(i => i).ToListAsync();
                if (tmGrpschedule.Count == 0)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("No Group Schedule with ID = {0}", schno)),
                        ReasonPhrase = "Group Schedule Code  Not Found"
                    };
                    throw new System.Web.Http.HttpResponseException(resp);
                }
                return tmGrpschedule;

            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("SaveUpdate")]
        public async Task<ActionResult<Response>> PostTmGrpschedule(int schno,TmGrpschedule tmGrpschedule)
        {
            if (schno != tmGrpschedule.SchNo)
            {
                _context.TmGrpschedule.Add(tmGrpschedule);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (TmGrpscheduleExists(tmGrpschedule.SchNo))
                    {
                        //return Conflict();
                        return new Response { Status = "Conflict", Message = "Record Already Exist" };
                    }
                }
                return new Response { Status = "SUCCESSFULL", Message = "SAVED SUCCESSFULLY" };
                // return CreatedAtAction("GetTmCostcenter", new { id = tmCostcenter.CcCode }, tmCostcenter);
            }
            else if (schno == tmGrpschedule.SchNo)
            {
                /* {
                     return BadRequest();
                 }*/

                _context.Entry(tmGrpschedule).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TmGrpscheduleExists(schno))
                    {
                        //return NotFound();
                        return new Response { Status = "NotFound", Message = "Record Not Found" };
                    }
                    /* else
                     {
                         throw;
                     }*/
                }

                // return NoContent();
                return new Response { Status = "Updated", Message = "Record Updated Sucessfull" };
            }
            return null;
        }
        // PUT: api/TmGrpschedules/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTmGrpschedule(int id, TmGrpschedule tmGrpschedule)
        {
            if (id != tmGrpschedule.SchNo)
            {
                return BadRequest();
            }

            _context.Entry(tmGrpschedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TmGrpscheduleExists(id))
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

        // POST: api/TmGrpschedules
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TmGrpschedule>> PostTmGrpschedule(TmGrpschedule tmGrpschedule)
        {
            _context.TmGrpschedule.Add(tmGrpschedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTmGrpschedule", new { id = tmGrpschedule.SchNo }, tmGrpschedule);
        }

        // DELETE: api/TmGrpschedules/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TmGrpschedule>> DeleteTmGrpschedule(int id)
        {
            var tmGrpschedule = await _context.TmGrpschedule.FindAsync(id);
            if (tmGrpschedule == null)
            {
                return NotFound();
            }

            _context.TmGrpschedule.Remove(tmGrpschedule);
            await _context.SaveChangesAsync();

            return tmGrpschedule;
        }

        private bool TmGrpscheduleExists(int id)
        {
            return _context.TmGrpschedule.Any(e => e.SchNo == id);
        }
    }
}
