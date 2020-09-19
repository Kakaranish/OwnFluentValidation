using System;
using CustomValidation.Types;
using System.Linq;
using System.Linq.Expressions;

namespace CustomValidation
{
    public abstract class SyncValidator<TObject> : ValidatorBase<TObject>, ISyncValidator<TObject>
    {
        public ValidationResult Validate(TObject objToValidate)
        {
            var casted = PropertyValidationBuilders.Cast<ISyncPropertyValidator>();
            
            var propertyValidationErrors = casted.Select(propertyRuleBuilder =>
                propertyRuleBuilder.Validate(objToValidate));

            return new ValidationResult(propertyValidationErrors);
        }

        protected SyncPropertyValidationBuilder<TObject, TProp> RuleFor<TProp>(Expression<Func<TObject, TProp>> expression)
        {
            if (!(expression.Body is MemberExpression memberExpression))
            {
                memberExpression = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            if (memberExpression == null)
            {
                throw new InvalidOperationException(nameof(memberExpression));
            }

            var propertyRuleBuilder = new SyncPropertyValidationBuilder<TObject, TProp>(memberExpression);
            PropertyValidationBuilders.Add(propertyRuleBuilder);
            
            return propertyRuleBuilder;
        }
    }
}
