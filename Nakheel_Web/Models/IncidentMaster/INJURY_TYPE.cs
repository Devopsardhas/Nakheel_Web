using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nakheel_Web.Models.IncidentMaster
{
    public class INJURY_TYPE
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Injury_Type_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^[a-zA-Z0-9- &/().]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string? Injury_Type_Name { get; set; }
        public string? Unique_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^[a-zA-Z0-9- &/().]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [MaxLength(50)]
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

    public class GET_INJURY_TYPE
    {
        public IReadOnlyCollection<INJURY_TYPE>? Get_All { get; set; }
        public INJURY_TYPE? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
}
