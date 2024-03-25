using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Models.IncidentMaster;
using Nakheel_Web.Models.IncidentReport;
using Nakheel_Web.Models.Masters;
using Newtonsoft.Json;
using System.Diagnostics.Contracts;
using System.Net.Http.Headers;
using System.Text;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using System.Text.RegularExpressions;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models.AccountsMaster;
using static Nakheel_Web.Authentication.Common;
using Microsoft.AspNetCore.Authorization;
using Nakheel_Web.Models;
//using System.Web.Security;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class MasterController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private string conn;        

        public MasterController(IConfiguration configuration)
        {
            this.configuration = configuration;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            conn = configuration.GetConnectionString("DefaultConnection");
        }

        #region[BUSINESS UNIT]
        public async Task<IActionResult> Business_Unit()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync(conn + "BusinessUnitMaster/Business_Unit_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_BUSINESS_UNIT deserialized = JsonConvert.DeserializeObject<GET_BUSINESS_UNIT>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBusinessUnit(BUSINESS_UNIT model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Business_Unit_Id == "0")
                    {
                        URL = "BusinessUnitMaster/Business_Unit_Add";
                    }
                    else
                    {
                        URL = "BusinessUnitMaster/Business_Unit_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(conn + URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Business_Unit");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> BU_GetByID(string ID)
        {

            using (client)
            {
                BUSINESS_UNIT _UNIT = new BUSINESS_UNIT
                {
                    Business_Unit_Id = ID

                };
                HttpResponseMessage response = client.PostAsync(conn + "BusinessUnitMaster/Business_Unit_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_BUSINESS_UNIT deserialized = JsonConvert.DeserializeObject<GET_BUSINESS_UNIT>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> BU_Delete(string ID)
        {

            using (client)
            {
                BUSINESS_UNIT _UNIT = new BUSINESS_UNIT
                {
                    Business_Unit_Id = ID

                };
                HttpResponseMessage response = client.PostAsync(conn + "BusinessUnitMaster/Business_Unit_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_BUSINESS_UNIT deserialized = JsonConvert.DeserializeObject<GET_BUSINESS_UNIT>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        #endregion

        #region[ZONE MANAGEMENT]
        public async Task<IActionResult> Zone_Management()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync(conn + "ZoneMaster/Zone_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ZONE_MANAGEMENT deserialized = JsonConvert.DeserializeObject<GET_ZONE_MANAGEMENT>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddZoneManagement(ZONE_MANAGEMENT model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Zone_Id == "0")
                    {
                        URL = "ZoneMaster/Zone_Add";
                    }
                    else
                    {
                        URL = "ZoneMaster/Zone_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(conn + URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["Zonemsg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Zone_Management");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> ZONE_GetByID(string ID)
        {

            using (client)
            {
                ZONE_MANAGEMENT _UNIT = new ZONE_MANAGEMENT
                {
                    Zone_Id = ID

                };
                HttpResponseMessage response = client.PostAsync(conn + "ZoneMaster/Zone_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ZONE_MANAGEMENT deserialized = JsonConvert.DeserializeObject<GET_ZONE_MANAGEMENT>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> ZONE_Delete(string ID)
        {
            using (client)
            {
                ZONE_MANAGEMENT _UNIT = new ZONE_MANAGEMENT
                {
                    Zone_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "ZoneMaster/Zone_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ZONE_MANAGEMENT deserialized = JsonConvert.DeserializeObject<GET_ZONE_MANAGEMENT>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region[COMMUNITY MANAGEMENT]
        public async Task<IActionResult> Community_Management()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync(conn + "CommunityMaster/Community_Master_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_COMMUNITY_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_COMMUNITY_MANAGEMNT>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCommunity(COMMUNITY_MANAGEMNT model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Community_Master_Id == "0")
                    {
                        URL = "CommunityMaster/Community_Master_Add";
                    }
                    else
                    {
                        URL = "CommunityMaster/Community_Master_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(conn + URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Community_Management");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> Community_Master_GetByID(string ID)
        {

            using (client)
            {
                COMMUNITY_MANAGEMNT _UNIT = new COMMUNITY_MANAGEMNT
                {
                    Community_Master_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "CommunityMaster/Community_Master_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_COMMUNITY_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_COMMUNITY_MANAGEMNT>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        public async Task<IActionResult> Community_Master_Delete(string ID)
        {

            using (client)
            {
                COMMUNITY_MANAGEMNT _UNIT = new COMMUNITY_MANAGEMNT
                {
                    Community_Master_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "CommunityMaster/Community_Master_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_COMMUNITY_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_COMMUNITY_MANAGEMNT>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        #endregion

        #region [BUILDING MANAGEMENT]
        public async Task<IActionResult> Building_Management()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync(conn + "BuildingMaster/Building_Master_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_BUILDING_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_BUILDING_MANAGEMNT>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddBuilding([FromBody] BUILDING_MANAGMENT _UNIT)
        {
            using (client)
            {
                string URL = "";
                if (_UNIT.Building_Id == "0")
                {
                    URL = "BuildingMaster/Building_Master_Add";
                }
                else
                {
                    URL = "BuildingMaster/Building_Master_Update";
                }
                HttpResponseMessage response = client.PostAsync(conn + URL, new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_BUILDING_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_BUILDING_MANAGEMNT>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        public async Task<IActionResult> Building_Master_GetByID(string ID)
        {

            using (client)
            {
                BUILDING_MANAGMENT _UNIT = new BUILDING_MANAGMENT
                {
                    Building_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "BuildingMaster/Building_Master_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_BUILDING_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_BUILDING_MANAGEMNT>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        public async Task<IActionResult> Building_Master_Delete(string ID)
        {

            using (client)
            {
                BUILDING_MANAGMENT _UNIT = new BUILDING_MANAGMENT
                {
                    Building_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "BuildingMaster/Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_BUILDING_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_BUILDING_MANAGEMNT>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        public async Task<IActionResult> Sub_Building_Master_Delete(string ID)
        {

            using (client)
            {
                M_Building_List _UNIT = new M_Building_List
                {
                    Sub_Building_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "BuildingMaster/Sub_Building_Master_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_BUILDING_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_BUILDING_MANAGEMNT>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        #endregion

        #region [MASTER COMMUNITY MANAGEMENT]
        public async Task<IActionResult> Master_Community_Management()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync(conn + "MasterCommunity/MasterCommunity_Master_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_MASTER_COMMUNITY_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_MASTER_COMMUNITY_MANAGEMNT>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
            //return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMasterCommunity([FromBody] MASTER_COMMUNITY_MANAGEMNT _UNIT)
        {
            using (client)
            {
                string URL = "";
                if (_UNIT.MasterCommunity_Id == "0")
                {
                    URL = "MasterCommunity/MasterCommunity_Master_Add";
                }
                else
                {
                    URL = "MasterCommunity/MasterCommunity_Master_Update";
                }
                HttpResponseMessage response = client.PostAsync(conn + URL, new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_MASTER_COMMUNITY_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_MASTER_COMMUNITY_MANAGEMNT>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        public async Task<IActionResult> Master_Community_GetByID(string ID)
        {

            using (client)
            {
                MASTER_COMMUNITY_MANAGEMNT _UNIT = new MASTER_COMMUNITY_MANAGEMNT
                {
                    MasterCommunity_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "MasterCommunity/Master_Community_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_MASTER_COMMUNITY_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_MASTER_COMMUNITY_MANAGEMNT>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        public async Task<IActionResult> Master_Community_Delete(string ID)
        {
            using (client)
            {
                MASTER_COMMUNITY_MANAGEMNT _UNIT = new MASTER_COMMUNITY_MANAGEMNT
                {
                    MasterCommunity_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "MasterCommunity/Master_Community_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_MASTER_COMMUNITY_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_MASTER_COMMUNITY_MANAGEMNT>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        public async Task<IActionResult> Sub_Master_Community_Delete(string ID)
        {
            using (client)
            {
                M_MasterCommunity_List _UNIT = new M_MasterCommunity_List
                {
                    Sub_MasterCommunity_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "MasterCommunity/Sub_Master_Community_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_MASTER_COMMUNITY_MANAGEMNT deserialized = JsonConvert.DeserializeObject<GET_MASTER_COMMUNITY_MANAGEMNT>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        #endregion

        #region[ROLE MASTER]
        public async Task<IActionResult> Role_Master()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync(conn + "RoleMaster/Role_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ROLE_MASTER deserialized = JsonConvert.DeserializeObject<GET_ROLE_MASTER>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleMaster(M_Role_Master model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Role_Id == "0")
                    {
                        URL = "RoleMaster/Role_Add";
                    }
                    else
                    {
                        URL = "RoleMaster/Role_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(conn + URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Role_Master");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> Role_GetByID(string ID)
        {

            using (client)
            {
                M_Role_Master _UNIT = new M_Role_Master
                {
                    Role_Id = ID

                };
                HttpResponseMessage response = client.PostAsync(conn + "RoleMaster/Role_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ROLE_MASTER deserialized = JsonConvert.DeserializeObject<GET_ROLE_MASTER>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> Role_Delete(string ID)
        {

            using (client)
            {
                M_Role_Master _UNIT = new M_Role_Master
                {
                    Role_Id = ID

                };
                HttpResponseMessage response = client.PostAsync(conn + "RoleMaster/Role_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ROLE_MASTER deserialized = JsonConvert.DeserializeObject<GET_ROLE_MASTER>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        #endregion

        #region[DEPARTMENT MASTER]
        public async Task<IActionResult> Department()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync(conn + "DepartmentMaster/Department_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_DEPARTMENT_MASTER deserialized = JsonConvert.DeserializeObject<GET_DEPARTMENT_MASTER>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDepartmentMaster(M_Department_Master model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Department_Id == "0")
                    {
                        URL = "DepartmentMaster/Department_Add";
                    }
                    else
                    {
                        URL = "DepartmentMaster/Department_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(conn + URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Department");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> Department_GetByID(string ID)
        {

            using (client)
            {
                M_Department_Master _UNIT = new M_Department_Master
                {
                    Department_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "DepartmentMaster/Department_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_DEPARTMENT_MASTER deserialized = JsonConvert.DeserializeObject<GET_DEPARTMENT_MASTER>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> Department_Delete(string ID)
        {

            using (client)
            {
                M_Department_Master _UNIT = new M_Department_Master
                {
                    Department_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "DepartmentMaster/Department_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_DEPARTMENT_MASTER deserialized = JsonConvert.DeserializeObject<GET_DEPARTMENT_MASTER>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        #endregion

        #region[DESIGNATION MASTER]
        public async Task<IActionResult> Designation()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync(conn + "DesignationMaster/Designation_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_DESIGNATION_MASTER deserialized = JsonConvert.DeserializeObject<GET_DESIGNATION_MASTER>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDesignationMaster(M_Designation_Master model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Designation_Id == "0")
                    {
                        URL = "DesignationMaster/Designation_Add";
                    }
                    else
                    {
                        URL = "DesignationMaster/Designation_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(conn + URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["msg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Designation");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> Designation_GetByID(string ID)
        {

            using (client)
            {
                M_Designation_Master _UNIT = new M_Designation_Master
                {
                    Designation_Id = ID

                };
                HttpResponseMessage response = client.PostAsync(conn + "DesignationMaster/Designation_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_DESIGNATION_MASTER deserialized = JsonConvert.DeserializeObject<GET_DESIGNATION_MASTER>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> Designation_Delete(string ID)
        {

            using (client)
            {
                M_Designation_Master _UNIT = new M_Designation_Master
                {
                    Designation_Id = ID

                };
                HttpResponseMessage response = client.PostAsync(conn + "DesignationMaster/Designation_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_DESIGNATION_MASTER deserialized = JsonConvert.DeserializeObject<GET_DESIGNATION_MASTER>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        #endregion

        #region[EMPLOYEE MASTER]
        public async Task<IActionResult> Employee_Management()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync(conn + "EmployeeMaster/Employee_Master_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_EMPLOYEE_MASTER deserialized = JsonConvert.DeserializeObject<GET_EMPLOYEE_MASTER>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployeeMaster([FromBody] M_Employee_Master _UNIT)
        {
            using (client)
            {
                string URL = "";
                if (_UNIT.Employee_Identity_Id == "0")
                {
                    URL = "EmployeeMaster/Employee_Master_Add";
                }
                else
                {
                    URL = "EmployeeMaster/Employee_Master_Update";
                }
                HttpResponseMessage response = client.PostAsync(conn + URL, new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_EMPLOYEE_MASTER deserialized = JsonConvert.DeserializeObject<GET_EMPLOYEE_MASTER>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public JsonResult CreatePassword()
        {
            int length = 10;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            var password = res.ToString();
            return Json(password);
        }
        public async Task<IActionResult> Employee_Master_GetByID(string ID)
        {
            using (client)
            {
                M_Employee_Master _UNIT = new M_Employee_Master
                {
                    Employee_Identity_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "EmployeeMaster/Employee_Master_GetByID", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_EMPLOYEE_MASTER deserialized = JsonConvert.DeserializeObject<GET_EMPLOYEE_MASTER>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        public async Task<IActionResult> Employee_Master_Delete(string ID)
        {
            using (client)
            {
                M_Employee_Master _UNIT = new M_Employee_Master
                {
                    Employee_Identity_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "EmployeeMaster/Employee_Master_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_EMPLOYEE_MASTER deserialized = JsonConvert.DeserializeObject<GET_EMPLOYEE_MASTER>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }
        #endregion

        #region[SERVICE PROVIDER CONTRACTOR MANAGEMENT]
        public IActionResult Service_Provider_Contractor_Management()
        {
            return View();
        }
        public async Task<IActionResult> SP_Sign_Up_List(string id)
        {
            using (client)
            {
                //HttpContext.Session.SetString("Sign_Up_Card_Id", id);
                //var Z_str = HttpContext.Session.GetString("Sign_Up_Card_Id");
                Login_ LoginClass = GetLoginDetails();
                ServiceProviderSignUp _UNIT;
                if (LoginClass.Role_Id == "5" || LoginClass.Role_Id == "2")
                {
                    _UNIT = new()
                    {
                        Status = "1",
                        Zone_Id = LoginClass.Zone_Id,
                        Action = id,
                    };
                }
                else
                {
                    _UNIT = new()
                    {
                        Status = "2",
                        Zone_Id = LoginClass.Zone_Id,
                        Action = id,
                    };
                }
                HttpResponseMessage response = client.PostAsync(conn + "Accounts/SP_Sign_Up_Users_List", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_ServiceProviderSignUp deserialized = JsonConvert.DeserializeObject<Get_ServiceProviderSignUp>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }


        public async Task<IActionResult> SP_Sign_Up_List_Email()
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                ServiceProviderSignUp _UNIT;
                _UNIT = new()
                {
                    Official_Email_Id = LoginClass.Email_Id,
                };

                HttpResponseMessage response = client.PostAsync(conn + "Accounts/SP_Sign_Up_Users_List_Email", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_ServiceProviderSignUp deserialized = JsonConvert.DeserializeObject<Get_ServiceProviderSignUp>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> _ViewUserReport(int Sign_Up)
        {
            using (client)
            {
                ServiceProviderSignUp _UNIT = new()
                {
                    SignUp_Id = Convert.ToString(Sign_Up),
                };

                HttpResponseMessage response = client.PostAsync(conn + "Accounts/Get_Service_Provider_Sign_Up", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_ServiceProviderSignUp deserialized = JsonConvert.DeserializeObject<Get_ServiceProviderSignUp>(customerJsonString)!;
                return PartialView("_ViewUserReport", deserialized!.Get_ById);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSignUpStatus([FromBody] ServiceProviderSignUp _UNIT)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                _UNIT.CreatedBy = LoginClass.Employee_Identity_Id;
                _UNIT.Role_Id = LoginClass.Role_Id;
                HttpResponseMessage response = client.PostAsync(conn + "Accounts/UpdateSignUpStatus", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update_SignUp_Document_Submission([FromBody] L_M_RequiredAttachments_List _UNIT)
        {
            using (client)
            {
                HttpResponseMessage response = client.PostAsync(conn + "Accounts/Update_SignUp_Document_Submission", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        public async Task<IActionResult> Get_SP_Sign_Up_List(string Zone_Id, string Community_Id, string Building_Id, string Approval_Status)
        {
            using (client)
            {
                ServiceProviderSignUp _UNIT = new ServiceProviderSignUp()
                {
                    Zone_Id = Zone_Id,
                    Community_Id = Community_Id,
                    Building_Id = Building_Id,
                    ApproveRes_1 = Approval_Status,
                };
                //HttpResponseMessage response = client.GetAsync("ServiceStatistics/GetService_Monthly_Statics").Result;
                HttpResponseMessage response = client.PostAsync(conn + "Accounts/Get_SP_Sign_Up_Users_List", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_ServiceProviderSignUp deserialized = JsonConvert.DeserializeObject<Get_ServiceProviderSignUp>(customerJsonString)!;
                return PartialView(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> Get_Employee_Filter(string Zone_Id, string Community_Id, string Building_Id)
        {
            using (client)
            {
                ServiceProviderSignUp _UNIT = new ServiceProviderSignUp()
                {
                    Zone_Id = Zone_Id,
                    Community_Id = Community_Id,
                    Building_Id = Building_Id
                };
                HttpResponseMessage response = client.PostAsync(conn + "EmployeeMaster/Get_Employee_Filter", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_EMPLOYEE_MASTER deserialized = JsonConvert.DeserializeObject<GET_EMPLOYEE_MASTER>(customerJsonString)!;
                return PartialView(deserialized!.Get_All);
            }
        }

        #endregion

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
        public async Task<IActionResult> ClearNotification(string ID)
        {
            List<tbl_Notification_Sequence> tbl_Notification_s = new List<tbl_Notification_Sequence>();
            tbl_Notification_Sequence _UNIT = new tbl_Notification_Sequence();

            if (ID == "0")
            {
                Login_ login_ = GetLoginDetails();
                _UNIT.Login_User_Id = login_.Employee_Identity_Id;
            }
            else
            {
                _UNIT.ANS_Notification_ID = ID;
            }
            HttpResponseMessage response = client.PostAsync(conn + "Accounts/UpdateWebBellReadStatus", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
            return Json(deserialized);

        }

        #endregion

        #region [CRISIS MANAGEMENT]

        #region[LEVEL MASTER]
        public async Task<IActionResult> Level_Master()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync(conn + "MasterCommunity/LevelM_Master_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Level_Master deserialized = JsonConvert.DeserializeObject<Get_Level_Master>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLevel_Master(Level_Master model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.LevelM_Id == "0")
                    {
                        URL = "MasterCommunity/LevelM_Master_Add";
                    }
                    else
                    {
                        URL = "MasterCommunity/LevelM_Master_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(conn + URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["Zonemsg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Level_Master");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> Level_Master_GetByID(string ID)
        {

            using (client)
            {
                Level_Master _UNIT = new Level_Master
                {
                    LevelM_Id = ID

                };
                HttpResponseMessage response = client.PostAsync(conn + "MasterCommunity/LevelM_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Level_Master deserialized = JsonConvert.DeserializeObject<Get_Level_Master>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> Level_Master_Delete(string ID)
        {
            using (client)
            {
                Level_Master _UNIT = new Level_Master
                {
                    LevelM_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "MasterCommunity/LevelM_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion

        #region[CRISIS MANAGEMENT]
        public async Task<IActionResult> Crisis_Master()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync(conn + "MasterCommunity/Crisis_Master_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Crisis_Master deserialized = JsonConvert.DeserializeObject<Get_Crisis_Master>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> Crisis_Master_Comm()
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();

                Crisis_Team_Master _UNIT = new Crisis_Team_Master
                {
                    CreatedBy = LoginClass.Employee_Identity_Id
                };
                //HttpResponseMessage response = client.PostAsync(conn + "MasterCommunity/Crisis_Master_GetAll").Result;
                HttpResponseMessage response = client.PostAsync(conn + "MasterCommunity/Crisis_Viewby_Emp", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Crisis_Master deserialized = JsonConvert.DeserializeObject<Get_Crisis_Master>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCrisis_Master([FromBody] Crisis_Team_Master model)
        {
            using (client)
            {
                string URL = "";
                if (model.Crisis_Master_Id == "0")
                {
                    URL = "MasterCommunity/Crisis_Master_Add";
                }
                else
                {
                    URL = "MasterCommunity/Crisis_Master_Update";
                }

                HttpResponseMessage response = client.PostAsync(conn + URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }

        public async Task<IActionResult> Crisis_Master_GetByID(string ID)
        {

            using (client)
            {
                Level_Master _UNIT = new Level_Master
                {
                    LevelM_Id = ID

                };
                HttpResponseMessage response = client.PostAsync(conn + "MasterCommunity/Crisis_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Level_Master deserialized = JsonConvert.DeserializeObject<Get_Level_Master>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> Crisis_Master_Delete(string ID)
        {
            using (client)
            {
                Crisis_Master _UNIT = new Crisis_Master
                {
                    Crisis_Master_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "MasterCommunity/Crisis_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        public async Task<IActionResult> _View_Crisis_Master_list(string Crisis_Master_Id)
        {

            using (client)
            {
                Crisis_Team_Master _UNIT = new Crisis_Team_Master
                {
                    Crisis_Master_Id = Crisis_Master_Id
                };
                HttpResponseMessage response = client.PostAsync(conn + "MasterCommunity/Crisis_View", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Crisis_Master deserialized = JsonConvert.DeserializeObject<Get_Crisis_Master>(customerJsonString)!;
                return PartialView(deserialized.Get_ById);
            }
        }
        public async Task<IActionResult> Crisis_Get_Emp_Level(string Level_Id, string Zone_Id, string Community_Id)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();

                Crisis_Master _UNIT = new Crisis_Master
                {
                    LevelM_Id = Level_Id,
                    Zone_Id = Zone_Id,
                    Remarks = Community_Id,
                };
                HttpResponseMessage response = client.PostAsync(conn + "MasterCommunity/Crisis_Emp_List_Level_Id", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Crisis_SubEmp_Master deserialized = JsonConvert.DeserializeObject<Get_Crisis_SubEmp_Master>(customerJsonString)!;
                return Json(deserialized.Get_All);
            }
        }
        #endregion

        #region[EMERGENCY CATEGORY]
        public async Task<IActionResult> Emergency_Category_Master()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync(conn + "MasterCommunity/Emergency_Category_Master_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Emergency_Category_Master deserialized = JsonConvert.DeserializeObject<Get_Emergency_Category_Master>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmergency_Category_Master(Emergency_Category_Master model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    string URL = "";
                    if (model.Emergency_Category_Id == "0")
                    {
                        URL = "MasterCommunity/Emergency_Category_Add";
                    }
                    else
                    {
                        URL = "MasterCommunity/Emergency_Category_Update";
                    }

                    HttpResponseMessage response = client.PostAsync(conn + URL, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
                    string customerJsonString = await response.Content.ReadAsStringAsync();
                    RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                    TempData["Zonemsg"] = deserialized!.MESSAGE;
                }
                return RedirectToAction("Emergency_Category_Master");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> Emergency_Category_GetByID(string ID)
        {

            using (client)
            {
                Emergency_Category_Master _UNIT = new Emergency_Category_Master
                {
                    Emergency_Category_Id = ID

                };
                HttpResponseMessage response = client.PostAsync(conn + "MasterCommunity/Emergency_Category_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Emergency_Category_Master deserialized = JsonConvert.DeserializeObject<Get_Emergency_Category_Master>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> Emergency_Category_Delete(string ID)
        {
            using (client)
            {
                Emergency_Category_Master _UNIT = new Emergency_Category_Master
                {
                    Emergency_Category_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "MasterCommunity/Emergency_Category_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        #endregion
        #region[Location]
        public async Task<IActionResult> Location_Master()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync(conn + "LocationMaster/Location_Master_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_Location_Master deserialized = JsonConvert.DeserializeObject<GET_Location_Master>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddLocation([FromBody] Location_Master _UNIT)
        {
            using (client)
            {
                string URL = "";
                if (_UNIT.Location_Id == "0")
                {
                    URL = "LocationMaster/Location_Master_Add";
                }
                else
                {
                    URL = "LocationMaster/Location_Master_Update";
                }
                HttpResponseMessage response = client.PostAsync(conn + URL, new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_Location_Master deserialized = JsonConvert.DeserializeObject<GET_Location_Master>(customerJsonString)!;
                return Json(deserialized!.STATUS_CODE);
            }
        }
        public async Task<IActionResult> Location_Master_GetByID(string ID)
        {

            using (client)
            {
                Location_Master _UNIT = new Location_Master
                {
                    Location_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "LocationMaster/Location_Master_GetById", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_Location_Master deserialized = JsonConvert.DeserializeObject<GET_Location_Master>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }
        #endregion
        #endregion

        #region [Hse Bulletin]
        public async Task<IActionResult> Hse_Bulletin()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync(conn + "HSE_Bulletin/Hse_Bulletin_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_Hse_Bulletin deserialized = JsonConvert.DeserializeObject<GET_Hse_Bulletin>(customerJsonString)!;
                return View(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Hse_Bulletin_Delete(string ID)
        {
            using (client)
            {
                Hse_BulletinMaster _UNIT = new Hse_BulletinMaster
                {
                    HSE_Bulletin_Id = ID
                };
                HttpResponseMessage response = client.PostAsync(conn + "HSE_Bulletin/Hse_Bulletin_Delete", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_Hse_Bulletin deserialized = JsonConvert.DeserializeObject<GET_Hse_Bulletin>(customerJsonString)!;
                return Json(deserialized!.STATUS);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddHseBulletin([FromBody] Hse_BulletinMaster model)
        {
            if (ModelState.IsValid)
            {
                using (client)
                {
                    HttpResponseMessage response = client.PostAsync(conn + "HSE_Bulletin/Hse_Bulletin_Add", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
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
        //File Upload
        public async Task<JsonResult> Hse_BulletinFile_Upload(List<IFormFile> files)
        {
            try
            {
                var str = "";
                using (client)
                {
                    string url = conn+"HSE_Bulletin/Hse_Bulletin_PhotosUpload";
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

        #region Emergencychecklist
        public async Task<IActionResult> Emergencychecklist()
        {
            HttpResponseMessage response = client.GetAsync(conn + "EmergencyAlert/EMR_CHK_GetAll").Result;
            string customerJsonString = await response.Content.ReadAsStringAsync();
            EMR_Alert_CHK_list deserialized = JsonConvert.DeserializeObject<EMR_Alert_CHK_list>(customerJsonString)!;
            return View(deserialized!.Data);

        }
        public async Task<IActionResult> GetByID_EmergencySubcategory(string ID)
        {
            using (client)
            {
                EMR_Alert_CHK eMR_Alert = new EMR_Alert_CHK
                {
                    Mitigation_Type = ID

                };
                HttpResponseMessage response = client.PostAsync(conn + "EmergencyAlert/EMR_CHK_GetByID", new StringContent(JsonConvert.SerializeObject(eMR_Alert), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                EMR_Alert_CHK_list deserialized = JsonConvert.DeserializeObject<EMR_Alert_CHK_list>(customerJsonString)!;
                if (deserialized.Status_Code == "200")
                {
                    return Json(deserialized);
                }

                else
                {
                    return Json(deserialized);
                }

            }
        }
        public async Task<IActionResult> DeleteEmg_Checklist(string ID)
        {
            using (client)
            {
                Login_ LoginClass = GetLoginDetails();
                EMR_Alert_CHK eMR_Alert = new EMR_Alert_CHK
                {
                    Alert_Checklist_Id = ID,
                    Created_By = LoginClass.Employee_Identity_Id

                };
                HttpResponseMessage response = client.PostAsync(conn + "EmergencyAlert/EMR_CHK_Delete", new StringContent(JsonConvert.SerializeObject(eMR_Alert), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                EMR_Alert_CHK_list deserialized = JsonConvert.DeserializeObject<EMR_Alert_CHK_list>(customerJsonString)!;

                return Json(deserialized.Status_Code);



            }
        }
        public async Task<IActionResult> Submit_Checklist(List<EMR_Alert_CHK> TNI)
        {
            if (ModelState.IsValid)
            {
                Login_ LoginClass = GetLoginDetails();
                foreach (var item in TNI)
                {
                    item.Created_By = LoginClass.Employee_Identity_Id;
                }
                HttpResponseMessage response = client.PostAsync(conn + "EmergencyAlert/EMR_CHK_Add", new StringContent(JsonConvert.SerializeObject(TNI), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized!.STATUS);

            }
            else
            {
                return Ok("500");
            }
        }
        #endregion
    }

}
