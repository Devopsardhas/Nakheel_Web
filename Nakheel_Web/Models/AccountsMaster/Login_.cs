using Nakheel_Web.Models.Masters;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;

namespace Nakheel_Web.Models.AccountsMaster
{
    public class Login_
    {
        public string? Employee_Identity_Id { get; set; }
        public string? Employee_Id { get; set; }
        [Required]
        public string? Department_Id { get; set; }
        [Required]
        public string? First_Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email_Id { get; set; }
        [Required]
        public string? Mobile_Number { get; set; }
        public string? Address { get; set; }
        public string? Location_Name { get; set; }
        public string? Department_Name { get; set; }
        public string? Designation_Id { get; set; }
        public string? Designation_Name { get; set; }
        public string? Type_Name { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? Project_Building_Name { get; set; }
        public string? Role_Id { get; set; }
        public string? Role_Name { get; set; }
        public string? Project_Building_Id { get; set; }
        public string? Is_Common { get; set; }
        public string? User_Name { get; set; }
        public string? Password { get; set; }
        public string? Confirm_Password { get; set; }
        public string? AD_Id { get; set; }
        public string? Email_OTP { get; set; }
        public string? Status_Code { get; set; }
        public string? Status { get; set; }
        public bool Ret_Status { get; set; }
        public string? Message { get; set; }
        public string? Zone_Id { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? JWT_Token { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string? Employee_Type { get; set; }
        public string? Company_Name { get; set; }
        public string? CreatedBy { get; set; }
        public Employee_Common_Model? Employee_Common_List { get; set; }
    }
    public class Employee_Common_Model
    {
        public List<Employee_Sub_Model>? Employee_Role_List { get; set; }
        public List<Employee_Sub_Model>? Employee_Project_Building_List { get; set; }
        public List<Employee_Sub_Model>? Employee_Business_List { get; set; }
        public List<Employee_Sub_Model>? Employee_Type_List { get; set; }
        public List<Employee_Sub_Model>? Emp_Community_List { get; set; }
        public List<Employee_Sub_Model>? Emp_Master_Community_List { get; set; }
        public List<Employee_Sub_Model>? Emp_Building_List { get; set; }
    }
    public class Employee_Sub_Model
    {
        public string? Identity_Id { set; get; }
        public string? Common_Id { set; get; }
        public string? Common_Name { set; get; }
        public string? Employee_Id { set; get; }
        public string? Employee_Identity_Id { set; get; }
    }
    public class Email_Val
    {
        [Required(ErrorMessage = "Email Address is required")]
        [DataType(DataType.EmailAddress)]
        //[RegularExpression(@"^[a-zA-Z0-9._%+-]+@dlf\.in$", ErrorMessage = "Email must be from dlf.in domain")]
        [MaxLength(50)]
        public string? Email_ID { get; set; }
        public string? Password { get; set; }
        public string? OTP { get; set; }
    }

    public class PassOtp
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        //[RegularExpression(@"^[a-zA-Z0-9._%+-]+@dlf\.in$", ErrorMessage = "Email must be from dlf.in domain")]
        [MaxLength(50)]
        public string? Email_ID { get; set; }
        //[RequiredIf("OTP", null, ErrorMessage = "Either PhoneNumber or Email is required")]
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(50)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "OTP is required")]
        [MaxLength(50)]
        public string? Email_OTP { get; set; }
    }

    public class SignUp
    {
        public string? SignUp_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(10)]
        public string? Employee_Identity_Id { get; set; }
        public string? Employee_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Employee_Type { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Company_Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? First_Name { get; set; }

        public string? Last_Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.EmailAddress)]
        public string? Email_Id { get; set; }

        public string? Designation_Id { get; set; }
        public string? Designation_Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Business_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Type_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string[]? Project_Building_Id { get; set; }
        public string? Location_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string? Mobile_Number { get; set; }
        public string? Role_Id { get; set; }
        public string? Role_Name { get; set; }
        public string? Email_OTP { get; set; }
        public string? HSE_BU_Head { get; set; }
        public string? Is_In_Ex_Employee { get; set; }
        public string? Unique_Id { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
        public string? Immediate_Supervisor_Id { get; set; }
        public string? Line_Manger_Id { get; set; }
        public string? HSE_Representative_Id { get; set; }
        public string? HSE_Head_Id { get; set; }
    }

    public class LoginModel
    {
        public Email_Val? Email { get; set; }
        public PassOtp? Pass { get; set; }
        public SignUp? Signup { get; set; }
    }

    public class GET_LOGIN_DETAILS
    {
        public Login_? Get_User { get; set; }
        public List<Login_>? Get_All { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    public class ServiceProviderSignUp :M_Common_Fields
    {
        public string? SignUp_Id { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_Id { get; set; }
        public string? Company_Name { get; set; }
        public string? Inc_Business_Unit_Type { get; set; }
        public string? Manager_Incharge { get; set; }
        public string? Designation { get; set; }
        public string? Mobile_No { get; set; }
        public string? Official_Email_Id { get; set; }
        public string? Purchase_Order_Number { get; set; }
        public string? DMS_Number { get; set; }
        public string? HSE_Officer { get; set; }
        public string? HSE_Officer_Email { get; set; }
        public string? HSE_Officer_Mobile_Number { get; set; }
        public string? Contract_Start_Date { get; set; }
        public string? Contract_End_Date { get; set; }
        public string? W_Name_Position { get; set; } //Project Title
        public string? W_Contact { get; set; }  // Contract Type
        public string? W_Email_Id { get; set; }
        public string? W_Work_Description { get; set; }
        public string? W_Specific_Requirements { get; set; }
        public string? W_From_Date { get; set; }
        public string? W_To_date { get; set; }
        public string? W_Night_Schedule { get; set; }
        public string? W_Permit_Validity { get; set; }
        public string? W_Scope_of_work { get; set; }
        public string? Company_Id { get; set; }
        public string? ApproveRes_1 { get; set; }
        public string? ApproveRes_2 { get; set; }
        public List<L_M_Work_Superviosor_List>? L_M_Work_Superviosor_List { get; set; }
        public List<L_M_RequiredAttachments_List>? L_M_RequiredAttachments_List { get; set; }
        public List<L_M_Major_HSE_Risk_List>? L_M_Major_HSE_Risk_List { get; set; }
        public List<L_M_Service_Provider_Update_History>? L_M_Service_Provider_Update_History { get; set; }
        public List<L_M_Building_List>? L_M_Building_List { get; set; }
    }

    public class L_M_Building_List
    {
        public string? M_Service_Provider_Bu_Id { get; set; }
        public string? SignUp_Id { get; set; }
        public string? Building_Id { get; set; }
    }

    public class L_M_Work_Superviosor_List
    {
        public string? Work_Superviosor_Id { get; set; }
        public string? SignUp_Id { get; set; }
        public string? Name_Position { get; set; }
        public string? Contact { get; set; }
        public string? Email_Id { get; set; }
    }
    public class L_M_RequiredAttachments_List
    {
        public string? RequiredDcouments_Id { get; set; }
        public string? SignUp_Id { get; set; }
        public string? Document_Type { get; set; }
        public string? Required_File_Path { get; set; }
        public string? Expiry_Date { get; set; }
        public string? Description { get; set; }
    }

    public class L_M_Major_HSE_Risk_List
    {
        public string? Major_HSE_Risk_Id { get; set; }
        public string? SignUp_Id { get; set; }
        public string? Major_HSE_Work_Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
        public string? Description { get; set; }
    }

    public class L_M_Service_Provider_Update_History
    {
        public string? Service_Provider_History_Id { get; set; }
        public string? SignUp_Id { get; set; }
        public string? Emp_Id { get; set; }
        public string? Role_Id { get; set; }
        public string? Updated_DateTime { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public string? Remarks1 { get; set; }
        public string? Remarks2 { get; set; }
        public string? Remarks3 { get; set; }
        public string? Remarks4 { get; set; }
        public string? Remarks5 { get; set; }
    }

    public class HSE_Plan_Document
    {
        public string? HSE_Plan_Id { get; set; }
        public string? SignUp_Id { get; set; }
        public string? HSE_Plan_File_Path { get; set; }
    }
    public class Risk_Assessments_Document
    {
        public string? Risk_Assessments_Id { get; set; }
        public string? SignUp_Id { get; set; }
        public string? Risk_Assessments_File_Path { get; set; }
    }
    public class Method_Statement_Document
    {
        public string? Method_Statement_Id { get; set; }
        public string? SignUp_Id { get; set; }
        public string? Method_Statement_File_Path { get; set; }
    }
    public class Emergency_Plan_Document
    {
        public string? Emergency_Plan_Id { get; set; }
        public string? SignUp_Id { get; set; }
        public string? Emergency_Plan_File_Path { get; set; }
    }
    public class Insurance_Details_Document
    {
        public string? Insurance_Details_Id { get; set; }
        public string? SignUp_Id { get; set; }
        public string? Insurance_Details_File_Path { get; set; }
    }


    public class Get_ServiceProviderSignUp
    {
        public IReadOnlyCollection<ServiceProviderSignUp>? Get_All { get; set; }
        public ServiceProviderSignUp? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }

    public class Service_Company_Details
    {
        public string? Company_Id { get; set; }
        public string? Company_Name { get; set; }
        public string? Company_MobileNo { get; set; }
        public string? Company_Email { get; set; }
    }

    public class Get_Service_Company_Details
    {
        public IReadOnlyCollection<Get_Service_Company_Details>? Get_All_Company_Details { get; set; }
        public Service_Company_Details? Get_Company_Details { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
}
