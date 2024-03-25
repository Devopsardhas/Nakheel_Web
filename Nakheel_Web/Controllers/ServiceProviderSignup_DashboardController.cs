using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.ServiceProviderSignup_Dashboard;
using Nakheel_Web.Models.Masters;
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
    public class ServiceProviderSignup_DashboardController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string Knowledge_Share_File;
        private readonly string ApplicableReportPath;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        [Obsolete]
        public ServiceProviderSignup_DashboardController(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
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
        public IActionResult ServiceProvider_SignupDashboard()
        {
            return View();
        }
        public async Task<IActionResult> GetServiceProviderDashboard(ServiceProvSignup_Dashboard_Param entity)
        {
            HttpResponseMessage response = client.PostAsync("ServiceProviderSignup_Dashboard/ServiceProviderDashboard", new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            ServiceProvider_Dashboard deserialized = JsonConvert.DeserializeObject<ServiceProvider_Dashboard>(customerJsonString)!;
            if (deserialized != null && deserialized.Status_Code == "200")
            {
                return Json(deserialized.Get_Data);
            }
            else
            {
                return Json("Failed");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ServiceProv_Dashboard_Card_View(ServiceProvSignup_Dashboard_Param dash_Params)
        {
            HttpResponseMessage response = client.PostAsync("ServiceProviderSignup_Dashboard/ServiceProv_Dashboard_Card_View", new StringContent(JsonConvert.SerializeObject(dash_Params), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            ServiceProvider_Dashboard deserialized = JsonConvert.DeserializeObject<ServiceProvider_Dashboard>(customerJsonString)!;
            return Json(deserialized.Get_Data1);
        }
    }
}
