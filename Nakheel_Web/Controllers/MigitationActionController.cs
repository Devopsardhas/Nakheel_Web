using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models.Emergency;
using Nakheel_Web.Models;
using Newtonsoft.Json;
using System.Text;
using Nakheel_Web.Models.Masters;
using Nakheel_Web.Models.IncidentReport;
using Nakheel_Web.Models.SecurityIncidentReport;
using Microsoft.AspNetCore.Authorization;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class MigitationActionController : Controller
    {
        private readonly HttpClient client;
        public MigitationActionController(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("API");
        }

        public IActionResult MigitationAction()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Alert_Assignee_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            using (client)
            {
                TRIG_Assignee_Get deserialized = new TRIG_Assignee_Get();
                HttpResponseMessage response = client.PostAsync("TriggerAlert/Alert_Assignee_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                deserialized = JsonConvert.DeserializeObject<TRIG_Assignee_Get>(customerJsonString)!;
                if (deserialized.STATUS_CODE == "404")
                {
                    deserialized.Data = new List<TRIG_Assignee>();
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

        public async Task<IActionResult> _View_Mitigation_Action(int Alert_Task_Id)
        {
            using (client)
            {
                Trigger_Alert _UNIT = new Trigger_Alert
                {
                    Alert_Task_Id = Convert.ToString(Alert_Task_Id),

                };

                HttpResponseMessage response = client.PostAsync("TriggerAlert/TRG_Alert_Action_Get", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Edit_Trigger_Alert deserialized = JsonConvert.DeserializeObject<Edit_Trigger_Alert>(customerJsonString)!;

                return PartialView("_View_Mitigation_Action", deserialized!.Data);
            }
        }

        public async Task<IActionResult> _View_Trigger_Action(int Alert_Task_Id)
        {
            using (client)
            {
                Trigger_Alert _UNIT = new Trigger_Alert
                {
                    Alert_Task_Id = Convert.ToString(Alert_Task_Id),
                };

                HttpResponseMessage response = client.PostAsync("TriggerAlert/TRG_Alert_Action_Get", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Edit_Trigger_Alert deserialized = JsonConvert.DeserializeObject<Edit_Trigger_Alert>(customerJsonString)!;
                return Json(deserialized!.Data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Mitigation_Action(ChecklistMap model)
        {
            if (model.Chk_Map_Id == null || model.Chk_Map_Id == "0")
            {
                using (client)
                {
                    string URL = "";
                    URL = "TriggerAlert/TRG_Checklist_Add";
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    return Json(deserialized!.STATUS_CODE);
                }
            }
            else
            {
                using (client)
                {
                    string URL = "";
                    URL = "TriggerAlert/TRG_Checklist_Update";
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    return Json(deserialized!.STATUS_CODE);
                }
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update_Mitigation_Action(Trigger_Alert model)
        {           
           using (client)
           {
               string URL = "";
               URL = "TriggerAlert/TRG_Checklist_Rej_Update";
               HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
               string customerJsonString = await response.Content.ReadAsStringAsync();
               RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
               return Json(deserialized!.STATUS_CODE);
           }         
        }
        public async Task<JsonResult> SpotFileUpload(List<IFormFile> files)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = "TriggerAlert/UploadPhoto";
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

        public async Task<IActionResult> _Checklist_GetBy_SpotId(int Alert_Task_Id, int Hot_Spot_Id)
        {
            using (client)
            {
                ChecklistMap _UNIT = new ChecklistMap
                {
                    Alert_Task_Id = Convert.ToString(Alert_Task_Id),
                    Hot_Spot_Id = Convert.ToString(Hot_Spot_Id)
                };

                HttpResponseMessage response = client.PostAsync("TriggerAlert/TRG_CHK_GetBySpotID", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_ChecklistMap deserialized = JsonConvert.DeserializeObject<Get_ChecklistMap>(customerJsonString)!;
                return Json(deserialized!.Data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Approve_Action(Alert_Spot model)
        {
            using (client)
            {
                string URL = "";
                URL = "TriggerAlert/TRG_Checklist_Update_ST";
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Reject_Action(TRIG_Reject model)
        {
            using (client)
            {
                string URL = "";
                URL = "TriggerAlert/TRG_Checklist_Reject";
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Mitigation_Report([FromBody] Mitigation_Report model)
        {
            using (client)
            {
                string URL = "";

                List<Report_File> _Photos = new List<Report_File>();
                var Secure = HttpContext.Session.GetString("Photo");
                if (Secure != null)
                {
                    string[] photo = JsonConvert.DeserializeObject<string[]>(Secure)!;
                    if (photo != null)
                    {
                        for (int i = 0; i < photo.Length; i++)
                        {
                            Report_File pHotos = new Report_File
                            {
                                File_Path = photo[i]
                            };
                            _Photos.Add(pHotos);
                        }
                    }
                }
                if (_Photos.Count == 0)
                {
                    return Json("202");
                }
                else
                {
                    model._Files = _Photos;

                    URL = "MitigationReport/Alert_Report_Add";
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    return Json(deserialized!.STATUS_CODE);
                }
                
            }
        }

        #region [Rain Mitigation Report]
        public IActionResult MigitationReport()
        {
            return View();
        }

        public async Task<IActionResult> Get_Mitigation_Report_List(string RefNo, string Zone_Id, string FromDate, string ToDate)
        {
            using (client)
            {
                Mitigation_Param _UNIT = new Mitigation_Param()
                {
                    REF_NO = RefNo,
                    Zone_Id = Zone_Id,
                    From_Date = FromDate,
                    To_Date = ToDate
                };
                HttpResponseMessage response = client.PostAsync("MitigationReport/Alert_Report_Get_All", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_Mitigation_Report deserialized = JsonConvert.DeserializeObject<GET_Mitigation_Report>(customerJsonString)!;
                return PartialView(deserialized!.Data);
            }
        }

        public async Task<IActionResult> LoadAllRefNo()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("MitigationReport/Get_All_REF_ID").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Rain_Ref deserialized = JsonConvert.DeserializeObject<Get_Rain_Ref>(customerJsonString)!;
                return Json(deserialized!.Data);
            }
        }
        #endregion


    }
}
