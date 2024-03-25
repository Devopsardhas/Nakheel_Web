using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.AuditMaster;
using Nakheel_Web.Models.ControlOfWorkMaster;
using Nakheel_Web.Models.HandOverInsMaster;
using Nakheel_Web.Models.Masters;
using Nakheel_Web.Models.ServiceStatistics;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class ServiceMonthlyStatistics : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;

      
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private string conn;
        [Obsolete]
        public ServiceMonthlyStatistics(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            client = httpClientFactory.CreateClient("API");
            _hostingEnvironment = hostingEnvironment;
            conn = configuration.GetConnectionString("DefaultConnection");
        }
        public IActionResult ServiceMonthly()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Get_Service_Monthly_Statics(int CreatedBy)
        {
            using (client)
            {
                Service_Sat_Model _UNIT = new Service_Sat_Model
                {
                    CreatedBy = Convert.ToString(CreatedBy),
                };
                //HttpResponseMessage response = client.GetAsync("ServiceStatistics/GetService_Monthly_Statics").Result;
                HttpResponseMessage response = client.PostAsync("ServiceStatistics/GetService_Monthly_Statics", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;

                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_ServiceMonthly deserialized = JsonConvert.DeserializeObject<M_Get_ServiceMonthly>(customerJsonString)!;
                return PartialView(deserialized!.Get_All);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Service_Monthly_Statics([FromBody] Service_Sat_Model model)
        {
            using (client)
            {            
                string URL = "";
                //if (model.Service_Monthly_Id == "0")
                //{
                    URL = "ServiceStatistics/Add_Service_Monthly_Statics";
                //}

                HttpResponseMessage response = client.PostAsync(conn + URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        public async Task<IActionResult> Edit_Service_Monthly_Statics(int Service_Monthly_Id)
        {
            using (client)
            {
                Service_Sat_Model _UNIT = new Service_Sat_Model
                {
                    Service_Monthly_Id = Convert.ToString(Service_Monthly_Id),
                };

                HttpResponseMessage response = client.PostAsync("ServiceStatistics/Edit_Service_Monthly_Statics", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_ServiceMonthly deserialized = JsonConvert.DeserializeObject<M_Get_ServiceMonthly>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> Get_Service_Monthly_Statics_Filter(string Zone_Id, string Community_Id, string Building_Id,string CreatedBy,string Service_Year,string Service_Month)
        {
            using (client)
            {
                Service_Sat_Model _UNIT = new Service_Sat_Model()
                {
                    Zone_Id = Zone_Id,
                    Community_Id = Community_Id,
                    Building_Id = Building_Id,
                    CreatedBy = CreatedBy,
                    Service_Year = Service_Year,
                    Service_Month = Service_Month,
                };
                HttpResponseMessage response = client.PostAsync(conn + "ServiceStatistics/Get_Service_Monthly_Statics_Filter", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_ServiceMonthly deserialized = JsonConvert.DeserializeObject<M_Get_ServiceMonthly>(customerJsonString)!;
                return PartialView(deserialized!.Get_All);
            }
        }

        public async Task<JsonResult> Training_Attachments(List<IFormFile> files)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = "ServiceStatistics/Training_Evidence_Upload";
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
        public async Task<JsonResult> Training_LegalAttachments(List<IFormFile> files)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = "ServiceStatistics/Training_LegalEvidence_Upload";
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
        public async Task<JsonResult> Training_OtherAttachments(List<IFormFile> files)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = "ServiceStatistics/Training_OtherEvidence_Upload";
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
        public async Task<IActionResult> Service_Monthly_Statics_Approve([FromBody] Service_Sat_Model model)
        {
            using (client)
            {
                string URL = "";
                URL = "ServiceStatistics/Service_Monthly_Statics_Approve";
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Service_Monthly_Statics_Reject([FromBody] Reject_Reason model)
        {
            using (client)
            {
                string URL = "";
                URL = "ServiceStatistics/Service_Monthly_Statics_Reject";
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
    }
}
