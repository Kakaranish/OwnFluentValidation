using System;

namespace CustomValidation
{
    public static class ValidationExtensions
    {
        public static PropertyValidationBuilder<TObject, TProperty> IsEqual<TObject, TProperty>(
            this PropertyValidationBuilder<TObject, TProperty> propertyValidationBuilder, TProperty value)
            where TProperty : IEquatable<TProperty>
        {
            var message = $"Value must equal to {value}";
            const string errorCode = "EQUAL";
            propertyValidationBuilder.AddRule(x => x.Equals(value), message, errorCode);

            return propertyValidationBuilder;
        }

        public static PropertyValidationBuilder<TObject, string> IsEqual<TObject>(
            this PropertyValidationBuilder<TObject, string> propertyValidationBuilder, string value, 
            StringComparison stringComparison = StringComparison.CurrentCulture)
        {
            var message = $"Value must equal to {value}";
            const string errorCode = "EQUAL";
            propertyValidationBuilder.AddRule(x => x.Equals(value, stringComparison), message, errorCode);

            return propertyValidationBuilder;
        }

        public static PropertyValidationBuilder<TObject, TNumber> IsGreaterThan<TObject, TNumber>(
            this PropertyValidationBuilder<TObject, TNumber> propertyValidationBuilder, TNumber value)
            where TNumber : IComparable<TNumber>
        {
            var message = $"Value must be greater than {value}";
            const string errorCode = "NUM_GREATER";
            propertyValidationBuilder.AddRule(x => x.CompareTo(value) > 0, message, errorCode);

            return propertyValidationBuilder;
        }

        public static PropertyValidationBuilder<TObject, TNumber> IsNotNull<TObject, TNumber>(
            this PropertyValidationBuilder<TObject, TNumber> propertyValidationBuilder)
        {
            var message = "Value cannot be null";
            const string errorCode = "NOT_NULL";
            propertyValidationBuilder.AddRule(x => x != null, message, errorCode);

            return propertyValidationBuilder;
        }

        public static PropertyValidationBuilder<TObject, string> IsNotNullOrEmpty<TObject>(
            this PropertyValidationBuilder<TObject, string> propertyValidationBuilder)
        {
            var message = "Value cannot be null or empty string";
            const string errorCode = "NOT_NULL_OR_EMPTY";
            propertyValidationBuilder.AddRule(x => string.IsNullOrEmpty(x), message, errorCode);

            return propertyValidationBuilder;
        }

        public static PropertyValidationBuilder<TObject, string> IsNotNullOrWhiteSpace<TObject>(
            this PropertyValidationBuilder<TObject, string> propertyValidationBuilder)
        {
            var message = "Value cannot be null or whitespace";
            const string errorCode = "NOT_NULL_OR_WHITE_SPACE";
            propertyValidationBuilder.AddRule(x => string.IsNullOrWhiteSpace(x), message, errorCode);

            return propertyValidationBuilder;
        } 
    }
}
