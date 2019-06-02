using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Helpers.Interfaces;
using umbraco_headless_cms.library.ProcessorAttributes;
using Umbraco.Core.Models;

namespace umbraco_headless_cms.tests.ProcessorAttributes
{
    [TestFixture]
    public class QuantityListProcessorAttributeTest
    {
        private Mock<IProcessorAttributeHelper> _mockProcessorAttributeHelper;

        private Mock<IPublishedContent> _mockedPublishedContent;
        private Mock<IPublishedProperty> _mockedPublishedProperty;

        [SetUp]
        public void SetUp()
        {
            _mockProcessorAttributeHelper = new Mock<IProcessorAttributeHelper>();
        }

        [Test]
        public void ProcessValue_should_return_list_of_integers()
        {
            // Arrange
            _mockProcessorAttributeHelper.Setup(x => x.ResolveContent<IEnumerable<IPublishedContent>>(
                It.IsAny<string>(),
                It.IsAny<DittoProcessorContext>()
            )).Returns(QuantityListContentStub());

            var quantityListProcessorAttribute =
                new QuantityListProcessorAttribute(_mockProcessorAttributeHelper.Object);

            // Act
            var result = quantityListProcessorAttribute.ProcessValue() as List<int?>;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(5, result[1]);
            Assert.AreEqual(10, result[2]);
            Assert.AreEqual(25, result[3]);
        }

        private IEnumerable<IPublishedContent> QuantityListContentStub()
        {
            var quantityList = new List<IPublishedContent>
            {
                GetProductQuantityContentMock(1),
                GetProductQuantityContentMock(5),
                GetProductQuantityContentMock(10),
                GetProductQuantityContentMock(25)
            };
            return quantityList;
        }

        private IPublishedContent GetProductQuantityContentMock(int quantity)
        {
            _mockedPublishedProperty = new Mock<IPublishedProperty>();
            _mockedPublishedProperty.SetupGet(p => p.HasValue).Returns(true);
            _mockedPublishedProperty.SetupGet(p => p.PropertyTypeAlias).Returns("quantity");
            _mockedPublishedProperty.SetupGet(p => p.DataValue).Returns(quantity);
            _mockedPublishedProperty.SetupGet(p => p.Value).Returns(quantity);

            _mockedPublishedContent = new Mock<IPublishedContent>();
            _mockedPublishedContent.Setup(p => p.Properties.Add(_mockedPublishedProperty.Object));
            _mockedPublishedContent.Setup(p => p.GetProperty("quantity")).Returns(_mockedPublishedProperty.Object);

            return _mockedPublishedContent.Object;
        }
    }
}