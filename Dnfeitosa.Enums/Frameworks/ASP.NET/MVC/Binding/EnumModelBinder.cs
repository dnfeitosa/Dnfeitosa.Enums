using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

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
            if (!IsEnumType(bindingContext.ModelType))
                return _defaultBinder.BindModel(controllerContext, bindingContext);

            var enumName = controllerContext.HttpContext.Request[bindingContext.ModelName];
            if (enumName == null || enumName.Trim() == "")
                return null;

            var @enum = bindingContext.ModelType
                .GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(f => f.Name == enumName)
                .FirstOrDefault();

            return @enum == null ? null : @enum.GetValue(null);
        }

        private bool IsEnumType(Type type)
        {
            return type
                .GetInterfaces()
                .Any(iface => iface == typeof(IEnum));
        }
    }
}
