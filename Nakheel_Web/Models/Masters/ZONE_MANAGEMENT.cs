using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nakheel_Web.Models.Masters
{
    public class ZONE_MANAGEMENT
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Zone_Id { get; set; }

        public string? Business_Unit_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^[a-zA-Z0-9- &/().]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string? Zone_Name { get; set; }
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

    public class GET_ZONE_MANAGEMENT
    {
        public IReadOnlyCollection<ZONE_MANAGEMENT>? Get_All { get; set; }
        public ZONE_MANAGEMENT? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
}
