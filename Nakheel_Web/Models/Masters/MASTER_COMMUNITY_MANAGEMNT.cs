using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nakheel_Web.Models.Masters
{

    public class MASTER_COMMUNITY_MANAGEMNT
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? MasterCommunity_Id { get; set; }
        public string? Zone_Id { get; set; }
        public string? Community_Id { get; set; }
        public string? Community_Master_Name { get; set; }
        public string? Unique_Id { get; set; }
        public string? CreatedDate { get; set; }
        public List<M_MasterCommunity_List>? L_M_MasterCommunity_List { get; set; }
    }

    public class M_MasterCommunity_List
    {
        public string? Sub_MasterCommunity_Id { get; set; }
        public string? MasterCommunity_Id { get; set; }
        public string? MasterCommunity_Name { get; set; }
        public string? MasterCommunity_Description { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class GET_MASTER_COMMUNITY_MANAGEMNT
    {
        public IReadOnlyCollection<MASTER_COMMUNITY_MANAGEMNT>? Get_All { get; set; }
        public IReadOnlyCollection<M_MasterCommunity_List>? Get_All_Sub { get; set; }
        public MASTER_COMMUNITY_MANAGEMNT? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
}
