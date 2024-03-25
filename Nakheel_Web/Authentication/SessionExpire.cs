using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Nakheel_Web.Authentication
{
    public class SessionExpireActionFilter : Attribute, IActionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SessionExpireActionFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            var session = _httpContextAccessor.HttpContext!.Session.Get("Login");
            if (session == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", controller = "Account" }));
            }
        }

        /// <summary>
        /// OnActionExecuted
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
