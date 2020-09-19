using CustomValidation.Types;

namespace CustomValidation.Rules
{
    public interface ISyncValidationRule
    {
        RuleValidationResult Validate(object propertyValueObj);
    }
}