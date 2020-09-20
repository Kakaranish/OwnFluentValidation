using System.Collections.Generic;
using System.Linq;

namespace CustomValidation.Types
{
    public class ValidationResult
    {
        public IList<PropertyValidationResult> PropertyValidationResults { get; }

        public bool Succeeded => PropertyValidationResults == null || !PropertyValidationResults.Any();

        public ValidationResult(IList<PropertyValidationResult> propertyValidationErrors)
        {
            PropertyValidationResults = propertyValidationErrors;
        }
    }
}
