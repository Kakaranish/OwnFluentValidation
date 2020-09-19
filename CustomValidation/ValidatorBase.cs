using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CustomValidation
{
    public abstract class ValidatorBase<TObject>
    {
        protected readonly IList<IPropertyValidationBuilderMarker> PropertyValidationBuilders = 
            new List<IPropertyValidationBuilderMarker>();

        protected ValidatorBase()
        {
            SetupRules();
        }

        protected abstract void SetupRules();
    }
}