using CustomValidation.PropertyValidators;

namespace CustomValidation.PropertyValidationBuilders
{
    public class SyncPropertyValidationBuilder<TObject, TProperty> :
        PropertyValidationBuilderBase<SyncPropertyValidationBuilder<TObject, TProperty>, TProperty>,
        IPropertyValidationBuilder<SyncPropertyValidationBuilder<TObject, TProperty>, TProperty>
    {
        public SyncPropertyValidationBuilder(PropertyValidatorBase propertyValidator) :
            base(propertyValidator)
        {
        }

        public SyncPropertyValidationBuilder<TObject, TProperty> Builder => this;
        public PropertyValidatorBase PropertyValidator => BasePropertyValidator;
    }
}
