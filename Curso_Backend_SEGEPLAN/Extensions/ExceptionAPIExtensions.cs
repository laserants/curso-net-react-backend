using Microsoft.AspNetCore.Mvc;

namespace Curso_Backend_SEGEPLAN.Extensions
{
    public static class ExceptionAPIExtensions
    {
        public static ActionResult ConvertToActionResult(this Exception exception, HttpContext context)
        {
            var problemsDetails = new ProblemDetails()
            {
                Detail = exception.ToString(),
                Instance = context?.Request.Path,
                Title = exception.Message
            };

            switch (exception)
            {
                case ArgumentException _:
                    problemsDetails.Status = 400;
                    problemsDetails.Type = "https://httpstatuses.com/400";

                    return new BadRequestObjectResult(problemsDetails);

                case UnauthorizedAccessException _:
                    problemsDetails.Status = 401;
                    problemsDetails.Type = "https://httpstatuses.com/401";

                    return new UnauthorizedObjectResult(problemsDetails);

                case HttpRequestException _:
                    problemsDetails.Status = 500;
                    problemsDetails.Type = "https://httpstatuses.com/500";

                    if (problemsDetails.Title.ToLower().Contains("Operation timed out"))
                    {
                        problemsDetails.Status = 408;
                        problemsDetails.Type = "https://httpstatuses.com/408";
                        problemsDetails.Title = "Hay un problema con su conexión, porfavor intente más tarde..";

                        return new ObjectResult(problemsDetails)
                        {
                            StatusCode = StatusCodes.Status408RequestTimeout
                        };
                    }

                    return new ObjectResult(problemsDetails)
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
            }

            throw new Exception("Error desconocido", exception);
        }
    }
}
