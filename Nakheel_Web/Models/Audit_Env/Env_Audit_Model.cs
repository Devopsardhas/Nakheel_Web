using Nakheel_Web.Models.AuditSp;
using Nakheel_Web.Models.ControlOfWorkMaster;

namespace Nakheel_Web.Models.Audit_Env
{
    public class Env_Audit_Model
    {
        public string? Env_Audit_Id { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? CreatedBy_Name { get; set; }                                
        public string? Zone_Id { get; set; }
        public string? Zone_Name { get; set; }
        public string? Community_Id { get; set; }
        public string? Community_Name { get; set; }
        public string? Building_Id { get; set; }
        public string? Building_Name { get; set; }
        public string? Date_Time { get; set; }
        public string? Lead_Auditor_Id { get; set; }
        public string? Lead_Auditor_Name { get; set; }
        public string? Insp_Type { get; set; }
        public string? Schedule_Type { get; set; }
        public string? Created_by { get; set; }
        public string? CreatedDate { get; set; }
        public string? Unique_Id { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
        public string? Audit_Team_Id { get; set; }
        public string? SP_Rep_Id { get; set; }
        public string? Created_by_Name { get; set; }
        public string? Team_Member_Access { get; set; }
        public string? CA_Target_Date { get; set; }
        public string? Safety_Violation { get; set; }
        public string? Building_Text { get; set; }
        public string? Service_Provider_Id { get; set; }
        public string? Service_Provider_Name { get; set; }
        public string? Role_Id { get; set; }
        public string? Login_Id { get; set; }
        public AM_ENV_Basic_Master_Data? _Env_Audit_Master { get; set; }
        public List<Am_Env_Auditor_Team_Member>? Am_Env_Audit_Team_Member_List { get; set; }
        public List<Am_Env_Service_Rep>? Am_Env_Service_Rep_List { get; set; }
        public List<Am_Env_Topics_Master>? Am_Sp_Topics_Master_List { get; set; }
        public List<Am_Env_Ques_Finding_Reject>? Am_Env_Ques_Reject_List { get; set; }
        public List<Am_Env_NCR_Action>? Am_Sp_NCR_Action_List { get; set; }
        public List<Am_Env_NCR_Root_Cause_Analysis>? Am_Sp_NCR_Root_Cause_Analysis_List { get; set; }
        public List<Am_Env_NCR_Desc_Root_Cause>? Am_Sp_NCR_Desc_Root_Cause_List { get; set; }
        public List<Am_Env_Audit_Add_Questionnaires>? Add_Finding_Questionnaires_List { get; set; }
        public List<Env_Audit_Reject>? _Reject_List { get; set; }
        public List<Env_Audit_History_Approval>? _History_List { get; set; }
        public List<Env_Audit_Evd_Files>? _Get_Evidence_List { get; set; }
    }

    public class AM_ENV_Basic_Master_Data
    {
        public List<AM_Env_Dropdown_Values>? Audit_Team_List { get; set; }
        public List<AM_Env_Dropdown_Values>? SP_Rep_List { get; set; }
    }

    public class AM_Env_Dropdown_Values
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
    }
    public class Am_Env_Auditor_Team_Member : M_Audit_Env_Common
    {
        public string? Env_Auditor_Team_Id { get; set; }
        public string? Env_Audit_Id { get; set; }
        public string? Emp_Id { get; set; }
        public string? Emp_Name { get; set; }
    }
    public class Am_Env_Service_Rep : M_Audit_Env_Common
    {
        public string? Env_Service_Rep_Id { get; set; }
        public string? Env_Audit_Id { get; set; }
        public string? Emp_Id { get; set; }
        public string? Emp_Name { get; set; }
    }
    public class M_Audit_Env_Common
    {
        public string? Unique_Id { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? Created_by { get; set; }
        public string? Updated_by { get; set; }
        public string? Remarks { get; set; }
        public string? Description { get; set; }

    }
    public class Get_Env_Audit_Edit
    {
        public Env_Audit_Model? Get_ById { get; set; }
    }
    public class Get_Env_Audit
    {
        public IReadOnlyCollection<Env_Audit_Model>? Get_All { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }
    public class Am_Env_Audit_Add_Questionnaires_List
    {
        public string? Safety_Violation { get; set; }
        public string? Env_Audit_Id { get; set; }
        public string? Created_by { get; set; }
        public List<Am_Env_Audit_Add_Questionnaires>? _Add_Questionnaires_List { get; set; }
        public List<Env_Audit_Evd_Files>? _List_Evd_Files { get; set; }
    }

    public class Am_Env_Audit_Add_Questionnaires
    {
        public string? Am_Sp_Ques_Finding_Id { get; set; }
        public string? Env_Audit_Id { get; set; }
        public string? Audit_Topics_Id { get; set; }
        public string? Audit_Sub_Topics_Id { get; set; }
        public string? Audit_Questionnaires_Id { get; set; }
        public string? Is_Questionnaires_Checked { get; set; }
        public string? Objective_Evidence { get; set; }
        public string? Findings_Type_Name { get; set; }
        public string? Find_Comments { get; set; }    
        public string? Closure_Description { get; set; }
        public string? File_Path { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class Env_Audit_Evd_Files
    {
        public string? Env_Audit_File_Id { get; set; }
        public string? Env_Audit_Id { get; set; }
        public string? Env_Ad_Env_File_Path { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class Am_Env_Ques_Finding_Reject
    {
        public string? Am_Env_Ques_Rej_Id { get; set; }
        public string? Env_Audit_Id { get; set; }
        public string? Reject_Reason { get; set; }
        public string? Unique_Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedBy_Name { get; set; }
    }

    public class Am_Env_Topics_Master
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
        public string? Row_No { get; set; }
        public List<Am_Env_Questionnaires_Master>? Am_Sp_Questionnaires_Master_List { get; set; }
        public List<Am_Env_Sub_Topics_Master>? Am_Sp_Sub_Topics_Master_List { get; set; }
    }
    public class Am_Env_Sub_Topics_Master
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
        public string? Row_No { get; set; }
        public List<Am_Env_Questionnaires_Master>? Am_Sp_Questionnaires_Master_List { get; set; }
    }
    public class Am_Env_Questionnaires_Master
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
        public string? Row_No { get; set; }
        public string? Am_Sp_Ques_Finding_Id { get; set; }
        public string? Is_Questionnaires_Checked { get; set; }
        public string? Objective_Evidence { get; set; }
        public string? Findings_Type_Name { get; set; }
        public string? Findings_Type_Name_Text { get; set; }
        public string? Find_Comments { get; set; }
        public string? Emp_Name { get; set; }
        public string? Safety_Violation { get; set; }
        public string? Closure_Description { get; set; }
        public string? File_Path { get; set; }
    }
    public class Am_Env_NCR_Action
    {
        public string? Am_Env_NCR_Action_Id { get; set; }
        public string? Env_Audit_Id { get; set; }
        public string? Non_Conformity_Desc { get; set; }
        public string? AS_Procedure_Require { get; set; }
        public string? Unique_Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedBy_Name { get; set; }

    }
    public class Am_Env_NCR_Root_Cause_Analysis
    {
        public string? Root_Cause_Analysis_Id { get; set; }
        public string? Am_Env_NCR_Action_Id { get; set; }
        public string? Env_Audit_Id { get; set; }
        public string? Root_Cause_Analysis_Name { get; set; }
    }
    public class Am_Env_NCR_Desc_Root_Cause
    {
        public string? Desc_Root_Cause_Id { get; set; }
        public string? Am_Env_NCR_Action_Id { get; set; }
        public string? Env_Audit_Id { get; set; }
        public string? Description_Root_Cause_Name { get; set; }
    }
    public class Env_Audit_Approval
    {
        public string? Env_Audit_Id { get; set; }
        public string? Env_Approve_Id { get; set; }
        public string? Emp_Id { get; set; }
        public string? Role_Id { get; set; }
        public string? Updated_DateTime { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public string? Approve_Comments { get; set; }
    }
    public class Env_Audit_Reject
    {
        public string? Am_Env_Ques_Rej_Id { get; set; }
        public string? Env_Audit_Id { get; set; }
        public string? Reject_Reason { get; set; }
        public string? Unique_Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? Status { get; set; }
    }
    public class Env_Audit_History_Approval
    {
        public string? History_Id { get; set; }
        public string? Env_Audit_Id { get; set; }
        public string? Emp_Id { get; set; }
        public string? Role_Id { get; set; }
        public string? History_DateTime { get; set; }
        public string? CreatedDate { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public string? Next_Action { get; set; }
        public string? Status_Name { get; set; }
    }

}
