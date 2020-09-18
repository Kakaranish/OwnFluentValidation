using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomValidation
{
    public class ValidationResult
    {
        public IEnumerable<PropertyValidationError> PropertyValidationErrors { get; }

        public bool Success => !PropertyValidationErrors.Any();
        public bool Failure => !Success;

        public ValidationResult(IEnumerable<PropertyValidationError> propertyValidationErrors)
        {
            PropertyValidationErrors = propertyValidationErrors ??
                                       throw new ArgumentNullException(nameof(propertyValidationErrors));
        }
    }
}
