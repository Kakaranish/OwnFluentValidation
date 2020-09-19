using CustomValidation.Types;

namespace CustomValidation
{
    public interface ISyncValidator<in TObject>
    {
        ValidationResult Validate(TObject objToValidate);
    }
}