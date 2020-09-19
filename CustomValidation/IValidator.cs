using CustomValidation.Types;

namespace CustomValidation
{
    public interface IValidator<in TObject>
    {
        ValidationResult Validate(TObject objToValidate);
    }
}