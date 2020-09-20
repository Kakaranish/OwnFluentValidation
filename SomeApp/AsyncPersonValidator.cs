using CustomValidation.Validators;
using System.Threading.Tasks;
using CustomValidation;
using CustomValidation.Extensions;

namespace SomeApp
{
    public class AsyncPersonValidator : AsyncValidator<Person>
    {
        protected override void SetupRules()
        {
            RuleFor(x => x.FirstName)
                .AddRule(x => x.EndsWith("x"), "must end with 'x'", "")
                .AddAsyncRule(async firstName =>
                {
                    await Task.Delay(1000);
                    return false;
                }, "SOME ERROR XD")
                .WithMessage("")
                .IsNotNull();
        }
    }
}
