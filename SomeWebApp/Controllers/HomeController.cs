using CustomValidation.Validators;
using Microsoft.AspNetCore.Mvc;
using SomeWebApp.Filters;
using SomeWebApp.Validators;

namespace SomeWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ISyncValidator<Person> _personValidator;

        public HomeController(ISyncValidator<Person> personValidator)
        {
            _personValidator = personValidator;
        }

        //[Validation(TypeToValidate = typeof(Person))]
        [Validation]
        [HttpPost("test")]
        public IActionResult Test([FromBody] Person person)
        {
            return Ok();
        }
    }
}
