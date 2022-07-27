using FluentValidation;

namespace Processing.Configuration.Processors;

public class ProcessorServiceValidator : AbstractValidator<CkoService>
{
    private static readonly string[] SupportedTypes = { "3ds" };
    
    public ProcessorServiceValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty()
            .WithErrorCode(Errors.ProcessorServiceTypeRequired)
            .Must(SupportedTypes.Contains)
            .WithErrorCode(Errors.ProcessorServiceTypeInvalid);

        RuleFor(x => x.Key)
            .NotEmpty()
            .WithErrorCode(Errors.ProcessorServiceKeyRequired);

        RuleFor(x => x.Version)
            .Matches(@"^([0-9]+)\.([0-9]+)\.([0-9]+)(?:-([0-9A-Za-z-]+(?:\.[0-9A-Za-z-]+)*))?(?:\+[0-9A-Za-z-]+)?$")
            .WithErrorCode(Errors.ProcessorVersionSemVer);
    }

    public static class Errors
    {
        public static string ProcessorServiceTypeRequired = "processor_service_type_required";
        public static string ProcessorServiceTypeInvalid = "processor_service_type_invalid";
        public static string ProcessorServiceKeyRequired = "processor_service_key_required";
        public static string ProcessorVersionSemVer = "processor_service_version_must_be_semver";
    }
}