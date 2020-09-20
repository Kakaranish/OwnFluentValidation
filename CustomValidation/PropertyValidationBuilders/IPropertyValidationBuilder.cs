using CustomValidation.PropertyValidators;

namespace CustomValidation.PropertyValidationBuilders
{
    public interface IPropertyValidationBuilder<out TBuilder, TProperty>
    {
        TBuilder Builder { get; }
        PropertyValidatorBase PropertyValidator { get; }
    }
}