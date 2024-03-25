using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models.AuditMaster;
using Nakheel_Web.Models.ControlOfWorkMaster;
using Nakheel_Web.Models.IncidentMaster;
using Nakheel_Web.Models.Masters;
using Nakheel_Web.Models.SecurityIncidentMaster;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    [TypeFilter(typeof(SessionExpireActionFilter))]
    [TypeFilter(typeof(ExpFilter))]
    public class CommonMasterController : Controller
    {
        private readonly HttpClient client = new HttpClient();

        public CommonMasterController(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("API");
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

        public async Task<IActionResult> LoadAllInjuryTypes()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentMaster/Injury_Type_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_INJURY_TYPE deserialized = JsonConvert.DeserializeObject<GET_INJURY_TYPE>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadAllNatrueInjuryTypes()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentMaster/Nature_Injury_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_NATURE_OF_INJURY deserialized = JsonConvert.DeserializeObject<GET_NATURE_OF_INJURY>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadAllInc_Cat()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentMaster/Inc_Category_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_INCIDENT_CATEGORY deserialized = JsonConvert.DeserializeObject<GET_INCIDENT_CATEGORY>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadAllIncCatbyType(string Inc_Cat_Id)
        {
            using (client)
            {
                INCIDENT_TYPE _UNIT = new INCIDENT_TYPE
                {
                    Inc_Cat_Id = Inc_Cat_Id
                };
                HttpResponseMessage response = client.PostAsync("IncidentMaster/Inc_Type_GetAllByCatId", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ICIDENT_TYPE deserialized = JsonConvert.DeserializeObject<GET_ICIDENT_TYPE>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadAllHealthSafety()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentSubMaster/Health_Safety_Type_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_HEALTH_SAFETY deserialized = JsonConvert.DeserializeObject<GET_HEALTH_SAFETY>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadAllEnvironment()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentSubMaster/Environment_Type_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ENVIRONMENT_OBSERVATION deserialized = JsonConvert.DeserializeObject<GET_ENVIRONMENT_OBSERVATION>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadAllVehicleAccident()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentSubMaster/Vehicle_Accident_Type_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_VEHICLE_ACCIDENT_TYPE deserialized = JsonConvert.DeserializeObject<GET_VEHICLE_ACCIDENT_TYPE>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadAllZoneIsCommunity()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("ZoneMaster/Zone_GetAll_Is_Community").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ZONE_MANAGEMENT deserialized = JsonConvert.DeserializeObject<GET_ZONE_MANAGEMENT>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadAllZoneIsMasterCommunity()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("ZoneMaster/Zone_GetAll_Is_MasterCommunity").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ZONE_MANAGEMENT deserialized = JsonConvert.DeserializeObject<GET_ZONE_MANAGEMENT>(customerJsonString)!;
                return Json(deserialized!.Get_All);
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

        public async Task<IActionResult> LoadAllRole()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("RoleMaster/Role_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_ROLE_MASTER deserialized = JsonConvert.DeserializeObject<GET_ROLE_MASTER>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadAllDes()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("DesignationMaster/Designation_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_DESIGNATION_MASTER deserialized = JsonConvert.DeserializeObject<GET_DESIGNATION_MASTER>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadAllDepartment()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("DepartmentMaster/Department_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_DEPARTMENT_MASTER deserialized = JsonConvert.DeserializeObject<GET_DEPARTMENT_MASTER>(customerJsonString)!;
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

        public async Task<IActionResult> LoadEmpMasterFilter(string Zone_Id, string Emp_Community_Id)
        {
            using (client)
            {
                M_Employee_Master_Filter _UNIT = new M_Employee_Master_Filter
                {

                    Zone_Id = Zone_Id,
                    Emp_Community_Id = Emp_Community_Id
                };
                HttpResponseMessage response = client.PostAsync("EmployeeMaster/Get_Employee_Master_Filter", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_EMPLOYEE_MASTER deserialized = JsonConvert.DeserializeObject<GET_EMPLOYEE_MASTER>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadEmpMasterFilter_Gold()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("EmployeeMaster/Get_Employee_Master_Filter_Gold").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_EMPLOYEE_MASTER deserialized = JsonConvert.DeserializeObject<GET_EMPLOYEE_MASTER>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadEmpMasterFilter_Silver()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("EmployeeMaster/Get_Employee_Master_Filter_Silver").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_EMPLOYEE_MASTER deserialized = JsonConvert.DeserializeObject<GET_EMPLOYEE_MASTER>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadEmpMasterFilter_Bronze(string Zone_Id)
        {
            using (client)
            {
                BUILDING_MANAGMENT _UNIT = new BUILDING_MANAGMENT
                {
                    Zone_Id = Zone_Id,
                };
                HttpResponseMessage response = client.PostAsync("EmployeeMaster/Get_Employee_Master_Filter_Bronze", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_EMPLOYEE_MASTER deserialized = JsonConvert.DeserializeObject<GET_EMPLOYEE_MASTER>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadEmpContact(string Employee_Identity_Id)
        {
            using (client)
            {
                M_Employee_Master _UNIT = new M_Employee_Master
                {
                    Employee_Identity_Id = Employee_Identity_Id,
                };
                HttpResponseMessage response = client.PostAsync("EmployeeMaster/Get_Contact_GetByEmpID", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_EMPLOYEE_MASTER deserialized = JsonConvert.DeserializeObject<GET_EMPLOYEE_MASTER>(customerJsonString)!;
                return Json(deserialized!.Get_ById);
            }
        }

        public async Task<IActionResult> LoadEmpbyRole(string Role_Name, string Zone_Id, string Community_Id)
        {
            using (client)
            {
                M_Employee_Master_Filter_by_Role _UNIT = new M_Employee_Master_Filter_by_Role
                {
                    Role_Name = Role_Name,
                    Zone_Id = Zone_Id,
                    Emp_Community_Id = Community_Id
                };
                HttpResponseMessage response = client.PostAsync("EmployeeMaster/Get_Employee_by_Role", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_EMPLOYEE_MASTER deserialized = JsonConvert.DeserializeObject<GET_EMPLOYEE_MASTER>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        // Load Employee By Zone - Line Manager
        public async Task<IActionResult> LoadEmpbyZone(string Zone_Id)
        {
            using (client)
            {
                M_Employee_Master_Filter_by_Role _UNIT = new M_Employee_Master_Filter_by_Role
                {
                    Zone_Id = Zone_Id,
                };
                HttpResponseMessage response = client.PostAsync("EmployeeMaster/Get_Employee_by_Zone", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_EMPLOYEE_MASTER deserialized = JsonConvert.DeserializeObject<GET_EMPLOYEE_MASTER>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadAllIncident_Related()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentSubMaster/Incident_Relate_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_INCIDENT_RELATE_MASTER deserialized = JsonConvert.DeserializeObject<GET_INCIDENT_RELATE_MASTER>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        #region [INVESTIGATION MASTER LOAD]
        public async Task<IActionResult> LoadUnsafeActList()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentMaster/Unsafe_Act_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_UNSAFE_ACT deserialized = JsonConvert.DeserializeObject<GET_UNSAFE_ACT>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> LoadUnsafeConditionList()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentMaster/Unsafe_Condition_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_UNSAFE_CONDITION deserialized = JsonConvert.DeserializeObject<GET_UNSAFE_CONDITION>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> LoadPersonalfactorList()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentMaster/Personal_Factor_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_PERSONAL_FACTOR deserialized = JsonConvert.DeserializeObject<GET_PERSONAL_FACTOR>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> LoadSystemfactorList()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentMaster/System_Factor_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SYSTEM_FACTOR deserialized = JsonConvert.DeserializeObject<GET_SYSTEM_FACTOR>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> LoadMechanismOfInjuryList()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentSubMaster/Mechanism_Injury_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_MECHANISM_INJURY deserialized = JsonConvert.DeserializeObject<GET_MECHANISM_INJURY>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> LoadAgencyOfInjuryList()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentSubMaster/Agency_Injury_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_AGENCY_INJURY deserialized = JsonConvert.DeserializeObject<GET_AGENCY_INJURY>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> LoadClassificationList()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("IncidentSubMaster/Classification_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_CLASSIFICATION deserialized = JsonConvert.DeserializeObject<GET_CLASSIFICATION>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        public async Task<IActionResult> LoadTypeofSecurityIncident(string Classification_Id)
        {
            using (client)
            {
                CLASSIFICATION _UNIT = new CLASSIFICATION
                {
                    Classification_Id = Classification_Id
                };
                HttpResponseMessage response = client.PostAsync("IncidentSubMaster/Type_Security_GetAll_Classfication", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_TYPE_SECURITY_INCIDENT deserialized = JsonConvert.DeserializeObject<GET_TYPE_SECURITY_INCIDENT>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        #endregion

        #region [GOOGLE MAP]
        public IActionResult _LoadGoogleMap()
        {
            return PartialView();
        }
        public IActionResult _Load_View_GoogleMap([FromBody] RETURN_MESSAGE fgf)
        {
            ViewBag.lat = fgf.STATUS_CODE;
            ViewBag.longt = fgf.UNIQUE_ID;
            ViewBag.exact = fgf.MESSAGE;
            return PartialView();
        }
        #endregion

        #region [CHECK DUPLICATE BUILDING]
        public async Task<IActionResult> CheckBuilding_Name(string Building_Name)
        {
            using (client)
            {
                M_Building_List _UNIT = new M_Building_List
                {
                    Building_Name = Building_Name
                };
                HttpResponseMessage response = client.PostAsync("BuildingMaster/CheckBuilding_Name", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized.MESSAGE);

            }
        }

        public async Task<IActionResult> CheckCommunity_Name(string MasterCommunity_Name)
        {
            using (client)
            {
                M_MasterCommunity_List _UNIT = new M_MasterCommunity_List
                {
                    MasterCommunity_Name = MasterCommunity_Name
                };
                HttpResponseMessage response = client.PostAsync("MasterCommunity/CheckMasterCommunity_Name", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                RETURN_MESSAGE deserialized = JsonConvert.DeserializeObject<RETURN_MESSAGE>(customerJsonString)!;
                return Json(deserialized.MESSAGE);

            }
        }
        #endregion

        #region[Security Incident master Load]
        public async Task<IActionResult> LoadAllSecInc_Cat()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("SecurityIncidentMaster/Sec_Inc_Category_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                GET_SECINCIDENT_CATEGORY deserialized = JsonConvert.DeserializeObject<GET_SECINCIDENT_CATEGORY>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        #endregion

        #region [Audit]
        public async Task<IActionResult> Load_Sub_Topics_Quest(string Audit_Category_Id)
         {
            using (client)
            {
                Audit_Schedule_Model _UNIT = new Audit_Schedule_Model
                {
                    Audit_Category_Id = Audit_Category_Id
                };
                HttpResponseMessage response = client.PostAsync("AuditMaster/Get_All_Questionnaire_Chk", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Audit_Questionnaires deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Questionnaires>(customerJsonString)!;
                return Json(deserialized!.Get_All_Questionnaire);
            }
        }

        public async Task<IActionResult> Load_Audit_Type()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("AuditMaster/Get_All_Audit_Type").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                M_Get_Audit_Questionnaires deserialized = JsonConvert.DeserializeObject<M_Get_Audit_Questionnaires>(customerJsonString)!;
                return Json(deserialized!.Get_All_Type);
            }
        }
        #endregion

        #region[PERMIT OF WORK MASTER]
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
        #endregion

        #region [LEVEL_MASTER]
        public async Task<IActionResult> Load_Level_Master()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("MasterCommunity/LevelM_Master_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Level_Master deserialized = JsonConvert.DeserializeObject<Get_Level_Master>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Load_Emergency_Category_Master()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("MasterCommunity/Emergency_Category_Master_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_Emergency_Category_Master deserialized = JsonConvert.DeserializeObject<Get_Emergency_Category_Master>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        #endregion

        #region [ERT TEAM DETAILS MASTERS ONLY --GET ALL]
        public async Task<IActionResult> Load_ERT_Department_GetAll()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("MasterCommunity/ERT_Department_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_ERT_Team_Details_Master deserialized = JsonConvert.DeserializeObject<Get_ERT_Team_Details_Master>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Load_ERT_Building_No_GetAll()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("MasterCommunity/ERT_Building_No_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_ERT_Team_Details_Master deserialized = JsonConvert.DeserializeObject<Get_ERT_Team_Details_Master>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Load_ERT_FloorNo_GetAll()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("MasterCommunity/ERT_FloorNo_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_ERT_Team_Details_Master deserialized = JsonConvert.DeserializeObject<Get_ERT_Team_Details_Master>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Load_ERT_Role_GetAll()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("MasterCommunity/ERT_Role_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_ERT_Team_Details_Master deserialized = JsonConvert.DeserializeObject<Get_ERT_Team_Details_Master>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Load_ERT_Training_Status_GetAll()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("MasterCommunity/ERT_Training_Status_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_ERT_Team_Details_Master deserialized = JsonConvert.DeserializeObject<Get_ERT_Team_Details_Master>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }
        public async Task<IActionResult> Load_ERT_Type_GetAll()
        {
            using (client)
            {
                HttpResponseMessage response = client.GetAsync("MasterCommunity/ERT_Type_GetAll").Result;
                string customerJsonString = await response.Content.ReadAsStringAsync();
                Get_ERT_Team_Details_Master deserialized = JsonConvert.DeserializeObject<Get_ERT_Team_Details_Master>(customerJsonString)!;
                return Json(deserialized!.Get_All);
            }
        }

        #endregion
    }
}
