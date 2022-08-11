namespace Arcueid.Server.Core;

public class OperationResult
{
    public bool Success { get; }
    public FailureReason FailureReason { get; }
    public Exception? Error { get; }
    private readonly string? _errorMessage;
    public string? ErrorMessage => _errorMessage ?? Error?.Message;
    private readonly string? _errorDetail;
    public string? ErrorDetail => _errorDetail ?? Error?.InnerException?.Message;
    public IEnumerable<ValidationError>? ValidationErrors { get; }

    private OperationResult(bool success = true, FailureReason failureReason = FailureReason.None, string? message = null, string? detail = null, Exception? error = null, IEnumerable<ValidationError>? validationErrors = null)
    {
        Success = success;
        FailureReason = failureReason;
        _errorMessage = message;
        _errorDetail = detail;
        Error = error;
        ValidationErrors = validationErrors;
    }

    private static OperationResult Ok()
        => new(success: true);

    public static OperationResult Fail(FailureReason failureReason, ValidationError validationError)
        => new(false, failureReason: failureReason, validationErrors: new ValidationError[1] { validationError });

    public static OperationResult Fail(FailureReason failureReason, string? message, ValidationError validationError)
        => new(false, failureReason: failureReason, message: message, validationErrors: new ValidationError[1] { validationError });

    public static OperationResult Fail(FailureReason failureReason, string? message, string? detail, ValidationError validationError)
        => new(false, failureReason: failureReason, message: message, detail: detail, validationErrors: new ValidationError[1] { validationError });

    public static OperationResult Fail(FailureReason failureReason, Exception? error, ValidationError validationError)
        => new(false, failureReason: failureReason, error: error, validationErrors: new ValidationError[1] { validationError });

    public static OperationResult Fail(FailureReason failureReason, IEnumerable<ValidationError>? validationErrors = null)
        => new(false, failureReason: failureReason, validationErrors: validationErrors);

    public static OperationResult Fail(FailureReason failureReason, string? message, IEnumerable<ValidationError>? validationErrors = null)
        => new(false, failureReason: failureReason, message: message, validationErrors: validationErrors);

    public static OperationResult Fail(FailureReason failureReason, string? message, string? detail, IEnumerable<ValidationError>? validationErrors = null)
        => new(false, failureReason: failureReason, message: message, detail: detail, validationErrors: validationErrors);

    public static OperationResult Fail(FailureReason failureReason, Exception? error, IEnumerable<ValidationError>? validationErrors = null)
        => new(false, failureReason: failureReason, error: error, validationErrors: validationErrors);
}
