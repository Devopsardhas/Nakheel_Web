using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.AuditMaster;
using Nakheel_Web.Models;
using Newtonsoft.Json;
using System.Text;
using static Nakheel_Web.Authentication.Common;
using Nakheel_Web.Models.EMR_Drill;
using Nakheel_Web.Models.Masters;
using System.Reflection;
using Nakheel_Web.Models.IncidentReport;
using Microsoft.AspNetCore.Authorization;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class DrillScheduleController : Controller
    {
        private readonly HttpClient client;

        public DrillScheduleController(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("API");
        }
        public IActionResult Index(string id)
        {
            HttpContext.Session.SetString("DrillSchedule_Card_Id", id);
            return View();
        }

        public async Task<IActionResult> Drill_Schedule_Get_All([FromBody] DataTableAjaxPostModel model)
        {
            using (client)
            {
                var str = HttpContext.Session.GetString("DrillSchedule_Card_Id");
                Drill_Schedule_Get deserialized = new Drill_Schedule_Get();
                Login_ LoginClass = GetLoginDetails();
                model.CreatedBy = LoginClass.Employee_Identity_Id;
                model.Card_Id = str;
                HttpResponseMessage response = client.PostAsync("DrillSchedule/Drill_SCH_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                deserialized = JsonConvert.DeserializeObject<Drill_Schedule_Get>(customerJsonString)!;
                if (deserialized.STATUS_CODE == "404")
                {
                    deserialized.Data = new List<Drill_Schedule>();
                }
                return Json(new
                {
                    draw = model.Draw,
                    recordsTotal = deserialized.RecordsTotal,
                    recordsFiltered = deserialized.RecordsFiltered,
                    data = deserialized.Data
                });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Drill_Sch_Assign(string Drill_Sch_ID)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                Drill_Schedule_Param drill_ = new Drill_Schedule_Param()
                {
                    Drill_Schedule_ID = Drill_Sch_ID,
                    Created_By = LoginClass.CreatedBy
                };
                HttpResponseMessage response = client.PostAsync("DrillSchedule/Drill_SCH_Get_Assignee", new StringContent(JsonConvert.SerializeObject(drill_), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Sch_Assignee deserialized = JsonConvert.DeserializeObject<Get_Sch_Assignee>(customerJsonString)!;
                return Json(deserialized);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Drill_Add_Assign(Drill_Schedule drill_)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                drill_.Created_By = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync("DrillSchedule/Drill_Schedule_Add", new StringContent(JsonConvert.SerializeObject(drill_), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized);
            }
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Drill_Update_Status(Drill_Schedule_Param drill_)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                drill_.Created_By = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync("DrillSchedule/Drill_Sch_Update_ST", new StringContent(JsonConvert.SerializeObject(drill_), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Drill_Add_Review(Drill_Action_Param drill_)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                drill_.Created_By = LoginClass.Employee_Identity_Id;
                if (drill_._Acts != null && drill_._Acts.Count > 0)
                {
                    foreach (var item in drill_._Acts)
                    {
                        item.Created_By = LoginClass.Employee_Identity_Id;
                    }
                }

                HttpResponseMessage response = client.PostAsync("DrillSchedule/Drill_Sch_Update_Action", new StringContent(JsonConvert.SerializeObject(drill_), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Drill_Add_Assignees(Drill_Action_Param drill_)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                drill_.Created_By = LoginClass.Employee_Identity_Id;
                if (drill_._Acts != null && drill_._Acts.Count > 0)
                {
                    foreach (var item in drill_._Acts)
                    {
                        item.Created_By = LoginClass.Employee_Identity_Id;
                    }
                }

                HttpResponseMessage response = client.PostAsync("DrillSchedule/Drill_Sch_Update_IMP_Action", new StringContent(JsonConvert.SerializeObject(drill_), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized);
            }
        }
        #region DrillForm
        public IActionResult Drill()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Schedule_Report(Drill_Schedule_Param _Param)
        {
            Login_ LoginClass = GetLoginDetails();
            _Param.Created_By = LoginClass.Employee_Identity_Id;
            HttpResponseMessage response = client.PostAsync("DrillSchedule/Drill_Schedule_Get", new StringContent(JsonConvert.SerializeObject(_Param), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            Get_Drill_Details deserialized = JsonConvert.DeserializeObject<Get_Drill_Details>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "200" && deserialized.Data != null)
            {
                string Drill_Type = deserialized.Data.Schedule!.Drill_Type_ID!;
                return PartialView(deserialized.Data);
               
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Schedule_Add_Fire(Drill_Fire _Fire)
        {

            using (client)
            {
                if (ModelState.IsValid)
                {
                    Login_ LoginClass = GetLoginDetails();
                    _Fire.Drill_Photos = new List<Drill_Photos>();
                    _Fire.Drill_Vedios = new List<Drill_Vedios>();
                    _Fire.Created_By = LoginClass.Employee_Identity_Id;
                    if (_Fire.Photos != null && _Fire.Photos.Count > 0)
                    {
                        string url = "DrillSchedule/UploadPhoto";
                        string[] files = await FileUpload.UploadMultipleFiles(_Fire.Photos, url, client);
                        if (files != null)
                        {
                            foreach (var item in files)
                            {
                                Drill_Photos pHOTO = new Drill_Photos
                                {
                                    Photo_File_Path = item,
                                    Created_By = LoginClass.Employee_Identity_Id
                                };

                                _Fire.Drill_Photos.Add(pHOTO);
                            }
                        }
                    }
                    if (_Fire.Videos != null && _Fire.Videos.Count > 0)
                    {
                        string url = "DrillSchedule/UploadVideo";
                        string[] files = await FileUpload.UploadMultipleFiles(_Fire.Videos, url, client);
                        if (files != null)
                        {
                            foreach (var item in files)
                            {
                                Drill_Vedios pHOTO = new Drill_Vedios
                                {
                                    Video_File_Path = item,
                                    Created_By = LoginClass.Employee_Identity_Id
                                };

                                _Fire.Drill_Vedios.Add(pHOTO);
                            }
                        }
                    }
                    if (_Fire._Acts != null && _Fire._Acts.Count > 0)
                    {
                        foreach (var item in _Fire._Acts)
                        {
                            item.Created_By = LoginClass.Employee_Identity_Id;
                        }
                    }
                    HttpResponseMessage response = client.PostAsync("DrillSchedule/Drill_SCH_Fire_Add", new StringContent(JsonConvert.SerializeObject(_Fire), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return RedirectToAction("Index");
                }
                else
                {
                    return NoContent();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Schedule_Add_Fire_Obsr(Drill_Fire_Obsr _Fire)
        {

            using (client)
            {
                if (ModelState.IsValid)
                {
                    Login_ LoginClass = GetLoginDetails();
                    _Fire.Drill_Photos = new List<Drill_Photos>();
                    _Fire.Drill_Vedios = new List<Drill_Vedios>();
                    _Fire.Created_By = LoginClass.Employee_Identity_Id;
                    if (_Fire.Photos != null && _Fire.Photos.Count > 0)
                    {
                        string url = "DrillSchedule/UploadPhoto";
                        string[] files = await FileUpload.UploadMultipleFiles(_Fire.Photos, url, client);
                        if (files != null)
                        {
                            foreach (var item in files)
                            {
                                Drill_Photos pHOTO = new Drill_Photos
                                {
                                    Photo_File_Path = item,
                                    Created_By = LoginClass.Employee_Identity_Id
                                };

                                _Fire.Drill_Photos.Add(pHOTO);
                            }
                        }
                    }
                    if (_Fire.Videos != null && _Fire.Videos.Count > 0)
                    {
                        string url = "DrillSchedule/UploadVideo";
                        string[] files = await FileUpload.UploadMultipleFiles(_Fire.Videos, url, client);
                        if (files != null)
                        {
                            foreach (var item in files)
                            {
                                Drill_Vedios pHOTO = new Drill_Vedios
                                {
                                    Video_File_Path = item,
                                    Created_By = LoginClass.Employee_Identity_Id
                                };

                                _Fire.Drill_Vedios.Add(pHOTO);
                            }
                        }
                    }
                    if (_Fire._Acts != null && _Fire._Acts.Count > 0)
                    {
                        foreach (var item in _Fire._Acts)
                        {
                            item.Created_By = LoginClass.Employee_Identity_Id;
                        }
                    }
                    HttpResponseMessage response = client.PostAsync("DrillSchedule/Drill_SCH_Fire_Obsr_Add", new StringContent(JsonConvert.SerializeObject(_Fire), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return RedirectToAction("Index");
                }
                else
                {
                    return NoContent();
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Schedule_Add_DrillFRM(Drill_CommonFRM CMM_Frm)
        {

            using (client)
            {
                if (ModelState.IsValid)
                {
                    Login_ LoginClass = GetLoginDetails();
                    CMM_Frm.Created_By = LoginClass.Employee_Identity_Id;
                    CMM_Frm.Drill_Vedios = new List<Drill_Vedios>();
                    CMM_Frm.Drill_Photos = new List<Drill_Photos>();
                    if (CMM_Frm.Photos != null && CMM_Frm.Photos.Count > 0)
                    {
                        string url = "DrillSchedule/UploadPhoto";
                        string[] files = await FileUpload.UploadMultipleFiles(CMM_Frm.Photos, url, client);
                        if (files != null)
                        {
                            foreach (var item in files)
                            {
                                Drill_Photos pHOTO = new Drill_Photos
                                {
                                    Photo_File_Path = item,
                                    Created_By = LoginClass.Employee_Identity_Id
                                };

                                CMM_Frm.Drill_Photos.Add(pHOTO);
                            }
                        }
                    }
                    if (CMM_Frm.Videos != null && CMM_Frm.Videos.Count > 0)
                    {
                        string url = "DrillSchedule/UploadVideo";
                        string[] files = await FileUpload.UploadMultipleFiles(CMM_Frm.Videos, url, client);
                        if (files != null)
                        {
                            foreach (var item in files)
                            {
                                Drill_Vedios ved = new Drill_Vedios
                                {
                                    Video_File_Path = item,
                                    Created_By = LoginClass.Employee_Identity_Id
                                };

                                CMM_Frm.Drill_Vedios.Add(ved);
                            }
                        }
                    }
                    if (CMM_Frm._Acts != null && CMM_Frm._Acts.Count > 0)
                    {
                        foreach (var item in CMM_Frm._Acts)
                        {
                            item.Created_By = LoginClass.Employee_Identity_Id;
                        }
                    }
                    HttpResponseMessage response = client.PostAsync("DrillSchedule/Drill_SCH_CMM_Frms_Add", new StringContent(JsonConvert.SerializeObject(CMM_Frm), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return RedirectToAction("Index");
                }
                else
                {
                    return NoContent();
                }
            }
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload_Evid_File(IFormFile file)
        {
            using (client)
            {
                if (file != null)
                {
                    List<IFormFile> formFiles = new List<IFormFile>();
                    formFiles.Add(file);
                    string url = "DrillSchedule/UploadPhoto";
                    string filesPath = await FileUpload.UploadSingleFile(formFiles, url, client);
                    if (filesPath != null)
                    {
                        return Json(filesPath);
                    }
                    else
                    {
                        return Json("Failed");
                    }

                }
                else { 
                return NoContent(); 
                }
              
            }
        }
        #endregion

        #region Reject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Drill_Add_REJ(Drill_REJ _REJ)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _REJ.Reject_By = LoginClass.Employee_Identity_Id;
                HttpResponseMessage response = client.PostAsync("DrillSchedule/Drill_SCH_REJ_Add", new StringContent(JsonConvert.SerializeObject(_REJ), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeletePhoto(string Key)
        {
            Login_ LoginClass = GetLoginDetails();
            Drill_Schedule_Param photo_List = new Drill_Schedule_Param()
            {
                Drill_Schedule_ID = Key,
                Created_By= LoginClass.Employee_Identity_Id
            };
            HttpResponseMessage response = client.PostAsync("DrillSchedule/Drill_Photo_Update", new StringContent(JsonConvert.SerializeObject(photo_List), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
            return Json(deserialized!);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteVideo(string ID)
        {
            Login_ LoginClass = GetLoginDetails();
            Drill_Schedule_Param photo_List = new Drill_Schedule_Param()
            {
                Drill_Schedule_ID = ID,
                Created_By = LoginClass.Employee_Identity_Id
            };
            HttpResponseMessage response = client.PostAsync("DrillSchedule/Drill_Video_Update", new StringContent(JsonConvert.SerializeObject(photo_List), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
            return Json(deserialized!);

        }
        #endregion
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
    }
}
