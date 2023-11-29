using Nexus.Core.Configuration;
using Nexus.Core.Localization;
using Nexus.Core.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Selenium.Browsers
{
    /// <summary>
    /// Abstract representation of <see cref="IBrowserFactory"/>.
    /// </summary>
    public abstract class BrowserFactory : IBrowserFactory
    {
        protected BrowserFactory(IActionRetrier actionRetrier, IBrowserProfile browserProfile, ITimeoutConfiguration timeoutConfiguration, ILocalizedLogger localizedLogger)
        {
            ActionRetrier = actionRetrier;
            BrowserProfile = browserProfile;
            TimeoutConfiguration = timeoutConfiguration;
            LocalizedLogger = localizedLogger;
        }

        protected IActionRetrier ActionRetrier { get; }
        protected IBrowserProfile BrowserProfile { get; }
        protected ITimeoutConfiguration TimeoutConfiguration { get; }
        protected ILocalizedLogger LocalizedLogger { get; }

        protected abstract WebDriver Driver { get; }

        public virtual Browser Browser
        {
            get
            {
                var browser = new Browser(ActionRetrier.DoWithRetry(() => Driver, new[] { typeof(WebDriverException), typeof(InvalidOperationException) }));
                LocalizedLogger.Info("loc.browser.ready", BrowserProfile.BrowserName);
                return browser;
            }
        }
    }
}
