using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.Audit_Env;
using Nakheel_Web.Models.AuditSp;
using Nakheel_Web.Models.ControlOfWorkMaster;
using Nakheel_Web.Models.Masters;
using Newtonsoft.Json;
using System.Text;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class AuditEnvController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        [Obsolete]
        public AuditEnvController(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            client = httpClientFactory.CreateClient("API");
            _hostingEnvironment = hostingEnvironment;
            _webHostEnvironment = webHostEnvironment;
        }
             
        public IActionResult EnvironmentalAudit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAll_Env_Audit([FromBody] DataTableAjaxPostModel model)
        {
            HttpResponseMessage response = client.PostAsync("AuditEnv/GetAll_Env_Audit", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            Get_Env_Audit deserialized = JsonConvert.DeserializeObject<Get_Env_Audit>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<Env_Audit_Model>();
            }

            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList()
            });
        }
       
        public async Task<IActionResult> _ViewEnv_Aud_Sch(string Env_Audit_Id)
        {
            using (client)
            {
                Env_Audit_Model _UNIT = new Env_Audit_Model
                {
                    Env_Audit_Id = Env_Audit_Id,
                };

                HttpResponseMessage response = client.PostAsync("AuditEnv/Edit_Env_Audit", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Env_Audit_Edit deserialized = JsonConvert.DeserializeObject<Get_Env_Audit_Edit>(customerJsonString)!;
                return PartialView("_ViewEnv_Aud_Sch", deserialized!.Get_ById);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Env_Audit([FromBody] Env_Audit_Model model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditEnv/Add_Env_Audit";
                    //Login_ LoginClass = GetLoginDetails();

                    //model.Created_by = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Am_Env_Audit_Questionnaire_Add([FromBody] Am_Env_Audit_Add_Questionnaires_List model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditEnv/Am_Env_Audit_Questionnaire_Add";
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
        public async Task<IActionResult> Am_Env_Audit_ApprovalReject(Env_Audit_Model model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditEnv/Am_Env_Audit_ApprovalReject";
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
        public async Task<IActionResult> Am_Env_Audit_Add_Sp_Target([FromBody] Env_Audit_Model model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditEnv/Am_Env_Audit_Add_Sp_Target";
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
        public async Task<IActionResult> Am_Env_Audit_Add_NCR_Action([FromBody] Env_Audit_Model model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                   
                    string URL = "AuditEnv/Am_Env_Audit_Add_NCR_Action";
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
        public async Task<JsonResult> Env_Audit_Evidence(List<IFormFile> files)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = "AuditEnv/Env_Audit_Evidence";
                    string[] deserialized = await FileUpload.UploadMultipleFiles(files, url, client);
                    var key = "Photo";
                    str = JsonConvert.SerializeObject(deserialized);
                    HttpContext.Session.SetString(key, str);
                }
                return Json(str);
            }
            catch (Exception ex)
            {
                return Json("Uploaded Successfully");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Am_Env_Audit_HSE_Approve([FromBody] Env_Audit_Approval model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditEnv/Am_Env_Audit_HSE_Approve";
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
        public async Task<IActionResult> Am_Env_Audit_Reject([FromBody] Env_Audit_Reject model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditEnv/Am_Env_Audit_Reject";
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

        #region [LOGIN DETAILS]
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
    }
}
