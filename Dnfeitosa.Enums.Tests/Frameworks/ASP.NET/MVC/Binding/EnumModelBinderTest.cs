//using System.Web;
//using System.Web.Mvc;
//using Dnfeitosa.Enums.Frameworks.ASP.NET.MVC.Binding;
//using Dnfeitosa.Enums.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dnfeitosa.Enums.Tests.Frameworks.ASP.NET.MVC.Binding
{
    [TestClass]
    public class EnumModelBinderTest
    {
        private MockFactory _factory;

        [TestInitialize]
        public void Setup()
        {
            _factory = new MockFactory(MockBehavior.Strict);
        }

        //[TestMethod]
        //public void ShouldBindAnEnumValue()
        //{
        //    const string enumName = "Value1";

        //    var bindingContext = new ModelBindingContext
        //                             {
        //                                 ModelMetadata = new ModelMetadata(new DataAnnotationsModelMetadataProvider(), null, null, typeof (EnumFixture), "test.EnumProperty"),
        //                             };

        //    var request = _factory.Create<HttpRequestBase>();
        //    request.Setup(req => req[bindingContext.ModelName])
        //        .Returns(enumName);

        //    var contextBase = _factory.Create<HttpContextBase>();
        //    contextBase.SetupGet(ctxBase => ctxBase.Request)
        //        .Returns(request.Object);

        //    var context = _factory.Create<ControllerContext>();
        //    context.SetupGet(ctx => ctx.HttpContext)
        //        .Returns(contextBase.Object);

        //    var binderDictionary = new ModelBinderDictionary();
        //    var binder = new EnumModelBinder(binderDictionary);

        //    var model = binder.BindModel(context.Object, bindingContext);

        //    Assert.IsNotNull(model);
        //    Assert.IsInstanceOfType(model, typeof(EnumFixture));
        //    Assert.AreEqual(enumName, ((EnumFixture)model).Name);
        //}
    }
}
