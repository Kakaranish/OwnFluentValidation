using CustomValidation.Types;
using System;
using System.Threading.Tasks;

namespace CustomValidation.Rules
{
    public class AsyncValidationRule<TProperty> : ValidationRuleBase, IAsyncValidationRule
    {
        private readonly Func<TProperty, Task<bool>> _validationPredicate;

        public AsyncValidationRule(Func<TProperty, Task<bool>> validationPredicate, string errorMessage, string errorCode = null)
            : base(errorMessage, errorCode)
        {
            _validationPredicate = validationPredicate ?? throw new ArgumentNullException(nameof(validationPredicate));
        }

        public async Task<RuleValidationError> Validate(object propertyValueObj)
        {
            var propertyValue = (TProperty)propertyValueObj;
            var validationSucceeded = await _validationPredicate(propertyValue);

            return validationSucceeded 
                ? null 
                : new RuleValidationError(ErrorMessage, ErrorCode);
        }
    }
}
