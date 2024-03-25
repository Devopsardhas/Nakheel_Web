using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nakheel_Web.Models.IncidentMaster
{
    public class TYPE_SECURITY_INCIDENT
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Inc_Type_Security_Id { get; set; }
        public string? Classification_Id { get; set; }
        public string? Classification_Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string? Inc_Type_Security_Name { get; set; }
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

    public class GET_TYPE_SECURITY_INCIDENT
    {
        public IReadOnlyCollection<TYPE_SECURITY_INCIDENT>? Get_All { get; set; }
        public TYPE_SECURITY_INCIDENT? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
}
