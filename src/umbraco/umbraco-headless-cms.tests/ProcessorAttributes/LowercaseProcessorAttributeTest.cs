using Moq;
using NUnit.Framework;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Helpers.Interfaces;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.tests.ProcessorAttributes
{
    [TestFixture]
    public class LowercaseProcessorAttributeTest
    {
        private Mock<IProcessorAttributeHelper> _mockProcessorAttributeHelper;

        [SetUp]
        public void SetUp()
        {
            _mockProcessorAttributeHelper = new Mock<IProcessorAttributeHelper>();
        }

        [TestCase("SomeCasingHere", "somecasinghere")]
        [TestCase("SomeOtherSTRANGEcaseingHere", "someotherstrangecaseinghere")]
        public void String_should_be_lowercased(string actual, string expected)
        {
            // Arange
            _mockProcessorAttributeHelper.Setup(x => x.ResolveContent<string>(
                    It.IsAny<string>(),
                    It.IsAny<DittoProcessorContext>()
            )).Returns(actual);

            var lowercaseProcessorAttribute = new LowercaseProcessorAttribute(_mockProcessorAttributeHelper.Object);

            // Act
            var result = lowercaseProcessorAttribute.ProcessValue() as string;

            // Assert
            Assert.AreEqual(expected, result);
        }

    }
}
