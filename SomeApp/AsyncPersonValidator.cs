using CustomValidation;
using System.Threading.Tasks;
using CustomValidation.PropertyValidationBuilders;
using CustomValidation.Validators;

namespace SomeApp
{
    public class AsyncPersonValidator : AsyncValidator<Person>
    {
        protected override void SetupRules()
        {
            RuleFor(x => x.FirstName)
                .AddAsyncRule(async firstName =>
                {
                    await Task.Delay(1000);
                    return false;
                }, "SOME ERROR XD")
                .WithMessage("");

        }
    }
}
