using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nakheel_Web.Models.Masters
{
    public class M_Location_Master
    {
    }
    public class Location_Master
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Location_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Building_Id { get; set; }
        public string? Location_Name { get;set; }
        public string? Community_Master_Name { get; set; }
        //public string? Location_Description { get; set; }
        public string? Unique_Id { get; set; }
        public string? CreatedDate { get; set; }
        public List<M_Location_List>? L_M_Location { get; set; }
    }

    public class M_Location_List
    {
        public string? Sub_Location_Id { get; set; }
        public string? Location_Id { get; set; }
        public string? Sub_Location_Name { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class GET_Location_Master
    {
        public IReadOnlyCollection<Location_Master>? Get_All { get; set; }
        public IReadOnlyCollection<M_Location_List>? Get_All_Sub { get; set; }
        public Location_Master? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }
}
