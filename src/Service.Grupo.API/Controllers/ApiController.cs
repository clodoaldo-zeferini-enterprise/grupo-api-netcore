using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Service.Grupo.Application.Models.Response.Errors;
using System.Text;

namespace Service.Grupo.API.Controllers
{
    public class ApiController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ErrorsResponse errors = new ErrorsResponse();

                StringBuilder errorMessage;

                foreach(var modelState in context.ModelState)
                {
                    ErrorResponse error = new ErrorResponse(Guid.NewGuid().ToString(), modelState.Key);

                    if (modelState.Value.Errors.Count > 0)
                    {
                        errorMessage = new StringBuilder();

                        foreach(var erro in modelState.Value.Errors)
                        {
                            errorMessage.Append(erro.ErrorMessage + "\n");
                        }
                        error.AddMessage(errorMessage.ToString());
                        errors.Errors.Add(error);
                    }
                }

                context.Result = new BadRequestObjectResult(errors);
            }

            var authorization = context.HttpContext.Request.Headers.Authorization;

            /*Como decriptografar um Token*/

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //Faça algo após a ação ser executada

            /*gravarlog*/
            /*
            string request = JsonConvert.SerializeObject(context.HttpContext.Request);
            string response = JsonConvert.SerializeObject(context.HttpContext.Response);
            */
            base.OnActionExecuted(context);
        }
    }
}
