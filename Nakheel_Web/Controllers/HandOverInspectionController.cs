using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.HandOverInsMaster;
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
    public class HandOverInspectionController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;

        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private string conn;
        [Obsolete]
        public HandOverInspectionController(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            client = httpClientFactory.CreateClient("API");
            _hostingEnvironment = hostingEnvironment;
            conn = configuration.GetConnectionString("DefaultConnection");
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
        #region [Upload Files]
        public async Task<JsonResult> UploadImage(List<IFormFile> files)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = client.BaseAddress + "HandOverInspection/UploadImage";

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

        public async Task<JsonResult> Upload_Picture(List<IFormFile> files)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = client.BaseAddress + "HandOverInspection/Upload_Picture";

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

        #endregion
        public IActionResult Index()
        {
            return View();
        }
        #region [Handover Inspection]
        public IActionResult HandOver_Building()
        {
           return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetAll_HandOver_Building([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            GET_HANDOVER_INSPECTION deserialized = new GET_HANDOVER_INSPECTION();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("HandOverInspection/Insp_HandOver_Building_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<GET_HANDOVER_INSPECTION>(customerJsonString)!;
            
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_HandOver_Insp_Model>();
            }
            else
            {
                foreach (var item in deserialized.Get_All!)
                {
                    item.Role_Id = LoginClass.Role_Id;
                    item.HandOver_Type = item.HandOver_Type;
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
        public async Task<IActionResult> AddHandOverBuilding([FromBody] M_HandOver_Insp_Model model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";
                if (model.Insp_HndOver_Building_Id == "0")
                {
                    URL = "HandOverInspection/Insp_HandOver_Building_Qn_Add";
                }
                else
                {
                    URL = "HandOverInspection/Insp_HandOver_Building_Qn_Add";
                }
                model.CreatedBy= LoginClass.Employee_Identity_Id;
                model.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> _ViewHandOverBuilding(int Insp_HndOver_Building_Id)
        {
            using (client)
            {
                M_HandOver_Insp_Model _UNIT = new M_HandOver_Insp_Model
                {
                    Insp_HndOver_Building_Id = Convert.ToString(Insp_HndOver_Building_Id),
                };

                HttpResponseMessage response = client.PostAsync("HandOverInspection/Insp_HandOver_Building_GetByID", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HANDOVER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HANDOVER_INSPECTION>(customerJsonString)!;
                return PartialView("_ViewHandOverBuilding", deserialized!.Get_Insp_HandOver_Building);
            }
        }
        public async Task<IActionResult> _ViewBuildingQuestionnairs(int Insp_HndOver_Building_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_HandOver_Insp_Model _UNIT = new M_HandOver_Insp_Model
                {
                    Insp_HndOver_Building_Id = Convert.ToString(Insp_HndOver_Building_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Insp_HandOver_Building_Qn_GetAll", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HANDOVER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HANDOVER_INSPECTION>(customerJsonString)!;
                return PartialView(deserialized!.Get_Insp_HandOver_Bldg_View);
            }
        }
        public async Task<IActionResult> _ViewHndOverBldgQuestionnairs(int Insp_HndOver_Building_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_HandOver_Insp_Model _UNIT = new M_HandOver_Insp_Model
                {
                    Insp_HndOver_Building_Id = Convert.ToString(Insp_HndOver_Building_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Get_HndOver_Bldg_Qns_View", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HANDOVER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HANDOVER_INSPECTION>(customerJsonString)!;
                return PartialView(deserialized!.Get_Insp_HandOver_Building);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update_HndOver_Building_Pending([FromBody] M_HandOver_Insp_Model _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Update_HndOver_Building_Pending", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;

                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update_HndOver_Bldg_Qns_Approval([FromBody] M_HandOver_Insp_Model _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Update_HndOver_Bldg_Qns_Approval", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;

                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Handover_Insp_Reject(int Insp_HndOver_Building_Id, string Reject_Stage, string Reject_Reason)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_HndOver_Assign_Action _UNIT = new M_Insp_HndOver_Assign_Action
                {
                    Insp_HndOver_Building_Id = Convert.ToString(Insp_HndOver_Building_Id),
                    Reject_Stage = Reject_Stage,
                    Reject_Reason = Reject_Reason,
                    CreatedBy = LoginClass.Employee_Identity_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/HndOver_Insp_Reject", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add_Bldg_Assign_Action([FromBody] M_Insp_HndOver_Assign_Action _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/HndOver_Building_Assign_Action", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HANDOVER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HANDOVER_INSPECTION>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> Add_Bldg_Closure_Action([FromBody] M_Insp_HndOver_Assign_Action _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                //_UNIT.Inc_Obser_Report_Id = _UNIT.Inc_Obser_Report_Id;
                List<M_Insp_HandOver_Photos> pHOTOs = new List<M_Insp_HandOver_Photos>();
                var obser = HttpContext.Session.GetString("Photo");
                if (obser != null)
                {
                    string[] photo = JsonConvert.DeserializeObject<string[]>(obser)!;
                    if (photo != null)
                    {
                        for (int i = 0; i < photo.Length; i++)
                        {
                            M_Insp_HandOver_Photos pHOTO = new M_Insp_HandOver_Photos
                            {
                                Photo_File_Path = photo[i],
                                //CreatedBy = LoginClass.Employee_Identity_Id
                            };
                            pHOTOs.Add(pHOTO);
                        }
                    }
                }
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                //_UNIT.Role_Id = LoginClass.Role_Id;
                _UNIT.L_Insp_HndOver_Photos = pHOTOs;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Add_HndOver_Bldg_Closure_Action", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HANDOVER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HANDOVER_INSPECTION>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public IActionResult HandOver_Infrastructure()
        {
            return View();
        }
        public async Task<IActionResult> _ViewHandOverInfrastructureQns(int Insp_HndOver_Building_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_HandOver_Insp_Model _UNIT = new M_HandOver_Insp_Model
                {
                    Insp_HndOver_Building_Id = Convert.ToString(Insp_HndOver_Building_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Insp_HandOver_Building_Qn_GetAll", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HANDOVER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HANDOVER_INSPECTION>(customerJsonString)!;
                return PartialView(deserialized!.Get_Insp_HandOver_Building);
            }
        }
        public async Task<IActionResult> HandOver_Action_Closure()
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_HndOver_Assign_Action _UNIT = new()
                {
                    Responsible_Id = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };

                HttpResponseMessage response = client.PostAsync("HandOverInspection/HandOver_Action_Closure_GetAll", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HANDOVER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HANDOVER_INSPECTION>(customerJsonString)!;
                return View(deserialized!.GetAll_HandOver_CA);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetAll_HandOver_Corrective_Action([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            GET_HANDOVER_INSPECTION deserialized = new GET_HANDOVER_INSPECTION();
            Login_ LoginClass = GetLoginDetails();
            //model.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("HandOverInspection/Insp_HandOver_Infrastructure_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<GET_HANDOVER_INSPECTION>(customerJsonString)!;
            foreach (var item in deserialized.Get_All!)
            {
                item.Role_Id = LoginClass.Role_Id;
            }
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_HandOver_Insp_Model>();
            }

            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList()
            });
        }
        public async Task<IActionResult> _ViewHandOver_Closure(int Insp_HndOver_Building_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_HndOver_Assign_Action _UNIT = new M_Insp_HndOver_Assign_Action
                {
                    Insp_HndOver_Building_Id = Convert.ToString(Insp_HndOver_Building_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/HandOver_Closure_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HANDOVER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HANDOVER_INSPECTION>(customerJsonString)!;
                return PartialView(deserialized!.Get_Insp_HandOver_Building);
            }
        }
        public async Task<IActionResult> AddHandOver_Closure_Action([FromBody] M_Insp_HndOver_Assign_Action _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Add_HndOver_Bldg_Closure_Action", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HANDOVER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HANDOVER_INSPECTION>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> HandOver_Closure_Action_Approval(string Insp_HndOver_Building_Id, int Corrective_Action_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_HndOver_Assign_Action _UNIT = new M_Insp_HndOver_Assign_Action
                {
                    Insp_HndOver_Building_Id = Insp_HndOver_Building_Id,
                    Corrective_Action_Id = Convert.ToString(Corrective_Action_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/HandOver_Closure_Action_Approval", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;

                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> HandOver_Closure_Action_Reject(int Corrective_Action_Id, string Insp_HndOver_Building_Id, string Reject_Stage, string Reject_Reason)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_HndOver_Assign_Action _UNIT = new M_Insp_HndOver_Assign_Action
                {
                    Corrective_Action_Id = Convert.ToString(Corrective_Action_Id),
                    Insp_HndOver_Building_Id = Insp_HndOver_Building_Id,
                    Reject_Stage = Reject_Stage,
                    Reject_Reason = Reject_Reason,
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/HandOver_Closure_Action_Reject", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        #endregion
        #region [Health & Safety Inspection]
        public IActionResult Insp_HealthSafety_Building()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetAll_HealthSafety_Inspection([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            GET_HEALTHSAFETY_INSPECTION deserialized = new GET_HEALTHSAFETY_INSPECTION();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("HandOverInspection/Insp_HealthSafety_Building_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<GET_HEALTHSAFETY_INSPECTION>(customerJsonString)!;

            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Insp_HealthSafety_Model>();
            }
            else
            {
                foreach (var item in deserialized.Get_All!)
                {
                    item.Role_Id = LoginClass.Role_Id;
                    item.Health_Safety_Type = item.Health_Safety_Type;   
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
        public async Task<IActionResult> AddHealthSafetyInspection([FromBody] M_Insp_HealthSafety_Model model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                if (model.Insp_HealthSafety_Id == "0")
                {
                    URL = "HandOverInspection/Insp_HealthSafety_Building_Add";
                }
                else
                {
                    URL = "HandOverInspection/Insp_HealthSafety_Building_Update";
                }
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                model.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                return Json(deserialized);
            }
        }
        public async Task<IActionResult> _ViewHealthSafetyBuilding(int Insp_HealthSafety_Id)
        {
            using (client)
            {
                M_Insp_HealthSafety_Model _UNIT = new M_Insp_HealthSafety_Model
                {
                    Insp_HealthSafety_Id = Convert.ToString(Insp_HealthSafety_Id),
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Insp_HealthSafety_Building_GetByID", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HEALTHSAFETY_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HEALTHSAFETY_INSPECTION>(customerJsonString)!;
                return PartialView("_ViewHealthSafetyBuilding", deserialized!.Get_Insp_HealthSafety_Building);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update_HealthSafety_Building_Pending([FromBody] M_Insp_HealthSafety_Model _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                _UNIT.Module_Name = "Health & Safety Building";
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Update_HealthSafety_Building_Pending", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;

                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> _ViewHealthSafetyQuestionnairs(string Health_Safety_Type)  
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_HealthSafety_Model _UNIT = new M_Insp_HealthSafety_Model
                {
                    Health_Safety_Type = Health_Safety_Type,
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Insp_HealthSafety_CheckList_Qn_GetAll", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HEALTHSAFETY_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HEALTHSAFETY_INSPECTION>(customerJsonString)!;
                return PartialView(deserialized!.Get_Insp_HealthSafety_Bldg_View);
            }
        }
        
        public async Task<IActionResult> _ViewHealthSafetyMCQuestionnairs(int Insp_HealthSafety_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_HealthSafety_Model _UNIT = new M_Insp_HealthSafety_Model
                {
                    Insp_HealthSafety_Id = Convert.ToString(Insp_HealthSafety_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Insp_HealthSafety_Qns_GetAll", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HEALTHSAFETY_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HEALTHSAFETY_INSPECTION>(customerJsonString)!;
                return PartialView(deserialized!.Get_Insp_HealthSafety_Building);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_HealthSafety_Qn_Add([FromBody] M_Insp_HealthSafety_Model model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                if (model.Insp_HealthSafety_Id != "0")
                {
                    URL = "HandOverInspection/Insp_HealthSafety_Qn_Add";
                }
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                model.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                return Json(deserialized);
            }
        }
        public async Task<IActionResult> _GetHealthSafetyQnsView(int Insp_HealthSafety_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_HealthSafety_Model _UNIT = new M_Insp_HealthSafety_Model
                {
                    Insp_HealthSafety_Id = Convert.ToString(Insp_HealthSafety_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Get_HealthSafety_Bldg_Qns_View", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HEALTHSAFETY_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HEALTHSAFETY_INSPECTION>(customerJsonString)!;
                return PartialView(deserialized!.Get_Insp_HealthSafety_Building);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update_HealthSafety_Bldg_Qns_Approval([FromBody] M_Insp_HealthSafety_Model _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Update_HealthSafety_Bldg_Qns_Approval", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> HealthSafety_Insp_Reject(int Insp_HealthSafety_Id, string Reject_Reason_Stage, string Reject_Reason_Description)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_HealthSafety_Assign_Team _UNIT = new M_Insp_HealthSafety_Assign_Team
                {
                    Insp_HealthSafety_Id = Convert.ToString(Insp_HealthSafety_Id),
                    Reject_Stage = Reject_Reason_Stage,
                    Reject_Reason_Description = Reject_Reason_Description,
                    CreatedBy = LoginClass.Employee_Identity_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/HealthSafety_Insp_Reject", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add_HealthSafety_Assign_Team([FromBody] M_Insp_HealthSafety_Assign_Team _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Add_HealthSafety_Assign_Team", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HEALTHSAFETY_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HEALTHSAFETY_INSPECTION>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        
        public async Task<IActionResult> HealthSafety_Closure_Action_Add([FromBody] M_Insp_HealthSafety_Assign_Team _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/HealthSafety_Closure_Action_Add", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HANDOVER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HANDOVER_INSPECTION>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> HealthSafety_Action_Closure()
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_HealthSafety_Assign_Team _UNIT = new()
                {
                    Responsible_Id = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/HealthSafety_Action_Closure_GetAll", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HEALTHSAFETY_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HEALTHSAFETY_INSPECTION>(customerJsonString)!;
                return View(deserialized!.GetAll_HealthSafety_CA);
            }
        }
        public async Task<IActionResult> _ViewHealthSafetyClosureAction(int Insp_HealthSafety_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_HealthSafety_Assign_Team _UNIT = new M_Insp_HealthSafety_Assign_Team
                {
                    Insp_HealthSafety_Id = Convert.ToString(Insp_HealthSafety_Id),
                    //CreatedBy = LoginClass.Employee_Identity_Id,
                    //Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/HealthSafety_Closure_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HEALTHSAFETY_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HEALTHSAFETY_INSPECTION>(customerJsonString)!;
                return PartialView(deserialized!.Get_Insp_HealthSafety_Building);
            }
        }
        [HttpPost]
        public async Task<IActionResult> HealthSafety_Closure_Action_Approval(string Insp_HealthSafety_Id, int Assign_Action_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_HealthSafety_Assign_Team _UNIT = new M_Insp_HealthSafety_Assign_Team
                {
                    Insp_HealthSafety_Id = Insp_HealthSafety_Id,
                    Assign_Action_Id = Convert.ToString(Assign_Action_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/HealthSafety_Closure_Action_Approval", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> HealthSafety_Closure_Action_Reject(int Assign_Action_Id, string Reject_Reason_Stage, string Reject_Reason_Description)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_HealthSafety_Assign_Team _UNIT = new M_Insp_HealthSafety_Assign_Team
                {
                    Assign_Action_Id = Convert.ToString(Assign_Action_Id),
                    Reject_Stage = Reject_Reason_Stage,
                    Reject_Reason_Description = Reject_Reason_Description,
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/HealthSafety_Closure_Action_Reject", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        #endregion
        #region [Service Provider Inspection]
        public IActionResult Insp_Service_Provider()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetAll_ServiceProvider_Inspection([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            GET_SERVICEPROVIDER_INSPECTION deserialized = new GET_SERVICEPROVIDER_INSPECTION();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;
            HttpResponseMessage response = client.PostAsync("HandOverInspection/Insp_ServiceProvider_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<GET_SERVICEPROVIDER_INSPECTION>(customerJsonString)!;

            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Insp_ServiceProvider_Model>();
            }
            else
            {
                foreach (var item in deserialized.Get_All!)
                {
                    item.Role_Id = LoginClass.Role_Id;
                    item.Service_Provider_Type = item.Service_Provider_Type;
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
        public async Task<IActionResult> AddServiceProviderInspection([FromBody] M_Insp_ServiceProvider_Model model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                if (model.Insp_ServiceProvider_Id == "0")
                {
                    URL = "HandOverInspection/Insp_Service_Provider_Add";
                }
                else
                {
                    URL = "HandOverInspection/Insp_Service_Provider_Update";
                }
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                model.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> _ViewServiceProviderSchedule(int Insp_ServiceProvider_Id)
        {
            using (client)
            {
                M_Insp_ServiceProvider_Model _UNIT = new M_Insp_ServiceProvider_Model
                {
                    Insp_ServiceProvider_Id = Convert.ToString(Insp_ServiceProvider_Id),
                };

                HttpResponseMessage response = client.PostAsync("HandOverInspection/Insp_ServiceProvider_GetByID", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SERVICEPROVIDER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_SERVICEPROVIDER_INSPECTION>(customerJsonString)!;
                return PartialView("_ViewServiceProviderSchedule", deserialized!.Get_Insp_Service_Provider);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update_ServiceProvider_Schedule([FromBody] M_Insp_ServiceProvider_Model _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Update_ServiceProvider_Schedule", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;

                return Json(deserialized!.STATUS_CODE);
            }
        }
        //public async Task<IActionResult> _ViewServiceProviderQuestionnairs()
        public async Task<IActionResult> _ViewServiceProviderQuestionnairs(int Insp_ServiceProvider_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_ServiceProvider_Model _UNIT = new M_Insp_ServiceProvider_Model
                {
                    Insp_ServiceProvider_Id = Convert.ToString(Insp_ServiceProvider_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Insp_ServiceProvider_Qn_GetAll", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SERVICEPROVIDER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_SERVICEPROVIDER_INSPECTION>(customerJsonString)!;
                return PartialView(deserialized!.Get_Insp_Service_Provider);
            }
        }
        public async Task<IActionResult> _ViewServiceProviderCSQnsObs(int Insp_ServiceProvider_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_ServiceProvider_Model _UNIT = new M_Insp_ServiceProvider_Model
                {
                    Insp_ServiceProvider_Id = Convert.ToString(Insp_ServiceProvider_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Insp_ServiceProvider_Qn_GetAll", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SERVICEPROVIDER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_SERVICEPROVIDER_INSPECTION>(customerJsonString)!;
                return PartialView(deserialized!.Get_Insp_Service_Provider);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_ServiceProvider_Qn_Add([FromBody] M_Insp_ServiceProvider_Model model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                if (model.Insp_ServiceProvider_Id != "0")
                {
                    URL = "HandOverInspection/Insp_ServiceProvider_Qn_Add";
                }
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                model.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                return Json(deserialized);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_ServiceProvider_Qn_HSSETeam([FromBody] M_Insp_ServiceProvider_Model model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                if (model.Insp_ServiceProvider_Id != "0")
                {
                    URL = "HandOverInspection/Insp_ServiceProvider_Qn_HSSETeam";
                }
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                model.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> _GetServiceProviderQnsView(int Insp_ServiceProvider_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_ServiceProvider_Model _UNIT = new M_Insp_ServiceProvider_Model
                {
                    Insp_ServiceProvider_Id = Convert.ToString(Insp_ServiceProvider_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Get_ServiceProvider_Qns_View", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SERVICEPROVIDER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_SERVICEPROVIDER_INSPECTION>(customerJsonString)!;
                return PartialView(deserialized!.Get_Insp_Service_Provider);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update_ServiceProvider_Qns_Approval([FromBody] M_Insp_Safety_Violation _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Emp_Service_provider = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Update_ServiceProvider_Qns_Approval", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ServiceProvider_Insp_Qns_Reject([FromBody] M_Insp_ServiceProvider_Assign_Team _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/ServiceProvider_Insp_Qns_Reject", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add_ServiceProvider_Assign_Team([FromBody] M_Insp_ServiceProvider_Assign_Team _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Add_ServiceProvider_Assign_Team", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SERVICEPROVIDER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_SERVICEPROVIDER_INSPECTION>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        //public async Task<IActionResult> Add_Closure_Action([FromBody] M_Insp_HndOver_Assign_Action _UNIT)
        //{
        //    using (client)
        //    {
        //        Login_ LoginClass = GetLoginDetails();
        //        //_UNIT.Inc_Obser_Report_Id = _UNIT.Inc_Obser_Report_Id;
        //        List<M_Insp_HandOver_Photos> pHOTOs = new List<M_Insp_HandOver_Photos>();
        //        var obser = HttpContext.Session.GetString("Photo");
        //        if (obser != null)
        //        {
        //            string[] photo = JsonConvert.DeserializeObject<string[]>(obser)!;
        //            if (photo != null)
        //            {
        //                for (int i = 0; i < photo.Length; i++)
        //                {
        //                    M_Insp_HandOver_Photos pHOTO = new M_Insp_HandOver_Photos
        //                    {
        //                        Photo_File_Path = photo[i],
        //                        //CreatedBy = LoginClass.Employee_Identity_Id
        //                    };
        //                    pHOTOs.Add(pHOTO);
        //                }
        //            }
        //        }
        //        _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
        //        //_UNIT.Role_Id = LoginClass.Role_Id;
        //        _UNIT.L_Insp_HndOver_Photos = pHOTOs;
        //        HttpResponseMessage response = client.PostAsync("HandOverInspection/Add_HndOver_Bldg_Closure_Action", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
        //        string customerJsonString = await response.Content.ReadAsStringAsync();
        //        GET_HANDOVER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_HANDOVER_INSPECTION>(customerJsonString)!;
        //        return Json(deserialized!.STATUS_CODE);
        //    }
        //}
        public async Task<IActionResult> ServiceProvider_Closure_Action_Add([FromBody] M_Insp_ServiceProvider_Assign_Team _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/ServiceProvider_Closure_Action_Add", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SERVICEPROVIDER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_SERVICEPROVIDER_INSPECTION>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> ServiceProvider_Action_Closure()
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_ServiceProvider_Assign_Team _UNIT = new()
                {
                    Responsible_Id = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/ServiceProvider_Action_Closure_GetAll", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SERVICEPROVIDER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_SERVICEPROVIDER_INSPECTION>(customerJsonString)!;
                //foreach (var item in deserialized.Get_All!)
                //{
                //    item.Role_Id = LoginClass.Role_Id;
                //}
                return View(deserialized!.GetAll_ServiceProvider_CA);
            }
        }
        public async Task<IActionResult> _ViewServiceProviderClosureAction(int Insp_ServiceProvider_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_ServiceProvider_Assign_Team _UNIT = new M_Insp_ServiceProvider_Assign_Team
                {
                    Insp_ServiceProvider_Id = Convert.ToString(Insp_ServiceProvider_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/ServiceProvider_Closure_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SERVICEPROVIDER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_SERVICEPROVIDER_INSPECTION>(customerJsonString)!;
                return PartialView(deserialized!.Get_Insp_Service_Provider);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ServiceProvider_Closure_Action_Approval(string Insp_ServiceProvider_Id,int Corrective_Action_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_ServiceProvider_Assign_Team _UNIT = new M_Insp_ServiceProvider_Assign_Team
                {
                    Insp_ServiceProvider_Id = Insp_ServiceProvider_Id,
                    Corrective_Action_Id = Convert.ToString(Corrective_Action_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/ServiceProvider_Closure_Action_Approval", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ServiceProvider_Closure_Action_Reject([FromBody] M_Insp_ServiceProvider_Assign_Team _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/ServiceProvider_Closure_Action_Reject", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized);
            }
        }
        #endregion
        #region [Fire & Life Safety Inspection]
        public IActionResult Insp_FireLife_Safety()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetAll_FireLifeSafety_Inspection([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            GET_FIRELIFESAFETY_INSPECTION deserialized = new GET_FIRELIFESAFETY_INSPECTION();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("HandOverInspection/Insp_FireLifeSafety_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<GET_FIRELIFESAFETY_INSPECTION>(customerJsonString)!;

            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Insp_FireLifeSafety_Model>();
            }
            else
            {
                foreach (var item in deserialized.Get_All!)
                {
                    item.Role_Id = LoginClass.Role_Id;
                    item.Service_Provider_Id = item.Service_Provider_Id;
                    item.CreatedBy = LoginClass.Employee_Identity_Id;
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
        public async Task<IActionResult> AddFireLifeSafetyInspection([FromBody] M_Insp_FireLifeSafety_Model model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                if (model.Insp_Request_Id == "0")
                {
                    URL = "HandOverInspection/Insp_FireLifeSafety_Add";
                }
                else
                {
                    URL = "HandOverInspection/Insp_FireLifeSafety_Update";
                }
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                model.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> _ViewFireLifeSafetySchedule(int Insp_Request_Id)
        {
            using (client)
            {
                M_Insp_FireLifeSafety_Model _UNIT = new M_Insp_FireLifeSafety_Model
                {
                    Insp_Request_Id = Convert.ToString(Insp_Request_Id),
                };

                HttpResponseMessage response = client.PostAsync("HandOverInspection/Insp_FireLifeSafety_GetByID", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_FIRELIFESAFETY_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_FIRELIFESAFETY_INSPECTION>(customerJsonString)!;
                return PartialView("_ViewFireLifeSafetySchedule", deserialized!.Get_Insp_FireLifeSafety);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update_FireLifeSafety_Schedule([FromBody] M_Insp_FireLifeSafety_Model _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Update_FireLifeSafety_Schedule", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        
        public async Task<IActionResult> _ViewFireLifeSafetyQuestionnairs()
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_FireLifeSafety_Model _UNIT = new M_Insp_FireLifeSafety_Model
                {
                    //Insp_Request_Id = Convert.ToString(Insp_Request_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Insp_FireLifeSafety_Qns_GetAll", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_FIRELIFESAFETY_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_FIRELIFESAFETY_INSPECTION>(customerJsonString)!;
                return PartialView(deserialized!.Get_Insp_FireLifeSafety_Qns);
            }
        }
        public async Task<IActionResult> _ViewFireLifeSafetyCSQuestionnairs(int Insp_Request_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_FireLifeSafety_Model _UNIT = new M_Insp_FireLifeSafety_Model
                {
                    Insp_Request_Id = Convert.ToString(Insp_Request_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Insp_FireLifeSafetyCS_Qns_GetAll", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_FIRELIFESAFETY_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_FIRELIFESAFETY_INSPECTION>(customerJsonString)!;
                return PartialView(deserialized!.Get_Insp_FireLifeSafety);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_FireLifeSafety_QnObs_Add([FromBody] M_Insp_FireLifeSafety_Model model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                if (model.Insp_Request_Id != "0")
                {
                    URL = "HandOverInspection/Insp_FireLifeSafety_QnObs_Add";
                }
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                model.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                return Json(deserialized!);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_FireLifeSafety_Qn_HSSETeam([FromBody] M_Insp_FireLifeSafety_Model model)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                string URL = "";

                if (model.Insp_Request_Id != "0")
                {
                    URL = "HandOverInspection/Insp_FireLifeSafety_Qn_HSSETeam";
                }
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> _GetFireLifeSafetyQnsView(int Insp_Request_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_FireLifeSafety_Model _UNIT = new M_Insp_FireLifeSafety_Model
                {
                    Insp_Request_Id = Convert.ToString(Insp_Request_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Get_FireLifeSafety_Qns_View", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_FIRELIFESAFETY_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_FIRELIFESAFETY_INSPECTION>(customerJsonString)!;
                return PartialView(deserialized!.Get_Insp_FireLifeSafety);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update_FireLifeSafety_Qns_Approval([FromBody] M_Insp_FireLifeSafety_Model _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Update_FireLifeSafety_Qns_Approval", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;

                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add_FireLifeSafety_Assign_Team([FromBody] M_Insp_FireLifeSafety_Assign_Action _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/Add_FireLifeSafety_Assign_Team", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_FIRELIFESAFETY_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_FIRELIFESAFETY_INSPECTION>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> FireLifeSafety_Closure_Action_Add([FromBody] M_Insp_FireLifeSafety_Assign_Action _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync("HandOverInspection/FireLifeSafety_Closure_Action_Add", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SERVICEPROVIDER_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_SERVICEPROVIDER_INSPECTION>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> FireLifeSafety_Action_Closure()
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_FireLifeSafety_Assign_Action _UNIT = new()
                {
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/FireLifeSafety_Action_Closure_GetAll", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_FIRELIFESAFETY_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_FIRELIFESAFETY_INSPECTION>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> _ViewFireLifeSafetyClosureAction(int Insp_Request_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_FireLifeSafety_Assign_Action _UNIT = new M_Insp_FireLifeSafety_Assign_Action
                {
                    Insp_Request_Id = Convert.ToString(Insp_Request_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/FireLifeSafety_Closure_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_FIRELIFESAFETY_INSPECTION deserialized = JsonConvert.DeserializeObject<GET_FIRELIFESAFETY_INSPECTION>(customerJsonString)!;
                return PartialView(deserialized!.Get_Insp_FireLifeSafety);
            }
        }
        [HttpPost]
        public async Task<IActionResult> FireLifeSafety_Closure_Action_Approval(string Insp_Request_Id, int Corrective_Action_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_FireLifeSafety_Assign_Action _UNIT = new M_Insp_FireLifeSafety_Assign_Action
                {
                    Insp_Request_Id = Insp_Request_Id,
                    Corrective_Action_Id = Convert.ToString(Corrective_Action_Id),
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/FireLifeSafety_Closure_Action_Approval", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> FireLifeSafety_Closure_Action_Reject(string Insp_Request_Id, string Corrective_Action_Id, string Reject_Stage, string Reject_Reason)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                M_Insp_FireLifeSafety_Assign_Action _UNIT = new M_Insp_FireLifeSafety_Assign_Action
                {
                    Insp_Request_Id = Insp_Request_Id,
                    Corrective_Action_Id = Corrective_Action_Id,
                    Reject_Stage = Reject_Stage,
                    Reject_Reason = Reject_Reason,
                    CreatedBy = LoginClass.Employee_Identity_Id,
                    Role_Id = LoginClass.Role_Id
                };
                HttpResponseMessage response = client.PostAsync("HandOverInspection/FireLifeSafety_Closure_Action_Reject", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        #endregion
    }
}