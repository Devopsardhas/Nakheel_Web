using Nakheel_Web.Models.Masters;

namespace Nakheel_Web.Models.IncidentReport
{

    public class M_Investigation_Report_Add : M_Common_Fields
    {
        public string? Inc_Id { get; set; }
        public string? Description_circumstances { get; set; }
        public string? Additional_Information { get; set; }
        public string? Risk_Assessment_Environmental_Impact { get; set; }
        public string? Is_Ref { get; set; }
        public string? Invs_File_Path { get; set; }
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
        public List<M_Inc_Persons_Injured>? L_M_Inc_Persons_Injured { get; set; }
        public List<Inves_Complete_Corrective_Actions>? L_Inves_Complete_Corrective_Actions { get; set; }
        public List<M_Inc_Inve_Injury_Type>? L_Inc_Inve_Injury_Type { get; set; }
        public List<M_Inc_Inve_Nature_Injury>? L_Inc_Inve_Nature_Injury { get; set; }
        public List<M_Incident_Notification_Reject>? L_M_Incident_Notification_Reject { get; set; }
        public Inc_Knowledge_Share? L_M_Incident_Knowledge_Share { get; set; }
        
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
        public string? Is_PropertyDamaged { get; set; }
        public string? Is_InsuranceClaim { get; set; }
        public string? Inc_Business_Unit_Type { get; set; }
        public string? Inc_Fire_Location { get; set; }
        public string? Immediate_Action { get; set; }
        public string? Is_Follow_required { get; set; }
        public string? Actions_Taken_Description { get; set; }
        public string? Add_Description_1 { get; set; }
        public string? Add_Description_2 { get; set; }
        public string? Add_Description_3 { get; set; }
        public string? Exact_Loaction { get; set; }
        public string? Is_Vehicle_Accident_Per { get; set; }
        public string? Master_Community_Name { get; set; }
        public List<PHOTO>? Incident_Notification_Photos_List { get; set; }
        public List<VIDEO>? Incident_Notification_Videos_List { get; set; }
        public List<IFormFile>? inputPhoto { get; set; }
        public List<IFormFile>? inputVideo { get; set; }
        public List<M_Inc_Vehicle_Accident>? L_M_Inc_Vehicle_Accident { get; set; }
        public List<M_Inc_Notification_Photos>? L_Inc_Notification_Photos { get; set; }
        public List<M_Inc_Notification_Videos>? L_Inc_Notification_Videos { get; set; }
        public List<Incident_Update_History>? L_Incident_Update_History { get; set; }
        public List<Circulate_Knowledge_Details>? L_Circulate_Knowledge_Details { get; set; }
        public string? Incident_Party { get; set; }
        public string? Description_of_Incident_Party { get; set; }
        public string? Service_Provider_Id { get; set; }

    }
    public class M_Investigation_Report : M_Common_Fields
    {
        public string? Investigation_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Total_Man_Days { get; set; }
        public string? Involved_Department_Contractor { get; set; }
        public string? Description_circumstances { get; set; }
        public string? Applicable_Reports { get; set; }
        public string? Justification_Comments { get; set; }
        public string? Additional_Information { get; set; }
        public string? Risk_Assessment_Environmental_Impact { get; set; }
        public string? Is_Ref { get; set; }
        public string? Lead_Investigator_Id { get; set; }
        public string? Invs_File_Path { get; set; }
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
        public List<M_Inc_Persons_Injured>? L_M_Inc_Persons_Injured { get; set; }
        public List<Inves_Complete_Corrective_Actions>? L_Inves_Complete_Corrective_Actions { get; set; }
        public List<M_Inc_Inve_Injury_Type>? L_Inc_Inve_Injury_Type { get; set; }
        public List<M_Inc_Inve_Nature_Injury>? L_Inc_Inve_Nature_Injury { get; set; }
        public List<M_Incident_Notification_Reject>? L_M_Incident_Notification_Reject { get; set; }
        public List<Incident_Update_History>? L_Incident_Update_History { get; set; }

        public Inc_Knowledge_Share? L_M_Incident_Knowledge_Share { get; set; }
        public M_Incident_Report? M_Incident_Report { get; set; }
    }

    public class Inves_Other_Parties_Involved : M_Common_Fields
    {
        public string? Inves_Other_Parties_Id { get; set; }
        public string? Investigation_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Role_Position { get; set; }
        public string? Other_Name { get; set; }
        public string? Other_Contact_no { get; set; }
    }

    public class Inves_Immediate_Cause_Unsafe_Act : M_Common_Fields
    {
        public string? Inves_Unsafe_Act_Id { get; set; }
        public string? Investigation_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? M_Unsafe_Act_Id { get; set; }
    }

    public class Inves_Immediate_Cause_Unsafe_Cond : M_Common_Fields
    {
        public string? Inves_Unsafe_Cond_Id { get; set; }
        public string? Investigation_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? M_Unsafe_Cond_Id { get; set; }
    }

    public class Inves_Root_Cause_PF : M_Common_Fields
    {
        public string? Inves_RootCause_PF_Id { get; set; }
        public string? Investigation_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? M_RootCause_PF_Id { get; set; }
    }

    public class Inves_Root_Cause_SF : M_Common_Fields
    {
        public string? Inves_RootCause_SF_Id { get; set; }
        public string? Investigation_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? M_RootCause_SF_Id { get; set; }
    }

    public class Inves_Mechanism_InjuryIllness : M_Common_Fields
    {
        public string? Inves_Mechanism_Injury_Id { get; set; }
        public string? Investigation_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? M_Mechanism_InjuryIllness_Id { get; set; }
    }

    public class Inves_AgencySource_InjuryIllness : M_Common_Fields
    {
        public string? Inves_AgencySource_Injury_Id { get; set; }
        public string? Investigation_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? M_AgencySource_InjuryIllness_Id { get; set; }
    }

    public class Inves_Environmental_Impact_Details : M_Common_Fields
    {
        public string? Inves_Environmental_Id { get; set; }
        public string? Investigation_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Environmental_Incident { get; set; }
        public string? Cause_Of_Release { get; set; }
        public string? Impact_Details { get; set; }
    }

    public class Inves_Security_Impact_Details : M_Common_Fields
    {
        public string? Inves_Security_Id { get; set; }
        public string? Investigation_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Classification_Id { get; set; }
        public string? Type_of_Security_Incident_Id { get; set; }
        public string? Impact_Details { get; set; }
    }

    public class Inves_Actions_Taken_Immediately : M_Common_Fields
    {
        public string? Inves_Action_Taken_Id { get; set; }
        public string? Investigation_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Description_of_Actions { get; set; }
        public string? Action_Taken_By { get; set; }
        public string? Date_Completed { get; set; }
    }

    public class Inves_Incident_Root_Cause : M_Common_Fields
    {
        public string? Inves_Root_Cause_Id { get; set; }
        public string? Investigation_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Root_Cause_Description { get; set; }
    }

    public class Inves_Corrective_Actions : M_Common_Fields
    {
        public string? Inves_Corrective_Action_Id { get; set; }
        public string? Investigation_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Description_Actions { get; set; }
        public string? Person_Responsible { get; set; }
        public string? Priority { get; set; }
        public string? Target_Date { get; set; }
        public string? Action_Taken { get; set; }
        public string? Upload_Evidence { get; set; }
    }

    public class Inves_Declarations_Approvals : M_Common_Fields
    {
        public string? Inves_Declarations_Approvals_Id { get; set; }
        public string? Investigation_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Report_Status { get; set; }
        public string? HSSE_Representative_Id { get; set; }
        public string? Department_Representative_Id { get; set; }
        public string? Other_Representative_Id { get; set; }
        public string? Date { get; set; }
    }

    public class Inves_Attachements : M_Common_Fields
    {
        public string? Inves_Attachements_Id { get; set; }
        public string? Investigation_Id { get; set; }
        public string? Inc_Id { get; set; }
        public string? Title { get; set; }
        public string? Is_Attachements { get; set; }
        public string? Attachements_Description { get; set; }
        public string? File_Path { get; set; }

    }

    public class GET_INVESTIGATION_REPORT
    {
        public IReadOnlyCollection<M_Investigation_Report>? Get_All { get; set; }
        public M_Investigation_Report? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
}
