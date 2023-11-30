using Nexus.Core.Elements;
using OpenQA.Selenium;


namespace Nexus.Selenium.Elements
{
    public abstract class CheckableElement : Element
    {
        protected CheckableElement(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        public bool IsChecked
        {
            get
            {
                LogElementAction("loc.checkable.get.state");
                var state = GetState();
                LogElementAction("loc.checkable.state", state);
                return state;
            }
        }

        protected virtual bool GetState()
        {
            return DoWithRetry(() => GetElement().Selected);
        }
    }
}
