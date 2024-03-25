namespace Nakheel_Web.Models.Masters
{
    public class tbl_Notification_Sequence
    {
        public string? ANS_Notification_ID { get; set; }
        public string? ANS_UserAccessID { get; set; }
        public string? ANS_RequestID { get; set; }
        public string? ANS_UserMobile { get; set; }
        public string? ANS_Notification_Type { get; set; }
        public string? ANS_Notification_Language { get; set; }
        public string? ANS_SendFlag { get; set; }
        public string? ANS_createdtime { get; set; }
        public string? ANS_Modifiedtime { get; set; }
        public string? ANS_Notification_Content { get; set; }
        public string? ANS_Attempts { get; set; }
        public string? ANS_Content_Title { get; set; }
        public string? ANS_Content_Title_Description { get; set; }
        public string? ANS_Content_For { get; set; }
        public string? ANS_Notification_Send_Status { get; set; }
        public string? ANS_Notification_Send_Time { get; set; }
        public string? ANS_Notification_Attempts { get; set; }
        public string? ANS_Event_From { get; set; }
        public string? ANS_Read_Status { get; set; }
        public string? ANS_Notification_Set_Id { get; set; }
        public string? ANS_Appointment_Category { get; set; }
        public string? Login_User_Id { get; set; }
        public string? Hyper_Link { get; set; }
        public string? Hyper_Link2 { get; set; }
        public string? Designation { get; set; }
        public string? Notification_Date { get; set; }
    }
    public class Get_tbl_Notification_Sequence
    {
        public List<tbl_Notification_Sequence>? Get_All_Notifications { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }
}
