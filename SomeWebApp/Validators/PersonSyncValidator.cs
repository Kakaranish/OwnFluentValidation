using CustomValidation.Extensions;
using CustomValidation.Validators;

namespace SomeWebApp.Validators
{
    public class PersonSyncValidator : SyncValidator<Person>
    {
        protected override void SetupRules()
        {
            RuleFor(x => x.Age)
                .IsGreaterThan(18)
                .WithMessage("Must be adult");
        }
    }
}
