using CustomValidation;
using System.Threading.Tasks;

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
                    return true;
                }, "SOME ERROR XD")
                .IsNotNull();

        }
    }
}
