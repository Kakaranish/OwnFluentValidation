using System.Threading.Tasks;
using CustomValidation.Types;

namespace CustomValidation
{
    public interface IAsyncPropertyValidator
    {
        Task<PropertyValidationResult> Validate(object obj);
    }
}