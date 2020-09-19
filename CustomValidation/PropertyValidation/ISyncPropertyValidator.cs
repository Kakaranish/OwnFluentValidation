using CustomValidation.Types;

namespace CustomValidation.PropertyValidation
{
    public interface ISyncPropertyValidator
    {
        PropertyValidationResult Validate(object obj);
    }
}