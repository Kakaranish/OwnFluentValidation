using System.Threading.Tasks;
using CustomValidation.Types;

namespace CustomValidation.Rules
{
    public interface IAsyncValidationRule
    {
        Task<RuleValidationResult> Validate(object propertyValueObj);
    }
}