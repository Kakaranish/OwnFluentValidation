using System;

namespace CustomValidation
{
    public class PropertyValidationError
    {
        public string ErrorMessage { get; }
        public string ErrorCode { get; }

        public PropertyValidationError(string errorMessage, string errorCode = null)
        {
            ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
            ErrorCode = errorCode;
        }
    }
}
