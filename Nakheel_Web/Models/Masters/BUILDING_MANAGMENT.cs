using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Models.HandOverInsMaster;
using Nakheel_Web.Models.IncidentReport;
using System.ComponentModel.DataAnnotations;

namespace Nakheel_Web.Models.Masters
{
    public class BUILDING_MANAGMENT
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Building_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Community_Master_Name { get; set; }
        //public string? Building_Description { get; set; }
        public string? Unique_Id { get; set; }
        public string? CreatedDate { get; set; }
        public List<M_Building_List>? L_M_Building { get; set; }
    }

    public class M_Building_List
    {
        public string? Sub_Building_Id { get; set; }
        public string? Building_Id { get; set; }
        public string? Building_Name { get; set; }
        public string? Building_Description { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class GET_BUILDING_MANAGEMNT
    {
        //public List<BUILDING_MANAGMENT>? Get_All_Building { get; set; }
        public IReadOnlyCollection<BUILDING_MANAGMENT>? Get_All { get; set; }
        public IReadOnlyCollection<M_Building_List>? Get_All_Sub { get; set; }
        public BUILDING_MANAGMENT? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }

    public class Upcoming_Activity_Dashboard
    {
        public string? Activity_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_Id { get; set; }
        public string? Module_Name { get; set; }
        public string? Hyper_Link { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
        public string? Description { get; set; }
        public string? Schedule_Type_Id { get; set; }
        public string? Status { get; set; }
        public string? Role_Id { get; set; }
        public string? Target_Date { get; set; }
        public string? Color_Code { get; set; }
    }

    public class Upcoming_Activity_Dashboard_Team_Task
    {
        public string? Activity_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_Id { get; set; }
        public string? Module_Name { get; set; }
        public string? Hyper_Link { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
        public string? Description { get; set; }
        public string? Schedule_Type_Id { get; set; }
        public string? Status { get; set; }
        public string? Role_Id { get; set; }
        public string? Target_Date { get; set; }
        public string? Color_Code { get; set; }
    }

    public class Upcoming_Activity_Dashboard_My_Task
    {
        public string? Activity_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_Id { get; set; }
        public string? Module_Name { get; set; }
        public string? Hyper_Link { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
        public string? Description { get; set; }
        public string? Schedule_Type_Id { get; set; }
        public string? Status { get; set; }
        public string? Role_Id { get; set; }
        public string? Target_Date { get; set; }
        public string? Color_Code { get; set; }
    }

    public class Upcoming_Activity_Dashboard_Emer_Escal
    {
        public string? Activity_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_Id { get; set; }
        public string? Module_Name { get; set; }
        public string? Hyper_Link { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
        public string? Description { get; set; }
        public string? Schedule_Type_Id { get; set; }
        public string? Status { get; set; }
        public string? Role_Id { get; set; }
        public string? Target_Date { get; set; }
        public string? Color_Code { get; set; }
    }

    public class Emergency_SubZone_Alert_Master_Dashboard
    {
        public string? Emergency_SubZoneAlert_Id { get; set; }
        public string? Emergency_Alert_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? Emergency_Category_Id { get; set; }
        public string? Description { get; set; }
        public string? Emergency_Date { get; set; }
    }
    public class Hse_Bulletin
    {
        public string? HSE_Bulletin_Id { get; set; }
        public string? HSE_Bulletin_Name { get; set; }
        public string? File_Path { get; set; }
    }
    public class MainDashBoards_Model
    {
        public string? CreatedBy { get; set; }
        public string? Zone_Id { get; set; }
        public List<Upcoming_Activity_Dashboard>? L_M_Upcoming_Activity { get; set; }
        public List<Notification_Dashboard_Count>? L_M_Notification_Dashboard_Count { get; set; }
        public List<Upcoming_Activity_Dashboard_Team_Task>? L_M_Upcoming_Activity_Team { get; set; }
        public List<Upcoming_Activity_Dashboard_My_Task>? L_M_Upcoming_Activity_My { get; set; }
        public List<Upcoming_Activity_Dashboard_Emer_Escal>? L_M_Upcoming_Activity_Emer_Escal { get; set; }
        public List<Emergency_SubZone_Alert_Master_Dashboard>? L_M_Emergency_SubZone_Alert_Master_Dashboard { get; set; }
        public Notification_Dashboard_Count? Main_Dash_Insp_Count { get; set; }
        public List<Hse_Bulletin>? L_Hse_Bulletins { get; set; }
    }

    public class Get_Dashboard_Details
    {
        public MainDashBoards_Model? Get_All { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }
}
