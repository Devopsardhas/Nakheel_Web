using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.IncidentMaster;
using Nakheel_Web.Models.IncidentReport;
using Nakheel_Web.Models.Masters;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using static Nakheel_Web.Authentication.Common;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class IncidentController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string Knowledge_Share_File;
        private readonly string ApplicableReportPath;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;



        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        [Obsolete]
        public IncidentController(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            client = httpClientFactory.CreateClient("API");
            _hostingEnvironment = hostingEnvironment;
            Knowledge_Share_File = configuration.GetConnectionString("UploadKnowledgeShareFile");
            ApplicableReportPath = configuration.GetConnectionString("ApplicableReportPath");
            //Report_conn = configuration.GetConnectionString("ReportConnectionPath");
            _webHostEnvironment = webHostEnvironment;

        }
       
        public IActionResult Index()
        {
            return View();
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

        #region[INCIDENT MASTERS]

        #region[INCIDENT CATEGORY]
        public async Task<IActionResult> Incident_Category()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentMaster/Inc_Category_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_INCIDENT_CATEGORY deserialized = JsonConvert.DeserializeObject<GET_INCIDENT_CATEGORY>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddIncidentCategory(INCIDENT_CATEGORY model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Inc_Category_Id == "0")
                    {
                        URL = "IncidentMaster/Inc_Category_Add";
                    }
                    else
                    {
                        URL = "IncidentMaster/Inc_Category_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Incident_Category");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> Inc_Category_GetByID(string ID)
        {

            using (client)
            {
                INCIDENT_CATEGORY _UNIT = new INCIDENT_CATEGORY
                {
                    Inc_Category_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentMaster/Inc_Category_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_INCIDENT_CATEGORY deserialized = JsonConvert.DeserializeObject<GET_INCIDENT_CATEGORY>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> Inc_Category_Delete(string ID)
        {

            using (client)
            {
                INCIDENT_CATEGORY _UNIT = new INCIDENT_CATEGORY
                {
                    Inc_Category_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentMaster/Inc_Category_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_INCIDENT_CATEGORY deserialized = JsonConvert.DeserializeObject<GET_INCIDENT_CATEGORY>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region[INCIDENT TYPE]
        public async Task<IActionResult> Incident_Type()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentMaster/Inc_Type_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ICIDENT_TYPE deserialized = JsonConvert.DeserializeObject<GET_ICIDENT_TYPE>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddIncidentType(INCIDENT_TYPE model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Inc_Type_Id == "0")
                    {
                        URL = "IncidentMaster/Inc_Type_Add";
                    }
                    else
                    {
                        URL = "IncidentMaster/Inc_Type_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Incident_Type");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> IncidentType_GetByID(string ID)
        {

            using (client)
            {
                INCIDENT_TYPE _UNIT = new INCIDENT_TYPE
                {
                    Inc_Type_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentMaster/Inc_Type_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ICIDENT_TYPE deserialized = JsonConvert.DeserializeObject<GET_ICIDENT_TYPE>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> IncidentType_Delete(string ID)
        {

            using (client)
            {
                INCIDENT_TYPE _UNIT = new INCIDENT_TYPE
                {
                    Inc_Type_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentMaster/Inc_Type_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ICIDENT_TYPE deserialized = JsonConvert.DeserializeObject<GET_ICIDENT_TYPE>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region[INJURY TYPE]
        public async Task<IActionResult> Injury_Type()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentMaster/Injury_Type_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_INJURY_TYPE deserialized = JsonConvert.DeserializeObject<GET_INJURY_TYPE>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> AddInjuryType(INJURY_TYPE model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Injury_Type_Id == "0")
                    {
                        URL = "IncidentMaster/Injury_Type_Add";
                    }
                    else
                    {
                        URL = "IncidentMaster/Injury_Type_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Injury_Type");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> InjuryType_GetByID(string ID)
        {

            using (client)
            {
                INJURY_TYPE _UNIT = new INJURY_TYPE
                {
                    Injury_Type_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentMaster/Injury_Type_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_INJURY_TYPE deserialized = JsonConvert.DeserializeObject<GET_INJURY_TYPE>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> InjuryType_Delete(string ID)
        {

            using (client)
            {
                INJURY_TYPE _UNIT = new INJURY_TYPE
                {
                    Injury_Type_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentMaster/Injury_Type_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_INJURY_TYPE deserialized = JsonConvert.DeserializeObject<GET_INJURY_TYPE>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region[NATURE OF INJURY]
        public async Task<IActionResult> Nature_Injury_Illness()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentMaster/Nature_Injury_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_NATURE_OF_INJURY deserialized = JsonConvert.DeserializeObject<GET_NATURE_OF_INJURY>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> AddNatureInjury(NATURE_OF_INJURY model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Injury_Illness_Id == "0")
                    {
                        URL = "IncidentMaster/Nature_Injury_Add";
                    }
                    else
                    {
                        URL = "IncidentMaster/Nature_Injury_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Nature_Injury_Illness");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> NatureInjury_GetByID(string ID)
        {

            using (client)
            {
                NATURE_OF_INJURY _UNIT = new NATURE_OF_INJURY
                {
                    Injury_Illness_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentMaster/Nature_Injury_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_NATURE_OF_INJURY deserialized = JsonConvert.DeserializeObject<GET_NATURE_OF_INJURY>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> NatureInjury_Delete(string ID)
        {

            using (client)
            {
                NATURE_OF_INJURY _UNIT = new NATURE_OF_INJURY
                {
                    Injury_Illness_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentMaster/Nature_Injury_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_NATURE_OF_INJURY deserialized = JsonConvert.DeserializeObject<GET_NATURE_OF_INJURY>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region[UNSAFE ACT]
        public async Task<IActionResult> Unsafe_Act()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentMaster/Unsafe_Act_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_UNSAFE_ACT deserialized = JsonConvert.DeserializeObject<GET_UNSAFE_ACT>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> AddUnsafeAct(UNSAFE_ACT model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Unsafe_Act_Id == "0")
                    {
                        URL = "IncidentMaster/Unsafe_Act_Add";
                    }
                    else
                    {
                        URL = "IncidentMaster/Unsafe_Act_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Unsafe_Act");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> UnsafeAct_GetByID(string ID)
        {

            using (client)
            {
                UNSAFE_ACT _UNIT = new UNSAFE_ACT
                {
                    Unsafe_Act_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentMaster/Unsafe_Act_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_UNSAFE_ACT deserialized = JsonConvert.DeserializeObject<GET_UNSAFE_ACT>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> UnsafeAct_Delete(string ID)
        {

            using (client)
            {
                UNSAFE_ACT _UNIT = new UNSAFE_ACT
                {
                    Unsafe_Act_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentMaster/Unsafe_Act_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_UNSAFE_ACT deserialized = JsonConvert.DeserializeObject<GET_UNSAFE_ACT>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region[UNSAFE CONDITION]

        public async Task<IActionResult> Unsafe_Condition()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentMaster/Unsafe_Condition_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_UNSAFE_CONDITION deserialized = JsonConvert.DeserializeObject<GET_UNSAFE_CONDITION>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> AddUnsafeCondition(UNSAFE_CONDITION model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Unsafe_Condition_Id == "0")
                    {
                        URL = "IncidentMaster/Unsafe_Condition_Add";
                    }
                    else
                    {
                        URL = "IncidentMaster/Unsafe_Condition_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Unsafe_Condition");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> UnsafeCondition_GetByID(string ID)
        {

            using (client)
            {
                UNSAFE_CONDITION _UNIT = new UNSAFE_CONDITION
                {
                    Unsafe_Condition_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentMaster/Unsafe_Condition_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_UNSAFE_CONDITION deserialized = JsonConvert.DeserializeObject<GET_UNSAFE_CONDITION>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> UnsafeCondition_Delete(string ID)
        {

            using (client)
            {
                UNSAFE_CONDITION _UNIT = new UNSAFE_CONDITION
                {
                    Unsafe_Condition_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentMaster/Unsafe_Condition_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_UNSAFE_CONDITION deserialized = JsonConvert.DeserializeObject<GET_UNSAFE_CONDITION>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region[PERSONAL FACTOR]

        public async Task<IActionResult> Personal_factor()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentMaster/Personal_Factor_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_PERSONAL_FACTOR deserialized = JsonConvert.DeserializeObject<GET_PERSONAL_FACTOR>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPersonalFactor(PERSONAL_FACTOR model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Personal_Factor_Id == "0")
                    {
                        URL = "IncidentMaster/Personal_Factor_Add";
                    }
                    else
                    {
                        URL = "IncidentMaster/Personal_Factor_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Personal_factor");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> PersonalFactor_GetByID(string ID)
        {

            using (client)
            {
                PERSONAL_FACTOR _UNIT = new PERSONAL_FACTOR
                {
                    Personal_Factor_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentMaster/Personal_Factor_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_PERSONAL_FACTOR deserialized = JsonConvert.DeserializeObject<GET_PERSONAL_FACTOR>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> PersonalFactor_Delete(string ID)
        {

            using (client)
            {
                PERSONAL_FACTOR _UNIT = new PERSONAL_FACTOR
                {
                    Personal_Factor_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentMaster/Personal_Factor_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_PERSONAL_FACTOR deserialized = JsonConvert.DeserializeObject<GET_PERSONAL_FACTOR>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region [SYSTEM FACTOR]
        public async Task<IActionResult> System_factor()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentMaster/System_Factor_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SYSTEM_FACTOR deserialized = JsonConvert.DeserializeObject<GET_SYSTEM_FACTOR>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSystemFactor(SYSTEM_FACTOR model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.System_Factor_Id == "0")
                    {
                        URL = "IncidentMaster/System_Factor_Add";
                    }
                    else
                    {
                        URL = "IncidentMaster/System_Factor_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("System_factor");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> SystemFactor_GetByID(string ID)
        {

            using (client)
            {
                SYSTEM_FACTOR _UNIT = new SYSTEM_FACTOR
                {
                    System_Factor_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentMaster/System_Factor_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SYSTEM_FACTOR deserialized = JsonConvert.DeserializeObject<GET_SYSTEM_FACTOR>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> SystemFactor_Delete(string ID)
        {

            using (client)
            {
                SYSTEM_FACTOR _UNIT = new SYSTEM_FACTOR
                {
                    System_Factor_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentMaster/System_Factor_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SYSTEM_FACTOR deserialized = JsonConvert.DeserializeObject<GET_SYSTEM_FACTOR>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region[MECHANISM OF INJURY]
        public async Task<IActionResult> Mechanism_Of_Injury()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentSubMaster/Mechanism_Injury_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_MECHANISM_INJURY deserialized = JsonConvert.DeserializeObject<GET_MECHANISM_INJURY>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMechanismInjury(MECHANISM_INJURY model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Inc_Mechanism_Injury_Id == "0")
                    {
                        URL = "IncidentSubMaster/Mechanism_Injury_Add";
                    }
                    else
                    {
                        URL = "IncidentSubMaster/Mechanism_Injury_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Mechanism_Of_Injury");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> MechanismInjury_GetByID(string ID)
        {

            using (client)
            {
                MECHANISM_INJURY _UNIT = new MECHANISM_INJURY
                {
                    Inc_Mechanism_Injury_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentSubMaster/Mechanism_Injury_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_MECHANISM_INJURY deserialized = JsonConvert.DeserializeObject<GET_MECHANISM_INJURY>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> MechanismInjury_Delete(string ID)
        {

            using (client)
            {
                MECHANISM_INJURY _UNIT = new MECHANISM_INJURY
                {
                    Inc_Mechanism_Injury_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentSubMaster/Mechanism_Injury_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_MECHANISM_INJURY deserialized = JsonConvert.DeserializeObject<GET_MECHANISM_INJURY>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region[AGENCY OF INJURY]
        public async Task<IActionResult> Agency_Of_Injury()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentSubMaster/Agency_Injury_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_AGENCY_INJURY deserialized = JsonConvert.DeserializeObject<GET_AGENCY_INJURY>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAgencyInjury(AGENCY_INJURY model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Inc_Agency_Injury_Id == "0")
                    {
                        URL = "IncidentSubMaster/Agency_Injury_Add";
                    }
                    else
                    {
                        URL = "IncidentSubMaster/Agency_Injury_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Agency_Of_Injury");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> AgencyInjury_GetByID(string ID)
        {

            using (client)
            {
                AGENCY_INJURY _UNIT = new AGENCY_INJURY
                {
                    Inc_Agency_Injury_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentSubMaster/Agency_Injury_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_AGENCY_INJURY deserialized = JsonConvert.DeserializeObject<GET_AGENCY_INJURY>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> AgencyInjury_Delete(string ID)
        {

            using (client)
            {
                AGENCY_INJURY _UNIT = new AGENCY_INJURY
                {
                    Inc_Agency_Injury_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentSubMaster/Agency_Injury_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_AGENCY_INJURY deserialized = JsonConvert.DeserializeObject<GET_AGENCY_INJURY>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region[Health_Safety_Observation]
        public async Task<IActionResult> Health_Safety_Observation_Types()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentSubMaster/Health_Safety_Type_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HEALTH_SAFETY deserialized = JsonConvert.DeserializeObject<GET_HEALTH_SAFETY>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddHealthSafety(HEALTH_SAFETY_OBSERVATION model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Health_Safety_Type_Id == "0")
                    {
                        URL = "IncidentSubMaster/Health_Safety_Type_Add";
                    }
                    else
                    {
                        URL = "IncidentSubMaster/Health_Safety_Type_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Health_Safety_Observation_Types");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> HealthSafety_GetByID(string ID)
        {

            using (client)
            {
                HEALTH_SAFETY_OBSERVATION _UNIT = new HEALTH_SAFETY_OBSERVATION
                {
                    Health_Safety_Type_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentSubMaster/Health_Safety_Type_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HEALTH_SAFETY deserialized = JsonConvert.DeserializeObject<GET_HEALTH_SAFETY>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> HealthSafety_Delete(string ID)
        {

            using (client)
            {
                HEALTH_SAFETY_OBSERVATION _UNIT = new HEALTH_SAFETY_OBSERVATION
                {
                    Health_Safety_Type_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("IncidentSubMaster/Health_Safety_Type_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HEALTH_SAFETY deserialized = JsonConvert.DeserializeObject<GET_HEALTH_SAFETY>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        #endregion

        #region[Environment_Observation_Types]
        public async Task<IActionResult> Environment_Observation_Types()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentSubMaster/Environment_Type_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ENVIRONMENT_OBSERVATION deserialized = JsonConvert.DeserializeObject<GET_ENVIRONMENT_OBSERVATION>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEnvironment_Observation(ENVIRONMENT_OBSERVATION model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Environment_Type_Id == "0")
                    {
                        URL = "IncidentSubMaster/Environment_Type_Add";
                    }
                    else
                    {
                        URL = "IncidentSubMaster/Environment_Type_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Environment_Observation_Types");
            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> Environment_Type_GetByID(string ID)
        {

            using (client)
            {
                ENVIRONMENT_OBSERVATION _UNIT = new ENVIRONMENT_OBSERVATION
                {
                    Environment_Type_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("IncidentSubMaster/Environment_Type_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ENVIRONMENT_OBSERVATION deserialized = JsonConvert.DeserializeObject<GET_ENVIRONMENT_OBSERVATION>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        public async Task<IActionResult> Environment_Type_Delete(string ID)
        {

            using (client)
            {
                ENVIRONMENT_OBSERVATION _UNIT = new ENVIRONMENT_OBSERVATION
                {
                    Environment_Type_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("IncidentSubMaster/Environment_Type_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ENVIRONMENT_OBSERVATION deserialized = JsonConvert.DeserializeObject<GET_ENVIRONMENT_OBSERVATION>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region[Types_of_Environmental_Factor]
        public async Task<IActionResult> Types_of_Environmental_factor()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentSubMaster/Type_Environmental_Factor_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_TYPE_ENVIRONMENTAL_FACTOR deserialized = JsonConvert.DeserializeObject<GET_TYPE_ENVIRONMENTAL_FACTOR>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddType_Environmental_Factor(TYPE_ENVIRONMENTAL_FACTOR model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Type_Eml_Factor_Id == "0")
                    {
                        URL = "IncidentSubMaster/Type_Environmental_Factor_Add";
                    }
                    else
                    {
                        URL = "IncidentSubMaster/Type_Environmental_Factor_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Types_of_Environmental_factor");
            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> Type_Environmental_Factor_GetByID(string ID)
        {

            using (client)
            {
                TYPE_ENVIRONMENTAL_FACTOR _UNIT = new TYPE_ENVIRONMENTAL_FACTOR
                {
                    Type_Eml_Factor_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("IncidentSubMaster/Type_Environmental_Factor_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_TYPE_ENVIRONMENTAL_FACTOR deserialized = JsonConvert.DeserializeObject<GET_TYPE_ENVIRONMENTAL_FACTOR>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        public async Task<IActionResult> Type_Environmental_Factor_Delete(string ID)
        {

            using (client)
            {
                TYPE_ENVIRONMENTAL_FACTOR _UNIT = new TYPE_ENVIRONMENTAL_FACTOR
                {
                    Type_Eml_Factor_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("IncidentSubMaster/Type_Environmental_Factor_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_TYPE_ENVIRONMENTAL_FACTOR deserialized = JsonConvert.DeserializeObject<GET_TYPE_ENVIRONMENTAL_FACTOR>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        #endregion

        #region [Classification]
        public async Task<IActionResult> Classification()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentSubMaster/Classification_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_CLASSIFICATION deserialized = JsonConvert.DeserializeObject<GET_CLASSIFICATION>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddClassification(CLASSIFICATION model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Classification_Id == "0")
                    {
                        URL = "IncidentSubMaster/Classification_Add";
                    }
                    else
                    {
                        URL = "IncidentSubMaster/Classification_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Classification");
            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> Classification_GetByID(string ID)
        {

            using (client)
            {
                CLASSIFICATION _UNIT = new CLASSIFICATION
                {
                    Classification_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("IncidentSubMaster/Classification_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_CLASSIFICATION deserialized = JsonConvert.DeserializeObject<GET_CLASSIFICATION>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        public async Task<IActionResult> Classification_Delete(string ID)
        {

            using (client)
            {
                CLASSIFICATION _UNIT = new CLASSIFICATION
                {
                    Classification_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("IncidentSubMaster/Classification_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_CLASSIFICATION deserialized = JsonConvert.DeserializeObject<GET_CLASSIFICATION>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region[Types_of_Security_Incident]
        public async Task<IActionResult> Types_of_Security_Incident()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentSubMaster/Type_Security_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_TYPE_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_TYPE_SECURITY_INCIDENT>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddType_Security(TYPE_SECURITY_INCIDENT model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Inc_Type_Security_Id == "0")
                    {
                        URL = "IncidentSubMaster/Type_Security_Add";
                    }
                    else
                    {
                        URL = "IncidentSubMaster/Type_Security_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Types_of_Security_Incident");
            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> Type_Security_GetByID(string ID)
        {

            using (client)
            {
                TYPE_SECURITY_INCIDENT _UNIT = new TYPE_SECURITY_INCIDENT
                {
                    Inc_Type_Security_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("IncidentSubMaster/Type_Security_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_TYPE_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_TYPE_SECURITY_INCIDENT>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> Classification_GetAll()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentSubMaster/Classification_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_TYPE_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_TYPE_SECURITY_INCIDENT>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Type_Security_Delete(string ID)
        {
            using (client)
            {
                TYPE_SECURITY_INCIDENT _UNIT = new TYPE_SECURITY_INCIDENT
                {
                    Inc_Type_Security_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("IncidentSubMaster/Type_Security_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_TYPE_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_TYPE_SECURITY_INCIDENT>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        #endregion

        #region [Vehicle Accident Type]
        public async Task<IActionResult> Vehicle_Accident_Type()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentSubMaster/Vehicle_Accident_Type_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_VEHICLE_ACCIDENT_TYPE deserialized = JsonConvert.DeserializeObject<GET_VEHICLE_ACCIDENT_TYPE>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVehicleAccidentType(VEHICLE_ACCIDENT_TYPE model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Vehicle_Accident_Type_Id == "0")
                    {
                        URL = "IncidentSubMaster/Vehicle_Accident_Type_Add";
                    }
                    else
                    {
                        URL = "IncidentSubMaster/Vehicle_Accident_Type_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Vehicle_Accident_Type");
            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> Vehicle_Accident_Type_GetByID(string ID)
        {

            using (client)
            {
                VEHICLE_ACCIDENT_TYPE _UNIT = new VEHICLE_ACCIDENT_TYPE
                {
                    Vehicle_Accident_Type_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("IncidentSubMaster/Vehicle_Accident_Type_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_VEHICLE_ACCIDENT_TYPE deserialized = JsonConvert.DeserializeObject<GET_VEHICLE_ACCIDENT_TYPE>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        public async Task<IActionResult> Vehicle_Accident_Type_Delete(string ID)
        {

            using (client)
            {
                VEHICLE_ACCIDENT_TYPE _UNIT = new VEHICLE_ACCIDENT_TYPE
                {
                    Vehicle_Accident_Type_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("IncidentSubMaster/Vehicle_Accident_Type_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_VEHICLE_ACCIDENT_TYPE deserialized = JsonConvert.DeserializeObject<GET_VEHICLE_ACCIDENT_TYPE>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #endregion

        #region [INCIDENT MANAGEMENT]
        #region [IncidentReporting]
        public IActionResult IncidentDashboard()
        {
            return View();
        }
        public IActionResult IncidentDashboardNew()
        {
            return View();
        }
        public IActionResult IncidentReporting(string id)
        {
            HttpContext.Session.SetString("Card_Id", id);
            return View();
        }
        public IActionResult IncidentReportDetails()
        {
            return View();
        }
        public async Task<IActionResult> INC_Action_Closure()
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                Corrective_Assign_Action _UNIT = new()
                {
                    Person_Responsible_Id = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };

                HttpResponseMessage response = client.PostAsync("Incident_Report/Inc_Action_Closure_Get_All", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Corrective_Assign_Action deserialized = JsonConvert.DeserializeObject<Get_Corrective_Assign_Action>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> INC_Action_Approval()
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                Corrective_Assign_Action _UNIT = new()
                {
                    Person_Responsible_Id = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("Incident_Report/Action_Closure_Get_All_Approval", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Corrective_Assign_Action deserialized = JsonConvert.DeserializeObject<Get_Corrective_Assign_Action>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> _ViewIncidentClosureEv(int Corrective_Action_Id)
        {
            using (client)
            {
                Corrective_Assign_Action _UNIT = new()
                {
                    Corrective_Action_Id = Convert.ToString(Corrective_Action_Id),
                };

                HttpResponseMessage response = client.PostAsync("Incident_Report/Incident_Closure_Evidence", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Evidence_Upload_Photos deserialized = JsonConvert.DeserializeObject<Get_Evidence_Upload_Photos>(customerJsonString)!;
                return PartialView("_ViewIncidentClosureEv", deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> _ViewIncidentClosureEv_edit(int Corrective_Action_Id)
        {
            using (client)
            {
                Corrective_Assign_Action _UNIT = new()
                {
                    Corrective_Action_Id = Convert.ToString(Corrective_Action_Id),
                };

                HttpResponseMessage response = client.PostAsync("Incident_Report/Incident_Closure_Evidence", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Evidence_Upload_Photos deserialized = JsonConvert.DeserializeObject<Get_Evidence_Upload_Photos>(customerJsonString)!;
                return PartialView("_ViewIncidentClosureEv_edit", deserialized!.Get_All);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update_Corrective_Assign_Action([FromBody] Corrective_Assign_Action _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.Created_By = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("Incident_Report/Update_Corrective_Assign_Action", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Re_Assign_Corrective_Assign_Action([FromBody] Corrective_Assign_Action _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.Created_By = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("Incident_Report/Re_Assign_Corrective_Assign_Action", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update_Corrective_Assign_Status([FromBody] Corrective_Assign_Action _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.Created_By = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("Incident_Report/Update_Corrective_Assign_Status", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddIncidentReporting([FromBody] M_Incident_Report _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                List<M_Inc_Notification_Photos> pHOTOs = new List<M_Inc_Notification_Photos>();
                List<M_Inc_Notification_Videos> Videos = new List<M_Inc_Notification_Videos>();
                var str = HttpContext.Session.GetString("Photo");
                if (str != null)
                {
                    string[] photo = JsonConvert.DeserializeObject<string[]>(str)!;
                    if (photo != null)
                    {
                        for (int i = 0; i < photo.Length; i++)
                        {
                            M_Inc_Notification_Photos pHOTO = new M_Inc_Notification_Photos
                            {
                                Photo_File_Path = photo[i],
                                //CreatedBy = LoginClass.Employee_Identity_Id
                            };

                            pHOTOs.Add(pHOTO);
                        }
                    }
                    //HttpContext.Session.Clear();
                    HttpContext.Session.Remove("Photo");
                }
                var strVideos = HttpContext.Session.GetString("Video");
                if (strVideos != null)
                {
                    string[] videos = JsonConvert.DeserializeObject<string[]>(strVideos)!;
                    if (videos != null)
                    {
                        for (int i = 0; i < videos.Length; i++)
                        {
                            M_Inc_Notification_Videos vIdeos = new M_Inc_Notification_Videos
                            {
                                Video_File_Path = videos[i],
                                //CreatedBy = LoginClass.Employee_Identity_Id
                            };

                            Videos.Add(vIdeos);
                        }
                    }
                    //HttpContext.Session.Clear();
                    HttpContext.Session.Remove("Video");
                }
                _UNIT.L_Inc_Notification_Photos = pHOTOs;
                _UNIT.L_Inc_Notification_Videos = Videos;
                HttpResponseMessage response = client.PostAsync("Incident_Report/Inc_Category_Add", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ICIDENT_TYPE deserialized = JsonConvert.DeserializeObject<GET_ICIDENT_TYPE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIncStatus([FromBody] M_Incident_Report _UNIT)
        {
            using (client)
            {
                if (_UNIT.Status == "8")
                {
                    int incid = Convert.ToInt32(_UNIT.Inc_Id);
                    ReportController reportController = new ReportController(configuration, _webHostEnvironment);
                    byte[] bytes = reportController.Inc_Knowledge_Share_Report_Mail_Aattc(incid, _UNIT.Unique_Id!);
                    _UNIT.Mail_Remarks = bytes;
                }
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("Incident_Report/Inc_Status_Update", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ICIDENT_TYPE deserialized = JsonConvert.DeserializeObject<GET_ICIDENT_TYPE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIncStatus_IsReq([FromBody] M_Incident_Report _UNIT)
        {
            using (client)
            {
                if (_UNIT.Is_Investigation_Req == "Yes" || _UNIT.Is_Investigation_Req == "No" || _UNIT.Add_Description_2 == "No")
                {
                    int incid = Convert.ToInt32(_UNIT.Inc_Id);
                    ReportController reportController = new ReportController(configuration, _webHostEnvironment);
                    byte[] bytes = reportController.Incident_Report_Mail_Aattc(incid, _UNIT.Unique_Id!);
                    _UNIT.Mail_Remarks = bytes;
                }

                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("Incident_Report/Inc_Status_Update_IsReq", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ICIDENT_TYPE deserialized = JsonConvert.DeserializeObject<GET_ICIDENT_TYPE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        public async Task<IActionResult> GetIncidentReporting([FromBody] DataTableAjaxPostModel model)
        {
            var str = HttpContext.Session.GetString("Card_Id");
            var data = new List<object>();
            GET_INCIDENT_NOTIF deserialized = new GET_INCIDENT_NOTIF();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;
            model.Zone_Id = LoginClass.Zone_Id;
            model.Card_Id = str;
            HttpResponseMessage response = client.PostAsync("Incident_Report/Inc_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<GET_INCIDENT_NOTIF>(customerJsonString)!;
            if (deserialized.Get_All_Incident_Notification != null)
            {
                foreach (var item in deserialized.Get_All_Incident_Notification)
                {
                    item.Role_Id = LoginClass.Role_Id;
                    item.Login_Id = LoginClass.Employee_Identity_Id;
                    item.CreatedBy = LoginClass.Employee_Identity_Id;
                }
            }

            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All_Incident_Notification = new List<M_Incident_Report>();
            }

            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All_Incident_Notification!.ToList()
            });
        }

      
        public async Task<IActionResult> _ViewIncidentReport(int Inc_Id)
        {
            using (client)
            {
                M_Incident_Report _UNIT = new M_Incident_Report
                {
                    Inc_Id = Convert.ToString(Inc_Id),
                };

                HttpResponseMessage response = client.PostAsync("Incident_Report/Inc_GetBy_Id", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_INCIDENT_NOTIF deserialized = JsonConvert.DeserializeObject<GET_INCIDENT_NOTIF>(customerJsonString)!;
                return PartialView("_ViewIncidentReport", deserialized!.Get_Incident_Notification);
            }
        }

        public async Task<IActionResult> _ViewIncidentReportApproveProcess(int Inc_Id)
        {
            using (client)
            {
                M_Incident_Report _UNIT = new M_Incident_Report
                {
                    Inc_Id = Convert.ToString(Inc_Id),
                };

                HttpResponseMessage response = client.PostAsync("Incident_Report/Inc_GetBy_Id", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_INCIDENT_NOTIF deserialized = JsonConvert.DeserializeObject<GET_INCIDENT_NOTIF>(customerJsonString)!;
                return PartialView("_ViewIncidentReportApproveProcess", deserialized!.Get_Incident_Notification);
            }
        }

        public IActionResult _ViewBodyParts()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddInvestigationTeam([FromBody] M_Inc_Investigation_Team _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("Incident_Report/Inc_Investigation_Team", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ICIDENT_TYPE deserialized = JsonConvert.DeserializeObject<GET_ICIDENT_TYPE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddKnowledgeShare([FromBody] Inc_Knowledge_Share _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("Incident_Report/Inc_Add_Knowledge_Share", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ICIDENT_TYPE deserialized = JsonConvert.DeserializeObject<GET_ICIDENT_TYPE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddKnowledgeShare_No([FromBody] Inc_Knowledge_Share _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("Incident_Report/Inc_Add_Knowledge_Share_No", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ICIDENT_TYPE deserialized = JsonConvert.DeserializeObject<GET_ICIDENT_TYPE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCorrective_Assign_Action([FromBody] M_Corrective_Assign_Action _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                foreach (var item in _UNIT.L_Corrective_Assign_Action!)
                {
                    item.Created_By = LoginClass.Employee_Identity_Id;
                }
                HttpResponseMessage response = client.PostAsync("Incident_Report/Inc_AddCorrective_Assign_Action", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ICIDENT_TYPE deserialized = JsonConvert.DeserializeObject<GET_ICIDENT_TYPE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Corrective_Action([FromBody] Add_Corrective_Action _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.Created_By = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                List<List_Add_Corrective_Action> pHOTOs = new List<List_Add_Corrective_Action>();
                var str = HttpContext.Session.GetString("EvidencePhoto");
                if (str != null)
                {
                    string[] photo = JsonConvert.DeserializeObject<string[]>(str)!;
                    if (photo != null)
                    {
                        for (int i = 0; i < photo.Length; i++)
                        {
                            List_Add_Corrective_Action pHOTO = new List_Add_Corrective_Action
                            {
                                Photo_File_Path = photo[i],
                                Created_By = LoginClass.Employee_Identity_Id,
                                Inc_Id = _UNIT.Inc_Id,
                                Action_Taken_Description = _UNIT.Action_Taken_Description,
                                Corrective_Action_Id = _UNIT.Corrective_Action_Id,
                                Status = _UNIT.Status
                            };

                            pHOTOs.Add(pHOTO);
                        }
                    }
                    HttpContext.Session.Remove("EvidencePhoto");
                }
                _UNIT.L_List_Add_Corrective_Action = pHOTOs;
                HttpResponseMessage response = client.PostAsync("Incident_Report/Inc_Add_Corrective_Evidence_Upload", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddReject_Reason_Action([FromBody] M_Incident_Notification_Reject _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                _UNIT.Rejected_By = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync("Incident_Report/Inc_AddReject_Reason_Action", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        #endregion

        #region [Observation]
        public IActionResult ObservationDashboard()
        {

            return View();
        }
        public IActionResult ObservationReporting(string id)
        {
            HttpContext.Session.SetString("Obs_Card_Id", id);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit_Observation_Reporting([FromBody] M_Observation_Corrective_Action _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                //M_Observation_Corrective_Action _UNIT = new()
                //{
                //    Inc_Obser_Report_Id = Convert.ToString(Inc_Obser_Report_Id)
                //};

                HttpResponseMessage response = client.PostAsync("Incident_Observation_Report/Inc_Observation_Report_GetByID", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_OBSERVATION_REPORT deserialized = JsonConvert.DeserializeObject<GET_OBSERVATION_REPORT>(customerJsonString)!;
                return Json(deserialized!.Get_Observation_Report);
            }
        }
        public async Task<IActionResult> OBS_Action_Closure()
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Observation_Corrective_Action _UNIT = new()
                {
                    Responsible_Id = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id,
                    Zone_Id = LoginClass.Zone_Id
                };

                HttpResponseMessage response = client.PostAsync("Incident_Observation_Report/Obs_Action_Closure_Get_All", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Observation_Corrective_Action deserialized = JsonConvert.DeserializeObject<Get_Observation_Corrective_Action>(customerJsonString)!;
                //foreach (var item in deserialized.Get_All!)
                //{
                //    item.Role_Id = LoginClass.Role_Id;
                //}
                return View(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Add_Observation_Corrective_Action([FromBody] M_Observation_Corrective_Action _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                List<Observation_Corrective_Action> pHOTOs = new List<Observation_Corrective_Action>();
                var str = HttpContext.Session.GetString("EvidencePhoto");
                if (str != null)
                {
                    string[] photo = JsonConvert.DeserializeObject<string[]>(str)!;
                    if (photo != null)
                    {
                        for (int i = 0; i < photo.Length; i++)
                        {
                            Observation_Corrective_Action pHOTO = new Observation_Corrective_Action
                            {
                                Photo_File_Path = photo[i],
                                CreatedBy = LoginClass.Employee_Identity_Id,
                                Inc_Observation_Id = _UNIT.Inc_Observation_Id,
                                Description_Action = _UNIT.Description_Action,
                                Obs_Corrective_Action_Id = _UNIT.Obs_Corrective_Action_Id,
                                Role_Id = LoginClass.Role_Id,
                                Status = "1"
                            };

                            pHOTOs.Add(pHOTO);
                        }
                    }
                    HttpContext.Session.Remove("EvidencePhoto");
                }
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                _UNIT.L_Observation_Corrective_Action = pHOTOs;
                HttpResponseMessage response = client.PostAsync("Incident_Observation_Report/Add_Observation_Corrective_Action", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Observation_Corrective_Action deserialized = JsonConvert.DeserializeObject<Get_Observation_Corrective_Action>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        public async Task<IActionResult> GetIncidentTypeGraph(DashboardParam entity)
        {
            HttpResponseMessage response = client.PostAsync("Dashboard/Get_Incident_Dashboard", new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            Incident_Dashboard deserialized = JsonConvert.DeserializeObject<Incident_Dashboard>(customerJsonString)!;
            if (deserialized != null && deserialized.Status_Code == "200")
            {
                return Json(deserialized.Get_Data);
            }
            else
            {
                return Json("Failed");
            }
        }

        public async Task<IActionResult> GetIncidentTypeGraph1(DashboardParam entity)
        {
            HttpResponseMessage response = client.PostAsync("Dashboard/Get_Incident_Dashboard1", new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            Incident_Dashboard deserialized = JsonConvert.DeserializeObject<Incident_Dashboard>(customerJsonString)!;
            if (deserialized != null && deserialized.Status_Code == "200")
            {
                return Json(deserialized.Get_Data1);
            }
            else
            {
                return Json("Failed");
            }
        }
        public async Task<IActionResult> Get_Observation_Dashboard(DashboardParam entity)
        {
            Login_ LoginClass = GetLoginDetails();
            entity.CreatedBy = LoginClass.Employee_Identity_Id;
            HttpResponseMessage response = client.PostAsync("Dashboard/Get_Observation_Dashboard", new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            Observation_Dashboard deserialized = JsonConvert.DeserializeObject<Observation_Dashboard>(customerJsonString)!;
            if (deserialized != null && deserialized.Status_Code == "200")
            {
                return Json(deserialized.Get_Data);
            }
            else
            {
                return Json("Failed");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddObservationTeam([FromBody] M_Inc_Observation_Team _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                //_UNIT.Inc_Obser_Report_Id = _UNIT.Inc_Obser_Report_Id;
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("Incident_Observation_Report/Inc_Observation_Team", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ICIDENT_TYPE deserialized = JsonConvert.DeserializeObject<GET_ICIDENT_TYPE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Incident_Dashboard_Card_View(DashboardParam dash_Params)
        {
            HttpResponseMessage response = client.PostAsync("Dashboard/Incident_Dashboard_Card_View", new StringContent(JsonConvert.SerializeObject(dash_Params), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            GET_INCIDENT_NOTIF deserialized = JsonConvert.DeserializeObject<GET_INCIDENT_NOTIF>(customerJsonString)!;
            return Json(deserialized.Get_Data);
        }
        [HttpPost]
        public async Task<IActionResult> Incident_Dashboard_Trend_MonthWise(DashboardParam dash_Params)
        {
            HttpResponseMessage response = client.PostAsync("Dashboard/Inc_Dash_Card_View", new StringContent(JsonConvert.SerializeObject(dash_Params), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            Inc_Dash_Analytics_Trend deserialized = JsonConvert.DeserializeObject<Inc_Dash_Analytics_Trend>(customerJsonString)!;
            return Json(deserialized.Get_Data);
        }

        [HttpPost]
        public async Task<IActionResult> Observation_Dashboard_Card_View(DashboardParam dash_Params)
        {
            Login_ LoginClass = GetLoginDetails();
            dash_Params.CreatedBy = LoginClass.Employee_Identity_Id;
            HttpResponseMessage response = client.PostAsync("Dashboard/Observation_Dashboard_Card_View", new StringContent(JsonConvert.SerializeObject(dash_Params), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            GET_OBSERVATION_REPORT deserialized = JsonConvert.DeserializeObject<GET_OBSERVATION_REPORT>(customerJsonString)!;
            return Json(deserialized.Get_All_Observation_Report);
        }

        [HttpPost]
        public async Task<IActionResult> GetObservationReporting([FromBody] DataTableAjaxPostModel model)
        {
            var str = HttpContext.Session.GetString("Obs_Card_Id");
            var data = new List<object>();
            GET_OBSERVATION_REPORT deserialized = new GET_OBSERVATION_REPORT();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;
            model.Zone_Id = LoginClass.Zone_Id;
            model.Card_Id = str;
            HttpResponseMessage response = client.PostAsync("Incident_Observation_Report/Inc_Observation_Report_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<GET_OBSERVATION_REPORT>(customerJsonString)!;

            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All_Observation_Report = new List<M_IncidentObservationReport>();
            }
            else
            {
                foreach (var item in deserialized.Get_All_Observation_Report!)
                {
                    item.Role_Id = LoginClass.Role_Id;
                }
            }

            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All_Observation_Report!.ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddObservationReport([FromBody] M_IncidentObservationReport model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                List<M_Inc_Observation_Photos> pHOTOs = new List<M_Inc_Observation_Photos>();
                List<M_Inc_Observation_Videos> Videos = new List<M_Inc_Observation_Videos>();
                var obser = HttpContext.Session.GetString("Photo");
                if (obser != null)
                {
                    string[] photo = JsonConvert.DeserializeObject<string[]>(obser)!;
                    if (photo != null)
                    {
                        for (int i = 0; i < photo.Length; i++)
                        {
                            M_Inc_Observation_Photos pHOTO = new M_Inc_Observation_Photos
                            {
                                Photo_File_Path = photo[i],
                                //CreatedBy = LoginClass.Employee_Identity_Id
                            };

                            pHOTOs.Add(pHOTO);
                        }
                    }
                    //HttpContext.Session.Clear();
                }

                var obsVideos = HttpContext.Session.GetString("Video");
                if (obsVideos != null)
                {
                    string[] videos = JsonConvert.DeserializeObject<string[]>(obsVideos)!;
                    if (videos != null)
                    {
                        for (int i = 0; i < videos.Length; i++)
                        {
                            M_Inc_Observation_Videos vIdeos = new M_Inc_Observation_Videos
                            {
                                Video_File_Path = videos[i],
                                //CreatedBy = LoginClass.Employee_Identity_Id
                            };

                            Videos.Add(vIdeos);
                        }
                    }
                    //HttpContext.Session.Clear();
                }
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                model.Role_Id = LoginClass.Role_Id;
                model.L_Inc_Observation_Photos = pHOTOs;
                model.L_Inc_Observation_Videos = Videos;

                if (model.Inc_Obser_Report_Id == "0")//Inc_Obser_Report_Id
                {
                    URL = "Incident_Observation_Report/Inc_Observation_Report_Add";
                }
                else
                {
                    URL = "Incident_Observation_Report/Inc_Observation_Report_Update";
                }

                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> _ViewObservationReport(int Inc_Obser_Report_Id)
        {
            using (client)
            {
                M_IncidentObservationReport _UNIT = new M_IncidentObservationReport
                {
                    Inc_Obser_Report_Id = Convert.ToString(Inc_Obser_Report_Id),
                };

                HttpResponseMessage response = client.PostAsync("Incident_Observation_Report/Inc_Observation_Report_GetByID", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_OBSERVATION_REPORT deserialized = JsonConvert.DeserializeObject<GET_OBSERVATION_REPORT>(customerJsonString)!;
                return PartialView("_ViewObservationReport", deserialized!.Get_Observation_Report);
            }
        }
        public async Task<IActionResult> _ViewSupervisorObservationReport(int Inc_Obser_Report_Id)
        {
            Login_ LoginClass = GetLoginDetails();
            using (client)
            {
                M_Observation_Corrective_Action _UNIT = new M_Observation_Corrective_Action
                {
                    Inc_Obser_Report_Id = Convert.ToString(Inc_Obser_Report_Id),
                };
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("Incident_Observation_Report/Inc_Observation_Supervisor_GetByID", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Observation_Corrective_Action deserialized = JsonConvert.DeserializeObject<Get_Observation_Corrective_Action>(customerJsonString)!;
                return PartialView("_ViewSupervisorObservationReport", deserialized!.Get_Obs_Corrective_Action);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update_Observation_Corrective_Action([FromBody] Observation_Corrective_Action _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("Incident_Observation_Report/Update_Observation_Corrective_Action", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Obs_Re_Assign_Corrective_Assign_Action([FromBody] M_Observation_Corrective_Action _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("Incident_Observation_Report/Re_Assign_Corrective_Assign_Action", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update_Observation_Corrective_Action_HSE([FromBody] Observation_Corrective_Action _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("Incident_Observation_Report/Update_Observation_Corrective_Action_HSE", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update_Observation_Corrective_Action_HSE_Reject([FromBody] M_Observation_Corrective_Action _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                _UNIT.RejectedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync("Incident_Observation_Report/Update_Observation_Corrective_Action_HSE_Reject", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateObservationPriority([FromBody] M_IncidentObservationReport _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("Incident_Observation_Report/UpdateObservationPriority", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;

                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update_Obs_Category_Positive([FromBody] M_IncidentObservationReport _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("Incident_Observation_Report/Update_Obs_Category_Positive", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;

                return Json(deserialized!.STATUS_CODE);
            }
        }
        #endregion
        #endregion

        #region [INVESTIGATION OF INCIDENT]

        public IActionResult IncidentInvestigation()
        {
            return View();
        }

        public async Task<IActionResult> GetInvestigation([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            GET_INCIDENT_NOTIF deserialized = new GET_INCIDENT_NOTIF();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("InvestigationReport/IncInve_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<GET_INCIDENT_NOTIF>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All_Incident_Notification = new List<M_Incident_Report>();
            }

            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All_Incident_Notification!.ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add_IncidentReport_Investigation([FromBody] M_Investigation_Report_Add _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                List<M_Inc_Notification_Photos> pHOTOs = new List<M_Inc_Notification_Photos>();
                List<M_Inc_Notification_Videos> Videos = new List<M_Inc_Notification_Videos>();
                var str = HttpContext.Session.GetString("INC_Photo");
                if (str != null)
                {
                    string[] photo = JsonConvert.DeserializeObject<string[]>(str)!;
                    if (photo != null)
                    {
                        for (int i = 0; i < photo.Length; i++)
                        {
                            M_Inc_Notification_Photos pHOTO = new M_Inc_Notification_Photos
                            {
                                Photo_File_Path = photo[i],
                                //CreatedBy = LoginClass.Employee_Identity_Id
                            };

                            pHOTOs.Add(pHOTO);
                        }
                    }
                    //HttpContext.Session.Clear();
                    HttpContext.Session.Remove("INC_Photo");
                }
                var strVideos = HttpContext.Session.GetString("INC_Video");
                if (strVideos != null)
                {
                    string[] videos = JsonConvert.DeserializeObject<string[]>(strVideos)!;
                    if (videos != null)
                    {
                        for (int i = 0; i < videos.Length; i++)
                        {
                            M_Inc_Notification_Videos vIdeos = new M_Inc_Notification_Videos
                            {
                                Video_File_Path = videos[i],
                                //CreatedBy = LoginClass.Employee_Identity_Id
                            };

                            Videos.Add(vIdeos);
                        }
                    }
                    //HttpContext.Session.Clear();
                    HttpContext.Session.Remove("INC_Video");
                }
                _UNIT.L_Inc_Notification_Photos = pHOTOs;
                _UNIT.L_Inc_Notification_Videos = Videos;

                string URL = "";
                if (_UNIT.Inc_Id == "0" || _UNIT.Inc_Id == null)
                {
                    URL = "Incident_Report/IncidentNotificationAdd";
                }
                else
                {
                    URL = "InvestigationReport/Investigation_Update";
                }

                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddIncidentInvestigation([FromBody] M_Investigation_Report model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                model.Role_Id = LoginClass.Role_Id;

                string URL = "";
                if (model.Investigation_Id == "0")//Inc_Obser_Report_Id
                {
                    URL = "InvestigationReport/Investigation_Add";
                }
                else
                {
                    URL = "InvestigationReport/Investigation_Update";
                }

                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }


        public async Task<IActionResult> _ViewInvestigationReport(int Inc_Id)
        {
            using (client)
            {
                M_Investigation_Report _UNIT = new M_Investigation_Report
                {
                    Inc_Id = Convert.ToString(Inc_Id),
                };

                HttpResponseMessage response = client.PostAsync("InvestigationReport/Get_Investigation_Details", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_INVESTIGATION_REPORT deserialized = JsonConvert.DeserializeObject<GET_INVESTIGATION_REPORT>(customerJsonString)!;
                return PartialView("_ViewInvestigationReport", deserialized!.Get_ById);
            }
        }
        [HttpPost]
        public async Task<JsonResult> _EditInvestigationReport([FromBody] M_Investigation_Report _UNIT)
        {
            using (client)
            {
                //M_Investigation_Report _UNIT = new M_Investigation_Report
                //{
                //    Inc_Id = Convert.ToString(Inc_Id),
                //};

                HttpResponseMessage response = client.PostAsync("InvestigationReport/Get_Investigation_Details", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_INVESTIGATION_REPORT deserialized = JsonConvert.DeserializeObject<GET_INVESTIGATION_REPORT>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        #endregion


        #region [FILE UPLOAD]
        public async Task<JsonResult> UploadPhotoFiles(List<IFormFile> files)
        {
            try
            {
                string url = "Incident_Report/ImageUpload";
                string[] deserialized = await FileUpload.UploadMultipleFiles(files, url, client);
                var key = "INC_Photo";
                var str = JsonConvert.SerializeObject(deserialized);
                HttpContext.Session.SetString(key, str);

                return Json("Uploaded Sccuessfully");
            }
            catch (Exception ex)
            {
                return Json("Uploaded Sccuessfully");
            }
        }


        public async Task<JsonResult> Inc_UploadPhotoFiles(List<IFormFile> files)
        {
            try
            {
                string url = "Incident_Report/ImageUpload";
                string[] deserialized = await FileUpload.UploadMultipleFiles(files, url, client);
                var key = "Photo";
                var str = JsonConvert.SerializeObject(deserialized);
                HttpContext.Session.SetString(key, str);

                return Json("Uploaded Sccuessfully");
            }
            catch (Exception ex)
            {
                return Json("Uploaded Sccuessfully");
            }
        }

        public async Task<JsonResult> UploadVideoFiles(List<IFormFile> files)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = "Incident_Report/VideoUpload";
                    string[] deserialized = await FileUpload.UploadMultipleFiles(files, url, client);
                    var key = "INC_Video";
                    str = JsonConvert.SerializeObject(deserialized);
                    HttpContext.Session.SetString(key, str);
                }
                return Json(str);
            }
            catch (Exception ex)
            {
                return Json("Uploaded Sccuessfully");
            }
        }

        [Obsolete]
        public string FiletoData(string FileData)
        {
            try
            {
                string AttachmentName = "";
                if (FileData.Contains("data:"))
                {
                    FileData = FileData.Replace("data:", "");
                }
                string ext = "";
                if (FileData.Contains("image/jpeg"))
                {
                    ext = ".jpeg";
                }
                else if (FileData.Contains("image/jpg"))
                {
                    ext = ".jpg";
                }
                else if (FileData.Contains("image/png"))
                {
                    ext = ".png";
                }

                Guid FileName = Guid.NewGuid();
                var lst = Regex.Split(FileData, "base64,");
                if (lst.Count() > 1)
                {
                    FileData = lst[1];
                    string projectRootPath = $"{this._webHostEnvironment.WebRootPath}\\Knowledge_Share_Files\\";
                    //string path = Path.Combine(projectRootPath, "Knowledge_Share_Files");
                    if (!Directory.Exists(projectRootPath))
                    {
                        Directory.CreateDirectory(projectRootPath);
                    }
                    string fileName = Path.GetFileName(FileData);
                    using (FileStream stream = System.IO.File.Create(projectRootPath + FileName + ext))
                    {
                        byte[] byteArray = Convert.FromBase64String(FileData);
                        stream.Write(byteArray, 0, byteArray.Length);
                    }
                    AttachmentName = Knowledge_Share_File + "/Knowledge_Share_Files/" + FileName + ext;
                }
                return AttachmentName;
            }
            catch (Exception)
            {
                throw;
            }

        }


        [Obsolete]
        public string FiletoDataApplicableReports(string FileData)
        {
            try
            {
                string AttachmentName = "";
                if (FileData.Contains("data:"))
                {
                    FileData = FileData.Replace("data:", "");
                }
                string ext = "";
                if (FileData.Contains("image/jpeg"))
                {
                    ext = ".jpeg";
                }
                else if (FileData.Contains("image/jpg"))
                {
                    ext = ".jpg";

                }
                else if (FileData.Contains("image/png"))
                {
                    ext = ".png";
                }
                else if (FileData.Contains("application/pdf"))
                {
                    ext = ".pdf";
                }
                else
                {
                    ext = ".doc";
                }
                Guid FileName = Guid.NewGuid();
                var lst = Regex.Split(FileData, "base64,");
                if (lst.Count() > 1)
                {
                    FileData = lst[1];
                    string projectRootPath = $"{this._webHostEnvironment.WebRootPath}\\Applicable_Reports\\";
                    //string path = Path.Combine(projectRootPath, "FileUpload_Web");
                    if (!Directory.Exists(projectRootPath))
                    {
                        Directory.CreateDirectory(projectRootPath);
                    }
                    string fileName = Path.GetFileName(FileData);
                    using (FileStream stream = System.IO.File.Create(projectRootPath + FileName + ext))
                    {
                        byte[] byteArray = Convert.FromBase64String(FileData);
                        stream.Write(byteArray, 0, byteArray.Length);
                    }
                    AttachmentName = ApplicableReportPath + "/Applicable_Reports/" + FileName + ext;
                }
                return AttachmentName;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<JsonResult> UploadPhotoEvidence(List<IFormFile> files)
        {
            try
            {
                string url = "Incident_Report/ImageUpload";
                string[] deserialized = await FileUpload.UploadMultipleFiles(files, url, client);
                var key = "EvidencePhoto";
                var str = JsonConvert.SerializeObject(deserialized);
                HttpContext.Session.SetString(key, str);
                return Json("Uploaded Sccuessfully");
            }
            catch (Exception ex)
            {
                return Json("Uploaded Sccuessfully");
            }
        }
        #endregion

        #region [OBSERVATION FILE UPLOAD]
        public async Task<JsonResult> ObservationUploadPhotoFiles(List<IFormFile> files)
        {
            try
            {
                string url = "Incident_Observation_Report/ImageUpload";
                string[] deserialized = await FileUpload.UploadMultipleFiles(files, url, client);
                var key = "Photo";
                var obser = JsonConvert.SerializeObject(deserialized);
                HttpContext.Session.SetString(key, obser);
                return Json(obser);
                //return Json("Uploaded Sccuessfully");
            }
            catch (Exception ex)
            {
                return Json("Uploaded Sccuessfully");
            }
        }
        public async Task<JsonResult> ObsUploadPhotoEvidence(List<IFormFile> files)
        {
            try
            {
                string url = "Incident_Observation_Report/ImageUpload";
                string[] deserialized = await FileUpload.UploadMultipleFiles(files, url, client);
                var key = "EvidencePhoto";
                var str = JsonConvert.SerializeObject(deserialized);
                HttpContext.Session.SetString(key, str);
                return Json("Uploaded Sccuessfully");
            }
            catch (Exception ex)
            {
                return Json("Uploaded Sccuessfully");
            }
        }

        public async Task<JsonResult> ObservationUploadVideoFiles(List<IFormFile> files)
        {
            try
            {
                var obsVideos = "";
                using (client)
                {
                    string url = "Incident_Observation_Report/VideoUpload";
                    string[] deserialized = await FileUpload.UploadMultipleFiles(files, url, client);
                    var key = "Video";
                    obsVideos = JsonConvert.SerializeObject(deserialized);
                    HttpContext.Session.SetString(key, obsVideos);
                }
                return Json(obsVideos);
            }
            catch (Exception ex)
            {
                return Json("Uploaded Sccuessfully");
            }
        }
        #endregion

        #region 

        #endregion
    }
}


