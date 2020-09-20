using CustomValidation.Types;

namespace CustomValidation.Rules
{
    public interface ISyncValidationRule
    {
        RuleValidationError Validate(object propertyValueObj);
    }
}