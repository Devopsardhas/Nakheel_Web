using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Models.HandOverInsMaster;
using Nakheel_Web.Models.IncidentReport;
using Nakheel_Web.Models.InspectionMaster;
using System.ComponentModel.DataAnnotations;

namespace Nakheel_Web.Models.ControlOfWorkMaster
{
    public class PermitOfWorkModel
    {
    }
    public class Ptw_Confined_Space_Add
    {
        public string? CSP_Id { get; set; }
        public string? EWP_Id { get; set; }
        public string? Unique_Id { get; set; }
        public string? Date_Time_of_Observation { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? Zone_Id { get; set; }
        public string? Zone_Name { get; set; }
        public string? Community_Id { get; set; }
        public string? Community_Name { get; set; }
        public string? Building_Id { get; set; }
        public string? Building_Name { get; set; }
        public string? Business_Unit_Type { get; set; }
        public string? Company_Name { get; set; }
        public string? Contrator_Title_No { get; set; }
        public string? Competent_Person { get; set; }
        public string? Competent_Person_Name { get; set; }
        public string? Contractor_Name { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? Contact { get; set; }
        public string? Email_Id { get; set; }
        public string? Date_and_Time { get; set; }
        public string? Description_of_Work { get; set; }
        public string? Approved_Method { get; set; }
        public string? Additional_Precautions { get; set; }
        public string? Work_Duration_From_Date { get; set; }
        public string? Work_Duration_To_Date { get; set; }
        public string? Night_Schedule { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedBy_Name { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Location_Address { get; set; }
        public string? CreatedDate { get; set; }
        public string? Exact_Loc_Address { get; set; }
        public string? Contractor_Start_Date { get; set; }
        public string? Contractor_End_Date { get; set; }
        public string? DMS_No_Id { get; set; }
        public List<Confined_Space_Add_Ques>? _Add_CSP_Ques { get; set; }
        public Basic_Master_Data? CSP_Comman_Master_List { get; set; }
        public List<Ptw_History_of_Approval>? _Ptw_History_List { get; set; }
        public List<Confined_Space_Reject>? _Get_Zone_Reject_List { get; set; }
        public List<Ptw_Sp_Add_Evidence_Details>? _Evidence_Details { get; set; }
        public List<Ptw_Sp_Add_Renewal_Details>? _Get_Renewal_Details { get; set; }
        public List<Staff_Comptency_Files>? _Staff_Comptency_Files { get; set; }
        public List<Method_Statement_Files>? _Method_Statement_Files { get; set; }
        public List<Risk_Assess_Files>? _Risk_Assess_Files { get; set; }
        public List<Emr_Plan_Files>? _Emr_Plan_Files { get; set; }
        public List<HSE_Plan_Files>? _HSE_Plan_Files { get; set; }
        public List<Sp_Signed_Files>? _Sp_Signed_Files { get; set; }
        public List<Extent_Area_Affected_AddMore>? _Area_Affected { get; set; }
        public List<Extent_Personnel_Affected_AddMore>? _Personnel_Affected { get; set; }
        public List<Method_Isolation_AddMore>? _Method_Isolation { get; set; }
        public string? Isolation_Impair_Coordinate { get; set; }
        public string? Fire_Alarm_Chk { get; set; }
        public string? Fire_Protection_Chk { get; set; }
        public string? PAVA_Chk { get; set; }
        public string? Degree_Isolation { get; set; }
        public string? Reason_Isolation { get; set; }
        public string? During_Work_Hrs { get; set; }
        public string? Out_Work_Hrs { get; set; }
        public string? alter_fire_fight_option { get; set; }
        public string? Fire_watcher_Detail { get; set; }
        public string? Fire_Alarm_Chk_Name { get; set; }
        public string? Fire_Protection_Chk_Name { get; set; }
        public string? PAVA_Chk_Name { get; set; }
        public string? Competent_Number { get; set; }
        public string? HSE_Officer_Name { get; set; }
        public string? HSE_Mobile_Number { get; set; }
        public string? Declare_Name { get; set; }
        public string? Declare_Designation { get; set; }
        public string? Declare_Chk { get; set; }
    }
    public class Extent_Area_Affected_AddMore
    {
        public string? FS_Area_Affect_Id { get; set; }
        public string? CSP_Id { get; set; }
        public string? Extent_Area_Affected { get; set; }
    }
    public class Extent_Personnel_Affected_AddMore
    {
        public string? FS_Personnel_Affect_Id { get; set; }
        public string? CSP_Id { get; set; }
        public string? Extent_Personnel_Affected { get; set; }
    }
    public class Method_Isolation_AddMore
    {
        public string? FS_Method_Isolation_Id { get; set; }
        public string? CSP_Id { get; set; }
        public string? Method_Isolation { get; set; }
    }
    public class Staff_Comptency_Files
    {
        public string? File_Id { get; set; }
        public string? CSP_Id { get; set; }
        public string? EWP_Id { get; set; }
        public string? File_Path { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class Method_Statement_Files
    {
        public string? Method_File_Id { get; set; }
        public string? CSP_Id { get; set; }
        public string? EWP_Id { get; set; }
        public string? Method_File_Path { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class Risk_Assess_Files
    {
        public string? Risk_File_Id { get; set; }
        public string? CSP_Id { get; set; }
        public string? EWP_Id { get; set; }
        public string? Risk_File_Path { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class Emr_Plan_Files
    {
        public string? Emr_File_Id { get; set; }
        public string? CSP_Id { get; set; }
        public string? EWP_Id { get; set; }
        public string? Emr_File_Path { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class HSE_Plan_Files
    {
        public string? Hse_File_Id { get; set; }
        public string? CSP_Id { get; set; }
        public string? EWP_Id { get; set; }
        public string? Hse_File_Path { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class Sp_Signed_Files
    {
        public string? Sign_File_Id { get; set; }
        public string? CSP_Id { get; set; }
        public string? EWP_Id { get; set; }
        public string? Sign_File_Path { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class Confined_Space_Add_Ques
    {
        public string? Ques_CSP_ID { get; set; }
        public string? CSP_Id { get; set; }
        public string? Major_HSE_Work_Id { get; set; }
        public string? CSP_Ques_Name { get; set; }
        public string? CSP_Ques_Radio_Btn { get; set; }
        public string? CSP_Remarks { get; set; }
        public string? Ques_EWP_ID { get; set; }
        public string? EWP_Id { get; set; }
        public string? EWP_Ques_Name { get; set; }
        public string? EWP_Ques_Radio_Btn { get; set; }
        public string? EWP_Remarks { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class GET_CONFINED_WORK_PERMIT
    {
        public IReadOnlyCollection<Ptw_Confined_Space_Add>? Get_All { get; set; }
        public Ptw_Contractor_Load_Details? Get_ByAll { get; set; }
        public IReadOnlyCollection<Ptw_Contractor_Load_Details>? Get_All_Cont { get; set; }
        public Ptw_Confined_Space_Add? Get_All_Qn { get; set; }
        public Ptw_Confined_Space_Add? Get_ById { get; set; }
        public Ptw_Competent_Number? Get_By_Number { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }


    public class Confined_Space_Reject
    {
        public string? Unique_Id { get; set; }
        public string? CSP_Id { get; set; }
        public string? EWP_Id { get; set; }
        public string? Zone_Reject_Id { get; set; } //Request Zone Id
        public string? Hse_Reject_Id { get; set; } //Request Hse Id
        public string? Zone_Evd_Reject_Id { get; set; } //Evidence Zone Id
        public string? Zone_Renewal_Reject_Id { get; set; } //Renewal Zone Id
        public string? HSE_Evd_Reject_Id { get; set; } //Evidence HSE Id
        public string? CreatedBy { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public string? Zone_Approver_Name { get; set; } //Request Zone Id
        public string? Hse_Approver_Name { get; set; } //Request Hse Id
        public string? Zone_Evd_Approver_Name { get; set; } //Evidence Zone Id
        public string? Zone_Renewal_Approver_Name { get; set; } //Renewal Zone Id
        public string? HSE_Evd_Approver_Name { get; set; } //Evidence HSE Id
        public string? Designation { get; set; }
        public string? Date_Time { get; set; }
    }
    public class Ptw_History_of_Approval
    {
        public string? History_Id { get; set; }
        public string? CSP_Id { get; set; }
        public string? EWP_Id { get; set; }
        public string? Emp_Id { get; set; }
        public string? Role_Id { get; set; }
        public string? Updated_DateTime { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public string? Approve_Comments { get; set; }
        public List<Confined_Space_Reject>? _Zone_HSE_Reject { get; set; }
    }

    public class Ptw_Sp_Add_Evidence_Details
    {
        public string? CSP_Add_Id { get; set; }
        public string? CSP_Id { get; set; }
        public string? EWP_Id { get; set; }
        public string? Action_Radio_Btn { get; set; }
        public string? Competent_Person { get; set; }
        public string? Date_Time_Work_Comp { get; set; }
        public string? Date_Time_Safe_Return { get; set; }
        public string? Service_Provider_Name { get; set; }
        public string? Date_Time { get; set; }
        public string? CreatedBy { get; set; }
        public string? Service_Provider_Role { get; set; }
        public List<Ptw_History_of_Approval>? _Sp_Ptw_History_List { get; set; }
        public List<Ptw_Sp_Add_Personnel_Tools>? _Add_Personnel_Tools { get; set; }
        public List<Ptw_Sp_Add_Renewal_Evidence>? _Add_Renewal_Evidences { get; set; }
    }
    public class Ptw_Sp_Add_Personnel_Tools
    {
        public string? CSP_Pers_Tools_Id { get; set; }
        public string? CSP_Id { get; set; }
        public string? EWP_Id { get; set; }
        public string? CSP_Add_Id { get; set; }
        public string? Personnel_Tools_Count { get; set; }
        public string? Personnel_Tools_Name { get; set; }
        public string? CreatedBy { get; set; }
        public List<Ptw_Sp_Add_Closure_Evidence>? _Closure_Evidences { get; set; }
    }
    public class Ptw_Sp_Add_Closure_Evidence
    {
        public string? Closure_File_Id { get; set; }
        public string? CSP_Pers_Tools_Id { get; set; }
        public string? CSP_Id { get; set; }
        public string? EWP_Id { get; set; }
        public string? CSP_Add_Id { get; set; }
        public string? Closure_File_Path { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class Ptw_Sp_Add_Renewal_Evidence
    {
        public string? Evidence_File_Id { get; set; }
        public string? CSP_Id { get; set; }
        public string? EWP_Id { get; set; }
        public string? CSP_Add_Id { get; set; }
        public string? Evidence_File_Path { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class Ptw_Sp_Add_Renewal_Details
    {
        public string? CSP_Id { get; set; }
        public string? EWP_Id { get; set; }
        public string? CSP_Renewal_Id { get; set; }
        public string? Action_Radio_Btn { get; set; }
        public string? Renewal_From_Date { get; set; }
        public string? Renewal_To_Date { get; set; }
        public string? Remarks { get; set; }
        public string? Competent_Person { get; set; }
        public string? Service_Provider_Name { get; set; }
        public string? Service_DateTime { get; set; }
        public string? CreatedBy { get; set; }
        public string? Service_Provider_Role { get; set; }
        public List<Ptw_History_of_Approval>? Sp_Renewal_Ptw_History_List { get; set; }

    }
    public class Ptw_Contractor_Load_Details
    {
        public string? Official_Email_Id { get; set; }
        public string? Contract_Start_Date { get; set; }
        public string? Contract_End_Date { get; set; }
        public string? Company_Name { get; set; }
        public string? Purchase_Order_Number { get; set; }
        public string? SignUp_Id { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public List<Ptw_Competent_Person>? _Competent_Person { get; set; }
        public List<Ptw_Building>? _Building_List { get; set; }
    }
    public class Ptw_Competent_Person
    {
        public string? Work_Superviosor_Id { get; set; }
        public string? Name_Position { get; set; }
    }
    public class Ptw_Competent_Number
    {
        public string? Work_Superviosor_Id { get; set; }
        public string? Contact { get; set; }
    }
    public class Ptw_Building
    {
        public string? M_Service_Provider_Bu_Id { get; set; }
        public string? Building_Name { get; set; }
    }
}
