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
        public async Task<ActionResult<List<ItemCategory>>> ViewTmItemcategory(string CatgyCode)
        {
            try
            {
                var tmItemcategory = await _context.TmItemcategory
                                     .Join(_context.TmItemcategory,A =>A.PrtCode,B => B.CatgyCode,(A,B) => new {TmItemcategory = A, B })
                                     .Join(_context.TmItemcategory,A =>A.TmItemcategory.GprtCode ,C => C.CatgyCode,(A,C) => new {TmItemcategory = A ,C })
                                     .Where(i => i.TmItemcategory.TmItemcategory.CatgyCode == CatgyCode)
                                     .Select(i => new ItemCategory
                                     {
                                        CatgyCode = i.TmItemcategory.TmItemcategory.CatgyCode,
                                        CatgyName = i.TmItemcategory.TmItemcategory.CatgyName,
                                        PrtCode = i.TmItemcategory.TmItemcategory.PrtCode,
                                        PrtName = i.TmItemcategory.B.CatgyName,
                                        GprtCode = i.TmItemcategory.TmItemcategory.GprtCode,
                                        GprtName =i.C.CatgyName,
                                        IsActive = i.TmItemcategory.TmItemcategory.IsActive
                                     }).ToListAsync();
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

        [HttpPost("SaveUpdate")]
        public async Task<ActionResult<Response>> PostTmItemcategory(ItemCategory tmItemcategory)
        {
            var itemcategory = await _context.TmItemcategory.FindAsync(tmItemcategory.CatgyCode);
            TmItemcategory newitemcategory = new TmItemcategory();
            if(itemcategory == null)
            {
                newitemcategory.CatgyCode = tmItemcategory.CatgyCode;
                newitemcategory.CatgyName = tmItemcategory.CatgyName;
                newitemcategory.PrtCode = tmItemcategory.PrtCode;
                newitemcategory.GprtCode = tmItemcategory.GprtCode;
                newitemcategory.IsActive = tmItemcategory.IsActive;
                _context.TmItemcategory.Add(newitemcategory);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    if (TmItemcategoryExists(tmItemcategory.CatgyCode))
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
            if(itemcategory != null)
            {
                itemcategory.CatgyName = tmItemcategory.CatgyName;
                itemcategory.PrtCode = tmItemcategory.PrtCode;
                itemcategory.GprtCode = tmItemcategory.GprtCode;
                itemcategory.IsActive = tmItemcategory.IsActive;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!TmItemcategoryExists(tmItemcategory.CatgyCode))
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
        //public async Task<ActionResult<Response>> PostTmItemcategory(string catgycode,TmItemcategory tmItemcategory)
        //{
        //    if (catgycode != tmItemcategory.CatgyCode)
        //    {
        //        _context.TmItemcategory.Add(tmItemcategory);
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException)
        //        {
        //            if (TmItemcategoryExists(tmItemcategory.CatgyCode))
        //            {
        //                return new Response { Status = "Conflict", Message = "Record Already Exist" };
        //            }
        //        }
        //        return new Response { Status = "SUCCESSFULL", Message = "SAVED SUCCESSFULLY" };

        //    }
        //    else if (catgycode == tmItemcategory.CatgyCode)
        //    {
        //        /* {
        //             return BadRequest();
        //         }*/

        //        _context.Entry(tmItemcategory).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TmItemcategoryExists(catgycode))
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
        private bool TmItemcategoryExists(string id)
        {
            return _context.TmItemcategory.Any(e => e.CatgyCode == id);
        }
    }
}
