using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.ControlOfWorkMaster;
using Nakheel_Web.Models.IncidentMaster;
using Nakheel_Web.Models.IncidentReport;
using Nakheel_Web.Models.Masters;
using Nakheel_Web.Models.SecurityIncidentMaster;
using Nakheel_Web.Models.Emergency;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using static Nakheel_Web.Authentication.Common;
using Microsoft.AspNetCore.Authorization;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class Emergencymitigation : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string Knowledge_Share_File;
        private readonly string ApplicableReportPath;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        [Obsolete]
        public Emergencymitigation(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
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
        public IActionResult Index()
        {
            return View();
        }


        #region[EMER MITIGATION]
        [HttpPost]
        public async Task<IActionResult> GetAll_Emer_Mitigation([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            GET_EMERGENCY_MITIGATION deserialized = new GET_EMERGENCY_MITIGATION();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("Emergency/Emer_Mitigation_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<GET_EMERGENCY_MITIGATION>(customerJsonString)!;
            //foreach (var item in deserialized.Get_All!)
            //{
            //    item.Role_Id = LoginClass.Role_Id;
            //}
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Emer_Mitigation_Add>();
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
        public async Task<IActionResult> AddEmer_Mitigation([FromBody] M_Emer_Mitigation_Add model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                List<M_Emer_Miti_Photos> pHOTOs = new List<M_Emer_Miti_Photos>();
                var Secure = HttpContext.Session.GetString("Photo");
                if (Secure != null)
                {
                    string[] photo = JsonConvert.DeserializeObject<string[]>(Secure)!;
                    if (photo != null)
                    {
                        for (int i = 0; i < photo.Length; i++)
                        {
                            M_Emer_Miti_Photos pHotos = new M_Emer_Miti_Photos
                            {
                                Photo_File_Path = photo[i]
                            };
                            pHOTOs.Add(pHotos);
                        }
                    }
                }
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                model.Role_Id = LoginClass.Role_Id;
                model.L_Emer_Miti_Photos = pHOTOs;
                URL = "Emergency/Emer_Mitigation_Add";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        public async Task<IActionResult> ViewEmer_Mitigation(int Emer_Miti_Id)
        {

            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Emer_Mitigation_Add _UNIT = new M_Emer_Mitigation_Add
                {
                    Emer_Miti_Id = Convert.ToString(Emer_Miti_Id),
                    Zone_Id = LoginClass.Zone_Id
                };
                HttpResponseMessage response = client.PostAsync("Emergency/Emer_Miti_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_EMERGENCY_MITIGATION deserialized = JsonConvert.DeserializeObject<GET_EMERGENCY_MITIGATION>(customerJsonString)!;
                return PartialView("ViewEmer_Mitigation", deserialized!.Get_ById);
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateEmergency_MitigationStatus([FromBody] Emergency_Mitigation_Update_History model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                model.Emp_Id = LoginClass.Employee_Identity_Id;
                model.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("Emergency/UpdateEmergency_MitigationStatus", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized.STATUS_CODE);
            }
        }
        #endregion

        #region[EMERGENCY ALERT]
        public async Task<IActionResult> Emergency_Alert_Master()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("Emergency/Emer_Alert_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Emergency_Alert_Master deserialized = JsonConvert.DeserializeObject<Get_Emergency_Alert_Master>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmergencyAlert_Master([FromBody] Emergency_Alert_Master model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Emergency_Alert_Id == "0")
                    {
                        URL = "Emergency/Emer_Alert_Add";
                    }
                    else
                    {
                        URL = "Emergency/Emer_Alert_Update";
                    }
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

        public async Task<IActionResult> Emergency_Alert_GetByID(string ID)
        {

            using (client)
            {
                Emergency_Alert_Master _UNIT = new Emergency_Alert_Master
                {
                    Emergency_Alert_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("Emergency/Emergency_Category_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Emergency_Alert_Master deserialized = JsonConvert.DeserializeObject<Get_Emergency_Alert_Master>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> Emergency_Alert_Delete(string ID)
        {
            using (client)
            {
                Emergency_Alert_Master _UNIT = new Emergency_Alert_Master
                {
                    Emergency_Alert_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("MasterCommunity/Emergency_Category_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        public async Task<IActionResult> _View_Emergency_Alert_list(string Emergency_Alert_Id)
        {
            using (client)
            {
                Emergency_Alert_Master _UNIT = new Emergency_Alert_Master
                {
                    Emergency_Alert_Id = Emergency_Alert_Id
                };
                HttpResponseMessage response = client.PostAsync("Emergency/Emer_Alert_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Emergency_Alert_Master deserialized = JsonConvert.DeserializeObject<Get_Emergency_Alert_Master>(customerJsonString)!;
                return PartialView(deserialized.Get_ById);
            }
        }

        #endregion

        #region[FILE UPLOAD]
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
        public async Task<JsonResult> EmerEviUploadPhotoFiles(List<IFormFile> files)
        {
            try
            {
                string url = "Emergency/ImageFileUpload";
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
        #endregion
    }
}
