using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.ControlOfWorkMaster;
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
    public class EmergencyAlert : Controller
    {
        private readonly HttpClient client;
        public EmergencyAlert(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("API");
        }
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddAlert(Drill_Schedule_Param model)
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Emr_Alert_GetAll([FromBody] DataTableAjaxPostModel model)
        {

            using (client)
            {
                EMR_Alert_Get deserialized = new EMR_Alert_Get();
                //Login_ LoginClass = GetLoginDetails();
                //model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync("EmergencyAlert/Emr_Alert_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                deserialized = JsonConvert.DeserializeObject<EMR_Alert_Get>(customerJsonString)!;
                if (deserialized.STATUS_CODE == "404")
                {
                    deserialized.Data = new List<EMR_Alert>();
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
        public async Task<IActionResult> Add_Emr_Alert([FromBody] EMR_Alert model)
        {
            using (client)
            {
                string URL = "";
                URL = "EmergencyAlert/Alert_M_Add";            
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        public async Task<IActionResult> _View_Emr_Alert(int EMR_Alert_ID)
        {
            using (client)
            {
                EMR_Alert _UNIT = new EMR_Alert
                {
                    EMR_Alert_ID = Convert.ToString(EMR_Alert_ID),
                };

                HttpResponseMessage response = client.PostAsync("EmergencyAlert/Emr_Alert_GetBy_Id", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Edit_EMR_ALERT deserialized = JsonConvert.DeserializeObject<Edit_EMR_ALERT>(customerJsonString)!;
                //TempData["Task_Id"] = deserialized!.Data[0].EMR_Alert_ID;
                return PartialView("_View_Emr_Alert", deserialized!.Data);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Alert_Mitigation_CHG(int Mitigation_ID, int Building_ID )
        {
            using (client)
            {
                Drill_Alert_Param _UNIT = new Drill_Alert_Param
                {
                    Mitigation_ID = Convert.ToString(Mitigation_ID),
                    Building_ID = Convert.ToString(Building_ID)
                };
                HttpResponseMessage response = client.PostAsync("EmergencyAlert/Alert_Mitigation_CHG", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Migitation_Loc deserialized = JsonConvert.DeserializeObject<Get_Migitation_Loc>(customerJsonString)!;
                return Json(deserialized!.Data);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Spot_Location_Delete(int EMR_Alert_ID)
        {
            using (client)
            {
                Drill_Alert_Param _UNIT = new Drill_Alert_Param
                {
                    EMR_Alert_ID = Convert.ToString(EMR_Alert_ID)
                };
                HttpResponseMessage response = client.PostAsync("EmergencyAlert/Alert_Spot_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Emr_Approve([FromBody] EMR_Approve_Reject model)
        {
            using (client)
            {
                string URL = "";
                URL = "EmergencyAlert/Alert_M_Approve";
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Emr_Reject([FromBody] EMR_Approve_Reject model)
        {
            using (client)
            {
                string URL = "";
                URL = "EmergencyAlert/Alert_M_Reject";
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public IActionResult _EmrLoadGoogleMap()
        {
            return PartialView();
        }
    }
}
