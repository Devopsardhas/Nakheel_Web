namespace Nakheel_Web.Models.Audit_Wel
{
    public class M_AuditWelfare : M_Audit_Welfare_Common
    {
        public string? Audit_Welfare_Sch_Id { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_Id { get; set; }
        public string? Audit_Welfare_Date { get; set; }
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
        public string? SP_Rep_Id { get; set; }
        public string? Team_Member_Access { get; set; }
        public string? Responsibility_To { get; set; }
        public string? CA_Target_Date { get; set; }
        public string? Safety_Violation { get; set; }
        public string? Service_Provider_Id { get; set; }
        public string? Service_Provider_Name { get; set; }
        public string? Building_Area_Name { get; set; }
        public string? CA_Access { get; set; }
        public AM_Welfare_Basic_Master_Data? AM_Welfare_Comman_Master_List { get; set; }
        public List<Am_Welfare_Audit_Team_Member>? Am_Welfare_Audit_Team_Member_List { get; set; }
        public List<Am_Welfare_Representative_Member>? Am_Welfare_Representative_Member_List { get; set; }
        public List<Am_Welfare_Topics_Master>? Am_Welfare_Topics_Master_List { get; set; }
        public List<Am_Welfare_Ques_Finding_Reject>? Am_Welfare_Ques_Finding_Reject_List { get; set; }
        public List<AM_Welfare_Dropdown_Values>? Am_Welfare_CA_Service_Provider_List { get; set; }
        public List<Am_Welfare_Corrective_Action>? Am_Welfare_Corrective_Action_List { get; set; }
        public List<Am_Welfare_NCR_Action>? Am_Welfare_NCR_Action_List { get; set; }
        public List<Am_Welfare_NCR_Root_Cause_Analysis>? Am_Welfare_NCR_Root_Cause_Analysis_List { get; set; }
        public List<Am_Welfare_NCR_Desc_Root_Cause>? Am_Welfare_NCR_Desc_Root_Cause_List { get; set; }
        public List<Am_Welfare_Add_Finding_Questionnaires>? Add_Finding_Questionnaires_List { get; set; }
        public List<Am_Welfare_Ques_Action_Reject_Reject>? Am_Welfare_Ques_Action_Reject_List { get; set; }
        public List<Am_Welfare_History_Approval>? Am_Welfare_History_Approval_List { get; set; }
    }
    public class Am_Welfare_Add_Finding_Questionnaires_List
    {
        public string? Audit_Welfare_Sch_Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? Safety_Violation { get; set; }
        public string? Building_Area { get; set; }
        public string? Audit_Team { get; set; }
        public List<Am_Welfare_Add_Finding_Questionnaires>? Add_Finding_Questionnaires_List { get; set; }
    }
    public class Am_Welfare_NCR_Root_Cause_Analysis
    {
        public string? Root_Cause_Analysis_Id { get; set; }
        public string? Am_Welfare_NCR_Action_Id { get; set; }
        public string? Audit_Welfare_Sch_Id { get; set; }
        public string? Root_Cause_Analysis_Name { get; set; }
    }
    public class Am_Welfare_NCR_Desc_Root_Cause
    {
        public string? Desc_Root_Cause_Id { get; set; }
        public string? Am_Welfare_NCR_Action_Id { get; set; }
        public string? Audit_Welfare_Sch_Id { get; set; }
        public string? Description_Root_Cause_Name { get; set; }
    }
    public class Am_Welfare_NCR_Action
    {
        public string? Am_Welfare_NCR_Action_Id { get; set; }
        public string? Audit_Welfare_Sch_Id { get; set; }
        public string? Non_Conformity_Desc { get; set; }
        public string? AS_Procedure_Require { get; set; }
        public string? Unique_Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedBy_Name { get; set; }
    }
    public class Am_Welfare_Corrective_Action
    {
        public string? Am_Welfare_CA_Id { get; set; }
        public string? Audit_Welfare_Sch_Id { get; set; }
        public string? Audit_Topics_Id { get; set; }
        public string? Audit_Sub_Topics_Id { get; set; }
        public string? Audit_Questionnaires_Id { get; set; }
        public string? Am_Welfare_Ques_Finding_Id { get; set; }
        public string? Responsibility_To { get; set; }
        public string? Responsibility_To_Name { get; set; }
        public string? Target_Date { get; set; }
        public string? Action_Description { get; set; }
        public string? Closure_Description { get; set; }
        public string? File_Path { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class Am_Welfare_Ques_Finding_Reject
    {
        public string? Am_Welfare_Ques_Rej_Id { get; set; }
        public string? Audit_Welfare_Sch_Id { get; set; }
        public string? Reject_Reason { get; set; }
        public string? Unique_Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedBy_Name { get; set; }
    }
    public class Am_Welfare_Add_Finding_Questionnaires
    {
        public string? Am_Welfare_Ques_Finding_Id { get; set; }
        public string? Audit_Welfare_Sch_Id { get; set; }
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
        public string? Actual { get; set; }
        public string? Target { get; set; }
        public string? Actual_Total { get; set; }
        public string? Target_Total { get; set; }
        public string? Percentage { get; set; }
        public string? Closure_File_Path { get; set; }
    }
    public class Am_Welfare_Topics_Master
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
        public string? Row_No { get; set; }
        public List<Am_Welfare_Sub_Topics_Master>? Am_Welfare_Sub_Topics_Master_List { get; set; }
        public List<Am_Welfare_Questionnaires_Master>? Am_Welfare_Questionnaires_Master_List { get; set; }
        public List<Am_Welfare_Corrective_Action>? Am_Welfare_Corrective_Action_List { get; set; }
    }
    public class Am_Welfare_Sub_Topics_Master
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
        public string? Row_No { get; set; }
        public List<Am_Welfare_Questionnaires_Master>? Am_Welfare_Questionnaires_Master_List { get; set; }
    }
    public class Am_Welfare_Questionnaires_Master
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
        public string? Row_No { get; set; }
        public string? Is_Questionnaires_Checked { get; set; }
        public string? Am_Welfare_Ques_Finding_Id { get; set; }
        public string? Objective_Evidence { get; set; }
        public string? Findings_Type_Name { get; set; }
        public string? Findings_Type_Name_Text { get; set; }
        public string? Find_Comments { get; set; }
        public string? Emp_Name { get; set; }
        public string? Closure_Description { get; set; }
        public string? File_Path { get; set; }
        public string? Actual { get; set; }
        public string? Target { get; set; }
        public string? Actual_Total { get; set; }
        public string? Target_Total { get; set; }
        public string? Percentage { get; set; }
        public string? Closure_File_Path { get; set; }
    }
    public class AM_Welfare_Basic_Master_Data
    {
        public List<AM_Welfare_Dropdown_Values>? Audit_Team_List { get; set; }
        public List<AM_Welfare_Dropdown_Values>? Welfare_Rep_List { get; set; }
    }
    public class AM_Welfare_Dropdown_Values
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
    }
    public class Am_Welfare_Audit_Team_Member : M_Audit_Welfare_Common
    {
        public string? Am_Welfare_Audit_Team_Id { get; set; }
        public string? Audit_Welfare_Sch_Id { get; set; }
        public string? Emp_Id { get; set; }
        public string? Emp_Name { get; set; }
    }
    public class Am_Welfare_Representative_Member : M_Audit_Welfare_Common
    {
        public string? Am_Welfare_Representative_Id { get; set; }
        public string? Audit_Welfare_Sch_Id { get; set; }
        public string? Emp_Id { get; set; }
        public string? Emp_Name { get; set; }
    }
    public class M_Get_AuditSp_Filter
    {
        public IReadOnlyCollection<AM_Welfare_Dropdown_Values>? Get_All { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }

    public class M_Get_AuditWelfare
    {
        public IReadOnlyCollection<M_AuditWelfare>? Get_All { get; set; }
        public M_AuditWelfare? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }


    public class M_Audit_Welfare_Common
    {
        public string? Unique_Id { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
        public string? Description { get; set; }

    }

    public class Am_Welfare_Ques_Action_Reject_Reject
    {
        public string? Am_Welfare_Qus_Action_Rej_Id { get; set; }
        public string? Audit_Welfare_Sch_Id { get; set; }
        public string? Reject_Reason { get; set; }
        public string? Unique_Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedBy_Name { get; set; }
    }

    public class Am_Welfare_History_Approval
    {
        public string? History_Id { get; set; }
        public string? Audit_Welfare_Sch_Id { get; set; }
        public string? Emp_Name { get; set; }
        public string? Role_Name { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public string? Last_DateTime { get; set; }
        public string? CreatedDate { get; set; }
        public string? Next_Action { get; set; }
    }
}
