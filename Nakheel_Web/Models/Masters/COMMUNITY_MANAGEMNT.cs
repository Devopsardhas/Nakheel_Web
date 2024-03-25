using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Models.InspectionMaster;
using System.ComponentModel.DataAnnotations;

namespace Nakheel_Web.Models.Masters
{
    public class COMMUNITY_MANAGEMNT
    {
        public string? Community_Master_Id { get; set; }
        public string? Business_Unit_Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Zone_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        //[RegularExpression("^[a-zA-Z0-9- &/().]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string? Community_Master_Name { get; set; }
        public string? Unique_Id { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
        public string? Is_Community { get; set; }
        public string? Is_Master_Community { get; set; }
    }

    public class GET_COMMUNITY_MANAGEMNT
    {
        public IReadOnlyCollection<COMMUNITY_MANAGEMNT>? Get_All { get; set; }
        public COMMUNITY_MANAGEMNT? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }


    public class M_Get_SafetyPer_Dash_Card_View_Data
    {
        public IReadOnlyCollection<SafetyPer_Dash_Card_View_Data>? Get_All { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }

    public class SafetyPer_Dash_Card_View_Data
    {
        public string? CSP_Id { get; set; }
        public string? Unique_Id { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? Zone_Name { get; set; }
        public string? Community_Name { get; set; }
        public string? Building_Name { get; set; }
        public string? Company_Name { get; set; }
        public string? Created_by { get; set; }
        public string? CreatedDate { get; set; }
        public string? Status { get; set; }
        public string? Permit_Name { get; set; }
        public string? Action { get; set; }
        public string? Priority_Module { get; set; }
    }

    public class M_Get_Audit_Dash_Card_View_Data
    {
        public IReadOnlyCollection<Audit_Dash_Card_View_Data>? Get_All { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }


    public class Audit_Dash_Card_View_Data
    {
        public string? Audit_Id { get; set; }
        public string? Unique_Id { get; set; }
        public string? Business_Unit_Name { get; set; }
        public string? Zone_Name { get; set; }
        public string? Community_Name { get; set; }
        public string? Building_Name { get; set; }
        public string? Company_Name { get; set; }
        public string? Created_by { get; set; }
        public string? CreatedDate { get; set; }
        public string? Status { get; set; }
        public string? Audit_Type_Name { get; set; }
        public string? HSE_Rep_Name { get; set; }
        public string? Action { get; set; }
        public string? Service_Provider_Name { get; set; }
        public string? Initial_Date { get; set; }
    }
}
