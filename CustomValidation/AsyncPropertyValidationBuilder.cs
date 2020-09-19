using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CustomValidation.Types;

namespace CustomValidation
{
    public class AsyncPropertyValidationBuilder<TObject, TProperty> : PropertyValidationBuilderBase<TObject, TProperty>, 
        IAsyncPropertyValidator
    {
        private readonly IList<AsyncValidationRule<TProperty>> _asyncRules = new List<AsyncValidationRule<TProperty>>();

        internal AsyncPropertyValidationBuilder(MemberExpression memberExpression) : base(memberExpression)
        {
        }

        public AsyncPropertyValidationBuilder<TObject, TProperty> AddAsyncRule(Func<TProperty, Task<bool>> validationPredicate,
            string errorMessage, string errorCode = null)
        {
            var rule = new AsyncValidationRule<TProperty>(validationPredicate, errorMessage, errorCode);
            _asyncRules.Add(rule);

            return this;
        }

        public Task<PropertyValidationResult> Validate(object obj)
        {
            throw new NotImplementedException();
        }
    }
}