using System;

namespace CustomValidation.Types
{
    public class RuleValidationError
    {
        public string ErrorMessage { get; }
        public string ErrorCode { get; }

        public RuleValidationError(string errorMessage, string errorCode = null)
        {
            ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
            ErrorCode = errorCode;
        }
    }
}
