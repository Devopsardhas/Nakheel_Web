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

namespace Nakheel_Web.Controllers
{
    public class LoginMController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string SignupReportPath;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        [Obsolete]
        public LoginMController(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            client = httpClientFactory.CreateClient("API");
            _hostingEnvironment = hostingEnvironment;
            SignupReportPath = configuration.GetConnectionString("SignupReportPath");
            _webHostEnvironment = webHostEnvironment;
        }
        #region [LOGIN]
        public async Task<IActionResult> AzureLogin()
        {
            try
            {
                if (User != null)
                {
                    var name = User.Identity.Name;
                    Login_ login_ = new Login_();
                    login_.Email_Id = name;
                    HttpResponseMessage response = client.PostAsync("Accounts/User_Verification", new StringContent(JsonConvert.SerializeObject(login_), Encoding.UTF8, "application/json")).Result;
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
                        //TempData["Res"] = "Fail";
                        return View("Login");
                    }
                }
                else
                {
                    return View("Login");
                }
            }
            catch (Exception)
            {
                throw;
            }
            //return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Remove("Login");
        //    HttpContext.Session.Clear();
        //    return RedirectToAction("Login");
        //}

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("Login");
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/Account/Login");
        }
        private void SetLocSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }
        #endregion
    }
}
