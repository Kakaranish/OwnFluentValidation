using System.Collections.Generic;
using System.Linq;

namespace CustomValidation.Types
{
    public class ValidationResult
    {
        public IEnumerable<PropertyValidationResult> PropertyValidationResults { get; }

        public bool Success => PropertyValidationResults == null || !PropertyValidationResults.Any();
        public bool Failure => !Success;

        public ValidationResult(IEnumerable<PropertyValidationResult> propertyValidationErrors)
        {
            PropertyValidationResults = propertyValidationErrors;
        }
    }
}
