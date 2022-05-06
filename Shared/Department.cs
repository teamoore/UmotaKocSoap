using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmotaKocSoap.Shared
{
    [Serializable]
    public class Department
    {
        public string externalCode { get; set; }
        public DateTime startDate { get; set; }
        public string name_tr_TR { get; set; }
        public string cust_Dep_OrgUnt { get; set; }
        public string cust_Dep_Company { get; set; }
        public string status { get; set; }
    }
}
