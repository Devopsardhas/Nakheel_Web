using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.AuditSp;
using Nakheel_Web.Models;
using Newtonsoft.Json;
using System.Text;
using static Nakheel_Web.Authentication.Common;
using Nakheel_Web.Models.MAuditNCMForm;
using Nakheel_Web.Models.InspectionMaster;
using Nakheel_Web.Models.Masters;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class AuditNCMFormController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        [Obsolete]
        public AuditNCMFormController(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            client = httpClientFactory.CreateClient("API");
            _hostingEnvironment = hostingEnvironment;
            _webHostEnvironment = webHostEnvironment;
        }

        #region [Audit NCM Form]

        public IActionResult Audit_NCM_Form(string id)
        {
            HttpContext.Session.SetString("NCR_Card_Id", id);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Audit_NCM_Form_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            var str = HttpContext.Session.GetString("NCR_Card_Id");
            M_Get_AuditNCMForm deserialized = new M_Get_AuditNCMForm();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;
            model.Card_Id = str;
            HttpResponseMessage response = client.PostAsync("AuditNCMForm/Audit_NCM_Form_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_AuditNCMForm>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<MAuditNCMForm>();
            }
            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList(),
            });
        }

        public async Task<IActionResult> P_Audit_NCM_Form_AddView(string ID)
        {
            Login_ LoginClass = GetLoginDetails();

            MAuditNCMForm _UNIT = new MAuditNCMForm
            {
                Am_Sp_NCR_Id = ID,
                CreatedBy = LoginClass.Employee_Identity_Id
            };
            HttpResponseMessage response = client.PostAsync("AuditNCMForm/Am_Sp_NCR_Form_GetbyId", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_AuditNCMForm deserialized = JsonConvert.DeserializeObject<M_Get_AuditNCMForm>(customerJsonString)!;
            DateTime StartDates = Convert.ToDateTime(deserialized.Get_ById!.Audit_Date);
            TempData["Msg_NCR_Report_Sp_Date"] = StartDates.ToString("yyyy-MM-dd");
            return PartialView(deserialized.Get_ById!);
        }

        public async Task<IActionResult> P_Create_Audit_NCM_Form()
        {
            Login_ LoginClass = GetLoginDetails();
            MAuditNCMForm _UNIT = new MAuditNCMForm
            {
                CreatedBy = LoginClass.Employee_Identity_Id
            };
            HttpResponseMessage response = client.PostAsync("AuditNCMForm/Audit_NCM_Form_Create_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_AuditNCMForm deserialized = JsonConvert.DeserializeObject<M_Get_AuditNCMForm>(customerJsonString)!;
            return PartialView(deserialized.Get_ById);
        }

        [HttpPost]
        public async Task<IActionResult> Audit_NCM_Form_Add([FromBody] MAuditNCMForm model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditNCMForm/Audit_NCM_Form_Add";
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
        public async Task<IActionResult> Audit_NCM_CA_Action_Add([FromBody] MAuditNCMForm model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditNCMForm/Audit_NCM_CA_Action_Add";
                    Login_ LoginClass = GetLoginDetails();

                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGES_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGES_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<IActionResult> NCM_CA_Action_Lead_Auditor_ApprovalReject(MAuditNCMForm model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditNCMForm/NCM_CA_Action_Lead_Auditor_ApprovalReject";
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
        public async Task<IActionResult> Audit_NCM_CA_Action_HSE_ApprovalReject(MAuditNCMForm model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditNCMForm/Audit_NCM_CA_Action_HSE_ApprovalReject";
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
        public async Task<IActionResult> Audit_NCM_Report_Form_Add([FromBody] MAuditNCMForm model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditNCMForm/Audit_NCM_Report_Form_Add";
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
        public async Task<IActionResult> Audit_NCM_Form_HSE_ApprovalReject(MAuditNCMForm model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditNCMForm/Audit_NCM_Form_HSE_ApprovalReject";
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
        public async Task<IActionResult> Audit_NCM_Form_Dir_ApprovalReject(MAuditNCMForm model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditNCMForm/Audit_NCM_Form_Dir_ApprovalReject";
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
