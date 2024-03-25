using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nakheel_Web.Models.Masters
{
    public class BUSINESS_UNIT
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Business_Unit_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        //[RegularExpression("^[a-zA-Z0-9- &/().]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [MaxLength(500)]
        [DataType(DataType.Text)]
        public string? Business_Unit_Name { get; set; }
        public string? Unique_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        //[RegularExpression("^[a-zA-Z0-9- &/().]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
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

    public class GET_BUSINESS_UNIT
    {
        public IReadOnlyCollection<BUSINESS_UNIT>? Get_All { get; set; }
        public BUSINESS_UNIT? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
}
