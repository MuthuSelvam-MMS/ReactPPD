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
    public class TmAccountsController : ControllerBase
    {
        private readonly reactppdContext _context;

        public TmAccountsController(reactppdContext context)
        {
            _context = context;
        }

        // GET: api/TmAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TmAccounts>>> GetTmAccounts()
        {
            return await _context.TmAccounts.ToListAsync();
        }

        // GET: api/TmAccounts/5
        [HttpPost("AccountCode")]
        public async Task<ActionResult<List<Account>>> GetTmAccounts(string accountname)
        {
            try
            {
                if (accountname == null)
                {
                    var tmAccounts = await _context.TmAccounts                                     
                                     .Where(i => i.IsActive.Contains("A"))
                                     .OrderBy(i => i.AccountName)
                                     .Select(i => new Account { AccountCode = i.AccountCode, AccountName = i.AccountName, CreditDays = i.CreditDays })
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
                                    .Where(i => i.IsActive.Contains("A"))
                                    .Where(i => i.AccountName.StartsWith(accountname))
                                    .OrderBy(i => i.AccountName)
                                    .Select(i => new Account { AccountCode = i.AccountCode, AccountName = i.AccountName, CreditDays = i.CreditDays })
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

        [HttpPost("TdNature")]
        public async Task<ActionResult<List<Tds>>> GetTmTds(string naturedecn,DateTime fromdate)
        {
            try
            {
                
                //string selectDate = fromdate.ToString("yyyy-MM-dd");
                //DateTime formattedDate = DateTime.Parse(selectDate);

                if (naturedecn == null )
                {
                    var tmTds = await _context.TmTdsnature
                                .Join(_context.TmTdsac,Tdn => Tdn.NatureCode,Tda =>Tda.Nature, (Tdn, Tda) => new { TmTdsnature = Tdn, TmTdsac = Tda })
                                .Join(_context.TmAccounts, Tda=>Tda.TmTdsac.AccountCode, A => A.AccountCode, (Tda, A) => new {TmTdsac =Tda, TmAccounts = A})
                                .Where(m => m.TmTdsac.TmTdsnature.IsActive == m.TmTdsac.TmTdsac.IsActive && m.TmTdsac.TmTdsac.IsActive == m.TmAccounts.IsActive )                             
                                .Where(m => m.TmTdsac.TmTdsnature.IsActive.Contains("A"))
                                .OrderBy(m => m.TmTdsac.TmTdsnature.NatureDesc)
                                .Select(m => new Tds
                                {
                                  AccountCode = m.TmTdsac.TmTdsac.AccountCode,
                                  NatureCode = m.TmTdsac.TmTdsnature.NatureCode,
                                  NatureDesc = m.TmTdsac.TmTdsnature.NatureDesc,
                                  DedPer = (decimal)m.TmTdsac.TmTdsac.DedPer
                                
                              }).ToListAsync();


                    if (tmTds.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No TDS with Name = {0}", naturedecn)),
                            ReasonPhrase = "TDS  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmTds;
                }
                else if (naturedecn != null && fromdate != null)
                {                   
                    var tmTds = await _context.TmTdsnature
                               .Join(_context.TmTdsac, Tdn => Tdn.NatureCode, Tda => Tda.Nature, (Tdn, Tda) => new { TmTdsnature = Tdn, TmTdsac = Tda })
                               .Join(_context.TmAccounts, Tda => Tda.TmTdsac.AccountCode, A => A.AccountCode, (Tda, A) => new { TmTdsac = Tda, TmAccounts = A })
                               .Where(m => m.TmTdsac.TmTdsnature.IsActive == m.TmTdsac.TmTdsac.IsActive && m.TmTdsac.TmTdsac.IsActive == m.TmAccounts.IsActive)
                               .Where(m => m.TmTdsac.TmTdsac.FromDate >= m.TmTdsac.TmTdsac.FromDate && m.TmTdsac.TmTdsac.FromDate <= m.TmTdsac.TmTdsac.ToDate)
                               .Where(m => m.TmTdsac.TmTdsnature.IsActive.Contains("A"))
                               .Where(m => m.TmTdsac.TmTdsac.FromDate == fromdate)
                               .OrderBy(m => m.TmTdsac.TmTdsnature.NatureDesc)
                             .Select(m => new Tds
                             {
                                 AccountCode = m.TmTdsac.TmTdsac.AccountCode,
                                 NatureCode = m.TmTdsac.TmTdsnature.NatureCode,
                                 NatureDesc = m.TmTdsac.TmTdsnature.NatureDesc,
                                 DedPer = (decimal)m.TmTdsac.TmTdsac.DedPer

                             }).ToListAsync();


                    if (tmTds.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No TDS with Name = {0}", naturedecn)),
                            ReasonPhrase = "TDS  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmTds;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Accounttype")]
        public async Task<ActionResult<List<Gcm>>> GetTmGcm(string gcmdescn, string gcmtype)
        {
            try
            {
                if (gcmdescn == null && gcmtype == null)
                {
                    var tmgcm = await _context.TmGcm
                                     .Where(i => i.IsActive.Contains("A"))
                                     .OrderBy(i => i.GcmDesc)
                                     .Select(i => new Gcm {GcmCode = i.GcmCode, GcmDesc = i.GcmDesc})
                                     .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Gcm with Name = {0}", gcmdescn)),
                            ReasonPhrase = "GCM  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                else if (gcmdescn != null && gcmtype != null)
                {
                    var tmgcm = await _context.TmGcm
                               .Where(i => i.GcmDesc.StartsWith(gcmdescn))
                               .Where(i => i.GcmType.StartsWith(gcmtype))
                               .Where(i => i.IsActive.Contains("A"))
                               .OrderBy(i => i.GcmDesc)
                               .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc })
                               .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Gcm with Name = {0}", gcmdescn)),
                            ReasonPhrase = "GCM  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Grouptype")]
        public async Task<ActionResult<List<Gcm>>> GetTmGcmGrouptype(string gcmdescn)
        {
            try
            {
                if (gcmdescn == null )
                {
                    var tmgcm = await _context.TmGcm
                                .Where( i => i.GcmType.Contains("GPT"))
                                .Where(i => i.GcmCode.Contains("LG") || i.GcmCode.Contains("AG") || i.GcmCode.Contains("DE"))
                                .Where(i => i.IsActive.Contains("A"))
                                .OrderBy(i => i.GcmDesc)
                                .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc })
                                .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Gcm with Name = {0}", gcmdescn)),
                            ReasonPhrase = "GCM  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                else if (gcmdescn != null )
                {
                    var tmgcm = await _context.TmGcm
                                .Where(i => i.GcmType.Contains("GPT"))
                                .Where(i => i.GcmCode.Contains("LG") || i.GcmCode.Contains("AG") || i.GcmCode.Contains("DE"))
                                .Where(i => i.IsActive.Contains("A"))
                                .Where(i => i.GcmDesc.StartsWith(gcmdescn))
                                .OrderBy(i => i.GcmDesc)
                                .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc })
                                .ToListAsync();

                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Gcm with Name = {0}", gcmdescn)),
                            ReasonPhrase = "GCM  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("CreditGroup")]
        public async Task<ActionResult<List<AcGroup>>> GetCreditGroup(string grpname,string grptype)
        {
            try
            {
                if (grpname == null && grptype == null)
                {
                    var tmgcm = await _context.TmAcgroup                                
                                .Where(i => i.UsageType.Contains("UB") || i.UsageType.Contains("UN"))
                                .Where(i => i.IsActive.Contains("A"))
                                .OrderBy(i => i.GrpName)
                                .Select(i => new AcGroup { GrpCode = i.GrpCode, GrpName = i.GrpName})
                                .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No GRP with Name = {0}", grpname)),
                            ReasonPhrase = "GRP  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                else if (grpname == null && grptype == null)
                {
                    var tmgcm = await _context.TmAcgroup
                                .Where(i => i.GrpName.StartsWith(grpname))
                                .Where( i => i.GrpType.StartsWith(grptype))
                                .Where(i => i.UsageType.Contains("UB") || i.UsageType.Contains("UN"))
                                .Where(i => i.IsActive.Contains("A"))
                                .OrderBy(i => i.GrpName)
                                .Select(i => new AcGroup { GrpCode = i.GrpCode, GrpName = i.GrpName })
                                .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No GRP with Name = {0}", grpname)),
                            ReasonPhrase = "GRP  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("DebitGroup")]
        public async Task<ActionResult<List<AcGroup>>> GetDebitGroup(string grpname, string grptype)
        {
            try
            {
                if (grpname == null && grptype == null)
                {
                    var tmgcm = await _context.TmAcgroup
                                .Where(i => i.UsageType.Contains("UB") || i.UsageType.Contains("UN"))
                                .Where(i => i.IsActive.Contains("A"))
                                .OrderBy(i => i.GrpName)
                                .Select(i => new AcGroup { GrpCode = i.GrpCode, GrpName = i.GrpName })
                                .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No GRP with Name = {0}", grpname)),
                            ReasonPhrase = "GRP  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                else if (grpname == null && grptype == null)
                {
                    var tmgcm = await _context.TmAcgroup
                                .Where(i => i.GrpName.StartsWith(grpname))
                                .Where(i => i.GrpType.StartsWith(grptype))
                                .Where(i => i.UsageType.Contains("UB") || i.UsageType.Contains("UN"))
                                .Where(i => i.IsActive.Contains("A"))
                                .OrderBy(i => i.GrpName)
                                .Select(i => new AcGroup { GrpCode = i.GrpCode, GrpName = i.GrpName })
                                .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No GRP with Name = {0}", grpname)),
                            ReasonPhrase = "GRP  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("ConsCreditGroup")]
        public async Task<ActionResult<List<AcGroup>>> GetConsCreditGroup(string grpname, string grptype)
        {
            try
            {
                if (grpname == null && grptype == null)
                {
                    var tmgcm = await _context.TmAcgroup
                                .Where(i => i.UsageType.Contains("UB") || i.UsageType.Contains("UN"))
                                .Where(i => i.IsActive.Contains("A"))
                                .OrderBy(i => i.GrpName)
                                .Select(i => new AcGroup { GrpCode = i.GrpCode, GrpName = i.GrpName })
                                .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No GRP with Name = {0}", grpname)),
                            ReasonPhrase = "GRP  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                else if (grpname == null && grptype == null)
                {
                    var tmgcm = await _context.TmAcgroup
                                .Where(i => i.GrpName.StartsWith(grpname))
                                .Where(i => i.GrpType.StartsWith(grptype))
                                .Where(i => i.UsageType.Contains("UB") || i.UsageType.Contains("UN"))
                                .Where(i => i.IsActive.Contains("A"))
                                .OrderBy(i => i.GrpName)
                                .Select(i => new AcGroup { GrpCode = i.GrpCode, GrpName = i.GrpName })
                                .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No GRP with Name = {0}", grpname)),
                            ReasonPhrase = "GRP  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("ConsDebitGroup")]
        public async Task<ActionResult<List<AcGroup>>> GetConsDebitGroup(string grpname, string grptype)
        {
            try
            {
                if (grpname == null && grptype == null)
                {
                    var tmgcm = await _context.TmAcgroup
                                .Where(i => i.UsageType.Contains("UB") || i.UsageType.Contains("UN"))
                                .Where(i => i.IsActive.Contains("A"))
                                .OrderBy(i => i.GrpName)
                                .Select(i => new AcGroup { GrpCode = i.GrpCode, GrpName = i.GrpName })
                                .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No GRP with Name = {0}", grpname)),
                            ReasonPhrase = "GRP  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                else if (grpname == null && grptype == null)
                {
                    var tmgcm = await _context.TmAcgroup
                                .Where(i => i.GrpName.StartsWith(grpname))
                                .Where(i => i.GrpType.StartsWith(grptype))
                                .Where(i => i.UsageType.Contains("UB") || i.UsageType.Contains("UN"))
                                .Where(i => i.IsActive.Contains("A"))
                                .OrderBy(i => i.GrpName)
                                .Select(i => new AcGroup { GrpCode = i.GrpCode, GrpName = i.GrpName })
                                .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No GRP with Name = {0}", grpname)),
                            ReasonPhrase = "GRP  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("ControlAcType")]
        public async Task<ActionResult<List<Account>>> GetControlAcType(string accountname)
        {
            try
            {
                if (accountname == null)
                {
                    var tmaccount = await _context.TmAccounts
                                .Where(i => i.AcType.Contains("CL") )
                                .Where(i => i.IsActive.Contains("A"))
                                .OrderBy(i => i.AccountName)
                                .Select(i => new Account { AccountCode = i.AccountCode, AccountName = i.AccountName })
                                .ToListAsync();


                    if (tmaccount.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Account with Name = {0}", accountname)),
                            ReasonPhrase = "Account  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmaccount;
                }
                else if (accountname != null)
                {
                    var tmaccount = await _context.TmAccounts 
                                   .Where(i => i.AccountName.StartsWith(accountname))
                                   .Where(i => i.AcType.Contains("CL"))
                                   .Where(i => i.IsActive.Contains("A"))
                                   .OrderBy(i => i.AccountName)
                                   .Select(i => new Account { AccountCode = i.AccountCode, AccountName = i.AccountName })
                                   .ToListAsync();


                    if (tmaccount.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Account with Name = {0}", accountname)),
                            ReasonPhrase = "Account  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmaccount;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("SubLegCategory")]
        public async Task<ActionResult<List<Gcm>>> GetSubLegCategory(string gcmdescn, string gcmtype)
        {
            try
            {
                if (gcmdescn == null && gcmtype == null)
                {
                    var tmgcm = await _context.TmGcm
                                     .Where(i => i.IsActive.Contains("A"))
                                     .OrderBy(i => i.GcmDesc)
                                     .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc })
                                     .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Gcm with Name = {0}", gcmdescn)),
                            ReasonPhrase = "GCM  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                else if (gcmdescn != null && gcmtype != null)
                {
                    var tmgcm = await _context.TmGcm
                               .Where(i => i.GcmDesc.StartsWith(gcmdescn))
                               .Where(i => i.GcmType.StartsWith(gcmtype))
                               .Where(i => i.IsActive.Contains("A"))
                               .OrderBy(i => i.GcmDesc)
                               .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc })
                               .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Gcm with Name = {0}", gcmdescn)),
                            ReasonPhrase = "GCM  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Currency")]
        public async Task<ActionResult<List<currency>>> GetCurrency()
        {
            try
            {                
                    var tmgcm = await _context.TmCurrency
                                     .Where(i => i.IsActive.Contains("A"))
                                     .OrderBy(i => i.CurrencyDesc)
                                     .Select(i => new currency { Currency= i.Currency, CurrencyDesc = i.CurrencyDesc })
                                     .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Currency Found")),
                            ReasonPhrase = "Currency  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
              
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Payment")]
        public async Task<ActionResult<List<Gcm>>> GetPayment(string gcmdescn, string gcmtype)
        {
            try
            {
                if (gcmdescn == null && gcmtype == null)
                {
                    var tmgcm = await _context.TmGcm
                                     .Where(i => i.IsActive.Contains("A"))
                                     .OrderBy(i => i.GcmDesc)
                                     .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc })
                                     .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Gcm with Name = {0}", gcmdescn)),
                            ReasonPhrase = "GCM  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                else if (gcmdescn != null && gcmtype != null)
                {
                    var tmgcm = await _context.TmGcm
                               .Where(i => i.GcmDesc.StartsWith(gcmdescn))
                               .Where(i => i.GcmType.StartsWith(gcmtype))
                               .Where(i => i.IsActive.Contains("A"))
                               .OrderBy(i => i.GcmDesc)
                               .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc })
                               .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Gcm with Name = {0}", gcmdescn)),
                            ReasonPhrase = "GCM  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("BillAllocate")]
        public async Task<ActionResult<List<Gcm>>> GetBillAllocate(string gcmdescn, string gcmtype)
        {
            try
            {
                if (gcmdescn == null && gcmtype == null)
                {
                    var tmgcm = await _context.TmGcm
                                     .Where(i => i.IsActive.Contains("A"))
                                     .OrderBy(i => i.GcmDesc)
                                     .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc })
                                     .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Gcm with Name = {0}", gcmdescn)),
                            ReasonPhrase = "GCM  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                else if (gcmdescn != null && gcmtype != null)
                {
                    var tmgcm = await _context.TmGcm
                               .Where(i => i.GcmDesc.StartsWith(gcmdescn))
                               .Where(i => i.GcmType.StartsWith(gcmtype))
                               .Where(i => i.IsActive.Contains("A"))
                               .OrderBy(i => i.GcmDesc)
                               .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc })
                               .ToListAsync();


                    if (tmgcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Gcm with Name = {0}", gcmdescn)),
                            ReasonPhrase = "GCM  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmgcm;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Country")]
        public async Task<ActionResult<List<Country>>> GetCountry(string countryname)
        {
            try
            {
                if (countryname == null )
                {
                    var tmcountry = await _context.TmPlace
                                     .Where(i => i.PlaceType.Contains("O"))
                                      .OrderBy(i => i.PlaceName)
                                     .Select(i => new Country { CountryCode = i.CountryCode, CountryName = i.CountryName })
                                     .ToListAsync();


                    if (tmcountry.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Country with Name = {0}",countryname)),
                            ReasonPhrase = "Country  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmcountry;
                }
                else if (countryname == null)
                {
                    var tmcountry = await _context.TmPlace
                                    .Where(i => i.CountryName.StartsWith(countryname))
                                    .Where(i => i.PlaceType.Contains("O"))
                                    .OrderBy(i => i.PlaceName)
                                    .Select(i => new Country { CountryCode = i.CountryCode, CountryName = i.CountryName })
                                    .ToListAsync();


                    if (tmcountry.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Country with Name = {0}", countryname)),
                            ReasonPhrase = "Country  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmcountry;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("State")]
        public async Task<ActionResult<List<State>>> GetState(string statename,string countrycode)
        {
            try
            {
                if (statename == null && countrycode== null )
                {
                    var tmstate = await _context.TmPlace
                                  .Where(i => i.PlaceType.Contains("S"))
                                  .OrderBy(i => i.PlaceName)
                                  .Select(i => new State {StateCode = i.StateCode, StateName= i.StateName })
                                  .ToListAsync();


                    if (tmstate.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No State with Name = {0}", statename)),
                            ReasonPhrase = "State  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmstate;
                }
                else if (statename != null && countrycode != null)
                {
                    var tmstate = await _context.TmPlace
                                  .Where(i => i.StateName.StartsWith(statename))
                                  .Where(i => i.CountryCode.StartsWith(countrycode))
                                  .Where(i => i.PlaceType.Contains("S"))
                                  .OrderBy(i => i.PlaceName)
                                  .Select(i => new State { StateCode = i.StateCode, StateName = i.StateName })
                                  .ToListAsync();


                    if (tmstate.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No State with Name = {0}", statename)),
                            ReasonPhrase = "State  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmstate;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("District")]
        public async Task<ActionResult<List<District>>> GetDistrict(string districtname, string statecode)
        {
            try
            {
                if (districtname == null && statecode == null)
                {
                    var tmdistrict = await _context.TmPlace
                                     .Where(i => i.PlaceType.Contains("D"))
                                     .OrderBy(i => i.PlaceName)
                                     .Select(i => new District {DistrictCode = i.DistCode, DistrictName = i.DistName })
                                     .ToListAsync();


                    if (tmdistrict.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No District with Name = {0}", districtname)),
                            ReasonPhrase = "District  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmdistrict;
                }
                else if (districtname == null && statecode == null)
                {
                    var tmdistrict = await _context.TmPlace
                                     .Where( i => i.DistName.StartsWith(districtname))
                                     .Where(i => i.StateCode.StartsWith(statecode))
                                     .Where(i => i.PlaceType.Contains("D"))
                                     .OrderBy(i => i.PlaceName)
                                     .Select(i => new District { DistrictCode = i.DistCode, DistrictName = i.DistName })
                                     .ToListAsync();


                    if (tmdistrict.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No District with Name = {0}", districtname)),
                            ReasonPhrase = "District  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmdistrict;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Taluk")]
        public async Task<ActionResult<List<Taluk>>> GetTaluk(string talukname, string districtcode)
        {
            try
            {
                if (talukname == null && districtcode == null)
                {
                    var tmtaluk = await _context.TmPlace
                                  .Where(i => i.PlaceType.Contains("M"))
                                  .OrderBy(i => i.PlaceName)
                                  .Select(i => new Taluk { TalukCode = i.TalukCode, TalukName = i.TalukName })
                                  .ToListAsync();


                    if (tmtaluk.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Taulk with Name = {0}", talukname)),
                            ReasonPhrase = "District  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmtaluk;
                }
                else if (talukname != null && districtcode != null)
                {
                    var tmtaluk = await _context.TmPlace
                                  .Where(i => i.TalukName.StartsWith(talukname))
                                  .Where(i => i.PlaceType.Contains("M"))
                                  .OrderBy(i => i.PlaceName)
                                  .Select(i => new Taluk { TalukCode = i.TalukCode, TalukName = i.TalukName })
                                  .ToListAsync();


                    if (tmtaluk.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Taulk with Name = {0}", talukname)),
                            ReasonPhrase = "Taluk  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmtaluk;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("City")]
        public async Task<ActionResult<List<City>>> GetCity(string cityname, string talukcode)
        {
            try
            {
                if (cityname == null && talukcode == null)
                {
                    var tmcity = await _context.TmPlace
                                 .Where(i => i.PlaceType.Contains("T"))
                                 .OrderBy(i => i.PlaceName)
                                 .Select(i => new City { CityName = i.CityName, CityCode = i.CityCode })
                                 .ToListAsync();


                    if (tmcity.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No City with Name = {0}", cityname)),
                            ReasonPhrase = "City Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmcity;
                }
                else if (cityname != null && talukcode != null)
                {
                    var tmcity = await _context.TmPlace
                                 .Where(i => i.CountryName.StartsWith(cityname))
                                 .Where(i => i.TalukCode.StartsWith(talukcode))
                                 .Where(i => i.PlaceType.Contains("T"))
                                 .OrderBy(i => i.PlaceName)
                                 .Select(i => new City { CityName = i.CityName, CityCode = i.CityCode })
                                 .ToListAsync();


                    if (tmcity.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No City with Name = {0}", cityname)),
                            ReasonPhrase = "City Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmcity;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Pincode")]
        public async Task<ActionResult<List<Place>>> GetPincode(string placename, string citycode)
        {
            try
            {
                if (placename == null && citycode == null)
                {
                    var tmplace = await _context.TmPlace
                                     .Where(i => i.PlaceType.Contains("Z"))
                                     .OrderBy(i => i.PlaceName)
                                     .Select(i => new Place { PinCode = i.PinCode, PlaceName = i.PlaceName })
                                     .ToListAsync();


                    if (tmplace.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Place with Name = {0}", placename)),
                            ReasonPhrase = "Place Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmplace;
                }
                else if (placename != null && citycode != null)
                {
                    var tmplace = await _context.TmPlace
                                 .Where(i => i.PlaceName.StartsWith(placename))
                                 .Where(i => i.CityCode.StartsWith(citycode))
                                 .Where(i => i.PlaceType.Contains("Z"))
                                 .OrderBy(i => i.PlaceName)
                                 .Select(i => new Place { PinCode = i.PinCode, PlaceName = i.PlaceName })
                                 .ToListAsync();


                    if (tmplace.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Place with Name = {0}", placename)),
                            ReasonPhrase = "Place Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmplace;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Analysis1")]
        public async Task<ActionResult<List<Analysis>>> GetAnalysis1(string analname)
        {
            try
            {
                if (analname == null)
                {
                    var tmanalysis = await _context.TmAnalysis
                                     .Where(i => i.IsActive.Contains("A"))
                                     .Where(i => i.Category.Contains("1"))
                                     .OrderBy(i => i.AnalName)
                                     .Select(i => new Analysis { AnalCode = i.AnalCode, AnalName= i.AnalName })
                                     .ToListAsync();


                    if (tmanalysis.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Anal with Name = {0}", analname)),
                            ReasonPhrase = "AnalName Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmanalysis;
                }
                else if (analname != null)
                {
                    var tmanalysis = await _context.TmAnalysis
                                     .Where(i => i.AnalName.StartsWith(analname))
                                     .Where(i => i.IsActive.Contains("A"))
                                     .Where(i => i.Category.Contains("1"))
                                     .OrderBy(i => i.AnalName)
                                     .Select(i => new Analysis { AnalCode = i.AnalCode, AnalName = i.AnalName })
                                     .ToListAsync();


                    if (tmanalysis.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Anal with Name = {0}", analname)),
                            ReasonPhrase = "AnalName Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmanalysis;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Analysis2")]
        public async Task<ActionResult<List<Analysis>>> GetAnalysis2(string analname)
        {
            try
            {
                if (analname == null)
                {
                    var tmanalysis = await _context.TmAnalysis
                                     .Where(i => i.IsActive.Contains("A"))
                                     .Where(i => i.Category.Contains("2"))
                                     .OrderBy(i => i.AnalName)
                                     .Select(i => new Analysis { AnalCode = i.AnalCode, AnalName = i.AnalName })
                                     .ToListAsync();


                    if (tmanalysis.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Anal with Name = {0}", analname)),
                            ReasonPhrase = "AnalName Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmanalysis;
                }
                else if (analname != null)
                {
                    var tmanalysis = await _context.TmAnalysis
                                     .Where(i => i.AnalName.StartsWith(analname))
                                     .Where(i => i.IsActive.Contains("A"))
                                     .Where(i => i.Category.Contains("2"))
                                     .OrderBy(i => i.AnalName)
                                     .Select(i => new Analysis { AnalCode = i.AnalCode, AnalName = i.AnalName })
                                     .ToListAsync();


                    if (tmanalysis.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Anal with Name = {0}", analname)),
                            ReasonPhrase = "AnalName Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmanalysis;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Analysis3")]
        public async Task<ActionResult<List<Analysis>>> GetAnalysis3(string analname)
        {
            try
            {
                if (analname == null)
                {
                    var tmanalysis = await _context.TmAnalysis
                                     .Where(i => i.IsActive.Contains("A"))
                                     .Where(i => i.Category.Contains("3"))
                                     .OrderBy(i => i.AnalName)
                                     .Select(i => new Analysis { AnalCode = i.AnalCode, AnalName = i.AnalName })
                                     .ToListAsync();


                    if (tmanalysis.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Anal with Name = {0}", analname)),
                            ReasonPhrase = "AnalName Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmanalysis;
                }
                else if (analname != null)
                {
                    var tmanalysis = await _context.TmAnalysis
                                     .Where(i => i.AnalName.StartsWith(analname))
                                     .Where(i => i.IsActive.Contains("A"))
                                     .Where(i => i.Category.Contains("3"))
                                     .OrderBy(i => i.AnalName)
                                     .Select(i => new Analysis { AnalCode = i.AnalCode, AnalName = i.AnalName })
                                     .ToListAsync();


                    if (tmanalysis.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Anal with Name = {0}", analname)),
                            ReasonPhrase = "AnalName Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmanalysis;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Analysis4")]
        public async Task<ActionResult<List<Analysis>>> GetAnalysis4(string analname)
        {
            try
            {
                if (analname == null)
                {
                    var tmanalysis = await _context.TmAnalysis
                                     .Where(i => i.IsActive.Contains("A"))
                                     .Where(i => i.Category.Contains("4"))
                                     .OrderBy(i => i.AnalName)
                                     .Select(i => new Analysis { AnalCode = i.AnalCode, AnalName = i.AnalName })
                                     .ToListAsync();


                    if (tmanalysis.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Anal with Name = {0}", analname)),
                            ReasonPhrase = "AnalName Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmanalysis;
                }
                else if (analname != null)
                {
                    var tmanalysis = await _context.TmAnalysis
                                     .Where(i => i.AnalName.StartsWith(analname))
                                     .Where(i => i.IsActive.Contains("A"))
                                     .Where(i => i.Category.Contains("4"))
                                     .OrderBy(i => i.AnalName)
                                     .Select(i => new Analysis { AnalCode = i.AnalCode, AnalName = i.AnalName })
                                     .ToListAsync();


                    if (tmanalysis.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Anal with Name = {0}", analname)),
                            ReasonPhrase = "AnalName Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmanalysis;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Analysis5")]
        public async Task<ActionResult<List<Analysis>>> GetAnalysis5(string analname)
        {
            try
            {
                if (analname == null)
                {
                    var tmanalysis = await _context.TmAnalysis
                                     .Where(i => i.IsActive.Contains("A"))
                                     .Where(i => i.Category.Contains("5"))
                                     .OrderBy(i => i.AnalName)
                                     .Select(i => new Analysis { AnalCode = i.AnalCode, AnalName = i.AnalName })
                                     .ToListAsync();


                    if (tmanalysis.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Anal with Name = {0}", analname)),
                            ReasonPhrase = "AnalName Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmanalysis;
                }
                else if (analname != null)
                {
                    var tmanalysis = await _context.TmAnalysis
                                     .Where(i => i.AnalName.StartsWith(analname))
                                     .Where(i => i.IsActive.Contains("A"))
                                     .Where(i => i.Category.Contains("5"))
                                     .OrderBy(i => i.AnalName)
                                     .Select(i => new Analysis { AnalCode = i.AnalCode, AnalName = i.AnalName })
                                     .ToListAsync();


                    if (tmanalysis.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Anal with Name = {0}", analname)),
                            ReasonPhrase = "AnalName Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmanalysis;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("Area")]
        public async Task<ActionResult<List<Gcm>>> GetArea(string gcmtype)
        {
            try
            {
                if (gcmtype == null)
                {
                    var tmarea = await _context.TmGcm
                                     .Where(i => i.IsActive.Contains("A"))                                     
                                     .OrderBy(i => i.GcmDesc)
                                     .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc })
                                     .ToListAsync();


                    if (tmarea.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Area Not Found ")),
                            ReasonPhrase = "Area Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmarea;
                }
                else if (gcmtype != null)
                {
                    var tmarea = await _context.TmGcm
                                 .Where(i => i.GcmType.StartsWith(gcmtype))
                                 .Where(i => i.IsActive.Contains("A"))
                                 .OrderBy(i => i.GcmDesc)
                                 .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc })
                                 .ToListAsync();


                    if (tmarea.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Area Not Found ")),
                            ReasonPhrase = "Area Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmarea;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }


        [HttpPost("Bank")]
        public async Task<ActionResult<List<Gcm>>> GetBank(string gcmdesc,string gcmtype)
        {
            try
            {
                if (gcmdesc == null && gcmtype == null)
                {
                    var tmbank = await _context.TmGcm
                                     .Where(i => i.IsActive.Contains("A"))
                                     .OrderBy(i => i.GcmDesc)
                                     .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc })
                                     .ToListAsync();


                    if (tmbank.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Bank with Name = {0}", gcmdesc)),
                            ReasonPhrase = "Bank  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmbank;
                }
                else if (gcmdesc!= null && gcmtype != null)
                {
                    var tmarea = await _context.TmGcm
                                 .Where( i => i.GcmDesc.StartsWith(gcmdesc))
                                 .Where(i => i.GcmType.StartsWith(gcmtype))
                                 .Where(i => i.IsActive.Contains("A"))
                                 .OrderBy(i => i.GcmDesc)
                                 .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc })
                                 .ToListAsync();


                    if (tmarea.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Bank with Name = {0}", gcmdesc)),
                            ReasonPhrase = "Bank Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmarea;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("PayableAt")]
        public async Task<ActionResult<List<Place>>> GetPayableAt(string placename)
        {
            try
            {
                if (placename == null )
                {
                    var tmpayabaleat = await _context.TmPlace
                                     .Where(i => i.PlaceType.Contains("T"))
                                     .OrderBy(i => i.PlaceName)
                                     .Select(i => new Place {PlaceCode = i.PlaceCode, PlaceName = i.PlaceName })
                                     .ToListAsync();


                    if (tmpayabaleat.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Place with Name = {0}", placename)),
                            ReasonPhrase = "PlaceName Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmpayabaleat;
                }
                else if (placename == null)
                {
                    var tmpayabaleat = await _context.TmPlace
                                       .Where(i => i.PlaceName.StartsWith(placename))
                                       .Where(i => i.PlaceType.Contains("T"))
                                       .OrderBy(i => i.PlaceName)
                                       .Select(i => new Place { PlaceCode = i.PlaceCode, PlaceName = i.PlaceName })
                                       .ToListAsync();


                    if (tmpayabaleat.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Place with Name = {0}", placename)),
                            ReasonPhrase = "PlaceName Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmpayabaleat;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("PartyRegion")]
        public async Task<ActionResult<List<Gcm>>> GetPartyRegion()
        {
            try
            {
                    var tmpartyregion = await _context.TmGcm
                                     .Where(i => i.IsActive.Contains("A"))
                                     .Where(i => i.GcmType.StartsWith("PST"))
                                     .OrderBy(i => i.GcmDesc)
                                     .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc })
                                     .ToListAsync();


                    if (tmpartyregion.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No PartyRegion Not Found")),
                            ReasonPhrase = "Party Region Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmpartyregion;
               
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("ViewData")]
        public async Task<ActionResult<List<AccountView>>> ViewAccountDetails(string AccountCode)
        {
            try
            {
                var tmItem = await _context.TmAccounts
                             .Join(_context.TmAccountsdetail, Ac => Ac.AccountCode, Acd => Acd.AccountCode, (Ac, Acd) => new { TmAccounts = Ac, TmAccountsdetail = Acd })
                            .Where(i => i.TmAccounts.AccountCode == AccountCode)
                            .Select(i => new AccountView 
                            {
                                AccountCode = i.TmAccounts.AccountCode,
                                AccountName = i.TmAccounts.AccountName,
                                GrpCodeCr = i.TmAccounts.GrpCodeCr,
                                GrpCodeDr = i.TmAccounts.GrpCodeDr,
                                AcType = i.TmAccounts.AcType,
                                GrpType = i.TmAccounts.GrpType,
                                ControlAc = i.TmAccounts.ControlAc,
                                BillAlloc = i.TmAccounts.BillAlloc,
                                AnalCode1 = i.TmAccounts.AnalCode1,
                                AnalCode2 = i.TmAccounts.AnalCode2,
                                AnalCode3 = i.TmAccounts.AnalCode3,
                                AnalCode4 = i.TmAccounts.AnalCode4,
                                AnalCode5 = i.TmAccounts.AnalCode5,
                                CcReq = i.TmAccounts.CcReq,
                                SubLedCategory = i.TmAccounts.SubLedCategory,
                                Currency = i.TmAccounts.Currency,
                                TransControl = i.TmAccounts.TransControl,
                                CreditDays = i.TmAccounts.CreditDays,
                                ExpPymt = i.TmAccounts.ExpPymt,
                                AreaCode = i.TmAccounts.AreaCode,
                                ConsGrpCodeCr = i.TmAccounts.ConsGrpCodeCr,
                                ConsGrpCodeDr = i.TmAccounts.ConsGrpCodeDr,
                                IsActive = i.TmAccounts.IsActive,
                                BeAcOnSl = i.TmAccounts.BeAcOnSl,
                                BeAcOnGl = i.TmAccounts.BeAcOnGl,
                                BeAcOnDesc = i.TmAccounts.BeAcOnDesc,
                                TdsNature = i.TmAccounts.TdsNature,
                                PayFavour = i.TmAccountsdetail.PayFavour,
                                PartyCode = i.TmAccounts.PartyCode,
                                PartyName = i.TmAccounts.PartyName,
                                Address1 = i.TmAccountsdetail.Address1,
                                Address2 = i.TmAccountsdetail.Address2,
                                Address3 = i.TmAccountsdetail.Address3,
                                LandMark = i.TmAccountsdetail.LandMark,
                                PinCode = i.TmAccountsdetail.PinCode,
                                CityCode = i.TmAccountsdetail.CityCode,
                                TalukCode = i.TmAccountsdetail.TalukCode,
                                DistCode = i.TmAccountsdetail.DistCode,
                                StateCode = i.TmAccountsdetail.StateCode,
                                CountryCode = i.TmAccountsdetail.CountryCode,
                                PhoneNo = i.TmAccountsdetail.PhoneNo,
                                FaxNo = i.TmAccountsdetail.FaxNo,
                                MobileNo = i.TmAccountsdetail.MobileNo,
                                MailId = i.TmAccountsdetail.MailId,
                                Website = i.TmAccountsdetail.Website,
                                ContactPer = i.TmAccountsdetail.ContactPer,
                                AcRefNo = i.TmAccountsdetail.AcRefNo,
                                CstNo = i.TmAccountsdetail.CstNo,
                                CstDate = i.TmAccountsdetail.CstDate.ToString(),
                                VatNo = i.TmAccountsdetail.VatNo,
                                VatDate = i.TmAccountsdetail.VatDate.ToString(),
                                PanNo = i.TmAccountsdetail.PanNo,
                                PanDate = i.TmAccountsdetail.PanDate.ToString(),
                                BankCode = i.TmAccountsdetail.BankCode,
                                BankAdd1 = i.TmAccountsdetail.BankAdd1,
                                BankAdd2 = i.TmAccountsdetail.BankAdd2,
                                BankAdd3 = i.TmAccountsdetail.BankAdd3,
                                PayableAt = i.TmAccountsdetail.PayableAt,
                                GstDate = i.TmAccountsdetail.GstDate.ToString(),
                                GstNo = i.TmAccountsdetail.GstNo                                
                             }).ToListAsync();
                if (tmItem.Count == 0)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("No Account with ID = {0}", AccountCode)),
                        ReasonPhrase = "Account Not Found"
                    };
                    throw new System.Web.Http.HttpResponseException(resp);
                }
                return tmItem;

            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        //[HttpPost("SaveUpdate")]
        //public async Task<ActionResult<Response>> PostTmAccountsDetail(string accountcode, TmAccounts tmAccounts,TmAccountsdetail tmAccountsdetail)
        //{
        //    if (accountcode != tmAccounts.AccountCode && accountcode != tmAccountsdetail.AccountCode)
        //    {
        //        _context.TmAccounts.Add(tmAccounts);
        //        _context.TmAccountsdetail.Add(tmAccountsdetail);
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException)
        //        {
        //            if (TmAccountsExists(tmAccounts.AccountCode) && TmAccountsdetailExists(tmAccountsdetail.AccountCode))
        //            {
        //                return new Response { Status = "Conflict", Message = "Record Already Exist" };
        //            }
        //        }
        //        return new Response { Status = "SUCCESSFULL", Message = "SAVED SUCCESSFULLY" };

        //    }
        //    else if (accountcode == tmAccounts.AccountCode && accountcode == tmAccountsdetail.AccountCode)
        //    {
        //        _context.Entry(tmAccounts).State = EntityState.Modified;
        //        _context.Entry(tmAccountsdetail).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TmAccountsExists(accountcode) && !TmAccountsdetailExists(accountcode))
        //            {

        //                return new Response { Status = "NotFound", Message = "Record Not Found" };
        //            }
        //            /* else
        //             {
        //                 throw;
        //             }*/
        //        }

        //        return new Response { Status = "Updated", Message = "Record Updated Sucessfull" };
        //    }
        //    return null;
        //}
        // PUT: api/TmAccounts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTmAccounts(string id, TmAccounts tmAccounts)
        {
            if (id != tmAccounts.AccountCode)
            {
                return BadRequest();
            }

            _context.Entry(tmAccounts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TmAccountsExists(id))
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

        // POST: api/TmAccounts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TmAccounts>> PostTmAccounts(TmAccounts tmAccounts)
        {
            _context.TmAccounts.Add(tmAccounts);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TmAccountsExists(tmAccounts.AccountCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTmAccounts", new { id = tmAccounts.AccountCode }, tmAccounts);
        }

        // DELETE: api/TmAccounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TmAccounts>> DeleteTmAccounts(string id)
        {
            var tmAccounts = await _context.TmAccounts.FindAsync(id);
            if (tmAccounts == null)
            {
                return NotFound();
            }

            _context.TmAccounts.Remove(tmAccounts);
            await _context.SaveChangesAsync();

            return tmAccounts;
        }

        private bool TmAccountsExists(string id)
        {
            return _context.TmAccounts.Any(e => e.AccountCode == id);
        }

        private bool TmAccountsdetailExists(string id)
        {
            return _context.TmAccountsdetail.Any(e => e.AccountCode == id);
        }
    }
}
