using Nexus.Core.Configuration;
using Nexus.Core.Localization;
using Nexus.Core.Utilities;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Selenium.Browsers
{    
     /// <summary>
     /// Factory that creates instance of remote Browser.
     /// </summary>
    public class RemoteBrowserFactory : BrowserFactory
    {
        public RemoteBrowserFactory(IActionRetrier actionRetrier, IBrowserProfile browserProfile, ITimeoutConfiguration timeoutConfiguration, ILocalizedLogger localizedLogger)
    : base(actionRetrier, browserProfile, timeoutConfiguration, localizedLogger)
        {
        }

        protected override WebDriver Driver
        {
            get
            {
                LocalizedLogger.Info("loc.browser.grid");
                var capabilities = BrowserProfile.DriverSettings.DriverOptions.ToCapabilities();
                try
                {
                    return new RemoteWebDriver(BrowserProfile.RemoteConnectionUrl, capabilities, TimeoutConfiguration.Command);
                }
                catch (Exception e)
                {
                    LocalizedLogger.Fatal("loc.browser.grid.fail", e);
                    throw;
                }
            }
        }
    }
}
