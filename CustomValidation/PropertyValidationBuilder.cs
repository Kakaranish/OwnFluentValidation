using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CustomValidation.Types;

namespace CustomValidation
{
    public class PropertyValidationBuilder<TObject, TProperty> : IPropertyRuleBuilder
    {
        private readonly IList<ValidationRule<TProperty>> _rules = new List<ValidationRule<TProperty>>();
        private readonly MemberExpression _memberExpression;
        private string _propertyDisplayName;

        public PropertyValidationBuilder(MemberExpression memberExpression)
        {
            _memberExpression = memberExpression ?? throw new ArgumentNullException(nameof(memberExpression));
            _propertyDisplayName = _memberExpression.Member.Name;
        }

        public PropertyValidationBuilder<TObject, TProperty> AddRule(Expression<Predicate<TProperty>> validationPredicate,
            string errorMessage)
        {
            var rule = new ValidationRule<TProperty>(validationPredicate, errorMessage);
            _rules.Add(rule);

            return this;
        }

        public PropertyValidationBuilder<TObject, TProperty> StopValidationAfterFailure()
        {
            if (_rules.Any())
            {
                _rules.Last().StopValidationAfterFailure = true;
            }

            return this;
        }

        public PropertyValidationBuilder<TObject, TProperty> SetPropertyDisplayName(string propertyDisplayName)
        {
            _propertyDisplayName = propertyDisplayName ?? throw new ArgumentNullException(nameof(propertyDisplayName));

            return this;
        }

        public PropertyValidationResult Validate(TObject obj)
        {
            var property = typeof(TObject).GetProperty(_memberExpression.Member.Name);
            if (property == null)
            {
                throw new InvalidOperationException("Property from member expression is null");
            }

            var propertyValueAsObj = property.GetValue(obj);
            var propertyValue = (TProperty) Convert.ChangeType(propertyValueAsObj, typeof(TProperty));

            var ruleValidationErrors = new List<RuleValidationError>();
            foreach (var rule in _rules)
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

            return new PropertyValidationResult(_propertyDisplayName, ruleValidationErrors);
        }

        public PropertyValidationResult Validate(object obj)
        {
            return Validate((TObject) obj);
        }
    }
}
