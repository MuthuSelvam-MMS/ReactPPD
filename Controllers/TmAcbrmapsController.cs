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
    public class TmAcbrmapsController : ControllerBase
    {
        private readonly reactppdContext _context;

        public TmAcbrmapsController(reactppdContext context)
        {
            _context = context;
        }

        // GET: api/TmAcbrmaps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TmAcbrmap>>> GetTmAcbrmap()
        {
            return await _context.TmAcbrmap.ToListAsync();
        }

        // GET: api/TmAcbrmaps/5
        [HttpPost("Account")]
        public async Task<ActionResult<IEnumerable<TmAccounts>>> GetTmAccounts( string accountname)
        {
            try
            {
                if (accountname == null)
                {
                    var tmAccounts = await _context.TmAccounts
                                     .Where(i => i.PartyType !="E" || i.PartyType != "F")
                                     .Where(i => i.IsActive.Contains("A"))
                                     .OrderBy(i => i.AccountName)
                                     .Select(i => new TmAccounts { AccountCode = i.AccountCode, AccountName = i.AccountName,AcType = i.AcType })
                                     .ToListAsync();


                    if (tmAccounts.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Account with Name = {0}", accountname)),
                            ReasonPhrase = "Account  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmAccounts;
                }
                else if (accountname != null)
                {
                    var tmAccounts = await _context.TmAccounts
                                    .Where(i => i.PartyType != "E" || i.PartyType != "F")
                                    .Where(i => i.IsActive.Contains("A"))
                                    .Where(i => i.AccountName.Contains(accountname))
                                    .OrderBy(i => i.AccountName)
                                    .Select(i => new TmAccounts { AccountCode = i.AccountCode, AccountName = i.AccountName, AcType = i.AcType })
                                    .ToListAsync();


                    if (tmAccounts.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Account with Name = {0}", accountname)),
                            ReasonPhrase = "Account  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmAccounts;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }
        [HttpPost("Company")]
        public async Task<ActionResult<IEnumerable<TmCompany>>> GetTmCompany(string companyname)
        {
            try
            {
                if (companyname == null)
                {
                    var tmCompany = await _context.TmCompany                                   
                                   .OrderBy(i => i.CompanyName)
                                   .Select(i => new TmCompany { CompanyCode = i.CompanyCode, CompanyName = i.CompanyName })
                                   .ToListAsync();


                    if (tmCompany.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Company with Name = {0}", companyname)),
                            ReasonPhrase = "Company Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmCompany;
                }
                else if (companyname != null)
                {
                    var tmCompany = await _context.TmCompany
                                   .Where(i => i.CompanyName.Contains(companyname))
                                   .OrderBy(i => i.CompanyName)
                                   .Select(i => new TmCompany { CompanyCode = i.CompanyCode, CompanyName = i.CompanyName })
                                   .ToListAsync();


                    if (tmCompany.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Company with Name = {0}", companyname)),
                            ReasonPhrase = "Company Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmCompany;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }
        [HttpPost("Division")]
        public async Task<ActionResult<IEnumerable<TmDivision>>> GetTmDivision(string companycode)
        {
            try
            {
                if (companycode == null)
                {
                    var tmDivision = await _context.TmDivision
                                     .Join(_context.TmBranch, d => d.DivCode,b => b.DivCode, (d, b) => new { TmDivision = d, TmBranch = b })                             
                                     .Where(m => m.TmDivision.DivCode == m.TmBranch.DivCode)
                                     .Where(m => m.TmDivision.IsActive == m.TmDivision.IsActive)
                                     .Where(m => m.TmDivision.IsActive.Contains("A"))
                                     .OrderBy(m => m.TmDivision.DivName)
                                     .Select(m => new TmDivision
                                     {
                                       DivCode = m.TmDivision.DivCode,
                                       DivName = m.TmDivision.DivName
                                     }).ToListAsync();
                                    
                                    

                    if (tmDivision.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Division with Name = {0}", companycode)),
                            ReasonPhrase = "Division Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmDivision;
                }
                else if (companycode != null)
                {
                    var tmDivision = await _context.TmDivision
                                     .Join(_context.TmBranch, d => d.DivCode, b => b.DivCode, (d, b) => new { TmDivision = d, TmBranch = b })
                                     .Where(m => m.TmDivision.DivCode == m.TmBranch.DivCode)
                                     .Where(m => m.TmDivision.IsActive == m.TmDivision.IsActive)
                                     .Where(m => m.TmDivision.IsActive.Contains("A"))
                                     .Where(m => m.TmBranch.CompanyCode.Contains(companycode))
                                     .OrderBy(m => m.TmDivision.DivName)
                                     .Select(m => new TmDivision
                                     {
                                         DivCode = m.TmDivision.DivCode,
                                         DivName = m.TmDivision.DivName
                                     }).ToListAsync();



                    if (tmDivision.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Division with Name = {0}", companycode)),
                            ReasonPhrase = "Division Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmDivision;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Region")]
        public async Task<ActionResult<IEnumerable<TmRegion>>> GetTmRegionMap(string companycode,string divcode,string regzonecode)
        {
            try
            {
                if (companycode == null && divcode == null && regzonecode == null)
                {
                    var tmRegion = await _context.TmRegionmap
                                     .Join(_context.TmRegion, rm =>rm.RegionCode, r => r.RegionCode, (rm, r) => new { TmRegionmap = rm, TmRegion = r })
                                     .Join(_context.TmBranch, br =>br.TmRegion.RegionCode, b =>b.RegionCode,(br,b) => new {TmRegion =br,TmBranch =b })
                                     .Where(m => m.TmBranch.DivCode == m.TmRegion.TmRegionmap.DivCode)
                                     .Where(m => m.TmRegion.TmRegionmap.IsActive.Contains("A"))
                                     .Where(m => m.TmRegion.TmRegion.RegionCode != "NONE")
                                     .OrderBy(m => m.TmRegion.TmRegion.RegionName)
                                     .Select(m => new TmRegion
                                     {
                                         RegionCode = m.TmRegion.TmRegion.RegionCode,
                                         RegionName = m.TmRegion.TmRegion.RegionName
                                     }).ToListAsync();



                    if (tmRegion.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Region ")),
                            ReasonPhrase = "Region Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmRegion;
                }
                else if (companycode != null && divcode != null && regzonecode != null)
                {
                    var tmRegion = await _context.TmRegionmap
                                   .Join(_context.TmRegion, rm => rm.RegionCode,r => r.RegionCode, (rm, r) => new { TmRegionmap = rm, TmRegion = r })
                                   .Join(_context.TmBranch, br => br.TmRegion.RegionCode, b => b.RegionCode, (br, b) => new { TmRegion = br, TmBranch = b })
                                   .Where(m => m.TmBranch.DivCode == m.TmRegion.TmRegionmap.DivCode)
                                   .Where(m => m.TmRegion.TmRegionmap.IsActive.Contains("A"))
                                   .Where(m => m.TmRegion.TmRegion.RegionCode != "NONE")
                                   .Where(m => m.TmRegion.TmRegionmap.CompanyCode.Contains(companycode))
                                   .Where( m => m.TmRegion.TmRegionmap.DivCode.Contains(divcode))
                                   .Where(m => m.TmRegion.TmRegionmap.RegZoneCode.Contains(regzonecode))
                                   .OrderBy(m => m.TmRegion.TmRegion.RegionName)
                                   .Select(m => new TmRegion
                                     {
                                         RegionCode = m.TmRegion.TmRegion.RegionCode,
                                         RegionName = m.TmRegion.TmRegion.RegionName
                                     }).ToListAsync();



                    if (tmRegion.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Region ")),
                            ReasonPhrase = "Region Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmRegion;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Territory")]
        public async Task<ActionResult<IEnumerable<TmZone>>> GetTmZonep(string companycode, string divcode, string regzonecode,string regioncode)
        {
            try
            {
                if (companycode == null && divcode == null && regzonecode == null && regioncode == null)
                {
                    var tmZone = await _context.TmZone
                                     .Join(_context.TmBranch, z => z.RegionCode, b => b.RegionCode, (z,b) => new { TmZone = z, TmBranch = b })                                     
                                     .Where(m => m.TmZone.ZoneCode == m.TmBranch.ZoneCode)
                                     .Where(m => m.TmZone.RegionCode == m.TmZone.RegionCode)
                                     .Where(m => m.TmZone.RegZoneCode == m.TmZone.RegZoneCode)
                                     .Where(m => m.TmZone.ZoneCode != "NONE")
                                     .OrderBy(m => m.TmZone.ZoneName)
                                     .Select(m => new TmZone
                                     {
                                         ZoneCode = m.TmZone.ZoneCode,
                                         ZoneName = m.TmZone.ZoneName
                                     }).ToListAsync();



                    if (tmZone.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Zone ")),
                            ReasonPhrase = "Zone Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmZone;
                }
                else if (companycode != null && divcode != null && regzonecode != null && regzonecode != null)
                {
                    var tmZone = await _context.TmZone
                                      .Join(_context.TmBranch, z => z.RegionCode, b => b.RegionCode, (z, b) => new { TmZone = z, TmBranch = b })
                                      .Where(m => m.TmZone.ZoneCode == m.TmBranch.ZoneCode)
                                      .Where(m => m.TmZone.RegionCode == m.TmZone.RegionCode)
                                      .Where(m => m.TmZone.RegZoneCode == m.TmZone.RegZoneCode)
                                      .Where(m => m.TmZone.ZoneCode != "NONE")
                                      .Where(m => m.TmBranch.CompanyCode.Contains(companycode))
                                      .Where(m => m.TmBranch.DivCode.Contains(divcode))
                                      .Where(m => m.TmZone.RegionCode.Contains(regioncode))
                                      .Where(m => m.TmZone.RegZoneCode.Contains(regzonecode))
                                      .OrderBy(m => m.TmZone.ZoneName)
                                      .Select(m => new TmZone
                                      {
                                          ZoneCode = m.TmZone.ZoneCode,
                                          ZoneName = m.TmZone.ZoneName
                                      }).ToListAsync();



                    if (tmZone.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Zone ")),
                            ReasonPhrase = "Zone Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmZone;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Branch")]
        public async Task<ActionResult<IEnumerable<TmBranch>>> GetTmBranch(string companycode, string divcode, string regzonecode, string regioncode,string zonecode)
        {
            try
            {
                if (companycode == null && divcode == null && regzonecode == null && zonecode == null)
                {
                    var tmBranch = await _context.TmBranch                                    
                                   .OrderBy(i => i.BranchName)
                                   .Select(i => new TmBranch {BranchCode = i.BranchCode,BranchName= i.BranchName })
                                    .ToListAsync();


                    if (tmBranch.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Branch ")),
                            ReasonPhrase = "Branch Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmBranch;
                }
                else if (companycode != null && divcode != null && regzonecode != null && zonecode != null)
                {
                    var tmBranch = await _context.TmBranch
                                   .Where(i => i.CompanyCode.Contains(companycode))
                                   .Where(i => i.DivCode.Contains(divcode))
                                   .Where(i => i.RegZoneCode.Contains(regzonecode))
                                   .Where(i => i.RegionCode.Contains(regioncode))
                                   .Where(i => i.ZoneCode.Contains(zonecode))
                                   .OrderBy(i => i.BranchName)
                                   .Select(i => new TmBranch { BranchCode = i.BranchCode, BranchName = i.BranchName })
                                   .ToListAsync();


                    if (tmBranch.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Branch ")),
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

     [HttpPost("RegZone")]
        public async Task<ActionResult<IEnumerable<TmRegzone>>> GetTmRegZone(string companycode, string divcode)
        {
            try
            {
                if (companycode == null && divcode == null)
                {
                    var tmRegion = await _context.TmRegzonemap
                                     .Join(_context.TmRegzone, rm => rm.RegZoneCode, rz => rz.RegZoneCode, (rm, rz) => new { TmRegzonemap = rm, TmRegzone = rz })
                                     .Join(_context.TmDivision,rmz =>rmz.TmRegzonemap.DivCode,d =>divcode,(rmz,d)=> new { TmRegzonemap = rmz,TmDivision = d})
                                     .Join(_context.TmBranch, db => db.TmDivision.DivCode, b => divcode, (d,db) => new { TmBranch = d,TmDivision = db})
                                     .Where(m => m.TmBranch.TmRegzonemap.TmRegzone.RegZoneCode == m.TmDivision.RegZoneCode)
                                     .Where(m => m.TmBranch.TmRegzonemap.TmRegzone.CmpId == m.TmDivision.CompanyCode)
                                     .Where(m =>m.TmBranch.TmRegzonemap.TmRegzone.RegZoneCode != "NONE")
                                     .OrderBy(m => m.TmBranch.TmRegzonemap.TmRegzone.RegZoneCode)
                                     .Select(m => new TmRegzone
                                     {
                                         RegZoneCode = m.TmBranch.TmRegzonemap.TmRegzone.RegZoneCode,
                                         RegZoneName= m.TmBranch.TmRegzonemap.TmRegzone.RegZoneName
                                     }).ToListAsync();



                    if (tmRegion.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No RegionZone ")),
                            ReasonPhrase = "RegionZone Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmRegion;
                }
                else if (companycode != null && divcode != null)
                {
                    var tmRegion = await _context.TmRegzonemap
                                    .Join(_context.TmRegzone, rm => rm.RegZoneCode, rz => rz.RegZoneCode, (rm, rz) => new { TmRegzonemap = rm, TmRegzone = rz })
                                    .Join(_context.TmDivision, rmz => rmz.TmRegzonemap.DivCode, d => divcode, (rmz, d) => new { TmRegzonemap = rmz, TmDivision = d })
                                    .Join(_context.TmBranch, db => db.TmDivision.DivCode, b => divcode, (d, db) => new { TmBranch = d, TmDivision = db })                                    
                                    .Where(m => m.TmBranch.TmRegzonemap.TmRegzone.RegZoneCode == m.TmDivision.RegZoneCode)
                                    .Where(m => m.TmBranch.TmRegzonemap.TmRegzone.CmpId == m.TmDivision.CompanyCode)
                                    .Where(m => m.TmBranch.TmRegzonemap.TmRegzone.RegZoneCode != "NONE")
                                    .Where(m => m.TmDivision.CompanyCode.Contains(companycode))
                                    .Where(m =>m.TmDivision.DivCode.Contains(divcode))
                                    .OrderBy(m => m.TmBranch.TmRegzonemap.TmRegzone.RegZoneCode)
                                    .Select(m => new TmRegzone
                                    {
                                        RegZoneCode = m.TmBranch.TmRegzonemap.TmRegzone.RegZoneCode,
                                        RegZoneName = m.TmBranch.TmRegzonemap.TmRegzone.RegZoneName
                                    }).ToListAsync();



                    if (tmRegion.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No RegionZone ")),
                            ReasonPhrase = "RegionZone Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmRegion;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

       /* [HttpPost("ViewData")]
        public async Task<ActionResult<List<AccountBrMap>>> ViewTmVendor(string branchcode, string divcode,string zonecode)
        {
            try
            {
                var tmVendor = await _context.TmAcbrmap
                              .Join(_context.TmBranch, acm => acm.BranchCode,b => b.BranchCode, (acm,b) => new { TmAcbrmap = acm, TmBranch = b })
                              .Where(m =>m.TmAcbrmap.BranchCode.Contains(branchcode))
                              .Where(m =>m.TmBranch.DivCode.Contains(divcode))
                              .Where(m =>m.TmBranch.ZoneCode.Contains(zonecode))
                              .Where(m => m.TmBranch.IsActive.Contains("A")
                              .Select(m => new
                              {
                                 
                              }).ToListAsync();
                List<VendorItem> vendoritems = new List<VendorItem>();
                foreach (var items in tmVendor)
                {
                    vendoritems.Add(new VendorItem
                    {
                        AccountCode = items.AccountCode.ToString(),
                        AccountName = items.AccountName.ToString(),
                        ItemCode = items.ItemCode.ToString(),
                        ItemName = items.ItemName.ToString()
                    });
                }

                if (tmVendor.Count == 0)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("No Vendor with Code{0} And Items with Code{1} ", accountcode, itemcode)),
                        ReasonPhrase = "Vendor And Iten  Not Found"
                    };
                    throw new System.Web.Http.HttpResponseException(resp);
                }

                return vendoritems;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }*/
        // PUT: api/TmAcbrmaps/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTmAcbrmap(string id, TmAcbrmap tmAcbrmap)
        {
            if (id != tmAcbrmap.BranchCode)
            {
                return BadRequest();
            }

            _context.Entry(tmAcbrmap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TmAcbrmapExists(id))
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

        // POST: api/TmAcbrmaps
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TmAcbrmap>> PostTmAcbrmap(TmAcbrmap tmAcbrmap)
        {
            _context.TmAcbrmap.Add(tmAcbrmap);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TmAcbrmapExists(tmAcbrmap.BranchCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTmAcbrmap", new { id = tmAcbrmap.BranchCode }, tmAcbrmap);
        }

        // DELETE: api/TmAcbrmaps/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TmAcbrmap>> DeleteTmAcbrmap(string id)
        {
            var tmAcbrmap = await _context.TmAcbrmap.FindAsync(id);
            if (tmAcbrmap == null)
            {
                return NotFound();
            }

            _context.TmAcbrmap.Remove(tmAcbrmap);
            await _context.SaveChangesAsync();

            return tmAcbrmap;
        }

        private bool TmAcbrmapExists(string id)
        {
            return _context.TmAcbrmap.Any(e => e.BranchCode == id);
        }
    }
}
