using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Logging;

namespace umbraco_headless_cms.library.ApplicationEvents
{
    public class ConfigurationEvent : ApplicationEventHandler
    {
        private readonly string _configurationName;
        private const string UsyncConfigurationsSourcePath = @"~\config\uSyncConfigurations";
        private const string UsyncConfigurationsTargetPath = @"~\uSync\data";


        public ConfigurationEvent() : this(ConfigurationManager.AppSettings["configurationName"])
        {
        }

        public ConfigurationEvent(string configurationName)
        {
            if (configurationName == null) throw new ArgumentNullException(nameof(configurationName));
            _configurationName = configurationName;
        }

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            if (_configurationName.ToLowerInvariant() == "default")
            {
                return;
            }

            LogHelper.Info<ConfigurationEvent>($"Configuration '{_configurationName}' will be executed.");

            try
            {
                var sourcePath = System.Web.Hosting.HostingEnvironment.MapPath(UsyncConfigurationsSourcePath);
                var targetPath = System.Web.Hosting.HostingEnvironment.MapPath(UsyncConfigurationsTargetPath);

                if (!Directory.Exists(targetPath))
                {
                    LogHelper.Warn<ConfigurationEvent>($"Copy uSync config files problem: Target path '{targetPath}' does not exist.");
                    return;
                }

                CopyUsyncFiles(sourcePath, targetPath, string.Empty);
            }
            catch (Exception ex)
            {
                LogHelper.Error<ConfigurationEvent>($"Error occurred while configuring '{_configurationName}'", ex);
                throw;
            }
        }

        private void CopyUsyncFiles(string sourcePath, string targetPath, string targetDirectoryName)
        {
            if (!Directory.Exists(sourcePath))
            {
                LogHelper.Warn<ConfigurationEvent>($"Copy uSync config files problem: Source path '{sourcePath}' does not exist.");
                return;
            }

            var targetDirectory = Path.Combine(targetPath, targetDirectoryName);
            
            var files = Directory.GetFiles(sourcePath);
            if (files.Any())
            {
                if (Directory.Exists(targetDirectory))
                {
                    foreach (var file in files)
                    {
                        if (!file.Contains(_configurationName)) continue;
                        var targetFileName = Path.GetFileName(file).Replace($"{_configurationName}.", "");
                        var targetFilePath = Path.Combine(targetDirectory, targetFileName);
                        File.Copy(file, targetFilePath, true);
                        LogHelper.Info<ConfigurationEvent>($"uSync file '{file}' was copied to target path '{targetFilePath}'.");
                    }
                }
                else
                {
                    LogHelper.Warn<ConfigurationEvent>($"Copy uSync config files: Target directory '{targetDirectory}' does not exist.");
                }
            }

            var subDirectories = Directory.GetDirectories(sourcePath);
            foreach (var subDirectory in subDirectories)
            {
                var directoryName = new DirectoryInfo(subDirectory).Name;
                CopyUsyncFiles(subDirectory, targetDirectory, directoryName);
            }
        }
    }
}