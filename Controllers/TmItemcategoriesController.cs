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
    public class TmItemcategoriesController : ControllerBase
    {
        private readonly reactppdContext _context;

        public TmItemcategoriesController(reactppdContext context)
        {
            _context = context;
        }

        // GET: api/TmItemcategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TmItemcategory>>> GetTmItemcategory()
        {
            return await _context.TmItemcategory.ToListAsync();
        }

        // GET: api/TmItemcategories/5
        [HttpPost("CategoryCode")]
        public async Task<ActionResult<List<ItemCategory>>> GetTmItemcategory(string CatgyCode)
        {
            try
            {
                if (CatgyCode == null)
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
                            Content = new StringContent(string.Format("No CategoryCode with Name = {0}", CatgyCode)),
                            ReasonPhrase = "CategoryCode  Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmItemcategory;
                }
                else if (CatgyCode != null)
                {
                    var tmItemcategory = await _context.TmItemcategory
                                .Where(i => i.CatgyCode.StartsWith(CatgyCode))
                                .Where(i => i.IsActive == "A")
                                .OrderBy(i => i.CatgyName)
                                .Select(i => new ItemCategory { CatgyCode = i.CatgyCode, CatgyName = i.CatgyName })
                                .ToListAsync();


                    if (tmItemcategory.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No CategoryCode with Id = {0}", CatgyCode)),
                            ReasonPhrase = "CategoryCode  Not Found"
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

        [HttpPost("ParentCode")]
        public async Task<ActionResult<List<ItemCategory>>> GetTmItemcategoryParentCode(string ParentCode)
        {
            try
            {
                if (ParentCode == null)
                { 
                    var tmItemcategory = await _context.TmItemcategory
                                        .Where(i => i.IsActive == "A")
                                        .OrderBy(i => i.CatgyName)
                                        .Select(i => new ItemCategory { PrtCode = i.PrtCode, CatgyName = i.CatgyName })
                                        .ToListAsync();


                    if (tmItemcategory.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No ParentCode with Id = {0}", ParentCode)),
                            ReasonPhrase = "ParentCode Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmItemcategory;
                }
                else if (ParentCode != null)
                {
                    var tmItemcategory = await _context.TmItemcategory
                                        .Where(i =>i.CatgyCode == i.PrtCode)
                                        .Where(i =>i.PrtCode.StartsWith(ParentCode))
                                        .Where(i => i.IsActive == "A")
                                        .OrderBy(i => i.CatgyName)
                                        .Select(i => new ItemCategory { PrtCode = i.PrtCode, CatgyName = i.CatgyName })
                                        .ToListAsync();


                    if (tmItemcategory.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No ParentCode with Id = {0}", ParentCode)),
                            ReasonPhrase = "ParentCode  Not Found"
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

        [HttpPost("GrantParentCode")]
        public async Task<ActionResult<List<ItemCategory>>> GetTmItemcategoryGrantParentCode(string GrantParentCode)
        {
            try
            {
                if (GrantParentCode == null)
                {
                    var tmItemcategory = await _context.TmItemcategory
                                        .Where(i => i.IsActive == "A")
                                        .OrderBy(i => i.CatgyName)
                                        .Select(i => new ItemCategory { GprtCode = i.GprtCode, CatgyName = i.CatgyName })
                                        .ToListAsync();


                    if (tmItemcategory.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No GrantParentCode with Id = {0}", GrantParentCode)),
                            ReasonPhrase = "GrantParentCode Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }

                    return tmItemcategory;
                }
                else if (GrantParentCode != null)
                {
                    var tmItemcategory = await _context.TmItemcategory
                                        .Where(i => i.CatgyCode == i.GprtCode)
                                        .Where(i => i.IsActive == "A")
                                        .Where(i => i.GprtCode.StartsWith(GrantParentCode.ToUpper()))
                                        .OrderBy(i => i.CatgyName)
                                        .Select(i => new ItemCategory { GprtCode = i.GprtCode, CatgyName = i.CatgyName })
                                        .ToListAsync();


                    if (tmItemcategory.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No GrantParentCode with Id = {0}", GrantParentCode)),
                            ReasonPhrase = "GrantParentCode  Not Found"
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

        [HttpPost("ViewItemCategory")]
        public async Task<ActionResult<IEnumerable<TmItemcategory>>> ViewTmItemcategory(string CatgyCode)
        {
            try
            {
                var tmItemcategory = await _context.TmItemcategory
                            .Where(i => i.CatgyCode == CatgyCode)
                            .Select(i => i).ToListAsync();
                if (tmItemcategory.Count == 0)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("No CategoryCode with ID = {0}", CatgyCode)),
                        ReasonPhrase = "CategoryCode  Not Found"
                    };
                    throw new System.Web.Http.HttpResponseException(resp);
                }
                return tmItemcategory;

            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        // PUT: api/TmItemcategories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
      /*  [HttpPut("{id}")]
        public async Task<IActionResult> PutTmItemcategory(string id, TmItemcategory tmItemcategory)
        {
            if (id != tmItemcategory.CatgyCode)
            {
                return BadRequest();
            }

            _context.Entry(tmItemcategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TmItemcategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/TmItemcategories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("SaveUpdate")]
        public async Task<ActionResult<Response>> PostTmItemcategory(string catgycode,TmItemcategory tmItemcategory)
        {
            if (catgycode != tmItemcategory.CatgyCode)
            {
                _context.TmItemcategory.Add(tmItemcategory);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (TmItemcategoryExists(tmItemcategory.CatgyCode))
                    {
                        return new Response { Status = "Conflict", Message = "Record Already Exist" };
                    }
                }
                return new Response { Status = "SUCCESSFULL", Message = "SAVED SUCCESSFULLY" };

            }
            else if (catgycode == tmItemcategory.CatgyCode)
            {
                /* {
                     return BadRequest();
                 }*/

                _context.Entry(tmItemcategory).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TmItemcategoryExists(catgycode))
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

        // DELETE: api/TmItemcategories/5
       /* [HttpDelete("{id}")]
        public async Task<ActionResult<TmItemcategory>> DeleteTmItemcategory(string id)
        {
            var tmItemcategory = await _context.TmItemcategory.FindAsync(id);
            if (tmItemcategory == null)
            {
                return NotFound();
            }

            _context.TmItemcategory.Remove(tmItemcategory);
            await _context.SaveChangesAsync();

            return tmItemcategory;
        }*/

        private bool TmItemcategoryExists(string id)
        {
            return _context.TmItemcategory.Any(e => e.CatgyCode == id);
        }
    }
}
