using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CustomValidation.Types;

namespace CustomValidation
{
    public abstract class BaseValidator<TObject> : IValidator<TObject>
    {
        private readonly IList<IPropertyValidator> _propertyRuleBuilders = new List<IPropertyValidator>();
        protected BaseValidator()
        {
            SetupRules();
        }

        protected abstract void SetupRules();

        public ValidationResult Validate(TObject objToValidate)
        {
            var propertyValidationErrors = _propertyRuleBuilders.Select(propertyRuleBuilder =>
                propertyRuleBuilder.Validate(objToValidate));

            return new ValidationResult(propertyValidationErrors);
        }

        protected PropertyValidationBuilder<TObject, TProp> RuleFor<TProp>(Expression<Func<TObject, TProp>> expression)
        {
            if (!(expression.Body is MemberExpression memberExpression))
            {
                memberExpression = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            if (memberExpression == null)
            {
                throw new InvalidOperationException(nameof(memberExpression));
            }

            var propertyRuleBuilder = new PropertyValidationBuilder<TObject, TProp>(memberExpression);
            _propertyRuleBuilders.Add(propertyRuleBuilder);

            return propertyRuleBuilder;
        }
    }
}
