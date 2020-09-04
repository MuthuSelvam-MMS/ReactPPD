using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactPPD.VM
{
    public class Reason
    {
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string ReasonCode { get; set; }
        public string ReasonName { get; set; }       
        public string DocType { get; set; }
        public string DocName { get; set; }
        public string GrpCode { get; set; }
        public string GrpName { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string IsActive { get; set; }
    }
}
