using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CustomValidation.Rules;
using CustomValidation.Types;

namespace CustomValidation.PropertyValidation
{
    public class SyncPropertyValidationBuilder<TObject, TProperty> : PropertyValidationBuilderBase<TObject, TProperty>,
        ISyncPropertyValidator
    {
        internal SyncPropertyValidationBuilder(MemberExpression memberExpression) : base(memberExpression)
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
                var propValidationResult = rule.Validate(propertyValue);
                if (propValidationResult.Failure)
                {
                    ruleValidationErrors.Add(propValidationResult.Error);

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
