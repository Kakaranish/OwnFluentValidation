using System;

namespace CustomValidation
{
    public abstract class ValidationRuleBase
    {
        protected string ErrorMessage;
        protected string ErrorCode;

        public bool StopValidationAfterFailure { get; set; } = false;

        protected ValidationRuleBase(string errorMessage, string errorCode = null)
        {
            ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
            ErrorCode = errorCode;
        }

        public void OverrideErrorMessage(string errorMessage)
        {
            ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
        }
        public void OverrideErrorCode(string errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}