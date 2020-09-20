using System;
using CustomValidation.Types;

namespace CustomValidation.Rules
{
    public class SyncValidationRule<TProperty> : ValidationRuleBase, ISyncValidationRule
    {
        private readonly Predicate<TProperty> _validationPredicate;

        public SyncValidationRule(Predicate<TProperty> validationPredicate, string errorMessage, string errorCode = null)
            : base(errorMessage, errorCode)
        {
            _validationPredicate = validationPredicate ?? throw new ArgumentNullException(nameof(validationPredicate));
        }

        public RuleValidationError Validate(object propertyValueObj)
        {
            var propertyValue = (TProperty) propertyValueObj;

            return _validationPredicate(propertyValue) 
                ? null 
                : new RuleValidationError(ErrorMessage, ErrorCode);
        }
    }
}