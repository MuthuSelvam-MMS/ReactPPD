using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactPPD.VM
{
    public class CostCenter
    {
        public string CcCode { get; set; }
        public string CcName { get; set; }
        public string PrtCcCode { get; set; }
        public string PrtccName { get; set; }
        public string GprtCcCode { get; set; }
        public string GprtCcName { get; set; }
        public string CcType { get; set; }
        public string Descn { get; set; }
        public string IsActive { get; set; }
        public string RegionCode { get; set; }
    }
}
