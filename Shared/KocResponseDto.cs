using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmotaKocSoap.Shared
{
    public class KocResponseDto
    {
        public string cust_Dep_GrpPresid { get; set; }
        public string cust_Dep_GM_defaultValue { get; set; }

        public DateTime createdOn { get; set; }
        public string externalCode { get; set; }
    }
}
