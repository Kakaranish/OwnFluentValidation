using CustomValidation.Types;
using System.Linq;
using System.Linq.Expressions;

namespace CustomValidation
{
    public abstract class SyncValidator<TObject> : ValidatorBase<TObject>, ISyncValidator<TObject>
    {
        protected sealed override PropertyValidationBuilderBase<TObject, TProp> GetPropertyValidationBuilder<TProp>(
            MemberExpression memberExpression)
        {
            return new SyncPropertyValidationBuilder<TObject, TProp>(memberExpression);
        }

        public ValidationResult Validate(TObject objToValidate)
        {
            var casted = PropertyValidationBuilders.Cast<IPropertyValidator>();
            
            var propertyValidationErrors = casted.Select(propertyRuleBuilder =>
                propertyRuleBuilder.Validate(objToValidate));

            return new ValidationResult(propertyValidationErrors);
        }
    }
}
