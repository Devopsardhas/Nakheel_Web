using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Models.Masters;
using System.ComponentModel.DataAnnotations;

namespace Nakheel_Web.Models.InspectionMaster
{
    #region [Spot Insp]


    #region [Inspection Spot Request]
    public class M_Insp_Request : M_Common_Insp_Master
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Insp_Request_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Business_Unit_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Zone_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Community_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Building_Id { get; set; }


        [Required(ErrorMessage = "This field is required")]
        public string? Insp_Type_Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Inspection_Date { get; set; }
        public string? Req_Description { get; set; }
        public string? Category_Id { get; set; }
        public string? Insp_Category_Name { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? Zone_Name { get; set; }
        public string? Community_Name { get; set; }
        public string? Building_Name { get; set; }
        public string? MasterCommunity_Name { get; set; }
        public string? Insp_Type_Name { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
        public string? Role_Id { get; set; }
        public string? Login_Id { get; set; }
        public string? Walk_In_Insp_Name { get; set; }
        public string? Responsible_Dept { get; set; }
        public string? Emp_Service_provider { get; set; }
        public string? Req_Unique_Id { get; set; }
        public string? Req_Insp_Type { get; set; }
        public string? Safety_Violation_Id { get; set; }
        public string? Schedule_Type { get; set; }
        public string? Access_Schedule { get; set; }
        public Basic_Master_Data? Inc_Comman_Master_List { get; set; }
        public List<Insp_Request_Reject>? Insp_Request_Reject_List { get; set; }
        public M_Insp_Spot_Finding? Insp_Spot_Finding_List { get; set; }
        public List<Insp_Req_Doc_Review>? Insp_Req_Doc_Review_List { get; set; }
        public List<Insp_Req_Approval_History>? Insp_Req_Approval_History_List { get; set; }
    }
    public class Insp_Req_Approval_History
    {
        public string? History_Id { get; set; }
        public string? Insp_Request_Id { get; set; }
        public string? Emp_Name { get; set; }
        public string? Role_Name { get; set; }
        public string? Updated_DateTime { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public string? Last_DateTime { get; set; }
        public string? CreatedDate { get; set; }
        public string? Next_Action { get; set; }
    }
    public class Insp_Req_Doc_Review
    {
        public string? Req_Document_Review_Id { get; set; }
        public string? Req_Document_Name { get; set; }
        public string? Req_Document_Description { get; set; }
        public string? File_Path { get; set; }
    }
    public class Basic_Master_Data
    {
        public List<Dropdown_Values>? Business_Master_List { get; set; }
        public List<Dropdown_Values>? Zone_Master_List { get; set; }
        public List<Dropdown_Values>? Community_Master_List { get; set; }
        public List<Dropdown_Values>? Building_Master_List { get; set; }
        public List<Dropdown_Values>? Inspection_Type_Master_List { get; set; }
        public List<M_Insp_Value_Text>? Insp_Category_Master_List { get; set; }
        public List<Dropdown_Values>? Supervisor_Req_List { get; set; }
        public List<Dropdown_Values>? Service_Provider_Req_List { get; set; }
        public List<Dropdown_Values>? HSE_Team_Req_Emp_List { get; set; }
    }
    public class Dropdown_Values
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
    }
    public class Insp_Request_Reject
    {
        public string? Insp_Request_Reject_Id { get; set; }
        public string? Insp_Request_Id { get; set; }
        public string? Reject_Reason { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? Unique_Id { get; set; }
    }
    public class M_Get_Insp_Request
    {
        public IReadOnlyCollection<M_Insp_Request>? Get_All { get; set; }
        public M_Insp_Request? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }
    public class M_Insp_Request_Approval
    {
        [Required(ErrorMessage = "This field is required")]

        public string? Insp_Request_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Status { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Inspection_Date { get; set; }

        public string? Remarks { get; set; }

        public string? CreatedBy { get; set; }
    }
    #endregion

    #region [Inspection Spot Finding]
    public class M_Insp_Spot_Finding : M_Common_Insp_Master
    {
        public string? Business_Unit_Name { get; set; }
        public string? Zone_Name { get; set; }
        public string? Community_Name { get; set; }
        public string? Building_Name { get; set; }
        public string? Insp_Type_Name { get; set; }
        public string? Inspection_Date { get; set; }
        public string? Req_Description { get; set; }
        public string? Insp_Finding_Id { get; set; }
        public string? Insp_Request_Id { get; set; }
        public string? NCM_Supervisor { get; set; }
        public string? NCM_Supervisor_Text { get; set; }
        public string? NCM_Community_Manager { get; set; }
        public string? Zone_Rep_Attended { get; set; }
        public string? Service_Provider_Attended { get; set; }
        public string? Zone_Rep_Id { get; set; }
        public string? Service_Provider_ID { get; set; }
        public string? Zone_Rep_Name { get; set; }
        public string? Service_Provider_Name { get; set; }
        public string? Main_Status { get; set; }
        public string? Req_Createdby { get; set; }
        public string? Login_Id { get; set; }
        public string? Safety_Violation { get; set; }
        public string? Insp_Category_Name { get; set; }
        public string? S_P_Representative_Name { get; set; }
        public string? HSE_Representative_Name { get; set; }
        public string? Schedule_Type { get; set; }
        public string? Other_Attendees { get; set; }
        public string? Summary { get; set; }
        public string? Highlights { get; set; }
        public string? Lowlights { get; set; }
        public List<M_Insp_Leader_Finding_Sub_Photo>? Insp_Leader_Finding_Sub_Photo_List { get; set; }
        public List<M_Insp_Spot_Sub_Finding>? Insp_Spot_Sub_Finding_List { get; set; }
        public List<M_Insp_Value_Text>? Insp_Spot_Emp_Master_List { get; set; }
        public List<M_Insp_Value_Text>? Insp_Spot_Category_Master_List { get; set; }
        public List<M_Insp_Value_Text>? Insp_Spot_Sub_Category_Master_List { get; set; }
        public List<Insp_Finding_Reject>? Insp_Finding_Reject_List { get; set; }
        public List<M_Insp_Value_Text>? Insp_Spot_Supervisor_Master_List { get; set; }
        public List<Insp_Req_Approval_History>? Insp_Req_Approval_History_List { get; set; }
        public List<M_Insp_Value_Text>? Insp_Spot_Provider_Com_Name_List { get; set; }

    }
    public class M_Insp_Leader_Finding_Sub_Photo
    {
        public string? Insp_Leader_Sub_Photo_Id { get; set; }
        public string? Insp_Finding_Id { get; set; }
        public string? Insp_Request_Id { get; set; }
        public string? File_Path { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class M_Insp_Spot_Sub_Finding : M_Common_Insp_Master
    {
        public string? Insp_Sub_Finding_Id { get; set; }
        public string? Insp_Finding_Id { get; set; }
        public string? Insp_Request_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Observations { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Hazard_Risk { get; set; }
        public string? Requirements { get; set; }
        public string? Action_Required { get; set; }
        public string? Category { get; set; }
        public string? Risk_Level { get; set; }
        public string? Insp_Category_Name { get; set; }
        public string? Location_Address { get; set; }
        public string? Exact_Location_Address { get; set; }
        public string? Type { get; set; }
        public List<M_Insp_Spot_Find_Sub_Category>? Insp_Spot_Find_Sub_Category_List { get; set; }
        public List<M_Insp_Spot_Find_Sub_Photo>? Insp_Spot_Find_Sub_Photo_List { get; set; }
        public List<M_Insp_Value_Text>? Insp_Spot_Category_Master_List { get; set; }
        public List<M_Insp_Value_Text>? Insp_Spot_Sub_Category_Master_List { get; set; }
        public List<M_Insp_Spot_Corrective_Action>? Insp_Spot_Find_Corrective_Action_List { get; set; }


    }
    public class M_Insp_Spot_Find_Sub_Category : M_Common_Insp_Master
    {
        public string? Insp_Sub_Finding_Sub_Cat_Id { get; set; }
        public string? Insp_Sub_Finding_Id { get; set; }
        public string? Insp_Finding_Id { get; set; }
        public string? Insp_Request_Id { get; set; }
        public string? Insp_Sub_Category_Id { get; set; }
        public string? Insp_Sub_Category_Name { get; set; }

    }
    public class M_Insp_Spot_Find_Sub_Photo : M_Common_Insp_Master
    {
        public string? Insp_Sub_Photo_Id { get; set; }
        public string? Insp_Sub_Finding_Id { get; set; }
        public string? Insp_Finding_Id { get; set; }
        public string? Insp_Request_Id { get; set; }
        public string? File_Path { get; set; }
    }
    public class M_Insp_Spot_WalkIn_Master
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
        public List<M_Insp_Value_Text>? Zone_Super_Master_List { get; set; }
        public List<M_Insp_Value_Text>? Zone_Rep_Master_List { get; set; }
        public List<M_Insp_Value_Text>? Zone_Service_Provider_Master_List { get; set; }
    }
    public class M_Insp_Value_Text
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
        public string? Type_Violation_Mas_Id { get; set; }
    }
    public class M_Get_Spot_WalkIn_Master
    {
        public IReadOnlyCollection<M_Insp_Spot_WalkIn_Master>? Get_All { get; set; }
        public M_Insp_Spot_WalkIn_Master? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }
    public class M_Get_Master_Finding_List
    {
        public IReadOnlyCollection<M_Insp_Value_Text>? Get_All { get; set; }
        public M_Insp_Value_Text? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }
    public class M_Get_Spot_Find_Sub_Photo_List
    {
        public IReadOnlyCollection<M_Insp_Value_Text>? Get_All { get; set; }
        public M_Insp_Value_Text? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }
    public class M_Get_Sub_Finding_List
    {
        public IReadOnlyCollection<M_Insp_Spot_Sub_Finding>? Get_All { get; set; }
        public M_Insp_Spot_Sub_Finding? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }
    public class M_Insp_Finding_Approval
    {
        [Required(ErrorMessage = "This field is required")]
        public string? Insp_Finding_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Status { get; set; }

        public string? Remarks { get; set; }

        public string? CreatedBy { get; set; }
    }
    public class Insp_Finding_Reject
    {
        public string? Insp_Find_Reject_Id { get; set; }
        public string? Insp_Finding_Id { get; set; }
        public string? Insp_Request_Id { get; set; }
        public string? Reject_Reason { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? Unique_Id { get; set; }
    }
    #endregion

    #region [Insp Spot Corrective Action]
    public class M_Insp_Spot_Corrective_Action_Add
    {
        public string? Corrective_Action_Id { get; set; }
        public string? Insp_Finding_Id { get; set; }
        public string? Insp_Request_Id { get; set; }
        public string? Insp_Sub_Finding_Id { get; set; }
        public string? Assignee_Type { get; set; }
        public string? Responsibility_To { get; set; }
        public string? Target_Date { get; set; }
        public string? Action_Description { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class M_Insp_Spot_Corrective_Action : M_Common_Insp_Master
    {
        public string? Corrective_Action_Id { get; set; }
        public string? Insp_Finding_Id { get; set; }
        public string? Insp_Request_Id { get; set; }
        public string? Insp_Sub_Finding_Id { get; set; }
        public string? Assignee_Type { get; set; }
        public string? Responsibility_To { get; set; }
        public string? Responsibility_To_Name { get; set; }
        public string? Target_Date { get; set; }
        public string? Action_Description { get; set; }
        public string? Closure_Action_Description { get; set; }
        public string? Company_Name { get; set; }
        public List<M_Insp_Spot_CA_Photo>? Insp_Spot_CA_Photo_List { get; set; }
        public List<M_Insp_Spot_CA_Reject>? Insp_Spot_CA_Reject_List { get; set; }
        public List<M_Insp_Spot_CA_Re_Assign>? Insp_Spot_CA_ReAssign_List { get; set; }

    }
    public class M_Insp_Spot_CA_Photo : M_Common_Insp_Master
    {
        public string? Insp_CA_Photo_Id { get; set; }
        public string? Corrective_Action_Id { get; set; }
        public string? File_Path { get; set; }
    }
    public class M_Get_Spot_CA_Photo_List
    {
        public IReadOnlyCollection<M_Insp_Spot_CA_Photo>? Get_All { get; set; }
        public M_Insp_Spot_CA_Photo? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }
    public class M_Insp_Spot_CA_Reject
    {
        public string? Insp_CA_Reject_Id { get; set; }
        public string? Corrective_Action_Id { get; set; }
        public string? Reject_Reason { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? Unique_Id { get; set; }
    }
    public class M_Insp_Spot_CA_Re_Assign
    {
        public string? Insp_Re_Assign_Id { get; set; }
        public string? Corrective_Action_Id { get; set; }
        public string? Remarks { get; set; }
        public string? CreatedDate { get; set; }
        public string? Responsibility_To { get; set; }
        public string? Re_Assign_To { get; set; }
        public string? Unique_Id { get; set; }
    }

    #endregion

    #region [Safety Violation]
    public class M_Insp_Safety_Violation : M_Common_Insp_Master
    {
        public string? Safety_Violation_Id { get; set; }
        public string? Insp_Request_Id { get; set; }
        public string? Req_Insp_Type { get; set; }
        public string? Req_Unique_Id { get; set; }
        public string? Responsible_Dept { get; set; }
        public string? Description_Violation { get; set; }
        public string? Requirement { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? Zone_Name { get; set; }
        public string? Community_Name { get; set; }
        public string? Inspection_Date { get; set; }
        public string? Inspection_Time { get; set; }
        public string? Emp_Service_provider { get; set; }
        public string? Emp_Service_provider_Name { get; set; }
        public List<M_Insp_Value_Text>? Insp_Spot_Category_Master_List { get; set; }
        public List<M_Insp_Safety_Violation_Sub_Type>? Safety_Violation_Sub_Type_List { get; set; }
        public List<M_Insp_Safety_ViolationReject>? Safety_Violation_Reject_List { get; set; }
        public List<M_Safety_Vio_Corrective_Action>? Safety_Violation_CA_List { get; set; }
        public List<M_Emp_Service_Provider>? Emp_Service_Provider_List { get; set; }
        public List<Insp_Req_Approval_History>? Insp_Safe_Vio_Approval_History_List { get; set; }
        public List<Dropdown_Values>? Business_Master_List { get; set; }
        public List<Dropdown_Values>? Zone_Master_List { get; set; }
        public List<M_Insp_Safety_Photos>? Insp_Safe_Vio_Photos_List { get; set; }

    }
    public class M_Insp_Safety_Photos
    {
        public string? Photo_Id { get; set; }
        public string? Safety_Violation_Id { get; set; }
        public string? Insp_Request_Id { get; set; }
        public string? File_Path { get; set; }
    }
    public class M_Emp_Service_Provider
    {
        public string? Employee_Identity_Id { get; set; }
        public string? First_Name { get; set; }
        public string? Email_Id { get; set; }
    }
    public class M_Get_M_Emp_Service_Provider
    {
        public IReadOnlyCollection<M_Emp_Service_Provider>? Get_All { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }
    public class M_Insp_Safety_ViolationReject : M_Common_Insp_Master
    {
        public string? Insp_Safe_Violation_Reject_Id { get; set; }
        public string? Safety_Violation_Id { get; set; }
        public string? Reject_Reason { get; set; }
    }
    public class M_Insp_Safety_Violation_Sub_Type : M_Common_Insp_Master
    {
        public string? Type_of_Violation_Id { get; set; }
        public string? Insp_Request_Id { get; set; }
        public string? Safety_Violation_Id { get; set; }
        public string? Type_Master_Id { get; set; }
        public string? Type_Master_Name { get; set; }
        public string? Others_Name { get; set; }
    }
    public class M_Get_Insp_Safety_Violation
    {
        public IReadOnlyCollection<M_Insp_Safety_Violation>? Get_All { get; set; }
        public M_Insp_Safety_Violation? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }
    public class M_Safety_Vio_Corrective_Action : M_Common_Insp_Master
    {
        public string? SV_Corrective_Action_Id { get; set; }
        public string? Safety_Violation_Id { get; set; }
        public string? Assignee_Type { get; set; }
        public string? Responsibility_To { get; set; }
        public string? Responsibility_To_Name { get; set; }
        public string? Target_Date { get; set; }
        public string? Action_Description { get; set; }
        public string? Closure_Action_Description { get; set; }
        public List<M_Insp_SV_CA_Photo>? Insp_SV_CA_Photo_List { get; set; }
        public List<M_Insp_SV_CA_Reject>? Insp_SV_CA_Reject_List { get; set; }
        public List<M_Insp_SV_CA_Re_Assign>? Insp_SV_CA_ReAssign_List { get; set; }
    }
    public class M_Insp_SV_CA_Photo : M_Common_Insp_Master
    {
        public string? Insp_SV_CA_Photo_Id { get; set; }
        public string? SV_Corrective_Action_Id { get; set; }
        public string? File_Path { get; set; }
    }
    public class M_Insp_SV_CA_Reject
    {
        public string? Insp_SV_CA_Reject_Id { get; set; }
        public string? SV_Corrective_Action_Id { get; set; }
        public string? Reject_Reason { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? Unique_Id { get; set; }
    }
    public class M_Insp_SV_CA_Re_Assign
    {
        public string? Insp_SV_Re_Assign_Id { get; set; }
        public string? SV_Corrective_Action_Id { get; set; }
        public string? Remarks { get; set; }
        public string? CreatedDate { get; set; }
        public string? Responsibility_To { get; set; }
        public string? Re_Assign_To { get; set; }
        public string? Unique_Id { get; set; }
    }
    public class M_Get_SV_CA_Photo_List
    {
        public IReadOnlyCollection<M_Insp_SV_CA_Photo>? Get_All { get; set; }
        public M_Insp_SV_CA_Photo? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }

    public class M_Get_Insp_Safety_Photos
    {
        public IReadOnlyCollection<M_Insp_Safety_Photos>? Get_All { get; set; }
        public M_Insp_Safety_Photos? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }

    #endregion


    #endregion

    #region [Joint Insp]

    public class M_Insp_Joint_Request : M_Common_Insp_Master
    {
        public string? Insp_Request_Id { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_Id { get; set; }
        public string? Supervisor_Id { get; set; }
        public string? Service_Provider_Id { get; set; }
        public string? HSE_Representative_Id { get; set; }
        public string? Insp_Type_Id { get; set; }
        public string? Inspection_Date { get; set; }
        public string? Req_Description { get; set; }
        public string? Category_Id { get; set; }
        public string? Insp_Category_Name { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? Zone_Name { get; set; }
        public string? Community_Name { get; set; }
        public string? Building_Name { get; set; }
        public string? MasterCommunity_Name { get; set; }
        public string? Insp_Type_Name { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
        public string? Role_Id { get; set; }
        public string? Login_Id { get; set; }
        public string? Walk_In_Insp_Name { get; set; }
        public string? Responsible_Dept { get; set; }
        public string? Emp_Service_provider { get; set; }
        public string? Supervisor_Name { get; set; }
        public string? Service_Provider_Name { get; set; }
        public string? HSE_Representative_Name { get; set; }
        public string? Schedule_Type { get; set; }
        public string? Access_Schedule { get; set; }
        public string? Scope_Of_Work { get; set; }
        public Basic_Master_Data? Inc_Comman_Master_List { get; set; }
        public List<Insp_Request_Reject>? Insp_Request_Reject_List { get; set; }
        public M_Insp_Spot_Finding? Insp_Spot_Finding_List { get; set; }
        public List<Insp_Req_Doc_Review>? Insp_Req_Doc_Review_List { get; set; }
        public List<Insp_Req_Approval_History>? Insp_Req_Approval_History_List { get; set; }
    }
    public class M_Insp_Joint_Request_Approval
    {
        [Required(ErrorMessage = "This field is required")]

        public string? Insp_Request_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Status { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Inspection_Date { get; set; }

        public string? Remarks { get; set; }

        public string? CreatedBy { get; set; }
    }
    public class M_Get_Insp_Joint_Request
    {
        public IReadOnlyCollection<M_Insp_Joint_Request>? Get_All { get; set; }
        public M_Insp_Joint_Request? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }
    public class M_Get_Dropdown_Values
    {
        public IReadOnlyCollection<Dropdown_Values>? Get_All { get; set; }
        public Dropdown_Values? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }

    #endregion

    #region [Leadership Tour Insp]
    public class M_Insp_Leader_Request : M_Common_Insp_Master
    {
        public string? Insp_Request_Id { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_Id { get; set; }
        public string? Zone_Director_Id { get; set; }
        public string? HSSE_Director_Id { get; set; }
        public string? Insp_Type_Id { get; set; }
        public string? Inspection_Date { get; set; }
        public string? Req_Description { get; set; }
        public string? Category_Id { get; set; }
        public string? Insp_Category_Name { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? Zone_Name { get; set; }
        public string? Community_Name { get; set; }
        public string? Building_Name { get; set; }
        public string? MasterCommunity_Name { get; set; }
        public string? Insp_Type_Name { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
        public string? Role_Id { get; set; }
        public string? Login_Id { get; set; }
        public string? Walk_In_Insp_Name { get; set; }
        public string? Responsible_Dept { get; set; }
        public string? Emp_Service_provider { get; set; }
        public string? Zone_Director_Name { get; set; }
        public string? HSSE_Director_Name { get; set; }
        public string? Other_Attendees { get; set; }
        public string? Schedule_Type { get; set; }
        public string? Access_Schedule { get; set; }
        public Basic_Master_Data? Inc_Comman_Master_List { get; set; }
        public List<Insp_Request_Reject>? Insp_Request_Reject_List { get; set; }
        public M_Insp_Spot_Finding? Insp_Spot_Finding_List { get; set; }
        public List<Insp_Req_Doc_Review>? Insp_Req_Doc_Review_List { get; set; }
        public List<Insp_Req_Approval_History>? Insp_Req_Approval_History_List { get; set; }
    }

    public class M_Get_Insp_Leader_Request
    {
        public IReadOnlyCollection<M_Insp_Leader_Request>? Get_All { get; set; }
        public M_Insp_Leader_Request? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }
    #endregion

    #region [Advisory Notice Insp]


    #region [Complaint Register]
    public class M_Complaint_Register : M_Common_Insp_Master
    {
        public string? Complaint_Id { get; set; }
        public string? Reported_By_Id { get; set; }
        public string? Reported_By_Name { get; set; }
        public string? Reported_By_Number { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_Id { get; set; }
        public string? Customer_Name { get; set; }
        public string? Customer_Address { get; set; }
        public string? Customer_Email { get; set; }
        public string? Customer_Number { get; set; }
        public string? Date_Complaint { get; set; }
        public string? Received_Via { get; set; }
        public string? Name_Complaint { get; set; }
        public string? Description_Complaint { get; set; }
        public string? Walk_In_Insp_Name { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? Zone_Name { get; set; }
        public string? Community_Name { get; set; }
        public string? Building_Name { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
        public string? Role_Id { get; set; }
        public string? Login_Id { get; set; }
        public string? Target_Date { get; set; }
        public string? Service_Provider_Id { get; set; }
        public string? Service_Provider_Name { get; set; }
        public string? Action_Details { get; set; }
        public string? Closure_Action_Details { get; set; }
        public string? Closure_File_Path { get; set; }
        public string? If_Observation { get; set; }
        public string? Observation_Action_Details { get; set; }
        public string? Ob_Recommended_Action_Details { get; set; }
        public string? Ob_File_Path { get; set; }
        public string? If_Advisory_Notice { get; set; }
        public string? AN_Closure_Action_Details { get; set; }
        public string? AN_Closure_Remarks { get; set; }
        public string? AN_File_Path { get; set; }
        public Basic_Master_Data? Inc_Comman_Master_List { get; set; }
        public List<Insp_AN_Approval_History>? Insp_AN_Approval_History_List { get; set; }
    }
    public class Insp_AN_Approval_History
    {
        public string? History_Id { get; set; }
        public string? Complaint_Id { get; set; }
        public string? Emp_Name { get; set; }
        public string? Role_Name { get; set; }
        public string? Updated_DateTime { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
    }
    public class M_Get_Complaint_Register
    {
        public IReadOnlyCollection<M_Complaint_Register>? Get_All { get; set; }
        public M_Complaint_Register? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }
    #endregion

    #endregion

    #region [Create Schedule]
    public class Insp_Audit_Building_Model
    {
        public string? Building_Id { get; set; }
        public string? Sub_Building_Id { get; set; }
        public string? Building_Name { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public List<Audit_Insp_Schd_List>? _Scheduled_List { get; set; }
    }
    public class Audit_Insp_Schd_List
    {
        public string? Type_Id { get; set; }
        public string? Due_Date { get; set; }
        public string? Schedule_Type { get; set; }
        public string? First_Name { get; set; }
        public string? Insp_Request_Id { get; set; }
    }

    public class Get_Audit_Insp_Sch
    {
        public IReadOnlyCollection<Audit_Insp_Emp_List>? Get_All_Emp { get; set; }
        public IReadOnlyCollection<Insp_Audit_Building_Model>? Get_All_Sub { get; set; }
        public IReadOnlyCollection<Insp_Audit_Building_Model>? Get_All_List { get; set; }
        public Insp_Audit_Building_Model? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    public class Audit_Insp_Emp_List
    {
        public string? Employee_Identity_Id { get; set; }
        public string? First_Name { get; set; }
    }
    public class Audit_Insp_Table_List
    {
        public List<Audit_Insp_Row_Submit>? _Sch_List { get; set; }
    }
    public class Audit_Insp_Row_Submit
    {
        public string? Insp_Request_Id { get; set; }
        public string? Insp_Service_Provider_Id { get; set; }
        public string? Insp_HealthSafety_Id { get; set; }
        public string? Health_Safety_Type { get; set; }
        public string? Service_Provider_Type { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_Id { get; set; } //Sub_Building_Id
        public string? Inspection_Date { get; set; }
        public string? Date_of_Inspection { get; set; }
        public string? Schedule_Type { get; set; }
        public string? Access_Schedule { get; set; }
        public string? Status { get; set; }
        public string? Type_Id { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? Date_Time { get; set; }
        public string? Scope_Of_Work { get; set; }
        public string? Service_Provider_Id { get; set; }
        public string? Category_Id { get; set; }
        public string? Insp_Sch_Calendar_Id { get; set; }
    }
    public class Insp_Sch_Calendar_List
    {
        public List<Insp_Sch_Calendar>? _Sch_Calendar_List { get; set; }
    }
    public class Insp_Sch_Calendar
    {
        public string? Insp_Sch_Calendar_Id { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_Id { get; set; }
        public string? Initial_Date { get; set; }
        public string? Access_Schedule { get; set; }
        public string? Insp_Type { get; set; }
        public string? Frequency { get; set; }
        public string? Created_by { get; set; }
        public string? Service_Provider_Id { get; set; }
        public string? Scope_Work_Id { get; set; }
        public string? Category_Id { get; set; }
    }
    public class Get_Insp_Audit_Edit
    {
        public Audit_Insp_Row_Submit? Get_ById { get; set; }
    }

    public class Get_Serviceprovider
    {
        public IReadOnlyCollection<Audit_Insp_Emp_List>? Get_All { get; set; }
    }

    public class Update_Audit_Insp_Row_Submit
    {
        public string? Insp_Request_Id { get; set; }
        public string? Inspection_Date { get; set; }
        public string? Schedule_Type { get; set; }
        public string? Access_Schedule { get; set; }
        public string? Type_Id { get; set; }
        public string? Insp_Sch_Calendar_Id { get; set; }
        public string? Service_Provider_Id { get; set; }
        public string? Scope_Work_Id { get; set; }
        public string? Category_Id { get; set; }
    }

    public class Get_Insp_Service_Provider
    {
        public IReadOnlyCollection<Audit_Insp_Emp_List>? Get_All { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }

    public class Get_Scope_of_Work
    {
        public IReadOnlyCollection<SP_Scope_of_Work>? Get_All { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }

    public class SP_Scope_of_Work
    {
        public string? Scope_of_Work_Id { get; set; }
        public string? Scope_of_Work { get; set; }
        public string? Unique_Id { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
    }
    public class Get_Category
    {
        public IReadOnlyCollection<M_Insp_Category>? Get_All { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }

    #endregion

    #region [Dashboard]
    public class Insp_Dashboard_Param
    {
        public string? Year { get; set; }
        public string? Category_Name { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_Id { get; set; }
        public string? From_Date { get; set; }
        public string? To_date { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class M_InspectionDashboard
    {
        public Insp_Dash_Management? Insp_Dash_Management_Count { get; set; }
        public Insp_Dash_Types_Insp? Insp_Dash_Types_Insp_Count { get; set; }
        public List<Insp_Dash_ZoneWise_Graph>? Insp_Dash_ZoneWise_Count { get; set; }
        public List<Insp_Dash_ZoneWise>? Insp_Dash_CommunityWise_Count { get; set; }
        public List<Insp_Dash_ZoneWise>? Insp_Dash_CategoryWise_Count { get; set; }
        public List<Insp_Dash_ZoneWise>? Insp_Dash_SubCategoryWise_Count { get; set; }
        public Insp_Dash_Management? Insp_Dash_ServiceProvider_Count { get; set; }
        public List<Insp_Dash_Management_Graph>? Insp_Dash_Management_Graph { get; set; }
        public List<Insp_Dash_Management_Year_Graph>? Insp_Dash_Management_Year_Graph { get; set; }
        public Insp_Dash_Management? Insp_Dash_Mgmt_Schedule_Count { get; set; }
        public Insp_Dash_Management? Insp_Dash_Mgmt_WalkIn_Count { get; set; }
        public List<Insp_Dash_ZoneWise_Graph>? Insp_Dash_HSE_Officer_Count { get; set; }
    }
    public class Insp_Dash_ZoneWise_Graph
    {
        public string? Zone_Name { get; set; }
        public string? HSE_Officer { get; set; }
        public string? Planned { get; set; }
        public string? Completed { get; set; }
    }
    public class Insp_Dash_Management_Year_Graph
    {
        public string? Name { get; set; }
        public string? Year2030 { get; set; }
        public string? Year2029 { get; set; }
        public string? Year2028 { get; set; }
        public string? Year2027 { get; set; }
        public string? Year2026 { get; set; }
        public string? Year2025 { get; set; }
        public string? Year2024 { get; set; }
        public string? Year2023 { get; set; }
        public string? Year2022 { get; set; }
        public string? Year2021 { get; set; }
        public string? Year2020 { get; set; }
        public string? Year2019 { get; set; }
    }
    public class Insp_Dash_Management_Graph
    {
        public string? Name { get; set; }
        public string? January { get; set; }
        public string? February { get; set; }
        public string? March { get; set; }
        public string? April { get; set; }
        public string? May { get; set; }
        public string? June { get; set; }
        public string? July { get; set; }
        public string? August { get; set; }
        public string? September { get; set; }
        public string? October { get; set; }
        public string? November { get; set; }
        public string? December { get; set; }
    }
    public class Insp_Dash_Management
    {
        public string? Total_Insp_Persentage { get; set; }
        public string? Total_Insp_Count { get; set; }
        public string? Planned_Count { get; set; }
        public string? Closed_Count { get; set; }
        public string? Total_Actions_Count { get; set; }
        public string? Action_Open_Count { get; set; }
        public string? Action_Closed_Count { get; set; }
        public string? Action_Overdue_Count { get; set; }
        public string? Insp_Completed_Count { get; set; }
        public string? Findings_Overdue_Count { get; set; }
    }
    public class Insp_Dash_Types_Insp
    {
        public string? Spot_Count { get; set; }
        public string? Joint_Count { get; set; }
        public string? Leadership_Count { get; set; }
        public string? Handover_Count { get; set; }
        public string? Health_Safety_Count { get; set; }
        public string? Service_Provider_Count { get; set; }
        public string? Fire_Safety_Count { get; set; }
    }
    public class Insp_Dash_ZoneWise
    {
        public string? Value { get; set; }
        public string? Name { get; set; }
        public string? Id { get; set; }
    }
    public class M_Get_Insp_Dash_Management
    {
        public IReadOnlyCollection<M_InspectionDashboard>? Get_All { get; set; }
        public M_InspectionDashboard? Get_Data { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }

    public class Insp_Dash_Card_View_Data
    {
        public string? Insp_Request_Id { get; set; }
        public string? Unique_Id { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? Zone_Name { get; set; }
        public string? Community_Name { get; set; }
        public string? Building_Name { get; set; }
        public string? Inspection_Date { get; set; }
        public string? Reported_By { get; set; }
        public string? Status { get; set; }
        public string? Module_Name { get; set; }
        public string? Priority_Module { get; set; }
        public string? Assignee_Type { get; set; }
        public string? Responsibility_To { get; set; }
        public string? Target_Date { get; set; }
        public string? Assign_Action_HSE { get; set; }
        public string? Action { get; set; }
    }

    public class M_Get_Insp_Dash_Card_View_Data
    {
        public IReadOnlyCollection<Insp_Dash_Card_View_Data>? Get_All { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }

    public class M_Get_Insp_Dash_Filter_HSETeam
    {
        public IReadOnlyCollection<Insp_Dash_ZoneWise>? Get_All { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }

    #endregion


    #region [Landscape Inspection]

    public class M_Insp_Landscape : M_Common_Insp_Master
    {
        public string? Insp_Request_Id { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_Id { get; set; }
        public string? Inspection_Date { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? Zone_Name { get; set; }
        public string? Community_Name { get; set; }
        public string? Building_Name { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
        public string? Role_Id { get; set; }
        public string? Login_Id { get; set; }
        public string? Walk_In_Insp_Name { get; set; }
        public string? Company_Id { get; set; }
        public string? Company_Name { get; set; }
        public string? All_Service_Emp { get; set; }
        public string? All_Zone_Emp_Id { get; set; }
        public string? If_Closure_Access { get; set; }
        public string? Final_Approve_Comments { get; set; }
        public string? If_Corrective_Action { get; set; }
        public string? Responsible_Dept { get; set; }
        public Basic_Master_Data? Inc_Comman_Master_List { get; set; }
        public List<M_Insp_Landscap_Master_List>? Insp_landscap_Check_Master_List { get; set; }
        public List<Insp_Landscape_Qns>? Insp_landscap_Qns_List { get; set; }
        public List<Insp_Edit_Landscape>? Edit_Insp_landscap_List { get; set; }
        public List<M_Insp_Master_List>? Insp_landscap_SP_Company_List { get; set; }
        public List<M_Insp_Master_List>? Insp_landscap_Zone_Emp_List { get; set; }
        public List<Insp_landscap_History>? Insp_landscap_History_List { get; set; }

    }
    public class Insp_landscap_History
    {
        public string? History_Id { get; set; }
        public string? Insp_Request_Id { get; set; }
        public string? Emp_Name { get; set; }
        public string? Role_Name { get; set; }
        public string? Last_DateTime { get; set; }
        public string? CreatedDate { get; set; }
        public string? Status { get; set; }
        public string? Next_Action { get; set; }
        public string? Remarks { get; set; }
    }
    public class Insp_Edit_Landscape
    {
        public string? Insp_Request_Id { get; set; }
        public string? Insp_Landscap_Mas_Id { get; set; }
        public string? Insp_Landscap_Mas_Name { get; set; }
        public string? ROW_NUMBER { get; set; }
        public List<Insp_Landscape_Qns>? Edit_Insp_landscap_Sub_List { get; set; }
    }
    public class Insp_Landscape_Qns
    {
        public string? Landscape_Qns_Id { get; set; }
        public string? Insp_Request_Id { get; set; }
        public string? Insp_Landscap_Mas_Id { get; set; }
        public string? Insp_Landscap_Sub_Mas_Id { get; set; }
        public string? Insp_Landscap_Mas_Name { get; set; }
        public string? Insp_Landscap_Sub_Mas_Name { get; set; }
        public string? Qns_Action { get; set; }
        public string? Qns_Remarks { get; set; }
        public string? File_Path { get; set; }
        public string? CreatedBy { get; set; }
        public string? ROW_NUMBER { get; set; }
        public string? Closure_Description { get; set; }
        public string? Closure_File_Path { get; set; }
        public string? Closure_Emp_Id { get; set; }
        public string? Closure_Emp_Name { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public string? Reject_Reason { get; set; }
        public List<M_Insp_Land_CA_Reject>? Insp_Land_CA_Reject_List { get; set; }
    }
    public class M_Insp_Land_CA_Reject
    {
        public string? Insp_Land_CA_Reject_Id { get; set; }
        public string? Insp_Request_Id { get; set; }
        public string? Landscape_Qns_Id { get; set; }
        public string? Reject_Reason { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? Unique_Id { get; set; }
    }
    public class M_Insp_Landscap_Master_List
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
        public string? ROW_NUMBER { get; set; }
        public List<M_Insp_Master_List>? Insp_landscap_Check_Sub_Master_List { get; set; }
    }
    public class M_Insp_Master_List
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
        public string? ROW_NUMBER { get; set; }
    }
    public class M_Get_Insp_Landscape
    {
        public IReadOnlyCollection<M_Insp_Landscape>? Get_All { get; set; }
        public M_Insp_Landscape? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }
    public class M_Get_Insp_Emp_Master_List
    {
        public IReadOnlyCollection<M_Insp_Master_List>? Get_All { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }

    #endregion

}
