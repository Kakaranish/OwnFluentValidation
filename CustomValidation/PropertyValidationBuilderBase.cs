using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CustomValidation
{
    public interface IPropertyValidationBuilderMarker
    {
    }

    public abstract class PropertyValidationBuilderBase<TObject, TProperty> : IPropertyValidationBuilderMarker
    {
        protected readonly IList<ValidationRule<TProperty>> Rules = new List<ValidationRule<TProperty>>();

        protected readonly MemberExpression MemberExpression;
        protected string PropertyDisplayName;

        protected PropertyValidationBuilderBase(MemberExpression memberExpression)
        {
            MemberExpression = memberExpression ?? throw new ArgumentNullException(nameof(memberExpression));
            PropertyDisplayName = MemberExpression.Member.Name;
        }

        public PropertyValidationBuilderBase<TObject, TProperty> AddRule(Expression<Predicate<TProperty>> validationPredicate,
            string errorMessage, string errorCode = null)
        {
            var rule = new ValidationRule<TProperty>(validationPredicate, errorMessage, errorCode);
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