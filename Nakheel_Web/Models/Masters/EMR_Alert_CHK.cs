using Nakheel_Web.Models.EMR_Drill;

namespace Nakheel_Web.Models.Masters
{
    public class EMR_Alert_CHK : Common_Tbl
    {
        public string? Alert_Checklist_Id { get; set; }
        public string? Mitigation_Type { get; set; }
        public string? Mitigation_Name { get; set; }
        public string? Check_List { get; set; }
    }

    public class EMR_Alert_CHK_list
    {
        public List<EMR_Alert_CHK>? Data { get; set; }
        public string? Message { get; set; }
        public string? Status { get; set; }
        public string? Status_Code { get; set; }
    }
}
