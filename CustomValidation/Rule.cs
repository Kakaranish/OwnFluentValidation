using System;
using System.Linq.Expressions;

namespace CustomValidation
{
    public class Rule<TProperty>
    {
        private readonly Expression<Predicate<TProperty>> _validationPredicate;
        private readonly string _errorMessage;
        private readonly string _errorCode;

        public Rule(Expression<Predicate<TProperty>> validationPredicate, string errorMessage, string errorCode = null)
        {
            _validationPredicate = validationPredicate ?? throw new ArgumentNullException(nameof(validationPredicate));
            _errorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
            _errorCode = errorCode;
        }

        public PropertyValidationResult Validate(TProperty propertyValue)
        {
            var validateFunc = _validationPredicate.Compile();
            var propertyValidationError = !validateFunc(propertyValue)
                ? new PropertyValidationError(_errorMessage, _errorCode)
                : null;

            return new PropertyValidationResult {PropertyValidationError = propertyValidationError};
        }
    }
}