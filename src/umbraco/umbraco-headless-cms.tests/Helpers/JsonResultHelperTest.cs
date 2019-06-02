using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using umbraco_headless_cms.library.Helpers;
using Umbraco.Web.Mvc;

namespace umbraco_headless_cms.tests.Helpers
{
    [TestFixture]
    public class JsonResultHelperTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        // TODO: Would be nice if we could test the actual json result, but seems we then need to mock Umbraco controller context.
        // Note: Possibly we can use the Gdev.Umbraco.Test package for that. 

        [Test]
        public void GetJsonResult_should_return_valid_json_result()
        {
            // Arrange
            var content = new TestContent
            {
                SomeText = "Some test string containing text.",
                SomeDateTime = new DateTime(2017, 10, 9, 10, 30, 30),
                SomeListOfStrings = new List<string>
                {
                    "Test string number 1",
                    "Some other test string",
                    "And yet another test string"
                },
                CouldBeNull = null
            };

            // Act
            var result = JsonResultHelper.GetJsonResult(content);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<JsonNetResult>(result);
            Assert.NotNull(result.Data);
            Assert.IsInstanceOf<TestContent>(result.Data);
            Assert.AreEqual("application/json", result.ContentType);
            Assert.AreEqual(Encoding.UTF8, result.ContentEncoding);
            Assert.AreEqual(NullValueHandling.Ignore, result.SerializerSettings.NullValueHandling);
            Assert.AreEqual(DateFormatHandling.IsoDateFormat, result.SerializerSettings.DateFormatHandling);
            Assert.IsInstanceOf<CamelCasePropertyNamesContractResolver>(result.SerializerSettings.ContractResolver);
        }

    }

    internal class TestContent
    {
        public string SomeText { get; set; }

        public DateTime SomeDateTime { get; set; }

        public IEnumerable<string> SomeListOfStrings { get; set; }

        public string CouldBeNull { get; set; }
    }
}