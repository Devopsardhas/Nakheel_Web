using Nakheel_Web.Models.Masters;

namespace Nakheel_Web.Models.IncidentReport
{
    public class M_IncidentObservationReport
    {
        public string? Inc_Obser_Report_Id { get; set; }
        public string? Obser_Date { get; set; }
        public string? Obser_Time { get; set; }
        public string? Zone_Id { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Business_Unit_Type_Id { get; set; }
        public string? Business_Unit_Type_Name { get; set; }
        public string? Building_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Master_Community_Id { get; set; }
        public string? Category { get; set; }
        public string? Observation_Type { get; set; }
        public string? Loc_Latitude { get; set; }
        public string? Loc_Longitude { get; set; }
        public string? Obser_Details { get; set; }
        public string? Description_Observation { get; set; }
        public string? Imm_Corrective_Actions { get; set; }
        public string? Company_Name { get; set; }
        public string? Contact_Name { get; set; }
        public string? Mobile_Number { get; set; }
        public string? Unique_Id { get; set; }
        public string? Status { get; set; }
        public string? Zone_Name { get; set; }
        public string? Building_Name { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? Community_Name { get; set; }
        public string? Master_Community_Name { get; set; }
        public string? Priority { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
        public string? Role_Id { get; set; }
        public string? Last_Reported_By { get; set; }
        public List<M_Inc_Health_Observation_Type>? L_Inc_Health_Observation_Type { get; set; }
        public List<M_Inc_Environment_Observation_Type>? L_Inc_Environment_Observation_Type { get; set; }
        public List<M_Inc_Observation_Photos>? L_Inc_Observation_Photos { get; set; }
        public List<M_Inc_Observation_Videos>? L_Inc_Observation_Videos { get; set; }
        public List<Observation_Status_History>? L_Observation_Status_History { get; set; }
        public List<M_Observation_Corrective_Action>? L_Observation_Report_Reject { get; set; }
        public List<M_Observation_Corrective_Action>? L_Observation_Report_CA { get; set; }
    }
    public class GET_OBSERVATION_REPORT
    {
        public IReadOnlyCollection<M_IncidentObservationReport>? Get_All_Observation_Report { get; set; }
        public M_IncidentObservationReport? Get_Observation_Report { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }
    public class M_Inc_Health_Observation_Type 
    {
        public string? Inc_Obser_Health_Id { get; set; }
        public string? Inc_Obser_Report_Id { get; set; }
        public string? Health_Safety_Id { get; set; }
        public string? Health_Safety_Name { get; set; }
    }
    public class M_Inc_Environment_Observation_Type 
    {
        public string? Inc_Envir_ObserType_Id { get; set; }
        public string? Inc_Obser_Report_Id { get; set; }
        public string? Environment_Id { get; set; }
        public string? Environment_Name { get; set; }
    }
    public class M_Inc_Observation_Photos 
    {
        public string? Obs_Photo_Id { get; set; }
        public string? Inc_Obser_Report_Id { get; set; }
        public string? Photo_File_Path { get; set; }
    }
    public class M_Inc_Observation_Videos 
    {
        public string? Obs_Video_Id { get; set; }
        public string? Inc_Obser_Report_Id { get; set; }
        public string? Video_File_Path { get; set; }
    }
    public class M_Inc_Observation_Team
    {
        public string? Inc_Obser_Report_Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? Role_Id { get; set; }
        public List<M_Inc_Observation_Emp>? L_M_Inc_Observation_Emp { get; set; }
    }

    public class M_Inc_Observation_Emp
    {
        public string? Obs_Corrective_Action_Id { get; set; }
        public string? Inc_Obser_Report_Id { get; set; }
        public string? Responsible_Id { get; set; }
        public string? Target_Date { get; set; }
        public string? Action_Description { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class M_Observation_Corrective_Action
    {
        public string? Inc_Obser_Report_Id { get; set; }
        public string? Obser_UId { get; set; }
        public string? Corrective_Action_Id { get; set; }
        public string? Obs_Corrective_Action_Id { get; set; }
        public string? Inc_Observation_Id { get; set; }
        public string? Description_Action { get; set; }
        public string? Photo_File_Path { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
        public string? Status { get; set; }
        public string? Role_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Responsible_Id { get; set; }
        public string? Responsible_Name { get; set; }
        public string? Obs_Reject_Id { get; set; }
        public string? Reject_Reason_Stage { get; set; }
        public string? Reject_Reason_Description { get; set; }
        public string? RejectedBy { get; set; }
        public string? Remarks { get; set; }
        public List<Observation_Corrective_Action>? L_Observation_Corrective_Action { get; set; }
    }
    public class Observation_Corrective_Action
    {
        public string? Inc_Obser_Report_Id { get; set; }
        public string? Corrective_Action_Id { get; set; }
        public string? Obs_Corrective_Action_Id { get; set; }
        public string? Inc_Observation_Id { get; set; }
        public string? Obser_UId { get; set; }
        public string? Description_Action { get; set; }
        public string? Priority { get; set; }
        public string? Responsible_Id { get; set; }
        public string? Photo_File_Path { get; set; }
        //public string? Target_Date { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
        public string? Status { get; set; }
        public string? Role_Id { get; set; }
    }
    public class Get_Observation_Corrective_Action
    {
        public IReadOnlyCollection<M_Observation_Corrective_Action>? Get_All { get; set; }
        public IReadOnlyCollection<M_Observation_Corrective_Action>? Get_All_Action { get; set; }
        public M_Observation_Corrective_Action? Get_Obs_Corrective_Action { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }
    public class Observation_Status_History
    {
        public string? History_Id { get; set; }
        public string? Inc_Observation_Id { get; set; }
        public string? Emp_Id { get; set; }
        public string? Role_Id { get; set; }
        public string? Updated_DateTime { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public string? Remarks1 { get; set; }
        public string? Remarks2 { get; set; }
        public string? Remarks3 { get; set; }
        public string? Remarks4 { get; set; }
        public string? Remarks5 { get; set; }
    }
}
