using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CustomValidation.PropertyValidation;
using CustomValidation.Types;

namespace CustomValidation.Validators
{
    public abstract class AsyncValidator<TObject> : ValidatorBase<TObject>, IAsyncValidator<TObject>
    {
        public async Task<ValidationResult> Validate(TObject objToValidate)
        {
            var casted = PropertyValidationBuilders.Cast<IAsyncPropertyValidator>();

            var propertyValidationErrors = new List<PropertyValidationResult>();
            foreach (var asyncPropertyValidator in casted)
            {
                var propertyValidationResult = await asyncPropertyValidator.Validate(objToValidate);
                propertyValidationErrors.Add(propertyValidationResult);
            }

            return new ValidationResult(propertyValidationErrors);
        }

        protected AsyncPropertyValidationBuilder<TObject, TProp> RuleFor<TProp>(Expression<Func<TObject, TProp>> expression)
        {
            var memberExpression = ExtractMemberExpression(expression);

            var propertyRuleBuilder = new AsyncPropertyValidationBuilder<TObject, TProp>(memberExpression);
            PropertyValidationBuilders.Add(propertyRuleBuilder);

            return propertyRuleBuilder;
        }
    }
}
