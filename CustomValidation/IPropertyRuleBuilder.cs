using System.Collections.Generic;

namespace CustomValidation
{
    public interface IPropertyRuleBuilder
    {
        IEnumerable<PropertyValidationError> Validate(object obj);
    }
}