using Nexus.Selenium.Elements.Interfaces;
using Nexus.Selenium.Forms;
using OpenQA.Selenium;

namespace Nexus.Template.Forms_PageFolder_
{
    public class BaseAppForm : Form
    {
        private IButton AcceptCookiesButton => ElementFactory.GetButton(By.ClassName("cookies__button"), "Accept cookies");

        protected BaseAppForm(By locator, string name) : base(locator, name)
        {
        }

        public bool IsAcceptCookiesButtonDisplayed => AcceptCookiesButton.State.IsDisplayed;

        public void AcceptCookies() => AcceptCookiesButton.Click();
    }
}
