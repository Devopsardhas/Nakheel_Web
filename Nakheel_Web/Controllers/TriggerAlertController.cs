using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.Emergency;
using Nakheel_Web.Models.EMR_Drill;
using Nakheel_Web.Models.Masters;
using Newtonsoft.Json;
using System.Text;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class TriggerAlertController : Controller
    {
        private readonly HttpClient client;
        public TriggerAlertController(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("API");
        }
        #region [Login Details]
        private Login_ GetLoginDetails()
        {
            Login_ LoginClass = new Login_();
            var str = HttpContext.Session.GetString("Login");
            string Des = Decrypt(str!);
            if (Des != "")
            {
                LoginClass = JsonConvert.DeserializeObject<Login_>(Des)!;
            }
            return LoginClass;
        }

        private string Decrypt(string v)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region [Trigger Alert]
        public IActionResult TriggerAlert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Trigger_Alert_GetAll([FromBody] DataTableAjaxPostModel model)      
        {
            using (client)
            {
                Trigger_Alert_Get deserialized = new Trigger_Alert_Get();
                HttpResponseMessage response = client.PostAsync("TriggerAlert/TRG_Alert_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                deserialized = JsonConvert.DeserializeObject<Trigger_Alert_Get>(customerJsonString)!;
                if (deserialized.STATUS_CODE == "404")
                {
                    deserialized.Data = new List<Trigger_Alert>();
                }
                return Json(new
                {
                    draw = model.Draw,
                    recordsTotal = deserialized.RecordsTotal,
                    recordsFiltered = deserialized.RecordsFiltered,
                    data = deserialized.Data
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Trigger_Alert([FromBody] List<Trigger_Alert> model)
        {
            using (client)
            {
                string URL = "";
                URL = "TriggerAlert/TRG_Alert_Add";
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        public async Task<IActionResult> _View_Trigger_Alert(int Trigger_ID)
        {
            using (client)
            {
                Trigger_Alert _UNIT = new Trigger_Alert
                {
                    Trigger_ID = Convert.ToString(Trigger_ID),
                };

                HttpResponseMessage response = client.PostAsync("TriggerAlert/TRG_Alert_GetBy_Id", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Edit_Trigger_Alert deserialized = JsonConvert.DeserializeObject<Edit_Trigger_Alert>(customerJsonString)!;
                
                return PartialView("_View_Trigger_Alert", deserialized!.Data);
            }
        }

        public async Task<IActionResult> _View_Mitigation_Report(int Trigger_ID,int Remarks)
        {
            using (client)
            {
                Edit_Mitigation deserialized = new Edit_Mitigation();
                Mitigation_Report _UNIT = new Mitigation_Report
                {
                    Trigger_ID = Convert.ToString(Trigger_ID),
                    Remarks = Convert.ToString(Remarks),

                };
                if (Convert.ToString(Remarks) == "0")
                {
                    deserialized.Data = new Mitigation_Report()
                    {
                        Remarks = Convert.ToString(Remarks),
                        Trigger_ID = Convert.ToString(Trigger_ID)
                    };

                }
                else
                {
                    HttpResponseMessage response = client.PostAsync("MitigationReport/Alert_Report_GetBy_Id", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    deserialized = JsonConvert.DeserializeObject<Edit_Mitigation>(customerJsonString)!;
                }
                return PartialView("_View_Mitigation_Report", deserialized!.Data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Trigger_Alert_Assign(string Sub_Building_Id)
        {
            using (client)
            {

                TRG_Building_List unit = new TRG_Building_List()
                {
                    Sub_Building_Id = Sub_Building_Id
                };
                HttpResponseMessage response = client.PostAsync("TriggerAlert/TRG_Alert_Assignee", new StringContent(JsonConvert.SerializeObject(unit), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Sch_Assignee deserialized = JsonConvert.DeserializeObject<Get_Sch_Assignee>(customerJsonString)!;
                if(deserialized.Data == null)
                {
                    return Json("404");
                }
                else
                {
                    return Json(deserialized.Data!.ServiceProviders);
                }

            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_TRG_Assign_Action(TRIG_Assignee model)
        {
            using (client)
            {
                string URL = "";
                URL = "TriggerAlert/TRG_Assignee_Add";
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized);
            }
        }

        public async Task<IActionResult> LoadAllEmr_Category()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("TriggerAlert/Get_All_EMR_Category").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Emergency_Type deserialized = JsonConvert.DeserializeObject<Get_Emergency_Type>(customerJsonString)!;
                return Json(deserialized.Data);
            }
        }
        #endregion
    }
}
