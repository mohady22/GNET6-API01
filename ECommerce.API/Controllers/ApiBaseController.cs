using ECommerce.Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        public static ActionResult<T> ToActionResult<T>(Result<T> results)
        {
            if (results.IsSuccess)
                return new OkObjectResult(results.data);

            return ToProblem(results.Errors);

        }
        public static ActionResult<T> ToActionResult<T>(Result result)
        {
            if (result.IsSuccess)
                return new OkResult();

            return ToProblem(result.Errors);

        }
        public static ObjectResult ToProblem(IReadOnlyList<Error> errors)
        {
            var first = errors[0];

            var status = first.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.UnAuthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError,
            };
            var problem = new ProblemDetails
            {
                Status = status,
                Title = first.code,
                Detail = first.Description,
                Extensions = { ["errors"] = errors }
            };
            return new ObjectResult(problem);
        }

    }
}
