using Nakheel_Web.Models.EMR_Drill;
using Nakheel_Web.Models.InspectionMaster;

namespace Nakheel_Web.Models.Emergency
{
    public class Trigger_Alert : Common_TRG
    {
        public string? Trigger_ID { get; set; }
        public string? Zone_Id { get; set; }
        public string? Mitigation_Id { get; set; }
        public string? Community_Id { get; set; }
        //public string? Created_By { get; set; }
        //public string? Status { get; set; }
        public string? Alert_Task_Id { get; set; }
        public string? REF_NO { get; set; }
        public List<TRG_Building_List>? _Building_Lists { get; set; }
        public List<ChecklistMap>? ChecklistMaps { get; set; }
        public List<Alert_Spot>? _Spots { get; set; }
        public List<TRIG_Reject>? _Reject { get; set; }
    }
    public class TRG_Building_List
    {
        public string? Building_Name { get; set; }
        public string? Sub_Building_Id { get; set; }
        public string? Status { get; set; }
        public string? Service_Provider { get; set; }
        public string? Alert_Task_Id { get; set; }
        public string? Exact_Loc { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Reason { get; set; }
    }
    public class ChecklistMap
    {
        public string? Chk_Map_Id { get; set; }
        public string? Hot_Spot_Id { get; set; }
        public string? Alert_Checklist_Id { get; set; }
        public string? Alert_Task_Id { get; set; }
        public string? Action_Taken { get; set; }
        public string? File_Path { get; set; }
        public string? Remarks { get; set; }
        public string? Created_By { get; set; }
    }
    public class Get_Trigger
    {
        public Trigger_Alert? trigger { get; set; }
        public List<TRG_Building_List>? _Building_Lists { get; set; }
    }
    public class Trigger_Alert_Get : RETURN_DATA_TBL
    {
        public List<Trigger_Alert>? Data { get; set; }
    }

    public class Edit_Trigger_Alert
    {
        public Trigger_Alert? Data { get; set; }
    }

    public class Get_Service_Provider
    {
        public  List<Dropdown_Values>? ServiceProviders { get; set; }

    }

    public class Dropdown_Values
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
    }

    public class TRIG_Assignee : Common_TRG
    {
        public string? Alert_Task_Id { get; set; }
        public string? Trigger_ID { get; set; }
        public string? Sub_Building_Id { get; set; }
        public string? Service_Provider { get; set; }
    }
    public class Common_TRG
    {
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? Created_By { get; set; }
        public string? Created_Date { get; set; }
        public string? Updated_By { get; set; }
        public string? Updated_Date { get; set; }
        public string? Remarks { get; set; }
        public string? Unique_ID { get; set; }
        public string? Building_Name { get; set; }
        public string? Zone { get; set; }
        public string? Community { get; set; }
        public string? Mitigation { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }
    public class TRIG_Assignee_Get : RETURN_DATA_TBL
    {
        public List<TRIG_Assignee>? Data { get; set; }
    }

    public class Get_ChecklistMap
    {
        public List<ChecklistMap>? Data { get; set; }
    }
    public class Mitigation_Report : Common_Tbl
    {
        public string? Alert_Report_Id { get; set; }
        public string? Trigger_ID { get; set; }
        public string? REF_NO { get; set; }
        public string? Area { get; set; }
        public string? Start_Time { get; set; }
        public string? End_Time { get; set; }
        public string? Resp_Time { get; set; }
        public string? Duration { get; set; }
        public string? Pumps_Deployed { get; set; }
        public string? Pump_OP_Duration { get; set; }
        public string? Tankers_Deployed { get; set; }
        public string? No_Trips { get; set; }
        public string? Clear_Time { get; set; }
        public string? Deployed_SP { get; set; }
        public string? Deployed_NCM { get; set; }
        public string? Mitigation_Cost { get; set; }
        public string? Gauge_Reading { get; set; }
        public string? Total_Pumps { get; set; }
        public string? Total_Tankers { get; set; }
        public string? Total_No_Trips { get; set; }
        public string? Total_Deployed_SP { get; set; }
        public string? Total_Deployed_NCM { get; set; }
        public string? Total_Mitigation_Cost { get; set; }
        public List<Report_File>? _Files { get; set; }
    }
    public class Report_File : Common_Tbl
    {
        public string? Report_File_Id { get; set; }
        public string? Alert_Report_Id { get; set; }
        public string? File_Name { get; set; }
        public string? File_Path { get; set; }
        public string? File_Size { get; set; }
        public string? File_Type { get; set; }

    }

    public class Edit_Mitigation
    {
        public Mitigation_Report? Data { get; set; }
    }
    public class GET_Mitigation_Report
    {
        public List<Mitigation_Report>? Data { get; set; }
    }

    public class Mitigation_Param
    {
        public string? Zone_Id { get; set; }
        public string? Year { get; set; }
        public string? From_Date { get; set; }
        public string? To_Date { get; set; }
        public string? REF_NO { get; set; }
    }

    public class Get_Emergency_Type
    {
        public List<Drop_Values>? Data { get; set; }
    }
    public class Drop_Values
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
    }

    public class Get_Rain_Ref
    {
        public IReadOnlyCollection<Drop_Values>? Data { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }
    public class Mitigation_Ref
    {
        public string? REF_NO { get; set; }
    }
}
