using Nexus.Core.Elements;
using Nexus.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Nexus.Selenium.Elements
{
    /// <summary>
    /// Defines Label UI element.
    /// </summary>
    public class Label : Element, ILabel
    {
        protected internal Label(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.label");
    }
}
