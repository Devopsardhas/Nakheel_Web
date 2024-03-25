namespace Nakheel_Web.Models.Masters
{
    public class RETURN_MESSAGE
    {
        public string? STATUS_CODE { get; set; }
        public bool STATUS { get; set; }
        public string? MESSAGE { get; set; }
        public string? UNIQUE_ID { get; set; }
        public string? Return_1 { get; set; }
        public string? Return_2 { get; set; }
        public string? Return_3 { get; set; }
    }
    public class RETURN_MESSAGE_ADD
    {
        public string? STATUS_CODE { get; set; }
        public bool STATUS { get; set; }
        public string? MESSAGE { get; set; }
        public string? UNIQUE_ID { get; set; }
        public string? Return_1 { get; set; }
        public string? Return_2 { get; set; }
        public string? Remarks { get; set; }
    }

    public class M_Common_Fields
    {
        public string? Action { get; set; }
        public string? Unique_Id { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Is_Active { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Remarks { get; set; }
        public string? Role_Id { get; set; }
        public string? Login_Id { get; set; }
        public byte[]? Mail_Remarks { get; set; }
        public string? Zone_Id { get; set; }
    }
}
