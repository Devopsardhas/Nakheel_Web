using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.SqlServer.Server;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models;
using Nakheel_Web.Models.AccountsMaster;
using Nakheel_Web.Models.AuditMaster;
using Nakheel_Web.Models.Emergency;
using Nakheel_Web.Models.IncidentReport;
using Nakheel_Web.Models.InspectionMaster;
using Nakheel_Web.Models.Masters;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using static Nakheel_Web.Authentication.Common;
using Dropdown_Values = Nakheel_Web.Models.InspectionMaster.Dropdown_Values;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class InspectionController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        [Obsolete]
        public InspectionController(IHttpClientFactory httpClientFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
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
        #endregion

        #region [Inspection Masters]

        #region [Inspection Category Masters]
        public async Task<IActionResult> Insp_Category()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("InspectionMaster/Insp_Category_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Category deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Category>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Insp_Category_GetAll()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("InspectionMaster/Insp_Category_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Category deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Category>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insp_Category_Add(M_Insp_Category model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;

                    string URL = "";
                    if (model.Insp_Category_Id == "0")
                    {
                        URL = "InspectionMaster/Insp_Category_Add";
                    }
                    else
                    {
                        URL = "InspectionMaster/Insp_Category_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Insp_Category");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> Insp_Category_GetByID(string ID)
        {

            using (client)
            {
                M_Insp_Category _UNIT = new M_Insp_Category
                {
                    Insp_Category_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_Category_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Category deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Category>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> Insp_Category_Delete(string ID)
        {

            using (client)
            {
                M_Insp_Category _UNIT = new M_Insp_Category
                {
                    Insp_Category_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_Category_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Category deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Category>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region [Inspection Sub Category Masters]
        public async Task<IActionResult> Insp_Sub_Category()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("InspectionMaster/Insp_Sub_Category_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Sub_Category deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Sub_Category>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insp_Sub_Category_Add(List<M_Insp_Sub_Category> model)
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
                    string URL = "InspectionMaster/Insp_Sub_Category_Add";
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

        public async Task<IActionResult> Insp_Sub_Category_GetByID(string ID)
        {

            using (client)
            {
                M_Insp_Sub_Category _UNIT = new M_Insp_Sub_Category
                {
                    Insp_Category_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_Sub_Category_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Sub_Category deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Sub_Category>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> Insp_Sub_Category_Delete(string ID)
        {
            using (client)
            {
                M_Insp_Sub_Category _UNIT = new M_Insp_Sub_Category
                {
                    Insp_Sub_Category_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_Sub_Category_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Sub_Category deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Sub_Category>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region [Inspection Type Masters]
        public async Task<IActionResult> Insp_Type()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("InspectionMaster/Insp_Type_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Type deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Type>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insp_Type_Add(M_Insp_Type model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;

                    string URL = "";
                    if (model.Insp_Type_Id == "0")
                    {
                        URL = "InspectionMaster/Insp_Type_Add";
                    }
                    else
                    {
                        URL = "InspectionMaster/Insp_Type_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Insp_Type");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> Insp_Type_GetByID(string ID)
        {

            using (client)
            {
                M_Insp_Type _UNIT = new M_Insp_Type
                {
                    Insp_Type_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_Type_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Type deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Type>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> Insp_Type_Delete(string ID)
        {

            using (client)
            {
                M_Insp_Type _UNIT = new M_Insp_Type
                {
                    Insp_Type_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_Type_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Type deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Type>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region [Inspection Observation Masters]
        public async Task<IActionResult> Insp_Observation()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("InspectionMaster/Insp_Observation_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Observation deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Observation>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insp_Observation_Add(M_Insp_Observation model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;

                    string URL = "";
                    if (model.Insp_Observation_Id == "0")
                    {
                        URL = "InspectionMaster/Insp_Observation_Add";
                    }
                    else
                    {
                        URL = "InspectionMaster/Insp_Observation_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Insp_Observation");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> Insp_Observation_GetByID(string ID)
        {

            using (client)
            {
                M_Insp_Observation _UNIT = new M_Insp_Observation
                {
                    Insp_Observation_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_Observation_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Observation deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Observation>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> Insp_Observation_Delete(string ID)
        {

            using (client)
            {
                M_Insp_Observation _UNIT = new M_Insp_Observation
                {
                    Insp_Observation_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_Observation_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Observation deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Observation>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region [Inspection Topic Masters]
        public async Task<IActionResult> Insp_Topic()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("InspectionMaster/Insp_Topic_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Topic deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Topic>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insp_Topic_Add(List<M_Insp_Topic> model)
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
                    string URL = "InspectionMaster/Insp_Topic_Add";
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

        public async Task<IActionResult> Insp_Topic_GetByID(string ID)
        {

            using (client)
            {
                M_Insp_Topic _UNIT = new M_Insp_Topic
                {
                    Insp_Type_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_Topic_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Topic deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Topic>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> Insp_Topic_Delete(string ID)
        {
            using (client)
            {
                M_Insp_Topic _UNIT = new M_Insp_Topic
                {
                    Insp_Topic_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_Topic_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Topic deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Topic>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region [Inspection Questionnaires Masters]
        public async Task<IActionResult> Insp_Questionnaires()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("InspectionMaster/Insp_Questionnaires_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Questionnaires deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Questionnaires>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insp_Questionnaires_Add(List<M_Insp_Questionnaires> model)
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
                    string URL = "InspectionMaster/Insp_Questionnaires_Add";
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

        public async Task<IActionResult> Insp_Questionnaires_GetByID(M_Insp_Questionnaires model)
        {

            using (client)
            {
                M_Insp_Questionnaires _UNIT = new M_Insp_Questionnaires
                {
                    Insp_Type_Id = model.Insp_Type_Id,
                    Insp_Topic_Id = model.Insp_Topic_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_Questionnaires_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Questionnaires deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Questionnaires>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> Insp_Questionnaires_Delete(string ID)
        {
            using (client)
            {
                M_Insp_Questionnaires _UNIT = new M_Insp_Questionnaires
                {
                    Insp_Questionnaires_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_Questionnaires_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Questionnaires deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Questionnaires>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region [Inspection Landscaping Master]
        public async Task<IActionResult> Insp_Landscap_Master()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("InspectionMaster/Insp_Landscap_Mas_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Landscap_Master deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscap_Master>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Insp_Landscap_Mas_GetAll()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("InspectionMaster/Insp_Landscap_Mas_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Landscap_Master deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscap_Master>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insp_Landscap_Mas_Add(M_Insp_Landscap_Master model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;

                    string URL = "";
                    if (model.Insp_Landscap_Mas_Id == "0")
                    {
                        URL = "InspectionMaster/Insp_Landscap_Mas_Add";
                    }
                    else
                    {
                        URL = "InspectionMaster/Insp_Landscap_Mas_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Insp_Landscap_Master");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> Insp_Landscap_Mas_GetByID(string ID)
        {

            using (client)
            {
                M_Insp_Landscap_Master _UNIT = new M_Insp_Landscap_Master
                {
                    Insp_Landscap_Mas_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_Landscap_Mas_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Landscap_Master deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscap_Master>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> Insp_Landscap_Mas_Delete(string ID)
        {

            using (client)
            {
                M_Insp_Landscap_Master _UNIT = new M_Insp_Landscap_Master
                {
                    Insp_Landscap_Mas_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_Landscap_Mas_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Landscap_Master deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscap_Master>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region [Inspection Sub Category Masters]
        public async Task<IActionResult> Insp_Landscap_Sub_Cat_GetAll()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("InspectionMaster/Insp_Landscap_Sub_Cat_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Landscap_Sub_Master deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscap_Sub_Master>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insp_Landscap_Sub_Cat_Add(List<M_Insp_Landscap_Sub_Master> model)
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
                    string URL = "InspectionMaster/Insp_Landscap_Sub_Cat_Add";
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

        public async Task<IActionResult> Insp_Landscap_Sub_Cat_GetById(string ID)
        {

            using (client)
            {
                M_Insp_Landscap_Sub_Master _UNIT = new M_Insp_Landscap_Sub_Master
                {
                    Insp_Landscap_Mas_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_Landscap_Sub_Cat_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Landscap_Sub_Master deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscap_Sub_Master>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> Insp_Landscap_Sub_Cat_Delete(string ID)
        {
            using (client)
            {
                M_Insp_Landscap_Sub_Master _UNIT = new M_Insp_Landscap_Sub_Master
                {
                    Insp_Landscap_Sub_Mas_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_Landscap_Sub_Cat_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Landscap_Sub_Master deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscap_Sub_Master>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region [Inspection SoftService Master]
        public async Task<IActionResult> Insp_SoftService_Master()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("InspectionMaster/Insp_SoftService_Mas_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Landscap_Master deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscap_Master>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Insp_SoftService_Mas_GetAll()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("InspectionMaster/Insp_SoftService_Mas_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Landscap_Master deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscap_Master>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insp_SoftService_Mas_Add(M_Insp_Landscap_Master model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;

                    string URL = "";
                    if (model.Insp_Landscap_Mas_Id == "0")
                    {
                        URL = "InspectionMaster/Insp_SoftService_Mas_Add";
                    }
                    else
                    {
                        URL = "InspectionMaster/Insp_SoftService_Mas_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Insp_SoftService_Master");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> Insp_SoftService_Mas_GetByID(string ID)
        {

            using (client)
            {
                M_Insp_Landscap_Master _UNIT = new M_Insp_Landscap_Master
                {
                    Insp_Landscap_Mas_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_SoftService_Mas_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Landscap_Master deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscap_Master>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> Insp_SoftService_Mas_Delete(string ID)
        {

            using (client)
            {
                M_Insp_Landscap_Master _UNIT = new M_Insp_Landscap_Master
                {
                    Insp_Landscap_Mas_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_SoftService_Mas_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Landscap_Master deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscap_Master>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region [Inspection SoftService Sub Category Masters]
        public async Task<IActionResult> Insp_SoftService_Sub_Cat_GetAll()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("InspectionMaster/Insp_SoftService_Sub_Cat_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Landscap_Sub_Master deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscap_Sub_Master>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insp_SoftService_Sub_Cat_Add(List<M_Insp_Landscap_Sub_Master> model)
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
                    string URL = "InspectionMaster/Insp_SoftService_Sub_Cat_Add";
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

        public async Task<IActionResult> Insp_SoftService_Sub_Cat_GetById(string ID)
        {

            using (client)
            {
                M_Insp_Landscap_Sub_Master _UNIT = new M_Insp_Landscap_Sub_Master
                {
                    Insp_Landscap_Mas_Id = ID

                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_SoftService_Sub_Cat_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Landscap_Sub_Master deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscap_Sub_Master>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> Insp_SoftService_Sub_Cat_Delete(string ID)
        {
            using (client)
            {
                M_Insp_Landscap_Sub_Master _UNIT = new M_Insp_Landscap_Sub_Master
                {
                    Insp_Landscap_Sub_Mas_Id = ID
                };
                HttpResponseMessage response = client.PostAsync("InspectionMaster/Insp_SoftService_Sub_Cat_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Landscap_Sub_Master deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscap_Sub_Master>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #endregion

        #region [SPOT INSPECTION]

        #region [Inspection Spot Request]
        public IActionResult Insp_Request()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Request_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            M_Get_Insp_Request deserialized = new M_Get_Insp_Request();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Request_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Request>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Insp_Request>();
            }

            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList(),
            });
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Request_Add([FromBody] M_Insp_Request model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Request_Add";
                    Login_ LoginClass = GetLoginDetails();

                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    if (model.Insp_Request_Id != "0")
                    {
                        URL = "InspectionForm/Insp_Request_Update";
                    }
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    deserialized.Remarks = model.Walk_In_Insp_Name;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> P_Insp_Request_Add_View(string ID, string Walk_In_Insp)
        {
            Login_ LoginClass = GetLoginDetails();
            M_Insp_Request _UNIT = new M_Insp_Request
            {
                Insp_Request_Id = ID,
                Zone_Id = LoginClass.Zone_Id
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Request_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Request deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Request>(customerJsonString)!;
            DateTime StartDate = Convert.ToDateTime(deserialized.Get_ById!.Inspection_Date);
            deserialized.Get_ById!.Inspection_Date = StartDate.ToString("yyyy-MM-dd");
            if (deserialized.Get_ById.Insp_Request_Id == "0")
            {
                deserialized.Get_ById.Business_Unit_Id = LoginClass.Business_Unit_Id;
                deserialized.Get_ById.Zone_Id = LoginClass.Zone_Id;
                deserialized.Get_ById.Walk_In_Insp_Name = Walk_In_Insp;
            }
            return PartialView(deserialized.Get_ById);
        }
        public async Task<IActionResult> P_Insp_Request_GetbyId(string ID)
        {
            M_Insp_Request _UNIT = new M_Insp_Request
            {
                Insp_Request_Id = ID
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Request_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Request deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Request>(customerJsonString)!;
            DateTime StartDate = Convert.ToDateTime(deserialized.Get_ById!.Inspection_Date);
            deserialized.Get_ById!.Inspection_Date = StartDate.ToString("yyyy-MM-dd");
            return PartialView(deserialized);
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Request_ApprovalReject(M_Insp_Request_Approval model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Request_ApprovalReject";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    //TempData["msg"] = deserialized!.MESSAGE;
                    return Json(deserialized!.STATUS_CODE);
                }
            }
            else
            {
                return NoContent();
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
                return Json("Uploaded Successfully");
            }
        }
        public async Task<JsonResult> UploadImagePdf(List<IFormFile> files)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = client.BaseAddress + "InspectionForm/UploadImagePdf";

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
        public async Task<JsonResult> Upload_Picture(List<IFormFile> files)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = client.BaseAddress + "InspectionForm/UploadPicture";

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
        public async Task<IActionResult> Insp_Req_Photo_Delete(string Req_Document_Review_Id)
        {
            using (client)
            {
                Insp_Req_Doc_Review _UNIT = new Insp_Req_Doc_Review
                {
                    Req_Document_Review_Id = Req_Document_Review_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Req_Photo_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Spot_CA_Photo_List deserialized = JsonConvert.DeserializeObject<M_Get_Spot_CA_Photo_List>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        #endregion

        #region [Inspection Spot Finding]
        public async Task<IActionResult> Insp_Sub_Category_Master_GetbyId(string Value)
        {
            using (client)
            {
                M_Insp_Value_Text _UNIT = new M_Insp_Value_Text
                {
                    Value = Value
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Sub_Category_Master_GetbyId", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Master_Finding_List deserialized = JsonConvert.DeserializeObject<M_Get_Master_Finding_List>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Spot_Finding_Add([FromBody] M_Insp_Spot_Finding model)
        {
            try
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    string URL = "InspectionForm/Insp_Spot_Finding_Add";
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Find_ApprovalReject(M_Insp_Finding_Approval model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Find_ApprovalReject";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    //TempData["msg"] = deserialized!.MESSAGE;
                    return Json(deserialized!.STATUS_CODE);
                }
            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> P_Insp_Spot_Find_AddView(string ID)
        {
            Login_ LoginClass = GetLoginDetails();
            M_Insp_Request _UNIT = new M_Insp_Request
            {
                Insp_Request_Id = ID
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Request_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Request deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Request>(customerJsonString)!;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Insp_Request_Id = _UNIT.Insp_Request_Id;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Login_Id = LoginClass.Employee_Identity_Id;
            DateTime StartDates = Convert.ToDateTime(deserialized.Get_ById!.Inspection_Date);
            TempData["msg_Insp_Date"] = StartDates.ToString("yyyy-MM-dd");
            TempData["msg"] = deserialized!.MESSAGE;
            return PartialView(deserialized.Get_ById!.Insp_Spot_Finding_List);
        }
        public async Task<IActionResult> P_Insp_Spot_Find_Rej_Edit(string Insp_Sub_Finding_Id)
        {
            using (client)
            {
                M_Insp_Spot_Sub_Finding _UNIT = new M_Insp_Spot_Sub_Finding
                {
                    Insp_Sub_Finding_Id = Insp_Sub_Finding_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Sub_Find_List_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Sub_Finding_List deserialized = JsonConvert.DeserializeObject<M_Get_Sub_Finding_List>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        public async Task<IActionResult> Insp_Sub_Find_Photo_Delete(string Insp_Sub_Photo_Id)
        {
            using (client)
            {
                M_Insp_Spot_Find_Sub_Photo _UNIT = new M_Insp_Spot_Find_Sub_Photo
                {
                    Insp_Sub_Photo_Id = Insp_Sub_Photo_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Sub_Find_Photo_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Spot_Find_Sub_Photo_List deserialized = JsonConvert.DeserializeObject<M_Get_Spot_Find_Sub_Photo_List>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> Insp_Sub_Finding_Delete(string Insp_Sub_Finding_Id)
        {
            using (client)
            {
                M_Insp_Spot_Sub_Finding _UNIT = new M_Insp_Spot_Sub_Finding
                {
                    Insp_Sub_Finding_Id = Insp_Sub_Finding_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Sub_Finding_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Sub_Finding_List deserialized = JsonConvert.DeserializeObject<M_Get_Sub_Finding_List>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> P_Insp_Spot_Find_Walkin_AddView(string ID)
        {
            Login_ LoginClass = GetLoginDetails();
            M_Insp_Request _UNIT = new M_Insp_Request
            {
                Insp_Request_Id = ID,
                Zone_Id = LoginClass.Zone_Id
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Request_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Request deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Request>(customerJsonString)!;
            DateTime StartDate = Convert.ToDateTime(deserialized.Get_ById!.Inspection_Date);
            deserialized.Get_ById!.Inspection_Date = StartDate.ToString("yyyy-MM-dd");
            if (deserialized.Get_ById.Insp_Request_Id == "0")
            {
                deserialized.Get_ById.Business_Unit_Id = LoginClass.Business_Unit_Id;
                deserialized.Get_ById.Zone_Id = LoginClass.Zone_Id;
            }
            return PartialView(deserialized.Get_ById);
        }
        public async Task<IActionResult> Load_Spot_Find_WalkinZone(string Zone_Id)
        {
            using (client)
            {
                M_Insp_Spot_WalkIn_Master _UNIT = new M_Insp_Spot_WalkIn_Master
                {
                    Value = Zone_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Load_Spot_Find_WalkinZone", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Spot_WalkIn_Master deserialized = JsonConvert.DeserializeObject<M_Get_Spot_WalkIn_Master>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Spot_Finding_Walk_In_Add([FromBody] M_Insp_Request model)
        {
            try
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    string URL = "InspectionForm/Add_Insp_Spot_Walk_In_Finding";
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        #region [Insp Spot Corrective Action]
        public IActionResult Insp_Spot_Corrective_Action()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Spot_CA_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            M_Get_Insp_Request deserialized = new M_Get_Insp_Request();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Spot_Corrective_Action_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Request>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Insp_Request>();
            }

            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList(),
            });
        }
        public async Task<IActionResult> P_Insp_Spot_CA_GetbyId(string ID)
        {
            Login_ LoginClass = GetLoginDetails();

            M_Insp_Request _UNIT = new M_Insp_Request
            {
                Insp_Request_Id = ID,
                CreatedBy = LoginClass.Employee_Identity_Id,
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Spot_CA_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Request deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Request>(customerJsonString)!;
            return PartialView(deserialized.Get_ById!.Insp_Spot_Finding_List);
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Spot_Corrective_Action_Add(List<M_Insp_Spot_Corrective_Action> model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Spot_Corrective_Action_Add";
                    Login_ LoginClass = GetLoginDetails();
                    foreach (var item in model)
                    {
                        item.CreatedBy = LoginClass.Employee_Identity_Id;
                    }
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    //TempData["msg"] = deserialized!.MESSAGE;
                    return Json(deserialized!.STATUS_CODE);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Spot_Corrective_Action_Reassign(M_Insp_Spot_Corrective_Action model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Spot_CA_ReAssign";
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
        [HttpPost]
        public async Task<IActionResult> Insp_Spot_Action_Closure_Add(M_Insp_Spot_Corrective_Action model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Spot_CA_Action_Closure_Add";
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
        [HttpPost]
        public async Task<IActionResult> Insp_Spot_CA_Photo_Delete(string Insp_CA_Photo_Id)
        {
            using (client)
            {
                M_Insp_Spot_CA_Photo _UNIT = new M_Insp_Spot_CA_Photo
                {
                    Insp_CA_Photo_Id = Insp_CA_Photo_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Spot_CA_Photo_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Spot_CA_Photo_List deserialized = JsonConvert.DeserializeObject<M_Get_Spot_CA_Photo_List>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Spot_CA_Approval_Add(string Corrective_Action_Id, string Remarks, string Status, string Description)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();

                    M_Insp_Spot_Corrective_Action _UNIT = new M_Insp_Spot_Corrective_Action
                    {
                        Corrective_Action_Id = Corrective_Action_Id,
                        Remarks = Remarks,
                        CreatedBy = LoginClass.Employee_Identity_Id,
                        Status = Status,
                        Description = Description,
                    };
                    string URL = "InspectionForm/Insp_Spot_CA_ApprovalReject";
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
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
        #endregion

        #endregion

        #region [INSP SAFETY VIOLATION]

        #region [Safety Violation]
       
        public IActionResult Insp_Safety_Violation(string id)
        {
            HttpContext.Session.SetString("Insp_Safety_Card_Id", id);
            return View();
        }
        public IActionResult Insp_Safety_Violation_Temp(string id)
        {
            TempData["Safety_Id"] = id;
            return RedirectToAction("Insp_Safety_Violation", new { id = id });
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Safety_Violation_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            var str = HttpContext.Session.GetString("Insp_Safety_Card_Id");
            var data = new List<object>();
            M_Get_Insp_Request deserialized = new M_Get_Insp_Request();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;
            model.Card_Id = str;
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Safety_Violation_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Request>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Insp_Request>();
            }

            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList(),
            });
        }
        public async Task<IActionResult> P_Safety_Violation_Add_View(string ID)
        {
            TempData["Safety_Id"] = "";
            M_Insp_Safety_Violation _UNIT = new M_Insp_Safety_Violation
            {
                Safety_Violation_Id = ID
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Safety_Violation_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Safety_Violation deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Safety_Violation>(customerJsonString)!;
            DateTime StartDatesv = Convert.ToDateTime(deserialized.Get_ById!.Inspection_Date);
            TempData["SV_msg_Insp_Date"] = StartDatesv.ToString("yyyy-MM-dd");
            return PartialView(deserialized.Get_ById);
        }
        public async Task<IActionResult> P_Create_Safety_Violation(string ID)
        {
            M_Insp_Safety_Violation _UNIT = new M_Insp_Safety_Violation
            {
                Safety_Violation_Id = ID
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Safety_Violation_Add_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Safety_Violation deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Safety_Violation>(customerJsonString)!;
            return PartialView(deserialized.Get_ById);
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Safety_Violation_Create([FromBody] M_Insp_Safety_Violation model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Safety_Violation_Create";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;

                    List<M_Insp_Safety_Photos> pHOTOs = new List<M_Insp_Safety_Photos>();
                    var str = HttpContext.Session.GetString("Safety_Photos");
                    if (str != null)
                    {
                        string[] photo = JsonConvert.DeserializeObject<string[]>(str)!;
                        if (photo != null)
                        {
                            for (int i = 0; i < photo.Length; i++)
                            {
                                M_Insp_Safety_Photos pHOTO = new M_Insp_Safety_Photos
                                {
                                    File_Path = photo[i],
                                };

                                pHOTOs.Add(pHOTO);
                            }
                        }
                        HttpContext.Session.Remove("Safety_Photos");
                        model.Insp_Safe_Vio_Photos_List = pHOTOs;
                    }

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
        [HttpPost]
        public async Task<IActionResult> Insp_Safety_Violation_Add([FromBody] M_Insp_Safety_Violation model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Safety_Violation_Add";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;

                    List<M_Insp_Safety_Photos> pHOTOs = new List<M_Insp_Safety_Photos>();
                    var str = HttpContext.Session.GetString("Safety_Photos");
                    if (str != null)
                    {
                        string[] photo = JsonConvert.DeserializeObject<string[]>(str)!;
                        if (photo != null)
                        {
                            for (int i = 0; i < photo.Length; i++)
                            {
                                M_Insp_Safety_Photos pHOTO = new M_Insp_Safety_Photos
                                {
                                    File_Path = photo[i],
                                };
                                pHOTOs.Add(pHOTO);
                            }
                        }
                        HttpContext.Session.Remove("Safety_Photos");
                        model.Insp_Safe_Vio_Photos_List = pHOTOs;
                    }

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
        [HttpPost]
        public async Task<IActionResult> Insp_Safety_Violation_ApprovalReject(M_Insp_Safety_ViolationReject model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Safe_Vio_ApprovalReject";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Safe_Vio_Corrective_Action_Add(List<M_Safety_Vio_Corrective_Action> model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Safe_Vio_Corrective_Action_Add";
                    Login_ LoginClass = GetLoginDetails();
                    foreach (var item in model)
                    {
                        item.CreatedBy = LoginClass.Employee_Identity_Id;
                    }
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
        [HttpPost]
        public async Task<IActionResult> Insp_SV_CA_Approval_Add(string SV_Corrective_Action_Id, string Remarks, string Status, string Description)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();

                    M_Safety_Vio_Corrective_Action _UNIT = new M_Safety_Vio_Corrective_Action
                    {
                        SV_Corrective_Action_Id = SV_Corrective_Action_Id,
                        Remarks = Remarks,
                        CreatedBy = LoginClass.Employee_Identity_Id,
                        Status = Status,
                        Description = Description,
                    };
                    string URL = "InspectionForm/Insp_Safe_Vio_Spot_CA_ApprovalReject";
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
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
        public async Task<IActionResult> Insp_LoadAllServiceProvider(string Zone_Id, string Community_Id)
        {
            using (client)
            {
                M_Insp_Safety_Violation _UNIT = new M_Insp_Safety_Violation
                {
                    Zone_Id = Zone_Id,
                    Community_Id = Community_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Safety_Vio_Ser_Pro_GetAll", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_M_Emp_Service_Provider deserialized = JsonConvert.DeserializeObject<M_Get_M_Emp_Service_Provider>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<JsonResult> UploadPhotoFiles(List<IFormFile> files)
        {
            try
            {
                string url = "InspectionForm/ImageUpload";
                string[] deserialized = await FileUpload.UploadMultipleFiles(files, url, client);
                var key = "Safety_Photos";
                var str = JsonConvert.SerializeObject(deserialized);
                HttpContext.Session.SetString(key, str);

                return Json("Uploaded Successfully");
            }
            catch (Exception ex)
            {
                return Json("Uploaded Successfully");
            }
        }
        public async Task<JsonResult> SafeUploadPhotoFiles(List<IFormFile> files)
        {
            try
            {
                string url = "InspectionForm/ImageUpload";
                string[] deserialized = await FileUpload.UploadMultipleFiles(files, url, client);
                var key = "Safety_Photos";
                var str = JsonConvert.SerializeObject(deserialized);
                HttpContext.Session.SetString(key, str);
                return Json(key);
            }
            catch (Exception ex)
            {
                return Json("Uploaded Successfully");
            }
        }
        public async Task<IActionResult> Insp_Safe_Photo_Delete(string Photo_Id)
        {
            using (client)
            {
                M_Insp_Safety_Photos _UNIT = new M_Insp_Safety_Photos
                {
                    Photo_Id = Photo_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Safe_Photo_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Safety_Photos deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Safety_Photos>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        public async Task<JsonResult> SafeUploadPicture(List<IFormFile> files)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = client.BaseAddress + "InspectionForm/UploadPicture";

                    string[] deserialized = await FileUpload.UploadMultipleFiles(files, url, client);
                    var key = "Safety_Photos";
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

        #region [Safety Violation Corrective Action]
        public IActionResult Insp_Safety_Vio_Corrective_Action()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Safety_Vio_CA_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            M_Get_Insp_Request deserialized = new M_Get_Insp_Request();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Safety_Vio_CA_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Request>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Insp_Request>();
            }

            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList(),
            });
        }
        public async Task<IActionResult> P_Safety_Violation_CA_GetbyId(string ID)
        {
            Login_ LoginClass = GetLoginDetails();
            M_Insp_Safety_Violation _UNIT = new M_Insp_Safety_Violation
            {
                Safety_Violation_Id = ID,
                CreatedBy = LoginClass.Employee_Identity_Id
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Safety_Violation_CA_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Safety_Violation deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Safety_Violation>(customerJsonString)!;
            return PartialView(deserialized.Get_ById);
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Safety_Violation_Action_Closure_Add(M_Safety_Vio_Corrective_Action model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_SV_CA_Action_Closure_Add";
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
        [HttpPost]
        public async Task<IActionResult> Insp_SV_Corrective_Action_Reassign(M_Safety_Vio_Corrective_Action model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_SV_CA_ReAssign";
                    Login_ LoginClass = GetLoginDetails();

                    model.CreatedBy = LoginClass.Employee_Identity_Id;

                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    return Json(deserialized!);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_SV_CA_Photo_Delete(string Insp_SV_CA_Photo_Id)
        {
            using (client)
            {
                M_Insp_SV_CA_Photo _UNIT = new M_Insp_SV_CA_Photo
                {
                    Insp_SV_CA_Photo_Id = Insp_SV_CA_Photo_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_SV_CA_Photo_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_SV_CA_Photo_List deserialized = JsonConvert.DeserializeObject<M_Get_SV_CA_Photo_List>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        #endregion

        #endregion

        #region [JOINT INSPECTION]

        #region [Inspection Joint Request]
        public IActionResult Joint_Insp_Request()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Joint_Insp_Request_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            M_Get_Insp_Joint_Request deserialized = new M_Get_Insp_Joint_Request();
            Login_ LoginClass = GetLoginDetails();

            model.CreatedBy = LoginClass.Employee_Identity_Id;
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Joint_Request_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Joint_Request>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Insp_Joint_Request>();
            }
            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList(),
            });
        }
        [HttpPost]
        public async Task<IActionResult> Joint_Insp_Request_Add([FromBody] M_Insp_Joint_Request model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Joint_Request_Add";
                    Login_ LoginClass = GetLoginDetails();

                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    //if (model.Insp_Request_Id != "0")
                    //{
                    //    URL = "InspectionForm/Insp_Joint_Request_Update";
                    //}
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    deserialized.Remarks = model.Walk_In_Insp_Name;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> P_Joint_Insp_Request_Add_View(string ID)
        {
            Login_ LoginClass = GetLoginDetails();

            M_Insp_Joint_Request _UNIT = new M_Insp_Joint_Request
            {
                Insp_Request_Id = ID,
                Zone_Id = LoginClass.Zone_Id
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Joint_Request_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Joint_Request deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Joint_Request>(customerJsonString)!;
            DateTime StartDate = Convert.ToDateTime(deserialized.Get_ById!.Inspection_Date);
            deserialized.Get_ById!.Inspection_Date = StartDate.ToString("yyyy-MM-dd");
            if (deserialized.Get_ById.Insp_Request_Id == "0")
            {
                deserialized.Get_ById.Business_Unit_Id = LoginClass.Business_Unit_Id;
                deserialized.Get_ById.Zone_Id = LoginClass.Zone_Id;
                DateTime AddDate = DateTime.Today.AddDays(10);
                deserialized.Get_ById!.Inspection_Date = AddDate.ToString("yyyy-MM-dd");
            }
            else
            {
                if (deserialized.Get_ById.Schedule_Type == "Semi Annually")
                {
                    deserialized.Get_ById.Schedule_Type = "semiannually";
                }
            }
            return PartialView(deserialized.Get_ById);
        }
        public async Task<IActionResult> P_Joint_Insp_Request_GetbyId(string ID)
        {
            M_Insp_Joint_Request _UNIT = new M_Insp_Joint_Request
            {
                Insp_Request_Id = ID
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Joint_Request_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Joint_Request deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Joint_Request>(customerJsonString)!;
            DateTime StartDate = Convert.ToDateTime(deserialized.Get_ById!.Inspection_Date);
            deserialized.Get_ById!.Inspection_Date = StartDate.ToString("yyyy-MM-dd");
            return PartialView(deserialized);
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Joint_Req_Supervisor_GetbyId(string Zone_Id)
        {
            using (client)
            {
                Dropdown_Values _UNIT = new Dropdown_Values
                {
                    Value = Zone_Id,
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Joint_Req_Supervisor_GetbyId", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Dropdown_Values deserialized = JsonConvert.DeserializeObject<M_Get_Dropdown_Values>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Joint_Request_ApprovalReject(M_Insp_Joint_Request_Approval model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Joint_Req_ApprovalReject";
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
        public async Task<IActionResult> Insp_Joint_Zone_based_HSE_Team_Emp(string Zone_Id)
        {
            M_Insp_Joint_Request _UNIT = new M_Insp_Joint_Request
            {
                Zone_Id = Zone_Id
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Joint_Zone_based_HSE_Team_Emp", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Dropdown_Values deserialized = JsonConvert.DeserializeObject<M_Get_Dropdown_Values>(customerJsonString)!;
            return Json(deserialized.Get_All);
        }
        #endregion

        #region [Inspection Joint Finding]
        public async Task<IActionResult> P_Insp_Joint_Find_AddView(string ID)
        {
            Login_ LoginClass = GetLoginDetails();
            M_Insp_Joint_Request _UNIT = new M_Insp_Joint_Request
            {
                Insp_Request_Id = ID
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Joint_Request_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Joint_Request deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Joint_Request>(customerJsonString)!;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Insp_Request_Id = ID;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Login_Id = LoginClass.Employee_Identity_Id;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.NCM_Supervisor_Text = deserialized.Get_ById!.Supervisor_Name;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Service_Provider_Name = deserialized.Get_ById!.Service_Provider_Name;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Insp_Type_Name = deserialized.Get_ById!.Insp_Category_Name;
            DateTime J_StartDates = Convert.ToDateTime(deserialized.Get_ById!.Inspection_Date);
            TempData["J_msg_Insp_Date"] = J_StartDates.ToString("yyyy-MM-dd");
            TempData["msg"] = deserialized!.MESSAGE;
            return PartialView(deserialized.Get_ById!.Insp_Spot_Finding_List);
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Joint_Finding_Add([FromBody] M_Insp_Spot_Finding model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    Login_ LoginClass = GetLoginDetails();

                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    if (model.Insp_Request_Id != "0")
                    {
                        URL = "InspectionForm/Insp_Joint_Finding_Add";
                    }
                    if (URL != "")
                    {
                        HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                        string customerJsonString = await response.Content.ReadAsStringAsync();
                        RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                        return Json(deserialized);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Joint_Find_ApprovalReject(M_Insp_Finding_Approval model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Joint_Find_ApprovalReject";
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
        public async Task<IActionResult> P_Insp_Joint_Find_Rej_Edit(string Insp_Sub_Finding_Id)
        {
            using (client)
            {
                M_Insp_Spot_Sub_Finding _UNIT = new M_Insp_Spot_Sub_Finding
                {
                    Insp_Sub_Finding_Id = Insp_Sub_Finding_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Joint_Sub_Find_List_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Sub_Finding_List deserialized = JsonConvert.DeserializeObject<M_Get_Sub_Finding_List>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        public async Task<IActionResult> Insp_Joint_Sub_Find_Photo_Delete(string Insp_Sub_Photo_Id)
        {
            using (client)
            {
                M_Insp_Spot_Find_Sub_Photo _UNIT = new M_Insp_Spot_Find_Sub_Photo
                {
                    Insp_Sub_Photo_Id = Insp_Sub_Photo_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Joint_Sub_Find_Photo_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Spot_Find_Sub_Photo_List deserialized = JsonConvert.DeserializeObject<M_Get_Spot_Find_Sub_Photo_List>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> Insp_Joint_Sub_Finding_Delete(string Insp_Sub_Finding_Id)
        {
            using (client)
            {
                M_Insp_Spot_Sub_Finding _UNIT = new M_Insp_Spot_Sub_Finding
                {
                    Insp_Sub_Finding_Id = Insp_Sub_Finding_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Joint_Sub_Finding_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Sub_Finding_List deserialized = JsonConvert.DeserializeObject<M_Get_Sub_Finding_List>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        #endregion

        #region [Insp Joint Corrective Action]

        [HttpPost]
        public async Task<IActionResult> Insp_Joint_Corrective_Action_Add(List<M_Insp_Spot_Corrective_Action> model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Joint_Corrective_Action_Add";
                    Login_ LoginClass = GetLoginDetails();
                    foreach (var item in model)
                    {
                        item.CreatedBy = LoginClass.Employee_Identity_Id;
                    }
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
        public IActionResult Insp_Joint_Corrective_Action()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Joint_CA_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            M_Get_Insp_Joint_Request deserialized = new M_Get_Insp_Joint_Request();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Join_Corrective_Action_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Joint_Request>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Insp_Joint_Request>();
            }

            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList(),
            });
        }
        public async Task<IActionResult> P_Insp_Joint_CA_GetbyId(string ID)
        {
            Login_ LoginClass = GetLoginDetails();

            M_Insp_Joint_Request _UNIT = new M_Insp_Joint_Request
            {
                Insp_Request_Id = ID,
                CreatedBy = LoginClass.Employee_Identity_Id,
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Joint_CA_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Joint_Request deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Joint_Request>(customerJsonString)!;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.NCM_Supervisor_Text = deserialized.Get_ById!.Supervisor_Name;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Service_Provider_Name = deserialized.Get_ById!.Service_Provider_Name;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Insp_Type_Name = deserialized.Get_ById!.Insp_Category_Name;
            return PartialView(deserialized.Get_ById!.Insp_Spot_Finding_List);
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Joint_Corrective_Action_Reassign(M_Insp_Spot_Corrective_Action model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Joint_CA_ReAssign";
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
        [HttpPost]
        public async Task<IActionResult> Insp_Joint_Action_Closure_Add(M_Insp_Spot_Corrective_Action model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Joint_CA_Action_Closure_Add";
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
        [HttpPost]
        public async Task<IActionResult> Insp_Joint_CA_Photo_Delete(string Insp_CA_Photo_Id)
        {
            using (client)
            {
                M_Insp_Spot_CA_Photo _UNIT = new M_Insp_Spot_CA_Photo
                {
                    Insp_CA_Photo_Id = Insp_CA_Photo_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Joint_CA_Photo_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Spot_CA_Photo_List deserialized = JsonConvert.DeserializeObject<M_Get_Spot_CA_Photo_List>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Joint_CA_Approval_Add(string Corrective_Action_Id, string Remarks, string Status, string Description)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();

                    M_Insp_Spot_Corrective_Action _UNIT = new M_Insp_Spot_Corrective_Action
                    {
                        Corrective_Action_Id = Corrective_Action_Id,
                        Remarks = Remarks,
                        CreatedBy = LoginClass.Employee_Identity_Id,
                        Status = Status,
                        Description = Description,
                    };
                    string URL = "InspectionForm/Insp_Joint_CA_ApprovalReject";
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
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

        #endregion

        #endregion

        #region [LEADERSHIP TOUR]

        public IActionResult Leader_Insp_Request()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Leader_Insp_Request_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            M_Get_Insp_Leader_Request deserialized = new M_Get_Insp_Leader_Request();
            Login_ LoginClass = GetLoginDetails();

            model.CreatedBy = LoginClass.Employee_Identity_Id;
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Leader_Request_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Leader_Request>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Insp_Leader_Request>();
            }
            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList(),
            });
        }
        public async Task<IActionResult> P_Leader_Insp_Request_Add_View(string ID, string Walk_Type)
        {
            Login_ LoginClass = GetLoginDetails();

            M_Insp_Leader_Request _UNIT = new M_Insp_Leader_Request
            {
                Insp_Request_Id = ID,
                Zone_Id = LoginClass.Zone_Id
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Leader_Request_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Leader_Request deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Leader_Request>(customerJsonString)!;
            DateTime StartDate = Convert.ToDateTime(deserialized.Get_ById!.Inspection_Date);
            deserialized.Get_ById!.Inspection_Date = StartDate.ToString("yyyy-MM-dd");
            if (deserialized.Get_ById.Insp_Request_Id == "0")
            {
                deserialized.Get_ById.Business_Unit_Id = LoginClass.Business_Unit_Id;
                deserialized.Get_ById.Zone_Id = LoginClass.Zone_Id;
                if (Walk_Type == "Walk_In_Insp")
                {
                    DateTime AddDate = DateTime.Today;
                    deserialized.Get_ById!.Inspection_Date = AddDate.ToString("yyyy-MM-dd");
                    deserialized.Get_ById!.Walk_In_Insp_Name = Walk_Type;
                }
                else
                {
                    DateTime AddDate = DateTime.Today.AddDays(10);
                    deserialized.Get_ById!.Inspection_Date = AddDate.ToString("yyyy-MM-dd");
                    deserialized.Get_ById!.Walk_In_Insp_Name = Walk_Type;
                }


            }
            return PartialView(deserialized.Get_ById);
        }
        public async Task<IActionResult> P_Insp_Leader_Request_GetbyId(string ID)
        {
            M_Insp_Leader_Request _UNIT = new M_Insp_Leader_Request
            {
                Insp_Request_Id = ID
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Leader_Request_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Leader_Request deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Leader_Request>(customerJsonString)!;
            DateTime StartDate = Convert.ToDateTime(deserialized.Get_ById!.Inspection_Date);
            deserialized.Get_ById!.Inspection_Date = StartDate.ToString("yyyy-MM-dd");
            return PartialView(deserialized);
        }
        public async Task<IActionResult> Leader_Insp_Zone_based_Dir_Hsse(string Zone_Id)
        {
            M_Insp_Leader_Request _UNIT = new M_Insp_Leader_Request
            {
                Zone_Id = Zone_Id
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Leader_Zone_based_Dir_Hsse", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Leader_Request deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Leader_Request>(customerJsonString)!;
            return Json(deserialized.Get_ById);
        }
        public async Task<IActionResult> Leader_Insp_Request_Add([FromBody] M_Insp_Leader_Request model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Leader_Request_Add";
                    Login_ LoginClass = GetLoginDetails();

                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    //if (model.Insp_Request_Id != "0")
                    //{
                    //    URL = "InspectionForm/Insp_Leader_Request_Update";
                    //}
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    deserialized.Remarks = model.Walk_In_Insp_Name;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Leader_Request_ApprovalReject(M_Insp_Joint_Request_Approval model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Leader_Req_ApprovalReject";
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
        public async Task<IActionResult> P_Insp_Leader_Find_AddView(string ID, string Walk_Type)
        {
            Login_ LoginClass = GetLoginDetails();
            M_Insp_Leader_Request _UNIT = new M_Insp_Leader_Request
            {
                Insp_Request_Id = ID
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Leader_Request_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Leader_Request deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Leader_Request>(customerJsonString)!;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Insp_Request_Id = ID;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Login_Id = LoginClass.Employee_Identity_Id;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.NCM_Supervisor_Text = deserialized.Get_ById!.Zone_Director_Name;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Service_Provider_Name = deserialized.Get_ById!.HSSE_Director_Name;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Insp_Type_Name = deserialized.Get_ById!.Insp_Category_Name;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Other_Attendees = deserialized.Get_ById!.Other_Attendees;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Schedule_Type = deserialized.Get_ById!.Schedule_Type;
            DateTime L_StartDates = Convert.ToDateTime(deserialized.Get_ById!.Inspection_Date);
            TempData["L_msg_Insp_Date"] = L_StartDates.ToString("yyyy-MM-dd");
            TempData["msg"] = deserialized!.MESSAGE;
            return PartialView(deserialized.Get_ById!.Insp_Spot_Finding_List);
        }
        public async Task<IActionResult> P_Insp_Leader_Find_Walkin_AddView(string ID, string Walk_Type)
        {
            Login_ LoginClass = GetLoginDetails();

            M_Insp_Leader_Request _UNIT = new M_Insp_Leader_Request
            {
                Insp_Request_Id = ID,
                Zone_Id = LoginClass.Zone_Id
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Leader_Request_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Leader_Request deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Leader_Request>(customerJsonString)!;
            DateTime StartDate = Convert.ToDateTime(deserialized.Get_ById!.Inspection_Date);
            deserialized.Get_ById!.Inspection_Date = StartDate.ToString("yyyy-MM-dd");
            if (deserialized.Get_ById.Insp_Request_Id == "0")
            {
                deserialized.Get_ById.Business_Unit_Id = LoginClass.Business_Unit_Id;
                deserialized.Get_ById.Zone_Id = LoginClass.Zone_Id;
                DateTime AddDate = DateTime.Today;
                deserialized.Get_ById!.Inspection_Date = AddDate.ToString("dd-MMM-yyyy");
                deserialized.Get_ById!.Walk_In_Insp_Name = Walk_Type;
            }
            return PartialView(deserialized.Get_ById);
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Leader_Finding_Add([FromBody] M_Insp_Spot_Finding model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    Login_ LoginClass = GetLoginDetails();

                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    if (model.Insp_Request_Id != "0")
                    {
                        URL = "InspectionForm/Insp_Leader_Finding_Add";
                    }
                    if (URL != "")
                    {
                        HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                        string customerJsonString = await response.Content.ReadAsStringAsync();
                        RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                        return Json(deserialized!.STATUS_CODE);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Leader_Find_ApprovalReject(M_Insp_Finding_Approval model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Leader_Find_ApprovalReject";
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
        public async Task<IActionResult> Insp_Leader_Sub_Find_Photo_Delete(string Insp_Sub_Photo_Id)
        {
            using (client)
            {
                M_Insp_Spot_Find_Sub_Photo _UNIT = new M_Insp_Spot_Find_Sub_Photo
                {
                    Insp_Sub_Photo_Id = Insp_Sub_Photo_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Leader_Sub_Find_Photo_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Spot_Find_Sub_Photo_List deserialized = JsonConvert.DeserializeObject<M_Get_Spot_Find_Sub_Photo_List>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Leader_Corrective_Action_Add(List<M_Insp_Spot_Corrective_Action> model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Leader_Corrective_Action_Add";
                    Login_ LoginClass = GetLoginDetails();
                    foreach (var item in model)
                    {
                        item.CreatedBy = LoginClass.Employee_Identity_Id;
                    }
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
        public IActionResult Insp_Leader_Corrective_Action()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Leader_CA_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            M_Get_Insp_Leader_Request deserialized = new M_Get_Insp_Leader_Request();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Leader_Corrective_Action_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Leader_Request>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Insp_Leader_Request>();
            }
            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList(),
            });
        }
        public async Task<IActionResult> P_Insp_Leader_CA_GetbyId(string ID)
        {
            Login_ LoginClass = GetLoginDetails();

            M_Insp_Leader_Request _UNIT = new M_Insp_Leader_Request
            {
                Insp_Request_Id = ID,
                CreatedBy = LoginClass.Employee_Identity_Id,
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Leader_CA_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Leader_Request deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Leader_Request>(customerJsonString)!;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.NCM_Supervisor_Text = deserialized.Get_ById!.Zone_Director_Name;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Service_Provider_Name = deserialized.Get_ById!.HSSE_Director_Name;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Insp_Type_Name = deserialized.Get_ById!.Insp_Category_Name;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Other_Attendees = deserialized.Get_ById!.Other_Attendees;
            deserialized.Get_ById!.Insp_Spot_Finding_List!.Schedule_Type = deserialized.Get_ById!.Schedule_Type;
            return PartialView(deserialized.Get_ById!.Insp_Spot_Finding_List);
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Leader_Action_Closure_Add(M_Insp_Spot_Corrective_Action model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Leader_CA_Action_Closure_Add";
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
        [HttpPost]
        public async Task<IActionResult> Insp_Leader_Corrective_Action_Reassign(M_Insp_Spot_Corrective_Action model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Leader_CA_ReAssign";
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
        [HttpPost]
        public async Task<IActionResult> Insp_Leader_CA_Photo_Delete(string Insp_CA_Photo_Id)
        {
            using (client)
            {
                M_Insp_Spot_CA_Photo _UNIT = new M_Insp_Spot_CA_Photo
                {
                    Insp_CA_Photo_Id = Insp_CA_Photo_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Leader_CA_Photo_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Spot_CA_Photo_List deserialized = JsonConvert.DeserializeObject<M_Get_Spot_CA_Photo_List>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Leader_CA_Approval_Add(string Corrective_Action_Id, string Remarks, string Status, string Description)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    Login_ LoginClass = GetLoginDetails();

                    M_Insp_Spot_Corrective_Action _UNIT = new M_Insp_Spot_Corrective_Action
                    {
                        Corrective_Action_Id = Corrective_Action_Id,
                        Remarks = Remarks,
                        CreatedBy = LoginClass.Employee_Identity_Id,
                        Status = Status,
                        Description = Description,
                    };
                    string URL = "InspectionForm/Insp_Leader_CA_ApprovalReject";
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
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

        [HttpPost]
        public async Task<IActionResult> Insp_Leader_WalkIn_Finding_Add([FromBody] M_Insp_Leader_Request model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    Login_ LoginClass = GetLoginDetails();

                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    URL = "InspectionForm/Add_Insp_Leader_WalkIn_Finding";

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
        [HttpPost]
        public async Task<IActionResult> Insp_Leader_Find_Photo_Delete(string Insp_Leader_Sub_Photo_Id)
        {
            using (client)
            {
                M_Insp_Leader_Finding_Sub_Photo _UNIT = new M_Insp_Leader_Finding_Sub_Photo
                {
                    Insp_Leader_Sub_Photo_Id = Insp_Leader_Sub_Photo_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Leader_Find_Photo_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Spot_CA_Photo_List deserialized = JsonConvert.DeserializeObject<M_Get_Spot_CA_Photo_List>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        #endregion

        #region [ADVISORY NOTICE INSPECTION]
        public IActionResult Complaint_Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Complaint_Register_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            M_Get_Complaint_Register deserialized = new M_Get_Complaint_Register();
            Login_ LoginClass = GetLoginDetails();
            model.CreatedBy = LoginClass.Employee_Identity_Id;

            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Complaint_Register_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Complaint_Register>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Complaint_Register>();
            }

            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList(),
            });
        }
        public async Task<IActionResult> P_Complaint_Register_Add_View(string ID, string Walk_In_Insp)
        {
            Login_ LoginClass = GetLoginDetails();
            M_Complaint_Register _UNIT = new M_Complaint_Register
            {
                Complaint_Id = ID,
                Zone_Id = LoginClass.Zone_Id
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Complaint_Register_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Complaint_Register deserialized = JsonConvert.DeserializeObject<M_Get_Complaint_Register>(customerJsonString)!;
            DateTime StartDate = Convert.ToDateTime(deserialized.Get_ById!.Date_Complaint);
            deserialized.Get_ById!.Date_Complaint = StartDate.ToString("yyyy-MM-dd");
            if (deserialized.Get_ById.Complaint_Id == "0")
            {
                deserialized.Get_ById.Business_Unit_Id = LoginClass.Business_Unit_Id;
                deserialized.Get_ById.Zone_Id = LoginClass.Zone_Id;
                deserialized.Get_ById.Walk_In_Insp_Name = Walk_In_Insp;
            }
            if (deserialized.Get_ById.Status == "Service Request Pending" || deserialized.Get_ById.Status == "Assessment Pending")
            {
                deserialized.Get_ById!.Date_Complaint = StartDate.ToString("dd-MMM-yyyy");
            }
            if (deserialized.Get_ById.Status == "Complaint Review Pending")
            {
                DateTime StartDateT = Convert.ToDateTime(deserialized.Get_ById!.Target_Date);
                deserialized.Get_ById!.Date_Complaint = StartDate.ToString("dd-MMM-yyyy");
                deserialized.Get_ById!.Target_Date = StartDateT.ToString("dd-MMM-yyyy");
            }
            return PartialView(deserialized.Get_ById);
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Complaint_Register_Add([FromBody] M_Complaint_Register model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Complaint_Register_Add";
                    Login_ LoginClass = GetLoginDetails();

                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    //if (model.Complaint_Id != "0")
                    //{
                    //    URL = "InspectionForm/Insp_Request_Update";
                    //}
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    deserialized.Remarks = model.Walk_In_Insp_Name;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Complaint_Service_Request_Add(M_Complaint_Register model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Complaint_Service_Request_Add";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Complaint_Assessment_Add(M_Complaint_Register model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Complaint_Assessment_Add";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> Advisory_Notice_Report_byId(string ID)
        {
            Login_ LoginClass = GetLoginDetails();
            M_Complaint_Register _UNIT = new M_Complaint_Register
            {
                Complaint_Id = ID,
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Pre_Advisory_Notice_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Complaint_Register deserialized = JsonConvert.DeserializeObject<M_Get_Complaint_Register>(customerJsonString)!;
            DateTime StartDate = Convert.ToDateTime(deserialized.Get_ById!.Date_Complaint);
            deserialized.Get_ById!.Date_Complaint = StartDate.ToString("yyyy-MM-dd");
            return Json(deserialized.Get_ById);
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Pre_Sub_Advisory_Notice_Report_Add([FromBody] M_Complaint_Register model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Pre_Sub_Advisory_Notice_Report_Add";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Pre_Advisory_Notice_Report_Add([FromBody] M_Complaint_Register model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Pre_Advisory_Notice_Report_Add";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Pre_Sub_Advisory_Notice_No_Add(M_Complaint_Register model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Pre_Sub_Advisory_Notice_No_Add";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Advisory_Notice_Final_Add(M_Complaint_Register model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Advisory_Notice_Final_Add";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }
        #endregion

        #region[AUDIT_INSPECTION_SCHEDULE]
        public IActionResult Create_Audit_Insp_Sch()
        {
            return View();
        }

        public async Task<IActionResult> Get_All_Building_Load(string Zone_Id, string Community_Id)
        {
            using (client)
            {
                Insp_Audit_Building_Model _UNIT = new Insp_Audit_Building_Model
                {
                    Zone_Id = Zone_Id,
                    Community_Id = Community_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Get_All_Building_Load", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Audit_Insp_Sch deserialized = JsonConvert.DeserializeObject<Get_Audit_Insp_Sch>(customerJsonString)!;
                return Json(deserialized!.Get_All_Sub);
            }
        }

        public async Task<IActionResult> Get_All_Audit_Insp_Emp()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("InspectionForm/Get_All_Audit_Insp_Emp").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Audit_Insp_Sch deserialized = JsonConvert.DeserializeObject<Get_Audit_Insp_Sch>(customerJsonString)!;
                return Json(deserialized!.Get_All_Emp);
            }
        }

        public async Task<IActionResult> Get_Building_Base_Sch_Load(string Sub_Building_Id)
        {
            using (client)
            {
                Insp_Audit_Building_Model _UNIT = new Insp_Audit_Building_Model
                {
                    Sub_Building_Id = Sub_Building_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Get_Building_Base_Sch_Load", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Audit_Insp_Sch deserialized = JsonConvert.DeserializeObject<Get_Audit_Insp_Sch>(customerJsonString)!;
                return Json(deserialized!.Get_All_List);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Audit_Insp_Sch_Row_Submit([FromBody] Insp_Sch_Calendar_List model)
        {
            using (client)
            {
                //string URL = "InspectionForm/Add_Audit_Insp_Sch_Row_Submit";
                string URL = "InspectionForm/Add_Audit_Insp_Sch_Table_Submit";
                
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized.STATUS_CODE);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Audit_Insp_Sch_Table_Submit([FromBody] Audit_Insp_Table_List model)
        {
            using (client)
            {
                string URL = "InspectionForm/Add_Audit_Insp_Sch_Table_Submit";
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized.STATUS_CODE);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Add_Audit_Insp_Sch_Table_Submit_Demo([FromBody] Insp_Sch_Calendar_List model)
        {
            using (client)
            {
                string URL = "InspectionForm/Add_Audit_Insp_Sch_Table_Submit";
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized.STATUS_CODE);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Insp_Audit_Edit_Row(int Insp_Request_Id, string TypeId)
        {
            using (client)
            {
                Audit_Insp_Row_Submit _UNIT = new Audit_Insp_Row_Submit
                {
                    Insp_Request_Id = Convert.ToString(Insp_Request_Id),
                    Type_Id = TypeId
                };

                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Audit_Edit_Row", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Insp_Audit_Edit deserialized = JsonConvert.DeserializeObject<Get_Insp_Audit_Edit>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update_Insp_Sch_Row_Submit([FromBody] Update_Audit_Insp_Row_Submit model)
        {
            using (client)
            {
                string URL = "InspectionForm/Update_Insp_Sch_Row_Submit";
                HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized.STATUS_CODE);

            }
        }

        public async Task<IActionResult> LoadAll_Service_Provider(string Community_Id)
        {
            using (client)
            {
                Insp_Audit_Building_Model _UNIT = new Insp_Audit_Building_Model
                {
                    Community_Id = Community_Id
                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/LoadAll_Service_Provider", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Serviceprovider deserialized = JsonConvert.DeserializeObject<Get_Serviceprovider>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadAllScope()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("MasterCommunity/Scope_of_Work_Master_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Scope_of_Work deserialized = JsonConvert.DeserializeObject<Get_Scope_of_Work>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadAllCategory()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("InspectionMaster/Insp_Category_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Category deserialized = JsonConvert.DeserializeObject<Get_Category>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        #endregion

        #region [LANDSCAPE INSPECTION]

        #region [LANDSCAPE INSPECTION FORM]
        public IActionResult Landscape_Inspection()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Landscape_Inspection_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            M_Get_Insp_Landscape deserialized = new M_Get_Insp_Landscape();
            Login_ LoginClass = GetLoginDetails();

            model.CreatedBy = LoginClass.Employee_Identity_Id;
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Landscape_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscape>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Insp_Landscape>();
            }
            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList(),
            });
        }
        public async Task<IActionResult> P_Landscape_Inspection_AddView(string ID)
        {
            Login_ LoginClass = GetLoginDetails();

            M_Insp_Landscape _UNIT = new M_Insp_Landscape
            {
                Insp_Request_Id = ID,
                Zone_Id = LoginClass.Zone_Id
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Landscape_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Landscape deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscape>(customerJsonString)!;
            DateTime StartDate = Convert.ToDateTime(deserialized.Get_ById!.Inspection_Date);
            deserialized.Get_ById!.Inspection_Date = StartDate.ToString("yyyy-MM-dd");
            if (deserialized.Get_ById.Insp_Request_Id == "0")
            {
                deserialized.Get_ById.Business_Unit_Id = LoginClass.Business_Unit_Id;
                deserialized.Get_ById.Zone_Id = LoginClass.Zone_Id;
                deserialized.Get_ById.CreatedBy_Name = LoginClass.First_Name;
                DateTime AddDate = DateTime.Today;
                //deserialized.Get_ById!.Inspection_Date = AddDate.ToString("dd-MMM-yyyy");
            }
            return PartialView(deserialized.Get_ById);
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Landscape_Add([FromBody] M_Insp_Landscape model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Landscape_Add";
                    Login_ LoginClass = GetLoginDetails();

                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    if (model.Insp_Request_Id != "0")
                    {
                        URL = "InspectionForm/Insp_Landscape_Update";
                    }
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized.STATUS_CODE);
                }
            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> P_Edit_Landscape_Inspection(string ID)
        {
            Login_ LoginClass = GetLoginDetails();

            M_Insp_Landscape _UNIT = new M_Insp_Landscape
            {
                Insp_Request_Id = ID,
                Zone_Id = LoginClass.Zone_Id,
                CreatedBy = LoginClass.Employee_Identity_Id,
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Landscape_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Landscape deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscape>(customerJsonString)!;
            return PartialView(deserialized.Get_ById);
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Landscape_CA_Add([FromBody] M_Insp_Landscape model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Landscape_CA_Add";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized.STATUS_CODE);
                }
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> Insp_Land_ZoneWise_All_Emp_GetbyId(string Zone_Id)
        {
            using (client)
            {
                M_Insp_Master_List _UNIT = new M_Insp_Master_List
                {
                    Value = Zone_Id

                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Land_ZoneWise_All_Emp_GetbyId", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Emp_Master_List deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Emp_Master_List>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Insp_Land_Service_Provider_Company_GetbyId(string Sub_Building_Id)
        {
            using (client)
            {
                M_Insp_Master_List _UNIT = new M_Insp_Master_List
                {
                    Value = Sub_Building_Id

                };
                HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Land_Service_Provider_Company_GetbyId", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Insp_Emp_Master_List deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Emp_Master_List>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        #endregion

        #region [LANDSCAPE CA INSPECTION FORM]
        public IActionResult Landscape_CA_Insp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Landscape_CA_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            M_Get_Insp_Landscape deserialized = new M_Get_Insp_Landscape();
            Login_ LoginClass = GetLoginDetails();

            model.CreatedBy = LoginClass.Employee_Identity_Id;
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Landscape_CA_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscape>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Insp_Landscape>();
            }
            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Insp_Landscape_CA_Closue_Add([FromBody] Insp_Landscape_Qns model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Landscape_CA_Closue_Add";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> P_Edit_CA_Landscape_Insp(string ID)
        {
            Login_ LoginClass = GetLoginDetails();

            M_Insp_Landscape _UNIT = new M_Insp_Landscape
            {
                Insp_Request_Id = ID,
                Zone_Id = LoginClass.Zone_Id,
                CreatedBy = LoginClass.Employee_Identity_Id,
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_Landscape_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Landscape deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscape>(customerJsonString)!;
            return PartialView(deserialized.Get_ById);
        }

        [HttpPost]
        public async Task<IActionResult> Insp_Landscape_CA_Approval([FromBody] Insp_Landscape_Qns model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Landscape_CA_Approval";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insp_Landscape_CA_Multi_Reject([FromBody] Insp_Landscape_Qns model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Landscape_CA_Multi_Reject";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_Landscape_CA_Final_Approve([FromBody] M_Insp_Landscape model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_Landscape_CA_Final_Approve";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }

        #endregion


        #endregion

        #region [SOFT SERVICE INSPECTION]

        #region [SOFT SERVICE INSPECTION FORM]
        public IActionResult SoftService_Inspection()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SoftService_Inspection_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            M_Get_Insp_Landscape deserialized = new M_Get_Insp_Landscape();
            Login_ LoginClass = GetLoginDetails();

            model.CreatedBy = LoginClass.Employee_Identity_Id;
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_SoftService_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscape>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Insp_Landscape>();
            }
            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList(),
            });
        }
        public async Task<IActionResult> P_SoftService_Inspection_AddView(string ID)
        {
            Login_ LoginClass = GetLoginDetails();

            M_Insp_Landscape _UNIT = new M_Insp_Landscape
            {
                Insp_Request_Id = ID,
                Zone_Id = LoginClass.Zone_Id
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_SoftService_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Landscape deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscape>(customerJsonString)!;
            DateTime StartDate = Convert.ToDateTime(deserialized.Get_ById!.Inspection_Date);
            deserialized.Get_ById!.Inspection_Date = StartDate.ToString("yyyy-MM-dd");
            if (deserialized.Get_ById.Insp_Request_Id == "0")
            {
                deserialized.Get_ById.Business_Unit_Id = LoginClass.Business_Unit_Id;
                deserialized.Get_ById.Zone_Id = LoginClass.Zone_Id;
                deserialized.Get_ById.CreatedBy_Name = LoginClass.First_Name;
                DateTime AddDate = DateTime.Today;
                //deserialized.Get_ById!.Inspection_Date = AddDate.ToString("dd-MMM-yyyy");
            }
            return PartialView(deserialized.Get_ById);
        }
        [HttpPost]
        public async Task<IActionResult> Insp_SoftService_Add([FromBody] M_Insp_Landscape model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_SoftService_Add";
                    Login_ LoginClass = GetLoginDetails();

                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    if (model.Insp_Request_Id != "0")
                    {
                        URL = "InspectionForm/Insp_SoftService_Update";
                    }
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized.STATUS_CODE);
                }
            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> P_Edit_SoftService_Inspection(string ID)
        {
            Login_ LoginClass = GetLoginDetails();

            M_Insp_Landscape _UNIT = new M_Insp_Landscape
            {
                Insp_Request_Id = ID,
                Zone_Id = LoginClass.Zone_Id,
                CreatedBy = LoginClass.Employee_Identity_Id,
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_SoftService_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Landscape deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscape>(customerJsonString)!;
            return PartialView(deserialized.Get_ById);
        }
        [HttpPost]
        public async Task<IActionResult> Insp_SoftService_CA_Add([FromBody] M_Insp_Landscape model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_SoftService_CA_Add";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized.STATUS_CODE);
                }
            }
            else
            {
                return NoContent();
            }
        }

        #endregion


        #region SOFT SERVICE CA INSPECTION FORM]
        public IActionResult SoftService_CA_Insp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insp_SoftService_CA_GetAll([FromBody] DataTableAjaxPostModel model)
        {
            var data = new List<object>();
            M_Get_Insp_Landscape deserialized = new M_Get_Insp_Landscape();
            Login_ LoginClass = GetLoginDetails();

            model.CreatedBy = LoginClass.Employee_Identity_Id;
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_SoftService_CA_GetAll", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscape>(customerJsonString)!;
            if (deserialized.STATUS_CODE == "404")
            {
                deserialized.Get_All = new List<M_Insp_Landscape>();
            }
            return Json(new
            {
                draw = model.Draw,
                recordsTotal = deserialized.RecordsTotal,
                recordsFiltered = deserialized.RecordsFiltered,
                data = deserialized.Get_All!.ToList(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Insp_SoftService_CA_Closue_Add([FromBody] Insp_Landscape_Qns model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_SoftService_CA_Closue_Add";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> P_Edit_CA_SoftService_Insp(string ID)
        {
            Login_ LoginClass = GetLoginDetails();

            M_Insp_Landscape _UNIT = new M_Insp_Landscape
            {
                Insp_Request_Id = ID,
                Zone_Id = LoginClass.Zone_Id,
                CreatedBy = LoginClass.Employee_Identity_Id,
            };
            HttpResponseMessage response = client.PostAsync("InspectionForm/Insp_SoftService_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            M_Get_Insp_Landscape deserialized = JsonConvert.DeserializeObject<M_Get_Insp_Landscape>(customerJsonString)!;
            return PartialView(deserialized.Get_ById);
        }

        [HttpPost]
        public async Task<IActionResult> Insp_SoftService_CA_Approval([FromBody] Insp_Landscape_Qns model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_SoftService_CA_Approval";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insp_SoftService_CA_Multi_Reject([FromBody] Insp_Landscape_Qns model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_SoftService_CA_Multi_Reject";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insp_SoftService_CA_Final_Approve([FromBody] M_Insp_Landscape model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "InspectionForm/Insp_SoftService_CA_Final_Approve";
                    Login_ LoginClass = GetLoginDetails();
                    model.CreatedBy = LoginClass.Employee_Identity_Id;
                    HttpResponseMessage response = client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE_ADD deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE_ADD>(customerJsonString)!;
                    return Json(deserialized);
                }
            }
            else
            {
                return NoContent();
            }
        }

        #endregion


        #endregion

    }
}
