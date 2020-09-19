using CustomValidation.Types;

namespace CustomValidation.Validators
{
    public interface ISyncValidator<in TObject>
    {
        ValidationResult Validate(TObject objToValidate);
    }
}