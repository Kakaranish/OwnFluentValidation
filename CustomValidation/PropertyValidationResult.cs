namespace CustomValidation
{
    public class PropertyValidationResult
    {
        public PropertyValidationError PropertyValidationError { get; set; }
        public bool Success => PropertyValidationError == null;
        public bool Failure => !Success;
    }
}
