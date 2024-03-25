using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Models.IncidentMaster;
using Nakheel_Web.Models.IncidentReport;
using Nakheel_Web.Models.Masters;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Nakheel_Web.Models.AuditMaster
{

    #region [Audit Category]
    public class M_Audit_Category : M_Common_Audit
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Audit_Category_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string? Audit_Category_Name { get; set; }
    }
    public class M_Get_Audit_Category
    {
        public IReadOnlyCollection<M_Audit_Category>? Get_All { get; set; }
        public M_Audit_Category? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    #endregion

    #region [Audit Topics]
    public class M_Audit_Topics : M_Common_Audit
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Audit_Topics_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Audit_Category_Id { get; set; }
        public string? Audit_Category_Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string? Audit_Topics_Name { get; set; }
    }
    public class M_Get_Audit_Topics
    {
        public IReadOnlyCollection<M_Audit_Topics>? Get_All { get; set; }
        public M_Audit_Topics? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    #endregion

    #region [Audit Sub Topics]
    public class M_Audit_Sub_Topics : M_Common_Audit
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Audit_Sub_Topics_Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string? Audit_Sub_Topics_Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Audit_Category_Id { get; set; }
        public string? Audit_Category_Name { get; set; }
        public List<M_Audit_Questionnaires>? Load_All_Quest { get; set; }
    }
    public class M_Get_Audit_Sub_Topics
    {
        public IReadOnlyCollection<M_Audit_Sub_Topics>? Get_All { get; set; }
        public M_Audit_Sub_Topics? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }
    #endregion

    #region [Audit Questionnaires]
    public class M_Audit_Questionnaires : M_Common_Audit
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Audit_Questionnaires_Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string? Audit_Questionnaires_Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Audit_Sub_Topics_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Audit_Category_Id { get; set; }
        public string? Audit_Category_Name { get; set; }
        public string? Audit_Sub_Topics_Name { get; set; }
        public string? Audit_Topics_Id { get; set; }
        public string? Audit_Topics_Name { get; set; }
        public List<M_Audit_Inspection_Details>? Aud_Ins_Details { get; set; }
    }
    public class M_Audit_Inspection_Details
    {
        public string? Findings_Id { get; set; }
        public string? Findings_Name { get; set; }
        public string? Findings_Type { get; set; }
    }
    public class M_Audit_QuestionnaireDetails
    {
        public string? Audit_Category_Id { get; set; }
        public string? Audit_Category_Name { get; set; }
        public string? Audit_Topics_Id { get; set; }
        public string? Audit_Topics_Name { get; set; }

        public List<M_Audit_Sub_Topics>? Audit_Sub_Topics { get; set; }

    }
    public class M_Get_Audit_Questionnaires
    {
        public IReadOnlyCollection<M_Audit_QuestionnaireDetails>? Get_All_Questionnaire { get; set; }
        public IReadOnlyCollection<M_Audit_Questionnaires>? Get_All { get; set; }
        public IReadOnlyCollection<M_Audit_Type>? Get_All_Type { get; set; }
        public M_Audit_Questionnaires? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    public class M_Audit_Type
    {
        public string? Audit_Type_Id { get; set; }
        public string? Audit_Type_Name { get; set; }
        public string? Unique_Id { get; set; }
    }
    public class M_Get_Audit_Findings
    {
        public IReadOnlyCollection<M_AuditFindings>? Get_All_Findings { get; set; }
        public List<M_AuditFindings>? Get_ById_Findings { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }
    public class M_AuditFindings : M_Common_Audit
    {
        public string? Audit_Category_Id { get; set; }
        public string? Audit_Category_Name { get; set; }
        public string? Findings_Id { get; set; }
        public string? Findings_Name { get; set; }
        public string? Findings_Type { get; set; }
    }
    #endregion

    #region [Service Provider]
    public class Audit_Schedule_Model : M_Common_Audit
    {
        public string? Audit_Category_Id { get; set; }
        public string? Audit_Sch_Id { get; set; }
        public string? DateTime { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Zone_Name { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Master_Name { get; set; }
        public string? Community_Id { get; set; }
        public string? Lead_Auditor_Id { get; set; }
        public string? Lead_Auditor_Name { get; set; }
        public string? Business_Unit_Type { get; set; }
        public string? Role_Id { get; set; }
        public string? Audit_Lati { get; set; }
        public string? Audit_Long { get; set; }
        public List<Audit_Team>? Audit_Team_List { get; set; }
        public List<Audit_Service_Provider>? Service_Prov_List { get; set; }
        public List<Add_Audit_Checklist>? add_Audit_Chk_List { get; set; }
    }

    public class Add_Audit_Checklist : M_Common_Audit
    {
        public string? Audit_Clause_Id { get; set; }
        public string? Audit_Sch_Id { get; set; }
        public string? Audit_Topics_Id { get; set; }
        public string? Audit_Topics_Name { get; set; }
        public string? Audit_Quest_Id { get; set; }
        public string? Audit_Verifi_Details { get; set; }
        public string? Site_Inspection_Details { get; set; }
        public string? Audit_Findings_ID { get; set; }
        public List<ServiceProv_Checlist_File_Upl>? Checklist_File_Upl { get; set; }
    }

    public class ServiceProv_Checlist_File_Upl : M_Common_Audit
    {
        public string? File_Upl_Id { get; set; }
        public string? Audit_Sch_Id { get; set; }
        public string? Audit_Clause_Id { get; set; }
        public string? File_Path { get; set; }
    }
    public class Audit_Team : M_Common_Audit
    {
        public string? Audit_Team_Id { get; set; }
        public string? Audit_Sch_Id { get; set; }
        public string? Audit_Emp_Id { get; set; }
        public string? Audit_Emp_Name { get; set; }
    }
    public class Audit_Service_Provider : M_Common_Audit
    {
        public string? Service_Prov_Id { get; set; }
        public string? Audit_Sch_Id { get; set; }
        public string? Service_Prov_Emp_Id { get; set; }
        public string? Service_Prov_Emp_Name { get; set; }
    }
    public class GET_SERVICE_PROVIDER_AUDIT
    {
        public IReadOnlyCollection<Audit_Schedule_Model>? Get_All { get; set; }
        public Audit_Schedule_Model? Get_ById { get; set; }
        public List<Audit_Schedule_Model>? Get_By_Sch { get; set; }
        public List<Audit_Schedule_Model>? Get_ByID_Chk { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }

    }
    #endregion

    #region [Audit Schedule - Internal Audit]

    public class Aud_Internal_Audit : M_Common_Audit
    {
        public string? Internal_Audit_Id { get; set; }
        public string? Reported_by { get; set; }
        public string? Designation { get; set; }
        public string? Date_Time { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Business_Unit_Type_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Lead_Auditor_Id { get; set; }
        public string? Aud_Lat { get; set; }
        public string? Aud_Long { get; set; }
        public string? Remarks_1 { get; set; }
        public string? Remarks_2 { get; set; }
        public string? Remarks_3 { get; set; }
        public string? Remarks_4 { get; set; }
        public string? Role_Id { get; set; }
        public string? Login_Id { get; set; }
        public List<Aud_Internal_Audit_Team>? L_Aud_Internal_Audit_Team { get; set; }
        public List<Aud_Internal_Service_Provider_Team>? L_Aud_Internal_Service_Provider_Team { get; set; }
    }
    public class Aud_Internal_Audit_Team : M_Common_Audit
    {
        public string? Internal_Audit_Team_Id { get; set; }
        public string? Internal_Audit_Id { get; set; }
        public string? Auditor_Id { get; set; }
    }
    public class Aud_Internal_Service_Provider_Team : M_Common_Audit
    {
        public string? Internal_Service_Provider_Team_Id { get; set; }
        public string? Internal_Audit_Id { get; set; }
        public string? Service_Provider_Id { get; set; }
    }
    public class Get_Aud_Internal_Audit : M_Common_Fields
    {
        public IReadOnlyCollection<Aud_Internal_Audit>? Get_All_Aud_Internal_Audit { get; set; }
        public Aud_Internal_Audit? Getby_Aud_Internal_Audit { get; set; }
        public IReadOnlyCollection<Internal_Aud_CheckList>? GetbyId_Aud_Internal_Audit_Checklist { get; set; }
        public Aud_Internal_NCR_Form? Getby_Aud_Internal_NCR_Form { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }
    public class Internal_Aud_CheckList : M_Common_Fields
    {
        public string? Clause_Id { get; set; }
        public string? Questionnaire_Id { get; set; }
        public string? Internal_Audit_Id { get; set; }
        public string? Auditor_Verification_Details { get; set; }
        public string? Site_Inspection_Details { get; set; }
        public string? Findings { get; set; }
        public string? Internal_Topic { get; set; }
        public string? Internal_Sub_Topic { get; set; }
        public string? Is_NCR_Checked { get; set; }
        public string? Remarks_1 { get; set; }
        public string? Remarks_2 { get; set; }
        public List<Aud_Internal_Evidence_Photos>? L_Aud_Internal_Evidence_Photos { get; set; }
    }
    public class Aud_Internal_Evidence_Photos
    {
        public string? Evidence_Id { get; set; }
        public string? Clause_Id { get; set; }
        public string? Internal_Audit_Id { get; set; }
        public string? Evidence_Path { get; set; }
    }

    public class Aud_Internal_NCR_Form : M_Common_Audit
    {
        public string? NCR_Report_Id { get; set; }
        public string? Int_Audit_Id { get; set; }
        public string? Questionnaires_Id { get; set; }
        public string? Non_Conformity_Description { get; set; }
        public string? Applicable_Standard_Procedure { get; set; }
        public string? Responsibility { get; set; }
        public string? Completion_Date { get; set; }
        public string? Target_Date { get; set; }
        public string? Authorised_by { get; set; }
        public string? Remarks_1 { get; set; }
        public string? Remarks_2 { get; set; }
        public string? Remarks_3 { get; set; }
        public string? Remarks_4 { get; set; }
        public List<L_NCR_Root_Cause_Analysis>? L_NCR_Root_Cause_Analysis { get; set; }
        public List<L_NCR_Corrective_Action>? L_NCR_Corrective_Action { get; set; }
    }

    public class L_NCR_Root_Cause_Analysis
    {
        public string? Root_Cause_Analysis_Id { get; set; }
        public string? NCR_Report_Id { get; set; }
        public string? Int_Audit_Id { get; set; }
        public string? Root_Cause_Analysis_Name { get; set; }
        public string? Root_Cause_Analysis_Description { get; set; }
        public string? Remarks_1 { get; set; }
        public string? Remarks_2 { get; set; }
        public string? Remarks_3 { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class L_NCR_Corrective_Action
    {
        public string? Corrective_Action_Id { get; set; }
        public string? NCR_Report_Id { get; set; }
        public string? Int_Audit_Id { get; set; }
        public string? Corrective_Action_Name { get; set; }
        public string? Corrective_Action_Description { get; set; }
        public string? Remarks_1 { get; set; }
        public string? Remarks_2 { get; set; }
        public string? Remarks_3 { get; set; }
        public string? CreatedBy { get; set; }
    }

    #endregion
}
