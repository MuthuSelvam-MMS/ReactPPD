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
        public async Task<ActionResult<List<ItemView>>> ViewTmItem(string ItemCode)
        {
            try
            {
                var tmitem = await _context.TmItem.FindAsync(ItemCode);
               if(tmitem !=null && tmitem.ItemType.StartsWith("CH") || tmitem.ItemType.StartsWith("FC"))
                {
                    var tmItem = await _context.TmItem                                  
                                 .Join(_context.TmItemtype,I =>I.ItemType,B =>B.ItemType,(I,B) => new { TmItem = I, TmItemtype = B })
                                 .Join(_context.TmItemcategory, I => I.TmItem.Category, C => C.CatgyCode, (I, C) => new { TmItem = I, TmItemcategory = C })
                                  .Join(_context.TmUom, I => I.TmItem.TmItem.UomBig, D => D.Uom, (I, D) => new { TmItem = I, TmUom = D })
                                  .Join(_context.TmUom, I => I.TmItem.TmItem.TmItem.UomSmall, E =>E.Uom, (I, E) => new { TmItem = I, TmUom = E })
                                  .Join(_context.TmUom, I => I.TmItem.TmItem.TmItem.TmItem.UomPur, F=> F.Uom, (I, F) => new { TmItem = I, TmUom = F })
                                  .Join(_context.TmUom, I => I.TmItem.TmItem.TmItem.TmItem.TmItem.UomStk, G => G.Uom, (I, G) => new { TmItem = I, TmUom = G })
                                  .Join(_context.TmItem, I => I.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.PrtItem, H => H.ItemCode, (I, H) => new { TmItem = I, H })
                                 .Join(_context.TmItem, I => I.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ContCode1, J => J.ItemCode, (I, J) => new { TmItem = I, J })
                                 .Join(_context.TmItem, I => I.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ContCode2, K => K.ItemCode, (I, K) => new { TmItem = I, K })
                                 .Join(_context.TmItem, I => I.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.PmxCode1, L => L.ItemCode, (I, L) => new { TmItem = I, L })
                                 .Join(_context.TmItem, I => I.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.PmxCode2, N => N.ItemCode, (I, N) => new { TmItem = I, N })
                                 .Join(_context.TmMeats, I => I.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ItemCode, M => M.MeatsCode, (I, M) => new { TmItem = I, TmMeats = M })                               
                                .Where(i => i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ItemCode == ItemCode)
                               .Select(i => new ItemView
                               {
                                   ItemCode = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ItemCode,
                                   ItemName = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ItemName,
                                   ItemSpec = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ItemSpec,
                                   PartNo = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.PartNo,
                                   ShortName = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ShortName,
                                   Itemtype = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ItemType,
                                   ItemDescn = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItemtype.Descn,
                                   Category = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.Category,
                                   CatgyName = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItemcategory.CatgyName,
                                   ItemGroup = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ItemGroup,
                                   PrtItem = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.PrtItem,
                                   PrtItemName = i.TmItem.TmItem.TmItem.TmItem.TmItem.H.ItemName,
                                   UomBig = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.UomBig,
                                   UomBigName = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmUom.UomName,
                                   UomSmall = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.UomSmall,
                                   UomSmallName = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmUom.UomName,
                                   UomRelation = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.UomRelation,
                                   UomPur = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.UomPur,
                                   UomPurName = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmUom.UomName,
                                   UomStk = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.UomStk,
                                   UomStkName = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmUom.UomName,
                                   VisInSpn = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.VisInSpn,
                                   InSpn = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.InSpn,
                                   CashPur = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.CashPur,
                                   Fsn = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.Fsn,
                                   Abc = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.Abc,
                                   IsActive = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.IsActive,
                                   StProdCode = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.StProdCode,
                                   ContCode1 = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ContCode1,
                                   ContCode1Name = i.TmItem.TmItem.TmItem.TmItem.J.ItemName,
                                   ContCode2 = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ContCode2,
                                   ContCode2Name = i.TmItem.TmItem.TmItem.K.ItemName,
                                   PmxCode1 = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.PmxCode1,
                                   PmxCode1Name = i.TmItem.TmItem.L.ItemName,
                                   PmxCode2 = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.PmxCode2,
                                   PmxCode2Name = i.TmItem.N.ItemName,
                                   Prodnature = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ProdNature,
                                   Cess = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.Cess,
                                   Nature = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.Nature,
                                   Div = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.Div,
                                   ContCap = (Decimal)i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ContCap,
                                   ContWtConf = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ContWtConf,
                                   ExpDays = (Int32)i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ExpDays,
                                   InReq = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.InReq,
                                   AcCode = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.AcCode,
                                   AcPostBase = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.AcPostBase,
                                   division = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.Division,
                                   HsnCode = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.HsnCode,
                                   Doses = (Decimal)i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.Doses,
                                   NoOfPackets = (Int32)i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.NoOfPackets,
                                   StdWt = (Decimal)i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.StdWt,
                                   MeatsCode = i.TmMeats.MeatsCode,
                                   MeatsName = i.TmMeats.MeatsName,
                                   BranchCode = i.TmMeats.BranchCode,
                                   AcYearNo = i.TmMeats.AcYearNo,
                                   Uom = i.TmMeats.Uom,
                                   Section = i.TmMeats.Section,
                                   GradeSection = i.TmMeats.GradeSection,
                                   SectionName = i.TmMeats.SectionName

                               }).ToListAsync();
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
               else
                {
                    var tmItem = await _context.TmItem
                                 .Join(_context.TmItemtype, I => I.ItemType, B => B.ItemType, (I, B) => new { TmItem = I, TmItemtype = B })
                                 .Join(_context.TmItemcategory, I => I.TmItem.Category, C => C.CatgyCode, (I, C) => new { TmItem = I, TmItemcategory = C })
                                  .Join(_context.TmUom, I => I.TmItem.TmItem.UomBig, D => D.Uom, (I, D) => new { TmItem = I, TmUom = D })
                                  .Join(_context.TmUom, I => I.TmItem.TmItem.TmItem.UomSmall, E => E.Uom, (I, E) => new { TmItem = I, TmUom = E })
                                  .Join(_context.TmUom, I => I.TmItem.TmItem.TmItem.TmItem.UomPur, F => F.Uom, (I, F) => new { TmItem = I, TmUom = F })
                                  .Join(_context.TmUom, I => I.TmItem.TmItem.TmItem.TmItem.TmItem.UomStk, G => G.Uom, (I, G) => new { TmItem = I, TmUom = G })
                                 .Join(_context.TmItem, I => I.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.PrtItem, H => H.ItemCode, (I, H) => new { TmItem =I, H })
                                 .Join(_context.TmItem, I => I.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ContCode1, J => J.ItemCode, (I, J) => new { TmItem = I, J })
                                 .Join(_context.TmItem, I=> I.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ContCode2, K =>K.ItemCode, (I, K) => new { TmItem = I, K })
                                 .Join(_context.TmItem, I => I.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.PmxCode1, L => L.ItemCode, (I, L) => new { TmItem = I, L })
                                 .Join(_context.TmItem, I => I.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.PmxCode2, N => N.ItemCode, (I, N) => new { TmItem = I, N })                                 
                                .Where(i => i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ItemCode == ItemCode)
                               .Select(i => new ItemView
                               {
                                   ItemCode = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ItemCode,
                                   ItemName = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ItemName,
                                   ItemSpec = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ItemSpec,
                                   PartNo = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.PartNo,
                                   ShortName = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ShortName,
                                   Itemtype = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ItemType,
                                   ItemDescn = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItemtype.Descn,
                                   Category = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.Category,
                                   CatgyName = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItemcategory.CatgyName,
                                   ItemGroup = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ItemGroup,
                                   PrtItem = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.PrtItem,
                                   PrtItemName = i.TmItem.TmItem.TmItem.TmItem.H.ItemName,
                                   UomBig = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.UomBig,
                                   UomBigName = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmUom.UomName,
                                   UomSmall = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.UomSmall,
                                   UomSmallName = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmUom.UomName,
                                   UomRelation = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.UomRelation,
                                   UomPur = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.UomPur,
                                   UomPurName = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmUom.UomName,
                                   UomStk = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.UomStk,
                                   UomStkName = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmUom.UomName,
                                   VisInSpn = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.VisInSpn,
                                   InSpn = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.InSpn,
                                   CashPur = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.CashPur,
                                   Fsn = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.Fsn,
                                   Abc = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.Abc,
                                   IsActive = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.IsActive,
                                   StProdCode = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.StProdCode,
                                   ContCode1 = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ContCode1,
                                   ContCode1Name = i.TmItem.TmItem.TmItem.J.ItemName,
                                   ContCode2 = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ContCode2,
                                   ContCode2Name = i.TmItem.TmItem.K.ItemName,
                                   PmxCode1 = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.PmxCode1,
                                   PmxCode1Name = i.TmItem.L.ItemName,
                                   PmxCode2 = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.PmxCode2,
                                   PmxCode2Name = i.N.ItemName,
                                   Prodnature = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ProdNature,
                                   Cess = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.Cess,
                                   Nature = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.Nature,
                                   Div = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.Div,
                                   ContCap = (Decimal)i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ContCap,
                                   ContWtConf = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ContWtConf,
                                   ExpDays = (Int32)i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.ExpDays,
                                   InReq = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.InReq,
                                   AcCode = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.AcCode,
                                   AcPostBase = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.AcPostBase,
                                   division = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.Division,
                                   HsnCode = i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.HsnCode,
                                   Doses = (Decimal)i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.Doses,
                                   NoOfPackets = (Int32)i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.NoOfPackets,
                                   StdWt = (Decimal)i.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.TmItem.StdWt
                                  

                               }).ToListAsync();
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

        [HttpPost("SaveUpdate")]
        public async Task<ActionResult<Response>> PostTmItem(ItemView tmItemView)
        {
            TmItem newtmItem = new TmItem();
            TmMeats newtmMeats = new TmMeats();
            var tmItems = await _context.TmItem.FindAsync(tmItemView.ItemCode);
            var tmMeats = await _context.TmMeats.FindAsync(tmItemView.MeatsCode);
           
                if (tmItems == null)
                {
                    if (tmItemView.Itemtype.StartsWith("CH") || tmItemView.Itemtype.StartsWith("FC"))
                    {
                        
                        newtmItem.ItemCode = tmItemView.ItemCode;
                        newtmItem.ItemName = tmItemView.ItemName;
                        newtmItem.ItemSpec = tmItemView.ItemSpec;
                        newtmItem.PartNo = tmItemView.PartNo;
                        newtmItem.ShortName = tmItemView.ShortName;
                        newtmItem.ItemType = tmItemView.Itemtype;
                        newtmItem.Category = tmItemView.Category;
                        newtmItem.ItemGroup = tmItemView.ItemGroup;
                        newtmItem.PrtItem = tmItemView.PrtItem;
                        newtmItem.UomBig = tmItemView.UomBig;
                        newtmItem.UomSmall = tmItemView.UomSmall;
                        newtmItem.UomRelation = tmItemView.UomRelation;
                        newtmItem.UomPur = tmItemView.UomPur;
                        newtmItem.UomStk = tmItemView.UomStk;
                        newtmItem.VisInSpn = tmItemView.VisInSpn;
                        newtmItem.InSpn = tmItemView.InSpn;
                        newtmItem.CashPur = tmItemView.CashPur;
                        newtmItem.Fsn = tmItemView.Fsn;
                        newtmItem.Abc = tmItemView.Abc;
                        newtmItem.IsActive = tmItemView.IsActive;
                        newtmItem.StProdCode = tmItemView.StProdCode;
                        newtmItem.ContCode1 = tmItemView.ContCode1;
                        newtmItem.ContCode2 = tmItemView.ContCode2;
                        newtmItem.PmxCode1 = tmItemView.PmxCode1;
                        newtmItem.PmxCode2 = tmItemView.PmxCode2;
                        newtmItem.ProdNature = tmItemView.Prodnature;
                        newtmItem.Cess = tmItemView.Cess;
                        newtmItem.Nature = tmItemView.Nature;
                        newtmItem.Div = tmItemView.Div;
                        newtmItem.ContCap = (Decimal)tmItemView.ContCap;
                        newtmItem.ContWtConf = tmItemView.ContWtConf;
                        newtmItem.ExpDays = (Int32)tmItemView.ExpDays;
                        newtmItem.InReq = tmItemView.InReq;
                        newtmItem.AcCode = tmItemView.AcCode;
                        newtmItem.AcPostBase = tmItemView.AcPostBase;
                        newtmItem.Division = tmItemView.division;
                        newtmItem.HsnCode = tmItemView.HsnCode;
                        newtmItem.Doses = (Decimal)tmItemView.Doses;
                        newtmItem.NoOfPackets = (Int32)tmItemView.NoOfPackets;
                        newtmItem.StdWt = (Decimal)tmItemView.StdWt;
                        newtmMeats.MeatsCode = tmItemView.MeatsCode;
                        newtmMeats.MeatsName = tmItemView.MeatsName;
                        newtmMeats.BranchCode = tmItemView.BranchCode;
                        newtmMeats.AcYearNo = tmItemView.AcYearNo;
                        newtmMeats.Uom = tmItemView.Uom;
                        newtmMeats.Section = tmItemView.Section;
                        newtmMeats.GradeSection = tmItemView.GradeSection;
                        newtmMeats.SectionName = tmItemView.SectionName;
                        _context.TmItem.Add(newtmItem);
                        _context.TmMeats.Add(newtmMeats);
                     //   await _context.SaveChangesAsync();
                    }
                    else
                      { 
                        newtmItem.ItemCode = tmItemView.ItemCode;
                        newtmItem.ItemName = tmItemView.ItemName;
                        newtmItem.ItemSpec = tmItemView.ItemSpec;
                        newtmItem.PartNo = tmItemView.PartNo;
                        newtmItem.ShortName = tmItemView.ShortName;
                        newtmItem.ItemType = tmItemView.Itemtype;
                        newtmItem.Category = tmItemView.Category;
                        newtmItem.ItemGroup = tmItemView.ItemGroup;
                        newtmItem.PrtItem = tmItemView.PrtItem;
                        newtmItem.UomBig = tmItemView.UomBig;
                        newtmItem.UomSmall = tmItemView.UomSmall;
                        newtmItem.UomRelation =(Decimal)tmItemView.UomRelation;
                        newtmItem.UomPur = tmItemView.UomPur;
                        newtmItem.UomStk = tmItemView.UomStk;
                        newtmItem.VisInSpn = tmItemView.VisInSpn;
                        newtmItem.InSpn = tmItemView.InSpn;
                        newtmItem.CashPur = tmItemView.CashPur;
                        newtmItem.Fsn = tmItemView.Fsn;
                        newtmItem.Abc = tmItemView.Abc;
                        newtmItem.IsActive = tmItemView.IsActive;
                        newtmItem.StProdCode = tmItemView.StProdCode;
                        newtmItem.ContCode1 = tmItemView.ContCode1;
                        newtmItem.ContCode2 = tmItemView.ContCode2;
                        newtmItem.PmxCode1 = tmItemView.PmxCode1;
                        newtmItem.PmxCode2 = tmItemView.PmxCode2;
                        newtmItem.ProdNature = tmItemView.Prodnature;
                        newtmItem.Cess = tmItemView.Cess;
                        newtmItem.Nature = tmItemView.Nature;
                        newtmItem.Div = tmItemView.Div;
                        newtmItem.ContCap = (Decimal)tmItemView.ContCap;
                        newtmItem.ContWtConf = tmItemView.ContWtConf;
                        newtmItem.ExpDays = (Int32)tmItemView.ExpDays;
                        newtmItem.InReq = tmItemView.InReq;
                        newtmItem.AcCode = tmItemView.AcCode;
                        newtmItem.AcPostBase = tmItemView.AcPostBase;
                        newtmItem.Division = tmItemView.division;
                        newtmItem.HsnCode = tmItemView.HsnCode;
                        newtmItem.Doses = (Decimal)tmItemView.Doses;
                        newtmItem.NoOfPackets = (Int32)tmItemView.NoOfPackets;
                        newtmItem.StdWt = (Decimal)tmItemView.StdWt;
                        _context.TmItem.Add(newtmItem);                 
                    
                    }
                
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (TmItemExists(tmItemView.ItemCode)|| TmMeatExists(tmItemView.MeatsCode))
                    {
                        return new Response { Status = "Conflict", Message = "Record Already Exist" };
                    }
                }

                return new Response { Status = "SUCCESSFULL", Message = "SAVED SUCCESSFULLY" };

            }

            if (tmItems != null)
            {
                if (tmItems.ItemType.StartsWith("CH") || tmItems.ItemType.StartsWith("FC"))
                {
                    tmItems.ItemName = tmItemView.ItemName;
                    tmItems.ItemSpec = tmItemView.ItemSpec;
                    tmItems.PartNo = tmItemView.PartNo;
                    tmItems.ShortName = tmItemView.ShortName;
                    tmItems.Category = tmItemView.Category;
                    tmItems.ItemGroup = tmItemView.ItemGroup;
                    tmItems.UomRelation = (Decimal)tmItemView.UomRelation;
                    tmItems.UomPur = tmItemView.UomPur;
                    tmItems.VisInSpn = tmItemView.VisInSpn;
                    tmItems.InSpn = tmItemView.InSpn;
                    tmItems.CashPur = tmItemView.CashPur;
                    tmItems.ContCode1 = tmItemView.ContCode1;
                    tmItems.ContCode2 = tmItemView.ContCode2;
                    tmItems.PmxCode1 = tmItemView.PmxCode1;
                    tmItems.PmxCode2 = tmItemView.PmxCode2;
                    tmItems.ProdNature = tmItemView.Prodnature;
                    tmItems.HsnCode = tmItemView.HsnCode;
                    tmItems.StdWt = (Decimal)tmItemView.StdWt;                    
                    tmMeats.MeatsName = tmItemView.MeatsName;
                    tmMeats.Uom = tmItemView.Uom;
                    tmMeats.Section = tmItemView.Section;
                    tmMeats.GradeSection = tmItemView.GradeSection;
                    tmMeats.SectionName = tmItemView.SectionName;
                    _context.Entry(newtmItem).State = EntityState.Modified;
                    _context.Entry(newtmMeats).State = EntityState.Modified;

                }
                else
                {
                   // newtmItem.ItemCode = tmItemView.ItemCode;
                    tmItems.ItemName = tmItemView.ItemName;
                    tmItems.ItemSpec = tmItemView.ItemSpec;
                    tmItems.PartNo = tmItemView.PartNo;
                    tmItems.ShortName = tmItemView.ShortName;
                    tmItems.Category = tmItemView.Category;
                    tmItems.ItemGroup = tmItemView.ItemGroup;
                    tmItems.UomRelation = (Decimal)tmItemView.UomRelation;
                    tmItems.UomPur = tmItemView.UomPur;                    
                    tmItems.VisInSpn = tmItemView.VisInSpn;
                    tmItems.InSpn = tmItemView.InSpn;
                    tmItems.CashPur = tmItemView.CashPur;
                    tmItems.ContCode1 = tmItemView.ContCode1;
                    tmItems.ContCode2 = tmItemView.ContCode2;
                    tmItems.PmxCode1 = tmItemView.PmxCode1;
                    tmItems.PmxCode2 = tmItemView.PmxCode2;
                    tmItems.ProdNature = tmItemView.Prodnature;
                    tmItems.HsnCode = tmItemView.HsnCode;
                    tmItems.StdWt = (Decimal)tmItemView.StdWt;
                    _context.Entry(tmItems).State = EntityState.Modified;
                }
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TmItemExists(tmItemView.ItemCode) || !TmMeatExists(tmItemView.MeatsCode))
                    {
                        return new Response { Status = "NotFound", Message = "Record Not Found" };
                    }
                    else
                    {
                        return new Response { Status = "Not Allowed", Message = "Update Not Allowed" };
                    }

                }

                return new Response { Status = "Updated", Message = "Record Updated Sucessfull" };
            }
            return null;
    }

     

        // POST: api/TmItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost("SaveUpdate")]
        //public async Task<ActionResult<Response>> PostTmItem(string branchcode, int acyear, string section, string gradesection, string sectionname, string itemcode, TmItem tmItem)
        //{
        //    if (itemcode != tmItem.ItemCode)
        //    {                
        //        if (tmItem.ItemType.StartsWith("CH") || tmItem.ItemType.StartsWith("FC"))
        //        {
        //            TmMeats tmmeats = new TmMeats();
        //            tmmeats.MeatsCode = tmItem.ItemCode;
        //            tmmeats.MeatsName = tmItem.ItemName;
        //            tmmeats.BranchCode = branchcode;
        //            tmmeats.AcYearNo = acyear;
        //            tmmeats.IsActive = tmItem.IsActive;
        //            tmmeats.Uom = tmItem.UomPur;
        //            tmmeats.Section = section;
        //            tmmeats.GradeSection = gradesection;
        //            tmmeats.SectionName = sectionname;
        //            _context.TmMeats.Add(tmmeats);
        //        }
        //        _context.TmItem.Add(tmItem);
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException)
        //        {
        //            if (TmItemExists(tmItem.ItemCode))
        //            {
        //                return new Response { Status = "Conflict", Message = "Record Already Exist" };
        //            }
        //        }

        //        return new Response { Status = "SUCCESSFULL", Message = "SAVED SUCCESSFULLY" };
        //    }
        //    else if (itemcode == tmItem.ItemCode)
        //    {
        //        TmMeats tmmeats = new TmMeats();
        //        TmItem updtmItem = new TmItem();
        //        if (tmItem.ItemType.StartsWith("CH") || tmItem.ItemType.StartsWith("FC"))
        //        {
        //            tmmeats.MeatsCode = tmItem.ItemCode;                  
        //            tmmeats.Uom = tmItem.UomPur;
        //            tmmeats.Section = section;
        //            tmmeats.GradeSection = gradesection;
        //            tmmeats.SectionName = sectionname;
                    

        //            _context.Entry(tmmeats).State = EntityState.Modified;      
                    
        //        }
        //       /* updtmItem.ItemCode = tmItem.ItemCode;
        //        updtmItem.ItemName = tmItem.ItemName;
        //        updtmItem.ItemSpec = tmItem.ItemSpec;
        //        updtmItem.PartNo = tmItem.PartNo;
        //        updtmItem.ShortName = tmItem.ShortName;
        //        updtmItem.Category = tmItem.Category;
        //        updtmItem.ItemGroup = tmItem.ItemGroup;
        //        updtmItem.UomRelation = tmItem.UomRelation;
        //        updtmItem.UomPur = tmItem.UomPur;
        //        updtmItem.VisInSpn = tmItem.VisInSpn;
        //        updtmItem.InSpn = tmItem.InSpn;
        //        updtmItem.CashPur = tmItem.CashPur;
        //        updtmItem.ContCode1 = tmItem.ContCode1;
        //        updtmItem.ContCode2 = tmItem.ContCode2;
        //        updtmItem.PmxCode1 = tmItem.PmxCode1;
        //        updtmItem.PmxCode2 = tmItem.PmxCode2;
        //        updtmItem.ProdNature = tmItem.ProdNature;
        //        updtmItem.HsnCode = tmItem.HsnCode;
        //        updtmItem.StdWt = tmItem.StdWt;*/
        //       _context.Entry(tmItem).State = EntityState.Modified;


        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TmItemExists(itemcode) || tmmeats.MeatsCode != itemcode)
        //        {
        //            return new Response { Status = "NotFound", Message = "Record Not Found" };
        //        }
        //            else { 
        //                return new Response { Status = "Not Allowed", Message = "Update Not Allowed" }; }
                
        //    }

        //    return new Response { Status = "Updated", Message = "Record Updated Sucessfull" }; 
        //    }
        //    return null;
        //}
       
        private bool TmItemExists(string id)
        {
            return _context.TmItem.Any(e => e.ItemCode == id);
        }

        private bool TmMeatExists(string id)
        {
            return _context.TmMeats.Any(e => e.MeatsCode == id);
        }
    }
}
