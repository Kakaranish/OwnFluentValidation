using CustomValidation.PropertyValidationBuilders;
using System.Linq;

namespace CustomValidation.Extensions
{
    public static class ValidationFlowExtensions
    {
        public static TBuilder SetPropertyDisplayName<TBuilder, TProperty>(
            this IPropertyValidationBuilder<TBuilder, TProperty> builder, string propertyDisplayName)
        {
            builder.PropertyValidator.SetPropertyDisplayName(propertyDisplayName);

            return builder.Builder;
        }

        public static TBuilder StopValidationAfterFailure<TBuilder, TProperty>(
            this IPropertyValidationBuilder<TBuilder, TProperty> builder)
        {
            var propertyValidator = builder.PropertyValidator;
            var lastRule = propertyValidator.Rules.LastOrDefault();
            if (lastRule != null)
            {
                lastRule.StopValidationAfterFailure = true;
            }

            return builder.Builder;
        }
        public static TBuilder WithMessage<TBuilder, TProperty>(
            this IPropertyValidationBuilder<TBuilder, TProperty> builder, string message)
        {
            builder.PropertyValidator.Rules.LastOrDefault()?.OverrideErrorMessage(message);

            return builder.Builder;
        }

        public static TBuilder WithCode<TBuilder, TProperty>(
            this IPropertyValidationBuilder<TBuilder, TProperty> builder, string code)
        {
            builder.PropertyValidator.Rules.LastOrDefault()?.OverrideErrorCode(code);

            return builder.Builder;
        }
    }
}
