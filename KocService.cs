using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using UmotaKocSoap.Shared;

namespace UmotaKocSoap
{
    [ServiceContract]
    [Serializable]
    public class KocService
    {
        [OperationContract]
        public async Task<Department> GetDepartment(string externalCode)
        {
            var url = @"https://e700091-iflmap.hcisbt.eu2.hana.ondemand.com/http/kocec_fodepartment_dev?externalCode='" + externalCode + "'";
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue("S0023446469", "Vakif@1969");
            
            var data = await http.GetAsync(url);

            var result = data.Content.ReadAsStringAsync().Result;

            var response = JsonConvert.DeserializeObject<dynamic>(result)!;

            var department = new Department();

            if (response.d.results.Count == 1)
            {
                department.externalCode = response.d.results[0].externalCode;
                department.startDate = response.d.results[0].startDate;
                department.name_tr_TR = response.d.results[0].name_tr_TR;
                department.cust_Dep_OrgUnt = response.d.results[0].cust_Dep_OrgUnt;
                department.cust_Dep_Company = response.d.results[0].cust_Dep_Company;
                department.status = response.d.results[0].status;
            }

            return department;
        }

        [OperationContract]
        public async Task<SaveResponseDto> SaveDepartment(Department department)
        {
            var url = @"https://e700091-iflmap.hcisbt.eu2.hana.ondemand.com/http/kocec_fodepartment_dev";
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue("S0023446469", "Vakif@1969");

            department.__metadata = new metadata();
            department.__metadata.uri = @"https://api012.successfactors.eu/odata/v2/FODepartment(externalCode='" + department.externalCode + "',startDate=datetime'" + department.startDate.Year + "-" + department.startDate.Month.ToString("00") + "-" + department.startDate.Day.ToString("00") + "T00:00:00')";
            department.__metadata.type = "SFOData.FODepartment";

            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            string jsonInString = JsonConvert.SerializeObject(department, microsoftDateFormatSettings);

            SaveResponseDto saveResponse = new SaveResponseDto();

            var data = await http.PostAsync(url, new StringContent(jsonInString, Encoding.UTF8, "application/json"));

            if (data.StatusCode == System.Net.HttpStatusCode.Created)
            {
                saveResponse.Status = "OK";
                saveResponse.Message = "";
            } 
            else
            {
                saveResponse.Status = "ERROR";
                var result = data.Content.ReadAsStringAsync().Result;
                saveResponse.Message = result;
            }

            return saveResponse;
        }

        [OperationContract]
        public async Task<SaveResponseDto> UpdateDepartment(Department department)
        {
            var url = @"https://e700091-iflmap.hcisbt.eu2.hana.ondemand.com/http/kocec_UPSERT_DEV";
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue("S0023446469", "Vakif@1969");

            department.__metadata = new metadata();
            department.__metadata.uri = "FODepartment";
            department.__metadata.type = "SFOData.FODepartment";

            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            string jsonInString = JsonConvert.SerializeObject(department, microsoftDateFormatSettings);

            SaveResponseDto saveResponse = new SaveResponseDto();

            var data = await http.PostAsync(url, new StringContent(jsonInString, Encoding.UTF8, "application/json"));

            string resultContent = data.Content.ReadAsStringAsync().Result;
            
            if (resultContent.Contains("status>OK<"))
            {
                saveResponse.Status = "OK";
                saveResponse.Message = "";
            }
            else
            {
                saveResponse.Status = "ERROR";
                saveResponse.Message = resultContent;
            }

            return saveResponse;
        }
    }
}
