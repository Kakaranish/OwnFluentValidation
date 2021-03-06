﻿using CustomValidation.Types;
using CustomValidation.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace SomeWebApp.Filters
{
    public class ValidateAttribute : ActionFilterAttribute
    {
        private static readonly Type ValidatorType = typeof(SyncValidator<>);

        public Type TypeToValidate { get; set; }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var valueValidatorPair = TypeToValidate != null
                ? ValidateAttributeCommon.GetValueValidatorExplicitly(context, ValidatorType, TypeToValidate)
                : ValidateAttributeCommon.GetValueValidatorImplicitly(context, ValidatorType);

            var validationMethod = valueValidatorPair.Validator.GetType().GetMethod("Validate");
            var validationResult = (ValidationResult)validationMethod?.Invoke(
                valueValidatorPair.Validator, new[] { valueValidatorPair.Value });

            if (validationResult.Succeeded)
            {
                return base.OnActionExecutionAsync(context, next);
            }

            context.Result = new BadRequestObjectResult(validationResult);

            return Task.CompletedTask;
        }
    }
}
