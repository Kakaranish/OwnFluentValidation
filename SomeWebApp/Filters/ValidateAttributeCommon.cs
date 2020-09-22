using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace SomeWebApp.Filters
{
    public static class ValidateAttributeCommon
    {
        public static ValueValidatorPair GetValueValidatorImplicitly(ActionExecutingContext context, Type validatorGenericType)
        {
            var serviceProvider = context.HttpContext.RequestServices;

            foreach (var value in context.ActionArguments.Values)
            {
                var validatorType = validatorGenericType.MakeGenericType(value.GetType());
                var validator = serviceProvider.GetService(validatorType);

                if (validator != null)
                {
                    return new ValueValidatorPair
                    {
                        Value = value,
                        Validator = validator
                    };
                }
            }

            throw new InvalidOperationException($"There is no argument to validate in action context");
        }

        public static ValueValidatorPair GetValueValidatorExplicitly(ActionExecutingContext context, Type validatorGenericType,
            Type typeToValidate)
        {
            var valueToValidate = context.ActionArguments.Values.FirstOrDefault(value =>
                value.GetType() == typeToValidate);
            if (valueToValidate == null)
            {
                throw new InvalidOperationException($"There is no argument with type '{typeToValidate.Name}' within action context");
            }

            var validatorType = validatorGenericType.MakeGenericType(typeToValidate);
            var validator = context.HttpContext.RequestServices.GetRequiredService(validatorType);

            return new ValueValidatorPair
            {
                Value = valueToValidate,
                Validator = validator
            };
        }
    }
}
