using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace EduHub.Filters
{
    public class ActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            /*var result = context.Result;

            if (!(result is OkObjectResult))
            {
                Log.Warning(JsonConvert.SerializeObject(result), "result is not ok");
            }
            else
            {
                Log.Verbose(JsonConvert.SerializeObject(result));
            }*/
            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                Log.Warning(context.ActionDescriptor.DisplayName + " model is not valid", "some");
            }

            Log.Information("Received arguments {@Arguments}", context.ActionArguments);
            base.OnActionExecuting(context);
        }
    }
}