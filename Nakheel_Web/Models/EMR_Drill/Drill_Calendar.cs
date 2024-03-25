using Microsoft.Build.Framework;
using Nakheel_Web.Models.Masters;

namespace Nakheel_Web.Models.EMR_Drill
{
    public class Drill_Calendar : Common_EMR
    {
        [Required]
        public string? Drill_Calendar_ID { get; set; }
        public string? Business_Unit_Id { get; set; }
        [Required]
        public string? Sub_Building_Id { get; set; }
        [Required]
        public string? Initial_Date { get; set; }
        [Required]
        public string? Frequency { get; set; }
        [Required]
        public string? HSE_Officer { get; set; }
        [Required]
        public string? Commander { get; set; }
        [Required]
        public string? Service_Provider { get; set; }
        [Required]
        public string? Drill_Type_ID { get; set; }
    }
    public class Common_EMR
    {
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? Created_By { get; set; }
        public string? Created_Date { get; set; }
        public string? Updated_By { get; set; }
        public string? Updated_Date { get; set; }
        public string? Remarks { get; set; }
        public string? Unique_ID { get; set; }
        public string? Zone { get; set; }
        public string? Community { get; set; }
        public string? Building_Name { get; set; }
        public string? Drill_Type { get; set; }
    }

    public class Drill_Calendar_Param
    {
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_Id { get; set; }
        public string? Drill_Type_ID { get; set; }
        public string? Action { get; set; }
    }

    public class EmployeesDrp
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
    }

    public class Get_Drill_Calendar : RETURN_MESSAGE
    {
        public Drill_Cal? Data { get; set; }
    }
    public class Drill_Cal
    {
        public List<Drill_Calendar>? Drill_SCH { get; set; }
        public List<EmployeesDrp>? HSE_Team { get; set; }
        public List<EmployeesDrp>? Commander_Team { get; set; }
        public List<EmployeesDrp>? SP_Team { get; set; }
        public string? Building_ID { get; set; }
    }
}
