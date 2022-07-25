
using FluentValidation.Results;

namespace Processing.Configuration;

public class ResponseOfT<TResponse>
{
    internal ResponseOfT(TResponse? response, IEnumerable<(string, string)>? errors)
    {
        Response = response;
        Errors = errors?.ToList().AsReadOnly() ?? new List<(string, string)>().AsReadOnly();
    }

    public IReadOnlyCollection<(string Name, string Error)> Errors { get; private set; }
    
    public TResponse? Response { get; private set; }

    public bool HasErrors()
    {
        return Errors?.Any() ?? false;
    }

    public static ResponseOfT<TResponse> FromResponse(TResponse response)
    {
        return new(response, null);
    }

    public static ResponseOfT<TResponse> FromErrors(IEnumerable<(string Name, string Error)> errors)
    {
        return new(default, errors);
    }

    public static ResponseOfT<TResponse> FromValidationResult(ValidationResult validationResult)
    {
        return new(default, validationResult.Errors.Select(e => (e.PropertyName, e.ErrorCode)));
    }
}