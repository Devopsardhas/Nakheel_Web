
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.IncidentReport;
using Nakheel_Web.Models.InspectionMaster;
using Nakheel_Web.Models.Masters;
using Nakheel_Web.Models.ControlOfWorkMaster;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using static Nakheel_Web.Authentication.Common;
using Nakheel_Web.Models.AuditMaster;
using Nakheel_Web.Models.HandOverInsMaster;
using Nakheel_Web.Models.SecurityIncidentMaster;
using Microsoft.AspNetCore.Authorization;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class ControlOfWorkController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        [Obsolete]
        public ControlOfWorkController(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
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
        //private ServiceProviderSignUp GetServiceProvider()
        //{
        //    ServiceProviderSignUp LoginAccount = new ServiceProviderSignUp();
        //    var strAccount = HttpContext.Session.GetString("LoginAccount");
        //    string Des_Acc = Decrypt(strAccount!);
        //    if (Des_Acc != "")
        //    {
        //        LoginAccount = JsonConvert.DeserializeObject<ServiceProviderSignUp>(Des_Acc)!;
        //    }
        //    return LoginAccount;
        //}
        #endregion

        #region[COW Major HSE Risk]
        public async Task<IActionResult> MajorHSERiskMaster()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("ControlofWorkMaster/COW_GenWork_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_Major_HSE_Risk deserialized = JsonConvert.DeserializeObject<GET_Major_HSE_Risk>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> MajorHSERisk_GetAll()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("ControlofWorkMaster/COW_GenWork_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_Major_HSE_Risk deserialized = JsonConvert.DeserializeObject<GET_Major_HSE_Risk>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MajorHSERisk_Add(M_Major_HSE_Risk model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    string URL = "";
                    if (model.Major_HSE_Work_Id == "0")
                    {
                        URL = "ControlofWorkMaster/COW_GenWork_Add";
                    }
                    else
                    {
                        URL = "ControlofWorkMaster/COW_GenWork_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("MajorHSERiskMaster");
            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> MajorHSERisk_GetByID(string ID)
        {

            using (client)
            {
                M_Major_HSE_Risk _UNIT = new M_Major_HSE_Risk
                {
                    Major_HSE_Work_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/COW_GenWork_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_Major_HSE_Risk deserialized = JsonConvert.DeserializeObject<GET_Major_HSE_Risk>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        public async Task<IActionResult> MajorHSERisk_Delete(string ID)
        {

            using (client)
            {
                M_Major_HSE_Risk _UNIT = new M_Major_HSE_Risk
                {
                    Major_HSE_Work_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/COW_GenWork_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_Major_HSE_Risk deserialized = JsonConvert.DeserializeObject<GET_Major_HSE_Risk>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        #endregion

        #region[COW Major HSE Risk Questioneries]
        public async Task<IActionResult> Major_HSE_Question()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("ControlofWorkMaster/MajorHSE_Ques_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Major_HSE_QUestion deserialized = JsonConvert.DeserializeObject<M_Get_Major_HSE_QUestion>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Major_HSE_Question_Add(List<M_Major_HSE_QUestion> model)
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
                    string URL = "ControlofWorkMaster/MajorHSE_Ques_Add";
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

        public async Task<IActionResult> Major_HSE_Question_GetByID(string ID)
        {

            using (client)
            {
                M_Major_HSE_QUestion _UNIT = new M_Major_HSE_QUestion
                {
                    Major_HSE_Work_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/MajorHSE_Ques_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Major_HSE_QUestion deserialized = JsonConvert.DeserializeObject<M_Get_Major_HSE_QUestion>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> Major_HSE_Question_Delete(string ID)
        {
            using (client)
            {
                M_Major_HSE_QUestion _UNIT = new M_Major_HSE_QUestion
                {
                    Major_HSE_Ques_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/MajorHSE_Ques_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Major_HSE_QUestion deserialized = JsonConvert.DeserializeObject<M_Get_Major_HSE_QUestion>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion
        public IActionResult General_WorkPermit()
        {
            return View();
        }
        
        public IActionResult Electrical_Work_Permit()
        {
            return View();
        }
        public IActionResult FireSafety_Work_Permit()
        {
            return View();
        }
        public IActionResult Hot_Work_Permit()
        {
            return View();
        }
        public IActionResult WorkAt_Height_Permit()
        {
            return View();
        }
        public IActionResult third_Party_Permit()
        {
            return View();
        }

        #region [Confined Space Permit]
        public async Task<IActionResult> Load_Confined_Space_Ques()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("ControlofWorkMaster/Get_All_Questionnaire_Work").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_PTW_Questionnaries_Details deserialized = JsonConvert.DeserializeObject<M_Get_PTW_Questionnaries_Details>(customerJsonString)!;
                return Json(deserialized!.Get_All_Questionnaire);
            }
        }
        public IActionResult Confined_Space_Permit()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetAll_Confined_Permit_Work([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            GET_CONFINED_WORK_PERMIT deserialized = new GET_CONFINED_WORK_PERMIT();
            Login_ LoginClass = GetLoginDetails();
            //model.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Get_All_Confined_Space_Req", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;
            //foreach (var item in deserialized.Get_All!)
            //{
            //    item.Role_Id = LoginClass.Role_Id;
            //}
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<Ptw_Confined_Space_Add>();
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
        public async Task<IActionResult> AddConfinedSpaceWorkPermit([FromBody] Ptw_Confined_Space_Add model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                
                string URL = "";


                
                    URL = "ControlofWorkMaster/Add_Confined_Space_Req";
               
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> _ViewConfinedSpace(int CSP_Id)
        {
            using (client)
            {
                Ptw_Confined_Space_Add _UNIT = new Ptw_Confined_Space_Add
                {
                    CSP_Id = Convert.ToString(CSP_Id),
                };

                HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Edit_Confined_Space", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_CONFINED_WORK_PERMIT deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;
                return PartialView("_ViewConfinedSpace", deserialized!.Get_ById);
            }
        }     
        [HttpPost]
        public async Task<JsonResult> _EditConfinedSpace(string ID)
        {
            using (client)
            {
                Ptw_Confined_Space_Add _UNIT = new Ptw_Confined_Space_Add
                {
                    CSP_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Edit_Confined_Space", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_CONFINED_WORK_PERMIT deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add_Sp_Additional_Details([FromBody] Ptw_Sp_Add_Evidence_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Add_Sp_Additional_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit_Sp_Additional_Details([FromBody] Ptw_Sp_Add_Evidence_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Edit_Sp_Additional_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Sp_Renewal_Details([FromBody] Ptw_Sp_Add_Renewal_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Add_Sp_Renewal_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit_Sp_Renewal_Details([FromBody] Ptw_Sp_Add_Renewal_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Edit_Sp_Renewal_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        public IActionResult _Edit_Load_Google_Map(string Lat,string Long,string Loc_Address,string Exact_Location)
        {
            Ptw_Confined_Space_Add model = new Ptw_Confined_Space_Add()
            {
                Latitude = Lat,
                Longitude = Long,
                Location_Address = Loc_Address,
                Exact_Loc_Address = Exact_Location
            };
            return PartialView(model);
        }
        #region

        [HttpPost]
        public async Task<IActionResult> Add_Zone_Confined_Space([FromBody] Ptw_History_of_Approval model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                 URL = "ControlofWorkMaster/Add_Zone_HSE_Csp_Approve";
               
                //model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> Add_Zone_Confined_Space_Reject([FromBody] Ptw_History_of_Approval model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                URL = "ControlofWorkMaster/Add_Zone_Csp_Reject";

                //model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add_Hse_Confined_Space_Reject([FromBody] Ptw_History_of_Approval model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                URL = "ControlofWorkMaster/Add_HSE_Csp_Reject";

                //model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Zone_Evd_Reject([FromBody] Ptw_History_of_Approval model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                URL = "ControlofWorkMaster/Add_Zone_Evd_Reject";

                //model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add_Zone_Renewal_Reject([FromBody] Ptw_History_of_Approval model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                URL = "ControlofWorkMaster/Add_Zone_Renewal_Reject";

                //model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_HSE_Evd_Reject([FromBody] Ptw_History_of_Approval model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                URL = "ControlofWorkMaster/Add_HSE_Evd_Reject";

                //model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<JsonResult> Get_Contractor_Load(string SignUp_Id)
        {
            using (client)
            {
                Ptw_Contractor_Load_Details _UNIT = new Ptw_Contractor_Load_Details
                {
                    SignUp_Id = SignUp_Id
                };
                HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Get_Contractor_Load", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_CONFINED_WORK_PERMIT deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;
                return Json(deserialized!.Get_ByAll);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Get_Competent_Number(string Work_Superviosor_Id)
        {
            using (client)
            {
                Ptw_Competent_Number _UNIT = new Ptw_Competent_Number
                {
                    Work_Superviosor_Id = Work_Superviosor_Id
                };
                HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Get_Competent_Number", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_CONFINED_WORK_PERMIT deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;
                return Json(deserialized!.Get_By_Number);
            }
        }
        public async Task<IActionResult> Get_Con_Pur_Drp(string Official_Email_Id)
        {
            using (client)
            {

                Ptw_Contractor_Load_Details _UNIT = new Ptw_Contractor_Load_Details
                {
                    Official_Email_Id = Official_Email_Id

                };
                HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Get_Con_Pur_Drp", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_CONFINED_WORK_PERMIT deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;
                return Json(deserialized!.Get_All_Cont);

            }
        }
        #endregion

        #region[File Upload]
        public async Task<JsonResult> ConUploadFileUpload(List<IFormFile> files)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = "ControlofWorkMaster/CSP_Staff_Upload";
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
        public async Task<JsonResult> CSP_Sp_Evidence_Upload(List<IFormFile> files)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = "ControlofWorkMaster/CSP_Sp_Evidence_Upload";
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
        #endregion

        #endregion

        #region [Electrical Work Permit]
        public async Task<IActionResult> Load_Electrical_Work_Ques()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("ControlofWorkMaster/Get_All_Electrical_Question").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_PTW_Questionnaries_Details deserialized = JsonConvert.DeserializeObject<M_Get_PTW_Questionnaries_Details>(customerJsonString)!;
                return Json(deserialized!.Get_All_Questionnaire);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Get_All_Electrical_Work([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            GET_CONFINED_WORK_PERMIT deserialized = new GET_CONFINED_WORK_PERMIT();
            Login_ LoginClass = GetLoginDetails();
            HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Get_All_Electrical_Work", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;
            
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<Ptw_Confined_Space_Add>();
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
        public async Task<IActionResult> Add_Electrical_Work_Req([FromBody] Ptw_Confined_Space_Add model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();

                string URL = "";
                URL = "ControlofWorkMaster/Add_Electrical_Work_Req";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> _ViewElectricalWork(int EWP_Id)
        {
            using (client)
            {
                Ptw_Confined_Space_Add _UNIT = new Ptw_Confined_Space_Add
                {
                    EWP_Id = Convert.ToString(EWP_Id),
                };

                HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Edit_Electrical_Work", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_CONFINED_WORK_PERMIT deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;
                return PartialView("_ViewElectricalWork", deserialized!.Get_ById);
            }
        }
        [HttpPost]
        public async Task<JsonResult> _EditElectricalWork(string ID)
        {
            using (client)
            {
                Ptw_Confined_Space_Add _UNIT = new Ptw_Confined_Space_Add
                {
                    EWP_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Edit_Electrical_Work", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_CONFINED_WORK_PERMIT deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Zone_HSE_EWP_Approve([FromBody] Ptw_History_of_Approval model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Add_Zone_HSE_EWP_Approve";
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Sp_EWP_Additional_Details([FromBody] Ptw_Sp_Add_Evidence_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Add_Sp_EWP_Additional_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit_Sp_EWP_Additional_Details([FromBody] Ptw_Sp_Add_Evidence_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Edit_Sp_EWP_Additional_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Sp_EWP_Renewal_Details([FromBody] Ptw_Sp_Add_Renewal_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Add_Sp_EWP_Renewal_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit_Sp_EWP_Renewal_Details([FromBody] Ptw_Sp_Add_Renewal_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Edit_Sp_EWP_Renewal_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        //Reject
        public async Task<IActionResult> Add_Zone_EWP_Reject([FromBody] Ptw_History_of_Approval model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                URL = "ControlofWorkMaster/Add_Zone_EWP_Reject";

                //model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        #endregion

        #region [Fire & Safety Work Permit]
        public async Task<IActionResult> Load_Fire_Safety_Ques()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("ControlofWorkMaster/Get_All_Fire_Safety_Question").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_PTW_Questionnaries_Details deserialized = JsonConvert.DeserializeObject<M_Get_PTW_Questionnaries_Details>(customerJsonString)!;
                return Json(deserialized!.Get_All_Questionnaire);
            }
        }
        #endregion

        #region [Hot Work Permit]
        public async Task<IActionResult> Get_All_HotWork_Question()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("ControlofWorkMaster/Get_All_HotWork_Question").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_PTW_Questionnaries_Details deserialized = JsonConvert.DeserializeObject<M_Get_PTW_Questionnaries_Details>(customerJsonString)!;
                return Json(deserialized!.Get_All_Questionnaire);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Get_All_Hot_Work([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            GET_CONFINED_WORK_PERMIT deserialized = new GET_CONFINED_WORK_PERMIT();
            Login_ LoginClass = GetLoginDetails();
            HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Get_All_Hot_Work", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;

            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<Ptw_Confined_Space_Add>();
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
        public async Task<IActionResult> Add_Hot_Work_Req([FromBody] Ptw_Confined_Space_Add model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();

                string URL = "";
                URL = "ControlofWorkMaster/Add_Hot_Work_Req";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        public async Task<IActionResult> _ViewHotWork(int CSP_Id)
        {
            using (client)
            {
                Ptw_Confined_Space_Add _UNIT = new Ptw_Confined_Space_Add
                {
                    CSP_Id = Convert.ToString(CSP_Id),
                };

                HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Edit_Hot_Work", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_CONFINED_WORK_PERMIT deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;
                return PartialView("_ViewHotWork", deserialized!.Get_ById);
            }
        }

        [HttpPost]
        public async Task<JsonResult> _EditHotWork(string ID)
        {
            using (client)
            {
                Ptw_Confined_Space_Add _UNIT = new Ptw_Confined_Space_Add
                {
                    CSP_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Edit_Hot_Work", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_CONFINED_WORK_PERMIT deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Zone_HSE_HW_Approve([FromBody] Ptw_History_of_Approval model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Add_Zone_HSE_HW_Approve";
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Sp_HW_Additional_Details([FromBody] Ptw_Sp_Add_Evidence_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Add_Sp_HW_Additional_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit_Sp_HW_Additional_Details([FromBody] Ptw_Sp_Add_Evidence_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Edit_Sp_HW_Additional_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Sp_HW_Renewal_Details([FromBody] Ptw_Sp_Add_Renewal_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Add_Sp_HW_Renewal_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit_Sp_HW_Renewal_Details([FromBody] Ptw_Sp_Add_Renewal_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Edit_Sp_HW_Renewal_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        //Reject
        public async Task<IActionResult> Add_Zone_HW_Reject([FromBody] Ptw_History_of_Approval model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                URL = "ControlofWorkMaster/Add_Zone_HW_Reject";

                //model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        #endregion

        #region [Work At Height Permit]
        public async Task<IActionResult> Get_All_Work_Height_Question()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("ControlofWorkMaster/Get_All_Work_Height_Question").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_PTW_Questionnaries_Details deserialized = JsonConvert.DeserializeObject<M_Get_PTW_Questionnaries_Details>(customerJsonString)!;
                return Json(deserialized!.Get_All_Questionnaire);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Get_All_Work_Height([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            GET_CONFINED_WORK_PERMIT deserialized = new GET_CONFINED_WORK_PERMIT();
            Login_ LoginClass = GetLoginDetails();
            HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Get_All_Work_Height", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;

            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<Ptw_Confined_Space_Add>();
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
        public async Task<IActionResult> Add_Work_Height_Req([FromBody] Ptw_Confined_Space_Add model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();

                string URL = "";
                URL = "ControlofWorkMaster/Add_Work_Height_Req";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        public async Task<IActionResult> _ViewWorkHeight(int CSP_Id)
        {
            using (client)
            {
                Ptw_Confined_Space_Add _UNIT = new Ptw_Confined_Space_Add
                {
                    CSP_Id = Convert.ToString(CSP_Id),
                };
                HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Edit_Work_Height", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_CONFINED_WORK_PERMIT deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;
                return PartialView("_ViewWorkHeight", deserialized!.Get_ById);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Edit_Work_Height(string ID)
        {
            using (client)
            {
                Ptw_Confined_Space_Add _UNIT = new Ptw_Confined_Space_Add
                {
                    CSP_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Edit_Work_Height", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_CONFINED_WORK_PERMIT deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Zone_HSE_WAH_Approve([FromBody] Ptw_History_of_Approval model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Add_Zone_HSE_WAH_Approve";
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Sp_WAH_Additional_Details([FromBody] Ptw_Sp_Add_Evidence_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Add_Sp_WAH_Additional_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit_Sp_WAH_Additional_Details([FromBody] Ptw_Sp_Add_Evidence_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Edit_Sp_WAH_Additional_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Sp_WAH_Renewal_Details([FromBody] Ptw_Sp_Add_Renewal_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Add_Sp_WAH_Renewal_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit_Sp_WAH_Renewal_Details([FromBody] Ptw_Sp_Add_Renewal_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Edit_Sp_WAH_Renewal_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        //Reject
        public async Task<IActionResult> Add_Zone_WAH_Reject([FromBody] Ptw_History_of_Approval model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                URL = "ControlofWorkMaster/Add_Zone_WAH_Reject";

                //model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        #endregion

        #region [Fire & Safety Work Permit]
        [HttpPost]
        public async Task<IActionResult> Get_All_Fire_Safety_Request([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            GET_CONFINED_WORK_PERMIT deserialized = new GET_CONFINED_WORK_PERMIT();
            Login_ LoginClass = GetLoginDetails();
            HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Get_All_Fire_Safety_Req", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;

            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<Ptw_Confined_Space_Add>();
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
        public async Task<IActionResult> Add_Fire_Safety_Req([FromBody] Ptw_Confined_Space_Add model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();

                string URL = "";
                URL = "ControlofWorkMaster/Add_Fire_Safety_Req";
                //model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        public async Task<IActionResult> _ViewFireSafety(int CSP_Id)
        {
            using (client)
            {
                Ptw_Confined_Space_Add _UNIT = new Ptw_Confined_Space_Add
                {
                    CSP_Id = Convert.ToString(CSP_Id),
                };
                HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Edit_Fire_Safety", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_CONFINED_WORK_PERMIT deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;
                return PartialView("_ViewFireSafety", deserialized!.Get_ById);
            }
        }

        [HttpPost]
        public async Task<JsonResult> _Edit_Fire_Safety(string ID)
        {
            using (client)
            {
                Ptw_Confined_Space_Add _UNIT = new Ptw_Confined_Space_Add
                {
                    CSP_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("ControlofWorkMaster/Edit_Fire_Safety", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_CONFINED_WORK_PERMIT deserialized = JsonConvert.DeserializeObject<GET_CONFINED_WORK_PERMIT>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Sp_FS_Additional_Details([FromBody] Ptw_Sp_Add_Evidence_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Add_Sp_FS_Additional_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit_Sp_FS_Additional_Details([FromBody] Ptw_Sp_Add_Evidence_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Edit_Sp_FS_Additional_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Sp_FS_Renewal_Details([FromBody] Ptw_Sp_Add_Renewal_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Add_Sp_FS_Renewal_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit_Sp_FS_Renewal_Details([FromBody] Ptw_Sp_Add_Renewal_Details model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Edit_Sp_FS_Renewal_Details";
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        //Approve and Reject
        [HttpPost]
        public async Task<IActionResult> Add_Zone_HSE_FS_Approve([FromBody] Ptw_History_of_Approval model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                URL = "ControlofWorkMaster/Add_Zone_HSE_FS_Approve";
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> Add_Zone_FS_Reject([FromBody] Ptw_History_of_Approval model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                URL = "ControlofWorkMaster/Add_Zone_FS_Reject";

                //model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        #endregion
    }
}
