using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Nakheel_Web.Authentication.Common;
using Nakheel_Web.Models.AccountsMaster;

namespace Nakheel_Web.Authentication
{
    public class AuthorizeRoles : Attribute, IAuthorizationFilter
    {
        private readonly string[] allowedroles;
        public AuthorizeRoles(params string[] roles)
        {
            this.allowedroles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            Login_ LoginClass = new Login_();
            bool check = false;
            var str = context.HttpContext.Session.GetString("Login");
            if (str != null)
            {
                string Des = Decrypt(str!);
                LoginClass = JsonConvert.DeserializeObject<Login_>(Des)!;
            }
            //bool check = allowedroles.Contains("Role2");
            if (LoginClass.Employee_Common_List != null && LoginClass.Employee_Common_List.Employee_Role_List != null)
            {
                check = LoginClass.Employee_Common_List.Employee_Role_List!.Any(x => x.Common_Id == "23" || x.Common_Id == "8");
            }
            if (!check)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
