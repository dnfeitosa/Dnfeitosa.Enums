using System;
using System.Web.Mvc;
using Dnfeitosa.Enums.Helpers;

namespace Dnfeitosa.Enums.Frameworks.ASP.NET.MVC.Binding
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

            if (!bindingContext.ValueProvider.ContainsKey(bindingContext.ModelName))
            {
                return null;
            }

            var enumName = bindingContext.ValueProvider[bindingContext.ModelName].AttemptedValue;
            if (enumName == null || enumName.Trim() == "")
                return null;

            var enumField = modelType
                .GetFieldNamed(enumName);

            return enumField == null
                ? null
                : enumField.GetValue(null);
        }

        public static void Setup()
        {
            new EnumModelBinder(ModelBinders.Binders);
        }
    }
}
