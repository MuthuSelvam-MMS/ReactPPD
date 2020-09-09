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
    public class TmVehiclesController : ControllerBase
    {
        private readonly reactppdContext _context;

        public TmVehiclesController(reactppdContext context)
        {
            _context = context;
        }

        // GET: api/TmVehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TmVehicle>>> GetTmVehicle()
        {
            return await _context.TmVehicle.ToListAsync();
        }

        // GET: api/TmVehicles/5
        [HttpPost("Vehicle")]
        public async Task<ActionResult<List<Vehicle>>> GetVehicle(string vehicleno)
        {
            try
            {
                if (vehicleno == null)
                {
                    var tmVehicle = await _context.TmVehicle
                                .OrderBy(i => i.VehNo)
                                .Select(i => new Vehicle { VehCode = i.VehCode, VehNo = i.VehNo }).ToListAsync();
                    if (tmVehicle.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Vehicle with No = {0}", vehicleno)),
                            ReasonPhrase = "Vehicle Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmVehicle;
                }
                else if (vehicleno != null)
                {
                    var tmVehicle = await _context.TmVehicle
                                    .Where(i => i.VehNo.StartsWith(vehicleno))
                                   .OrderBy(i => i.VehNo)
                                   .Select(i => new Vehicle { VehCode = i.VehCode, VehNo = i.VehNo }).ToListAsync();
                    if (tmVehicle.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Vehicle with No = {0}", vehicleno)),
                            ReasonPhrase = "Vehicle Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmVehicle;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("VehicleType")]
        public async Task<ActionResult<List<Gcm>>> GetVehicleType(string GcmType)
        {
            try
            {
                if (GcmType == null)
                {
                    var tmVehicle = await _context.TmGcm
                                    .Where(i => i.IsActive.StartsWith("A"))
                                .OrderBy(i => i.GcmDesc)
                                .Select(i => new Gcm {GcmCode = i.GcmCode, GcmDesc = i.GcmDesc }).ToListAsync();
                    if (tmVehicle.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Gcm with GcmType = {0}", GcmType)),
                            ReasonPhrase = "Gcm Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmVehicle;
                }
                else if (GcmType != null)
                {
                    var tmVehicle = await _context.TmGcm
                                    .Where(i => i.IsActive.StartsWith("A"))
                                    .Where(i => i.GcmType.StartsWith(GcmType))
                                    .OrderBy(i => i.GcmDesc)
                                    .Select(i => new Gcm { GcmCode = i.GcmCode, GcmDesc = i.GcmDesc }).ToListAsync();
                    if (tmVehicle.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Gcm with GcmType = {0}", GcmType)),
                            ReasonPhrase = "Gcm Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmVehicle;
                }
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("DivCode")]
        public async Task<ActionResult<List<Division>>> GetDivCode(string DivisionName)
        {
            try
            {
                if (DivisionName == null)
                {
                    var tmDivision = await _context.TmDivision
                                    .Where(i => i.IsActive.StartsWith("A"))
                                    .OrderBy(i => i.DivName)
                                    .Select(i => new Division { DivCode = i.DivCode, DivName = i.DivName, ShortName = i.ShortName }).ToListAsync();
                    if (tmDivision.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Division with DivisionName = {0}", DivisionName)),
                            ReasonPhrase = "Division Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmDivision;
                }
                else if (DivisionName != null)
                {
                    var tmDivision = await _context.TmDivision
                                    .Where(i => i.IsActive.StartsWith("A"))
                                    .Where(i => i.DivName.StartsWith(DivisionName))
                                    .OrderBy(i => i.DivName)
                                    .Select(i => new Division { DivCode = i.DivCode, DivName = i.DivName, ShortName = i.ShortName }).ToListAsync();
                    if (tmDivision.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Division with DivisionName = {0}", DivisionName)),
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
        public async Task<ActionResult<List<Region>>> GetRegion(string RegionName)
        {
            try
            {
                if (RegionName == null)
                {
                    var tmRegion = await _context.TmRegion
                                    .Where(i => i.IsActive.StartsWith("A"))
                                    .OrderBy(i => i.RegionName)
                                    .Select(i => new Region { RegionCode = i.RegionCode, RegionName = i.RegionName }).ToListAsync();
                    if (tmRegion.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Region with RegionName = {0}", RegionName)),
                            ReasonPhrase = "Region Not Found"
                        };
                        throw new System.Web.Http.HttpResponseException(resp);
                    }
                    return tmRegion;
                }
                else if (RegionName != null)
                {
                    var tmRegion = await _context.TmRegion
                                   .Where(i => i.RegionName.StartsWith(RegionName))
                                    .Where(i => i.IsActive.StartsWith("A"))
                                    .OrderBy(i => i.RegionName)
                                    .Select(i => new Region { RegionCode = i.RegionCode, RegionName = i.RegionName }).ToListAsync();
                    if (tmRegion.Count == 0)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Region with RegionName = {0}", RegionName)),
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

        [HttpPost("ViewData")]
        public async Task<ActionResult<List<Vehicle>>> GetVehicleData(string Vehiclecode)
        {
            try
            {
                var tmVehicle = await _context.TmVehicle
                                .Join(_context.TmDivision, A => A.DivCode, B => B.DivCode, (A, B) => new { TmVehicle = A, TmDivision = B })
                                .Join(_context.TmRegion, A => A.TmVehicle.RegionCode, C => C.RegionCode, (A, C) => new { TmVehicle = A, TmRegion = C })
                                .Join(_context.TmGcm, A => A.TmVehicle.TmVehicle.VehType, D => D.GcmCode, (A, D) => new { TmVehicle = A, TmGcm = D })
                                .Where(i => i.TmGcm.GcmType.StartsWith("VHT"))
                                .Where(i => i.TmVehicle.TmVehicle.TmVehicle.VehCode.StartsWith(Vehiclecode))
                                .Select(i => new Vehicle 
                                {
                                   VehCode = i.TmVehicle.TmVehicle.TmVehicle.VehCode,
                                   VehNo = i.TmVehicle.TmVehicle.TmVehicle.VehNo,
                                   VehType = i.TmVehicle.TmVehicle.TmVehicle.VehType,
                                   MakeName = i.TmVehicle.TmVehicle.TmVehicle.MakeName,
                                   DivCode = i.TmVehicle.TmVehicle.TmVehicle.DivCode,
                                   DivName = i.TmVehicle.TmVehicle.TmDivision.DivName,
                                   RegionCode = i.TmVehicle.TmVehicle.TmVehicle.RegionCode,
                                   RegionName = i.TmVehicle.TmRegion.RegionName,
                                   IsActive = i.TmVehicle.TmVehicle.TmVehicle.IsActive
                                }).ToListAsync();
                return null;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return BadRequest(new { Message = ex.Response.ReasonPhrase });
            }
        }

        [HttpPost("SaveUpdate")]
        public async Task<ActionResult<Response>> PostTmReason(Vehicle tmVehicle)
        {
            var vehicle = await _context.TmVehicle.FindAsync(tmVehicle.VehCode);
            TmVehicle newtmvehicle = new TmVehicle();
            if(vehicle == null)
            {
                newtmvehicle.VehCode = tmVehicle.VehCode;
                newtmvehicle.VehNo = tmVehicle.VehNo;
                newtmvehicle.VehType = tmVehicle.VehType;
                newtmvehicle.MakeName = tmVehicle.MakeName;
                newtmvehicle.DivCode = tmVehicle.DivCode;
                newtmvehicle.RegionCode = tmVehicle.RegionCode;
                newtmvehicle.IsActive = tmVehicle.IsActive;
                _context.TmVehicle.Add(newtmvehicle);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    if (TmVehicleExists(tmVehicle.VehCode))
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
            if(vehicle != null)
            {
                vehicle.VehType = tmVehicle.VehType;
                vehicle.MakeName = tmVehicle.MakeName;
                vehicle.DivCode = tmVehicle.DivCode;
                vehicle.RegionCode = tmVehicle.RegionCode;
                vehicle.IsActive = tmVehicle.IsActive;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!TmVehicleExists(tmVehicle.VehCode))
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
        private bool TmVehicleExists(string id)
        {
            return _context.TmVehicle.Any(e => e.VehCode == id);
        }
    }
}
