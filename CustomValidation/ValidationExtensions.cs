using System;

namespace CustomValidation
{
    public static class ValidationExtensions
    {
        public static PropertyValidationBuilder<TObject, TNumber> IsGreaterThan<TObject, TNumber>(
            this PropertyValidationBuilder<TObject, TNumber> propertyValidationBuilder, TNumber value)
            where TNumber : IComparable<TNumber>
        {
            // TODO:
            var message = $"Value must be greater than............";
            propertyValidationBuilder.AddRule(x => x.CompareTo(value) > 0, message);

            return propertyValidationBuilder;
        }
    }
}
