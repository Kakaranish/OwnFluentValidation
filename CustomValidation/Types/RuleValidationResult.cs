namespace CustomValidation.Types
{
    public class RuleValidationResult
    {
        public RuleValidationError RuleValidationError { get; set; }
        public bool Success => RuleValidationError == null;
        public bool Failure => !Success;
    }
}
