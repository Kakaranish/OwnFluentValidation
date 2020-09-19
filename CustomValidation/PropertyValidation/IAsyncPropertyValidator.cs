using System.Threading.Tasks;
using CustomValidation.Types;

namespace CustomValidation.PropertyValidation
{
    public interface IAsyncPropertyValidator
    {
        Task<PropertyValidationResult> Validate(object obj);
    }
}