using Nexus.Selenium.Browsers;
using Nexus.Selenium.Configurations;
using Nexus.Template.CustomAttributes;
using Nexus.Template.Forms_PageFolder_.Pages;
using Nexus.TestProject.Constants;
using Nexus.TestProject.Extensions;
namespace Nexus.TestProject.Steps
{
    public class MainPageSteps
    {
        private readonly MainPage mainPage = new MainPage();

        [LogStep(StepType.Assertion)]
        public void MainPageIsPresent()
        {
            mainPage.AssertIsPresent();
        }

        [LogStep(StepType.Assertion)]
        public void AcceptCookiesButtonIsDisplayed()
        {
            // Assert.IsTrue(mainPage.IsAcceptCookiesButtonDisplayed, "Accept cookies button should be displayed");
        }

        [LogStep(StepType.Assertion)]
        public void AcceptCookiesButtonIsNotDisplayed()
        {
            NexusServices.ConditionalWait.WaitForTrue(() => mainPage.IsAcceptCookiesButtonDisplayed,
            NexusServices.Get<ITimeoutConfiguration>().Script, NexusServices.Get<ITimeoutConfiguration>().PollingInterval,
                "Accept cookies button should not be displayed");
        }

        [LogStep(StepType.Step)]
        public void AcceptCookies()
        {
            mainPage.AcceptCookies();
        }

        [LogStep(StepType.Step)]
        public static void ScrollToTheFooter()
        {
            var fullPageHeight = GetFullPageHeight();
            NexusServices.Browser.ScrollWindowBy(0, fullPageHeight);
        }

        [LogStep(StepType.Step)]
        public static int GetFullPageHeight()
        {
            var pageHeight = NexusServices.Browser.ExecuteScriptFromFile<long>(ResourceConstants.PathToGetFullPageHeightJS);
            return (int)(long)pageHeight;
        }
    }
}
