using System;
using CustomValidation.PropertyValidators;

namespace CustomValidation.PropertyValidationBuilders
{
    public class SyncPropertyValidationBuilder<TObject, TProperty> :
        IPropertyValidationBuilder<SyncPropertyValidationBuilder<TObject, TProperty>, TProperty>
    {
        private readonly IPropertyValidationBuilder<PropertyValidationBuilderBase<TObject, TProperty>, TProperty> _baseValidationBuilder;

        internal SyncPropertyValidationBuilder(
            IPropertyValidationBuilder<PropertyValidationBuilderBase<TObject, TProperty>, TProperty> baseValidationBuilder)
        {
            _baseValidationBuilder = baseValidationBuilder ?? throw new ArgumentNullException(nameof(baseValidationBuilder));
        }

        public PropertyValidatorBase PropertyValidator => _baseValidationBuilder.PropertyValidator;

        public SyncPropertyValidationBuilder<TObject, TProperty> AddRule(Predicate<TProperty> validationPredicate, 
            string errorMessage, string errorCode = null)
        {
            _baseValidationBuilder.AddRule(validationPredicate, errorMessage, errorCode);
            return this;
        }

        public SyncPropertyValidationBuilder<TObject, TProperty> SetPropertyDisplayName(string propertyDisplayName)
        {
            _baseValidationBuilder.SetPropertyDisplayName(propertyDisplayName);
            return this;
        }

        public SyncPropertyValidationBuilder<TObject, TProperty> StopValidationAfterFailure()
        {
            _baseValidationBuilder.StopValidationAfterFailure();
            return this;
        }

        public SyncPropertyValidationBuilder<TObject, TProperty> WithMessage(string message)
        {
            _baseValidationBuilder.WithMessage(message);
            return this;
        }

        public SyncPropertyValidationBuilder<TObject, TProperty> WithCode(string code)
        {
            _baseValidationBuilder.WithCode(code);
            return this;
        }
    }
}
