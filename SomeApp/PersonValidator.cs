using CustomValidation;

namespace SomeApp
{
    public class PersonValidator : BaseValidator<Person>
    {
        protected override void SetupRules()
        {
            RuleFor(x => x.Age)
                .AddRule(x => x > 21, "Must be > 21")
                .StopValidationAfterFailure()
                .IsGreaterThan(21)
                .Validate(1);

            RuleFor(x => x.FirstName)
                .IsNotNullOrWhiteSpace().WithMessage("Null or whitespaces").StopValidationAfterFailure()
                .AddRule(x => char.IsUpper(x[0]), "FirstName must start with upper character")
                .SetPropertyDisplayName("PersonFirstName");

            RuleFor(x => x.LastName)
                .IsNotNullOrWhiteSpace().StopValidationAfterFailure()
                .AddRule(x => char.IsUpper(x[0]), "LastName must start with upper character");
        }
    }
}
