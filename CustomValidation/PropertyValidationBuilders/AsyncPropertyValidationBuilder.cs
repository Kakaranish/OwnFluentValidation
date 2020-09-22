using CustomValidation.PropertyValidators;
using CustomValidation.Rules;
using System;
using System.Threading.Tasks;

namespace CustomValidation.PropertyValidationBuilders
{
    public class AsyncPropertyValidationBuilder<TObject, TProperty> : 
        PropertyValidationBuilderBase<AsyncPropertyValidationBuilder<TObject, TProperty>, TProperty>
    {
        public AsyncPropertyValidationBuilder(PropertyValidatorBase propertyValidator) : base(propertyValidator)
        {
        }

        public override AsyncPropertyValidationBuilder<TObject, TProperty> Builder => this;
        public override PropertyValidatorBase PropertyValidator => BasePropertyValidator;

        public AsyncPropertyValidationBuilder<TObject, TProperty> AddAsyncRule(Func<TProperty, Task<bool>> validationPredicate,
            string errorMessage, string errorCode = null)
        {
            var rule = new AsyncValidationRule<TProperty>(validationPredicate, errorMessage, errorCode);
            PropertyValidator.Rules.Add(rule);

            return this;
        }
    }
}