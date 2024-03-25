using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nakheel_Web.Models.Masters
{
    public class Hse_BulletinMaster
    {
        public string? HSE_Bulletin_Id { get; set; }
        public string? HSE_Bulletin_Name { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
       public string? Unique_Id { get; set; }
        public List<file_Upload_Bulletin>? File_Upl_List { get; set; }
    }
    public class file_Upload_Bulletin
    {
        public string? Bulletin_File_Upl_Id { get; set; }
        public string? HSE_Bulletin_Id { get; set; }
        public string? File_Path { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        
       
    }
    public class GET_Hse_Bulletin
    {
        public IReadOnlyCollection<Hse_BulletinMaster>? Get_All { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }

}
