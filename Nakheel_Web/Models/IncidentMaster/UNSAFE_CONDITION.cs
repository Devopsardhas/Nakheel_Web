using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nakheel_Web.Models.IncidentMaster
{
    public class UNSAFE_CONDITION
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Unsafe_Condition_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string? Unsafe_Condition_Name { get; set; }
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

    public class GET_UNSAFE_CONDITION
    {
        public IReadOnlyCollection<UNSAFE_CONDITION>? Get_All { get; set; }
        public UNSAFE_CONDITION? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
}
