using System;
using System.Linq;
using System.Linq.Expressions;
using CustomValidation.PropertyValidation;
using CustomValidation.Types;

namespace CustomValidation.Validators
{
    public abstract class SyncValidator<TObject> : ValidatorBase<TObject>, ISyncValidator<TObject>
    {
        public ValidationResult Validate(TObject objToValidate)
        {
            var propertyValidators = PropertyValidationBuilders.Cast<ISyncPropertyValidator>();
            
            var propertyValidationErrors = propertyValidators.Select(propertyRuleBuilder =>
                propertyRuleBuilder.Validate(objToValidate));

            return new ValidationResult(propertyValidationErrors);
        }

        protected SyncPropertyValidationBuilder<TObject, TProp> RuleFor<TProp>(Expression<Func<TObject, TProp>> expression)
        {
            var memberExpression = ExtractMemberExpression(expression);

            var propertyRuleBuilder = new SyncPropertyValidationBuilder<TObject, TProp>(memberExpression);
            PropertyValidationBuilders.Add(propertyRuleBuilder);
            
            return propertyRuleBuilder;
        }
    }
}
