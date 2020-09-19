namespace CustomValidation.Types
{
    public class RuleValidationResult
    {
        public RuleValidationError Error { get; set; }
        public bool Success => Error == null;
        public bool Failure => !Success;
    }
}
