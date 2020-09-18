using CustomValidation.Types;

namespace CustomValidation
{
    public interface IPropertyRuleBuilder
    {
        PropertyValidationResult Validate(object obj);
    }
}