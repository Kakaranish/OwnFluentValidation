using CustomValidation.PropertyValidationBuilders;

namespace CustomValidation.Extensions
{
    public static class GeneralValidationExtensions
    {
        public static TBuilder IsNotNull<TBuilder, TProperty>(this IPropertyValidationBuilder<TBuilder, TProperty> builder)
        {
            var message = "Value cannot be null";
            const string errorCode = "NOT_NULL";
            builder.PropertyValidator.AddRule<TProperty>(x => x != null, message, errorCode);

            return builder.Builder;
        }
    }
}
