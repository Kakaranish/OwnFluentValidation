using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CustomValidation
{
    public class PropertyRuleBuilder<TObject, TProperty> : IPropertyRuleBuilder
    {
        private readonly IList<Rule<TProperty>> _rules = new List<Rule<TProperty>>();
        private readonly MemberExpression _memberExpression;

        public PropertyRuleBuilder(MemberExpression memberExpression)
        {
            _memberExpression = memberExpression ?? throw new ArgumentNullException(nameof(memberExpression));
        }

        public PropertyRuleBuilder<TObject, TProperty> AddRule(Expression<Predicate<TProperty>> validationPredicate,
            string errorMessage)
        {
            var rule = new Rule<TProperty>(validationPredicate, errorMessage);
            _rules.Add(rule);

            return this;
        }

        public IEnumerable<PropertyValidationError> Validate(TObject obj)
        {
            var property = typeof(TObject).GetProperty(_memberExpression.Member.Name);
            if (property == null)
            {
                throw new InvalidOperationException("Property from member expression is null");
            }

            var propertyValueAsObj = property.GetValue(obj);
            var propertyValue = (TProperty) Convert.ChangeType(propertyValueAsObj, typeof(TProperty));

            var validationErrors = new List<PropertyValidationError>();
            foreach (var rule in _rules)
            {
                var propValidationResult = rule.Validate(propertyValue);
                if (propValidationResult.Failure)
                {
                    validationErrors.Add(propValidationResult.PropertyValidationError);
                }
            }

            return validationErrors;
        }

        public IEnumerable<PropertyValidationError> Validate(object obj)
        {
            return Validate((TObject) obj);
        }
    }
}
