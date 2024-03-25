using Nakheel_Web.Models.IncidentReport;

namespace Nakheel_Web.Models.ServiceProviderSignup_Dashboard
{
    public class ServiceProviderSignup_Dashboard
    {
        public string? Name { get; set; }
        public string? January { get; set; }
        public string? February { get; set; }
        public string? March { get; set; }
        public string? April { get; set; }
        public string? May { get; set; }
        public string? June { get; set; }
        public string? July { get; set; }
        public string? August { get; set; }
        public string? September { get; set; }
        public string? October { get; set; }
        public string? November { get; set; }
        public string? December { get; set; }
    }
    public class ServiceProvSignup_Dashboard_Param
    {
        public string? Month { get; set; }
        public string? Year { get; set; }
        public string? CreatedBy { get; set; }
        public string? Zone_ID { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_ID { get; set; }
        public string? From_Date { get; set; }
        public string? To_date { get; set; }
        public string? Category_Name { get; set; }
        public string? Card_View_Id { get; set; }

    }
    public class ServiceProvider_Dashborad_Count
    {
        public string? Total_ServiceProvider_count { get; set; }
        public string? Approval_Pending_Count { get; set; }
        public string? Contractor_Active_Count { get; set; }
        public string? Contractor_Expired_Count { get; set; }
        public string? Permit_Active_Count { get; set; }
        public string? Permit_Expired_Count { get; set; }
        public string? Approved { get; set; }
        public string? Opened { get; set; }
    }   
    public class ServiceProv_PermitType_Counts
    {
        public string? Name { get; set; }
        public string? Open { get; set; }
        public string? Close { get; set; }
        public string? Overdue { get; set; }
    }
    public class ServiceProv_Count
    {
        public string? Name { get; set; }
        public string? Count { get; set; }
    }
    public class ServiceProv_LongShortTerm
    {
        public string? ID { get; set; }
        public string? Long_Term { get; set; }
        public string? Short_Term { get; set; }
    }
    public class ServiceProvider_Model
    {   
        public string? Zone_Name { get; set; }
        public string? Community_Master_Name { get; set; }
        public string? Company_Name { get; set; }
        public string? Manager_Incharge { get; set; }
        public string? Contract_End_Date { get; set; }
        public string? Scope_Of_Work { get; set; }
        public string? Purchase_Order_Number { get; set; }
        public string? Status { get; set; }
        public string? Contract_Status { get; set; }
        public IReadOnlyList<ServiceProvider_Dashborad_Count>? Total_Count_List { get; set; }
        public IReadOnlyList<ServiceProv_PermitType_Counts>? PermitType_Count_List { get; set; }
        public IReadOnlyList<ServiceProv_Count>? ZoneCount_List { get; set; }
        public IReadOnlyList<ServiceProviderSignup_Dashboard>? CommunityCount_List { get; set; }
        public IReadOnlyList<ServiceProv_Count>? ScopeOfWork_Count_List { get; set; }
        public IReadOnlyList<ServiceProv_Count>? ServiceProv_ZoneWise_List { get; set; }
        public IReadOnlyList<ServiceProv_LongShortTerm>? LongShort_Term_List { get; set; }
        public IReadOnlyList<ServiceProv_Count>? TotalPermit_Count { get; set; }
        public IReadOnlyList<MonthlyStatistics_Model>? Monthly_Statistic_List { get; set; }
    }
    public class ServiceProvider_Dashboard
    {
        public ServiceProvider_Model? Get_Data { get; set; }
        public IReadOnlyCollection<ServiceProvider_Model>? Get_Data1 { get; set; }
        public string? Message { get; set; }
        public string? Status { get; set; }
        public string? Status_Code { get; set; }

    }
    public class MonthlyStatistics_Model
    {
        public string? Approval_Pending { get; set; }
        public string? Approved { get; set; }
        public string? Rejected { get; set; }
        public string? Submitted { get; set; }
        public string? Not_Submitted { get; set; }
    }
}
