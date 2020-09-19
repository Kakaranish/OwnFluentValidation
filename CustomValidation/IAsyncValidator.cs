using System.Threading.Tasks;
using CustomValidation.Types;

namespace CustomValidation
{
    public interface IAsyncValidator<in TObject>
    {
        Task<ValidationResult> Validate(TObject objToValidate);
    }
}