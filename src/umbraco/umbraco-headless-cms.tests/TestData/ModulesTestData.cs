using System.Collections.Generic;
using Moq;
using Umbraco.Core.Models;

namespace umbraco_headless_cms.tests.TestData
{
    public class ModulesTestData
    {
        private Mock<IPublishedContent> _mockedPublishedContent;

        public IEnumerable<IPublishedContent> SimpleListOfModules()
        {
            var modules = new List<IPublishedContent>
            {
                GetTextboxModuleAsPublishedContent(),
                GetImageboxModuleAsPublishedContent(),
                GetComponentModuleAsPublishedContent()
            };
            return modules;
        }

        public IEnumerable<IPublishedContent> InnerComponentModule()
        {
            var modules = new List<IPublishedContent>
            {
                GetTextboxModuleAsPublishedContent()
            };
            return modules;
        }

        private IPublishedContent GetTextboxModuleAsPublishedContent()
        {
            _mockedPublishedContent = new Mock<IPublishedContent>();
            _mockedPublishedContent.SetupGet(y => y.DocumentTypeAlias)
                .Returns("textboxModule");

            return _mockedPublishedContent.Object;
        }

        private IPublishedContent GetImageboxModuleAsPublishedContent()
        {
            _mockedPublishedContent = new Mock<IPublishedContent>();
            _mockedPublishedContent.SetupGet(y => y.DocumentTypeAlias)
                .Returns("imageboxModule");

            return _mockedPublishedContent.Object;
        }

        private IPublishedContent GetComponentModuleAsPublishedContent()
        {
            _mockedPublishedContent = new Mock<IPublishedContent>();
            _mockedPublishedContent.SetupGet(y => y.DocumentTypeAlias)
                .Returns("componentModule");

            return _mockedPublishedContent.Object;
        }
    }

}