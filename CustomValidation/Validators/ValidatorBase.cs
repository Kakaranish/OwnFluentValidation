using CustomValidation.PropertyValidators;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace CustomValidation.Validators
{
    public abstract class ValidatorBase<TObject>
    {
        protected readonly List<PropertyValidatorBase> InnerPropertyValidators = new List<PropertyValidatorBase>();

        protected ValidatorBase()
        {
            SetupRules();
        }

        public IReadOnlyList<PropertyValidatorBase> PropertyValidators => InnerPropertyValidators;

        protected abstract void SetupRules();

        public void UseValidator<TValidator>() where TValidator : ValidatorBase<TObject>
        {
            var otherValidator = Activator.CreateInstance<TValidator>();
            InnerPropertyValidators.AddRange(otherValidator.InnerPropertyValidators);
        }

        protected PropertyInfo ExtractProperty<TProp>(Expression<Func<TObject, TProp>> expression)
        {
            if (!(expression.Body is MemberExpression memberExpression))
            {
                memberExpression = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            if (memberExpression == null)
            {
                throw new InvalidOperationException(nameof(memberExpression));
            }

            var property = typeof(TObject).GetProperty(memberExpression.Member.Name);
            if (property == null)
            {
                throw new InvalidOperationException("Property from member expression is null");
            }

            return property;
        }
    }
}