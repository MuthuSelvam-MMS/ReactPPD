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
    public class TmVendorsController : ControllerBase
    {
        private readonly reactppdContext _context;

        public TmVendorsController(reactppdContext context)
        {
            _context = context;
        }

        // GET: api/TmVendors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TmVendor>>> GetTmVendor()
        {
            return await _context.TmVendor.ToListAsync();
        }

        // GET: api/TmVendors/5
        [HttpPost("VendorCode")]
        public async Task<ActionResult<IEnumerable<TmAccounts>>> GetTmAccounts(string vendorname)
        {
            try
            {
                if (vendorname == null)
                {
                    var tmaccounts = await _context.TmAccounts
                                     .Where(i =>i.PartyType.Contains("A") || i.PartyType.Contains("C") )
                                     .OrderBy(i => i.AccountName)
                                     .Select(i => new TmAccounts { AccountCode = i.AccountCode, AccountName = i.AccountName })
                                     .ToListAsync();


                    if (tmaccounts.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Vendor with Name = {0}", vendorname)),
                            ReasonPhrase = "VendorName  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmaccounts;
                }
                else if (vendorname != null)
                {
                    var tmaccounts = await _context.TmAccounts
                                    .Where(i => i.AccountName.StartsWith(vendorname))
                                    .Where(i => i.PartyType.Contains("A") || i.PartyType.Contains("C"))
                                    .OrderBy(i => i.AccountName)
                                    .Select(i => new TmAccounts { AccountCode = i.AccountCode, AccountName = i.AccountName })
                                    .ToListAsync();



                    if (tmaccounts.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Vendor with Name = {0}", vendorname)),
                            ReasonPhrase = "VendorName  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmaccounts;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("ItemCode")]
        public async Task<ActionResult<IEnumerable<TmItem>>> GetTmItem(string itemname)
        {
            try
            {
                if (itemname == null)
                {
                    var tmItems = await _context.TmItem
                                    .OrderBy(i => i.ItemName)
                                    .Select(i => new TmItem{ ItemCode = i.ItemCode, ItemName = i.ItemName })
                                    .ToListAsync();


                    if (tmItems.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Item with Name = {0}", itemname)),
                            ReasonPhrase = "ItemName  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmItems;
                }
                else if (itemname != null)
                {
                    var tmItems = await _context.TmItem
                                    .Where(i => i.ItemName.StartsWith(itemname))                                    
                                    .OrderBy(i => i.ItemName)
                                    .Select(i => new TmItem { ItemCode = i.ItemCode, ItemName = i.ItemName })
                                    .ToListAsync();



                    if (tmItems.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Item with Name = {0}", itemname)),
                            ReasonPhrase = "ItemName  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmItems;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("ViewData")]
        public async Task<ActionResult<List<VendorItem>>> ViewTmVendor(string accountcode, string itemcode)
        {
            try
            {
                var tmVendor = await _context.TmVendor
                              .Join(_context.TmAccounts, v => v.AccountCode, a => a.AccountCode, (v, a) => new { TmVendor = v, TmAccounts = a })
                              .Join(_context.TmItem, vi => vi.TmVendor.ItemCode, i => i.ItemCode, (vi, i) => new { TmVendor = vi, TmItem = i })
                              .Where(m => m.TmVendor.TmVendor.AccountCode == accountcode && m.TmVendor.TmVendor.ItemCode == itemcode)
                              .Select(m => new
                              {
                                  AccountCode = m.TmVendor.TmAccounts.AccountCode,
                                  AccountName = m.TmVendor.TmAccounts.AccountName,
                                  ItemCode = m.TmVendor.TmVendor.ItemCode,
                                  ItemName = m.TmItem.ItemName,
                                  IsActive = m.TmVendor.TmVendor.IsActive,
                                  PurType = m.TmVendor.TmVendor.PurType
                              }).ToListAsync();
                List<VendorItem> vendoritems = new List<VendorItem>();
                foreach (var items in tmVendor)
                {
                    vendoritems.Add(new VendorItem
                    {
                        AccountCode = items.AccountCode.ToString(),
                        AccountName = items.AccountName.ToString(),
                        ItemCode = items.ItemCode.ToString(),
                        ItemName = items.ItemName.ToString(),
                        IsActive = items.IsActive.ToString(),
                        PurType = items.PurType.ToString()
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
        }
        [HttpPost("SaveData")]
        public async Task<ActionResult<Response>> PostTmVendor(VendorItem tmVendorview)
        {
            var tmVendor = await _context.TmVendor.Where(x => x.AccountCode == tmVendorview.AccountCode && x.ItemCode == tmVendorview.ItemCode).FirstOrDefaultAsync();
            TmVendor newtmVendor = new TmVendor();
            if(tmVendor == null)
            {
                newtmVendor.AccountCode = tmVendorview.AccountCode;
                newtmVendor.ItemCode = tmVendorview.ItemCode;
                newtmVendor.IsActive = tmVendorview.IsActive;
                newtmVendor.PurType = tmVendorview.PurType;
                _context.TmVendor.Add(newtmVendor);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    if (TmVendorExists(tmVendor.AccountCode))
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
            if(tmVendor != null)
            {
                return new Response { Status = "Conflict", Message = "Record Already Exist" };
            }
            return null;
        }
        //[HttpPost("SaveData")]
        //public async Task<ActionResult<Response>> PostTmVendor(TmVendor tmVendor)
        //{
        //    _context.TmVendor.Add(tmVendor);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TmVendorExists(tmVendor.AccountCode))
        //        {
        //            // return Conflict();
        //            return new Response { Status = "Conflict", Message = "Record Already Exist" };
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return new Response { Status = "SUCCESSFULL", Message = "SAVED SUCCESSFULLY" };
        //}       
        private bool TmVendorExists(string id)
        {
            return _context.TmVendor.Any(e => e.AccountCode == id);
        }
    }
}
