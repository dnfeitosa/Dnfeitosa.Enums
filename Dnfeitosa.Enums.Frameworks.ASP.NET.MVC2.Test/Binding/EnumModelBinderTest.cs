using System.Collections.Specialized;
using System.Web.Mvc;
using Dnfeitosa.Enums.Frameworks.ASP.NET.MVC2.Binding;
using Dnfeitosa.Enums.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dnfeitosa.Enums.Frameworks.ASP.NET.MVC2.Test.Binding
{
    [TestClass]
    public class EnumModelBinderTest
    {

        private MockFactory _factory;

        [TestInitialize]
        public void Setup()
        {
            _factory = new MockFactory(MockBehavior.Loose);
        }

        [TestMethod]
        public void ShouldCallDefaultBinderWhenGivenTypeIsNotAnEnum()
        {
            var defaultBinder = _factory.Create<IModelBinder>();
            var binders = new ModelBinderDictionary
            {
                DefaultBinder = defaultBinder.Object
            };

            var enumModelBinder = new EnumModelBinder(binders);

            var modelBindingContext = new ModelBindingContext
            {
                ModelMetadata = new ModelMetadata(new EmptyModelMetadataProvider(), null, null, typeof(string), "someProperty")
            };

            enumModelBinder.BindModel(null, modelBindingContext);

            defaultBinder.Verify(@default => @default.BindModel(null, modelBindingContext));
        }

        [TestMethod]
        public void ShouldBindAnEnumValue()
        {
            var collection = new ValueProviderCollection();
            var nameValueCollection = new NameValueCollection
                                          {
                                              {"someVariable", EnumFixture.Value2.Name}
                                          };

            collection.Add(new FormCollection(nameValueCollection));

            var modelMetadata = new ModelMetadata(new EmptyModelMetadataProvider(), null, null, typeof(EnumFixture), "someProperty");
            var bindingContext = new ModelBindingContext
                                     {
                                         ModelMetadata = modelMetadata,
                                         ValueProvider = collection,
                                         ModelName = "someVariable"
                                     };

            var binderDictionary = new ModelBinderDictionary();
            var binder = new EnumModelBinder(binderDictionary);

            var retrieved = binder.BindModel(null, bindingContext);

            Assert.IsInstanceOfType(retrieved, typeof(EnumFixture));

            var @enum = retrieved as EnumFixture;
            Assert.AreEqual(EnumFixture.Value2, @enum);
        }
    }
}
