using System;
using System.Collections.Generic;
using System.Text;
using CustomValidation;

namespace SomeApp
{
    public class PersonAsyncValidator : AsyncValidator<Person>
    {
        protected override void SetupRules()
        {
            RuleFor(x => x.FirstName);
        }
    }
}
