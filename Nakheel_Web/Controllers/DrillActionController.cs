using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.AuditMaster;
using Nakheel_Web.Models;
using Newtonsoft.Json;
using System.Text;
using static Nakheel_Web.Authentication.Common;
using Nakheel_Web.Models.EMR_Drill;
using Nakheel_Web.Models.Masters;
using System.Reflection;
using Nakheel_Web.Models.IncidentReport;
using Microsoft.AspNetCore.Authorization;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class DrillActionController : Controller
    {
        private readonly HttpClient client;

        public DrillActionController(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("API");
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Drill_Action_Get_All([FromBody] DataTableAjaxPostModel model)
        {
            using (client)
            {
                Drill_Schedule_Get deserialized = new Drill_Schedule_Get();
                Login_ LoginClass = GetLoginDetails();
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync("DrillSchedule/Drill_Action_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                deserialized = JsonConvert.DeserializeObject<Drill_Schedule_Get>(customerJsonString)!;
                if (deserialized.STATUS_CODE == "404")
                {
                    deserialized.Data = new List<Drill_Schedule>();
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
        public async Task<IActionResult> Schedule_Action(Drill_Schedule_Param _Param)
        {
            Login_ LoginClass = GetLoginDetails();
            _Param.Created_By = LoginClass.Employee_Identity_Id;
            HttpResponseMessage response = client.PostAsync("DrillSchedule/Drill_Action_GetByID", new StringContent(JsonConvert.SerializeObject(_Param), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            Get_Drill_Details deserialized = JsonConvert.DeserializeObject<Get_Drill_Details>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "200" && deserialized.Data != null)
            {
               
                return PartialView(deserialized.Data);

            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Drill_Add_CorrAct(Improvement_Act Imp)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                Imp.Created_By = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync("DrillSchedule/Drill_Add_Closure", new StringContent(JsonConvert.SerializeObject(Imp), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Drill_Reassign(Improvement_Act Imp)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                Imp.Created_By = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync("DrillSchedule/Drill_Reassign", new StringContent(JsonConvert.SerializeObject(Imp), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized);
            }
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
    }
}
