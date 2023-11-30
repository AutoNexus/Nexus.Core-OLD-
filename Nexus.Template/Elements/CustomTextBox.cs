using Nexus.Core.Elements;
using Nexus.Selenium.Elements;
using OpenQA.Selenium;

namespace Nexus.Template.Elements
{
    public class CustomTextBox : TextBox
    {
        public CustomTextBox(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        public new string Text => Value;
    }
}
