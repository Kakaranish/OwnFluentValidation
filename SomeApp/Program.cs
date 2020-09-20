using CustomValidation.Validators;
using System.Threading.Tasks;

namespace SomeApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var person = new Person
            {
                FirstName = null,
                LastName = "   ",
                Age = 13
            };

            ISyncValidator<Person> personSyncValidator = new PersonSyncValidator();
            IAsyncValidator<Person> asyncPersonValidator = new AsyncPersonValidator();

            var validationErrors = personSyncValidator.Validate(person);
            var asyncValidationErrors = await asyncPersonValidator.Validate(person);
        }
    }
}
