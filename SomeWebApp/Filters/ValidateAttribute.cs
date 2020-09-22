using CustomValidation.Types;
using CustomValidation.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SomeWebApp.Filters
{
    public class ValidateAttribute : ActionFilterAttribute
    {
        public Type TypeToValidate { get; set; }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var valueValidatorPair = TypeToValidate != null
                ? GetValueValidatorExplicitly(context)
                : GetValueValidatorImplicitly(context);

            var validationMethod = valueValidatorPair.Validator.GetType().GetMethod("Validate");
            var validationResult = (ValidationResult) validationMethod?.Invoke(
                valueValidatorPair.Validator, new[] {valueValidatorPair.Value});

            if (validationResult.Succeeded)
            {
                return base.OnActionExecutionAsync(context, next);
            }

            context.Result = new BadRequestObjectResult(validationResult);
            
            return Task.CompletedTask;
        }

        private ValueValidatorPair GetValueValidatorImplicitly(ActionExecutingContext context)
        {
            var serviceProvider = context.HttpContext.RequestServices;
            
            foreach (var value in context.ActionArguments.Values)
            {
                var validatorType = typeof(SyncValidator<>).MakeGenericType(value.GetType());
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

        private ValueValidatorPair GetValueValidatorExplicitly(ActionExecutingContext context)
        {
            var valueToValidate = context.ActionArguments.Values.FirstOrDefault(value => 
                value.GetType() == TypeToValidate);
            if (valueToValidate == null)
            {
                throw new InvalidOperationException($"There is no argument with type '{TypeToValidate.Name}' within action context");
            }

            var validatorType = typeof(SyncValidator<>).MakeGenericType(TypeToValidate);
            var validator = context.HttpContext.RequestServices.GetRequiredService(validatorType);

            return new ValueValidatorPair
            {
                Value = valueToValidate,
                Validator = validator
            };
        }

        private class ValueValidatorPair
        {
            public object Value { get; set; }
            public object Validator { get; set; }
        }
    }
}
