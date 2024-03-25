
using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Models.IncidentMaster;
using Nakheel_Web.Models.Masters;
using System.ComponentModel.DataAnnotations;


namespace Nakheel_Web.Models.SecurityIncidentMaster
{
    public class SECINCIDENT_CATEGORY
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Sec_Inc_Category_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        //[RegularExpression("^[a-zA-Z0-9- &/().]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string? Sec_Inc_Category_Name { get; set; }
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
    public class GET_SECINCIDENT_CATEGORY
    {
        public IReadOnlyCollection<SECINCIDENT_CATEGORY>? Get_All { get; set; }
        public SECINCIDENT_CATEGORY? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    public class SUB_SECURITY_INCIDENT
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Sec_Inc_Sub_cat_Id { get; set; }
        public string? Sec_Inc_Category_Id { get; set; }
        public string? Sec_Inc_Category_Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string? Sec_Inc_Sub_Name { get; set; }
        public string? Unique_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
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
    public class GET_SUB_SECURITY_INCIDENT
    {
        public IReadOnlyCollection<SUB_SECURITY_INCIDENT>? Get_All { get; set; }
        public SUB_SECURITY_INCIDENT? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
}

