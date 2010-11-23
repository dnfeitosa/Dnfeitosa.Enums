using System;
using System.Web.Mvc;
using Dnfeitosa.Enums.Helpers;

namespace Dnfeitosa.Enums.Frameworks.ASP.NET.MVC2.Binding
{
    public class EnumModelBinder : IModelBinder
    {
        private readonly IModelBinder _defaultBinder;

        public EnumModelBinder(ModelBinderDictionary binders)
        {
            if (binders == null)
                throw new ArgumentNullException("binders");
            _defaultBinder = binders.DefaultBinder;
            binders.DefaultBinder = this;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var modelType = bindingContext.ModelType;
            if (!modelType.IsAnEnumType())
                return _defaultBinder.BindModel(controllerContext, bindingContext);

            var enumName = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue;
            if (enumName == null || enumName.Trim() == "")
                return null;

            var @enum = modelType.GetFieldNamed(enumName);
            return @enum == null
                ? null
                : @enum.GetValue(null);
        }

        public static void Setup()
        {
            new EnumModelBinder(ModelBinders.Binders);
        }
    }
}
