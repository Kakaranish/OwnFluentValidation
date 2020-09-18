namespace CustomValidation
{
    public class PersonValidator : BaseValidator<Person>
    {
        protected override void SetupRules()
        {
            RuleFor(x => x.Age)
                .AddRule(x => x > 21, "Must be > 21");

            RuleFor(x => x.FirstName)
                .AddRule(x => char.IsUpper(x[0]), "FirstName must start with upper character");
        }
    }
}
