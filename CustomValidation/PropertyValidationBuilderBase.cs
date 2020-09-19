using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CustomValidation
{
    // TODO: Ensure really needed
    public interface IPropertyValidationBuilderMarker
    {
    }

    public abstract class PropertyValidationBuilderBase<TObject, TProperty> : IPropertyValidationBuilderMarker
    {
        protected readonly IList<ValidationRuleBase> Rules = new List<ValidationRuleBase>();

        protected readonly PropertyInfo Property;
        protected string PropertyDisplayName;

        protected PropertyValidationBuilderBase(MemberExpression memberExpression)
        {
            if (memberExpression == null)
            {
                throw new ArgumentNullException(nameof(memberExpression));
            }

            var property = typeof(TObject).GetProperty(memberExpression.Member.Name);
            if (property == null)
            {
                throw new InvalidOperationException("Property from member expression is null");
            }
            Property = property;

            PropertyDisplayName = Property.Name;
        }

        public PropertyValidationBuilderBase<TObject, TProperty> AddRule(Predicate<TProperty> validationPredicate,
            string errorMessage, string errorCode = null)
        {
            var rule = new SyncValidationRule<TProperty>(validationPredicate, errorMessage, errorCode);
            Rules.Add(rule);

            return this;
        }

        public PropertyValidationBuilderBase<TObject, TProperty> SetPropertyDisplayName(string propertyDisplayName)
        {
            PropertyDisplayName = propertyDisplayName ?? throw new ArgumentNullException(nameof(propertyDisplayName));

            return this;
        }

        public PropertyValidationBuilderBase<TObject, TProperty> StopValidationAfterFailure()
        {
            var lastRule = Rules.LastOrDefault();
            if (lastRule != null)
            {
                lastRule.StopValidationAfterFailure = true;
            }

            return this;
        }

        public PropertyValidationBuilderBase<TObject, TProperty> WithMessage(string message)
        {
            Rules.LastOrDefault()?.OverrideErrorMessage(message);

            return this;
        }

        public PropertyValidationBuilderBase<TObject, TProperty> WithCode(string code)
        {
            Rules.LastOrDefault()?.OverrideErrorCode(code);

            return this;
        }
    }
}