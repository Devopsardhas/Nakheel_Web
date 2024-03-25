using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Nakheel_Web.Authentication
{
    public class ExpFilter : Attribute, IExceptionFilter
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public ExpFilter(IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {

            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];
            var result = new ViewResult { ViewName = "Error" };
            //result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
            //result.ViewData.Add("Exception", context.Exception);

            // Here we can pass additional detailed data via ViewData
            context.ExceptionHandled = true; // mark exception as handled
            context.Result = result;

            Log.LogError(context.Exception, controllerName!.ToString()!, actionName!.ToString()!);
        }
    }
}
