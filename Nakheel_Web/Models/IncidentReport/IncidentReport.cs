using Nakheel_Web.Models.Masters;

namespace Nakheel_Web.Models.IncidentReport
{
    public class M_Incident_Report : M_Common_Fields
    {
        public string? Inc_Id { get; set; }
        public string? Inc_Category_Id { get; set; }
        public string? Inc_Type_Id { get; set; }
        public string? Inc_Date { get; set; }
        public string? Inc_Time { get; set; }
        public string? Zone { get; set; }
        public string? Building { get; set; }
        public string? Business_Unit { get; set; }
        public string? Loc_Latitude { get; set; }
        public string? Loc_Longitude { get; set; }
        public string? Inc_Description { get; set; }
        public string? Is_Injury_Illness { get; set; }
        public string? Injured_Person { get; set; }
        public string? Company_Name { get; set; }
        public string? Contact_Name { get; set; }
        public string? Mobile_Number { get; set; }
        public string? Community_Id { get; set; }
        public string? Master_Community_Id { get; set; }
        public string? Zone_Name { get; set; }
        public string? Building_Name { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? Community_Name { get; set; }
        public string? Master_Community_Name { get; set; }
        public string? Is_Investigation_Req { get; set; }
        public string? Is_Investigation_Reportable { get; set; }
        public string? Incident_Related_Id { get; set; }
        public string? Is_PropertyDamaged { get; set; }
        public string? Is_InsuranceClaim { get; set; }
        public string? Inc_Business_Unit_Type { get; set; }
        public string? Inc_Fire_Location { get; set; }
        public string? Lead_Investigator_Id { get; set; }
        public string? Immediate_Action { get; set; }
        public string? Is_Follow_required { get; set; }
        public string? Actions_Taken_Description { get; set; }
        public string? Last_Reported_By { get; set; }
        public string? Is_Knowledge_Share { get; set; }
        public string? Add_Description_1 { get; set; }
        public string? Add_Description_2 { get; set; }
        public string? Add_Description_3 { get; set; }
        public string? Exact_Loaction { get; set; }
        public string? Is_Vehicle_Accident_Per { get; set; }
        public string? Incident_Party { get; set; }
        public string? Description_of_Incident_Party { get; set; }
        public string? Service_Provider_Id { get; set; }
        public string? Inc_Remarks_1 { get; set; }
        public string? Inc_Remarks_2 { get; set; }
        public string? Inc_Remarks_3 { get; set; }
        public List<PHOTO>? Incident_Notification_Photos_List { get; set; }
        public List<VIDEO>? Incident_Notification_Videos_List { get; set; }
        public List<IFormFile>? inputPhoto { get; set; }
        public List<IFormFile>? inputVideo { get; set; }
        public List<M_Inc_Inve_Injury_Type>? L_Inc_Inve_Injury_Type { get; set; }
        public List<M_Inc_Inve_Nature_Injury>? L_Inc_Inve_Nature_Injury { get; set; }
        public List<M_Inc_Vehicle_Accident>? L_M_Inc_Vehicle_Accident { get; set; }
        public List<M_Inc_Notification_Photos>? L_Inc_Notification_Photos { get; set; }
        public List<M_Inc_Persons_Injured>? L_M_Inc_Persons_Injured { get; set; }
        public List<M_Inc_Notification_Videos>? L_Inc_Notification_Videos { get; set; }
        public List<Incident_Update_History>? L_Incident_Update_History { get; set; }
        public List<Circulate_Knowledge_Details>? L_Circulate_Knowledge_Details { get; set; }


        public List<Inves_Other_Parties_Involved>? L_Inves_Other_Parties_Involved { get; set; }
        public List<Inves_Immediate_Cause_Unsafe_Act>? L_Inves_Immediate_Cause_Unsafe_Act { get; set; }
        public List<Inves_Immediate_Cause_Unsafe_Cond>? L_Inves_Immediate_Cause_Unsafe_Cond { get; set; }
        public List<Inves_Root_Cause_PF>? L_Inves_Root_Cause_PF { get; set; }
        public List<Inves_Root_Cause_SF>? L_Inves_Root_Cause_SF { get; set; }
        public List<Inves_Mechanism_InjuryIllness>? L_Inves_Mechanism_InjuryIllness { get; set; }
        public List<Inves_AgencySource_InjuryIllness>? L_Inves_AgencySource_InjuryIllness { get; set; }
        public List<Inves_Environmental_Impact_Details>? L_Inves_Environmental_Impact_Details { get; set; }
        public List<Inves_Security_Impact_Details>? L_Inves_Security_Impact_Details { get; set; }
        public List<Inves_Actions_Taken_Immediately>? L_Inves_Actions_Taken_Immediately { get; set; }
        public List<Inves_Incident_Root_Cause>? L_Inves_Incident_Root_Cause { get; set; }
        public List<Inves_Corrective_Actions>? L_Inves_Corrective_Actions { get; set; }
        public List<Inves_Declarations_Approvals>? L_Inves_Declarations_Approvals { get; set; }
        public List<Inves_Attachements>? L_Inves_Attachements { get; set; }
        //public List<M_Inc_Persons_Injured>? L_M_Inc_Persons_Injured { get; set; }
        public List<Inves_Complete_Corrective_Actions>? L_Inves_Complete_Corrective_Actions { get; set; }
        //public List<M_Inc_Inve_Injury_Type>? L_Inc_Inve_Injury_Type { get; set; }
        //public List<M_Inc_Inve_Nature_Injury>? L_Inc_Inve_Nature_Injury { get; set; }
        public List<M_Incident_Notification_Reject>? L_M_Incident_Notification_Reject { get; set; }
        public Inc_Knowledge_Share? L_M_Incident_Knowledge_Share { get; set; }
    }

    public class GET_INCIDENT_NOTIF : M_Common_Fields
    {
        public IReadOnlyCollection<M_Incident_Report>? Get_All_Incident_Notification { get; set; }
        public M_Incident_Report? Get_Incident_Notification { get; set; }
        public IReadOnlyCollection<M_Incident_Report>? Get_Data { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }

    public class PHOTO
    {
        public string? Photo_Id { get; set; }
        public string? Incident_Notification_Id { get; set; }
        public string? Photo_File_Name { get; set; }
        public string? Photo_File_Path { get; set; }
        public string? Photo_File_Size { get; set; }
        public string? photo_File_Type { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
    }
    public class VIDEO
    {
        public string? Video_Id { get; set; }
        public string? Incident_Notification_Id { get; set; }
        public string? Video_File_Name { get; set; }
        public string? Video_File_Path { get; set; }
        public string? Video_File_Size { get; set; }
        public string? Video_File_Type { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
    }
    public class M_Inc_Inve_Injury_Type
    {
        public string? Inc_Injury_Type_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Injury_Type_Id { get; set; }
        public string? Injury_Type_Name { get; set; }
        public string? Remarks { get; set; }
    }
    public class M_Inc_Inve_Nature_Injury
    {
        public string? Inc_Nature_Injury_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Nature_Injury_Id { get; set; }
        public string? Nature_Injury_Name { get; set; }
        public string? Remarks { get; set; }
    }
    public class M_Inc_Vehicle_Accident
    {
        public string? Inc_VehicleAccident_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Vehicle_Accident_Id { get; set; }
        public string? Vehicle_Accident_Name { get; set; }
        public string? Remarks { get; set; }
    }
    public class M_Inc_Notification_Photos
    {
        public string? Photo_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Photo_File_Path { get; set; }
    }
    public class M_Inc_Notification_Videos
    {
        public string? Video_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Video_File_Path { get; set; }
    }
    public class M_Inc_Persons_Injured
    {
        public string? Inc_Persons_Injured_Id { get; set; }
        public string? Persons_Id { get; set; }
        public string? Persons_Name { get; set; }
        public string? Persons_ContactNo { get; set; }
        public string? BodyParts_List { get; set; }
        public string? Injured_Type { get; set; }
        public string? Total_man_days { get; set; }
        public string? Company_Name { get; set; }
    }
    public class M_Inc_Investigation_Team
    {
        public string? Inc_Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? Role_Id { get; set; }
        public List<M_Inc_Invs_Emp>? L_M_Inc_Invs_Emp { get; set; }
    }
    public class M_Inc_Invs_Emp
    {
        public string? Inc_Verification_TeamMember_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Persons_Id { get; set; }
    }
    public class Inc_Knowledge_Share
    {
        public string? Knowledge_Share_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? File_Path { get; set; }
        public string? Knowledge_Description { get; set; }
        public string? KnowledgeInc_Category { get; set; }
        public string? KnowledgeInc_Type { get; set; }
        public string? CreatedBy { get; set; }
        public string? Role_Id { get; set; }
        public string? Is_Preview { get; set; }
        public List<Inc_Knowledge_Share_Key_Points>? L_Inc_Knowledge_Share_Key_Points { get; set; }
        public List<Inc_Knowledge_Share_Recommendations>? L_Inc_Knowledge_Share_Recommendations { get; set; }
    }
    public class Inc_Knowledge_Share_Key_Points
    {
        public string? Key_Point_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Key_Point_Name { get; set; }
    }
    public class Inc_Knowledge_Share_Recommendations
    {
        public string? Recommendation_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Recommendations_Name { get; set; }
    }
    public class M_Corrective_Assign_Action
    {
        public string? CreatedBy { get; set; }
        public string? Role_Id { get; set; }
        public string? Inc_Id { get; set; }
        public List<Corrective_Assign_Action>? L_Corrective_Assign_Action { get; set; }
    }
    public class Corrective_Assign_Action
    {
        public string? Corrective_Action_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Description_Actions { get; set; }
        public string? Priority { get; set; }
        public string? Person_Responsible_Id { get; set; }
        public string? Target_Date { get; set; }
        public string? Created_By { get; set; }
        public string? Created_Date { get; set; }
        public string? Status { get; set; }
        public string? Role_Id { get; set; }
        public string? Zone { get; set; }
        public string? Community_Id { get; set; }
        public string? Inc_Corrective_Reject_Id { get; set; }
        public string? Rejected_Reason { get; set; }
    }
    public class Get_Corrective_Assign_Action
    {
        public IReadOnlyCollection<Corrective_Assign_Action>? Get_All { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    public class Add_Corrective_Action : M_Common_Fields
    {
        public string? Inc_Id { get; set; }
        public string? Action_Taken_Description { get; set; }
        public string? Photo_File_Path { get; set; }
        public string? Created_By { get; set; }
        public string? Corrective_Action_Id { get; set; }
        public List<List_Add_Corrective_Action>? L_List_Add_Corrective_Action { get; set; }
    }
    public class List_Add_Corrective_Action
    {
        public string? Inc_Evidence_Photo_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Action_Taken_Description { get; set; }
        public string? Photo_File_Path { get; set; }
        public string? Status { get; set; }
        public string? Created_By { get; set; }
        public string? Created_Date { get; set; }
        public string? Corrective_Action_Id { get; set; }
    }
    public class Evidence_Upload_Photos
    {
        public string? Inc_Evidence_Photo_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Action_Taken_Description { get; set; }
        public string? Photo_File_Path { get; set; }
        public string? Status { get; set; }
        public string? Created_By { get; set; }
        public string? Created_Date { get; set; }
        public string? Corrective_Action_Id { get; set; }
    }
    public class Get_Evidence_Upload_Photos
    {
        public IReadOnlyCollection<Evidence_Upload_Photos>? Get_All { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    public class Inves_Complete_Corrective_Actions : M_Common_Fields
    {
        public string? Inves_Corrective_Action_Id { get; set; }
        public string? Investigation_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Description_Actions { get; set; }
        public string? Person_Responsible { get; set; }
        public string? Priority { get; set; }
        public string? Target_Date { get; set; }
        public List<Evidence_Upload_Photos>? Evidence_Upload_Photos { get; set; }
    }
    public class Incident_Update_History
    {
        public string? History_Id { get; set; }
        public string? Inc_Id { get; set; }
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
    public class M_Incident_Notification_Reject : M_Common_Fields
    {
        public string? Reject_Id { get; set; }
        public string? Reject_Reason_Stage { get; set; }
        public string? Reject_Reason_Description { get; set; }
        public string? Rejected_By { get; set; }
        public string? Reject_Inc_Id { get; set; }
        public string? Rejected_Date { get; set; }
        public string? Updated_Date { get; set; }
        public string? Updated_By { get; set; }
    }

    public class Circulate_Knowledge_Details
    {
        public string? Circulate_Knowledge_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Created_By { get; set; }
        public string? Created_Date { get; set; }
        public string? Remarks { get; set; }
    }
}
