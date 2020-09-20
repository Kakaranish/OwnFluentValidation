using CustomValidation.PropertyValidators;
using CustomValidation.Rules;
using System;
using System.Threading.Tasks;

namespace CustomValidation.PropertyValidationBuilders
{
    public class AsyncPropertyValidationBuilder<TObject, TProperty> : PropertyValidationBuilderBase<TObject, TProperty>
    {
        internal AsyncPropertyValidationBuilder(PropertyValidatorBase propertyValidator) : base(propertyValidator)
        {
        }

        public AsyncPropertyValidationBuilder<TObject, TProperty> AddAsyncRule(Func<TProperty, Task<bool>> validationPredicate,
            string errorMessage, string errorCode = null)
        {
            var rule = new AsyncValidationRule<TProperty>(validationPredicate, errorMessage, errorCode);
            PropertyValidator.Rules.Add(rule);

            return this;
        }
    }
}