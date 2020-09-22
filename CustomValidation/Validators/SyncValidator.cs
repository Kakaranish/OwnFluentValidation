using System;
using System.Linq;
using System.Linq.Expressions;
using CustomValidation.PropertyValidationBuilders;
using CustomValidation.PropertyValidators;
using CustomValidation.Types;

namespace CustomValidation.Validators
{
    public abstract class SyncValidator<TObject> : ValidatorBase<TObject>, ISyncValidator<TObject>
    {
        public ValidationResult Validate(TObject objToValidate)
        {
            var propertyValidators = InnerPropertyValidators.Cast<ISyncPropertyValidator>();
            
            var propertyValidationErrors = propertyValidators.Select(propertyValidator => 
                propertyValidator.Validate(objToValidate)).ToList();

            return new ValidationResult(propertyValidationErrors);
        }

        protected SyncPropertyValidationBuilder<TObject, TProp> RuleFor<TProp>(Expression<Func<TObject, TProp>> expression)
        {
            var property = ExtractProperty(expression);
            var propertyValidator = new SyncPropertyValidator<TObject, TProp>(property);
            InnerPropertyValidators.Add(propertyValidator);

            return new SyncPropertyValidationBuilder<TObject, TProp>(propertyValidator);
        }
    }
}
