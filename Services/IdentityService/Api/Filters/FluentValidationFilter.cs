// using FluentValidation;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Filters;
//
// public class FluentValidationExceptionFilter : IExceptionFilter
// {
//     public void OnException(ExceptionContext context)
//     {
//         // Check if the exception is a FluentValidation exception
//         if (context.Exception is ValidationException validationException)
//         {
//             // Map the validation errors into a dictionary
//             var errors = validationException.Errors
//                 .GroupBy(e => e.PropertyName)
//                 .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
//
//             // Create the custom response
//             var response = new
//             {
//                 type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
//                 title = "One or more validation errors occurred.",
//                 status = 400,
//                 errors = errors,
//                 traceId = context.HttpContext.TraceIdentifier
//             };
//
//             // Set the response status code and return the formatted response
//             context.Result = new ObjectResult(response)
//             {
//                 StatusCode = 400 // Bad Request
//             };
//
//             // Mark the exception as handled
//             context.ExceptionHandled = true;
//         }
//     }
// }