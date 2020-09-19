using System;
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

            IValidator<Person> personValidator = new PersonValidator();

            var validationErrors = personValidator.Validate(person);
        }
    }
}
