using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models.InspectionMaster;
using Nakheel_Web.Models.Masters;
using Newtonsoft.Json;

namespace Nakheel_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class AuditInspCommonController : Controller
    {
        private readonly HttpClient client = new HttpClient();

        public AuditInspCommonController(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("API");
        }

        public async Task<IActionResult> Insp_Type_LoadAll()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("InspectionMaster/Insp_Type_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Type deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Type>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

    }
}
