using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactPPD.Model;
using ReactPPD.VM;
using System.Net.Http;
using System.Net;

namespace ReactPPD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TmPricelistsController : ControllerBase
    {
        private readonly reactppdContext _context;

        public TmPricelistsController(reactppdContext context)
        {
            _context = context;
        }

        // GET: api/TmPricelists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TmPricelist>>> GetTmPricelist()
        {
            return await _context.TmPricelist.ToListAsync();
        }

        // GET: api/TmPricelists/5
        [HttpPost("Company")]
        public async Task<ActionResult<List<Company>>> GetCompany(string companyname)
        {
            try
            {
                if (companyname == null)
                {
                    var tmcompany = await _context.TmCompany
                                .OrderBy(i => i.CompanyName)
                                .Select(i => new Company { CompanyCode = i.CompanyCode, CompanyName = i.CompanyName }).ToListAsync();
                    if (tmcompany.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Company with Name = {0}", companyname)),
                            ReasonPhrase = "Company Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmcompany;
                }
                else if (companyname != null)
                {
                    var tmcompany = await _context.TmCompany
                               .OrderBy(i => i.CompanyName)
                               .Select(i => new Company { CompanyCode = i.CompanyCode, CompanyName = i.CompanyName }).ToListAsync();
                    if (tmcompany.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Company with Name = {0}", companyname)),
                            ReasonPhrase = "Company Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmcompany;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Division")]
        public async Task<ActionResult<List<Division>>> GetDivisiony(string userid)
        {
            try
            {
                if (userid == null)
                {
                    var tmDivision = await _context.TmDivision
                                   .Join(_context.TmUserdivmap, D => D.DivCode, UD => UD.DivCode, (D, UD) => new { TmDivision = D, TmUserdivmap = UD })
                                   .Where(m => m.TmDivision.IsActive == "A")
                                   .OrderBy(i => i.TmDivision.DivName)
                                   .Select(i => new Division { DivCode = i.TmDivision.DivCode, DivName = i.TmDivision.DivName }).ToListAsync();
                    if (tmDivision.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Divison for UserId = {0}", userid)),
                            ReasonPhrase = "Division Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmDivision;
                }
                else if (userid != null)
                {
                    var tmDivision = await _context.TmDivision
                                   .Join(_context.TmUserdivmap, D => D.DivCode, UD => UD.DivCode, (D, UD) => new { TmDivision = D, TmUserdivmap = UD })
                                   .Where(m => m.TmUserdivmap.UserId == userid)
                                   .Where(m => m.TmDivision.IsActive == "A")
                                   .OrderBy(i => i.TmDivision.DivName)
                                   .Select(i => new Division { DivCode = i.TmDivision.DivCode, DivName = i.TmDivision.DivName }).ToListAsync();
                    if (tmDivision.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Divison for UserId = {0}", userid)),
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

        [HttpPost("RegZone")]
        public async Task<ActionResult<List<Regzone>>> GetRegZone(string RegZoneCode)
        {
            try
            {
                if (RegZoneCode == null)
                {
                    var tmRegZone = await _context.TmRegzone
                                    .OrderBy(i => i.RegZoneCode)
                                    .Select(i => new Regzone { RegZoneCode = i.RegZoneCode, RegZoneName = i.RegZoneName }).ToListAsync();
                    if (tmRegZone.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No RegZone for Id = {0}", RegZoneCode)),
                            ReasonPhrase = "RegZone Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmRegZone;
                }
                else if (RegZoneCode != null)
                {
                    var tmRegZone = await _context.TmRegzone
                                    .Where(i => i.RegZoneCode.StartsWith(RegZoneCode))
                                    .OrderBy(i => i.RegZoneCode)
                                    .Select(i => new Regzone { RegZoneCode = i.RegZoneCode, RegZoneName = i.RegZoneName }).ToListAsync();
                    if (tmRegZone.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No RegZone for Id = {0}", RegZoneCode)),
                            ReasonPhrase = "RegZone Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmRegZone;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Region")]
        public async Task<ActionResult<List<Region>>> GetRegion(string DivCode, string RegZoneCode)
        {
            try
            {
                if (DivCode == null && RegZoneCode == null)
                {
                    var tmRegion = await _context.TmRegion
                                   .Join(_context.TmBranch, R => R.RegionCode, B => B.RegionCode, (R, B) => new { TmRegion = R, TmBranch = B })
                                   .Where(i => i.TmBranch.IsActive == "A")
                                   .OrderBy(i => i.TmRegion.RegionName)
                                   .Select(i => new Region { RegionCode = i.TmRegion.RegionCode, RegionName = i.TmRegion.RegionName }).ToListAsync();
                    if (tmRegion.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Region with RegZone Id = {0}", RegZoneCode)),
                            ReasonPhrase = "Region Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmRegion;
                }
                else if (DivCode != null && RegZoneCode != null)
                {
                    var tmRegion = await _context.TmRegion
                                   .Join(_context.TmBranch, R => R.RegionCode, B => B.RegionCode, (R, B) => new { TmRegion = R, TmBranch = B })
                                   .Where(i => i.TmBranch.IsActive == "A")
                                    .Where(i => i.TmBranch.DivCode.StartsWith(DivCode))
                                   .Where(i => i.TmBranch.RegZoneCode.StartsWith(RegZoneCode))
                                   .OrderBy(i => i.TmRegion.RegionName)
                                   .Select(i => new Region { RegionCode = i.TmRegion.RegionCode, RegionName = i.TmRegion.RegionName }).ToListAsync();
                    if (tmRegion.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Region with RegZone Id = {0}", RegZoneCode)),
                            ReasonPhrase = "Region Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmRegion;
                }
                return BadRequest(new { Message = "Missing Some Parameter Values" });
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Territory")]
        public async Task<ActionResult<List<Zone>>> GetTerritory(string DivCode, string RegZoneCode)
        {
            try
            {
                if (DivCode == null && RegZoneCode == null)
                {
                    var tmTerritory = await _context.TmZone
                                   .Join(_context.TmBranch, Z => Z.ZoneCode, B => B.ZoneCode, (Z, B) => new { TmZone = Z, TmBranch = B })
                                   .OrderBy(i => i.TmZone.ZoneName)
                                   .Select(i => new Zone { ZoneCode = i.TmZone.ZoneCode, ZoneName = i.TmZone.ZoneName }).ToListAsync();
                    if (tmTerritory.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Territory with RegZone Id = {0}", RegZoneCode)),
                            ReasonPhrase = "Territory Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmTerritory;
                }
                else if (DivCode != null && RegZoneCode != null)
                {
                    var tmTerritory = await _context.TmZone
                                  .Join(_context.TmBranch, Z => Z.ZoneCode, B => B.ZoneCode, (Z, B) => new { TmZone = Z, TmBranch = B })
                                  .Where(i => i.TmBranch.DivCode.StartsWith(DivCode))
                                  .Where(i => i.TmBranch.RegZoneCode.StartsWith(RegZoneCode))
                                  .OrderBy(i => i.TmZone.ZoneName)
                                  .Select(i => new Zone { ZoneCode = i.TmZone.ZoneCode, ZoneName = i.TmZone.ZoneName }).ToListAsync();
                    if (tmTerritory.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Territory with RegZone Id = {0}", RegZoneCode)),
                            ReasonPhrase = "Territory Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmTerritory;
                }
                return BadRequest(new { Message = "Missing Some Parameter Values" });
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Branch")]
        public async Task<ActionResult<List<Branch>>> GetBranch(string DivCode, string ZoneCode)
        {
            try
            {
                if (DivCode == null && ZoneCode == null)
                {
                    var tmBranch = await _context.TmBranch
                                  .Where(i => i.IsActive.StartsWith("A"))
                                  .OrderBy(i => i.BranchName)
                                  .Select(i => new Branch { BranchCode = i.BranchCode, BranchName = i.BranchName }).ToListAsync();
                    if (tmBranch.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Branch  with ZoneCode  = {0}", ZoneCode)),
                            ReasonPhrase = "Branch Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmBranch;
                }
                else if (DivCode != null && ZoneCode != null)
                {
                    var tmBranch = await _context.TmBranch
                                    .Where(i => i.IsActive.StartsWith("A"))
                                    .OrderBy(i => i.BranchName)
                                    .Select(i => new Branch { BranchCode = i.BranchCode, BranchName = i.BranchName }).ToListAsync();
                    if (tmBranch.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Branch  with ZoneCode  = {0}", ZoneCode)),
                            ReasonPhrase = "Branch Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmBranch;
                }
                return BadRequest(new { Message = "Missing Some Parameter Values" });
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("SubBranch")]
        public async Task<ActionResult<List<Branch>>> GetSubBranch(string ZoneCode, string BranchCode, string RegZoneCode, string RegionCode)
        {
            try
            {
                if (ZoneCode == null && BranchCode == null && RegZoneCode == null && RegionCode == null)
                {
                    var tmBranch = await _context.TmBranch
                                  .Where(i => i.IsActive.StartsWith("A"))
                                  .OrderBy(i => i.BranchName)
                                  .Select(i => new Branch { BranchCode = i.BranchCode, BranchName = i.BranchName }).ToListAsync();
                    if (tmBranch.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Branch  with ZoneCode  = {0}", ZoneCode)),
                            ReasonPhrase = "Branch Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmBranch;
                }
                else if (ZoneCode != null && BranchCode != null && RegZoneCode != null && RegionCode != null)
                {
                    var tmBranch = await _context.TmBranch
                                   .Where(i => i.IsActive.StartsWith("A"))
                                   .Where(i => i.ZoneCode.StartsWith(ZoneCode))
                                   .Where(i => i.BranchCode.StartsWith(BranchCode))
                                   .Where(i => i.RegZoneCode.StartsWith(RegZoneCode))
                                   .Where(i => i.RegionCode.StartsWith(RegionCode))
                                   .OrderBy(i => i.BranchName)
                                   .Select(i => new Branch { BranchCode = i.BranchCode, BranchName = i.BranchName }).ToListAsync();
                    if (tmBranch.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Branch  with ZoneCode  = {0}", ZoneCode)),
                            ReasonPhrase = "Branch Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmBranch;
                }
                return BadRequest(new { Message = "Missing Some Parameter Values" });
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Price")]
        public async Task<ActionResult<List<Gcm>>> GetPrice(string GcmType)
        {
            try
            {
                if (GcmType == null)
                {
                    var tmprice = await _context.TmGcm
                                   .Where(i => i.IsActive.StartsWith("A"))
                                   .OrderBy(i => i.GcmDesc)
                                   .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc }).ToListAsync();
                    if (tmprice.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No GCM with Type = {0}", GcmType)),
                            ReasonPhrase = "GCM Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmprice;
                }
                else if (GcmType != null)
                {
                    var tmprice = await _context.TmGcm
                                  .Where(i => i.IsActive.StartsWith("A"))
                                  .Where(i => i.GcmType.StartsWith(GcmType))
                                  .OrderBy(i => i.GcmDesc)
                                  .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc }).ToListAsync();
                    if (tmprice.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No GCM with Type = {0}", GcmType)),
                            ReasonPhrase = "GCM Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmprice;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("ItemType")]
        public async Task<ActionResult<List<ItemType>>> GetItemType(string Desc)
        {
            try
            {
                if (Desc == null)
                {
                    var tmItemtype = await _context.TmItemtype
                                   .OrderBy(i => i.Descn)
                                   .Select(i => new ItemType { Itemtype = i.ItemType, Descn = i.Descn }).ToListAsync();
                    if (tmItemtype.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Itemtype with Name = {0}", Desc)),
                            ReasonPhrase = "ItemType Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItemtype;
                }
                else if (Desc != null)
                {
                    var tmItemtype = await _context.TmItemtype
                                     .Where(i => i.Descn.StartsWith(Desc))
                                    .OrderBy(i => i.Descn)
                                    .Select(i => new ItemType { Itemtype = i.ItemType, Descn = i.Descn }).ToListAsync();
                    if (tmItemtype.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Itemtype with Name = {0}", Desc)),
                            ReasonPhrase = "ItemType Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItemtype;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("ItemCode")]
        public async Task<ActionResult<List<Item>>> GetItem(string ItemName, string Itemtype)
        {
            try
            {
                if (ItemName == null && Itemtype == null)
                {
                    var tmItem = await _context.TmItem
                                   .OrderBy(i => i.ItemName)
                                   .Select(i => new Item { ItemCode = i.ItemCode, ItemName = i.ItemName, Nature = i.Nature, UomStk = i.UomStk }).ToListAsync();
                    if (tmItem.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Item with Name = {0}", ItemName)),
                            ReasonPhrase = "Item Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItem;
                }
                else if (ItemName != null && Itemtype != null)
                {
                    var tmItem = await _context.TmItem
                                 .Where(i => i.ItemName.StartsWith(ItemName))
                                 .Where(i => i.ItemType.StartsWith(Itemtype))
                                 .OrderBy(i => i.ItemName)
                                 .Select(i => new Item { ItemCode = i.ItemCode, ItemName = i.ItemName, Nature = i.Nature, UomStk = i.UomStk }).ToListAsync();
                    if (tmItem.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Item with Name = {0}", ItemName)),
                            ReasonPhrase = "Item Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItem;
                }
                return BadRequest(new { Message = "Missing Some Parameter Values" });
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Nature")]
        public async Task<ActionResult<List<ItemNature>>> GetNature(string ItemCode)
        {
            try
            {
                if (ItemCode == null)
                {
                    var tmItemNature = await _context.TmItemnature
                                      .OrderBy(i => i.Descn)
                                      .Select(i => new ItemNature { Nature = i.Nature, Descn = i.Descn }).ToListAsync();
                    if (tmItemNature.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Nature with ItemCode = {0}", ItemCode)),
                            ReasonPhrase = "Nature Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItemNature;
                }
                else if (ItemCode != null)
                {
                    var tmItemNature = await _context.TmItemnature
                                       .Where(i => i.ItemCode.StartsWith(ItemCode))
                                       .OrderBy(i => i.Descn)
                                       .Select(i => new ItemNature { Nature = i.Nature, Descn = i.Descn }).ToListAsync();
                    if (tmItemNature.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Nature with ItemCode = {0}", ItemCode)),
                            ReasonPhrase = "Nature Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItemNature;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Party")]
        public async Task<ActionResult<List<Account>>> GetParty(string AccountName)
        {
            try
            {
                if (AccountName == null)
                {
                    var tmAccount = await _context.TmAccounts
                                       .Where(i => i.AcType == "SL" && i.IsActive == "A")
                                       .Where(i => i.PartyType == "C" || i.PartyType == "D" || i.PartyType == "A" || i.PartyType == "G")
                                      .OrderBy(i => i.AccountName)
                                      .Select(i => new Account { AccountCode = i.AccountCode, AccountName = i.AccountName }).ToListAsync();
                    if (tmAccount.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Party with AccountName = {0}", AccountName)),
                            ReasonPhrase = "Party Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmAccount;
                }
                else if (AccountName != null)
                {
                    var tmAccount = await _context.TmAccounts
                                    .Where(i => i.AcType == "SL" && i.IsActive == "A")
                                    .Where(i => i.PartyType == "C" || i.PartyType == "D" || i.PartyType == "A" || i.PartyType == "G")
                                    .Where(i => i.AccountName.StartsWith(AccountName))
                                    .OrderBy(i => i.AccountName)
                                    .Select(i => new Account { AccountCode = i.AccountCode, AccountName = i.AccountName }).ToListAsync();
                    if (tmAccount.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Party with AccountName = {0}", AccountName)),
                            ReasonPhrase = "Party Not Found"
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

        [HttpPost("Place")]
        public async Task<ActionResult<List<Gcm>>> GetParty()
        {
            try
            {
                var tmGcm = await _context.TmGcm
                               .Where(i => i.GcmType == "PST" && i.IsActive == "A")
                                  .OrderBy(i => i.GcmDesc)
                                  .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc }).ToListAsync();
                if (tmGcm.Count == 0)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("No Place Found ")),
                        ReasonPhrase = "Place Not Found"
                    };
                    throw new System.Web.Http.HttpResponseException(resp);
                }
                return tmGcm;

            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("ViewPricelist")]
        public async Task<ActionResult<List<PricelistView>>> ViewTmPricelist(int DocIntNo)
        {
            try
            {
                var tmPricelist = _context.TmPricelist.Where( x => x.DocIntNo == DocIntNo).FirstOrDefault();
                if (tmPricelist != null && tmPricelist.ItemNature != "R")
                {
                    var Pricelist = await _context.TmPricelist
                                    .Join(_context.TmItemnature, A => A.ItemNature, B => B.Nature, (A, B) => new { TmPricelist = A, TmItemnature = B })
                                    .Join(_context.TmItemnature, A => A.TmPricelist.ItemCode, B => B.ItemCode, (A, B) => new { TmPricelist = A, TmItemnature = B })
                                    .Join(_context.TmItem, A => A.TmPricelist.TmPricelist.ItemCode, C => C.ItemCode, (A, C) => new { TmPricelist = A, TmItem = C })
                                    .Join(_context.TmAccounts, A => A.TmPricelist.TmPricelist.TmPricelist.PartyCode, D => D.AccountCode, (A, D) => new { TmPricelist = A, TmAccounts = D })
                                    .Join(_context.TmItemtype, C => C.TmPricelist.TmItem.ItemType, E => E.ItemType, (C, E) => new { TmItem = C, TmItemtype = E })
                                    .Join(_context.TmBranch, A => A.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.BranchCode, F => F.BranchCode, (A, F) => new { TmPricelist = A, TmBranch = F })
                                    .Join(_context.TmBranch, A => A.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.SubBrCode, G => G.BranchCode, (A, G) => new { TmPricelist = A, TmBranch = G })
                                    .Join(_context.TmZone, A => A.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.ZoneCode, H => H.ZoneCode, (A, H) => new { TmPricelist = A, TmZone = H })
                                    .Join(_context.TmRegion, A => A.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.RegionCode, I => I.RegionCode, (A, I) => new { TmPricelist = A, TmRegion = I })
                                    .Join(_context.TmRegzone, A => A.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.RegZoneCode, J => J.RegZoneCode, (A, J) => new { TmPricelist = A, TmRegzone = J })
                                    .Join(_context.TmDivision, A => A.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.DivCode, K => K.DivCode, (A, K) => new { TmPricelist = A, TmDivision = K })
                                    .Join(_context.TmCompany, A => A.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.CompanyCode, L => L.CompanyCode, (A, L) => new { TmPricelist = A, TmCompany = L })
                                    .Join(_context.TmGcm, A => A.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.PricingType, M => M.GcmCode, (A, M) => new { TmPricelist = A, TmGcm = M })
                                    .Where(i => i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.DocIntNo == DocIntNo)
                                    .Select(i => new PricelistView
                                    {
                                        DocNo = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.DocNo,
                                        TransDate = (DateTimeOffset)i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TransDate,
                                        PricingType = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.PricingType,
                                        PricingName = i.TmGcm.GcmDesc,
                                        EffectFrom = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.EffectFrom,
                                        EffectTo = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.EffectTo,
                                        BranchCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.BranchCode,
                                        BranchName = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmBranch.BranchName,
                                        ItemCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.ItemCode,
                                        ItemName = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmItem.ItemName,
                                        Rate = (Decimal)i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.Rate,
                                        DiscPer = (Decimal)i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.DiscPer,
                                        IsActive = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.IsActive,
                                        AcYearNo = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.AcYearNo,
                                        DocIntNo = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.DocIntNo,
                                        PartyCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.PartyCode,
                                        PartyName = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmAccounts.AccountName,
                                        Age = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.Age,
                                        ItemNature = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.ItemNature,
                                        ItemDescn = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmItemnature.Descn,
                                        CompanyCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.CompanyCode,
                                        CompanyName = i.TmPricelist.TmCompany.CompanyName,
                                        DivCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.DivCode,
                                        DivName = i.TmPricelist.TmPricelist.TmDivision.DivName,
                                        RegZoneCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.RegZoneCode,
                                        RegZoneName = i.TmPricelist.TmPricelist.TmPricelist.TmRegzone.RegZoneName,
                                        RegionCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.RegionCode,
                                        RegionName = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmRegion.RegionName,
                                        ZoneCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.ZoneCode,
                                        ZoneName = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmZone.ZoneName,
                                        SubBrCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.SubBrCode,
                                        SubBrCodeName = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmBranch.BranchName,
                                        LogBrCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.LogBrCode,
                                        RegCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.RegCode,
                                        RegName = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TmPricelist.RegName


                                    }).ToListAsync();

                    if (Pricelist.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No PriceList Found ")),
                            ReasonPhrase = "PriceList Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return Pricelist;              

                }
                else
                {
                    var Pricelist = await _context.TmPricelist                                   
                                   .Join(_context.TmItemnature, A => A.ItemCode, B => B.ItemCode, (A, B) => new { TmPricelist = A, TmItemnature = B })
                                   .Join(_context.TmItem, A => A.TmPricelist.ItemCode, C => C.ItemCode, (A, C) => new { TmPricelist = A, TmItem = C })
                                   .Join(_context.TmAccounts, A => A.TmPricelist.TmPricelist.PartyCode, D => D.AccountCode, (A, D) => new { TmPricelist = A, TmAccounts = D })
                                   .Join(_context.TmItemtype, C => C.TmPricelist.TmItem.ItemType, E => E.ItemType, (C, E) => new { TmItem = C, TmItemtype = E })
                                   .Join(_context.TmBranch, A => A.TmItem.TmPricelist.TmPricelist.TmPricelist.BranchCode, F => F.BranchCode, (A, F) => new { TmPricelist = A, TmBranch = F })
                                   .Join(_context.TmBranch, A => A.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.SubBrCode, G => G.BranchCode, (A, G) => new { TmPricelist = A, TmBranch = G })
                                   .Join(_context.TmZone, A => A.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.ZoneCode, H => H.ZoneCode, (A, H) => new { TmPricelist = A, TmZone = H })
                                   .Join(_context.TmRegion, A => A.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.RegionCode, I => I.RegionCode, (A, I) => new { TmPricelist = A, TmRegion = I })
                                   .Join(_context.TmRegzone, A => A.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.RegZoneCode, J => J.RegZoneCode, (A, J) => new { TmPricelist = A, TmRegzone = J })
                                   .Join(_context.TmDivision, A => A.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.DivCode, K => K.DivCode, (A, K) => new { TmPricelist = A, TmDivision = K })
                                   .Join(_context.TmCompany, A => A.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.CompanyCode, L => L.CompanyCode, (A, L) => new { TmPricelist = A, TmCompany = L })
                                   .Join(_context.TmGcm, A => A.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.PricingType, M => M.GcmCode, (A, M) => new { TmPricelist = A, TmGcm = M })
                                   .Where(i => i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.DocIntNo == DocIntNo)
                                   .Select(i => new PricelistView
                                   {
                                       DocNo = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.DocNo,
                                       TransDate = (DateTimeOffset)i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.TransDate,
                                       PricingType = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.PricingType,
                                       PricingName = i.TmGcm.GcmDesc,
                                       EffectFrom = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.EffectFrom,
                                       EffectTo = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.EffectTo,
                                       BranchCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.BranchCode,
                                       BranchName = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmBranch.BranchName,
                                       ItemCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.ItemCode,
                                       ItemName = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmItem.ItemName,
                                       Rate = (Decimal)i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.Rate,
                                       DiscPer = (Decimal)i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.DiscPer,
                                       IsActive = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.IsActive,
                                       AcYearNo = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.AcYearNo,
                                       DocIntNo = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.DocIntNo,
                                       PartyCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.PartyCode,
                                       PartyName = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmAccounts.AccountName,
                                       Age = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.Age,
                                       ItemNature = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.ItemNature,
                                       ItemDescn = "REGULAR",
                                       CompanyCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.CompanyCode,
                                       CompanyName = i.TmPricelist.TmCompany.CompanyName,
                                       DivCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.DivCode,
                                       DivName = i.TmPricelist.TmPricelist.TmDivision.DivName,
                                       RegZoneCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.RegZoneCode,
                                       RegZoneName = i.TmPricelist.TmPricelist.TmPricelist.TmRegzone.RegZoneName,
                                       RegionCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.RegionCode,
                                       RegionName = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmRegion.RegionName,
                                       ZoneCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.ZoneCode,
                                       ZoneName = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmZone.ZoneName,
                                       SubBrCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.SubBrCode,
                                       SubBrCodeName = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmBranch.BranchName,
                                       LogBrCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.LogBrCode,
                                       RegCode = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.RegCode,
                                       RegName = i.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmPricelist.TmItem.TmPricelist.TmPricelist.TmPricelist.RegName


                                   }).ToListAsync();

                    if (Pricelist.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No PriceList Found ")),
                            ReasonPhrase = "PriceList Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return Pricelist;
                }
               
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }
       


        // PUT: api/TmPricelists/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTmPricelist(int id, TmPricelist tmPricelist)
        {
            if (id != tmPricelist.DocIntNo)
            {
                return BadRequest();
            }

            _context.Entry(tmPricelist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TmPricelistExists(id))
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

        // POST: api/TmPricelists
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TmPricelist>> PostTmPricelist(TmPricelist tmPricelist)
        {
            _context.TmPricelist.Add(tmPricelist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TmPricelistExists(tmPricelist.DocIntNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTmPricelist", new { id = tmPricelist.DocIntNo }, tmPricelist);
        }

        // DELETE: api/TmPricelists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TmPricelist>> DeleteTmPricelist(int id)
        {
            var tmPricelist = await _context.TmPricelist.FindAsync(id);
            if (tmPricelist == null)
            {
                return NotFound();
            }

            _context.TmPricelist.Remove(tmPricelist);
            await _context.SaveChangesAsync();

            return tmPricelist;
        }

        private bool TmPricelistExists(int id)
        {
            return _context.TmPricelist.Any(e => e.DocIntNo == id);
        }
    }
}
