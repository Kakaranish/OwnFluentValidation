using CustomValidation.Types;

namespace CustomValidation
{
    public interface IPropertyValidator
    {
        PropertyValidationResult Validate(object obj);
    }
}