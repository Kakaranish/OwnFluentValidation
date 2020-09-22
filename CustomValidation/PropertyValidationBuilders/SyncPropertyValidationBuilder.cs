using CustomValidation.PropertyValidators;

namespace CustomValidation.PropertyValidationBuilders
{
    public class SyncPropertyValidationBuilder<TObject, TProperty> :
        PropertyValidationBuilderBase<SyncPropertyValidationBuilder<TObject, TProperty>, TProperty>
    {
        public SyncPropertyValidationBuilder(PropertyValidatorBase propertyValidator) :
            base(propertyValidator)
        {
        }

        public override SyncPropertyValidationBuilder<TObject, TProperty> Builder => this;
        public override PropertyValidatorBase PropertyValidator => BasePropertyValidator;
    }
}
