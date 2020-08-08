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
    public class TmCostcentersController : ControllerBase
    {
        private readonly reactppdContext _context;

        public TmCostcentersController(reactppdContext context)
        {
            _context = context;
        }

        // GET: api/TmCostcenters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TmCostcenter>>> GetTmCostcenter()
        {
            return await _context.TmCostcenter.ToListAsync();
        }

        // GET: api/TmCostcenters/5        
      
        [HttpPost("CostCenterName")]
        public async Task<ActionResult<IEnumerable<TmCostcenter>>> GetTmCostcenter(string CcName)
        {
            try
            {
                if (CcName == null)
                {
                    var tmCostcenter = await _context.TmCostcenter
                                        .OrderBy(i => i.CcName)
                                        .Select(i => new TmCostcenter { CcCode = i.CcCode, CcName = i.CcName })
                                        .ToListAsync();


                    if (tmCostcenter.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No CostCenterName with Name = {0}", CcName)),
                            ReasonPhrase = "CostCenterName  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmCostcenter;
                }
                else if (CcName != null)
                {
                    var tmCostcenter = await _context.TmCostcenter
                                .Where(i => i.CcName.StartsWith(CcName.ToUpper()))
                                .OrderBy(i => i.CcName)
                                .Select(i => new TmCostcenter { CcCode = i.CcCode, CcName = i.CcName })
                                .ToListAsync();


                    if (tmCostcenter.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No CostCenterName with Name = {0}", CcName)),
                            ReasonPhrase = "CostCenterName  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmCostcenter;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("ParentName")]
        public async Task<ActionResult<IEnumerable<TmCostcenter>>> GetTmCostcenterParent(string parentName)
        {
            try
            {
                if (parentName == null)
                {
                    var tmCostcenter = await _context.TmCostcenter
                                       .Where(i => i.IsActive == "A")
                                       .OrderBy(i => i.CcName)
                                       .Select(i => new TmCostcenter { PrtCcCode = i.PrtCcCode, CcName = i.CcName })
                                       .ToListAsync();


                    if (tmCostcenter.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No CostCenterParent with Name = {0}", parentName)),
                            ReasonPhrase = "CostCenterParentName  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmCostcenter;
                }
                else if (parentName != null)
                {
                    var tmCostcenter = await _context.TmCostcenter
                                       .Where(i => i.CcCode == i.PrtCcCode)
                                       .Where(i => i.CcName.StartsWith(parentName.ToUpper()))
                                       .Where(i => i.IsActive == "A")
                                       .OrderBy(i => i.CcName)
                                       .Select(i => new TmCostcenter { PrtCcCode = i.PrtCcCode, CcName = i.CcName })
                                       .ToListAsync();


                    if (tmCostcenter.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No CostCenterParentName with Name = {0}", parentName)),
                            ReasonPhrase = "CostCenterParentName  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmCostcenter;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("GrantParentName")]
        public async Task<ActionResult<IEnumerable<TmCostcenter>>> GetTmCostcenterGrantParent(string GrantparentName)
        {
            try
            {
                if (GrantparentName == null)
                {
                    var tmCostcenter = await _context.TmCostcenter
                                       .Where(i => i.IsActive == "A")
                                       .OrderBy(i => i.CcName)
                                       .Select(i => new TmCostcenter { GprtCcCode = i.GprtCcCode, CcName = i.CcName })
                                       .ToListAsync();


                    if (tmCostcenter.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No CostCenterGrantParent with Name = {0}", GrantparentName)),
                            ReasonPhrase = "CostCenterGrantParentName  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmCostcenter;
                }
                else if (GrantparentName != null)
                {
                    var tmCostcenter = await _context.TmCostcenter
                                       .Where(i => i.CcCode == i.GprtCcCode)
                                       .Where(i => i.CcName.StartsWith(GrantparentName.ToUpper()))
                                       .Where(i => i.IsActive == "A")
                                       .OrderBy(i => i.CcName)
                                       .Select(i => new TmCostcenter { GprtCcCode = i.GprtCcCode, CcName = i.CcName })
                                       .ToListAsync();


                    if (tmCostcenter.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No CostCenterGrantParentName with Name = {0}", GrantparentName)),
                            ReasonPhrase = "CostCenterGrantParentName  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmCostcenter;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("CostCenterType")]
        public async Task<ActionResult<IEnumerable<TmGcmtype>>> GetTmGcmtype(string Descn)
        {
            try
            {
                if (Descn == null)
                {
                    var tmGcmtypes = await _context.TmGcmtype                                       
                                       .OrderBy(i => i.Descn)
                                       .Select(i => new TmGcmtype { GcmType = i.GcmType, Descn = i.Descn })
                                       .ToListAsync();


                    if (tmGcmtypes.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No CostCenterType with Name = {0}", Descn)),
                            ReasonPhrase = "CostCenterType Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmGcmtypes;
                }
                else if (Descn != null)
                {
                    var tmGcmtypes = await _context.TmGcmtype
                                     .Where(i => i.Descn.StartsWith(Descn))                                      
                                     .OrderBy(i => i.Descn)
                                     .Select(i => new TmGcmtype { GcmType = i.GcmType, Descn = i.Descn })
                                     .ToListAsync();


                    if (tmGcmtypes.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No CostCenterType with Name = {0}", Descn)),
                            ReasonPhrase = "CostCenterType Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmGcmtypes;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("ViewCostCenter")]
        public async Task<ActionResult<IEnumerable<TmCostcenter>>> ViewTmCostcenter(string CcCode)
        {
            try
            {
                var tmCostcenter = await _context.TmCostcenter
                                  .Where(i => i.CcCode == CcCode)
                                  .Select(i => i).ToListAsync();
                if (tmCostcenter.Count == 0)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("No CostCenterCode with ID = {0}", CcCode)),
                        ReasonPhrase = "CostCenterCode  Not Found"
                    };
                    throw new System.Web.Http.HttpResponseException(resp);
                }
                return tmCostcenter;

            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("SaveUpdate")]
        public async Task<ActionResult<Response>> PostTmCostcenter(string CcCode,TmCostcenter tmCostcenter)
        {
            if (CcCode != tmCostcenter.CcCode)
            {
                _context.TmCostcenter.Add(tmCostcenter);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (TmCostcenterExists(tmCostcenter.CcCode))
                    {                       
                        return new Response {Status = "Conflict",Message= "Record Already Exist" };
                    }
                }
                return new Response { Status = "SUCCESSFULL", Message = "SAVED SUCCESSFULLY" };
                
            }
            else if (CcCode == tmCostcenter.CcCode)
            {
               /* {
                    return BadRequest();
                }*/

                _context.Entry(tmCostcenter).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TmCostcenterExists(CcCode))
                    {
                       
                        return new Response { Status = "NotFound", Message = "Record Not Found" };
                    }
                   /* else
                    {
                        throw;
                    }*/
                }
                                
                return new Response { Status = "Updated", Message = "Record Updated Sucessfull" };
            }
            return null;
        }
        // PUT: api/TmCostcenters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTmCostcenter(string id, TmCostcenter tmCostcenter)
        {
            if (id != tmCostcenter.CcCode)
            {
                return BadRequest();
            }

            _context.Entry(tmCostcenter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TmCostcenterExists(id))
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

        // POST: api/TmCostcenters
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
       /* [HttpPost]
        public async Task<ActionResult<TmCostcenter>> PostTmCostcenter(TmCostcenter tmCostcenter)
        {
            _context.TmCostcenter.Add(tmCostcenter);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TmCostcenterExists(tmCostcenter.CcCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTmCostcenter", new { id = tmCostcenter.CcCode }, tmCostcenter);
        }*/

        // DELETE: api/TmCostcenters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TmCostcenter>> DeleteTmCostcenter(string id)
        {
            var tmCostcenter = await _context.TmCostcenter.FindAsync(id);
            if (tmCostcenter == null)
            {
                return NotFound();
            }

            _context.TmCostcenter.Remove(tmCostcenter);
            await _context.SaveChangesAsync();

            return tmCostcenter;
        }

        private bool TmCostcenterExists(string id)
        {
            return _context.TmCostcenter.Any(e => e.CcCode == id);
        }
    }
}
