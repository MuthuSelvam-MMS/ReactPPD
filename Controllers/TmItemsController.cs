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
using ReactPPD.VM;

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
        public async Task<ActionResult<List<Item>>> GetTmItem(string Itemname)
        {
            try
            {
                if (Itemname == null)
                {
                    var tmItem = await _context.TmItem                              
                                .Where(i => i.IsActive == "A")
                                .OrderBy(i => i.ItemName)
                                .Select(i => new Item {ItemCode = i.ItemCode, ItemName = i.ItemName })
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
                                .Select(i => new Item{ ItemCode = i.ItemCode, ItemName = i.ItemName })
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
        public async Task<ActionResult<List<Item>>> GetTmParentItemCode(string ItemName)
        {
            try
            {
                if (ItemName == null)
                {
                    var tmItem = await _context.TmItem                              
                                .Where(i => i.IsActive == "A")
                                .OrderBy(i => i.ItemName)
                                .Select(i => new Item { ItemCode = i.ItemCode, ItemName = i.ItemName })
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
                                .Select(i => new Item { ItemCode = i.ItemCode, ItemName = i.ItemName })
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
        public async Task<ActionResult<List<Item>>> GetTmContainerCode1(string ItemName)
        {
            try {
                if (ItemName == null)
                {
                    var tmItem = await _context.TmItem                                
                                .Where(i => i.IsActive == "A")
                                .Where(i => i.ItemType == "NP")
                                .OrderBy(i => i.ItemName)
                                .Select(i => new Item { ItemCode = i.ItemCode, ItemName = i.ItemName })
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
                                .Select(i => new Item { ItemCode = i.ItemCode, ItemName = i.ItemName })
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
        public async Task<ActionResult<List<Item>>> GetTmContainerCode2(string ItemName)
        {
            try
            {
                if (ItemName == null)
                {
                    var tmItem = await _context.TmItem                                 
                                 .Where(i => i.IsActive == "A")
                                 .Where(i => i.ItemType == "NP")
                                 .OrderBy(i => i.ItemName)
                                 .Select(i => new Item { ItemCode = i.ItemCode, ItemName = i.ItemName })
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
                                .Select(i => new Item { ItemCode = i.ItemCode, ItemName = i.ItemName })
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
        public async Task<ActionResult<List<Item>>> GetTmPremixCode1(string ItemName)
        {
            try {
                if (ItemName == null)
                {
                    var tmItem = await _context.TmItem                                
                                 .Where(i => i.IsActive == "A")
                                 .Where(i => i.ItemType == "FX")
                                 .OrderBy(i => i.ItemName)
                                 .Select(i => new Item { ItemCode = i.ItemCode, ItemName = i.ItemName })
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
                                .Select(i => new Item { ItemCode = i.ItemCode, ItemName = i.ItemName })
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
        public async Task<ActionResult<List<Item>>> GetTmPremixCode2(string ItemName)
        {
            try { if (ItemName == null)
                {
                    var tmItem = await _context.TmItem                                  
                                 .Where(i => i.IsActive == "A")
                                 .Where(i => i.ItemType == "FX")
                                 .OrderBy(i => i.ItemName)
                                 .Select(i => new Item { ItemCode = i.ItemCode, ItemName = i.ItemName })
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
                                .Select(i => new Item { ItemCode = i.ItemCode, ItemName = i.ItemName })
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
        public async Task<ActionResult<List<ItemType>>> GetTmItemtype(string itemtype)
        {
            try
            {
                if (itemtype == null)
                {
                    var tmItemType = await (from i in _context.TmItemtype
                                      where i.IsActive == "A"
                                      orderby i.Descn
                                      select new ItemType { Itemtype = i.ItemType, Descn = i.Descn }).ToListAsync();
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
                                     .Select(i => new ItemType { Itemtype = i.ItemType, Descn = i.Descn }).ToListAsync();
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
        public async Task<ActionResult<List<ProdNature>>> GetTmProdNature()
        {
            try {
                var tmProdnature = await _context.TmProdnature
                                   .OrderBy(i => i.Name)
                                   .Select(i => new ProdNature { Code = i.Code, Name = i.Name }).ToListAsync();
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
        [HttpPost("UOML")]
        public async Task<ActionResult<List<UOM>>> GetTmUomLargest(string desc)
        {
            try
            {
                if (desc == null)
                {
                    var tmUom = await _context.TmUom
                                .Where(i => i.IsActive == "A")
                                .OrderBy(i => i.UomName)
                                .Select(i => new UOM { Uom = i.Uom, UomName = i.UomName }).ToListAsync();
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
                                .Select(i => new UOM { Uom = i.Uom, UomName = i.UomName }).ToListAsync();
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

       
        [HttpPost("UOMS")]
        public async Task<ActionResult<List<UOM>>> GetTmUomSmallest(string desc)
        {
            try
            {
                if (desc == null)
                {
                    var tmUom = await _context.TmUom
                                .Where(i => i.IsActive == "A")
                                .OrderBy(i => i.UomName)
                                .Select(i => new UOM { Uom = i.Uom, UomName = i.UomName }).ToListAsync();
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
                                .Select(i => new UOM { Uom = i.Uom, UomName = i.UomName }).ToListAsync();
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

       
        [HttpPost("UOMP")]
        public async Task<ActionResult<List<UOM>>> GetTmUomPurchase(string desc)
        {
            try
            {
                if (desc == null)
                {
                    var tmUom = await _context.TmUom
                                .Where(i => i.IsActive == "A")
                                .OrderBy(i => i.UomName)
                                .Select(i => new UOM { Uom = i.Uom, UomName = i.UomName }).ToListAsync();
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
                                .Select(i => new UOM { Uom = i.Uom, UomName = i.UomName }).ToListAsync();
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
        public async Task<ActionResult<List<Gcm>>> GetTmGcm(string gcmdescn)
        {
            try
            {
                if (gcmdescn == null)
                {
                    var tmGcm = await _context.TmGcm
                                .Where(i => i.GcmType == "PG" && i.IsActive == "A")
                                .OrderBy(i => i.GcmDesc)
                                .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc }).ToListAsync();
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
                                .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc }).ToListAsync();
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
        public async Task<ActionResult<List<ItemCategory>>> GetTmItemcategory(string categyname)
        {
            try
            {
                if (categyname == null)
                {
                    var tmItemcategory = await _context.TmItemcategory
                                         .Where(i => i.IsActive == "A")
                                         .OrderBy(i => i.CatgyName)
                                         .Select(i => new ItemCategory { CatgyCode = i.CatgyCode, CatgyName = i.CatgyName })
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
                                         .Select(i => new ItemCategory { CatgyCode = i.CatgyCode, CatgyName = i.CatgyName })
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
        [HttpPost("SaveUpdate")]
        public async Task<ActionResult<Response>> PostTmItem(string branchcode, int acyear, string section, string gradesection, string sectionname, string itemcode, TmItem tmItem)
        {
            if (itemcode != tmItem.ItemCode)
            {                
                if (tmItem.ItemType.StartsWith("CH") || tmItem.ItemType.StartsWith("FC"))
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
                _context.TmItem.Add(tmItem);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (TmItemExists(tmItem.ItemCode))
                    {
                        return new Response { Status = "Conflict", Message = "Record Already Exist" };
                    }
                }

                return new Response { Status = "SUCCESSFULL", Message = "SAVED SUCCESSFULLY" };
            }
            else if (itemcode == tmItem.ItemCode)
            {
                TmMeats tmmeats = new TmMeats();
                TmItem updtmItem = new TmItem();
                if (tmItem.ItemType.StartsWith("CH") || tmItem.ItemType.StartsWith("FC"))
                {
                    tmmeats.MeatsCode = tmItem.ItemCode;                  
                    tmmeats.Uom = tmItem.UomPur;
                    tmmeats.Section = section;
                    tmmeats.GradeSection = gradesection;
                    tmmeats.SectionName = sectionname;
                    

                    _context.Entry(tmmeats).State = EntityState.Modified;      
                    
                }
               /* updtmItem.ItemCode = tmItem.ItemCode;
                updtmItem.ItemName = tmItem.ItemName;
                updtmItem.ItemSpec = tmItem.ItemSpec;
                updtmItem.PartNo = tmItem.PartNo;
                updtmItem.ShortName = tmItem.ShortName;
                updtmItem.Category = tmItem.Category;
                updtmItem.ItemGroup = tmItem.ItemGroup;
                updtmItem.UomRelation = tmItem.UomRelation;
                updtmItem.UomPur = tmItem.UomPur;
                updtmItem.VisInSpn = tmItem.VisInSpn;
                updtmItem.InSpn = tmItem.InSpn;
                updtmItem.CashPur = tmItem.CashPur;
                updtmItem.ContCode1 = tmItem.ContCode1;
                updtmItem.ContCode2 = tmItem.ContCode2;
                updtmItem.PmxCode1 = tmItem.PmxCode1;
                updtmItem.PmxCode2 = tmItem.PmxCode2;
                updtmItem.ProdNature = tmItem.ProdNature;
                updtmItem.HsnCode = tmItem.HsnCode;
                updtmItem.StdWt = tmItem.StdWt;*/
               _context.Entry(tmItem).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TmItemExists(itemcode) || tmmeats.MeatsCode != itemcode)
                {
                    return new Response { Status = "NotFound", Message = "Record Not Found" };
                }
                    else { 
                        return new Response { Status = "Not Allowed", Message = "Update Not Allowed" }; }
                
            }

            return new Response { Status = "Updated", Message = "Record Updated Sucessfull" }; 
            }
            return null;
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
