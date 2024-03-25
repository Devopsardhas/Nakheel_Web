using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nakheel_Web.Models.IncidentMaster
{
    public class VEHICLE_ACCIDENT_TYPE
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Vehicle_Accident_Type_Id { get; set; }
        public string? Vehicle_Accident_Type_Name { get; set; }
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

    public class GET_VEHICLE_ACCIDENT_TYPE
    {
        public IReadOnlyCollection<VEHICLE_ACCIDENT_TYPE>? Get_All { get; set; }
        public VEHICLE_ACCIDENT_TYPE? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
}
