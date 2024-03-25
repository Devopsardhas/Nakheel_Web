using Nakheel_Web.Models.EMR_Drill;

namespace Nakheel_Web.Models.Emergency
{
    public class EMR_Alert: Common_EMR
    {
        public string? EMR_Alert_ID { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Sub_Building_Id { get; set; }
        public string? Mitigation_Type { get; set; }
        public string? Created_By { get; set; }
        public string? Status { get; set; }      
        public List<Alert_Spot>? _Spots { get; set; }
        public List<EMR_Approve_Reject>? Reject { get; set; }
    }
    public class Alert_Spot : Common_Tbl
    {
        public string? Hot_Spot_Id { get; set; }
        public string? EMR_Alert_ID { get; set; }
        public string? Address { get; set; }
        public string? Exact_Loc { get; set; }
        public string? Reason { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Alert_Task_Id { get; set; }
        public string? Status { get; set; }
        public string? Service_Provider { get; set; }
    }
    public class TRIG_Reject
    {
        public string? Alert_Trig_Rej_Id { get; set; }
        public string? Alert_Task_Id { get; set; }
        public string? Trigger_ID { get; set; }
        public string? Reason { get; set; }
        public string? Rejected_By { get; set; }
        public string? Status { get; set; }
        public string? Hot_Spot_Id { get; set; }
        public string? Created_Date { get; set; }
    }
    public class EMR_Alert_Get  : RETURN_DATA_TBL
    {
        public List<EMR_Alert>? Data { get; set; }        
    }

    public class Edit_EMR_ALERT
    {
        public EMR_Alert? Data { get; set; }     
    }
    public class Get_Migitation_Loc
    {
        public List<Alert_Spot>? Data { get; set; }
    }
    public class Drill_Alert_Param
    {
        public string? EMR_Alert_ID { get; set; }
        public string? Created_By { get; set; }
        public string? Building_ID { get; set; }
        public string? Mitigation_ID { get; set; }
        public string? Status { get; set; }
    }

    public class EMR_Approve_Reject
    {
        public string? Alert_Rej_Id { get; set; }
        public string? EMR_Alert_ID { get; set; }
        public string? Reason { get; set; }
        public string? Rejected_By { get; set; }
        public string? Status { get; set; }
        public string? Hot_Spot_Id { get; set; }
        public string? Address { get; set; }
        public string? Created_Date { get; set; }
    }

}
