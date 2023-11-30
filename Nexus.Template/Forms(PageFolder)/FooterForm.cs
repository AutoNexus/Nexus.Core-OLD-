using Nexus.Selenium.Browsers;
using Nexus.Selenium.Elements.Interfaces;
using Nexus.Selenium.Forms;
using Nexus.Template.Configurations;
using OpenQA.Selenium;

namespace Nexus.Template.Forms_PageFolder_
{
    public class FooterForm : Form
    {
        private ILabel LogoLabel => FormElement.FindChildElement<ILabel>(By.XPath("//a[contains(@class,'footer__logo')]"), "Logo");

        private ILabel ContactsLabel => FormElement.FindChildElement<ILabel>(By.XPath("//div[contains(@class,'footer__contacts')]"), "Contacts");

        private ILabel SubscribeLabel => FormElement.FindChildElement<ILabel>(By.XPath("//div[contains(@class,'footer__subscribe')]"), "Subscribe");

        private static readonly TimeSpan ElementPresenceTimeout = NexusServices.Get<ICustomTimeoutConfiguration>().ElementAppear;

        public FooterForm() : base(By.TagName("footer"), "Footer form")
        {
        }

        public bool IsLogoPresent => LogoLabel.State.WaitForDisplayed(ElementPresenceTimeout);

        public bool IsContactsPresent => ContactsLabel.State.WaitForDisplayed(ElementPresenceTimeout);

        public bool IsSubscribePresent => SubscribeLabel.State.WaitForDisplayed(ElementPresenceTimeout);

        protected override IDictionary<string, IElement> ElementsForVisualization => new Dictionary<string, IElement>()
        {
            {"FooterLogo", LogoLabel },
            {"FooterContacts", ContactsLabel },
            {"FooterSubscribe", SubscribeLabel },
        };
    }
}
