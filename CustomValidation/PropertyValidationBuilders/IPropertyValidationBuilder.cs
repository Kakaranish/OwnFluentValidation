using System;
using CustomValidation.PropertyValidators;

namespace CustomValidation.PropertyValidationBuilders
{
    public interface IPropertyValidationBuilder<out TBuilder, TProperty>
    {
        PropertyValidatorBase PropertyValidator { get; }
        TBuilder AddRule(Predicate<TProperty> validationPredicate, string errorMessage, string errorCode = null);
        TBuilder SetPropertyDisplayName(string propertyDisplayName);
        TBuilder StopValidationAfterFailure();
        TBuilder WithMessage(string message);
        TBuilder WithCode(string code);
    }
}