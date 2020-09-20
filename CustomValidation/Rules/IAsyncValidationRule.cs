using CustomValidation.Types;
using System.Threading.Tasks;

namespace CustomValidation.Rules
{
    public interface IAsyncValidationRule
    {
        Task<RuleValidationError> Validate(object propertyValueObj);
    }
}