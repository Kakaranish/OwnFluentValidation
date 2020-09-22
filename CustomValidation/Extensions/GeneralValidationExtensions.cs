using CustomValidation.PropertyValidationBuilders;

namespace CustomValidation.Extensions
{
    public static class GeneralValidationExtensions
    {
        public static TBuilder IsNull<TBuilder, TProperty>(this PropertyValidationBuilderBase<TBuilder, TProperty> builder)
        {
            var message = "Value must be null";
            const string errorCode = "NULL";
            builder.PropertyValidator.AddRule<TProperty>(x => x == null, message, errorCode);

            return builder.Builder;
        }

        public static TBuilder IsNotNull<TBuilder, TProperty>(this PropertyValidationBuilderBase<TBuilder, TProperty> builder)
        {
            var message = "Value cannot be null";
            const string errorCode = "NOT_NULL";
            builder.PropertyValidator.AddRule<TProperty>(x => x != null, message, errorCode);

            return builder.Builder;
        }
    }
}
