using CustomValidation.Types;
using System;
using System.Threading.Tasks;

namespace CustomValidation
{
    public interface IAsyncValidationRule
    {
        Task<RuleValidationResult> Validate(object propertyValueObj);
    }

    public class AsyncValidationRule<TProperty> : ValidationRuleBase, IAsyncValidationRule
    {
        private readonly Func<TProperty, Task<bool>> _validationPredicate;

        public AsyncValidationRule(Func<TProperty, Task<bool>> validationPredicate, string errorMessage, string errorCode = null) 
            : base(errorMessage, errorCode)
        {
            _validationPredicate = validationPredicate ?? throw new ArgumentNullException(nameof(validationPredicate));
        }

        public async Task<RuleValidationResult> Validate(object propertyValueObj)
        {
            var propertyValue = (TProperty) propertyValueObj;

            var validationSucceeded = await _validationPredicate(propertyValue);
            var propertyValidationError = !validationSucceeded
                ? new RuleValidationError(ErrorMessage, ErrorCode)
                : null;

            return new RuleValidationResult { Error = propertyValidationError };
        }
    }
}
