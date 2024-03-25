using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.HandOverInsMaster;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using Nakheel_Web.Authentication;
using static Nakheel_Web.Authentication.Common;
using Nakheel_Web.Models.Masters;
using Microsoft.AspNetCore.Authorization;
using Nakheel_Web.Models.IncidentReport;
using Nakheel_Web.Models.ServiceProviderSignup_Dashboard;
using Nakheel_Web.Models.InspectionMaster;

namespace Nakheel_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private string conn;
        [Obsolete]
        public HomeController(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            client = httpClientFactory.CreateClient("API");
            _hostingEnvironment = hostingEnvironment;
            conn = configuration.GetConnectionString("DefaultConnection");
        }
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
        public async Task<IActionResult> Index()
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_HealthSafety_Model _UNIT = new()
                {
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Zone_Id = LoginClass.Zone_Id
                };
                HttpResponseMessage response = client.PostAsync("Accounts/Get_MainDashBoards_Details", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Dashboard_Details deserialized = JsonConvert.DeserializeObject<Get_Dashboard_Details>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region[Master Dashboard]
        public IActionResult MasterDashboard()
        {
            return View();
        }
        #endregion

        #region [Incident_Dashboard_Card_View]
        public async Task<IActionResult> Incident_Dashboard_Card_View(string CreatedBy, string Cid)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Incident_Report _UNIT = new()
                {
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Zone = LoginClass.Zone_Id,
                    Action = Cid,
                };
                HttpResponseMessage response = client.PostAsync("Incident_Report/Get_IncidentNotificationCardView", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_INCIDENT_NOTIF deserialized = JsonConvert.DeserializeObject<GET_INCIDENT_NOTIF>(customerJsonString)!;
                return Json(deserialized!.Get_All_Incident_Notification);
            }
        }


        public async Task<IActionResult> Obs_Dashboard_Card_View(string CreatedBy, string Cid)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Incident_Report _UNIT = new()
                {
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Zone_Id = LoginClass.Zone_Id,
                    Action = Cid,
                };
                HttpResponseMessage response = client.PostAsync("Incident_Observation_Report/Get_ObservationNotificationCardView", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_OBSERVATION_REPORT deserialized = JsonConvert.DeserializeObject<GET_OBSERVATION_REPORT>(customerJsonString)!;
                return Json(deserialized!.Get_All_Observation_Report);
            }
        }


        [HttpPost]
        public async Task<IActionResult> ServiceProv_Dashboard_Card_View(string CreatedBy, string Cid)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                ServiceProvSignup_Dashboard_Param _UNIT = new ServiceProvSignup_Dashboard_Param();
                if (LoginClass.Role_Id == "5" || LoginClass.Role_Id == "4")
                {
                    _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                    _UNIT.Zone_ID = LoginClass.Zone_Id;
                    _UNIT.Card_View_Id = Cid;
                }
                else
                {
                    _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                    _UNIT.Card_View_Id = Cid;
                }

                HttpResponseMessage response = client.PostAsync("Accounts/ServiceProv_Dashboard_Card_View", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                ServiceProvider_Dashboard deserialized = JsonConvert.DeserializeObject<ServiceProvider_Dashboard>(customerJsonString)!;
                return Json(deserialized.Get_Data1);
            }
        }
        #endregion


        #region [Control of Work Dashboard]
        [HttpPost]
        public async Task<IActionResult> SafetyPer_Main_Dash_Card_View_Data(DashboardParam dash_Params)
        {
            M_Get_SafetyPer_Dash_Card_View_Data deserialized = new M_Get_SafetyPer_Dash_Card_View_Data();
            Login_ LoginClass = GetLoginDetails();
            HttpResponseMessage response = client.PostAsync("MasterCommunity/SafetyPer_Main_Dash_Card_View_Data", new StringContent(JsonConvert.SerializeObject(dash_Params), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_SafetyPer_Dash_Card_View_Data>(customerJsonString)!;
            return Json(deserialized.Get_All);
        }


        [HttpPost]
        public async Task<IActionResult> Audit_Main_Dash_Card_View_Data(DashboardParam dash_Params)
        {
            M_Get_Audit_Dash_Card_View_Data deserialized = new M_Get_Audit_Dash_Card_View_Data();
            Login_ LoginClass = GetLoginDetails();
            HttpResponseMessage response = client.PostAsync("MasterCommunity/Audit_Main_Dash_Card_View_Data", new StringContent(JsonConvert.SerializeObject(dash_Params), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Dash_Card_View_Data>(customerJsonString)!;
            return Json(deserialized.Get_All);
        }
        #endregion
    }
}