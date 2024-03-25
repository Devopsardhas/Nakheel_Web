using Nakheel_Web.Models.IncidentReport;
using Nakheel_Web.Models.InspectionMaster;
using Nakheel_Web.Models.Masters;
using Nakheel_Web.Models.SecurityIncidentMaster;

namespace Nakheel_Web.Models.SecurityIncidentReport
{
    public class M_Sec_Incident_Report
    {
        public string? Sec_Inc_Report_Id { set; get; }
        public string? Sec_Inc_Category_Id { set; get; }
        public string? Sec_Sub_Cat_Id { set; get; }
        public string? Sec_Inc_Severity { set; get; }
        public string? Sec_Inc_Complaint { set; get; }
        public string? Sec_Inc_Method { set; get; }
        public string? Zone_Id { set; get; }
        public string? Zone_Name { set; get; }
        public string? Community_Id { set; get; }
        public string? Community_Name { set; get; }
        public string? Building_Id { set; get; }
        public string? Building_Name { set; get; }
        public string? Location_Id { set; get; }
        public string? Location_Name { set; get; }
        public string? Sub_Location_Id { set; get; }
        public string? Sub_Location_Name { set; get; }
        public string? Sec_Inc_Occur { set; get; }
        public string? Sec_ReportedBy { set; get; }
        public string? Incident_Date { set; get; }
        public string? Incident_Time { set; get; }
        public string? Sec_Reported_To { set; get; }
        public string? Sec_Assigned_To { set; get; }
        public string? Sec_Related_Incident { set; get; }
        public string? Sec_Action { set; get; }
        public string? Sec_Comments { set; get; }
        public string? Sec_Recommended { set; get; }
        public string? Sec_Escalation { set; get; }
        public string? Sec_Further_Action { set; get; }
        public string? Sec_Inc_Status { set; get; }
        public string? Sec_Search_key { set; get; }
        public string? Sec_Medical_Assistance { set; get; }
        public string? Sec_Provider { set; get; }
        public string? Sec_Police_Enquire { set; get; }
        public string? Sec_Police { set; get; }
        public string? Sec_Recovery_Vechicle { set; get; }
        public string? Sec_CID { set; get; }
        public string? Sec_Ambulance { set; get; }
        public string? Sec_Ambulance_no { set; get; }
        public string? Sec_Injure_Person { set; get; }
        public string? Sec_Address { set; get; }
        public string? Sec_Approxi_Age { set; get; }
        public string? Sec_Witnesses { set; get; }
        public string? Sec_Damage { set; get; }
        public string? Sec_Details_Damage { set; get; }
        public string? Sec_DeptSection { set; get; }
        public string? Sec_Inc_Category_Name { set; get; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedBy_Name { get; set; }
        public string? Role_Id { get; set; }
        public string? Unique_Id { get; set; }
        public string? Recommended { get; set; }
        public string? Action_Taken { get; set; }
        public string? Comments { get; set; }
        public Basic_Security_Master_Data? Sec_Comman_Master_List { get; set; }
        public List<M_Sec_Incident_ActionTaken>? L_M_Security_ActionTaken { get; set; }
        public List<M_Sec_Incident_FollowUpAction>? L_M_Sec_Incident_FollowUp { get; set; }
        public List<M_Sec_FollowUp_Attachments>? L_M_Sec_FollowUp_Attach { get; set; }
        public List<M_Sec_FollowUp_AssignEmp>? L_M_Sec_FollowUp_ReportedTo { get; set; }
        public List<M_Sec_FollowUp_AssignEmp>? L_M_Sec_FollowUp_AssignedTo { get; set; }
        public List<M_Sec_Inc_Photos>? L_Sec_Inc_Photos { get; set; }
        public List<M_Sec_Inc_Videos>? L_Sec_Inc_Videos { get; set; }
        //public List<M_Sec_Investigation_Find>? L_Sec_Investigation_Finding { get; set; }
    }
    public class Basic_Security_Master_Data
    {
        public List<Sec_Dropdown_Values>? Zone_Master_List { get; set; }
        public List<Sec_Dropdown_Values>? Community_Master_List { get; set; }
        public List<Sec_Dropdown_Values>? Building_Master_List { get; set; }
        public List<Sec_Dropdown_Values>? Location_Master_List { get; set; }
        public List<Sec_Dropdown_Values>? Sub_Location_Master_List { get; set; }
        public List<Sec_Dropdown_Values>? Category_Master_List { get; set; }
    }
    public class Sec_Dropdown_Values
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
    }
    public class M_Sec_Inc_Photos : M_Common_Fields
    {
        public string? Sec_Inc_Photo_Id { get; set; }
        public string? Sec_Inc_Report_Id { get; set; }
        public string? Photo_File_Path { get; set; }
    }
    public class M_Sec_Inc_Videos : M_Common_Fields
    {
        public string? Sec_Inc_Video_Id { get; set; }
        public string? Sec_Inc_Report_Id { get; set; }
        public string? Video_File_Path { get; set; }
    }
    public class GET_SECURITY_INCIDENT
    {
        public IReadOnlyCollection<M_Sec_Incident_Report>? Get_All { get; set; }
        public M_Sec_Incident_Report? Get_ById { get; set; }
        public IReadOnlyCollection<M_Sec_Location_List>? Get_Location_List { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }

    }
    public class M_Sec_Investigation_Find
    {
        public string? Inves_Find_Id { get; set; }
        public string? Sec_Inc_Report_Id { get; set; }
        public string? Sec_Doc_Type { get; set; }
        public string? Sec_Description { get; set; }
        public string? Sec_File_Upload { get; set; }
        public string? Other_Document { get; set; }
        public string? Status { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class M_Sec_Investigation_Find_List : M_Common_Fields
    {
        public List<M_Sec_Investigation_Find>? L_Sec_Invest_Finding { get; set; }
    }
    public class M_Sec_Incident_ActionTaken
    {
        public string? ActionTaken_Id { get; set; }
        public string? Sec_Inc_Report_Id { get; set; }
        public string? Action_Taken { get; set; }
        public string? Comments { get; set; }
        public string? Recommended { get; set; }
        public string? File_Path { get; set; }
        public string? ReportedBy { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? Role_Id { get; set; }
    }
    public class M_Sec_Incident_FollowUpAction
    {
        public string? FollowUp_Id { get; set; }
        public string? Sec_Inc_Report_Id { get; set; }
        public string? Reference_No { get; set; }
        public string? Action_Details { get; set; }
        public string? Photo_File_Path { get; set; }
        public string? Video_File_Path { get; set; }
        public string? File_Path { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? Role_Id { get; set; }
        public List<M_Sec_FollowUp_Attachments>? L_M_Sec_FollowUp_Attachments { get; set; }
    }
    public class M_Sec_FollowUp_AssignEmp
    {
        public string? Sec_Inc_Report_Id { get; set; }
        public string? Emp_Id { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class M_Sec_FollowUp_Attachments
    {
        public string? File_Id { get; set; }
        public string? Sec_Inc_Report_Id { get; set; }
        public string? FollowUp_Id { get; set; }
        public string? File_Path { get; set; }
        public string? CreatedBy { get; set; }
        public string? Role_Id { get; set; }
    }
    public class M_Sec_Location_List
    {
        public string? Location_Id { get; set; }
        public string? Location_Name { get; set; }
        public string? Sub_Location_Id { get; set; }
        public string? Sub_Location_Name { get; set; }
        public string? Description { get; set; }
        public string? CreatedDate { get; set; }
    }
}