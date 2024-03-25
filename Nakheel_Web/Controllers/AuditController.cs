using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.AuditMaster;
using Nakheel_Web.Models.IncidentMaster;
using Nakheel_Web.Models.IncidentReport;
using Nakheel_Web.Models.Masters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using static Nakheel_Web.Authentication.Common;
using Nakheel_Web.Models.AuditSp;
using Nakheel_Web.Models.Audit_Wel;


namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class AuditController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        [Obsolete]
        public AuditController(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            client = httpClientFactory.CreateClient("API");
            _hostingEnvironment = hostingEnvironment;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region [Audit Masters]

        #region [Audit Category Masters]
        public async Task<IActionResult> Audit_Category()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("AuditMaster/Audit_Category_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Audit_Category deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Category>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAuditCategory(M_Audit_Category model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;

                    string URL = "";
                    if (model.Audit_Category_Id == "0")
                    {
                        URL = "AuditMaster/Audit_Category_Add";
                    }
                    else
                    {
                        URL = "AuditMaster/Audit_Category_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Audit_Category");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> Audit_Category_GetByID(string ID)
        {

            using (client)
            {
                M_Audit_Category _UNIT = new M_Audit_Category
                {
                    Audit_Category_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("AuditMaster/Audit_Category_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Audit_Category deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Category>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> Audit_Category_Delete(string ID)
        {

            using (client)
            {
                M_Audit_Category _UNIT = new M_Audit_Category
                {
                    Audit_Category_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("AuditMaster/Audit_Category_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Audit_Category deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Category>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region [Audit Topics Masters]
        public async Task<IActionResult> Audit_Topics()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("AuditMaster/Audit_Topics_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Audit_Topics deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Topics>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAuditTopics(List<M_Audit_Topics> model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    foreach (var item in model)
                    {
                        item.CreatedBy = LoginClass.Employee_Identity_Id;
                    }
                    string URL = "AuditMaster/Audit_Topics_Add";
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

        public async Task<IActionResult> Audit_Topics_GetByID(string ID)
        {

            using (client)
            {
                M_Audit_Topics _UNIT = new M_Audit_Topics
                {
                    Audit_Category_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("AuditMaster/Audit_Topics_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Audit_Topics deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Topics>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> Audit_Topics_Delete(string ID)
        {
            using (client)
            {
                M_Audit_Topics _UNIT = new M_Audit_Topics
                {
                    Audit_Topics_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("AuditMaster/Audit_Topics_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Audit_Topics deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Topics>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region [Audit Sub Topics Masters]
        public async Task<IActionResult> Audit_Sub_Topics()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("AuditMaster/Audit_Sub_Topics_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Audit_Sub_Topics deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Sub_Topics>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Audit_Sub_Topics(List<M_Audit_Sub_Topics> model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    foreach (var item in model)
                    {
                        item.CreatedBy = LoginClass.Employee_Identity_Id;
                    }
                    string URL = "AuditMaster/Audit_Sub_Topics_Add";
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
        public async Task<IActionResult> Audit_Sub_Topics_GetByID(string ID)
        {

            using (client)
            {
                M_Audit_Sub_Topics _UNIT = new M_Audit_Sub_Topics
                {
                    Audit_Category_Id = ID,

                };
                HttpResponseMessage response = client.PostAsync("AuditMaster/Audit_Sub_Topics_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Audit_Sub_Topics deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Sub_Topics>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Audit_Sub_Topics_Delete(string ID)
        {
            using (client)
            {
                M_Audit_Sub_Topics _UNIT = new M_Audit_Sub_Topics
                {
                    Audit_Sub_Topics_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("AuditMaster/Audit_Sub_Topics_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Audit_Sub_Topics deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Sub_Topics>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        #endregion

        #region [Audit Questionnaires Masters]
        public async Task<IActionResult> Audit_Questionnaires()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("AuditMaster/Audit_Sub_Topics_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Audit_Sub_Topics deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Sub_Topics>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Audit_Questionnaires(List<M_Audit_Questionnaires> model)
        {
            //if (ModelState.IsValid)
            //{
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                foreach (var item in model)
                {
                    item.CreatedBy = LoginClass.Employee_Identity_Id;
                }
                string URL = "AuditMaster/Audit_Questionnaires_Add";
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
            //}
            //else
            //{
            //    return NoContent();
            //}
        }
        public async Task<IActionResult> Audit_Questionnaires_GetByID(M_Audit_Questionnaires Model)
        {

            using (client)
            {
                M_Audit_Questionnaires _UNIT = new M_Audit_Questionnaires
                {
                    Audit_Category_Id = Model.Audit_Category_Id,
                    Audit_Sub_Topics_Id = Model.Audit_Sub_Topics_Id,
                };
                HttpResponseMessage response = client.PostAsync("AuditMaster/Audit_Questionnaires_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Audit_Questionnaires deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Questionnaires>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> Get_Env_Sp_Questionaire(M_Audit_Questionnaires Model)
        {

            using (client)
            {
                M_Audit_Questionnaires _UNIT = new M_Audit_Questionnaires
                {
                    Audit_Category_Id = Model.Audit_Category_Id,
                };
                HttpResponseMessage response = client.PostAsync("AuditMaster/Get_Env_Sp_Questionaire", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Audit_Questionnaires deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Questionnaires>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Audit_Questionnaires_Delete(string ID)
        {
            using (client)
            {
                M_Audit_Questionnaires _UNIT = new M_Audit_Questionnaires
                {
                    Audit_Questionnaires_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("AuditMaster/Audit_Questionnaires_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Audit_Questionnaires deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Questionnaires>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        #endregion

        #endregion

        #region [Findings Master]
        public async Task<IActionResult> Audit_Findings()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("AuditMaster/GetAll_Am_AuditFindings").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Audit_Findings deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Findings>(customerJsonString)!;
                return View(deserialized!.Get_All_Findings);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAuditFindings(List<M_AuditFindings> model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    foreach (var item in model)
                    {
                        item.CreatedBy = LoginClass.Employee_Identity_Id;
                    }

                    string URL = "AuditMaster/Add_Am_Audit_Findings";
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                    return Json(deserialized!.STATUS_CODE);
                }

            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Audit_Findings_ByID(M_AuditFindings model)
        {
            using (client)
            {
                M_AuditFindings _mODEL = new M_AuditFindings()
                {

                    Findings_Type = model.Findings_Type
                };
                HttpResponseMessage response = client.PostAsync("AuditMaster/GetByID_AuditFindings", new StringContent(JsonConvert.SerializeObject(_mODEL), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Audit_Findings deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Findings>(customerJsonString)!;
                return Json(deserialized!.Get_ById_Findings);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Audit_Findings_Delete(string id)
        {
            using (client)
            {
                M_AuditFindings _uNIT = new M_AuditFindings
                {
                    Findings_Id = id
                };
                HttpResponseMessage response = client.PostAsync("AuditMaster/Delete_AM_Audit_Findings", new StringContent(JsonConvert.SerializeObject(_uNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_AuditFindings deserialized = JsonConvert.DeserializeObject<M_AuditFindings>(customerJsonString)!;
                return Json(deserialized!.Status);
            }
        }
        #endregion

        #region[Audit Schedule - Service Provider]
        public async Task<IActionResult> AuditSch_ServiceProvider()
        {
            using (client)
            {
                return View();
            }
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_AuditSchedule([FromBody] Audit_Schedule_Model model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    model.Role_Id = LoginClass.Role_Id;
                    HttpResponseMessage response = client.PostAsync("AuditMaster/Add_Audit_Schedule", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                    return Json(deserialized!.STATUS_CODE);
                }

            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> Get_All_Audit_Schedule([FromBody] DataTableAjaxPostModel model)
        {
            using (client)
            {
                var data = new List<object>();
                GET_SERVICE_PROVIDER_AUDIT deserialized = new GET_SERVICE_PROVIDER_AUDIT();
                Login_ LoginClass = GetLoginDetails();
                HttpResponseMessage response = client.PostAsync("AuditMaster/Get_All_Audit_Schedule", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                deserialized = JsonConvert.DeserializeObject<GET_SERVICE_PROVIDER_AUDIT>(customerJsonString)!;
                if (deserialized.STATUS_CODE == "404")
                {
                    deserialized.Get_All = new List<Audit_Schedule_Model>();
                }
                else
                {
                    foreach (var item in deserialized.Get_All!)
                    {
                        item.Role_Id = LoginClass.Role_Id;
                    }
                }


                return Json(new
                {
                    draw = model.Draw,
                    recordsTotal = deserialized.RecordsTotal,
                    recordsFiltered = deserialized.RecordsFiltered,
                    data = deserialized.Get_All!.ToList()
                });
            }
        }

        public async Task<IActionResult> LoadAll_CheckList(int Audit_Sch_Id)
        {

            using (client)
            {
                Audit_Schedule_Model _mODEL = new Audit_Schedule_Model
                {
                    Audit_Team_List = new List<Audit_Team>(),
                    Service_Prov_List = new List<Audit_Service_Provider>(),
                    Audit_Sch_Id = Convert.ToString(Audit_Sch_Id),

                };

                HttpResponseMessage response = client.PostAsync("AuditMaster/GetById_AM_AuditSchedule", new StringContent(JsonConvert.SerializeObject(_mODEL), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SERVICE_PROVIDER_AUDIT deserialized = JsonConvert.DeserializeObject<GET_SERVICE_PROVIDER_AUDIT>(customerJsonString)!;
                return PartialView("LoadAll_CheckList", deserialized!.Get_By_Sch);
            }
        }

        public async Task<IActionResult> Add_Audit_Checklist([FromBody] Audit_Schedule_Model model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;

                    HttpResponseMessage response = client.PostAsync("AuditMaster/Add_Audit_Findings_Chk", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                    return Json(deserialized!.STATUS_CODE);
                }

            }
            else
            {
                return NoContent();
            }
        }

        public async Task<JsonResult> ServiceProv_Aud_UploadPhotoFiles(List<IFormFile> files)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = "AuditMaster/ImageUpload_ServiceProvider";
                    string[] deserialized = await FileUpload.UploadMultipleFiles(files, url, client);
                    var key = "ServiceProviderAuditEviPhoto";
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
        #endregion

        #region[Audit Schedule - Internal Audit by zone]


        public IActionResult Internal_Aud_Schedule()
        {
            return View();
        }

        public async Task<IActionResult> GetInternal_Aud_Schedule([FromBody] DataTableAjaxPostModel model)
        {

            var data = new List<object>();
            Get_Aud_Internal_Audit deserialized = new Get_Aud_Internal_Audit();
            Login_ LoginClass = GetLoginDetails();
            HttpResponseMessage response = client.PostAsync("AuditMaster/Get_All_Internal_Audit", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<Get_Aud_Internal_Audit>(customerJsonString)!;
            if (deserialized.Get_All_Aud_Internal_Audit != null)
            {
                foreach (var item in deserialized.Get_All_Aud_Internal_Audit)
                {
                    Aud_Internal_Audit _Internal_Audit = new Aud_Internal_Audit();
                    _Internal_Audit.Internal_Audit_Id = item.Internal_Audit_Id;
                    _Internal_Audit.Login_Id = LoginClass.Employee_Identity_Id;



                    HttpResponseMessage response1 = client.PostAsync("AuditMaster/Get_Internal_Audit_Audit_Team", new StringContent(JsonConvert.SerializeObject(_Internal_Audit), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString1 = await response1.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized1 = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString1)!;
                    item.Role_Id = LoginClass.Role_Id;
                    item.Login_Id = LoginClass.Employee_Identity_Id;
                    item.UpdatedBy = deserialized1.STATUS_CODE;
                }
            }

            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All_Aud_Internal_Audit = new List<Aud_Internal_Audit>();
            }

            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All_Aud_Internal_Audit!.ToList()
            });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Internal_Aud_Schedule([FromBody] Aud_Internal_Audit model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    model.Role_Id = LoginClass.Role_Id;
                    HttpResponseMessage response = client.PostAsync("AuditMaster/Add_Internal_Audit", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
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

        public async Task<IActionResult> _ViewInternalAud(int Internal_Audit_Id)
        {
            using (client)
            {
                Aud_Internal_Audit _UNIT = new Aud_Internal_Audit
                {
                    Internal_Audit_Id = Convert.ToString(Internal_Audit_Id),
                };

                HttpResponseMessage response = client.PostAsync("AuditMaster/GetBy_Id_Internal_Audit", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Aud_Internal_Audit deserialized = JsonConvert.DeserializeObject<Get_Aud_Internal_Audit>(customerJsonString)!;
                return PartialView("_ViewInternalAud", deserialized!.Getby_Aud_Internal_Audit);
            }
        }

        public async Task<IActionResult> _ViewInternalAudSed(int Internal_Audit_Id)
        {
            using (client)
            {
                Aud_Internal_Audit _UNIT = new Aud_Internal_Audit
                {
                    Internal_Audit_Id = Convert.ToString(Internal_Audit_Id),
                };

                HttpResponseMessage response = client.PostAsync("AuditMaster/GetByIdInternalAuditSed", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Aud_Internal_Audit deserialized = JsonConvert.DeserializeObject<Get_Aud_Internal_Audit>(customerJsonString)!;
                return Json(deserialized!.Getby_Aud_Internal_Audit);
            }
        }

        public async Task<IActionResult> _ViewInternalAudNcrForm(int Internal_Audit_Id)
        {
            using (client)
            {
                Aud_Internal_Audit _UNIT = new Aud_Internal_Audit
                {
                    Internal_Audit_Id = Convert.ToString(Internal_Audit_Id),
                };

                HttpResponseMessage response = client.PostAsync("AuditMaster/GetBy_Id_Internal_Audit", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Aud_Internal_Audit deserialized = JsonConvert.DeserializeObject<Get_Aud_Internal_Audit>(customerJsonString)!;
                return PartialView("_ViewInternalAudNcrForm", deserialized!.Getby_Aud_Internal_Audit);
            }
        }

        public async Task<IActionResult> Env_Aud_Schedule()
        {
            using (client) { return View(); }
        }

        public async Task<JsonResult> Internal_Aud_UploadPhotoFiles(List<IFormFile> files)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = "AuditMaster/ImageUpload";
                    string[] deserialized = await FileUpload.UploadMultipleFiles(files, url, client);
                    var key = "InternalAuditEviPhoto";
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

        [HttpPost]
        public async Task<JsonResult> Add_Internal_Aud_CheckList([FromBody] List<Internal_Aud_CheckList> model)
        {
            try
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    foreach (var item in model)
                    {
                        item.CreatedBy = LoginClass.Employee_Identity_Id;
                        item.Role_Id = LoginClass.Role_Id;
                    }

                    HttpResponseMessage response = client.PostAsync("AuditMaster/Add_Internal_Aud_CheckList", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    return Json(deserialized!.STATUS_CODE);
                }
            }
            catch (Exception ex)
            {
                return Json("Uploaded Sccuessfully");
            }
        }

        [HttpPost]
        public async Task<JsonResult> _ViewAuditCheckList(string Internal_Audit_Id)
        {
            using (client)
            {
                Aud_Internal_Audit _UNIT = new Aud_Internal_Audit
                {
                    Internal_Audit_Id = Convert.ToString(Internal_Audit_Id),
                };

                HttpResponseMessage response = client.PostAsync("AuditMaster/Get_Internal_Aud_CheckList", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Aud_Internal_Audit deserialized = JsonConvert.DeserializeObject<Get_Aud_Internal_Audit>(customerJsonString)!;
                return Json(deserialized!.GetbyId_Aud_Internal_Audit_Checklist);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_NCR_Report_Form([FromBody] Aud_Internal_NCR_Form model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                if (model.NCR_Report_Id == "" || model.NCR_Report_Id == "0")
                {
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync("AuditMaster/Add_NCR_Report_Form", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    return Json(deserialized!.STATUS_CODE);
                }
                else
                {
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync("AuditMaster/Update_NCR_Report_Form", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    return Json(deserialized!.STATUS_CODE);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Get_NCR_Report_Form_Details(string Internal_Audit_Id, string Questionnaire_Id)
        {
            using (client)
            {
                Aud_Internal_NCR_Form model = new Aud_Internal_NCR_Form();
                model.Int_Audit_Id = Internal_Audit_Id;
                model.Questionnaires_Id = Questionnaire_Id;

                Login_ LoginClass = GetLoginDetails();
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync("AuditMaster/Get_NCR_Report_Form_Details", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Aud_Internal_Audit deserialized = JsonConvert.DeserializeObject<Get_Aud_Internal_Audit>(customerJsonString)!;
                return Json(deserialized!.Getby_Aud_Internal_NCR_Form);
            }
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete_NCR_Report_Form([FromBody] Aud_Internal_Audit model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                model.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("AuditMaster/Complete_NCR_Report_Form", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        #endregion

        #region[Audit Schedule - Internal Audit by zone]
        public IActionResult Welfare_Aud_Schedule()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Audit_Welfare_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            M_Get_AuditWelfare deserialized = new M_Get_AuditWelfare();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;
            HttpResponseMessage response = client.PostAsync("AuditWelfare/Audit_Welfare_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_AuditWelfare>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_AuditWelfare>();
            }
            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList(),
            });
        }


        public async Task<IActionResult> P_Audit_Welfare_AddView(string ID)
        {
            Login_ LoginClass = GetLoginDetails();

            M_AuditWelfare _UNIT = new M_AuditWelfare
            {
                Audit_Welfare_Sch_Id = ID,
                CreatedBy = LoginClass.Employee_Identity_Id
            };
            HttpResponseMessage response = client.PostAsync("AuditWelfare/Audit_Welfare_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_AuditWelfare deserialized = JsonConvert.DeserializeObject<M_Get_AuditWelfare>(customerJsonString)!;
            DateTime StartDates = Convert.ToDateTime(deserialized.Get_ById!.Audit_Welfare_Date);
            TempData["Msg_Audit_Welfare_Date"] = StartDates.ToString("yyyy-MM-dd");
            return PartialView(deserialized.Get_ById!);
        }
        [HttpPost]
        public async Task<IActionResult> Am_Welfare_Find_Questionnaire_Add([FromBody] Am_Welfare_Add_Finding_Questionnaires_List model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();

                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    string URL = "AuditWelfare/Am_Welfare_Find_Questionnaire_Add";
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
        public async Task<IActionResult> Am_Welfare_Find_Ques_LM_Approval(M_AuditWelfare model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditWelfare/Am_Welfare_Find_Ques_LM_ApprovalReject";
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
        public async Task<IActionResult> Am_Welfare_Find_Ques_Director_Approval(M_AuditWelfare model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditWelfare/Am_Welfare_Find_Ques_Director_ApprovalReject";
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
        public async Task<IActionResult> Am_Sp_NCR_Action_Add([FromBody] M_AuditWelfare model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;

                    string URL = "AuditWelfare/Am_Welfare_NCR_Action_Add";
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
        public async Task<IActionResult> Am_Welfare_Find_Ques_Lead_Au_ApprovalReject(M_AuditWelfare model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditWelfare/Am_Welfare_Find_Ques_Lead_Au_ApprovalReject";
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
        public async Task<IActionResult> Am_Welfare_Find_Ques_HSE_ApprovalReject(M_AuditWelfare model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "AuditWelfare/Am_Welfare_Find_Ques_HSE_ApprovalReject";
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
