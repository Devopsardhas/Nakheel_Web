using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.Emergency;
using Nakheel_Web.Models.EMR_Drill;
using Nakheel_Web.Models.IncidentReport;
using Nakheel_Web.Models.Masters;
using Newtonsoft.Json;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using static Nakheel_Web.Authentication.Common;
using static Nakheel_Web.Models.Emergency.Emergency_Dashboard;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class EmergencyController : Controller
    {
        private readonly HttpClient client;

        public EmergencyController(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("API");
        }


        #region Calendar
        public IActionResult Schedule()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ScheduleTbl(Drill_Calendar_Param _Param)
        {

            HttpResponseMessage response = client.PostAsync("DrillCalendar/Drill_GetAllSchedule", new StringContent(JsonConvert.SerializeObject(_Param), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            Get_Drill_Calendar deserialized = JsonConvert.DeserializeObject<Get_Drill_Calendar>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "200")
            {
                deserialized.Data!.Building_ID= _Param.Building_Id;
                return PartialView(deserialized.Data);
            }
            else
            {
                return PartialView(new Drill_Cal());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Schedule_Add_Update(Drill_Calendar model)
        {
            using (client)
            {
                if (ModelState.IsValid)
                {
                    Login_ LoginClass = GetLoginDetails();
                    model.Created_By = LoginClass.Employee_Identity_Id;
                    string URL = "";
                    if (model.Drill_Calendar_ID == "0")
                    {
                        URL = "DrillCalendar/Drill_Schedule_Add";
                    }
                    else
                    {
                        URL = "DrillCalendar/Drill_Schedule_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
                else
                {
                    return Json("400");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Schedule_Delete(Drill_Calendar model)
        {
            using (client)
            {
                if (ModelState.IsValid)
                {
                    Login_ LoginClass = GetLoginDetails();
                    model.Created_By = LoginClass.Employee_Identity_Id;
                    string URL = "DrillCalendar/Drill_Schedule_Delete";
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
                else
                {
                    return Json("400");
                }
            }
        }
        #endregion


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

        #region [ERT TEAM DETAILS]

        public  IActionResult Index(string id)
        {
            HttpContext.Session.SetString("ERT_Card_Id", id);
            return View();
        }

        public async Task<IActionResult> _GetERT_Team_Master(string Business_Unit, string Role, string Type, string Training_Status)
        {
            using (client)
            {
                var str = HttpContext.Session.GetString("ERT_Card_Id");
                ERT_Team_Details _UNIT = new ERT_Team_Details();
                _UNIT.Business_Unit = Business_Unit;
                _UNIT.Role = Role;
                _UNIT.Type = Type;
                _UNIT.Training_Status = Training_Status;
                _UNIT.Card_Id = str;
                HttpResponseMessage response = client.PostAsync("MasterCommunity/ERT_Team_Master_GetAll_Filter", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_ERT_Team_Details deserialized = JsonConvert.DeserializeObject<Get_ERT_Team_Details>(customerJsonString)!;
                return PartialView(deserialized!.Get_All);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddERT_TeamDetails([FromBody] ERT_Team_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                string URL = "";
                if (model.ERT_Id == "0" || model.ERT_Id == "" || model.ERT_Id == null)
                {
                    URL = "MasterCommunity/ERT_Team_Add";
                }
                else
                {
                    URL = "MasterCommunity/ERT_Team_Update";
                }
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        public async Task<IActionResult> _View_ERTTeam_list(string ERT_Id)
        {
            using (client)
            {
                ERT_Team_Details _UNIT = new ERT_Team_Details
                {
                    Status = "4",
                    ERT_Id = ERT_Id
                };
                HttpResponseMessage response = client.PostAsync("MasterCommunity/ERT_Team_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_ERT_Team_Details deserialized = JsonConvert.DeserializeObject<Get_ERT_Team_Details>(customerJsonString)!;
                return PartialView(deserialized.Get_ById);
            }
        }

        public async Task<IActionResult> Edit_ERTTeam_list(string ERT_Id)
        {
            using (client)
            {
                ERT_Team_Details _UNIT = new ERT_Team_Details
                {
                    Status = "6",
                    ERT_Id = ERT_Id
                };
                HttpResponseMessage response = client.PostAsync("MasterCommunity/ERT_Team_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_ERT_Team_Details deserialized = JsonConvert.DeserializeObject<Get_ERT_Team_Details>(customerJsonString)!;
                return Json(deserialized.Get_ById);
            }
        }

        public async Task<IActionResult> DeleteEdit_ERTTeam_list(string ERT_Id)
        {
            using (client)
            {
                ERT_Team_Details _UNIT = new ERT_Team_Details
                {
                    ERT_Id = ERT_Id
                };
                HttpResponseMessage response = client.PostAsync("MasterCommunity/ERT_Team_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        #endregion

        #region Dashboard

        public IActionResult Dashboard()
        {

            return View();
        }
        public async Task<IActionResult> GetEmergencyDashGraph(Emergency_Param entity)
        {
            using (client)
            {
                HttpResponseMessage response = client.PostAsync("Emergency_Dashbaord/Emergency_Dashboard_GetAll", new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Emergency_Dash_Get deserialized = JsonConvert.DeserializeObject<Emergency_Dash_Get>(customerJsonString)!;
                if (deserialized != null && deserialized.Status_Code == "200")
                {
                    return Json(deserialized.Get_Data1);
                }
                else
                {
                    return Json("Failed");
                }
            }
        }
        #endregion
    }
}
