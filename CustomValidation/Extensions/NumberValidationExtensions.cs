using CustomValidation.PropertyValidationBuilders;
using System;

namespace CustomValidation.Extensions
{
    public static class NumberValidationExtensions
    {
        public static TBuilder IsGreaterThan<TBuilder, TNumber>(
            this PropertyValidationBuilderBase<TBuilder, TNumber> builder, TNumber value)
            where TNumber : IComparable<TNumber>
        {
            var message = $"Value must be greater than {value}";
            const string errorCode = "NUM_GREATER_THAN";
            builder.PropertyValidator.AddRule<TNumber>(x => x.CompareTo(value) > 0, message, errorCode);

            return builder.Builder;
        }

        public static TBuilder IsGreaterThanOrEqualTo<TBuilder, TNumber>(
            this PropertyValidationBuilderBase<TBuilder, TNumber> builder, TNumber value)
            where TNumber : IComparable<TNumber>
        {
            var message = $"Value must be greater than or equal to {value}";
            const string errorCode = "NUM_GREATER_THAN_OR_EQUAL_TO";
            builder.PropertyValidator.AddRule<TNumber>(x => x.CompareTo(value) >= 0, message, errorCode);

            return builder.Builder;
        }

        public static TBuilder IsLessThan<TBuilder, TNumber>(
            this PropertyValidationBuilderBase<TBuilder, TNumber> builder, TNumber value)
            where TNumber : IComparable<TNumber>
        {
            var message = $"Value must be less than {value}";
            const string errorCode = "NUM_LESS_THAN";
            builder.PropertyValidator.AddRule<TNumber>(x => x.CompareTo(value) < 0, message, errorCode);

            return builder.Builder;
        }

        public static TBuilder IsLessThanOrEqualTo<TBuilder, TNumber>(
            this PropertyValidationBuilderBase<TBuilder, TNumber> builder, TNumber value)
            where TNumber : IComparable<TNumber>
        {
            var message = $"Value must be less than or equal to {value}";
            const string errorCode = "NUM_LESS_THAN_OR_EQUAL_TO";
            builder.PropertyValidator.AddRule<TNumber>(x => x.CompareTo(value) <= 0, message, errorCode);

            return builder.Builder;
        }
    }
}
