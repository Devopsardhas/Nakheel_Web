using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Models.Masters;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using static Nakheel_Web.Authentication.Common;
using System.Text;
using NuGet.Protocol.Plugins;
using System.Security.Policy;
using Nakheel_Web.Models.AccountsMaster;
using System.Text.RegularExpressions;
using Nakheel_Web.Models;
using System.Configuration;
using Nakheel_Web.Models.IncidentReport;
using Nakheel_Web.Models.ControlOfWorkMaster;
using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
//using System.Web.Helpers;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string SignupReportPath;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        [Obsolete]
        public AccountController(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            client = httpClientFactory.CreateClient("API");
            _hostingEnvironment = hostingEnvironment;
            SignupReportPath = configuration.GetConnectionString("SignupReportPath");
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> AddLogin(Login_ login_)
        {
            try
            {
                Login_ Login_1 = new Login_();
                Login_1.Email_Id = login_.User_Name;
                Login_1.Password = login_.Password;
                HttpResponseMessage response = client.PostAsync("Accounts/User_Verification", new StringContent(JsonConvert.SerializeObject(Login_1), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_LOGIN_DETAILS deserialized = JsonConvert.DeserializeObject<GET_LOGIN_DETAILS>(customerJsonString)!;
                if (deserialized.STATUS_CODE == "200")
                {
                    var str = Encrypt(JsonConvert.SerializeObject(deserialized.Get_User));
                    SetLocSession("Login", str);
                    SetLocSession("JWT", deserialized.Get_User!.JWT_Token!);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Res"] = "Fail";
                    return View("Login");
                }
            }
            catch (Exception ex)
            {
                TempData["Res"] = ex.InnerException!.Message;
                throw;
            }
        }

        private void SetLocSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("Login");
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/Account/Login");
        }

        #region [BELL NOTIFICATION]
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

        [HttpPost]
        public async Task<IActionResult> GeBellNotification()
        {
            List<tbl_Notification_Sequence> tbl_Notification_s = new List<tbl_Notification_Sequence>();

            Login_ login_ = GetLoginDetails();
            tbl_Notification_Sequence _UNIT = new tbl_Notification_Sequence
            {
                Login_User_Id = login_.Employee_Identity_Id
            };
            HttpResponseMessage response = client.PostAsync("Accounts/GetWebBellNotification", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            Get_tbl_Notification_Sequence deserialized = JsonConvert.DeserializeObject<Get_tbl_Notification_Sequence>(customerJsonString)!;
            if (deserialized.Get_All_Notifications!.Count != 0 && deserialized.Get_All_Notifications != null)
            {
                tbl_Notification_s = deserialized.Get_All_Notifications;
            }
            return Json(tbl_Notification_s);

        }

        #endregion

        #region [SERVICE PROVIDER SIGN-UP]

      
        public IActionResult Service_Provider_Sign_Up()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add_Service_Provider_Sign_Up([FromBody] ServiceProviderSignUp _UNIT)
        {
            using (client)
            {
                if (_UNIT.SignUp_Id == "0")
                {
                    HttpResponseMessage response = client.PostAsync("Accounts/Add_Service_Provider_Sign_Up", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    return Json(deserialized!.STATUS_CODE);
                }
                else
                {
                    HttpResponseMessage response = client.PostAsync("Accounts/UpdateDelete_Service_Provider_Sign_Up", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    return Json(deserialized!.STATUS_CODE);
                }

            }
        }

        public async Task<IActionResult> UpdateService_Provider_Sign_Up(string id)
        {
            ServiceProviderSignUp _UNIT = new()
            {
                SignUp_Id = Convert.ToString(id),
            };

            HttpResponseMessage response = client.PostAsync("Accounts/Update_Service_Provider_Sign_Up", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            Get_ServiceProviderSignUp deserialized = JsonConvert.DeserializeObject<Get_ServiceProviderSignUp>(customerJsonString)!;
            return View("UpdateService_Provider_Sign_Up", deserialized!.Get_ById);
        }


        public async Task<JsonResult> UpdateService_Provider(int Company_id)
        {
            ServiceProviderSignUp _UNIT = new()
            {
                Company_Id = Convert.ToString(Company_id),
            };
            HttpResponseMessage response = client.PostAsync("Accounts/UpdateService_Provider_Id", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
            return Json(deserialized.STATUS_CODE);
        }

        public IActionResult ServiceProviderDetails()
        {
            return View();
        }

        public async Task<IActionResult> _View_ServiceProvider_list(int Comp_Id)
        {
            using (client)
            {
                Service_Company_Details model = new Service_Company_Details();
                model.Company_Id = Convert.ToString(Comp_Id);
                HttpResponseMessage response = client.PostAsync("Accounts/Get_Service_Provider_DetailsbyId", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_ServiceProviderSignUp deserialized = JsonConvert.DeserializeObject<Get_ServiceProviderSignUp>(customerJsonString)!;
                return PartialView(deserialized!.Get_All);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Add_Company_Details([FromBody] Service_Company_Details model)
        {
            using (client)
            {
                HttpResponseMessage response = client.PostAsync("Accounts/Add_Company_Details", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized);
            }
        }

        #endregion

        #region [MASTERS]

        public async Task<IActionResult> LoadCompanyDetails(string Company_Id)
        {
            using (client)
            {
                Service_Company_Details _UNIT = new Service_Company_Details();
                _UNIT.Company_Id = Company_Id;

                HttpResponseMessage response = client.PostAsync("Accounts/Get_Company_Details", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Service_Company_Details deserialized = JsonConvert.DeserializeObject<Get_Service_Company_Details>(customerJsonString)!;
                return Json(deserialized!.Get_Company_Details);
            }
        }

        public async Task<IActionResult> LoadAllBusinessUnit()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("BusinessUnitMaster/Business_Unit_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_BUSINESS_UNIT deserialized = JsonConvert.DeserializeObject<GET_BUSINESS_UNIT>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadAllZone()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("ZoneMaster/Zone_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ZONE_MANAGEMENT deserialized = JsonConvert.DeserializeObject<GET_ZONE_MANAGEMENT>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadAllMasterCommunitybyZone(string Zone_Id, string Community_Id)
        {
            using (client)
            {

                MASTER_COMMUNITY_MANAGEMNT _UNIT = new MASTER_COMMUNITY_MANAGEMNT
                {
                    Zone_Id = Zone_Id,
                    Community_Id = "0"
                };
                HttpResponseMessage response = client.PostAsync("MasterCommunity/Get_All_MasterCommunity_byZone", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_MASTER_COMMUNITY_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_MASTER_COMMUNITY_MANAGEMNT>(customerJsonString)!;
                if (deserialized.Get_All_Sub != null)
                {
                    return Json(deserialized!.Get_All_Sub);
                }
                else
                {
                    MASTER_COMMUNITY_MANAGEMNT _UNIT_1 = new MASTER_COMMUNITY_MANAGEMNT
                    {
                        Zone_Id = Zone_Id,
                        Community_Id = Community_Id
                    };
                    HttpResponseMessage response1 = client.PostAsync("MasterCommunity/Get_All_MasterCommunity_byZone", new StringContent(JsonConvert.SerializeObject(_UNIT_1), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString1 = await response1.Content.ReadAsStringAsync();
                    GET_MASTER_COMMUNITY_MANAGEMNT deserialized1 = JsonConvert.DeserializeObject<GET_MASTER_COMMUNITY_MANAGEMNT>(customerJsonString1)!;
                    return Json(deserialized1!.Get_All_Sub);
                }
            }
        }

        public async Task<IActionResult> LoadAllBuildingbyZone(string Zone_Id, string Community_Id)
        {
            using (client)
            {
                BUILDING_MANAGMENT _UNIT = new BUILDING_MANAGMENT
                {
                    Zone_Id = Zone_Id,
                    Community_Id = Community_Id
                };
                HttpResponseMessage response = client.PostAsync("BuildingMaster/Get_All_Building_byZone", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_BUILDING_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_BUILDING_MANAGEMNT>(customerJsonString)!;
                return Json(deserialized!.Get_All_Sub);
            }
        }

        public async Task<IActionResult> LoadAllCommunitybyZone(string Zone_Id)
        {
            using (client)
            {
                COMMUNITY_MANAGEMNT _UNIT = new COMMUNITY_MANAGEMNT
                {
                    Zone_Id = Zone_Id
                };
                HttpResponseMessage response = client.PostAsync("CommunityMaster/Community_Master_GetAllbyZone", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_COMMUNITY_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_COMMUNITY_MANAGEMNT>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> Load_Master_Major_HSE_Risk()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("ControlofWorkMaster/COW_GenWork_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_Major_HSE_Risk deserialized = JsonConvert.DeserializeObject<GET_Major_HSE_Risk>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }


        public async Task<IActionResult> LoadAllScope_of_Work()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("MasterCommunity/Scope_of_Work_Master_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Service_Provider_Scope_of_Work deserialized = JsonConvert.DeserializeObject<Get_Service_Provider_Scope_of_Work>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        #endregion

        #region [FILE UPLOAD]
        public async Task<JsonResult> UploadPhotoFiles(List<IFormFile> files, string Keyname)
        {
            try
            {
                string url = "Accounts/ServiceProviderFileUpload";
                string[] deserialized = await FileUpload.UploadMultipleFiles(files, url, client);
                var str = JsonConvert.SerializeObject(deserialized);
                if (Keyname == "HSE_Plan")
                {
                    HttpContext.Session.SetString(Keyname, str);
                }

                return Json("Uploaded Sccuessfully");
            }
            catch (Exception ex)
            {
                return Json("Error");
            }
        }


        public async Task<JsonResult> UploadImage(List<IFormFile> files)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = client.BaseAddress + "InspectionForm/UploadImage";

                    string[] deserialized = await FileUpload.UploadMultipleFiles(files, url, client);
                    var key = "Photo";
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


        public async Task<JsonResult> Internal_Aud_UploadPhotoFiles(List<IFormFile> files, string Keyname)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = "AuditMaster/ImageUpload";
                    string[] deserialized = await FileUpload.UploadMultipleFiles(files, url, client);
                    var key = "I";
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
        public string FiletoDataApplicableReports(string FileData)
        {
            try
            {
                if (FileData != null)
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
                        string projectRootPath = $"{this._webHostEnvironment.WebRootPath}\\ServiceProvidersFiles\\";
                        //var Savepath = 
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
                        AttachmentName = SignupReportPath + "/ServiceProvidersFiles/" + FileName + ext;
                    }
                    return AttachmentName;
                }
                else
                {
                    return "Failed";
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [EMAIL_CHECK]
        public async Task<IActionResult> Email_Id_Check_Duilpcate(string Email_Id, string Company_Id)
        {
            using (client)
            {
                M_Employee_Master _UNIT = new M_Employee_Master();
                _UNIT.User_Name = Email_Id;
                _UNIT.Company_Name = Company_Id;
                HttpResponseMessage response = client.PostAsync("Accounts/Email_Id_Check_Duilpcate", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized);
            }
        }
        #endregion

        #region [COMPANY_NAME_CHECK]
        public async Task<IActionResult> Company_Name_Check_Duilpcate(string Company_Name)
        {
            using (client)
            {
                M_Employee_Master _UNIT = new M_Employee_Master();
                _UNIT.Company_Name = Company_Name;
                HttpResponseMessage response = client.PostAsync("Accounts/Company_Name_Check_Duilpcate", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized);
            }
        }
        #endregion

        #region [PURCHASE_ORDER_NUMBER_CHECK]
        public async Task<IActionResult> Purchase_Order_Number_Check_Duilpcate(string Purchase_Order_Number)
        {
            using (client)
            {
                M_Employee_Master _UNIT = new M_Employee_Master();
                _UNIT.Company_Name = Purchase_Order_Number;
                HttpResponseMessage response = client.PostAsync("Accounts/Purchase_Order_Number_Check_Duilpcate", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized);
            }
        }
        #endregion
    }
}
