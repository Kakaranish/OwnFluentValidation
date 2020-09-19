using CustomValidation.Types;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CustomValidation
{
    public abstract class AsyncValidator<TObject> : ValidatorBase<TObject>, IAsyncValidator<TObject>
    {
        protected sealed override PropertyValidationBuilderBase<TObject, TProp> GetPropertyValidationBuilder<TProp>(
            MemberExpression memberExpression)
        {
            return new AsyncPropertyValidationBuilder<TObject, TProp>(memberExpression);
        }

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
    }
}
