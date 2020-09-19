using CustomValidation.Types;

namespace CustomValidation
{
    internal interface IPropertyValidator
    {
        PropertyValidationResult Validate(object obj);
    }
}