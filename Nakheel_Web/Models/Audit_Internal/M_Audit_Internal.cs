using Nakheel_Web.Models.AuditSp;

namespace Nakheel_Web.Models.Audit_Internal
{
    public class M_Audit_Internal: M_Audit_Sp_Common
    {
        public string? Audit_Internal_Id { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_Id { get; set; }
        public string? Audit_Int_Date { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? Zone_Name { get; set; }
        public string? Community_Name { get; set; }
        public string? Building_Name { get; set; }
        public string? Audit_Type_Name { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
        public string? Role_Id { get; set; }
        public string? Login_Id { get; set; }
        public string? Walk_In_Insp_Name { get; set; }
        public string? Access_Schedule { get; set; }
        public string? Access_Schedule_Name { get; set; }
        public string? Designation_Name { get; set; }
        public string? CreatedBy_Name { get; set; }
        public string? Audit_Team_Id { get; set; }
        public string? Audit_Team_Name { get; set; }
        public string? Finding_Id { get; set; }
        public string? SP_Rep_Id { get; set; }
        public string? Team_Member_Access { get; set; }
        public string? SP_Access { get; set; }
        public string? Responsibility_To { get; set; }
        public string? CA_Target_Date { get; set; }
        public string? Raise_NCR { get; set; }
        public string? Safety_Violation { get; set; }
        public string? Service_Provider_Id { get; set; }
        public string? Service_Provider_Name { get; set; }
        public string? Building_Area_Name { get; set; }
        public string? CA_Access { get; set; }
        public string? Closure_Description { get; set; }
        public string? File_Path { get; set; }
        public AM_Internal_Basic_Master_Data? Int_Comman_Master_List { get; set; }
        public List<AM_Internal_Audit_Team_Member>? Int_Audit_Team_Member_List { get; set; }
        public List<AM_Internal_Representative_Member>? Int_Rep_Member_List { get; set; }
        public List<AM_Internal_Topics_Master>? Int_Topics_Master_List { get; set; }
        public List<AM_Internal_Ques_Finding_Reject>? Int_Ques_Finding_Reject_List { get; set; }
        public List<AM_Internal_Dropdown_Values>? Int_CA_Service_Provider_List { get; set; }
        public List<AM_Internal_Corrective_Action>? Int_Corrective_Action_List { get; set; }
        public List<AM_Internal_NCR_Action>? Int_NCR_Action_List { get; set; }
        public List<AM_Internal_NCR_Root_Cause_Analysis>? Int_NCR_Root_Cause_Analysis_List { get; set; }
        public List<AM_Internal_NCR_Desc_Root_Cause>? Int_NCR_Desc_Root_Cause_List { get; set; }
        public List<AM_Internal_Add_Finding_Qns>? Add_Finding_Qns_List { get; set; }
        public List<AM_Internal_Qns_Action_Reject_Reject>? Int_Ques_Action_Reject_List { get; set; }
        public List<AM_Internal_History_Approval>? Int_History_Approval_List { get; set; }
    }
    public class AM_Internal_History_Approval
    {
        public string? History_Id { get; set; }
        public string? Audit_Internal_Id { get; set; }
        public string? Emp_Name { get; set; }
        public string? Role_Name { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public string? Last_DateTime { get; set; }
        public string? CreatedDate { get; set; }
        public string? Next_Action { get; set; }
    }
    public class AM_Internal_Add_Finding_Qns_List
    {
        public string? Audit_Internal_Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? Building_Area_Name { get; set; }
        public string? Raise_NCR { get; set; }
        public List<AM_Internal_Add_Finding_Qns>? AM_Int_Findings_Master_List { get; set; }
        public List<AM_Internal_Representative_Member>? Int_Rep_Member_List { get; set; }
    }
    public class AM_Internal_NCR_Action
    {
        public string? Am_Sp_NCR_Action_Id { get; set; }
        public string? Audit_Internal_Id { get; set; }
        public string? Non_Conformity_Desc { get; set; }
        public string? AS_Procedure_Require { get; set; }
        public string? Unique_Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedBy_Name { get; set; }

    }
    public class AM_Internal_NCR_Root_Cause_Analysis
    {
        public string? Root_Cause_Analysis_Id { get; set; }
        public string? Am_Sp_NCR_Action_Id { get; set; }
        public string? Audit_Internal_Id { get; set; }
        public string? Root_Cause_Analysis_Name { get; set; }
    }
    public class AM_Internal_NCR_Desc_Root_Cause
    {
        public string? Desc_Root_Cause_Id { get; set; }
        public string? Am_Sp_NCR_Action_Id { get; set; }
        public string? Audit_Internal_Id { get; set; }
        public string? Description_Root_Cause_Name { get; set; }
    }
    public class AM_Internal_Corrective_Action
    {
        public string? Am_Sp_CA_Id { get; set; }
        public string? Audit_Internal_Id { get; set; }
        public string? Audit_Topics_Id { get; set; }
        public string? Audit_Sub_Topics_Id { get; set; }
        public string? Audit_Questionnaires_Id { get; set; }
        public string? Am_Sp_Ques_Finding_Id { get; set; }
        public string? Responsibility_To { get; set; }
        public string? Responsibility_To_Name { get; set; }
        public string? Target_Date { get; set; }
        public string? Action_Description { get; set; }
        public string? Closure_Description { get; set; }
        public string? File_Path { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class AM_Internal_Ques_Finding_Reject
    {
        public string? Am_Sp_Ques_Rej_Id { get; set; }
        public string? Audit_Internal_Id { get; set; }
        public string? Reject_Reason { get; set; }
        public string? Unique_Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedBy_Name { get; set; }
    }
    public class AM_Internal_Qns_Action_Reject_Reject
    {
        public string? Am_Sp_Qus_Action_Rej_Id { get; set; }
        public string? Audit_Internal_Id { get; set; }
        public string? Reject_Reason { get; set; }
        public string? Unique_Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedBy_Name { get; set; }
    }
    public class AM_Internal_Add_Finding_Qns
    {
        public string? Finding_Id { get; set; }
        public string? Audit_Internal_Id { get; set; }
        public string? Audit_Topics_Id { get; set; }
        public string? Audit_Sub_Topics_Id { get; set; }
        public string? Audit_Questionnaires_Id { get; set; }
        public string? Is_Questionnaires_Checked { get; set; }
        public string? Objective_Evidence { get; set; }
        public string? Find_Comments { get; set; }
        public string? Findings_Type_Name { get; set; }
        public string? CreatedBy { get; set; }
        public string? Closure_Description { get; set; }
        public string? File_Path { get; set; }
        public string? Operation_Team_Id { get; set; }
    }
    public class AM_Internal_Topics_Master
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
        public string? Row_No { get; set; }
        public List<AM_Internal_Sub_Topics_Master>? Am_Sp_Sub_Topics_Master_List { get; set; }
        public List<AM_Internal_Qns_Master>? AM_Int_Qns_Master_List { get; set; }
        public List<AM_Internal_Corrective_Action>? Am_Sp_Corrective_Action_List { get; set; }
    }
    public class AM_Internal_Sub_Topics_Master
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
        public string? Row_No { get; set; }
        public List<AM_Internal_Qns_Master>? AM_Int_Qns_Master_List { get; set; }
    }
    public class AM_Internal_Qns_Master
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
        public string? Row_No { get; set; }
        public string? Is_Questionnaires_Checked { get; set; }
        public string? Finding_Id { get; set; }
        public string? Objective_Evidence { get; set; }
        public string? Findings_Type_Name { get; set; }
        public string? Findings_Type_Name_Text { get; set; }
        public string? Find_Comments { get; set; }
        public string? Emp_Name { get; set; }
        public string? Closure_Description { get; set; }
        public string? File_Path { get; set; }
        public string? Operation_Team_Id { get; set; }
        public string? CA_Access { get; set; }
        public string? Status { get; set; }
    }
    public class AM_Internal_Basic_Master_Data
    {
        public List<AM_Internal_Dropdown_Values>? Audit_Team_List { get; set; }
        public List<AM_Internal_Dropdown_Values>? SP_Rep_List { get; set; }
    }
    public class AM_Internal_Audit_Team_Member : M_Audit_Sp_Common
    {
        public string? AM_Audit_Team_Id { get; set; }
        public string? Audit_Internal_Id { get; set; }
        public string? Emp_Id { get; set; }
        public string? Emp_Name { get; set; }
    }
    public class AM_Internal_Representative_Member : M_Audit_Sp_Common
    {
        public string? AM_Rep_Id { get; set; }
        public string? Audit_Internal_Id { get; set; }
        public string? Emp_Id { get; set; }
        public string? Emp_Name { get; set; }
    }
    public class AM_Internal_Dropdown_Values
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
    }
    public class Am_Internal_Audit_Team_Member : M_Audit_Sp_Common
    {
        public string? AM_Audit_Team_Id { get; set; }
        public string? Audit_Internal_Id { get; set; }
        public string? Emp_Id { get; set; }
        public string? Emp_Name { get; set; }
    }
    public class M_Get_AuditInternal_Filter
    {
        public IReadOnlyCollection<AM_Internal_Dropdown_Values>? Get_All { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }

    public class M_Get_AuditInternal
    {
        public IReadOnlyCollection<M_Audit_Internal>? Get_All { get; set; }
        public M_Audit_Internal? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }
}
