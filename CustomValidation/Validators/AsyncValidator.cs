using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CustomValidation.PropertyValidationBuilders;
using CustomValidation.PropertyValidators;
using CustomValidation.Types;

namespace CustomValidation.Validators
{
    public abstract class AsyncValidator<TObject> : ValidatorBase<TObject>, IAsyncValidator<TObject>
    {
        public async Task<ValidationResult> Validate(TObject objToValidate)
        {
            var propertyValidators = InnerPropertyValidators.Cast<IAsyncPropertyValidator>();

            var propertyValidationErrors = new List<PropertyValidationResult>();
            foreach (var asyncPropertyValidator in propertyValidators)
            {
                var propertyValidationResult = await asyncPropertyValidator.Validate(objToValidate);
                propertyValidationErrors.Add(propertyValidationResult);
            }

            return new ValidationResult(propertyValidationErrors);
        }

        protected AsyncPropertyValidationBuilder<TObject, TProp> RuleFor<TProp>(Expression<Func<TObject, TProp>> expression)
        {
            var property = ExtractProperty(expression);
            var propertyValidator = new AsyncPropertyValidator<TObject, TProp>(property);
            InnerPropertyValidators.Add(propertyValidator);

            return new AsyncPropertyValidationBuilder<TObject, TProp>(propertyValidator);
        }
    }
}
