using CustomValidation.PropertyValidators;
using CustomValidation.Rules;
using System;
using System.Linq;

namespace CustomValidation.PropertyValidationBuilders
{
    public class PropertyValidationBuilderBase<TObject, TProperty> : 
        IPropertyValidationBuilder<PropertyValidationBuilderBase<TObject, TProperty>, TProperty>
    {
        protected readonly PropertyValidatorBase _propertyValidator;

        public  PropertyValidationBuilderBase(PropertyValidatorBase propertyValidator)
        {
            _propertyValidator = propertyValidator ?? throw new ArgumentNullException(nameof(propertyValidator));
        }

        public PropertyValidatorBase PropertyValidator => _propertyValidator;

        public PropertyValidationBuilderBase<TObject, TProperty> AddRule(Predicate<TProperty> validationPredicate, string errorMessage,
            string errorCode = null)
        {
            var rule = new SyncValidationRule<TProperty>(validationPredicate, errorMessage, errorCode);
            _propertyValidator.Rules.Add(rule);

            return this;
        }

        public PropertyValidationBuilderBase<TObject, TProperty> SetPropertyDisplayName(string propertyDisplayName)
        {
            _propertyValidator.SetPropertyDisplayName(propertyDisplayName);

            return this;
        }

        public PropertyValidationBuilderBase<TObject, TProperty> StopValidationAfterFailure()
        {
            var lastRule = _propertyValidator.Rules.LastOrDefault();
            if (lastRule != null)
            {
                lastRule.StopValidationAfterFailure = true;
            }

            return this;
        }

        public PropertyValidationBuilderBase<TObject, TProperty> WithMessage(string message)
        {
            _propertyValidator.Rules.LastOrDefault()?.OverrideErrorMessage(message);

            return this;
        }

        public PropertyValidationBuilderBase<TObject, TProperty> WithCode(string code)
        {
            _propertyValidator.Rules.LastOrDefault()?.OverrideErrorCode(code);

            return this;
        }
    }
}