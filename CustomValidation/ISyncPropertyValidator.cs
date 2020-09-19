using CustomValidation.Types;

namespace CustomValidation
{
    public interface ISyncPropertyValidator
    {
        PropertyValidationResult Validate(object obj);
    }
}