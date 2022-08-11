namespace Arcueid.Server.Core;
public class OperationResult<T>
{
    public bool Success { get; }

    public T Content { get; }

    public FailureReason FailureReason { get; }

    public Exception? Error { get; }

    private readonly string? _errorMessage;
    public string? ErrorMessage => _errorMessage ?? Error?.Message;

    private readonly string? _errorDetail;
    public string? ErrorDetail => _errorDetail ?? Error?.InnerException?.Message;

    public IEnumerable<ValidationError>? ValidationErrors { get; }

    private OperationResult(bool success = true, T content = default!, FailureReason failureReason = FailureReason.None, string? message = null, string? detail = null, Exception? error = null, IEnumerable<ValidationError>? validationErrors = null)
    {
        Success = success;
        Content = content;
        FailureReason = failureReason;
        _errorMessage = message;
        _errorDetail = detail;
        Error = error;
        ValidationErrors = validationErrors;
    }

    private static OperationResult<T> Ok(T content = default!)
        => new(success: true, content);

    public static OperationResult<T> Fail(FailureReason failureReason, ValidationError validationError)
        => new(false, failureReason: failureReason, validationErrors: new ValidationError[1] { validationError });

    public static OperationResult<T> Fail(FailureReason failureReason, string? message, ValidationError validationError)
        => new(false, failureReason: failureReason, message: message, validationErrors: new ValidationError[1] { validationError });

    public static OperationResult<T> Fail(FailureReason failureReason, string? message, string? detail, ValidationError validationError)
        => new(false, failureReason: failureReason, message: message, detail: detail, validationErrors: new ValidationError[1] { validationError });

    public static OperationResult<T?> Fail(FailureReason failureReason, T content, ValidationError validationError)
        => new(false, failureReason: failureReason, content: content, validationErrors: new ValidationError[1] { validationError });

    public static OperationResult<T> Fail(FailureReason failureReason, Exception? error, ValidationError validationError)
        => new(false, failureReason: failureReason, error: error, validationErrors: new ValidationError[1] { validationError });

    public static OperationResult<T> Fail(FailureReason failureReason, IEnumerable<ValidationError>? validationErrors = null)
        => new(false, failureReason: failureReason, validationErrors: validationErrors);

    public static OperationResult<T> Fail(FailureReason failureReason, string? message, IEnumerable<ValidationError>? validationErrors = null)
        => new(false, failureReason: failureReason, message: message, validationErrors: validationErrors);

    public static OperationResult<T> Fail(FailureReason failureReason, string? message, string? detail, IEnumerable<ValidationError>? validationErrors = null)
        => new(false, failureReason: failureReason, message: message, detail: detail, validationErrors: validationErrors);

    public static OperationResult<T> Fail(FailureReason failureReason, T content, IEnumerable<ValidationError>? validationErrors = null)
        => new(false, failureReason: failureReason, content: content, validationErrors: validationErrors);

    public static OperationResult<T> Fail(FailureReason failureReason, Exception? error, IEnumerable<ValidationError>? validationErrors = null)
        => new(false, failureReason: failureReason, error: error, validationErrors: validationErrors);

    public static implicit operator OperationResult<T>(T value)
        => Ok(value);

    public static implicit operator OperationResult<T?>(OperationResult result)
        => new(result.Success, default, result.FailureReason, result.ErrorMessage, result.ErrorDetail, result.Error, result.ValidationErrors);
}

