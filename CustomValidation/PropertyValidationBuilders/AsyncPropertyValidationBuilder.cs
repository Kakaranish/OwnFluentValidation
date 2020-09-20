using CustomValidation.PropertyValidators;
using CustomValidation.Rules;
using System;
using System.Threading.Tasks;

namespace CustomValidation.PropertyValidationBuilders
{
    public class AsyncPropertyValidationBuilder<TObject, TProperty> : 
        IPropertyValidationBuilder<AsyncPropertyValidationBuilder<TObject, TProperty>, TProperty>
    {
        private readonly IPropertyValidationBuilder<PropertyValidationBuilderBase<TObject, TProperty>, TProperty> _baseValidationBuilder;

        public AsyncPropertyValidationBuilder(
            IPropertyValidationBuilder<PropertyValidationBuilderBase<TObject, TProperty>, TProperty> baseValidationBuilder)
        {
            _baseValidationBuilder = baseValidationBuilder ?? throw new ArgumentNullException(nameof(baseValidationBuilder));
        }

        public AsyncPropertyValidationBuilder<TObject, TProperty> AddAsyncRule(Func<TProperty, Task<bool>> validationPredicate,
            string errorMessage, string errorCode = null)
        {
            var rule = new AsyncValidationRule<TProperty>(validationPredicate, errorMessage, errorCode);
            PropertyValidator.Rules.Add(rule);

            return this;
        }

        public PropertyValidatorBase PropertyValidator => _baseValidationBuilder.PropertyValidator;
        public AsyncPropertyValidationBuilder<TObject, TProperty> AddRule(Predicate<TProperty> validationPredicate,
            string errorMessage, string errorCode = null)
        {
            _baseValidationBuilder.AddRule(validationPredicate, errorMessage, errorCode);
            return this;
        }

        public AsyncPropertyValidationBuilder<TObject, TProperty> SetPropertyDisplayName(string propertyDisplayName)
        {
            _baseValidationBuilder.SetPropertyDisplayName(propertyDisplayName);
            return this;
        }

        public AsyncPropertyValidationBuilder<TObject, TProperty> StopValidationAfterFailure()
        {
            _baseValidationBuilder.StopValidationAfterFailure();
            return this;
        }

        public AsyncPropertyValidationBuilder<TObject, TProperty> WithMessage(string message)
        {
            _baseValidationBuilder.WithMessage(message);
            return this;
        }

        public AsyncPropertyValidationBuilder<TObject, TProperty> WithCode(string code)
        {
            _baseValidationBuilder.WithCode(code);
            return this;
        }
    }
}