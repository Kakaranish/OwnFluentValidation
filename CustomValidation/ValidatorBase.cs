using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CustomValidation
{
    public abstract class ValidatorBase<TObject>
    {
        protected readonly IList<IPropertyValidationBuilderMarker> PropertyValidationBuilders = 
            new List<IPropertyValidationBuilderMarker>();

        protected ValidatorBase()
        {
            SetupRules();
        }

        protected abstract void SetupRules();

        protected abstract PropertyValidationBuilderBase<TObject, TProp> GetPropertyValidationBuilder<TProp>(MemberExpression memberExpression);

        protected PropertyValidationBuilderBase<TObject, TProp> RuleFor<TProp>(Expression<Func<TObject, TProp>> expression)
        {
            if (!(expression.Body is MemberExpression memberExpression))
            {
                memberExpression = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            if (memberExpression == null)
            {
                throw new InvalidOperationException(nameof(memberExpression));
            }

            var propertyRuleBuilder = GetPropertyValidationBuilder<TProp>(memberExpression);
            PropertyValidationBuilders.Add(propertyRuleBuilder);

            return propertyRuleBuilder;
        }
    }
}