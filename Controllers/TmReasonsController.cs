using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactPPD.Model;
using ReactPPD.VM;

namespace ReactPPD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TmReasonsController : ControllerBase
    {
        private readonly reactppdContext _context;

        public TmReasonsController(reactppdContext context)
        {
            _context = context;
        }

        // GET: api/TmReasons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TmReason>>> GetTmReason()
        {
            return await _context.TmReason.ToListAsync();
        }

        // GET: api/TmReasons/5
        // GET: api/TmGrpschedules/5
        [HttpPost("DocType")]
        public async Task<ActionResult<List<Doctypes>>> GetTmDoctypes(string docname)
        {
            try
            {
                if (docname == null)
                {
                    var tmDoctypes = await _context.TmDoctypes
                                       .OrderBy(i => i.DocName)
                                       .Select(i => new Doctypes { DocType = i.DocType, DocName = i.DocName })
                                       .ToListAsync();


                    if (tmDoctypes.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No DocTYpe with Name = {0}", docname)),
                            ReasonPhrase = "DocType Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmDoctypes;
                }
                else if (docname != null)
                {
                    var tmDoctypes = await _context.TmDoctypes
                                        .Where(i => i.DocName.StartsWith(docname))
                                        .OrderBy(i => i.DocName)
                                        .Select(i => new Doctypes { DocType = i.DocType, DocName = i.DocName })
                                        .ToListAsync();



                    if (tmDoctypes.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No DocTYpe with Name = {0}", docname)),
                            ReasonPhrase = "DocType Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmDoctypes;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Branch")]
        public async Task<ActionResult<List<Branch>>> GetTmBranch(string branchname)
        {
            try
            {
                if (branchname == null)
                {
                    var tmBranch = await _context.TmBranch
                                    .Where(i => i.IsActive.Contains("A"))
                                    .OrderBy(i => i.BranchName)
                                    .Select(i => new Branch {BranchCode = i.BranchCode, BranchName = i.BranchName })
                                    .ToListAsync();


                    if (tmBranch.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Branch with Name = {0}", branchname)),
                            ReasonPhrase = "Branch Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmBranch;
                }
                else if (branchname != null)
                {
                    var tmBranch = await _context.TmBranch
                                    .Where(i => i.IsActive.Contains("A"))
                                    .Where(i => i.BranchName.Contains(branchname))
                                    .OrderBy(i => i.BranchName)
                                    .Select(i => new Branch { BranchCode = i.BranchCode, BranchName = i.BranchName })
                                    .ToListAsync();


                    if (tmBranch.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Branch with Name = {0}", branchname)),
                            ReasonPhrase = "Branch Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmBranch;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Reason")]
        public async Task<ActionResult<List<Reason>>> GetTmReason(string branchcode,string doctype)
        {
            try
            {
                if (branchcode == null && doctype == null)
                {
                    var tmReason = await _context.TmReason                                    
                                  .OrderBy(i => i.ReasonName)
                                  .Select(i => new Reason {ReasonCode = i.ReasonCode, ReasonName = i.ReasonName })
                                  .ToListAsync();


                    if (tmReason.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Reason Found")),
                            ReasonPhrase = "Reason Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmReason;
                }
                else if (branchcode != null && doctype != null)
                {
                    var tmReason = await _context.TmReason
                                  .Where(i => i.BranchCode.Contains(branchcode) && i.DocType.Contains(doctype))
                                  .OrderBy(i => i.ReasonName)
                                  .Select(i => new Reason { ReasonCode = i.ReasonCode, ReasonName = i.ReasonName })
                                  .ToListAsync();


                    if (tmReason.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Reason Found")),
                            ReasonPhrase = "Reason Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmReason;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("AcGroup")]
        public async Task<ActionResult<List<AcGroup>>> GetTmAcGroup(string grpname)
        {
            try
            {
                if (grpname == null )
                {
                    var tmAcGroup = await _context.TmAcgroup
                                  .OrderBy(i => i.GrpName)
                                  .Select(i => new AcGroup { GrpCode = i.GrpCode, GrpName = i.GrpName })
                                  .ToListAsync();


                    if (tmAcGroup.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No AcGroup with Name = {0}", grpname)),
                            ReasonPhrase = "AcGroup Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmAcGroup;
                }
                else if (grpname != null)
                {

                    var tmAcGroup = await _context.TmAcgroup
                                   .Where(i => i.GrpName.Contains(grpname))
                                  .OrderBy(i => i.GrpName)
                                  .Select(i => new AcGroup { GrpCode = i.GrpCode, GrpName = i.GrpName })
                                  .ToListAsync();


                    if (tmAcGroup.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No AcGroup with Name = {0}", grpname)),
                            ReasonPhrase = "AcGroup Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmAcGroup;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Expense")]
        public async Task<ActionResult<List<Account>>> GetExpense(string accountname,string branchcode)
        {
            try
            {
                if (accountname == null && branchcode == null)
                {
                    var tmAccount = await _context.TmAccounts
                                     .Join(_context.TmAcbrmap, Acc => Acc.AccountCode, Acb =>Acb.AccountCode, (Acc, Acb) => new { TmAccounts = Acc, TmAcbrmap = Acb})
                                     .Where(m => m.TmAccounts.AccountCode == m.TmAcbrmap.AccountCode)                                     
                                     .OrderBy(m => m.TmAccounts.AccountName)
                                     .Select(m => new Account
                                     {
                                         AccountCode= m.TmAccounts.AccountCode,
                                         AccountName = m.TmAccounts.AccountName
                                     }).ToListAsync();



                    if (tmAccount.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Account with Name = {0}", accountname)),
                            ReasonPhrase = "Account Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmAccount;
                }
                else if (accountname != null && branchcode != null)
                {
                    var tmAccount = await _context.TmAccounts
                                     .Join(_context.TmAcbrmap, Acc => Acc.AccountCode, Acb => Acb.AccountCode, (Acc, Acb) => new { TmAccounts = Acc, TmAcbrmap = Acb })
                                     .Where(m => m.TmAccounts.AccountCode == m.TmAcbrmap.AccountCode)
                                     .Where(m => m.TmAccounts.AccountName.Contains(accountname) && m.TmAcbrmap.BranchCode.Contains(branchcode))
                                     .OrderBy(m => m.TmAccounts.AccountName)
                                     .Select(m => new Account
                                     {
                                         AccountCode = m.TmAccounts.AccountCode,
                                         AccountName = m.TmAccounts.AccountName
                                     }).ToListAsync();



                    if (tmAccount.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Account with Name = {0}", accountname)),
                            ReasonPhrase = "Account Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmAccount;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("SaveUpdate")]
        public async Task<ActionResult<Response>> PostTmReason(string reasoncode, TmReason tmReason)
        {
            if (reasoncode != tmReason.ReasonCode)
            {
                _context.TmReason.Add(tmReason);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (TmReasonExists(tmReason.ReasonCode))
                    {
                        //return Conflict();
                        return new Response { Status = "Conflict", Message = "Record Already Exist" };
                    }
                }
                return new Response { Status = "SUCCESSFULL", Message = "SAVED SUCCESSFULLY" };
                // return CreatedAtAction("GetTmCostcenter", new { id = tmCostcenter.CcCode }, tmCostcenter);
            }
            else if (reasoncode == tmReason.ReasonCode)
            {
                /* {
                     return BadRequest();
                 }*/

                _context.Entry(tmReason).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TmReasonExists(reasoncode))
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

        // PUT: api/TmReasons/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTmReason(string id, TmReason tmReason)
        {
            if (id != tmReason.BranchCode)
            {
                return BadRequest();
            }

            _context.Entry(tmReason).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TmReasonExists(id))
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

        // POST: api/TmReasons
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
      
        // DELETE: api/TmReasons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TmReason>> DeleteTmReason(string id)
        {
            var tmReason = await _context.TmReason.FindAsync(id);
            if (tmReason == null)
            {
                return NotFound();
            }

            _context.TmReason.Remove(tmReason);
            await _context.SaveChangesAsync();

            return tmReason;
        }

        private bool TmReasonExists(string id)
        {
            return _context.TmReason.Any(e => e.BranchCode == id);
        }
    }
}
