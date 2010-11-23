using System.Collections.Generic;
using System.Web.Mvc;
using Dnfeitosa.Enums.Frameworks.ASP.NET.MVC.Binding;
using Dnfeitosa.Enums.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dnfeitosa.Enums.Frameworks.ASP.NET.MVC.Test
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
                                              ModelType = typeof(string)
                                          };

            enumModelBinder.BindModel(null, modelBindingContext);

            defaultBinder.Verify(@default => @default.BindModel(null, modelBindingContext));
        }

        [TestMethod]
        public void ShouldRetrieveAnEnum()
        {
            var enumModelBinder = new EnumModelBinder(new ModelBinderDictionary());
            var modelBindingContext = new ModelBindingContext
            {
                ModelType = typeof(EnumFixture),
                ModelName = "someVariable",
                ValueProvider = new Dictionary<string, ValueProviderResult>
                                    {
                                        {"someVariable", new ValueProviderResult(EnumFixture.Value2.Name, EnumFixture.Value2.Name, null)}
                                    }
            };

            var retrieved = enumModelBinder.BindModel(null, modelBindingContext);

            Assert.IsInstanceOfType(retrieved, typeof(EnumFixture));

            var @enum = retrieved as EnumFixture;
            Assert.AreEqual(EnumFixture.Value2, @enum);
        }
    }
}
