using CustomValidation.PropertyValidationBuilders;
using System;

namespace CustomValidation.Extensions
{
    public static class StringValidationExtensions
    {
        public static TBuilder IsEqual<TBuilder>(this IPropertyValidationBuilder<TBuilder, string> builder,
            string value, StringComparison stringComparison = StringComparison.CurrentCulture)
        {
            var message = "Values are not equal";
            const string errorCode = "STR_EQUALITY";
            builder.PropertyValidator.AddRule<string>(x => x.Equals(value, stringComparison), message, errorCode);

            return builder.Builder;
        }

        public static TBuilder IsNotNullOrEmpty<TBuilder>(this IPropertyValidationBuilder<TBuilder, string> builder)
        {
            var message = "Value cannot be null or empty string";
            const string errorCode = "NOT_NULL_OR_EMPTY";
            builder.PropertyValidator.AddRule<string>(x => !string.IsNullOrEmpty(x), message, errorCode);

            return builder.Builder;
        }

        public static TBuilder IsNotNullOrWhiteSpace<TBuilder>(this IPropertyValidationBuilder<TBuilder, string> builder)
        {
            var message = "Value cannot be null or whitespace";
            const string errorCode = "NOT_NULL_OR_WHITE_SPACE";
            builder.PropertyValidator.AddRule<string>(x => !string.IsNullOrWhiteSpace(x), message, errorCode);

            return builder.Builder;
        }
    }
}
