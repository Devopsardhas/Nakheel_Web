using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Authentication;
using Nakheel_Web.Models.Masters;

namespace Nakheel_Web.Notification.Repository
{
    [TypeFilter(typeof(ExpFilter))]
    public class BellNotificationRepo
    {
        string connectionString;
        public BellNotificationRepo(string connectionString)
        {
            this.connectionString = connectionString;

        }
        public List<tbl_Notification_Sequence> GetNotificationList(string ID)
        {

            List<tbl_Notification_Sequence> sequences = new List<tbl_Notification_Sequence>();
            //var str= _httpContextAccesso!.HttpContext!.Session.GetString("Login");
            if (ID != null)
            {
                //string Des = Decrypt(str!);
                //Login_ LoginClass = JsonConvert.DeserializeObject<Login_>(Des!)!;

                //tbl_Notification_Sequence _UNIT = new tbl_Notification_Sequence
                //{
                //    Login_User_Id = ID

                //};
                //HttpResponseMessage response = client.PostAsync(conn + "AccountsM/GetWebBellNotification", new StringContent(JsonConvert.SerializeObject(_UNIT), Encoding.UTF8, "application/json")).Result;
                //string customerJsonString = await response.Content.ReadAsStringAsync();
                //Get_tbl_Notification_Sequence deserialized = JsonConvert.DeserializeObject<Get_tbl_Notification_Sequence>(customerJsonString)!;
                //if (deserialized.STATUS_CODE == "200")
                //{
                //    sequences = deserialized.Get_All_Notifications!;
                //}
            }
            return sequences;
        }
    }
}
