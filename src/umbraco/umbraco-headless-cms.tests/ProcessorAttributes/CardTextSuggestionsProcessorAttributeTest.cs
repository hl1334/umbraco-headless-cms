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
    public class CardTextSuggestionsProcessorAttributeTest
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
        public void ProcessValue_should_return_cardtexts()
        {
            // Arrange
            _mockProcessorAttributeHelper.Setup(x => x.ResolveContent<IEnumerable<IPublishedContent>>(
                It.IsAny<string>(),
                It.IsAny<DittoProcessorContext>()
            )).Returns(CardtextContentStub());

            var cardTextSuggestionsProcessorAttribute =
                new CardTextSuggestionsProcessorAttribute(_mockProcessorAttributeHelper.Object);

            // Act
            var result = cardTextSuggestionsProcessorAttribute.ProcessValue() as List<string>;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Super duper kort tekst her.", result[0]);
            Assert.AreEqual("En anden super duper kort tekst.", result[1]);
        }

        private IEnumerable<IPublishedContent> CardtextContentStub()
        {
            var cardTexts = new List<IPublishedContent>
            {
                GetCardTextContentMock("Super duper kort tekst her."),
                GetCardTextContentMock("En anden super duper kort tekst.")
            };

            return cardTexts;
        }

        private IPublishedContent GetCardTextContentMock(string cardText)
        {
            _mockedPublishedProperty = new Mock<IPublishedProperty>();
            _mockedPublishedProperty.SetupGet(p => p.HasValue).Returns(true);
            _mockedPublishedProperty.SetupGet(p => p.PropertyTypeAlias).Returns("cardText");
            _mockedPublishedProperty.SetupGet(p => p.DataValue).Returns(cardText);
            _mockedPublishedProperty.SetupGet(p => p.Value).Returns(cardText);

            _mockedPublishedContent = new Mock<IPublishedContent>();
            _mockedPublishedContent.Setup(p => p.Properties.Add(_mockedPublishedProperty.Object));
            _mockedPublishedContent.Setup(p => p.GetProperty("cardText")).Returns(_mockedPublishedProperty.Object);

            return _mockedPublishedContent.Object;
        }

        
    }

}