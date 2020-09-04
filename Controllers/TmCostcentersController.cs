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
        public async Task<ActionResult<List<CostCenter>>> GetTmCostcenter(string CcName)
        {
            try
            {
                if (CcName == null)
                {
                    var tmCostcenter = await _context.TmCostcenter
                                        .OrderBy(i => i.CcName)
                                        .Select(i => new CostCenter { CcCode = i.CcCode, CcName = i.CcName })
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
                                .Select(i => new CostCenter { CcCode = i.CcCode, CcName = i.CcName })
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
        public async Task<ActionResult<List<CostCenter>>> GetTmCostcenterParent(string parentName)
        {
            try
            {
                if (parentName == null)
                {
                    var tmCostcenter = await _context.TmCostcenter
                                       .Where(i => i.IsActive == "A")
                                       .OrderBy(i => i.CcName)
                                       .Select(i => new CostCenter { PrtCcCode = i.PrtCcCode, CcName = i.CcName })
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
                                       .Select(i => new CostCenter { PrtCcCode = i.PrtCcCode, CcName = i.CcName })
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
        public async Task<ActionResult<List<CostCenter>>> GetTmCostcenterGrantParent(string GrantparentName)
        {
            try
            {
                if (GrantparentName == null)
                {
                    var tmCostcenter = await _context.TmCostcenter
                                       .Where(i => i.IsActive == "A")
                                       .OrderBy(i => i.CcName)
                                       .Select(i => new CostCenter { GprtCcCode = i.GprtCcCode, CcName = i.CcName })
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
                                       .Select(i => new CostCenter { GprtCcCode = i.GprtCcCode, CcName = i.CcName })
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
        public async Task<ActionResult<List<Gcmtype>>> GetTmGcmtype(string Descn)
        {
            try
            {
                if (Descn == null)
                {
                    var tmGcmtypes = await _context.TmGcmtype                                       
                                       .OrderBy(i => i.Descn)
                                       .Select(i => new Gcmtype { GcmType = i.GcmType, Descn = i.Descn })
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
                                     .Select(i => new Gcmtype { GcmType = i.GcmType, Descn = i.Descn })
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
        public async Task<ActionResult<List<CostCenter>>> ViewTmCostcenter(string CcCode)
        {
            try
            {
                var tmCostcenter = await _context.TmCostcenter
                                   .Join(_context.TmCostcenter,A => A.PrtCcCode, B => B.CcCode,(A,B) => new { TmCostcenter = A ,B})
                                   .Join(_context.TmCostcenter,A => A.TmCostcenter.GprtCcCode ,C => C.CcCode,(A,C) => new {TmCostcenter = A,C })
                                   .Join(_context.TmGcm, A => A.TmCostcenter.TmCostcenter.CcType, D => D.GcmType,(A,D) => new {TmCostcenter =A,TmGcm =D })                                  
                                   .Where(i => i.TmCostcenter.TmCostcenter.TmCostcenter.CcCode == CcCode)
                                  .Select(i => new CostCenter
                                  { 
                                    CcCode = i.TmCostcenter.TmCostcenter.TmCostcenter.CcCode,
                                    CcName = i.TmCostcenter.TmCostcenter.TmCostcenter.CcName,
                                    PrtCcCode = i.TmCostcenter.TmCostcenter.TmCostcenter.PrtCcCode,
                                    PrtccName = i.TmCostcenter.TmCostcenter.B.CcName,
                                    GprtCcCode = i.TmCostcenter.TmCostcenter.TmCostcenter.GprtCcCode,
                                    GprtCcName = i.TmCostcenter.C.CcName,
                                    CcType = i.TmCostcenter.TmCostcenter.TmCostcenter.CcType,
                                    Descn = i.TmGcm.GcmDesc,
                                    RegionCode =i.TmCostcenter.TmCostcenter.TmCostcenter.RegionCode,
                                    IsActive =i.TmCostcenter.TmCostcenter.TmCostcenter.IsActive
                                   }).ToListAsync();
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
        public async Task<ActionResult<Response>> PostTmCostcenter(CostCenter tmCostcenterview)
        {
            var tmCostcenter = await _context.TmCostcenter.FindAsync(tmCostcenterview.CcCode);
            TmCostcenter newtmCostcenter = new TmCostcenter();
            if(tmCostcenter == null)
            {
                newtmCostcenter.CcCode = tmCostcenterview.CcCode;
                newtmCostcenter.CcName = tmCostcenterview.CcName;
                newtmCostcenter.PrtCcCode = tmCostcenterview.PrtCcCode;
                newtmCostcenter.GprtCcCode = tmCostcenterview.GprtCcCode;
                newtmCostcenter.CcType = tmCostcenterview.CcType;
                newtmCostcenter.IsActive = tmCostcenterview.IsActive;
                newtmCostcenter.RegionCode = tmCostcenterview.RegionCode;
                _context.TmCostcenter.Add(newtmCostcenter);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (TmCostcenterExists(tmCostcenterview.CcCode))
                    {
                        return new Response { Status = "Conflict", Message = "Record Already Exist" };
                    }
                }
                return new Response { Status = "SUCCESSFULL", Message = "SAVED SUCCESSFULLY" };
            }
            if(tmCostcenter != null)
            {
                tmCostcenter.CcName = tmCostcenterview.CcName;
                tmCostcenter.PrtCcCode = tmCostcenterview.PrtCcCode;
                tmCostcenter.GprtCcCode = tmCostcenterview.GprtCcCode;
                tmCostcenter.CcType = tmCostcenterview.CcType;
                tmCostcenter.IsActive = tmCostcenterview.IsActive;
                tmCostcenter.RegionCode = tmCostcenterview.RegionCode;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TmCostcenterExists(tmCostcenter.CcCode))
                    {

                        return new Response { Status = "NotFound", Message = "Record Not Found" };
                    }
                    
                }

                return new Response { Status = "Updated", Message = "Record Updated Sucessfull" };
            }
            return null;
        }

        //[HttpPost("SaveUpdate")]
        //public async Task<ActionResult<Response>> PostTmCostcenter(string CcCode,TmCostcenter tmCostcenter)
        //{
        //    if (CcCode != tmCostcenter.CcCode)
        //    {
        //        _context.TmCostcenter.Add(tmCostcenter);
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException)
        //        {
        //            if (TmCostcenterExists(tmCostcenter.CcCode))
        //            {                       
        //                return new Response {Status = "Conflict",Message= "Record Already Exist" };
        //            }
        //        }
        //        return new Response { Status = "SUCCESSFULL", Message = "SAVED SUCCESSFULLY" };
                
        //    }
        //    else if (CcCode == tmCostcenter.CcCode)
        //    {
        //       /* {
        //            return BadRequest();
        //        }*/

        //        _context.Entry(tmCostcenter).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TmCostcenterExists(CcCode))
        //            {
                       
        //                return new Response { Status = "NotFound", Message = "Record Not Found" };
        //            }
        //           /* else
        //            {
        //                throw;
        //            }*/
        //        }
                                
        //        return new Response { Status = "Updated", Message = "Record Updated Sucessfull" };
        //    }
        //    return null;
        //}       

        private bool TmCostcenterExists(string id)
        {
            return _context.TmCostcenter.Any(e => e.CcCode == id);
        }
    }
}
