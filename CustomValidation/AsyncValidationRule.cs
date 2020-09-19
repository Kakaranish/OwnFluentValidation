using CustomValidation.Types;
using System;
using System.Threading.Tasks;

namespace CustomValidation
{
    public interface IRuleValidatorAsync<in TProperty>
    {
        Task<RuleValidationResult> ValidateAsync(TProperty propertyValue);
    }

    public class AsyncValidationRule<TProperty> : IRuleValidatorAsync<TProperty>
    {
        private readonly Func<TProperty, Task<bool>> _validationPredicate;
        private string _errorMessage;
        private string _errorCode;

        public bool StopValidationAfterFailure { get; set; } = false;

        public AsyncValidationRule(Func<TProperty, Task<bool>> validationPredicate, string errorMessage, string errorCode = null)
        {
            _validationPredicate = validationPredicate ?? throw new ArgumentNullException(nameof(validationPredicate));
            _errorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
            _errorCode = errorCode;
        }

        public async Task<RuleValidationResult> ValidateAsync(TProperty propertyValue)
        {
            var validationSucceeded = await _validationPredicate(propertyValue);
            var propertyValidationError = !validationSucceeded
                ? new RuleValidationError(_errorMessage, _errorCode)
                : null;

            return new RuleValidationResult { RuleValidationError = propertyValidationError };
        }

        public void OverrideErrorMessage(string errorMessage)
        {
            _errorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
        }
        public void OverrideErrorCode(string errorCode)
        {
            _errorCode = errorCode;
        }
    }
}
