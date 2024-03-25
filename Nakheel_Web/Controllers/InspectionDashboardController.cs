using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.IncidentReport;
using Nakheel_Web.Models.InspectionMaster;
using Nakheel_Web.Models.Masters;
using Newtonsoft.Json;
using System.Text;
using static Nakheel_Web.Authentication.Common;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class InspectionDashboardController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        [Obsolete]
        public InspectionDashboardController(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            client = httpClientFactory.CreateClient("API");
            _hostingEnvironment = hostingEnvironment;
            _webHostEnvironment = webHostEnvironment;
        }

        #region [INSP DASHBOARD]
        public IActionResult Inspection_Dashboard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Get_Insp_Dashboard_Data(DashboardParam dash_Params)
        {

            Login_ LoginClass = GetLoginDetails();
            dash_Params.CreatedBy = LoginClass.Employee_Identity_Id;
            HttpResponseMessage response = client.PostAsync("InspectionDashboard/Get_Inspection_Dashboard", new StringContent(JsonConvert.SerializeObject(dash_Params), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Dash_Management deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Dash_Management>(customerJsonString)!;
            return Json(deserialized.Get_Data);
        }

        [HttpPost]
        public async Task<IActionResult> Get_Insp_Dashboard_Cat_Onchage_Data(DashboardParam dash_Params)
        {

            Login_ LoginClass = GetLoginDetails();
            dash_Params.CreatedBy = LoginClass.Employee_Identity_Id;
            HttpResponseMessage response = client.PostAsync("InspectionDashboard/Get_Insp_Dash_Cat_Onchange", new StringContent(JsonConvert.SerializeObject(dash_Params), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Dash_Management deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Dash_Management>(customerJsonString)!;
            return Json(deserialized.Get_Data);
        }

        [HttpPost]
        public async Task<IActionResult> Insp_Main_Dash_Card_View_Data(DashboardParam dash_Params)
        {
            M_Get_Insp_Dash_Card_View_Data deserialized = new M_Get_Insp_Dash_Card_View_Data();
            Login_ LoginClass = GetLoginDetails();
            HttpResponseMessage response = client.PostAsync("InspectionDashboard/Insp_Main_Dash_Card_View_Data", new StringContent(JsonConvert.SerializeObject(dash_Params), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Dash_Card_View_Data>(customerJsonString)!;
            return Json(deserialized.Get_All);
        }

        [HttpPost]
        public async Task<IActionResult> Insp_Dash_Card_View_Data(DashboardParam dash_Params)
        {
            M_Get_Insp_Dash_Card_View_Data deserialized = new M_Get_Insp_Dash_Card_View_Data();
            Login_ LoginClass = GetLoginDetails();
            dash_Params.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("InspectionDashboard/Get_Insp_Dash_Card_View_Data", new StringContent(JsonConvert.SerializeObject(dash_Params), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Dash_Card_View_Data>(customerJsonString)!;
            return Json(deserialized.Get_All);
        }

        [HttpPost]
        public async Task<IActionResult> Get_Insp_Sch_Dash_Card_View_Data(DashboardParam dash_Params)
        {
            M_Get_Insp_Dash_Card_View_Data deserialized = new M_Get_Insp_Dash_Card_View_Data();
            Login_ LoginClass = GetLoginDetails();
            dash_Params.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("InspectionDashboard/Get_Insp_Sch_Dash_Card_View_Data", new StringContent(JsonConvert.SerializeObject(dash_Params), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Dash_Card_View_Data>(customerJsonString)!;
            return Json(deserialized.Get_All);
        }

        [HttpPost]
        public async Task<IActionResult> Get_Insp_Walk_Dash_Card_View_Data(DashboardParam dash_Params)
        {
            M_Get_Insp_Dash_Card_View_Data deserialized = new M_Get_Insp_Dash_Card_View_Data();
            Login_ LoginClass = GetLoginDetails();
            dash_Params.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("InspectionDashboard/Get_Insp_Walk_Dash_Card_View_Data", new StringContent(JsonConvert.SerializeObject(dash_Params), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Dash_Card_View_Data>(customerJsonString)!;
            return Json(deserialized.Get_All);
        }

        [HttpPost]
        public async Task<IActionResult> Get_Insp_Type_Dash_Card_View_Data(DashboardParam dash_Params)
        {
            M_Get_Insp_Dash_Card_View_Data deserialized = new M_Get_Insp_Dash_Card_View_Data();
            Login_ LoginClass = GetLoginDetails();
            dash_Params.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("InspectionDashboard/Get_Insp_Type_Dash_Card_View_Data", new StringContent(JsonConvert.SerializeObject(dash_Params), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Dash_Card_View_Data>(customerJsonString)!;
            return Json(deserialized.Get_All);
        }

        [HttpPost]
        public async Task<IActionResult> Get_Insp_Service_Dash_Card_View_Data(DashboardParam dash_Params)
        {
            M_Get_Insp_Dash_Card_View_Data deserialized = new M_Get_Insp_Dash_Card_View_Data();
            Login_ LoginClass = GetLoginDetails();
            dash_Params.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("InspectionDashboard/Get_Insp_Service_Dash_Card_View_Data", new StringContent(JsonConvert.SerializeObject(dash_Params), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Dash_Card_View_Data>(customerJsonString)!;
            return Json(deserialized.Get_All);
        }

        #endregion


        #region [Master Dropdown]
        public async Task<IActionResult> Insp_Dash_LoadAllZone()
        {
            using (client)
            {
                Insp_Dash_ZoneWise Login = new Insp_Dash_ZoneWise();
                Login_ LoginClass = GetLoginDetails();
                Login.Value = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync("InspectionDashboard/Insp_Dash_Zone_GetAll", new StringContent(JsonConvert.SerializeObject(Login), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ZONE_MANAGEMENT deserialized = JsonConvert.DeserializeObject<GET_ZONE_MANAGEMENT>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Insp_Dash_LoadAllCommunity(string Zone_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                COMMUNITY_MANAGEMNT _UNIT = new COMMUNITY_MANAGEMNT
                {
                    Zone_Id = Zone_Id,
                    Business_Unit_Id = LoginClass.Employee_Identity_Id,
                };
                HttpResponseMessage response = client.PostAsync("InspectionDashboard/Insp_Dash_Community_GetAll", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_COMMUNITY_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_COMMUNITY_MANAGEMNT>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Insp_Dash_LoadAllBuilding(string Zone_Id, string Community_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                BUILDING_MANAGMENT _UNIT = new BUILDING_MANAGMENT
                {
                    Zone_Id = Zone_Id,
                    Community_Id = Community_Id,
                    Unique_Id = LoginClass.Employee_Identity_Id,
                };
                HttpResponseMessage response = client.PostAsync("InspectionDashboard/Insp_Dash_Building_GetAll", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_BUILDING_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_BUILDING_MANAGEMNT>(customerJsonString)!;
                return Json(deserialized!.Get_All_Sub);
            }
        }

        public async Task<IActionResult> Insp_Dash_Filter_HSETeam_GetAll()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("InspectionDashboard/Insp_Dash_Filter_HSETeam_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Dash_Filter_HSETeam deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Dash_Filter_HSETeam>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        #endregion

        #region [LOGIN_DETAILS]
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
        #endregion

    }
}
