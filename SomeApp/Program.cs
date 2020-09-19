using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CustomValidation;

namespace SomeApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var person = new Person
            {
                FirstName = "joe",
                LastName = "   ",
                Age = 13
            };

            ISyncValidator<Person> personSyncValidator = new PersonSyncValidator();

            var validationErrors = personSyncValidator.Validate(person);
        }

        public static async Task SomeAsyncFunc()
        {
            Expression<Func<int, Task>> xD = a => Task.CompletedTask;
            await xD.Compile()(12);
        }

        public static Task DoSthAsync(int a) => Task.CompletedTask;
    }
}
