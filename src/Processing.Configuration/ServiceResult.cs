using FluentValidation.Results;

namespace Processing.Configuration;

public record ServiceResult<TResult>
{
    public record Success(TResult Result) : ServiceResult<TResult>;
    public record ValidationError(IEnumerable<(string Key, string Error)> Errors) : ServiceResult<TResult>;
    public record InternalError(IEnumerable<Exception> Exceptions) : ServiceResult<TResult>;

    public static Success FromResult(TResult result) => new(result);
    public static ValidationError FromValidationResult(ValidationResult validationResult) => new(validationResult.Errors.Select(e => (e.PropertyName, e.ErrorCode)));
    public static InternalError FromException(Exception exception) => new(new[] { exception });
}