using Humanizer;
using Nexus.Core.Logging;
using Nexus.Selenium.Browsers;
using Nexus.Template.Browsers;
using Nexus.Template.Utilities;
using NUnit.Framework.Interfaces;

namespace Nexus.TestProject.Tests
{
    public abstract class BaseTest
    {
        protected static string ScenarioName
    => TestContext.CurrentContext.Test.Properties.Get("Description")?.ToString()
    ?? TestContext.CurrentContext.Test.Name.Replace("_", string.Empty).Humanize();

        private static Logger Logger => Logger.Instance;

        private static TestContext.ResultAdapter Result => TestContext.CurrentContext.Result;
        private readonly ScreenshotProvider screenshotProvider = new ScreenshotProvider();

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Logger.Info("Setup startup config");
            NexusServices.SetStartup(new CustomStartup());
        }

        [SetUp]
        public void Setup()
        {
            Logger.Info($"Start scenario [{ScenarioName}]");
        }

        [TearDown]
        public virtual void AfterEach()
        {
            LogScenarioResult();
        }

        private void LogScenarioResult()
        {
            Logger.Info($"Scenario [{ScenarioName}] result is {Result.Outcome.Status}!");
            if (Result.Outcome.Status != TestStatus.Passed)
            {
                if (NexusServices.IsBrowserStarted)
                {
                    AttachmentHelper.AddAttachment(screenshotProvider.TakeScreenshot(), "Screenshot");
                }
                Logger.Error(Result.Message);
            }
            Logger.Info(new string('=', 100));
        }
    }
}
