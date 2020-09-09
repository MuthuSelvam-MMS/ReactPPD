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
                var tmAccounts = await _context.TmAccounts
                                .Join(_context.TmAccountsdetail, A => A.AccountCode, B => B.AccountCode, (A, B) => new { TmAccounts = A, TmAccountsdetail = B })
                               .Join(_context.TmGcm, A => A.TmAccounts.GrpType,C => C.GcmCode, (A, C) => new { TmAccounts = A, TmGcm = C})
                               .Join(_context.TmPlace, B => B.TmAccounts.TmAccountsdetail.PinCode, D => D.PlaceCode, (B,D) => new { TmAccountsdetail = B, TmPlace = D})
                               .Join(_context.TmPlace, B => B.TmAccountsdetail.TmAccounts.TmAccountsdetail.PayableAt, E => E.PlaceCode,(B,E) => new {TmAccountsdetail = B, TmPlace = E })
                               .Join(_context.TmGcm, B => B.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.BankCode,F => F.GcmCode, (B,F) => new {TmAccountsdetail = B, TmGcm = F })
                               .Join(_context.TmGcm, A => A.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.AreaCode, G => G.GcmCode, (A,G) => new {TmAccounts = A, TmGcm = G})
                               .Join(_context.TmGcm, A => A.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.ExpPymt, H => H.GcmCode,(A,H) => new {TmAccounts = A,TmGcm = H })
                               .Join(_context.TmGcm, A => A.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.SubLedCategory, I => I.GcmCode,(A,I) => new {TmAccounts = A, TmGcm = I})
                               .Join(_context.TmGcm, A => A.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.BillAlloc, J => J.GcmCode,(A,J) => new {TmAccounts = A, TmGcm = J })
                               .Join(_context.TmGcm, A => A.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.AcType, K => K.GcmCode,(A,K) => new {TmAccounts = A, TmGcm = K})
                               .Join(_context.TmAnalysis, A => A.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.AnalCode1, A1 => A1.AnalCode,(A,A1) => new {TmAccounts = A ,TmAnalysis = A1})
                               .Join(_context.TmAnalysis, A => A.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.AnalCode2, A2 => A2.AnalCode, (A, A2) => new { TmAccounts = A, TmAnalysis = A2})
                               .Join(_context.TmAnalysis, A => A.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.AnalCode3, A3 => A3.AnalCode, (A, A3) => new { TmAccounts = A, TmAnalysis = A3 })
                               .Join(_context.TmAnalysis, A => A.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.AnalCode3, A4 => A4.AnalCode, (A, A4) => new { TmAccounts = A, TmAnalysis = A4 })
                               .Join(_context.TmAnalysis, A => A.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.AnalCode3, A5 => A5.AnalCode, (A, A5) => new { TmAccounts = A, TmAnalysis = A5 })
                               .Join(_context.TmCurrency, A => A.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.Currency, L => L.Currency,(A,L) => new {TmAccounts = A, TmCurrency = L })
                               .Join(_context.TmAccounts, A => A.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.ControlAc, M => M.AccountCode,(A,M) => new {TmAccounts = A, M})
                               .Join(_context.TmAcgroup , A => A.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.ConsGrpCodeDr, N => N.GrpCode, (A,N) => new {TmAccounts = A ,TmAcgroup = N })
                               .Join(_context.TmAcgroup, A => A.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.ConsGrpCodeDr, O => O.GrpCode, (A, O) => new { TmAccounts = A, TmAcgroup = O })
                               .Join(_context.TmAcgroup, A => A.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.ConsGrpCodeDr, P => P.GrpCode, (A, P) => new { TmAccounts = A, TmAcgroup = P })
                               .Join(_context.TmAcgroup, A => A.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.ConsGrpCodeDr, Q => Q.GrpCode, (A, Q) => new { TmAccounts = A, TmAcgroup = Q })
                               .Join(_context.TmTdsnature, A => A.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.TdsNature , S => S.NatureCode, (A,S) => new {TmAccounts = A, TmTdsnature = S })
                               .Where(i => i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmGcm.GcmType.StartsWith("GPT"))
                               .Where(i => i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmGcm.GcmType.StartsWith("ARM"))
                               .Where(i => i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmGcm.GcmType.StartsWith("PMT"))
                               .Where(i => i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmGcm.GcmType.StartsWith("SLT"))
                               .Where(i => i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmGcm.GcmType.StartsWith("BAT"))
                               .Where(i => i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmGcm.GcmType.StartsWith("ACT"))
                               .Where(i => i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.AccountCode == AccountCode)
                               .Select(i => new AccountView
                                 {
                                     AccountCode = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.AccountCode,
                                     AccountName = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.AccountName,
                                     GrpCodeDr = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.GrpCodeDr,
                                     GrpCodeDrName = i.TmAccounts.TmAccounts.TmAcgroup.GrpName,
                                     GrpCodeCr = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.GrpCodeCr,
                                     AcType = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.AcType,
                                     AcTypeDesc = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmGcm.GcmType,
                                   GrpType = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.GrpType,
                                   GrpTypeName = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmGcm.GcmDesc,
                                   ControlAc = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.ControlAc,
                                   BillAlloc = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.BillAlloc,
                                   AnalCode1 = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.AnalCode1,
                                   AnalName1 = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAnalysis.AnalName,
                                   AnalCode2 = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.AnalCode2,
                                   AnalName2 = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAnalysis.AnalName,
                                   AnalCode3 = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.AnalCode3,
                                   AnalName3 = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAnalysis.AnalName,
                                   AnalCode4 = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.AnalCode4,
                                   AnalName4 = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAnalysis.AnalName,
                                   AnalCode5 = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.AnalCode5,
                                   AnalName5 = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAnalysis.AnalName,
                                   CcReq = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.CcReq,
                                   SubLedCategory = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.SubLedCategory,
                                   SubLedCategoryName = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmGcm.GcmDesc,
                                   Currency = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.Currency,
                                   CurrencyDesc = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmCurrency.CurrencyDesc,
                                   TransControl = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.TransControl,
                                   CreditDays = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.CreditDays,
                                   ExpPymt = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.ExpPymt,
                                   AreaCode = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.AreaCode,
                                   AreaDesc = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmGcm.GcmDesc,
                                   ConsGrpCodeDr = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.ConsGrpCodeDr,
                                   ConsGrpCodeDrName = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAcgroup.GrpName,
                                   ConsGrpCodeCr = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.ConsGrpCodeCr,
                                   ConsGrpCodeCrName = i.TmAccounts.TmAccounts.TmAccounts.TmAcgroup.GrpName,
                                   IsActive = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.IsActive,
                                   BeAcOnSl = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.BeAcOnSl,
                                   BeAcOnGl = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.BeAcOnGl,
                                   BeAcOnSlType = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.BeAcOnSlType,
                                   BeAcOnDesc = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.BeAcOnDesc,                                                                     
                                   TdsNature = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.TdsNature,
                                   TdsNatureDesc = i.TmTdsnature.NatureDesc,
                                   PayFavour = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.PayFavour,
                                   PartyCode = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.PartyCode,
                                   PartyName = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccounts.PartyName,
                                   Address1 = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.Address1,
                                   Address2 = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.Address2,
                                   Address3 = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.Address3,
                                   LandMark = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.LandMark,
                                   PinCode = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.PinCode,
                                   PostOff = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmPlace.PostOff,
                                   CityCode = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.CityCode,
                                   CityName = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmPlace.CityName,
                                   TalukCode = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.TalukCode,
                                   TalukName = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmPlace.TalukName,
                                   DistCode = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.DistCode,
                                   DistName = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmPlace.DistName,
                                   StateCode = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.StateCode,
                                   StateName = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmPlace.StateName,
                                   CountryCode = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.CountryCode,
                                   CountryName = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmPlace.CountryName,
                                   PhoneNo = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.PhoneNo,
                                   FaxNo = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.FaxNo,
                                   MobileNo = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.MobileNo,
                                   MailId = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.MailId,
                                   Website = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.Website,
                                   ContactPer = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.ContactPer,
                                   AcRefNo = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.AcRefNo,
                                   CstNo = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.CstNo,
                                   CstDate = (DateTime)i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.CstDate,
                                   LstNo = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.LstNo,
                                   LstDate = (DateTime) i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.LstDate,
                                   VatNo = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.VatNo,
                                   VatDate = (DateTime)i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.VatDate,
                                   PanNo = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.PanNo,
                                   PanDate =(DateTime)i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.PanDate,
                                   BankCode = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.BankCode,
                                   BankName = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmGcm.GcmDesc,
                                   BankAdd1 = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.BankAdd1,
                                   BankAdd2 = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.BankAdd2,
                                   BankAdd3 = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.BankAdd3,
                                   PayableAt = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.PayableAt,
                                   PayableAtName = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmPlace.PlaceName,
                                   GstNo = i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.GstNo,
                                   GstDate =(DateTime)i.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccounts.TmAccountsdetail.TmAccountsdetail.TmAccountsdetail.TmAccounts.TmAccountsdetail.GstDate

                               }).ToListAsync();
               
                if (tmAccounts.Count == 0)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("No Account with ID = {0}", AccountCode)),
                        ReasonPhrase = "Account Not Found"
                    };
                    throw new System.Web.Http.HttpResponseException(resp);
                }
                return tmAccounts;

            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("SaveUpdate")]
        public async Task<ActionResult<Response>> PostTmAccountsDetail(AccountView tmAccountsView)
        {
            var tmAccounts = await _context.TmAccounts.FindAsync(tmAccountsView.AccountCode);
            var tmAccountsdetail = await _context.TmAccountsdetail.FindAsync(tmAccountsView.AccountCode);
            TmAccounts newtmAccounts = new TmAccounts();
            TmAccountsdetail newtmAccountsdetail = new TmAccountsdetail();
            if(tmAccounts == null && tmAccountsdetail == null)
            {
                newtmAccounts.AccountCode = tmAccountsView.AccountCode;
                newtmAccounts.AccountName = tmAccountsView.AccountName;
                newtmAccounts.GrpCodeCr = tmAccountsView.GrpCodeCr;
                newtmAccounts.GrpCodeDr = tmAccountsView.GrpCodeDr;
                newtmAccounts.AcType = tmAccountsView.AcType;
                newtmAccounts.GrpType = tmAccountsView.AcType;
                newtmAccounts.ControlAc = tmAccountsView.ControlAc;
                newtmAccounts.BillAlloc = tmAccountsView.BillAlloc;
                newtmAccounts.AnalCode1 = tmAccountsView.AnalCode1;
                newtmAccounts.AnalCode2 = tmAccountsView.AnalCode2;
                newtmAccounts.AnalCode3 = tmAccountsView.AnalCode3;
                newtmAccounts.AnalCode4 = tmAccountsView.AnalCode4;
                newtmAccounts.AnalCode5 = tmAccountsView.AnalCode5;
                newtmAccounts.CcReq = tmAccountsView.CcReq;
                newtmAccounts.SubLedCategory = tmAccountsView.SubLedCategory;
                newtmAccounts.Currency = tmAccountsView.Currency;
                newtmAccounts.TransControl = tmAccountsView.TransControl;
                newtmAccounts.CreditDays = tmAccountsView.CreditDays;
                newtmAccounts.ExpPymt = tmAccountsView.ExpPymt;
                newtmAccounts.AreaCode = tmAccountsView.AreaCode;
                newtmAccounts.ConsGrpCodeCr = tmAccountsView.ConsGrpCodeCr;
                newtmAccounts.ConsGrpCodeDr = tmAccountsView.ConsGrpCodeDr;
                newtmAccounts.IsActive = tmAccountsView.IsActive;
                newtmAccounts.BeAcOnSl = tmAccountsView.BeAcOnSl;
                newtmAccounts.BeAcOnGl = tmAccountsView.BeAcOnGl;
                newtmAccounts.BeAcOnSlType = tmAccountsView.BeAcOnSlType;
                newtmAccounts.BeAcOnDesc = tmAccountsView.BeAcOnDesc;
                newtmAccounts.PayFavour = tmAccountsView.PayFavour;
                newtmAccounts.PartyCode = tmAccountsView.PartyCode;
                newtmAccounts.PartyName = tmAccountsView.PartyName;
                newtmAccounts.TdsNature = tmAccountsView.TdsNature;
                newtmAccountsdetail.AccountCode = tmAccountsdetail.AccountCode;
                newtmAccountsdetail.Address1 = tmAccountsView.Address1;
                newtmAccountsdetail.Address2 = tmAccountsView.Address2;
                newtmAccountsdetail.Address3 = tmAccountsView.Address3;
                newtmAccountsdetail.LandMark = tmAccountsView.LandMark;
                newtmAccountsdetail.PinCode = tmAccountsView.PinCode;
                newtmAccountsdetail.CityCode = tmAccountsView.CityCode;
                newtmAccountsdetail.TalukCode = tmAccountsView.TalukCode;
                newtmAccountsdetail.DistCode = tmAccountsView.DistCode;
                newtmAccountsdetail.StateCode = tmAccountsView.StateCode;
                newtmAccountsdetail.CountryCode = tmAccountsView.CountryCode;
                newtmAccountsdetail.PhoneNo = tmAccountsView.PhoneNo;
                newtmAccountsdetail.FaxNo = tmAccountsView.FaxNo;
                newtmAccountsdetail.MobileNo = tmAccountsView.MobileNo;
                newtmAccountsdetail.MailId = tmAccountsView.MailId;
                newtmAccountsdetail.Website = tmAccountsView.Website;
                newtmAccountsdetail.ContactPer = tmAccountsView.ContactPer;
                newtmAccountsdetail.AcRefNo = tmAccountsView.AcRefNo;
                newtmAccountsdetail.CstNo = tmAccountsView.CstNo;
                newtmAccountsdetail.CstDate = tmAccountsView.CstDate;
                newtmAccountsdetail.LstNo = tmAccountsView.LstNo;
                newtmAccountsdetail.LstDate = tmAccountsView.LstDate;
                newtmAccountsdetail.VatNo = tmAccountsView.VatNo;
                newtmAccountsdetail.VatDate = tmAccountsView.VatDate;
                newtmAccountsdetail.PanNo = tmAccountsView.PanNo;
                newtmAccountsdetail.PanDate = tmAccountsView.PanDate;
                newtmAccountsdetail.GstNo = tmAccountsView.GstNo;
                newtmAccountsdetail.GstDate = tmAccountsView.GstDate;
                newtmAccountsdetail.BankCode = tmAccountsView.BankCode;
                newtmAccountsdetail.BankAdd1 = tmAccountsView.BankAdd1;
                newtmAccountsdetail.BankAdd2 = tmAccountsView.BankAdd2;
                newtmAccountsdetail.BankAdd3 = tmAccountsView.BankAdd3;
                newtmAccountsdetail.PayableAt = tmAccountsView.PayableAt;
              
                _context.TmAccounts.Add(newtmAccounts);
                _context.TmAccountsdetail.Add(newtmAccountsdetail);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    if (TmAccountsExists(tmAccounts.AccountCode) && TmAccountsdetailExists(tmAccountsdetail.AccountCode))
                    {
                        return new Response { Status = "Conflict", Message = "Record Already Exist" };
                    }
                    else 
                    {
                        return new Response { Status = "Error", Message = ex.Message.ToString() };
                    }
                }
                return new Response { Status = "SUCCESSFULL", Message = "SAVED SUCCESSFULLY" };

            }
            if(tmAccounts != null && tmAccountsdetail != null)
            {
                tmAccounts.AccountName = tmAccountsView.AccountName;
                tmAccounts.GrpCodeCr = tmAccountsView.GrpCodeCr;
                tmAccounts.GrpCodeDr = tmAccountsView.GrpCodeDr;
                tmAccounts.BillAlloc = tmAccountsView.BillAlloc;
                tmAccounts.AnalCode1 = tmAccountsView.AnalCode1;
                tmAccounts.AnalCode2 = tmAccountsView.AnalCode2;
                tmAccounts.AnalCode3 = tmAccountsView.AnalCode3;
                tmAccounts.AnalCode4 = tmAccountsView.AnalCode4;
                tmAccounts.AnalCode5 = tmAccountsView.AnalCode5;
                tmAccounts.CcReq = tmAccountsView.CcReq;
                tmAccounts.SubLedCategory = tmAccountsView.SubLedCategory;                
                tmAccounts.CreditDays = tmAccountsView.CreditDays;
                tmAccounts.AreaCode = tmAccountsView.AreaCode;
                tmAccounts.ConsGrpCodeCr = tmAccountsView.ConsGrpCodeCr;
                tmAccounts.ConsGrpCodeDr = tmAccountsView.ConsGrpCodeDr;
                tmAccounts.IsActive = tmAccountsView.IsActive;
                tmAccounts.PayFavour = tmAccountsView.PayFavour;
                tmAccounts.PartyCode = tmAccountsView.PartyCode;
                tmAccounts.TdsNature = tmAccountsView.TdsNature;
                tmAccountsdetail.Address1 = tmAccountsView.Address1;
                tmAccountsdetail.Address2 = tmAccountsView.Address2;
                tmAccountsdetail.Address3 = tmAccountsView.Address3;
                tmAccountsdetail.LandMark = tmAccountsView.LandMark;
                tmAccountsdetail.PinCode = tmAccountsView.PinCode;
                tmAccountsdetail.CityCode = tmAccountsView.CityCode;
                tmAccountsdetail.TalukCode = tmAccountsView.TalukCode;
                tmAccountsdetail.DistCode = tmAccountsView.DistCode;
                tmAccountsdetail.StateCode = tmAccountsView.StateCode;
                tmAccountsdetail.CountryCode = tmAccountsView.CountryCode;
                tmAccountsdetail.PhoneNo = tmAccountsView.PhoneNo;
                tmAccountsdetail.FaxNo = tmAccountsView.FaxNo;
                tmAccountsdetail.MobileNo = tmAccountsView.MobileNo;
                tmAccountsdetail.MailId = tmAccountsView.MailId;
                tmAccountsdetail.Website = tmAccountsView.Website;
                tmAccountsdetail.ContactPer = tmAccountsView.ContactPer;
                tmAccountsdetail.AcRefNo = tmAccountsView.AcRefNo;
                tmAccountsdetail.CstNo = tmAccountsView.CstNo;
                tmAccountsdetail.CstDate = tmAccountsView.CstDate;
                tmAccountsdetail.LstNo = tmAccountsView.LstNo;
                tmAccountsdetail.LstDate = tmAccountsView.LstDate;
                tmAccountsdetail.VatNo = tmAccountsView.VatNo;
                tmAccountsdetail.VatDate = tmAccountsView.VatDate;
                tmAccountsdetail.PanNo = tmAccountsView.PanNo;
                tmAccountsdetail.PanDate = tmAccountsView.PanDate;
                tmAccountsdetail.GstNo = tmAccountsView.GstNo;
                tmAccountsdetail.GstDate = tmAccountsView.GstDate;
                tmAccountsdetail.BankCode = tmAccountsView.BankCode;
                tmAccountsdetail.BankAdd1 = tmAccountsView.BankAdd1;
                tmAccountsdetail.BankAdd2 = tmAccountsView.BankAdd2;
                tmAccountsdetail.BankAdd3 = tmAccountsView.BankAdd3;
                tmAccountsdetail.PayableAt = tmAccountsView.PayableAt;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!TmAccountsExists(tmAccountsView.AccountCode) && !TmAccountsdetailExists(tmAccountsView.AccountCode))
                    {

                        return new Response { Status = "NotFound", Message = "Record Not Found" };
                    }
                    else
                    {
                        return new Response { Status = "Error", Message = ex.Message.ToString() };
                    }
                }

                return new Response { Status = "Updated", Message = "Record Updated Sucessfull" };
            }
            return null;
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
