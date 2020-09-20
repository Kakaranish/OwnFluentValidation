using CustomValidation.PropertyValidationBuilders;
using System;
using System.Text.RegularExpressions;

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

        public static TBuilder HasMinLength<TBuilder>(this IPropertyValidationBuilder<TBuilder, string> builder,
    int minLength)
        {
            if (minLength < 0)
            {
                throw new ArgumentException($"'{nameof(minLength)}' must be >= 0");
            }

            var message = $"Has length less than {minLength}";
            const string errorCode = "STR_MIN_LENGTH";
            builder.PropertyValidator.AddRule<string>(x => x?.Length >= minLength, message, errorCode);

            return builder.Builder;
        }

        public static TBuilder HasMaxLength<TBuilder>(this IPropertyValidationBuilder<TBuilder, string> builder,
            int maxLength)
        {
            if (maxLength < 0)
            {
                throw new ArgumentException($"'{nameof(maxLength)}' must be >= 0");
            }

            var message = $"Has length greater than {maxLength}";
            const string errorCode = "STR_MAX_LENGTH";
            builder.PropertyValidator.AddRule<string>(x => x?.Length <= maxLength, message, errorCode);

            return builder.Builder;
        }

        public static TBuilder HasLengthBetween<TBuilder>(this IPropertyValidationBuilder<TBuilder, string> builder,
            int minLength, int maxLength)
        {
            if (minLength < 0 || maxLength < 0)
            {
                throw new ArgumentException($"'{nameof(minLength)}' and '{nameof(maxLength)}' must be >= 0");
            }
            if (minLength > maxLength)
            {
                throw new ArgumentException($"'{nameof(minLength)}' cannot be greater than '{nameof(maxLength)}'");
            }

            var message = $"Has length greater than {maxLength}";
            const string errorCode = "STR_MAX_LENGTH";
            builder.PropertyValidator.AddRule<string>(x => x?.Length > minLength &&
                                                           x.Length <= maxLength, message, errorCode);

            return builder.Builder;
        }

        public static TBuilder IsEmailAddress<TBuilder>(this IPropertyValidationBuilder<TBuilder, string> builder)
        {
            var message = "Has invalid email format";
            const string errorCode = "STR_INVALID_EMAIL";

            var emailRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            builder.PropertyValidator.AddRule<string>(x => Regex.IsMatch(x, emailRegex, RegexOptions.IgnoreCase),
                message, errorCode);

            return builder.Builder;
        }

        public static TBuilder MatchesRegex<TBuilder>(this IPropertyValidationBuilder<TBuilder, string> builder, string regex)
        {
            try
            {
                Regex.Match("", regex);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"'{nameof(regex)}' represents invalid regex");
            }

            var message = "Is not matching to regex";
            const string errorCode = "STR_NOT_MATCHING_TO_REGEX";

            builder.PropertyValidator.AddRule<string>(x => Regex.IsMatch(x, regex),
                message, errorCode);

            return builder.Builder;
        }
    }
}
