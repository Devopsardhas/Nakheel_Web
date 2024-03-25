namespace Nakheel_Web.Models.ServiceStatistics
{
    public class Service_Sat_Model
    {
        public string? Service_Monthly_Id { get; set; }
        public string? Service_Year { get; set; }
        public string? Service_Month { get; set; }
        public string? Service_Month_Name { get; set; }
        public string? Service_Total_Employees { get; set; }
        public string? Service_Man_of_Hours { get; set; }
        public string? Service_Training_Conducted { get; set; }
        public string? CreatedBy { get; set; }
        public string? Status { get; set; }
        public string? Unique_Id { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_Id { get; set; }
        public string? Business_Unit_Type { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? Zone_Name { get; set; }
        public string? Community_Name { get; set; }
        public string? Building_Name { get; set; }
        public string? DMS_Number_Id { get; set; }
        public string? Purchase_Order_Number { get; set; }
        public string? Company_Name { get; set; }
        public string? Remarks { get; set; }
        public List<Training_Legal_Compliance>? _Legal_Files { get; set; }
        public List<Training_Other_Files>? _Other_Files { get; set; }
        public List<Training_Name>? _Training_List { get; set; }
        public List<Training_Name_Other>? _Training_Other_List { get; set; }
        public List<Reject_Reason>? _Reject_List { get; set; }
    }

    public class Training_Legal_Compliance
    {
        public string? TR_Legal_File_Id { get; set; }
        public string? Service_Monthly_Id { get; set; }
        public string? Legal_File_Path { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class Training_Other_Files
    {
        public string? TR_Other_File_Id { get; set; }
        public string? Service_Monthly_Id { get; set; }
        public string? Other_File_Path { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class Training_Name
    {
        public string? Service_Training_Id { get; set; }
        public string? Service_Monthly_Id { get; set; }
        public string? Service_Training_Name { get; set; }
        public string? Service_Remarks { get; set; }
        public string? CreatedBy { get; set; }
        public List<Training_Files>? _Training_Files { get; set; }
    }
    public class Training_Files
    {
        public string? TR_File_Id { get; set; }
        public string? Service_Training_Id { get; set; }
        public string? Service_Monthly_Id { get; set; }
        public string? Training_File_Path { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class Training_Name_Other
    {
        public string? Service_Other_Training_Id { get; set; }
        public string? Service_Monthly_Id { get; set; }
        public string? Service_Other_Training_Name { get; set; }
        public string? Service_Other_Remarks { get; set; }
        public string? CreatedBy { get; set; }
        public List<Training_Name_Other_Files>? _Training_Other_Files { get; set; }
    }
    public class Training_Name_Other_Files
    {
        public string? TR_Other_File_Id { get; set; }
        public string? Service_Other_Training_Id { get; set; }
        public string? Service_Monthly_Id { get; set; }
        public string? Training_Other_File_Path { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class M_Get_ServiceMonthly
    {
        public IReadOnlyCollection<Service_Sat_Model>? Get_All { get; set; }
        public Service_Sat_Model? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    
    public class Reject_Reason
    {
        public string? Service_Monthly_Id { get; set; }
        public string? HSE_Reject_Id { get; set; }
        public string? Rejected_By { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public string? Unique_Id { get; set; }
    }
}
