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
    public class ValidationAttribute : ActionFilterAttribute
    {
        public Type TypeToValidate { get; set; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            object valueToValidate;
            if (TypeToValidate == null)
            {
                valueToValidate = GetValueToValidateImplicitly(context)
                    ?? throw new InvalidOperationException($"There is no argument to validate in action context");
            }
            else
            {
                valueToValidate = context.ActionArguments.Values.FirstOrDefault(value => value.GetType() == TypeToValidate)
                    ?? throw new InvalidOperationException($"There is no argument with type '{TypeToValidate.Name}' within action context");
            }

            var valueToValidateType = TypeToValidate ?? valueToValidate.GetType();
            var validatorType = typeof(ISyncValidator<>).MakeGenericType(valueToValidateType);
            var validator = context.HttpContext.RequestServices.GetRequiredService(validatorType);
            var validationResult = (ValidationResult)validatorType.GetMethod("Validate")?.Invoke(
                validator, new[] { valueToValidate });

            if (validationResult.Succeeded)
            {
                await base.OnActionExecutionAsync(context, next);
                return;
            }

            context.Result = new BadRequestObjectResult(validationResult);
        }

        private static object GetValueToValidateImplicitly(ActionExecutingContext context)
        {
            return context.ActionArguments.Values.FirstOrDefault(value =>
            {
                var baseType = value.GetType().BaseType;
                if (baseType == null)
                {
                    return false;
                }

                // TODO: Add async
                return baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(SyncValidator<>);
            });
        }
    }
}
