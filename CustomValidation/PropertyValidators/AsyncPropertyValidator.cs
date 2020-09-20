using CustomValidation.Rules;
using CustomValidation.Types;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace CustomValidation.PropertyValidators
{
    public class AsyncPropertyValidator<TObject, TProperty> : PropertyValidatorBase, IAsyncPropertyValidator
    {
        public AsyncPropertyValidator(PropertyInfo property) : base(property)
        {
        }

        public async Task<PropertyValidationResult> Validate(object obj)
        {
            return await Validate((TObject)obj);
        }

        public async Task<PropertyValidationResult> Validate(TObject obj)
        {
            var propertyValueAsObj = Property.GetValue(obj);
            var propertyValue = (TProperty)Convert.ChangeType(propertyValueAsObj, typeof(TProperty));

            var ruleValidationErrors = new List<RuleValidationError>();
            foreach (var rule in Rules)
            {
                RuleValidationError ruleValidationError;
                if (rule is ISyncValidationRule syncValidationRule)
                {
                    ruleValidationError = syncValidationRule.Validate(propertyValue);
                }
                else if (rule is IAsyncValidationRule asyncValidationRule)
                {
                    ruleValidationError = await asyncValidationRule.Validate(propertyValue);
                }
                else
                {
                    // TODO: Throwing exception here
                    throw new Exception("TODO:");
                }

                if (ruleValidationError != null)
                {
                    ruleValidationErrors.Add(ruleValidationError);
                }
            }

            return new PropertyValidationResult(PropertyDisplayName, ruleValidationErrors);
        }
    }
}
