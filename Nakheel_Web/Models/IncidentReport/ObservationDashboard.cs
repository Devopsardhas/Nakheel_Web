namespace Nakheel_Web.Models.IncidentReport
{
    public class ObservationDashboard
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
    public class DashboardOBSParam
    {
        public string? Year { get; set; }
        public string? CreatedBy { get; set; }
        public string? BU_ID { get; set; }
        public string? PB_ID { get; set; }
        public string? Six_Month { get; set; }
        public string? From_Date { get; set; }
        public string? To_date { get; set; }
        public string? Controller_Type { get; set; }
        public string? Category_Type { get; set; }
        public string? Three_Month { get; set; }
        public string? Category_Name { get; set; }
        public string? Employee_Type { get; set; }
        public string? Card_View_Id { get; set; }
    }
    public class Observation_Dashboard_Count
    {
        public string? Total_Observation { get; set; }
        public string? Total_Open_OBS { get; set; }
        public string? Total_Closed_OBS { get; set; }
        public string? Total_Today_Count { get; set; }
        public string? Total_Approval_Pending { get; set; }
        public string? Total_Action { get; set; }
        public string? Open_Action { get; set; }
        public string? Closed_Action { get; set; }
        public string? OverDue_Action { get; set; }
        public string? Total_Positive { get; set; }
        public string? Total_Negative { get; set; }
        public string? Total_Health_Safety { get; set; }
        public string? Total_Environment { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? Employee_Name { get; set; }
        public string? Observation_Time { get; set; }
    }
    public class Obs_Dashboard_Count
    {
        public string? Count { get; set; }
        public string? Name { get; set; }
    }
    public class Observation_Dashboard_Graph_List
    {
        public IReadOnlyList<ObservationDashboard>? Graph_Obs_Category { get; set; }
        public IReadOnlyList<ObservationDashboard>? Graph_Obs_Type { get; set; }
        //public IReadOnlyList<ObservationDashboard>? Graph_Nature_Injury { get; set; }
        public IReadOnlyList<ObservationDashboard>? Graph_Zone { get; set; }
        //public IReadOnlyList<ObservationDashboard>? Graph_NCM_Injured { get; set; }
        public IReadOnlyList<Observation_Dashboard_Count>? G_List_Location { get; set; }
        public IReadOnlyList<Observation_Dashboard_Count>? G_List_Building { get; set; }
        public IReadOnlyList<Observation_Dashboard_Count>? G_List_Inc_Type { get; set; }
        //public IReadOnlyList<Observation_Dashboard_Count>? G_List_Inc_Nature_Injury { get; set; }
        public IReadOnlyList<Observation_Dashboard_Count>? G_Cards_Obs_Counts { get; set; }
        public IReadOnlyList<ObservationDashboard>? Graph_Inc_Company { get; set; }
        public IReadOnlyList<Obs_Dashboard_Count>? Zone_Count_List { get; set; }
        public IReadOnlyList<ObservationDashboard>? Community_Count_List { get; set; }
        public IReadOnlyList<Obs_Dashboard_Count>? Raised_By_Ser_Prov_List { get; set; }
        public IReadOnlyList<Obs_Dashboard_Count>? Raised_By_Against_Ser_Prov_List { get; set; }
        public IReadOnlyList<Obs_Dashboard_Count>? Zone_Base_Emp_Details { get; set; }

    }
    public class Observation_Dashboard
    {
        public Observation_Dashboard_Graph_List? Get_Data { get; set; }
        public string? Message { get; set; }
        public string? Status { get; set; }
        public string? Status_Code { get; set; }

    }
}
