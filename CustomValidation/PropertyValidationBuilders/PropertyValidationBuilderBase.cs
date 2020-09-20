using CustomValidation.PropertyValidators;
using CustomValidation.Rules;
using System;
using System.Linq;

namespace CustomValidation.PropertyValidationBuilders
{
    public abstract class PropertyValidationBuilderBase<TObject, TProperty> : IPropertyValidationBuilder
    {
        protected readonly PropertyValidatorBase PropertyValidator;

        protected PropertyValidationBuilderBase(PropertyValidatorBase propertyValidator)
        {
            PropertyValidator = propertyValidator ?? throw new ArgumentNullException(nameof(propertyValidator));
        }

        public PropertyValidationBuilderBase<TObject, TProperty> AddRule(Predicate<TProperty> validationPredicate,
            string errorMessage, string errorCode = null)
        {
            var rule = new SyncValidationRule<TProperty>(validationPredicate, errorMessage, errorCode);
            PropertyValidator.Rules.Add(rule);

            return this;
        }

        public PropertyValidationBuilderBase<TObject, TProperty> SetPropertyDisplayName(string propertyDisplayName)
        {
            PropertyValidator.SetPropertyDisplayName(propertyDisplayName);

            return this;
        }

        public PropertyValidationBuilderBase<TObject, TProperty> StopValidationAfterFailure()
        {
            var lastRule = PropertyValidator.Rules.LastOrDefault();
            if (lastRule != null)
            {
                lastRule.StopValidationAfterFailure = true;
            }

            return this;
        }

        public PropertyValidationBuilderBase<TObject, TProperty> WithMessage(string message)
        {
            PropertyValidator.Rules.LastOrDefault()?.OverrideErrorMessage(message);

            return this;
        }

        public PropertyValidationBuilderBase<TObject, TProperty> WithCode(string code)
        {
            PropertyValidator.Rules.LastOrDefault()?.OverrideErrorCode(code);

            return this;
        }
    }
}