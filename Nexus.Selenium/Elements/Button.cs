using Nexus.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using ElementState = Nexus.Core.Elements.ElementState;

namespace Nexus.Selenium.Elements
{
    public class Button : Element, IButton
    {
        protected internal Button(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.button");
    }
}
