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
        [HttpPost("Godown")]
        public async Task<ActionResult<IEnumerable<TmGodown>>> GetTmGodown(string godownname,string branchcode)
        {
            try
            {
                if (godownname == null)
                {
                    var tmGodown = await _context.TmGodown
                                   .Where(i =>i.BranchCode == branchcode)
                                   .OrderBy(i => i.GoDownName)
                                   .Select(i => new TmGodown { GoDownCode = i.GoDownCode, GoDownName = i.GoDownName })
                                   .ToListAsync();


                    if (tmGodown.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No GoDown with Name = {0}", godownname)),
                            ReasonPhrase = "GoDown  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmGodown;
                }
                else if (godownname != null)
                {
                    var tmGodown = await _context.TmGodown
                                   .Where(i => i.BranchCode == branchcode)
                                   .Where(i => i.GoDownName.StartsWith(godownname))
                                   .OrderBy(i => i.GoDownName)
                                   .Select(i => new TmGodown { GoDownCode = i.GoDownCode, GoDownName = i.GoDownName })
                                   .ToListAsync();


                    if (tmGodown.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {

                            Content = new StringContent(string.Format("No GoDown with Name = {0}", godownname)),
                            ReasonPhrase = "GoDown  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmGodown;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }
        [HttpPost("Branch")]
        public async Task<ActionResult<List<Godown>>> GetTmGodownBranch(string divisionname, string regioncode)
        {
            try
            {
                if (divisionname == null)
                {
                    var tmBranch = await _context.TmBranch
                                  .Join(_context.TmDivision, b => b.DivCode, d => d.DivCode, (b, d) => new { TmBranch = b, TmDivision = d })                                                              
                                  .Where(m => m.TmBranch.DivCode == m.TmDivision.DivCode)
                                  .Where(m => m.TmBranch.RegionCode == regioncode)  
                                  .OrderBy(m =>m.TmDivision.DivName)
                                  .Select(m => new
                                  {
                                  BranchCode = m.TmBranch.BranchCode,
                                  DivisionName= m.TmDivision.DivName
                                  
                                 }).ToListAsync();
                    List<Godown> GoDownitems = new List<Godown>();
                    foreach (var items in tmBranch)
                    {
                        GoDownitems.Add(new Godown
                        {
                            Branchcode = items.BranchCode.ToString(),
                            DivisionName = items.DivisionName.ToString(),
                           
                        });
                    }

                    if (tmBranch.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Division with Name = {0}", divisionname)),
                            ReasonPhrase = "Division  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return GoDownitems;
                }
                else if (divisionname != null)
                {
                    var tmBranch = await _context.TmBranch
                                 .Join(_context.TmDivision, b => b.DivCode, d => d.DivCode, (b, d) => new { TmBranch = b, TmDivision = d })
                                 .Where(m => m.TmBranch.DivCode == m.TmDivision.DivCode)
                                 .Where(m => m.TmBranch.RegionCode == regioncode)
                                 .Where(m => m.TmDivision.DivName.StartsWith(divisionname))
                                 .Select(m => new
                                 {
                                     BranchCode = m.TmBranch.BranchCode,
                                     DivisionName = m.TmDivision.DivName

                                 }).ToListAsync();
                    List<Godown> GoDownitems = new List<Godown>();
                    foreach (var items in tmBranch)
                    {
                        GoDownitems.Add(new Godown
                        {
                            Branchcode = items.BranchCode.ToString(),
                            DivisionName = items.DivisionName.ToString(),

                        });
                    }

                    if (tmBranch.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Division with Name = {0}", divisionname)),
                            ReasonPhrase = "Division  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return GoDownitems;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }
        [HttpPost("GodownType")]
        public async Task<ActionResult<IEnumerable<TmGcm>>> GetTmGodownType(string description)
        {
            try
            {
                if (description == null)
                {
                   // string[] gmccode = new string[] { "CG", "WG", "HG", "IG", "MG" };
                    var tmGcm = await _context.TmGcm
                                   .Where(i => i.IsActive == "A")
                                   .Where(i => i.GcmType == "GTM")
                                   .Where(i => i.GcmCode != "CG" || i.GcmCode != "WG" || i.GcmCode != "HG" || i.GcmCode != "IG" || i.GcmCode != "MG")
                                   .OrderBy(i => i.GcmDesc)
                                   .Select(i => new TmGcm { GcmType = i.GcmType, GcmDesc = i.GcmDesc })
                                   .ToListAsync();

                    if (tmGcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No GoDown Type with Name = {0}", description)),
                            ReasonPhrase = "GoDown Type  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmGcm;
                }
                else if (description != null)
                {
                    var tmGcm = await _context.TmGcm
                                .Where(i => i.GcmDesc == description)
                                .Where(i => i.IsActive == "A")
                                .Where(i => i.GcmType == "GTM")
                                .Where(i => i.GcmCode != "CG" || i.GcmCode != "WG" || i.GcmCode != "HG" || i.GcmCode != "IG" || i.GcmCode != "MG")
                                .OrderBy(i => i.GcmDesc)
                                .Select(i => new TmGcm { GcmType = i.GcmType, GcmDesc = i.GcmDesc })
                                .ToListAsync();

                    if (tmGcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No GoDown Type with Name = {0}", description)),
                            ReasonPhrase = "GoDown Type  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmGcm;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Weighbridge")]
        public async Task<ActionResult<IEnumerable<TmGcm>>> GetTmGcm(string description)
        {
            try
            {
                if (description == null)
                {                   
                    var tmGcm = await _context.TmGcm
                                   .Where(i => i.IsActive == "A")
                                   .Where(i => i.GcmType == "WBT")
                                   .OrderBy(i => i.GcmDesc)
                                   .Select(i => new TmGcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc })
                                   .ToListAsync();

                    if (tmGcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No WeighBridge with Name = {0}", description)),
                            ReasonPhrase = "WeighBridge Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmGcm;
                }
                else if (description != null)
                {
                    var tmGcm = await _context.TmGcm
                                .Where(i => i.GcmDesc.Contains(description))
                                .Where(i => i.IsActive == "A")
                                .Where(i => i.GcmType == "WBT")
                                .OrderBy(i => i.GcmDesc)
                                .Select(i => new TmGcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc })
                                .ToListAsync();

                    if (tmGcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No WeighBridge with Name = {0}", description)),
                            ReasonPhrase = "WeighBridge Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmGcm;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("CostCenter")]
        public async Task<ActionResult<IEnumerable<TmCostcenter>>> GetTmCostcenter(string CcName,string regioncode)
        {
            try
            {
                if (CcName == null)
                {
                    var tmCostcenter = await _context.TmCostcenter
                                       .Where(i => i.RegionCode.Contains(regioncode))
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
                                      .Where(i => i.RegionCode.Contains(regioncode))
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

        [HttpPost("Country")]
        public async Task<ActionResult<IEnumerable<TmPlace>>> GetTmPlace(string countryname)
        {
            try
            {
                if (countryname == null)
                {
                    var tmPlace= await _context.TmPlace
                                       .Where(i => i.PlaceType.Contains("O"))
                                       .OrderBy(i => i.CountryName)
                                       .Select(i => new TmPlace { CountryCode = i.CountryCode, CountryName = i.CountryName })
                                       .ToListAsync();


                    if (tmPlace.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Country with Name = {0}", countryname)),
                            ReasonPhrase = "Country  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmPlace;
                }
                else if (countryname != null)
                {
                    var tmPlace = await _context.TmPlace
                                  .Where(i => i.PlaceType.Contains("O"))
                                  .Where(i => i.CountryName.Contains(countryname))
                                  .OrderBy(i => i.CountryName)
                                  .Select(i => new TmPlace { CountryCode = i.CountryCode, CountryName = i.CountryName })
                                  .ToListAsync();

                    if (tmPlace.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Country with Name = {0}", countryname)),
                            ReasonPhrase = "Country  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmPlace;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("State")]
        public async Task<ActionResult<IEnumerable<TmPlace>>> GetTmPlaceState(string statename,string countrycode)
        {
            try
            {
                if (statename == null)
                {
                    var tmPlace = await _context.TmPlace
                                  .Where(i => i.PlaceType.Contains("S"))
                                  .Where(i => i.CountryCode.Contains(countrycode))
                                  .OrderBy(i => i.StateName)
                                  .Select(i => new TmPlace { StateCode = i.StateCode, StateName = i.StateName })
                                  .ToListAsync();

                    if (tmPlace.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No State with Name = {0}", statename)),
                            ReasonPhrase = "State  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmPlace;
                }
                else if (statename != null)
                {
                    var tmPlace = await _context.TmPlace
                                 .Where(i => i.PlaceType.Contains("S"))
                                 .Where(i => i.CountryCode.Contains(countrycode))
                                 .Where(i => i.StateName.Contains(statename))
                                 .OrderBy(i => i.StateName)
                                 .Select(i => new TmPlace { StateCode = i.StateCode, StateName = i.StateName })
                                 .ToListAsync();

                    if (tmPlace.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No State with Name = {0}", statename)),
                            ReasonPhrase = "State  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmPlace;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("District")]
        public async Task<ActionResult<IEnumerable<TmPlace>>> GetTmPlaceDistrict(string districtname,string statecode)
        {
            try
            {
                if (districtname == null)
                {
                    var tmPlace = await _context.TmPlace
                                  .Where(i => i.PlaceType.Contains("D"))
                                  .Where(i => i.StateCode.Contains(statecode))
                                  .OrderBy(i => i.DistName)
                                  .Select(i => new TmPlace { DistCode = i.DistCode, DistName = i.DistName })
                                  .ToListAsync();

                    if (tmPlace.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No District with Name = {0}", districtname)),
                            ReasonPhrase = "District  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmPlace;
                }
                else if (districtname != null)
                {
                    var tmPlace = await _context.TmPlace
                                  .Where(i => i.PlaceType.Contains("D"))
                                  .Where(i => i.StateCode.Contains(statecode))
                                  .Where(i => i.DistName.Contains(districtname))
                                  .OrderBy(i => i.DistName)
                                  .Select(i => new TmPlace { DistCode = i.DistCode, DistName = i.DistName })
                                  .ToListAsync();

                    if (tmPlace.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No District with Name = {0}", districtname)),
                            ReasonPhrase = "District  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmPlace;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Taluka")]
        public async Task<ActionResult<IEnumerable<TmPlace>>> GetTmPlaceTaluka(string districtcode, string talukaname)
        {
            try
            {
                if (talukaname == null)
                {
                    var tmPlace = await _context.TmPlace
                                  .Where(i => i.PlaceType.Contains("M"))
                                  .Where(i => i.DistCode.Contains(districtcode))
                                  .OrderBy(i => i.TalukName)
                                  .Select(i => new TmPlace { TalukCode = i.TalukCode, TalukName = i.TalukName })
                                  .ToListAsync();

                    if (tmPlace.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Taluka with Name = {0}", talukaname)),
                            ReasonPhrase = "Taluka  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmPlace;
                }
                else if (talukaname != null)
                {
                    var tmPlace = await _context.TmPlace
                                  .Where(i =>i.TalukName.Contains(talukaname))
                                  .Where(i => i.PlaceType.Contains("M"))
                                  .Where(i => i.DistCode.Contains(districtcode))
                                  .OrderBy(i => i.TalukName)
                                  .Select(i => new TmPlace { TalukCode = i.TalukCode, TalukName = i.TalukName })
                                  .ToListAsync();

                    if (tmPlace.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Taluka with Name = {0}", talukaname)),
                            ReasonPhrase = "Taluka  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmPlace;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Town")]
        public async Task<ActionResult<IEnumerable<TmPlace>>> GetTmPlaceTown(string talukacode, string cityname)
        {
            try
            {
                if (cityname == null)
                {
                    var tmPlace = await _context.TmPlace
                                  .Where(i => i.PlaceType.Contains("T"))
                                  .Where(i => i.TalukCode.Contains(talukacode))
                                  .OrderBy(i => i.CityName)
                                  .Select(i => new TmPlace { CityCode = i.CityCode, CityName = i.CityName })
                                  .ToListAsync();

                    if (tmPlace.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No City with Name = {0}", cityname)),
                            ReasonPhrase = "City  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmPlace;
                }
                else if (cityname != null)
                {
                    var tmPlace = await _context.TmPlace
                                     .Where(i => i.PlaceType.Contains("T"))
                                     .Where(i => i.CityName.Contains(cityname))
                                     .Where(i => i.TalukCode.Contains(talukacode))
                                     .OrderBy(i => i.CityName)
                                     .Select(i => new TmPlace { CityCode = i.CityCode, CityName = i.CityName })
                                     .ToListAsync();

                    if (tmPlace.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No City with Name = {0}", cityname)),
                            ReasonPhrase = "City  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmPlace;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("pincode")]
        public async Task<ActionResult<IEnumerable<TmPlace>>> GetTmPlaceTown(string citycode)
        {
            try
            {              
                    var tmPlace = await _context.TmPlace
                                  .Where(i => i.PlaceType.Contains("Z"))
                                  .Where(i => i.CityCode.Contains(citycode))
                                  .OrderBy(i => i.PinCode)
                                  .Select(i => new TmPlace { PinCode= i.PinCode})
                                  .ToListAsync();

                    if (tmPlace.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No PinCode with CityCode = {0}",citycode )),
                            ReasonPhrase = "PinCode  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmPlace;
               
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("ViewData")]
        public async Task<ActionResult<IEnumerable<TmGodown>>> ViewTmGodown(string gcCode)
        {
            try
            {
                var tmGoddown = await _context.TmGodown
                                  .Where(i => i.GoDownCode == gcCode)
                                  .Select(i => i).ToListAsync();
                if (tmGoddown.Count == 0)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("No GodDown with ID = {0}", gcCode)),
                        ReasonPhrase = "Godown Not Found"
                    };
                    throw new System.Web.Http.HttpResponseException(resp);
                }
                return tmGoddown;

            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }
        [HttpPost("SaveUpdate")]
        public async Task<ActionResult<Response>> PostTmCostcenter(string gccode, TmGodown tmGodown)
        {
            if (gccode != tmGodown.GoDownCode)
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
                        return new Response { Status = "Conflict", Message = "Record Already Exist" };
                    }
                }
                return new Response { Status = "SUCCESSFULL", Message = "SAVED SUCCESSFULLY" };

            }
            else if (gccode == tmGodown.GoDownCode)
            {
                TmGodown newtmGodown = new TmGodown();
                newtmGodown.WeighBridge = tmGodown.WeighBridge;
                newtmGodown.CcCode = tmGodown.CcCode;
                newtmGodown.Address1 = tmGodown.Address1;
                newtmGodown.Address2 = tmGodown.Address2;
                newtmGodown.Address3 = tmGodown.Address3;
                newtmGodown.PinCode = tmGodown.PinCode;
                newtmGodown.CityCode = tmGodown.CityCode;
                newtmGodown.TalukCode = tmGodown.TalukCode;
                newtmGodown.DistCode = tmGodown.DistCode;
                newtmGodown.StateCode = tmGodown.StateCode;
                newtmGodown.CountryCode = tmGodown.CountryCode;
                newtmGodown.MailId = tmGodown.MailId;
                newtmGodown.FaxNo = tmGodown.FaxNo;
                newtmGodown.MobileNo = tmGodown.MobileNo;

                _context.Entry(newtmGodown).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TmGodownExists(gccode))
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
