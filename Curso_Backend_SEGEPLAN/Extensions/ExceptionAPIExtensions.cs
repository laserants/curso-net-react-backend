using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Curso_Backend_SEGEPLAN.Extensions
{
    public static class ExceptionAPIExtensions
    {
        public static ActionResult ConvertToActionResult(this Exception exception, HttpContext context)
        {
            var problemDetails = new ProblemDetails()
            {
                Detail = exception.ToString(),
                Instance = context?.Request.Path,
                Title = exception.Message
            };

            switch (exception)
            {
                case ArgumentException _:
                    problemDetails.Status = 400;
                    problemDetails.Type = "https://httpstatuses.com/400";

                    return new BadRequestObjectResult(problemDetails);

                case DbUpdateException _:
                    problemDetails.Status = 400;
                    problemDetails.Type = "https://httpstatuses.com/400";

                    return new BadRequestObjectResult(problemDetails);

                case UnauthorizedAccessException _:
                    problemDetails.Status = 401;
                    problemDetails.Type = $"https://httpstatuses.com/401";

                    return new UnauthorizedObjectResult(problemDetails);

                case InvalidOperationException _:
                    problemDetails.Status = 500;
                    problemDetails.Type = $"https://httpstatuses.com/500";

                    return new ObjectResult(problemDetails)
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };

                case HttpRequestException _:
                    problemDetails.Status = 500;
                    problemDetails.Type = $"https://httpstatuses.com/500";

                    if (problemDetails.Title.ToLower().Contains("Operation timed out"))
                    {
                        problemDetails.Status = 408;
                        problemDetails.Type = $"https://httpstatuses.com/408";
                        problemDetails.Title = "There is a connection problem, please try again later..";

                        return new ObjectResult(problemDetails)
                        {
                            StatusCode = StatusCodes.Status408RequestTimeout
                        };
                    }

                    return new ObjectResult(problemDetails)
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
            }

            throw new Exception("Unexpected error", exception);
        }
    }
}
