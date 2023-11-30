using Nexus.Core.Elements.Interfaces;
using Nexus.Core.Logging;
using Nexus.Core.Waitings;
using OpenQA.Selenium;
using CoreElementStateProvider = Nexus.Core.Elements.ElementStateProvider;

namespace Nexus.Selenium.Elements
{
    public class ElementStateProvider : CoreElementStateProvider
    {
        public ElementStateProvider(By elementLocator, IConditionalWait conditionalWait, IElementFinder elementFinder, LogElementState logElementState)
            : base(elementLocator, conditionalWait, elementFinder, logElementState)
        {
        }

        protected override bool IsElementEnabled(IWebElement element)
        {
            return element.Enabled && !element.GetAttribute(Attributes.Class).Contains(PopularClassNames.Disabled);
        }
    }
}
