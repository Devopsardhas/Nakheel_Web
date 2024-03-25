using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Models.IncidentReport;
using System.ComponentModel.DataAnnotations;

namespace Nakheel_Web.Models.Masters
{
    public class M_Incident_Relate_Master
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Incident_Related_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^[a-zA-Z0-9- &/().]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [MaxLength(500)]
        [DataType(DataType.Text)]
        public string? Incident_Related_Name { get; set; }
        public string? Unique_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^[a-zA-Z0-9- &/().]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [MaxLength(500)]
        [DataType(DataType.Text)]
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
    }

    public class GET_INCIDENT_RELATE_MASTER
    {
        public IReadOnlyCollection<M_Incident_Relate_Master>? Get_All { get; set; }
        public M_Incident_Relate_Master? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }


    public class Level_Master
    {
        public string? LevelM_Id { get; set; }
        public string? LevelM_Name { get; set; }
        public string? Unique_Id { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
    }

    public class Get_Level_Master
    {
        public IReadOnlyCollection<Level_Master>? Get_All { get; set; }
        public Level_Master? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }

    public class Crisis_Master
    {
        public string? Crisis_Master_Id { get; set; }
        public string? LevelM_Id { get; set; }
        public string? Business_Unit_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Unique_Id { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
        public List<Crisis_SubEmp_Master>? L_Crisis_SubEmp_Master_Details { get; set; }
    }


    public class Crisis_Team_Master
    {
        public string? Crisis_Master_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Unique_Id { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
        public List<Crisis_Emp_Team_Master>? L_M_Crisis_Team_Details { get; set; }
    }

    public class Crisis_Emp_Team_Master
    {
        public string? Crisis_Emp_Team_Master_Id { get; set; }
        public string? Crisis_Team_Master_Id { get; set; }
        public string? Level_Id { get; set; }
        public string? Role { get; set; }
        public string? Position { get; set; }
        public string? Name { get; set; }
        public string? Mobile { get; set; }
        public string? A_Role { get; set; }
        public string? A_Position { get; set; }
        public string? A_Name { get; set; }
        public string? A_Mobile { get; set; }
        public string? CreatedBy { get; set; }
        public string? Remarks { get; set; }
    }


    public class Crisis_SubEmp_Master
    {
        public string? Crisis_Sub_Id { get; set; }
        public string? Crisis_Master_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? LevelM_Id { get; set; }
        public string? Emp_Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
    }


    public class Get_Crisis_Master
    {
        public IReadOnlyCollection<Crisis_Team_Master>? Get_All { get; set; }
        public Crisis_Team_Master? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }

    public class Get_Crisis_SubEmp_Master
    {
        public IReadOnlyCollection<Crisis_SubEmp_Master>? Get_All { get; set; }
        public Crisis_SubEmp_Master? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }



    public class Emergency_Category_Master
    {
        public string? Emergency_Category_Id { get; set; }
        public string? Emergency_Category_Name { get; set; }
        public string? Unique_Id { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
    }


    public class Get_Emergency_Category_Master
    {
        public IReadOnlyCollection<Emergency_Category_Master>? Get_All { get; set; }
        public Emergency_Category_Master? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }


    public class Emergency_Alert_Master
    {
        public string? Emergency_Alert_Id { get; set; }
        public string? Emergency_Category_Id { get; set; }
        public string? Unique_Id { get; set; }
        public string? Description { get; set; }
        public string? Emergency_Date { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
        public List<Emergency_SubZone_Alert_Master>? L_Emergency_SubZone_Alert_Master { get; set; }
    }
    public class Emergency_SubZone_Alert_Master
    {
        public string? Emergency_SubZoneAlert_Id { get; set; }
        public string? Emergency_Alert_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class Get_Emergency_Alert_Master
    {
        public IReadOnlyCollection<Emergency_Alert_Master>? Get_All { get; set; }
        public Emergency_Alert_Master? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }


    public class Service_Provider_Scope_of_Work
    {
        public string? Scope_of_Work_Id { get; set; }
        public string? Scope_of_Work { get; set; }
        public string? Unique_Id { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
    }

    public class Get_Service_Provider_Scope_of_Work
    {
        public IReadOnlyCollection<Service_Provider_Scope_of_Work>? Get_All { get; set; }
        public Service_Provider_Scope_of_Work? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }


    public class ERT_Team_Details
    {
        public string? ERT_Id { get; set; }
        public string? Employee_Name { get; set; }
        public string? Gender { get; set; }
        public string? Langauge { get; set; }
        public string? Nationality { get; set; }
        public string? Age { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }
        public string? Role { get; set; }
        public string? Type { get; set; }
        public string? Certificate_Date { get; set; }
        public string? Exprity_Date { get; set; }
        public string? Staff_Status { get; set; }
        public string? Training_Status { get; set; }
        public string? Business_Unit { get; set; }
        public string? Building_No { get; set; }
        public string? Floor_No { get; set; }
        public string? Declaration_Completed { get; set; }
        public string? ERT_Upload_File { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? Is_Active { get; set; }
        public string? Status { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Unique_Id { get; set; }
        public string? Card_Id { get; set; }
    }



    public class ERT_Team_Details_Master
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Unique_Id { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
    }


    public class Get_ERT_Team_Details_Master
    {
        public IReadOnlyCollection<ERT_Team_Details_Master>? Get_All { get; set; }
        public ERT_Team_Details_Master? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }

    public class Get_ERT_Team_Details
    {
        public IReadOnlyCollection<ERT_Team_Details>? Get_All { get; set; }
        public ERT_Team_Details? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
}


