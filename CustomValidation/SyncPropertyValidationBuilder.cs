using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CustomValidation.Types;

namespace CustomValidation
{
    public class SyncPropertyValidationBuilder<TObject, TProperty> : PropertyValidationBuilderBase<TObject, TProperty>,
        IPropertyValidator
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
            var property = typeof(TObject).GetProperty(MemberExpression.Member.Name);
            if (property == null)
            {
                throw new InvalidOperationException("Property from member expression is null");
            }

            var propertyValueAsObj = property.GetValue(obj);
            var propertyValue = (TProperty)Convert.ChangeType(propertyValueAsObj, typeof(TProperty));

            var ruleValidationErrors = new List<RuleValidationError>();
            foreach (var rule in Rules)
            {
                var propValidationResult = rule.Validate(propertyValue);
                if (propValidationResult.Failure)
                {
                    ruleValidationErrors.Add(propValidationResult.RuleValidationError);

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
