using Nakheel_Web.Models.InspectionMaster;
using Nakheel_Web.Models.Masters;
using System.ComponentModel.DataAnnotations;

namespace Nakheel_Web.Models.EMR_Drill
{
    public class Drill_Schedule : Common_EMR
    {
        public string? Drill_Schedule_ID { get; set; }
        public string? Drill_Calendar_ID { get; set; }
        public string? Drill_Type_ID { get; set; }
        public string? Commander { get; set; }
        public string? Service_Provider { get; set; }
    }
    public class Get_All_DrillSCH
    {
        public Drill_Schedule? Schedule { get; set; }
        public Drill_Fire? _Fire { get; set; }
        public Drill_CommonFRM? Drill_Common { get; set; }
        public Drill_Fire_Obsr? Drill_Obsr { get; set; }
        public IReadOnlyCollection<Dropdown_Values>? Access { get; set; }
        public List<Improvement_Act>? TotalActions { get; set; }
        public List<Dropdown_Values>? ServiceProvider { get; set; }
        public List<Dropdown_Values>? HseTeam { get; set; }
        public List<Appr_History>? Histories { get; set; }
        public List<Drill_REJ>? Rej { get; set; }
    }
    public class Drill_REJ
    {
        public string? IMP_CA_Reject_Id { get; set; }
        public string? Drill_CRR_ID { get; set; }
        public string? Reject_Reason { get; set; }
        public string? Remarks { get; set; }
        public string? Description { get; set; }
        public string? Drill_Schedule_ID { get; set; }
        public string? Date { get; set; }
        public string? Reject_By { get; set; }
    }
    public class Appr_History
    {
        public string? Appr_History_Id { get; set; }
        public string? Drill_Schedule_ID { get; set; }
        public string? Role { get; set; }
        public string? Action_Date { get; set; }
        public string? Nxt_Action_Date { get; set; }
        public string? Action_Des { get; set; }
        public string? Action_Done_By { get; set; }
        public string? Nxt_Action_Done_By { get; set; }
        public string? Status { get; set; }
    }
    public class Drill_Access
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
        public string? Remarks { get; set; }
    }
    public class Get_Drill_Details : RETURN_MESSAGE
    {
        public Get_All_DrillSCH? Data { get; set; }
    }

    public class Drill_Fire : Drill_Files
    {
        [Required(ErrorMessage = "This field is required")]
        public string? Drill_Schedule_ID { get; set; }
        public string? Fire_Drill_ID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Weather_Condition { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Start_Time { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? End_Time { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? LP_Reporting { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? PLP_Reporting { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Total_Participants { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Assembly_Point { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Total_FW { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? TT_Evacuation { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MaxLength(5000)]
        [RegularExpression("^(?=.*\\S)[a-zA-Z0-9-&/():?.,'\\s\\r\\n]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [DataType(DataType.Text)]
        public string? Extinguish_Fire { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MaxLength(5000)]
        [RegularExpression("^(?=.*\\S)[a-zA-Z0-9-&/():?.,'\\s\\r\\n]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [DataType(DataType.Text)]
        public string? Smoke_Mgmt { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MaxLength(5000)]
        [RegularExpression("^(?=.*\\S)[a-zA-Z0-9-&/():?.,'\\s\\r\\n]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [DataType(DataType.Text)]
        public string? ERT_Mgmt { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Rating { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(5000)]
        [RegularExpression("^(?=.*\\S)[a-zA-Z0-9-&/():?.,'\\s\\r\\n]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [DataType(DataType.Text)]
        public string? Drill_Comments { get; set; }
      
    }
    public class Drill_CommonFRM : Drill_Files
    {
        public string? Drill_Schedule_ID { get; set; }
        public string? Common_ID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Dril_Loc { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Rating { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MaxLength(5000)]
        [RegularExpression("^(?=.*\\S)[a-zA-Z0-9-&/():?.,'\\s\\r\\n]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [DataType(DataType.Text)]
        public string? Drill_Comments { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Common_Ques { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? ERT { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Security_Res { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Drill_Type_ID { get; set; }

    }
    public class Drill_Schedule_Get : RETURN_DATA_TBL
    {
        public List<Drill_Schedule>? Data { get; set; }
    }

    public class Drill_Sch_Assignee
    {
        public List<Dropdown_Values>? ServiceProviders { get; set; }
        public List<Dropdown_Values>? Commanders { get; set; }

    }

    public class Drill_Schedule_Param
    {
        public string? Drill_Schedule_ID { get; set; }
        public string? Created_By { get; set; }
        public string? Status { get; set; }

    }

    public class Drill_Action_Param
    {
        public string? Drill_Schedule_ID { get; set; }
        public string? Created_By { get; set; }
        public string? Status { get; set; }
        public List<Improvement_Act>? _Acts { get; set; }

    }
    public class Get_Sch_Assignee : RETURN_MESSAGE
    {
        public Drill_Sch_Assignee? Data { get; set; }

    }
    public class RETURN_DATA_TBL
    {
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
        public string? RecordsTotal { get; set; }
        public string? RecordsFiltered { get; set; }
    }
    public class Common_Tbl
    {
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? Created_By { get; set; }
        public string? Created_Date { get; set; }
        public string? Updated_By { get; set; }
        public string? Updated_Date { get; set; }
        public string? Remarks { get; set; }
        public string? Unique_ID { get; set; }

    }

    public class Improvement_Act : Common_Tbl
    {
        public string? Drill_CRR_ID { get; set; }
        public string? Drill_Schedule_ID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MaxLength(5000)]
        [RegularExpression("^(?=.*\\S)[a-zA-Z0-9-&/():?.,'\\s\\r\\n]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [DataType(DataType.Text)]
        public string? Action_Des { get; set; }
        public string? Assignee_Id { get; set; }
        public string? Target_Date { get; set; }
        public string? Corrective_Action { get; set; }
        public string? Photo_Path { get; set; }
        public string? Close_On_Spot { get; set; }
        public string? Assign_Type { get; set; }

    }

    public class Drill_Fire_Obsr : Drill_Files
    {
        public string? Fire_Drill_OBS_ID { get; set; }
        public string? Drill_Schedule_ID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? First_Sound { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? First_Occupant { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Last_Occupant { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MaxLength(5000)]
        [RegularExpression("^(?=.*\\S)[a-zA-Z0-9-&/():?.,'\\s\\r\\n]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [DataType(DataType.Text)]
        public string? Ocuupant_Nature { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MaxLength(5000)]
        [RegularExpression("^(?=.*\\S)[a-zA-Z0-9-&/():?.,'\\s\\r\\n]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [DataType(DataType.Text)]
        public string? Life_Safety_Equip { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MaxLength(5000)]
        [RegularExpression("^(?=.*\\S)[a-zA-Z0-9-&/():?.,'\\s\\r\\n]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [DataType(DataType.Text)]
        public string? Lift_Status { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MaxLength(5000)]
        [RegularExpression("^(?=.*\\S)[a-zA-Z0-9-&/():?.,'\\s\\r\\n]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [DataType(DataType.Text)]
        public string? Pantry_Status { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MaxLength(5000)]
        [RegularExpression("^(?=.*\\S)[a-zA-Z0-9-&/():?.,'\\s\\r\\n]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [DataType(DataType.Text)]
        public string? Evac_Process { get; set; }

    }

    public class Drill_Files : Common_Tbl
    {
        public List<IFormFile>? Photos { get; set; }
        public List<IFormFile>? Videos { get; set; }
        public List<Improvement_Act>? _Acts { get; set; }
        public List<Drill_Photos>? Drill_Photos { get; set; }
        public List<Drill_Vedios>? Drill_Vedios { get; set; }
    }
}
