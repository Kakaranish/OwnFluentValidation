using System.Threading.Tasks;
using CustomValidation.Types;

namespace CustomValidation.PropertyValidators
{
    public interface IAsyncPropertyValidator
    {
        Task<PropertyValidationResult> Validate(object obj);
    }
}