using Nexus.Core.Elements;
using Nexus.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Nexus.Selenium.Elements
{
    /// <summary>
    /// Defines Link UI element.
    /// </summary>
    public class Link : Element, ILink
    {
        protected internal Link(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.link");

        public string Href => GetAttribute("href");
    }
}
