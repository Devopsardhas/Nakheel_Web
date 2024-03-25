using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.AuditSp;
using Nakheel_Web.Models.InspectionMaster;
using Nakheel_Web.Models.Masters;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using static Nakheel_Web.Authentication.Common;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class AuditSpController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        [Obsolete]
        public AuditSpController(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            client = httpClientFactory.CreateClient("API");
            _hostingEnvironment = hostingEnvironment;
            _webHostEnvironment = webHostEnvironment;
        }

        #region [Audit Service Provider]

        public IActionResult Audit_Service_Provider()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Audit_Sp_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            M_Get_AuditSp deserialized = new M_Get_AuditSp();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;
            HttpResponseMessage response = client.PostAsync("AuditSp/Audit_Sp_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_AuditSp>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_AuditSp>();
            }
            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList(),
            });
        }
        public async Task<IActionResult> P_Audit_Sp_AddView(string ID)
        {
            Login_ LoginClass = GetLoginDetails();

            M_AuditSp _UNIT = new M_AuditSp
            {
                Audit_Sp_Sch_Id = ID,
                CreatedBy = LoginClass.Employee_Identity_Id
            };
            HttpResponseMessage response = client.PostAsync("AuditSp/Audit_Sp_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_AuditSp deserialized = JsonConvert.DeserializeObject<M_Get_AuditSp>(customerJsonString)!;
            DateTime StartDates = Convert.ToDateTime(deserialized.Get_ById!.Audit_Sp_Date);
            TempData["Msg_Audit_Sp_Date"] = StartDates.ToString("yyyy-MM-dd");
            return PartialView(deserialized.Get_ById!);
        }
        [HttpPost]
        public async Task<IActionResult> Am_Sp_Audit_Team_GetBy_Id(string Zone_Id, string Community_Id)
        {
            M_AuditSp _UNIT = new M_AuditSp
            {
                Zone_Id = Zone_Id,
                Community_Id = Community_Id
            };
            HttpResponseMessage response = client.PostAsync("AuditSp/Am_Sp_Audit_Team_GetBy_Id", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_AuditSp_Filter deserialized = JsonConvert.DeserializeObject<M_Get_AuditSp_Filter>(customerJsonString)!;
            return Json(deserialized.Get_All);
        }
        [HttpPost]
        public async Task<IActionResult> Am_Sp_Representative_GetBy_Id(string Zone_Id, string Community_Id)
        {
            M_AuditSp _UNIT = new M_AuditSp
            {
                Zone_Id = Zone_Id,
                Community_Id = Community_Id
            };
            HttpResponseMessage response = client.PostAsync("AuditSp/Am_Sp_Representative_GetBy_Id", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_AuditSp_Filter deserialized = JsonConvert.DeserializeObject<M_Get_AuditSp_Filter>(customerJsonString)!;
            return Json(deserialized.Get_All);
        }
        [HttpPost]
        public async Task<IActionResult> Audit_Sp_Add([FromBody] M_AuditSp model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditSp/Audit_Sp_Add";
                    Login_ LoginClass = GetLoginDetails();

                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Am_Sp_Find_Questionnaire_Add([FromBody] Am_Sp_Add_Finding_Questionnaires_List model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();

                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    string URL = "AuditSp/Am_Sp_Find_Questionnaire_Add";
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Am_Sp_Find_Ques_LM_Approval(M_AuditSp model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditSp/Am_Sp_Find_Ques_LM_ApprovalReject";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    return Json(deserialized!.STATUS_CODE);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Am_Sp_Find_Ques_Director_Approval(M_AuditSp model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditSp/Am_Sp_Find_Ques_Director_ApprovalReject";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    return Json(deserialized!.STATUS_CODE);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Am_Sp_CA_Service_Provider_Zone_GetBy(string Zone_Id)
        {
            M_AuditSp _UNIT = new M_AuditSp
            {
                Zone_Id = Zone_Id,
            };
            HttpResponseMessage response = client.PostAsync("AuditSp/Am_Sp_CA_Service_Provider_Zone_GetBy", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_AuditSp_Filter deserialized = JsonConvert.DeserializeObject<M_Get_AuditSp_Filter>(customerJsonString)!;
            return Json(deserialized.Get_All);
        }
        [HttpPost]
        public async Task<IActionResult> Am_Sp_Corrective_Action_Add([FromBody] M_AuditSp model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditSp/Am_Sp_Corrective_Action_Add";
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Am_Sp_NCR_Action_Add([FromBody] M_AuditSp model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;

                    string URL = "AuditSp/Am_Sp_NCR_Action_Add";
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Am_Sp_Find_Ques_HSE_ApprovalReject(M_AuditSp model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditSp/Am_Sp_Find_Ques_HSE_ApprovalReject";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    return Json(deserialized!.STATUS_CODE);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Am_Sp_Find_Ques_Lead_Au_ApprovalReject(M_AuditSp model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditSp/Am_Sp_Find_Ques_Lead_Au_ApprovalReject";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    return Json(deserialized!.STATUS_CODE);
                }
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Am_Sp_Ques_Photo_Delete(string Id)
        {
            using (client)
            {
                Am_Sp_Qus_Evidence _UNIT = new Am_Sp_Qus_Evidence
                {
                    Am_SP_Qus_Photo_Id = Id
                };
                HttpResponseMessage response = client.PostAsync("AuditSp/Am_Sp_Ques_Photo_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Am_Sp_Qus_Evidence deserialized = JsonConvert.DeserializeObject<Am_Sp_Qus_Evidence>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
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
