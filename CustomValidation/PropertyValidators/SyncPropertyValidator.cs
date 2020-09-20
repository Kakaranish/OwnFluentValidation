using CustomValidation.Rules;
using CustomValidation.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CustomValidation.PropertyValidators
{
    public class SyncPropertyValidator<TObject, TProperty> : PropertyValidatorBase, ISyncPropertyValidator
    {
        public SyncPropertyValidator(PropertyInfo property) : base(property)
        {
        }

        public PropertyValidationResult Validate(object obj)
        {
            return Validate((TObject)obj);
        }

        private PropertyValidationResult Validate(TObject obj)
        {
            var propertyValueAsObj = Property.GetValue(obj);
            var propertyValue = (TProperty)Convert.ChangeType(propertyValueAsObj, typeof(TProperty));

            var ruleValidationErrors = new List<RuleValidationError>();
            foreach (var rule in Rules.Cast<SyncValidationRule<TProperty>>())
            {
                var ruleValidationError = rule.Validate(propertyValue);

                if (ruleValidationError != null)
                {
                    ruleValidationErrors.Add(ruleValidationError);

                    if (rule.StopValidationAfterFailure)
                    {
                        break;
                    }
                }
            }

            return new PropertyValidationResult(PropertyDisplayName, ruleValidationErrors);
        }
    }
}
