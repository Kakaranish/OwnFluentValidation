using CustomValidation.Extensions;
using CustomValidation.Validators;

namespace SomeWebApp.Validators
{
    public class OtherPersonSyncValidator : SyncValidator<Person>
    {
        protected override void SetupRules()
        {
            RuleFor(x => x.Age)
                .IsLessThan(18)
                .WithMessage("Must not be an adult");
        }
    }
}
