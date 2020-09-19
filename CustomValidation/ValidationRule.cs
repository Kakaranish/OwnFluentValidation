using System;
using System.Linq.Expressions;
using CustomValidation.Types;

namespace CustomValidation
{
    internal class ValidationRule<TProperty>
    {
        private readonly Expression<Predicate<TProperty>> _validationPredicate;
        private string _errorMessage;
        private string _errorCode;

        public bool StopValidationAfterFailure { get; set; } = false;

        public ValidationRule(Expression<Predicate<TProperty>> validationPredicate, string errorMessage, string errorCode = null)
        {
            _validationPredicate = validationPredicate ?? throw new ArgumentNullException(nameof(validationPredicate));
            _errorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
            _errorCode = errorCode;
        }

        public RuleValidationResult Validate(TProperty propertyValue)
        {
            var validateFunc = _validationPredicate.Compile();
            var propertyValidationError = !validateFunc(propertyValue)
                ? new RuleValidationError(_errorMessage, _errorCode)
                : null;

            return new RuleValidationResult {RuleValidationError = propertyValidationError};
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