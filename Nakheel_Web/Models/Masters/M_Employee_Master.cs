using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nakheel_Web.Models.Masters
{
    public class M_Employee_Master
    {
        public string? Employee_Identity_Id { get; set; }
        public string? Employee_Id { get; set; }
        public string? Employee_Type { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? Unique_Id { get; set; }
        public string? Email_Id { get; set; }
        public string? Mobile_Number { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? Zone_Id { get; set; }
        public string? Zone_Name { get; set; }
        public string? Department_Id { get; set; }
        public string? Department_Name { get; set; }
        public string? Role_Id { get; set; }
        public string? Role_Name { get; set; }
        public string? Designation_Id { get; set; }
        public string? Designation_Name { get; set; }
        public string? User_Name { get; set; }
        public string? Password { get; set; }
        public string? Confirm_Password { get; set; }
        public string? Email_OTP { get; set; }
        public string? Type_Name { get; set; }
        public string? Project_Building_Name { get; set; }
        public string? Company_Name { get; set; }
        public string? Line_Manager_Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? Line_Manager_Name { get; set; }
        public List<M_Employee_Community_Model>? L_Employee_Community_List { get; set; }
        public List<M_Employee_Building_Model>? L_Employee_Building_List { get; set; }
        public List<M_Employee_Master_Community_Model>? L_Employee_Master_Community_List { get; set; }
    }
    
    public class M_Employee_Community_Model
    {
        public string? Emp_Community_Id { set; get; }
        public string? Employee_Identity_Id { set; get; }
        public string? Zone_Id { set; get; }
        public string? Business_Unit_Id { set; get; }
        public string? Community_Id { set; get; }
        public string? Community_Master_Name { set; get; }
    }
    public class M_Employee_Building_Model 
    {
        public string? Emp_Building_Id { set; get; }
        public string? Employee_Identity_Id { set; get; }
        public string? Zone_Id { set; get; }
        public string? Business_Unit_Id { set; get; }
        public string? Building_Id { set; get; }
        public string? Building_Name { set; get; }
    }
    public class M_Employee_Master_Community_Model 
    {
        public string? Emp_Master_Community_Id { set; get; }
        public string? Employee_Identity_Id { set; get; }
        public string? Zone_Id { set; get; }
        public string? Business_Unit_Id { set; get; }
        public string? Master_Community_Id { set; get; }
    }
    public class GET_EMPLOYEE_MASTER
    {
        public IReadOnlyCollection<M_Employee_Master>? Get_All { get; set; }
        public M_Employee_Master? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }

    public class M_Employee_Master_Filter
    {
        public string? Zone_Id { set; get; }
        public string? Emp_Master_Community_Id { set; get; }
        public string? Emp_Building_Id { set; get; }
        public string? Emp_Community_Id { set; get; }
    }

    public class M_Employee_Master_Filter_by_Role
    {
        public string? Role_Name { get; set; }
        public string? Zone_Id { set; get; }
        public string? Emp_Community_Id { set; get; }
    }
    //public class M_Inc_Health_Observation_Type
    //{
    //    public string? Inc_Obser_Health_Id { get; set; }
    //    public string? Inc_Obser_Report_Id { get; set; }
    //    public string? Health_Safety_Id { get; set; }
    //    public string? Health_Safety_Name { get; set; }
    //}
}
