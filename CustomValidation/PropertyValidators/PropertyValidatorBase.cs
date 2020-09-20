using CustomValidation.Rules;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CustomValidation.PropertyValidators
{
    public abstract class PropertyValidatorBase
    {
        public IList<ValidationRuleBase> Rules { get; } = new List<ValidationRuleBase>();

        protected readonly PropertyInfo Property;
        protected string PropertyDisplayName;

        protected PropertyValidatorBase(PropertyInfo property)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
            PropertyDisplayName = property.Name;
        }

        public void SetPropertyDisplayName(string propertyDisplayName)
        {
            PropertyDisplayName = propertyDisplayName ?? throw new ArgumentNullException(nameof(propertyDisplayName));
        }
    }
}
