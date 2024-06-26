﻿

using Nexus.Core.Localization;
using Nexus.Core.Utilities;
using Nexus.Selenium.Configurations;
using Nexus.Selenium.Configurations.WebDriverSettings;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using WebDriverManager;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Nexus.Selenium.Browsers
{
    /// <summary>
    /// Factory that creates instance of local Browser.
    /// </summary>
    public class LocalBrowserFactory : BrowserFactory
    {
        private static readonly object WebDriverDownloadingLock = new object();
        private const string HostAddressDefault = "::1";

        public LocalBrowserFactory(IActionRetrier actionRetrier, IBrowserProfile browserProfile, ITimeoutConfiguration timeoutConfiguration, ILocalizedLogger localizedLogger)
            : base(actionRetrier, browserProfile, timeoutConfiguration, localizedLogger)
        {
        }

        protected override WebDriver Driver
        {
            get
            {
                var commandTimeout = TimeoutConfiguration.Command;
                var browserName = BrowserProfile.BrowserName;
                var driverSettings = BrowserProfile.DriverSettings;
                WebDriver driver;
                switch (browserName)
                {
                    case BrowserName.Chrome:
                    case BrowserName.Yandex:
                        SetUpDriver(new ChromeConfig(), driverSettings);
                        driver = GetDriver<ChromeDriver>(ChromeDriverService.CreateDefaultService(),
                            (ChromeOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    case BrowserName.Firefox:
                        SetUpDriver(new FirefoxConfig(), driverSettings);
                        var geckoService = FirefoxDriverService.CreateDefaultService();
                        geckoService.Host = ((FirefoxSettings)driverSettings).IsGeckoServiceHostDefaultEnabled ? HostAddressDefault : geckoService.Host;
                        driver = GetDriver<FirefoxDriver>(geckoService, (FirefoxOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    case BrowserName.IExplorer:
                        SetUpDriver(new InternetExplorerConfig(), driverSettings);
                        driver = GetDriver<InternetExplorerDriver>(InternetExplorerDriverService.CreateDefaultService(),
                            (InternetExplorerOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    case BrowserName.Edge:
                        SetUpDriver(new EdgeConfig(), driverSettings);
                        driver = GetDriver<EdgeDriver>(EdgeDriverService.CreateDefaultService(),
                            (EdgeOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    case BrowserName.Opera:
                        var config = new OperaConfig();
                        var driverPath = SetUpDriver(config, driverSettings);
                        driver = GetDriver<ChromeDriver>(ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(driverPath), config.GetBinaryName()),
                            (ChromeOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    case BrowserName.Safari:
                        driver = GetDriver<SafariDriver>(SafariDriverService.CreateDefaultService(),
                            (SafariOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    default:
                        throw new NotSupportedException($"Browser [{browserName}] is not supported.");
                }
                return driver;
            }
        }

        private WebDriver GetDriver<T>(DriverService driverService, DriverOptions driverOptions, TimeSpan commandTimeout) where T : WebDriver
        {
            return (T)Activator.CreateInstance(typeof(T), driverService, driverOptions, commandTimeout);
        }

        private static string SetUpDriver(IDriverConfig driverConfig, IDriverSettings driverSettings)
        {
            var architecture = driverSettings.SystemArchitecture.Equals(Architecture.Auto) ? ArchitectureHelper.GetArchitecture() : driverSettings.SystemArchitecture;
            var version = driverSettings.WebDriverVersion.Equals(VersionResolveStrategy.Latest) ? driverConfig.GetLatestVersion() : driverSettings.WebDriverVersion;
            version = version.Equals(VersionResolveStrategy.MatchingBrowser) ? driverConfig.GetMatchingBrowserVersion() : version;
            var url = UrlHelper.BuildUrl(architecture.Equals(Architecture.X32) ? driverConfig.GetUrl32() : driverConfig.GetUrl64(), version);
            var binaryPath = FileHelper.GetBinDestination(driverConfig.GetName(), version, architecture, driverConfig.GetBinaryName());
            if (!File.Exists(binaryPath) || !Environment.GetEnvironmentVariable("PATH").Contains(binaryPath))
            {
                lock (WebDriverDownloadingLock)
                {
                    return new DriverManager().SetUpDriver(url, binaryPath);
                }
            }
            return binaryPath;
        }
    }
}
