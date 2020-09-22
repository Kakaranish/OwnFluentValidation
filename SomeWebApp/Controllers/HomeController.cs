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
        //[Validation(TypeToValidate = typeof(Person))]
        [Validate]
        [HttpPost("test")]
        public IActionResult Test([FromBody] Person person)
        {
            return Ok();
        }
    }
}
