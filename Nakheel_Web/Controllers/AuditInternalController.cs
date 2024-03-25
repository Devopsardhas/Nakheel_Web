using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.Audit_Internal;
using Nakheel_Web.Models.Masters;
using Newtonsoft.Json;
using System.Text;
using static Nakheel_Web.Authentication.Common;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class AuditInternalController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        [Obsolete]
        public AuditInternalController(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            client = httpClientFactory.CreateClient("API");
            _hostingEnvironment = hostingEnvironment;
            _webHostEnvironment = webHostEnvironment;
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
        #region [Internal Audit]
        public IActionResult Internal_Audit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Audit_Internal_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            M_Get_AuditInternal deserialized = new M_Get_AuditInternal();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;
            HttpResponseMessage response = client.PostAsync("AuditInternal/Audit_Internal_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_AuditInternal>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Audit_Internal>();
            }
            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList(),
            });
        }

        public async Task<IActionResult> P_Audit_Internal_AddView(string ID)
        {
            Login_ LoginClass = GetLoginDetails();

            M_Audit_Internal _UNIT = new M_Audit_Internal
            {
                Audit_Internal_Id = ID,
                CreatedBy = LoginClass.Employee_Identity_Id
            };
            HttpResponseMessage response = client.PostAsync("AuditInternal/Audit_Internal_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_AuditInternal deserialized = JsonConvert.DeserializeObject<M_Get_AuditInternal>(customerJsonString)!;
            DateTime StartDates = Convert.ToDateTime(deserialized.Get_ById!.Audit_Int_Date);
            TempData["Msg_Audit_Int_Date"] = StartDates.ToString("yyyy-MM-dd");
            return PartialView(deserialized.Get_ById!);
        }

        [HttpPost]
        public async Task<IActionResult> AM_Internal_Audit_Team_GetBy_Id(string Zone_Id, string Community_Id)
        {
            M_Audit_Internal _UNIT = new M_Audit_Internal
            {
                Zone_Id = Zone_Id,
                Community_Id = Community_Id
            };
            HttpResponseMessage response = client.PostAsync("AuditInternal/AM_Internal_Audit_Team_GetBy_Id", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_AuditInternal_Filter deserialized = JsonConvert.DeserializeObject<M_Get_AuditInternal_Filter>(customerJsonString)!;
            return Json(deserialized.Get_All);
        }

        [HttpPost]
        public async Task<IActionResult> AM_Internal_Representative_GetById(string Zone_Id, string Community_Id)
        {
            M_Audit_Internal _UNIT = new M_Audit_Internal
            {
                Zone_Id = Zone_Id,
                Community_Id = Community_Id
            };
            HttpResponseMessage response = client.PostAsync("AuditInternal/AM_Int_Representative_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_AuditInternal_Filter deserialized = JsonConvert.DeserializeObject<M_Get_AuditInternal_Filter>(customerJsonString)!;
            return Json(deserialized.Get_All);
        }

        [HttpPost]
        public async Task<IActionResult> Audit_Internal_Add([FromBody] M_Audit_Internal model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditInternal/Audit_Internal_Add";
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
        public async Task<IActionResult> AM_Int_Find_Questionnaire_Add([FromBody] AM_Internal_Add_Finding_Qns_List model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();

                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    string URL = "AuditInternal/AM_Int_Find_Questionnaire_Add";
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
        public async Task<IActionResult> AM_Int_Find_Ques_LM_Approval(M_Audit_Internal model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditInternal/AM_Int_Find_Ques_LM_ApprovalReject";
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
        public async Task<IActionResult> AM_Int_Find_Ques_Director_Approval(M_Audit_Internal model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditInternal/AM_Int_Find_Ques_Director_ApprovalReject";
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
        public async Task<IActionResult> AM_Int_CA_Service_Provider_Zone_GetBy(string Zone_Id)
        {
            M_Audit_Internal _UNIT = new M_Audit_Internal
            {
                Zone_Id = Zone_Id,
            };
            HttpResponseMessage response = client.PostAsync("AuditInternal/AM_Int_CA_Service_Provider_Zone_GetBy", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_AuditInternal_Filter deserialized = JsonConvert.DeserializeObject<M_Get_AuditInternal_Filter>(customerJsonString)!;
            return Json(deserialized.Get_All);
        }

        [HttpPost]
        public async Task<IActionResult> Am_Sp_Corrective_Action_Add([FromBody] M_Audit_Internal model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditInternal/Am_Sp_Corrective_Action_Add";
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
        public async Task<IActionResult> AM_Int_NCR_Action_Add([FromBody] M_Audit_Internal model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;

                    string URL = "AuditInternal/AM_Int_NCR_Action_Add";
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
        public async Task<IActionResult> AM_Int_Find_Ques_HSE_ApprovalReject(M_Audit_Internal model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditInternal/AM_Int_Find_Ques_HSE_ApprovalReject";
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
        public async Task<IActionResult> AM_Int_Find_Ques_Lead_Au_ApprovalReject(M_Audit_Internal model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditInternal/AM_Int_Find_Ques_Lead_Au_ApprovalReject";
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
        #endregion
    }
}