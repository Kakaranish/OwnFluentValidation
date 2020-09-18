namespace CustomValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person
            {
                FirstName = "joe",
                LastName = "Doe",
                Age = 13
            };

            var personValidator = new PersonValidator();
            var validationErrors = personValidator.Validate(person);
        }
    }
}
