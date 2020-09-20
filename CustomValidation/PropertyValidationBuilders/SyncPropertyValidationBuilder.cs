using CustomValidation.PropertyValidators;

namespace CustomValidation.PropertyValidationBuilders
{
    public class SyncPropertyValidationBuilder<TObject, TProperty> : PropertyValidationBuilderBase<TObject, TProperty>
    {
        internal SyncPropertyValidationBuilder(PropertyValidatorBase propertyValidator) : base(propertyValidator)
        {
        }
    }
}
