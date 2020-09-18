using System;
using System.Linq.Expressions;
using CustomValidation.Types;

namespace CustomValidation
{
    public class ValidationRule<TProperty>
    {
        private readonly Expression<Predicate<TProperty>> _validationPredicate;
        private readonly string _errorMessage;
        private readonly string _errorCode;
        
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
    }
}