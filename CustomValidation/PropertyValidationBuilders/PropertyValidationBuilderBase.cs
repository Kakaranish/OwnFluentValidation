using CustomValidation.PropertyValidators;
using CustomValidation.Rules;
using System;

namespace CustomValidation.PropertyValidationBuilders
{
    public abstract class PropertyValidationBuilderBase<TBuilder, TProperty>
    {
        private readonly TBuilder _builder;

        protected readonly PropertyValidatorBase BasePropertyValidator;

        public abstract TBuilder Builder { get; }
        public abstract PropertyValidatorBase PropertyValidator { get; }

        protected PropertyValidationBuilderBase(PropertyValidatorBase propertyValidator)
        {
            _builder = (TBuilder)(object)this;

            BasePropertyValidator = propertyValidator ?? throw new ArgumentNullException(nameof(propertyValidator));
        }

        public TBuilder AddRule(Predicate<TProperty> validationPredicate, string errorMessage, string errorCode = null)
        {
            var rule = new SyncValidationRule<TProperty>(validationPredicate, errorMessage, errorCode);
            BasePropertyValidator.Rules.Add(rule);

            return _builder;
        }
    }
}
