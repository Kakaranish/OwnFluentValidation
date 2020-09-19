using CustomValidation.Types;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CustomValidation
{
    public class AsyncPropertyValidationBuilder<TObject, TProperty> : PropertyValidationBuilderBase<TObject, TProperty>,
        IAsyncPropertyValidator
    {
        internal AsyncPropertyValidationBuilder(MemberExpression memberExpression) : base(memberExpression)
        {
        }

        public AsyncPropertyValidationBuilder<TObject, TProperty> AddAsyncRule(Func<TProperty, Task<bool>> validationPredicate,
            string errorMessage, string errorCode = null)
        {
            var rule = new AsyncValidationRule<TProperty>(validationPredicate, errorMessage, errorCode);
            Rules.Add(rule);

            return this;
        }

        public async Task<PropertyValidationResult> Validate(object obj)
        {
            return await Validate((TObject)obj);
        }

        public async Task<PropertyValidationResult> Validate(TObject obj)
        {
            var propertyValueAsObj = Property.GetValue(obj);
            var propertyValue = (TProperty)Convert.ChangeType(propertyValueAsObj, typeof(TProperty));

            var ruleValidationErrors = new List<RuleValidationError>();
            foreach (var rule in Rules)
            {
                RuleValidationResult ruleValidationResult;
                if (rule is ISyncValidationRule syncValidationRule)
                {
                    ruleValidationResult = syncValidationRule.Validate(propertyValue);
                }
                else if (rule is IAsyncValidationRule asyncValidationRule)
                {
                    ruleValidationResult = await asyncValidationRule.Validate(propertyValue);
                }
                else
                {
                    // TODO: Throwing exception here
                    throw new Exception("TODO:");
                }

                if (ruleValidationResult.Failure)
                {
                    ruleValidationErrors.Add(ruleValidationResult.Error);
                }
            }

            return new PropertyValidationResult(PropertyDisplayName, ruleValidationErrors);
        }
    }
}