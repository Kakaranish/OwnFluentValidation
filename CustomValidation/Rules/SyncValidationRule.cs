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

        public RuleValidationResult Validate(object propertyValueObj)
        {
            var propertyValue = (TProperty) propertyValueObj;

            var propertyValidationError = !_validationPredicate(propertyValue)
                ? new RuleValidationError(ErrorMessage, ErrorCode)
                : null;

            return new RuleValidationResult { Error = propertyValidationError };
        }
    }
}