using CustomValidation.Types;

namespace CustomValidation.PropertyValidators
{
    public interface ISyncPropertyValidator
    {
        PropertyValidationResult Validate(object obj);
    }
}