using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Models.Content;
using umbraco_headless_cms.library.Models.Content.Modules;
using umbraco_headless_cms.tests.TestData;
using umbraco_headless_cms.library.Extensions.Wrappers;
using Umbraco.Core.Models;

namespace umbraco_headless_cms.tests.Helpers
{
    // TODO: To have full coverage of the moduleshelper class, we should add tests for resolve of all the different module types.

    [TestFixture]
    public class ModulesHelperTest
    {
        private ModulesHelper _modulesHelper;
        private ModulesTestData _modulesTestData;
        private Mock<IGetPropertyValueWrapper> _mockedGetPropertyValueWrapper;

        [SetUp]
        public void SetUp()
        {
            _modulesTestData = new ModulesTestData();
            _mockedGetPropertyValueWrapper = new Mock<IGetPropertyValueWrapper>();
        }

        [Test]
        public void ResolveModules_should_return_correct_types_of_modules()
        {
            // Arrange
            _modulesHelper = new ModulesHelper(false, _mockedGetPropertyValueWrapper.Object, false, null);
            var modules = _modulesTestData.SimpleListOfModules();

            // Act
            var response = _modulesHelper.ResolveModules(modules);
            var responseAsList = response.ToList();

            // Assert
            Assert.NotNull(response);
            Assert.IsInstanceOf<IEnumerable<BaseModule>>(response);
            Assert.AreEqual(3, responseAsList.Count);
            Assert.IsInstanceOf<TextboxModule>(responseAsList[0]);
            Assert.IsInstanceOf<ImageboxModule>(responseAsList[1]);
            Assert.IsInstanceOf<ComponentModule>(responseAsList[2]);
        }

        [Test]
        public void ResolveModules_should_resolve_Component_modules_if_option_parameter_is_true()
        {
            // Arrange
            var modules = _modulesTestData.SimpleListOfModules();
            _mockedGetPropertyValueWrapper.Setup(g => g.GetPropertyValue<IEnumerable<IPublishedContent>>(It.IsAny<IPublishedContent>(), "component"))
                .Returns(_modulesTestData.InnerComponentModule);
            _modulesHelper = new ModulesHelper(true, _mockedGetPropertyValueWrapper.Object, false, null);

            // Act
            var response = _modulesHelper.ResolveModules(modules);
            var responseAsList = response.ToList();

            // Assert
            Assert.NotNull(response);
            Assert.IsInstanceOf<IEnumerable<BaseModule>>(response);
            Assert.AreEqual(3, responseAsList.Count);
            Assert.IsInstanceOf<TextboxModule>(responseAsList[0]);
            Assert.IsInstanceOf<ImageboxModule>(responseAsList[1]);
            Assert.IsInstanceOf<TextboxModule>(responseAsList[2]);
        }

        [Test]
        public void ResolveModules_should_not_resolve_Component_modules_if_option_parameter_is_false()
        {
            // Arrange
            var modules = _modulesTestData.SimpleListOfModules();
            _modulesHelper = new ModulesHelper(false, _mockedGetPropertyValueWrapper.Object, false, null);

            // Act
            var response = _modulesHelper.ResolveModules(modules);
            var responseAsList = response.ToList();

            // Assert
            Assert.NotNull(response);
            Assert.IsInstanceOf<IEnumerable<BaseModule>>(response);
            Assert.IsInstanceOf<ComponentModule>(responseAsList[2]);
        }

        [Test]
        public void ResolveModules_should_return_empty_result_if_null_value_is_passed()
        {
            // Arrange
            _modulesHelper = new ModulesHelper(false, _mockedGetPropertyValueWrapper.Object, false, null);

            // Act
            var response = _modulesHelper.ResolveModules(null);
            
            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(0, response.Count());
        }

        [Test]
        public void ResolveModules_should_return_empty_result_if_empty_modules_list_is_passed()
        {
            // Arrange
            IEnumerable<IPublishedContent> modules = new List<IPublishedContent>();
            _modulesHelper = new ModulesHelper(false, _mockedGetPropertyValueWrapper.Object, false, null);

            // Act
            var response = _modulesHelper.ResolveModules(modules);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(0, response.Count());
        }

        [Test]
        public void ResolveModules_should_not_return_modules_that_should_not_be_published_yet()
        {
            // Arrange
            // We set all modules to have publishDate set 2 days into the future, so we expect no modules returned.
            _mockedGetPropertyValueWrapper
                .Setup(c => c.GetPropertyValue<DateTime>(It.IsAny<IPublishedContent>(), "publishDate"))
                .Returns(DateTime.Now.AddDays(2));
            _modulesHelper = new ModulesHelper(false, _mockedGetPropertyValueWrapper.Object, false, null);
            var modules = _modulesTestData.SimpleListOfModules();

            // Act
            var response = _modulesHelper.ResolveModules(modules);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(0, response.Count());
        }

        [Test]
        public void ResolveModules_should_return_modules_that_should_be_published()
        {
            // Arrange
            // We set all modules to have publishDate set 2 days back, so we expect all modules returned.
            _mockedGetPropertyValueWrapper
                .Setup(c => c.GetPropertyValue<DateTime>(It.IsAny<IPublishedContent>(), "publishDate"))
                .Returns(DateTime.Now.AddDays(-2));
            _modulesHelper = new ModulesHelper(false, _mockedGetPropertyValueWrapper.Object, false, null);
            var modules = _modulesTestData.SimpleListOfModules();

            // Act
            var response = _modulesHelper.ResolveModules(modules);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(3, response.Count());            
        }

        [Test]
        public void ResolveModules_should_not_return_modules_that_should_be_unpublished()
        {
            // Arrange
            // We set all modules to have unpublishDate set 2 days back, so we expect no modules returned.
            _mockedGetPropertyValueWrapper
                .Setup(c => c.GetPropertyValue<DateTime>(It.IsAny<IPublishedContent>(), "unpublishDate"))
                .Returns(DateTime.Now.AddDays(-2));
            _modulesHelper = new ModulesHelper(false, _mockedGetPropertyValueWrapper.Object, false, null);
            var modules = _modulesTestData.SimpleListOfModules();

            // Act
            var response = _modulesHelper.ResolveModules(modules);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(0, response.Count());
        }

        [Test]
        public void ResolveModules_should_return_modules_that_should_not_be_unpublished_yet()
        {
            // Arrange
            // We set all modules to have unpublishDate set 2 days into the future, so we expect all modules returned.
            _mockedGetPropertyValueWrapper
                .Setup(c => c.GetPropertyValue<DateTime>(It.IsAny<IPublishedContent>(), "unpublishDate"))
                .Returns(DateTime.Now.AddDays(2));
            _modulesHelper = new ModulesHelper(false, _mockedGetPropertyValueWrapper.Object, false, null);
            var modules = _modulesTestData.SimpleListOfModules();

            // Act
            var response = _modulesHelper.ResolveModules(modules);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(3, response.Count());
        }

        [Test]
        public void ResolveModules_should_not_return_modules_that_should_not_be_published_yet_with_a_preview_date_set()
        {
            // Arrange
            // We set preview mode and a preview date 2 days into the future and set all modules to have publishDate 3 days into the future, 
            // so we expect no modules returned.
            _mockedGetPropertyValueWrapper
                .Setup(c => c.GetPropertyValue<DateTime>(It.IsAny<IPublishedContent>(), "publishDate"))
                .Returns(DateTime.Now.AddDays(3));
            _modulesHelper = new ModulesHelper(false, _mockedGetPropertyValueWrapper.Object, true, DateTime.Now.AddDays(2).ToString("yyyy-MM-dd"));
            var modules = _modulesTestData.SimpleListOfModules();

            // Act
            var response = _modulesHelper.ResolveModules(modules);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(0, response.Count());
        }

        [Test]
        public void ResolveModules_should_return_modules_that_should_be_published_with_a_preview_date_set()
        {
            // Arrange
            // We set preview mode and a preview date 2 days into the future and set all modules to have publishDate 1 day into the future, 
            // so we expect all modules returned.
            _mockedGetPropertyValueWrapper
                .Setup(c => c.GetPropertyValue<DateTime>(It.IsAny<IPublishedContent>(), "publishDate"))
                .Returns(DateTime.Now.AddDays(1));
            _modulesHelper = new ModulesHelper(false, _mockedGetPropertyValueWrapper.Object, true, DateTime.Now.AddDays(2).ToString("yyyy-MM-dd"));
            var modules = _modulesTestData.SimpleListOfModules();

            // Act
            var response = _modulesHelper.ResolveModules(modules);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(3, response.Count());
        }

        [Test]
        public void ResolveModules_should_return_modules_that_should_not_be_published_yet_with_preview_true_but_no_preview_date_set()
        {
            // Arrange
            // We set preview mode to true, but set no preview date, and set all modules to have publishDate 2 days into the future, 
            // since it's preview mode we expect all modules returned.
            _mockedGetPropertyValueWrapper
                .Setup(c => c.GetPropertyValue<DateTime>(It.IsAny<IPublishedContent>(), "publishDate"))
                .Returns(DateTime.Now.AddDays(2));
            _modulesHelper = new ModulesHelper(false, _mockedGetPropertyValueWrapper.Object, true, null);
            var modules = _modulesTestData.SimpleListOfModules();

            // Act
            var response = _modulesHelper.ResolveModules(modules);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(3, response.Count());
        }

        [Test]
        public void ResolveModules_should_return_modules_that_should_be_published_with_preview_true_but_invalid_preview_date_set()
        {
            // Arrange
            // We set preview mode to true, and set an invalid preview date (date that cannot be parsed), 
            // and set all modules to have publishDate 2 days back, so we expect all modules returned.
            _mockedGetPropertyValueWrapper
                .Setup(c => c.GetPropertyValue<DateTime>(It.IsAny<IPublishedContent>(), "publishDate"))
                .Returns(DateTime.Now.AddDays(-2));
            _modulesHelper = new ModulesHelper(false, _mockedGetPropertyValueWrapper.Object, true, "2017-13-10");
            var modules = _modulesTestData.SimpleListOfModules();

            // Act
            var response = _modulesHelper.ResolveModules(modules);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(3, response.Count());
        }

    }
}