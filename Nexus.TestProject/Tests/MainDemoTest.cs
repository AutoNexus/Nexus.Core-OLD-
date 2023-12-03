using Nexus.TestProject.Steps;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;

namespace Nexus.TestProject.Tests
{
    [AllureNUnit]
    [AllureSuite("Main Page Demo Test")]
    public class MainDemoTest : BaseWebTest
    {
        private readonly MainPageSteps mainPageSteps = new MainPageSteps();

        [SetUp]
        public new void Setup()
        {
            GoToPageStartPage();
            SetScreenExpansionMaximize();
        }

        [Test(Description = "TC-0001 Check the cookie form")]
        public void TC0001_CheckTheCookieForm()
        {
            mainPageSteps.MainPageIsPresent();
            mainPageSteps.AcceptCookiesButtonIsDisplayed();
            mainPageSteps.AcceptCookies();
            mainPageSteps.AcceptCookiesButtonIsNotDisplayed();
            MainPageSteps.ScrollToTheFooter();
            //Ddawdawd
        }
    }
}
