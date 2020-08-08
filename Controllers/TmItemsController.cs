using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactPPD.Model;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Net.Http;

namespace ReactPPD.Controllers
{
    [EnableCors("*", "*", "*")]
    [Route("api/[controller]")]
    
    [ApiController]
    public class TmItemsController : ControllerBase
    {
        private readonly reactppdContext _context;

        public TmItemsController(reactppdContext context)
        {
            _context = context;
        }

        //[NotImplExceptionFilter]
        //[HttpPost("ViewItem")]
        //public ActionResult<IEnumerable<TmItem>> ViewTmItem()
        //{
        //    throw new NotImplementedException("This method is not implemented");
        //    return null;
        //}

        // GET: api/TmItems
        [HttpPost("ViewItem")]
        public async Task<ActionResult<IEnumerable<TmItem>>> ViewTmItem(string ItemCode)
        {
            try
            {
                var tmItem = await _context.TmItem
                            .Where(i => i.ItemCode == ItemCode)
                            .Select(i => i).ToListAsync();
                if (tmItem.Count == 0)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("No ItemCode with ID = {0}", ItemCode)),
                        ReasonPhrase = "Item  Not Found"
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
        // GET: api/TmItems/5

        [HttpPost("ItemName")]
        public async Task<ActionResult<IEnumerable<TmItem>>> GetTmItem(string Itemname)
        {
            try
            {
                if (Itemname == null)
                {
                    var tmItem = await _context.TmItem                              
                                .Where(i => i.IsActive == "A")
                                .OrderBy(i => i.ItemName)
                                .Select(i => new TmItem { ItemCode = i.ItemCode, ItemName = i.ItemName })
                                .ToListAsync();


                    if (tmItem.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No ItemName with Name = {0}", Itemname)),
                            ReasonPhrase = "Item  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmItem;
                }
                else if (Itemname != null)
                {
                    var tmItem = await _context.TmItem
                                .Where(i => i.ItemName.StartsWith(Itemname))
                                .Where(i => i.IsActive == "A")
                                .OrderBy(i => i.ItemName)
                                .Select(i => new TmItem { ItemCode = i.ItemCode, ItemName = i.ItemName })
                                .ToListAsync();


                    if (tmItem.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No ItemName with Name = {0}", Itemname)),
                            ReasonPhrase = "Item  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmItem;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }           
       }

        [HttpPost("ParentItemCode")]
        public async Task<ActionResult<IEnumerable<TmItem>>> GetTmParentItemCode(string ItemName)
        {
            try
            {
                if (ItemName == null)
                {
                    var tmItem = await _context.TmItem                              
                                .Where(i => i.IsActive == "A")
                                .OrderBy(i => i.ItemName)
                                .Select(i => new TmItem { ItemCode = i.ItemCode, ItemName = i.ItemName })
                                .ToListAsync();
                    if (tmItem.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No ItemName with Name = {0}", ItemName)),
                            ReasonPhrase = "Item  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItem;
                }
                else if (ItemName != null)
                {
                    var tmItem = await _context.TmItem
                                .Where(i => i.ItemName.StartsWith(ItemName))
                                .Where(i => i.IsActive == "A")
                                .OrderBy(i => i.ItemName)
                                .Select(i => new TmItem { ItemCode = i.ItemCode, ItemName = i.ItemName })
                                .ToListAsync();
                    if (tmItem.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No ItemName with Name = {0}", ItemName)),
                            ReasonPhrase = "Item  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItem;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });

            }
        }

        [HttpPost("ContainerCode1")]
        public async Task<ActionResult<IEnumerable<TmItem>>> GetTmContainerCode1(string ItemName)
        {
            try {
                if (ItemName == null)
                {
                    var tmItem = await _context.TmItem                                
                                .Where(i => i.IsActive == "A")
                                .Where(i => i.ItemType == "NP")
                                .OrderBy(i => i.ItemName)
                                .Select(i => new TmItem { ItemCode = i.ItemCode, ItemName = i.ItemName })
                                .ToListAsync();

                    if (tmItem.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No ItemName with Name = {0}", ItemName)),
                            ReasonPhrase = "Item  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItem;
                }
                else if (ItemName != null)
                {
                    var tmItem = await _context.TmItem
                                .Where(i => i.ItemName.StartsWith(ItemName))
                                .Where(i => i.IsActive == "A")
                                .Where(i => i.ItemType == "NP")
                                .OrderBy(i => i.ItemName)
                                .Select(i => new TmItem { ItemCode = i.ItemCode, ItemName = i.ItemName })
                                .ToListAsync();

                    if (tmItem.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No ItemName with Name = {0}", ItemName)),
                            ReasonPhrase = "Item  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItem;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });

            }
        }
       
        [HttpPost("ContainerCode2")]
        public async Task<ActionResult<IEnumerable<TmItem>>> GetTmContainerCode2(string ItemName)
        {
            try
            {
                if (ItemName == null)
                {
                    var tmItem = await _context.TmItem                                 
                                 .Where(i => i.IsActive == "A")
                                 .Where(i => i.ItemType == "NP")
                                 .OrderBy(i => i.ItemName)
                                 .Select(i => new TmItem { ItemCode = i.ItemCode, ItemName = i.ItemName })
                                 .ToListAsync();

                    if (tmItem.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No ItemName with Name = {0}", ItemName)),
                            ReasonPhrase = "Item  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItem;
                }
                else if (ItemName != null)
                {
                    var tmItem = await _context.TmItem
                                .Where(i => i.ItemName.StartsWith(ItemName))
                                .Where(i => i.IsActive == "A")
                                .Where(i => i.ItemType == "NP")
                                .OrderBy(i => i.ItemName)
                                .Select(i => new TmItem { ItemCode = i.ItemCode, ItemName = i.ItemName })
                                .ToListAsync();

                    if (tmItem.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No ItemName with Name = {0}", ItemName)),
                            ReasonPhrase = "Item  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItem;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });

            }
        }

       
        [HttpPost("PREMIXCODE1")]
        public async Task<ActionResult<IEnumerable<TmItem>>> GetTmPremixCode1(string ItemName)
        {
            try {
                if (ItemName == null)
                {
                    var tmItem = await _context.TmItem                                
                                 .Where(i => i.IsActive == "A")
                                 .Where(i => i.ItemType == "FX")
                                 .OrderBy(i => i.ItemName)
                                 .Select(i => new TmItem { ItemCode = i.ItemCode, ItemName = i.ItemName })
                                 .ToListAsync();

                    if (tmItem.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No ItemName with Name = {0}", ItemName)),
                            ReasonPhrase = "Item  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItem;
                }
                else if (ItemName != null)
                {
                    var tmItem = await _context.TmItem
                                .Where(i => i.ItemName.StartsWith(ItemName))
                                .Where(i => i.IsActive == "A")
                                .Where(i => i.ItemType == "FX")
                                .OrderBy(i => i.ItemName)
                                .Select(i => new TmItem { ItemCode = i.ItemCode, ItemName = i.ItemName })
                                .ToListAsync();

                    if (tmItem.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No ItemName with Name = {0}", ItemName)),
                            ReasonPhrase = "Item  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItem;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });

            }
        }
        
        [HttpPost("PREMIXCODE2")]
        public async Task<ActionResult<IEnumerable<TmItem>>> GetTmPremixCode2(string ItemName)
        {
            try { if (ItemName == null)
                {
                    var tmItem = await _context.TmItem                                  
                                 .Where(i => i.IsActive == "A")
                                 .Where(i => i.ItemType == "FX")
                                 .OrderBy(i => i.ItemName)
                                 .Select(i => new TmItem { ItemCode = i.ItemCode, ItemName = i.ItemName })
                                 .ToListAsync();


                    if (tmItem.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No ItemName with Name = {0}", ItemName)),
                            ReasonPhrase = "Item  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItem;
                }
                else if (ItemName != null)
                {
                    var tmItem = await _context.TmItem
                                .Where(i => i.ItemName.StartsWith(ItemName))
                                .Where(i => i.IsActive == "A")
                                .Where(i => i.ItemType == "FX")
                                .OrderBy(i => i.ItemName)
                                .Select(i => new TmItem { ItemCode = i.ItemCode, ItemName = i.ItemName })
                                .ToListAsync();


                    if (tmItem.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No ItemName with Name = {0}", ItemName)),
                            ReasonPhrase = "Item  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItem;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });

            }
        }
        [HttpPost("ItemType")]
        public async Task<ActionResult<IEnumerable<TmItemtype>>> GetTmItemtype(string itemtype)
        {
            try
            {
                if (itemtype == null)
                {
                    var tmItemType = await (from i in _context.TmItemtype
                                      where i.IsActive == "A"
                                      orderby i.Descn
                                      select new TmItemtype { ItemType = i.ItemType, Descn = i.Descn }).ToListAsync();
                    if (tmItemType.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No ItemName with Name = {0}", itemtype)),
                            ReasonPhrase = "ItemType  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItemType;

                }
                else if (itemtype != null)
                {
                    var tmItemType = await _context.TmItemtype
                                     .Where(i => i.ItemType.StartsWith(itemtype))
                                     .Where(i => i.IsActive == "A")
                                     .OrderBy(i => i.Descn)
                                     .Select(i => new TmItemtype { ItemType = i.ItemType, Descn = i.Descn }).ToListAsync();
                    if (tmItemType.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No ItemName with Name = {0}", itemtype)),
                            ReasonPhrase = "ItemType  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItemType;

                }

                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });

            }

        }

        [HttpPost("ProductNature")]
        public async Task<ActionResult<IEnumerable<TmProdnature>>> GetTmProdNature()
        {
            try {
                var tmProdnature = await _context.TmProdnature
                                   .OrderBy(i => i.Name)
                                   .Select(i => new TmProdnature { Code = i.Code, Name = i.Name }).ToListAsync();
                if (tmProdnature.Count == 0)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("No Item Found")),
                        ReasonPhrase = "Item  Not Found"
                    };
                    throw new System.Web.Http.HttpResponseException(resp);
                }
                return tmProdnature;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {   
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }
        [HttpPost("UOMLARGESTUNIT")]
        public async Task<ActionResult<IEnumerable<TmUom>>> GetTmUomLargest(string desc)
        {
            try
            {
                if (desc == null)
                {
                    var tmUom = await _context.TmUom
                                .Where(i => i.IsActive == "A")
                                .OrderBy(i => i.UomName)
                                .Select(i => new TmUom { Uom = i.Uom, UomName = i.UomName }).ToListAsync();
                    if(tmUom.Count==0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Unit OF Measurment with Name = {0}", desc)),
                            ReasonPhrase = "Unit Of Meaurment Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmUom;
                }
                else if (desc != null)
                {
                    var tmUom = await _context.TmUom
                                .Where(i => i.UomName.StartsWith(desc))
                                .Where(i => i.IsActive == "A")
                                .OrderBy(i => i.UomName)
                                .Select(i => new TmUom { Uom = i.Uom, UomName = i.UomName }).ToListAsync();
                    if (tmUom.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Unit OF Measurment with Name = {0}", desc)),
                            ReasonPhrase = "Unit Of Meaurment Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmUom;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

       
        [HttpPost("UOMSMALLESTUNIT")]
        public async Task<ActionResult<IEnumerable<TmUom>>> GetTmUomSmallest(string desc)
        {
            try
            {
                if (desc == null)
                {
                    var tmUom = await _context.TmUom
                                .Where(i => i.IsActive == "A")
                                .OrderBy(i => i.UomName)
                                .Select(i => new TmUom { Uom = i.Uom, UomName = i.UomName }).ToListAsync();
                    if (tmUom.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Unit OF Measurment with Name = {0}", desc)),
                            ReasonPhrase = "Unit Of Meaurment Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmUom;
                }
                else if (desc != null)
                {
                    var tmUom = await _context.TmUom
                                .Where(i => i.UomName.StartsWith(desc))
                                .Where(i => i.IsActive == "A")
                                .OrderBy(i => i.UomName)
                                .Select(i => new TmUom { Uom = i.Uom, UomName = i.UomName }).ToListAsync();
                    if (tmUom.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Unit OF Measurment with Name = {0}", desc)),
                            ReasonPhrase = "Unit Of Meaurment Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmUom;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

       
        [HttpPost("UOMPURCHASE")]
        public async Task<ActionResult<IEnumerable<TmUom>>> GetTmUomPurchase(string desc)
        {
            try
            {
                if (desc == null)
                {
                    var tmUom = await _context.TmUom
                                .Where(i => i.IsActive == "A")
                                .OrderBy(i => i.UomName)
                                .Select(i => new TmUom { Uom = i.Uom, UomName = i.UomName }).ToListAsync();
                    if (tmUom.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Unit OF Measurment with Name = {0}", desc)),
                            ReasonPhrase = "Unit Of Meaurment Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmUom;
                }
                else if (desc != null)
                {
                    var tmUom = await _context.TmUom
                                .Where(i => i.UomName.StartsWith(desc))
                                .Where(i => i.IsActive == "A")
                                .OrderBy(i => i.UomName)
                                .Select(i => new TmUom { Uom = i.Uom, UomName = i.UomName }).ToListAsync();
                    if (tmUom.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Unit OF Measurment with Name = {0}", desc)),
                            ReasonPhrase = "Unit Of Meaurment Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmUom;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }
        [HttpPost("Section")]
        public async Task<ActionResult<IEnumerable<TmGcm>>> GetTmGcm(string gcmdescn)
        {
            try
            {
                if (gcmdescn == null)
                {
                    var tmGcm = await _context.TmGcm
                                .Where(i => i.GcmType == "PG" && i.IsActive == "A")
                                .OrderBy(i => i.GcmDesc)
                                .Select(i => new TmGcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc }).ToListAsync();
                    if (tmGcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No GCM with Name = {0}", gcmdescn)),
                            ReasonPhrase = "GCM NOT Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmGcm;
                }
                else if (gcmdescn != null)
                {
                    var tmGcm = await _context.TmGcm
                                .Where(i => i.GcmDesc.StartsWith(gcmdescn))
                                .Where(i => i.GcmType == "PG" && i.IsActive == "A")
                                .OrderBy(i => i.GcmDesc)
                                .Select(i => new TmGcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc }).ToListAsync();
                    if (tmGcm.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No GCM with Name = {0}", gcmdescn)),
                            ReasonPhrase = "GCM NOT Found"
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

        [HttpPost("ItemCategory")]
        public async Task<ActionResult<IEnumerable<TmItemcategory>>> GetTmItemcategory(string categyname)
        {
            try
            {
                if (categyname == null)
                {
                    var tmItemcategory = await _context.TmItemcategory
                                         .Where(i => i.IsActive == "A")
                                         .OrderBy(i => i.CatgyName)
                                         .Select(i => new TmItemcategory { CatgyCode = i.CatgyCode, CatgyName = i.CatgyName })
                                         .ToListAsync();
                    if (tmItemcategory.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Category with Name = {0}", categyname)),
                            ReasonPhrase = "Category Name NOT Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItemcategory;
                }
                else if (categyname != null)
                {
                    var tmItemcategory = await _context.TmItemcategory
                                         .Where(i => i.CatgyCode.StartsWith(categyname))
                                         .Where(i => i.IsActive == "A")
                                         .OrderBy(i => i.CatgyName)
                                         .Select(i => new TmItemcategory { CatgyCode = i.CatgyCode, CatgyName = i.CatgyName })
                                         .ToListAsync();
                                          
                    if (tmItemcategory.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Category with Name = {0}", categyname)),
                            ReasonPhrase = "Category Name NOT Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmItemcategory;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }
        //[EnableCors("*", "*", "*")]
        //[HttpPost("ItemCode")]
        //public async IAsyncEnumerable<TmItem> GetTmItem(string itemname)
        //{
        //    //    var tmItem = await _context.TmItem.FindAsync(id);
        //    var tmItem = (from i in _context.TmItem
        //                  where i.ItemName.StartsWith(itemname) && i.IsActive.StartsWith('A')
        //                  orderby i.ItemName
        //                  select new TmItem { ItemCode = i.ItemCode, ItemName = i.ItemName }).AsAsyncEnumerable();
        //    Console.WriteLine(tmItem);
        //    //    if (tmItem == null)
        //    //    {
        //    //        return NotFound();
        //    //    }
        //    await foreach (var item in tmItem)
        //    {
        //        yield return item;
        //    }
        //}

        // PUT: api/TmItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTmItem(string id, TmItem tmItem)
        {
            if (id != tmItem.ItemCode)
            {
                return BadRequest();
            }

            _context.Entry(tmItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TmItemExists(id))
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

        // POST: api/TmItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("InsertData")]
        public async Task<ActionResult<TmItem>> PostTmItem(string branchcode,int acyear,string section,string gradesection,string sectionname,[FromBody]TmItem tmItem)
        {
            _context.TmItem.Add(tmItem);
            if (tmItem.ItemType.StartsWith("Ch") || tmItem.ItemType.StartsWith("FC"))
            {
                TmMeats tmmeats = new TmMeats();
                tmmeats.MeatsCode = tmItem.ItemCode;
                tmmeats.MeatsName = tmItem.ItemName;
                tmmeats.BranchCode = branchcode;
                tmmeats.AcYearNo = acyear;
                tmmeats.IsActive = tmItem.IsActive;
                tmmeats.Uom = tmItem.UomPur;
                tmmeats.Section = section;
                tmmeats.GradeSection = gradesection;
                tmmeats.SectionName = sectionname;
                _context.TmMeats.Add(tmmeats);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TmItemExists(tmItem.ItemCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTmItem", new { id = tmItem.ItemCode }, tmItem);
        }

        // DELETE: api/TmItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TmItem>> DeleteTmItem(string id)
        {
            var tmItem = await _context.TmItem.FindAsync(id);
            if (tmItem == null)
            {
                return NotFound();
            }

            _context.TmItem.Remove(tmItem);
            await _context.SaveChangesAsync();

            return tmItem;
        }

        private bool TmItemExists(string id)
        {
            return _context.TmItem.Any(e => e.ItemCode == id);
        }
    }
}
