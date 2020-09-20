using System;
using System.Collections.Generic;
using System.Text;
using CustomValidation.PropertyValidationBuilders;
using CustomValidation.PropertyValidators;

namespace CustomValidation.Misc
{
    public interface IPropertyBuilder<out TBuilder>
    {
        TBuilder SetPropertyDisplayName(string propertyDisplayName);
        TBuilder StopValidationAfterFailure();
        TBuilder WithMessage(string message);
        TBuilder WithCode(string code);
    }

    public class SyncPropertyBuilder<TObject, TProperty> : IPropertyBuilder<SyncPropertyBuilder<TObject, TProperty>>
    {
        private readonly IPropertyBuilder<PropertyBuilderBase<TObject, TProperty>> _baseBuilder;

        public SyncPropertyBuilder(IPropertyBuilder<PropertyBuilderBase<TObject, TProperty>> baseBuilder)
        {
            _baseBuilder = baseBuilder;
        }

        public SyncPropertyBuilder<TObject, TProperty> SetPropertyDisplayName(string propertyDisplayName)
        {
            _baseBuilder.WithMessage("");

            return this;
        }

        public SyncPropertyBuilder<TObject, TProperty> StopValidationAfterFailure()
        {
            _baseBuilder.StopValidationAfterFailure();

            return this;
        }

        public SyncPropertyBuilder<TObject, TProperty> WithMessage(string message)
        {
            throw new NotImplementedException();
        }

        public SyncPropertyBuilder<TObject, TProperty> WithCode(string code)
        {
            throw new NotImplementedException();
        }
    }

    public class AsyncPropertyBuilder<TObject, TProperty> : PropertyBuilderBase<TObject, TProperty>, 
        IPropertyBuilder<AsyncPropertyBuilder<TObject, TProperty>>
    {
        public static void Foo()
        {
            var builder = new AsyncPropertyBuilder<TObject, TProperty>();
            builder.WithMessage("").FooAsync().StopValidationAfterFailure();
        }

        public AsyncPropertyBuilder<TObject, TProperty> FooAsync()
        {
            return this;
        }

        public AsyncPropertyBuilder<TObject, TProperty> SetPropertyDisplayName(string propertyDisplayName)
        {
            return base.SetPropertyDisplayName(propertyDisplayName) as AsyncPropertyBuilder<TObject, TProperty>;
        }

        public AsyncPropertyBuilder<TObject, TProperty> StopValidationAfterFailure()
        {
            throw new NotImplementedException();
        }

        public AsyncPropertyBuilder<TObject, TProperty> WithMessage(string message)
        {
            throw new NotImplementedException();
        }

        public AsyncPropertyBuilder<TObject, TProperty> WithCode(string code)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class PropertyBuilderBase<TObject, TProperty> : IPropertyBuilder<PropertyBuilderBase<TObject, TProperty>>
    {
        public PropertyBuilderBase<TObject, TProperty> SetPropertyDisplayName(string propertyDisplayName)
        {
            return this;
        }

        public PropertyBuilderBase<TObject, TProperty> StopValidationAfterFailure()
        {
            return this;
        }

        public PropertyBuilderBase<TObject, TProperty> WithMessage(string message)
        {
            return this;
        }

        public PropertyBuilderBase<TObject, TProperty> WithCode(string code)
        {
            return this;
        }
    }
}
