using Nexus.Core.Utilities;
using Nexus.Selenium.Browsers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Settings specific for Yandex web driver.
    /// </summary>
    public class YandexSettings : ChromeSettings
    {
        private const string DefaultBinaryLocation = "%USERPROFILE%\\AppData\\Local\\Yandex\\YandexBrowser\\Application\\browser.exe";
        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public YandexSettings(ISettingsFile settingsFile) : base(settingsFile)
        {
        }

        public virtual string BinaryLocation
        {
            get
            {
                var pathInConfiguration = SettingsFile.GetValueOrDefault($"{DriverSettingsPath}.binaryLocation", DefaultBinaryLocation);
                return pathInConfiguration.StartsWith("%") ? Environment.ExpandEnvironmentVariables(pathInConfiguration) : Path.GetFullPath(pathInConfiguration);
            }
        }

        protected override BrowserName BrowserName => BrowserName.Yandex;

        public override DriverOptions DriverOptions
        {
            get
            {
                var options = (ChromeOptions)base.DriverOptions;
                options.BinaryLocation = BinaryLocation;
                return options;
            }
        }
    }
}
