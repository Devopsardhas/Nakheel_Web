using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.ControlOfWorkMaster;
using Nakheel_Web.Models.HandOverInsMaster;
using Nakheel_Web.Models.IncidentMaster;
using Nakheel_Web.Models.IncidentReport;
using Nakheel_Web.Models.Masters;
using Nakheel_Web.Models.SecurityIncidentMaster;
using Nakheel_Web.Models.SecurityIncidentReport;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using static Nakheel_Web.Authentication.Common;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class SecurityIncidentController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string Knowledge_Share_File;
        private readonly string ApplicableReportPath;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        [Obsolete]
        public SecurityIncidentController(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
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
        #region [SECURITY MANAGEMENT]
        #region[Security Incident Category]
        public async Task<IActionResult> Sec_Inc_Category()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("SecurityIncidentMaster/Sec_Inc_Category_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SECINCIDENT_CATEGORY deserialized = JsonConvert.DeserializeObject<GET_SECINCIDENT_CATEGORY>(customerJsonString)!;

                return View(deserialized!.Get_All);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSecIncidentcategory(SECINCIDENT_CATEGORY model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Sec_Inc_Category_Id == "0")
                    {
                        URL = "SecurityIncidentMaster/Sec_Inc_Category_Add";
                    }
                    else
                    {
                        URL = "SecurityIncidentMaster/Sec_Inc_Category_Update";
                    }
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Sec_Inc_Category");
            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> Sec_Inc_Category_GetByID(string ID)
        {

            using (client)
            {
                SECINCIDENT_CATEGORY _UNIT = new SECINCIDENT_CATEGORY
                {
                    Sec_Inc_Category_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("SecurityIncidentMaster/Sec_Inc_Category_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SECINCIDENT_CATEGORY deserialized = JsonConvert.DeserializeObject<GET_SECINCIDENT_CATEGORY>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        public async Task<IActionResult> Sec_Inc_Category_Delete(string ID)
        {

            using (client)
            {
                SECINCIDENT_CATEGORY _UNIT = new SECINCIDENT_CATEGORY
                {
                    Sec_Inc_Category_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("SecurityIncidentMaster/Sec_Inc_Category_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SECINCIDENT_CATEGORY deserialized = JsonConvert.DeserializeObject<GET_SECINCIDENT_CATEGORY>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        #endregion

        #region[Security Incident type]
        public async Task<IActionResult> Sec_Incident_Type()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("SecurityIncidentMaster/Sec_Inc_Type_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SEC_INC_TYPE deserialized = JsonConvert.DeserializeObject<GET_SEC_INC_TYPE>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSecIncType(SECINCIDENTTYPE model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Sec_Inc_Type_Id == "0")
                    {
                        URL = "SecurityIncidentMaster/Sec_Inc_Type_Add";
                    }
                    else
                    {
                        URL = "SecurityIncidentMaster/Sec_Inc_Type_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Sec_Incident_Type");
            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> SecIncType_GetByID(string ID)
        {

            using (client)
            {
                SECINCIDENTTYPE _UNIT = new SECINCIDENTTYPE
                {
                    Sec_Inc_Type_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("SecurityIncidentMaster/Sec_Inc_Type_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SEC_INC_TYPE deserialized = JsonConvert.DeserializeObject<GET_SEC_INC_TYPE>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        public async Task<IActionResult> SecIncType_Delete(string ID)
        {

            using (client)
            {
                SECINCIDENTTYPE _UNIT = new SECINCIDENTTYPE
                {
                    Sec_Inc_Type_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("SecurityIncidentMaster/Sec_Inc_Type_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SEC_INC_TYPE deserialized = JsonConvert.DeserializeObject<GET_SEC_INC_TYPE>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        #endregion

        #region[Types_of_Security_Incident]
        public async Task<IActionResult> Sub_Security_Incident()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("SecurityIncidentMaster/Sub_Security_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SUB_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_SUB_SECURITY_INCIDENT>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSub_Security(SUB_SECURITY_INCIDENT model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Sec_Inc_Sub_cat_Id == "0")
                    {
                        URL = "SecurityIncidentMaster/Sub_Security_Add";
                    }
                    else
                    {
                        URL = "SecurityIncidentMaster/Sub_Security_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Sub_Security_Incident");
            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> Sub_Security_GetByID(string ID)
        {

            using (client)
            {
                SUB_SECURITY_INCIDENT _UNIT = new SUB_SECURITY_INCIDENT
                {
                    Sec_Inc_Sub_cat_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("SecurityIncidentMaster/Sub_Security_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SUB_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_SUB_SECURITY_INCIDENT>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> SubCate_GetAll()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("SecurityIncidentMaster/Sub_Security_GetAll_Category").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SUB_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_SUB_SECURITY_INCIDENT>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Sub_Security_Delete(string ID)
        {
            using (client)
            {
                SUB_SECURITY_INCIDENT _UNIT = new SUB_SECURITY_INCIDENT
                {
                    Sec_Inc_Sub_cat_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("SecurityIncidentMaster/Sub_Security_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SUB_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_SUB_SECURITY_INCIDENT>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        public async Task<IActionResult> MainCategory_GetAll()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("SecurityIncidentMaster/Sec_Inc_Category_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SUB_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_SUB_SECURITY_INCIDENT>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        #endregion

        #endregion
        #region [SECURITY INCIDENT]
        public IActionResult Sec_Incident_Report()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetAll_SecIncident_Work([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            GET_SECURITY_INCIDENT deserialized = new GET_SECURITY_INCIDENT();
            Login_ LoginClass = GetLoginDetails();

            HttpResponseMessage response = client.PostAsync("SecurityIncidentMaster/Sec_Inc_Report_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<GET_SECURITY_INCIDENT>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Sec_Incident_Report>();
            }
            else
            {
                foreach (var item in deserialized.Get_All!)
                {
                    item.Role_Id = LoginClass.Role_Id;
                    item.Sec_Escalation = item.Sec_Escalation;
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
      
        [HttpPost]
        public async Task<IActionResult> AddSecurityIncReport(M_Sec_Incident_Report model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                //List<M_Sec_Inc_Photos> pHOTOs = new List<M_Sec_Inc_Photos>();
                //List<M_Sec_Inc_Videos> Videos = new List<M_Sec_Inc_Videos>();
                //var Secure = HttpContext.Session.GetString("Photo");
                //if (Secure != null)
                //{
                //    string[] photo = JsonConvert.DeserializeObject<string[]>(Secure)!;
                //    if (photo != null)
                //    {
                //        for (int i = 0; i < photo.Length; i++)
                //        {
                //            M_Sec_Inc_Photos pHotos = new M_Sec_Inc_Photos
                //            {
                //                Photo_File_Path = photo[i]
                //            };
                //            pHOTOs.Add(pHotos);
                //        }
                //    }
                //}
                //var SecureVideo = HttpContext.Session.GetString("Video");
                //if (SecureVideo != null)
                //{
                //    string[] videos = JsonConvert.DeserializeObject<string[]>(SecureVideo)!;
                //    if (videos != null)
                //    {
                //        for (int i = 0; i < videos.Length; i++)
                //        {
                //            M_Sec_Inc_Videos vIdeo = new M_Sec_Inc_Videos
                //            {
                //                Video_File_Path = videos[i]
                //            };
                //            Videos.Add(vIdeo);
                //        }
                //    }
                //}
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                model.Role_Id = LoginClass.Role_Id;
                //model.L_Sec_Inc_Photos = pHOTOs;
                //model.L_Sec_Inc_Videos = Videos;
                if (model.Sec_Inc_Report_Id == "0")
                {
                    URL = "SecurityIncidentMaster/Sec_Inc_Report_Add";
                }
                else
                {
                    URL = "SecurityIncidentMaster/Sec_Inc_Report_Update";
                }
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> Sec_Inc_Report_GetByID(string ID)
        {

            using (client)
            {
                M_Sec_Incident_Report _UNIT = new M_Sec_Incident_Report
                {
                    Sec_Inc_Report_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("SecurityIncidentMaster/Sec_Inc_Report_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_SECURITY_INCIDENT>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        public async Task<IActionResult> Sec_Inc_Report_Delete(string ID)
        {

            using (client)
            {
                M_Sec_Incident_Report _UNIT = new M_Sec_Incident_Report
                {
                    Sec_Inc_Report_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("SecurityIncidentMaster/Sec_Inc_Report_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_SECURITY_INCIDENT>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        public async Task<IActionResult> ViewSecurityIncidentReport(int Sec_Inc_Report_Id)
        {
            Login_ LoginClass = GetLoginDetails();
            using (client)
            {
                M_Sec_Incident_Report _UNIT = new M_Sec_Incident_Report
                {
                    Sec_Inc_Report_Id = Convert.ToString(Sec_Inc_Report_Id),
                    //Zone_Id = LoginClass.Zone_Id
                };

                HttpResponseMessage response = client.PostAsync("SecurityIncidentMaster/Sec_Incident_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_SECURITY_INCIDENT>(customerJsonString)!;
                return PartialView("ViewSecurityIncidentReport", deserialized!.Get_ById);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddInvestigationFinding([FromBody] M_Sec_Investigation_Find_List model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "SecurityIncidentMaster/Add_Invest_Finding_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        // New Action Taken 
        [HttpPost]
        public async Task<IActionResult> Security_ActionTaken_Update([FromBody] M_Sec_Incident_ActionTaken _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                //M_Sec_Incident_Report _UNIT = new M_Sec_Incident_Report
                //{
                //    Sec_Inc_Report_Id = Sec_Report_Id,
                //    Sec_Action = Sec_Action_Taken,
                //    Sec_Comments = Sec_Comments,
                //    Sec_Address = Sec_Recommended,

                //};
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("SecurityIncidentMaster/Security_Inc_ActionTaken_AddNew", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Security_Inc_FollowUpAction_Add([FromBody] M_Sec_Incident_FollowUpAction _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();

                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("SecurityIncidentMaster/Security_Inc_FollowUpAction_Add", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> SecurityIncActionStatus([FromBody] M_Sec_Incident_Report model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "SecurityIncidentMaster/SecurityIncActionStatus";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                model.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> Get_Filter_Inc_Sec_List(string Zone_Id, string Community_Id, string Building_Id, string Sec_Inc_Category_Id, string Sec_Sub_Cat_Id)
        {
            using (client)
            {
                M_Sec_Incident_Report _UNIT = new M_Sec_Incident_Report()
                {
                    Zone_Id = Zone_Id,
                    Community_Id = Community_Id,
                    Building_Id = Building_Id,
                    Sec_Inc_Category_Id = Sec_Inc_Category_Id,
                    Sec_Sub_Cat_Id = Sec_Sub_Cat_Id,
                };
                //HttpResponseMessage response = client.GetAsync("ServiceStatistics/GetService_Monthly_Statics").Result;
                HttpResponseMessage response = client.PostAsync("SecurityIncidentMaster/Get_Filter_Sec_Inc_List", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_SECURITY_INCIDENT>(customerJsonString)!;
                return PartialView(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> LoadAllLocationbyBuilding(string Zone_Id, string Community_Id, string Building_Id)
        {
            using (client)
            {
                M_Sec_Incident_Report _UNIT = new M_Sec_Incident_Report
                {
                    Building_Id = Building_Id,
                    Zone_Id = Zone_Id,
                    Community_Id = Community_Id
                };
                HttpResponseMessage response = client.PostAsync("SecurityIncidentMaster/Sec_Get_Location", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_SECURITY_INCIDENT>(customerJsonString)!;
                return Json(deserialized!.Get_Location_List);
            }
        }
        #endregion
        #region[SECURITY FILE UPLOAD]
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
        public async Task<JsonResult> SecurityUploadPhotoFiles(List<IFormFile> files)
        {
            try
            {
                string url = "SecurityIncidentMaster/ImageFileUpload";
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
        public async Task<JsonResult> SecurityUploadVideoFiles(List<IFormFile> files)
        {
            try
            {
                var obsVideos = "";
                using (client)
                {
                    string url = "SecurityIncidentMaster/VideoUpload";
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
        #region[SECURITY MASTER LOAD]
        public async Task<IActionResult> LoadAllSecInc_Cat()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("SecurityIncidentMaster/Sec_Inc_Category_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SECINCIDENT_CATEGORY deserialized = JsonConvert.DeserializeObject<GET_SECINCIDENT_CATEGORY>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> LoadSubCategory_GetAll(string Sec_Inc_Category_Id)
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("SecurityIncidentMaster/Sub_Security_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SUB_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_SUB_SECURITY_INCIDENT>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> LoadSubCategory_GetById(string Sec_Inc_Category_Id)
        {
            using (client)
            {
                M_Sec_Incident_Report _UNIT = new M_Sec_Incident_Report
                {
                    Sec_Inc_Category_Id = Sec_Inc_Category_Id,
                };
                HttpResponseMessage response = client.PostAsync("SecurityIncidentMaster/Sub_SecurityCategory_GetId", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_SECURITY_INCIDENT>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> LoadDeptSection_GetAll(string Sec_Sub_Cat_Id)
        {
            using (client)
            {
                M_Sec_Incident_Report _UNIT = new M_Sec_Incident_Report
                {
                    Sec_Sub_Cat_Id = Sec_Sub_Cat_Id,
                };
                HttpResponseMessage response = client.PostAsync("SecurityIncidentMaster/Sub_SecDeptSection_GetId", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_SECURITY_INCIDENT>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        //public async Task<IActionResult> LoadAllLocationbyBuilding(string Zone_Id, string Community_Id, string Building_Id)
        //{
        //    using (client)
        //    {
        //        M_Sec_Incident_Report _UNIT = new M_Sec_Incident_Report
        //        {
        //            Building_Id = Building_Id,
        //            Zone_Id = Zone_Id,
        //            Community_Id = Community_Id
        //        };
        //        HttpResponseMessage response = client.PostAsync("SecurityIncidentMaster/Sec_Get_Location", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
        //        string customerJsonString = await response.Content.ReadAsStringAsync();
        //        GET_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_SECURITY_INCIDENT>(customerJsonString)!;
        //        return Json(deserialized!.Get_Location_List);
        //    }
        //}
        #endregion
    }
}