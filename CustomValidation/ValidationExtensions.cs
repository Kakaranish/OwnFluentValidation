using System;

namespace CustomValidation
{
    public static class ValidationExtensions
    {
        public static PropertyRuleBuilder<TObject, TNumber> IsGreaterThan<TObject, TNumber>(
            this PropertyRuleBuilder<TObject, TNumber> propertyRuleBuilder, TNumber value)
            where TNumber : IComparable<TNumber>
        {
            // TODO:
            var message = $"Value must be greater than............";
            propertyRuleBuilder.AddRule(x => x.CompareTo(value) > 0, message);

            return propertyRuleBuilder;
        }
    }
}
