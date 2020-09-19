using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CustomValidation.PropertyValidation;

namespace CustomValidation.Validators
{
    public abstract class ValidatorBase<TObject>
    {
        protected readonly IList<IPropertyValidationBuilder> PropertyValidationBuilders = 
            new List<IPropertyValidationBuilder>();

        protected ValidatorBase()
        {
            SetupRules();
        }

        protected abstract void SetupRules();

        protected MemberExpression ExtractMemberExpression<TProp>(Expression<Func<TObject, TProp>> expression)
        {
            if (!(expression.Body is MemberExpression memberExpression))
            {
                memberExpression = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            if (memberExpression == null)
            {
                throw new InvalidOperationException(nameof(memberExpression));
            }

            return memberExpression;
        }
    }
}