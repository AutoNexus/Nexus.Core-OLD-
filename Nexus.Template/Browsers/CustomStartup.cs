using Microsoft.Extensions.DependencyInjection;
using Nexus.Core.Applications;
using Nexus.Core.Localization;
using Nexus.Core.Utilities;
using Nexus.Selenium.Browsers;
using Nexus.Template.Configurations;
using Nexus.Template.Logging;

namespace Nexus.Template.Browsers
{
    public class CustomStartup : BrowserStartup
    {
        public override IServiceCollection ConfigureServices(IServiceCollection services, Func<IServiceProvider, IApplication> applicationProvider, ISettingsFile settings = null)
        {
            settings = settings ?? GetSettings();
            base.ConfigureServices(services, applicationProvider, settings);
            //The logic is related to Allure.If you don't plan to use Allure, delete the following code:
            services.AddSingleton<ILocalizedLogger, AllureBasedLocalizedLogger>();
            //End of Allure Logic
            services.AddSingleton<ICustomTimeoutConfiguration, CustomTimeoutConfiguration>();
            return services;
        }
    }
}
