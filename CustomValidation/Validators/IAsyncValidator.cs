using System.Threading.Tasks;
using CustomValidation.Types;

namespace CustomValidation.Validators
{
    public interface IAsyncValidator<in TObject>
    {
        Task<ValidationResult> Validate(TObject objToValidate);
    }
}