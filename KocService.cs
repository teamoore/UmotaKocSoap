using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UmotaKocSoap.Shared;

namespace UmotaKocSoap
{
    [ServiceContract]
    public class KocService
    {
        [OperationContract]
        public async Task<KocResponseDto> KocDataGetir()
        {
            var url = @"https://e700091-iflmap.hcisbt.eu2.hana.ondemand.com/http/kocec_fodepartment_dev?externalCode='0400_01'";
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue("S0023446469", "Vakif@1969");

            var data = await http.GetAsync(url);
            
            var result = data.Content.ReadAsStringAsync().Result;


            var response = JsonConvert.DeserializeObject<dynamic>(result)!;

            var koc = new KocResponseDto();
            koc.createdOn = response.d.results[0].createdOn;
            koc.cust_Dep_GM_defaultValue = response.d.results[0].cust_Dep_GM_defaultValue;
            koc.cust_Dep_GrpPresid = response.d.results[0].cust_Dep_GrpPresid;
            koc.externalCode = response.d.results[0].externalCode;

            return koc;

        }
    }
}
