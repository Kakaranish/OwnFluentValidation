using CustomValidation.Types;
using CustomValidation.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace SomeWebApp.Filters
{
    public class ValidateAsyncAttribute : ActionFilterAttribute
    {
        private static readonly Type ValidatorType = typeof(AsyncValidator<>);

        public Type TypeToValidate { get; set; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var valueValidatorPair = TypeToValidate != null
                ? ValidateAttributeCommon.GetValueValidatorExplicitly(context, ValidatorType, TypeToValidate)
                : ValidateAttributeCommon.GetValueValidatorImplicitly(context, ValidatorType);

            var validationMethod = valueValidatorPair.Validator.GetType().GetMethod("Validate");
            var validationTask = (Task<ValidationResult>)validationMethod?.Invoke(
                valueValidatorPair.Validator, new[] { valueValidatorPair.Value });

            if (validationTask == null)
            {
                throw new InvalidOperationException(nameof(validationTask));
            }
            var validationResult = await validationTask;

            if (validationResult.Succeeded)
            {
                await base.OnActionExecutionAsync(context, next);
                return;
            }

            context.Result = new BadRequestObjectResult(validationResult);
        }
    }
}
