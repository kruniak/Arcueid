using System.Diagnostics;
using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Arcueid.Server.Core;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public abstract class BaseController : ControllerBase
{
    protected IActionResult CreateResponse(OperationResult operationResult, int? responseStatusCode = null)
    {
        return operationResult.Success ? StatusCode(responseStatusCode.GetValueOrDefault(StatusCodes.Status204NoContent)) : Problem(FailureReasonToStatusCode(operationResult.FailureReason), null, operationResult.ErrorMessage, operationResult.ErrorDetail, operationResult.ValidationErrors);
    }

    protected IActionResult CreateResponse<T>(OperationResult<T?> operationResult, int? responseStatusCode = null)
        => CreateResponse(operationResult, null, null, responseStatusCode);

    private IActionResult CreateResponse<T>(OperationResult<T?> operationResult, string? actionName, object? routeValues = null, int? responseStatusCode = null)
    {
        if (!operationResult.Success)
        {
            return Problem(FailureReasonToStatusCode(operationResult.FailureReason), operationResult.Content,
                operationResult.ErrorMessage, operationResult.ErrorDetail, operationResult.ValidationErrors);
        }

        if (operationResult.Content == null)
        {
            return StatusCode(responseStatusCode.GetValueOrDefault(StatusCodes.Status204NoContent));
        }

        switch (string.IsNullOrWhiteSpace(actionName))
        {
            case false:
            {
                var routeValueDictionary = new RouteValueDictionary(routeValues);
                return CreatedAtRoute(actionName, routeValueDictionary, operationResult.Content);
            }
            default:
            {
                switch (operationResult.Content)
                {
                    case StreamFileContent streamFileContent:
                        return File(streamFileContent.Content, streamFileContent.ContentType, streamFileContent.DownloadFileName);
                    case ByteArrayFileContent byteArrayFileContent:
                        return File(byteArrayFileContent.Content, byteArrayFileContent.ContentType, byteArrayFileContent.DownloadFileName);
                }

                break;
            }
        }

        return Ok(operationResult.Content);
    }

    protected IActionResult Problem(IEnumerable<ValidationError>? validationErrors)
        => Problem(StatusCodes.Status400BadRequest, content: null, title: null, detail: null, validationErrors: validationErrors);

    protected IActionResult Problem(int statusCode, object? content, IEnumerable<ValidationError>? validationErrors = null)
        => Problem(statusCode, content, title: null, detail: null, validationErrors: validationErrors);

    protected IActionResult Problem(HttpStatusCode statusCode, object? content, IEnumerable<ValidationError>? validationErrors = null)
        => Problem((int)statusCode, content, title: null, detail: null, validationErrors: validationErrors);

    protected IActionResult Problem(int statusCode, string? title = null, string? detail = null, IEnumerable<ValidationError>? validationErrors = null)
        => Problem(statusCode, content: null, title, detail, validationErrors: validationErrors);

    protected IActionResult Problem(HttpStatusCode statusCode, string? title = null, string? detail = null, IEnumerable<ValidationError>? validationErrors = null)
        => Problem((int)statusCode, content: null, title, detail, validationErrors: validationErrors);

    protected IActionResult Problem(Exception error, int statusCode = StatusCodes.Status500InternalServerError, IEnumerable<ValidationError>? validationErrors = null)
        => Problem(statusCode, content: null, error.Message, error.InnerException?.Message, validationErrors: validationErrors);

    protected IActionResult Problem(Exception error, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, IEnumerable<ValidationError>? validationErrors = null)
        => Problem((int)statusCode, content: null, error.Message, error.InnerException?.Message, validationErrors: validationErrors);

    private IActionResult Problem(int statusCode, object? content = null, string? title = null, string? detail = null, IEnumerable<ValidationError>? validationErrors = null)
    {
        if (content != null)
        {
            return StatusCode(statusCode, content);
        }

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Type = $"https://httpstatuses.io/{statusCode}",
            Title = title ?? ReasonPhrases.GetReasonPhrase(statusCode),
            Detail = detail,
            Instance = HttpContext.Request.Path
        };

        problemDetails.Extensions.Add("traceId", Activity.Current?.Id ?? HttpContext.TraceIdentifier);
        if (validationErrors?.Any() ?? false)
        {
            problemDetails.Extensions.Add("errors", validationErrors);
        }

        return StatusCode(statusCode, problemDetails);
    }

    private static int FailureReasonToStatusCode(FailureReason failureReason, int? defaultResponseStatusCode = null)
        => failureReason switch
        {
            FailureReason.Unauthorized => StatusCodes.Status401Unauthorized,
            FailureReason.LockedOut => StatusCodes.Status403Forbidden,
            FailureReason.ItemNotFound => StatusCodes.Status404NotFound,
            FailureReason.ClientError => StatusCodes.Status400BadRequest,
            FailureReason.InvalidToken => StatusCodes.Status419AuthenticationTimeout,
            FailureReason.NotAllowed => StatusCodes.Status424FailedDependency,
            _ => defaultResponseStatusCode.GetValueOrDefault(StatusCodes.Status500InternalServerError)
        };
}
