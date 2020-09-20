using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomValidation.Types
{
    public class PropertyValidationResult
    {
        public string PropertyName { get; set; }
        public IList<RuleValidationError> RuleValidationErrors { get; set; }

        public PropertyValidationResult(string propertyName, IList<RuleValidationError> ruleValidationErrors)
        {
            PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            RuleValidationErrors = ruleValidationErrors;
        }

        public bool Success => RuleValidationErrors == null || !RuleValidationErrors.Any();
        public bool Failure => !Success;
    }
}
