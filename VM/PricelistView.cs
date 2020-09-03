using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactPPD.VM
{
    public class PricelistView
    {       
        public string DocNo { get; set; }
        public DateTimeOffset TransDate { get; set; }        
        public string PricingType { get; set; }
        public string PricingName { get; set; }
        public DateTimeOffset EffectFrom { get; set; }
        public DateTimeOffset EffectTo { get; set; }        
        public string BranchCode { get; set; }   
        public string BranchName { get; set; }
        public string ItemCode { get; set; }  
        public string ItemName { get; set; }
        public decimal Rate { get; set; }        
        public decimal DiscPer { get; set; }        
        public string IsActive { get; set; }       
        public int AcYearNo { get; set; }       
        public int DocIntNo { get; set; }        
        public string PartyCode { get; set; }  
        public string PartyName { get; set; }
        public string Age { get; set; }       
        public string ItemNature { get; set; } 
        public string ItemDescn { get; set; }
        public string CompanyCode { get; set; }  
        public string CompanyName { get; set; }
        public string DivCode { get; set; }   
        public string DivName { get; set; }
        public string RegZoneCode { get; set; }
        public string RegZoneName { get; set; }
        public string RegionCode { get; set; }  
        public string RegionName { get; set; }
        public string ZoneCode { get; set; }   
        public string ZoneName { get; set; }
        public string SubBrCode { get; set; }
        public string SubBrCodeName { get; set; }
        public string LogBrCode { get; set; }       
        public string RegCode { get; set; }        
        public string RegName { get; set; }
    }
}
