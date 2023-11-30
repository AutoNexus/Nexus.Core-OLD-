﻿using Nexus.Selenium.Browsers;
using Nexus.Selenium.Configurations;
using Nexus.Core.Applications;
using Nexus.Core.Configuration;
using Nexus.Core.Elements;
using Nexus.Core.Localization;
using Nexus.Core.Utilities;
using Nexus.Core.Visualization;
using Nexus.Core.Waitings;
using Nexus.Selenium.Elements.Actions;
using Nexus.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System.Reflection;
using System.Linq;
using CoreElement = Nexus.Core.Elements.Element;
using ICoreElementFactory = Nexus.Core.Elements.Interfaces.IElementFactory;
using ICoreElementFinder = Nexus.Core.Elements.Interfaces.IElementFinder;
using ICoreElementStateProvider = Nexus.Core.Elements.Interfaces.IElementStateProvider;

namespace Nexus.Selenium.Elements
{
    /// <summary>
    /// Defines base class for any UI element.
    /// </summary>
    public abstract class Element : CoreElement, IElement
    {
        protected Element(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        public override ICoreElementStateProvider State => new ElementStateProvider(Locator, ConditionalWait, Finder, LogElementState);

        protected IBrowserProfile BrowserProfile => NexusServices.Get<IBrowserProfile>();

        public JsActions JsActions => new JsActions(this, ElementType, LocalizedLogger, BrowserProfile);

        public MouseActions MouseActions => new MouseActions(this, ElementType, LocalizedLogger, ActionRetrier);

        private Browser Browser => (Browser)Application;

        protected override IApplication Application => NexusServices.Browser;

        protected override IElementActionRetrier ActionRetrier => NexusServices.Get<IElementActionRetrier>();

        protected override ICoreElementFactory Factory => CustomFactory;

        protected virtual IElementFactory CustomFactory => NexusServices.Get<IElementFactory>();

        protected internal virtual ICoreElementFinder CustomFinder { get; internal set; } = NexusServices.Get<ICoreElementFinder>();

        protected override ICoreElementFinder Finder => CustomFinder;

        protected override IElementCacheConfiguration CacheConfiguration => NexusServices.Get<IElementCacheConfiguration>();

        protected override ILocalizedLogger LocalizedLogger => NexusServices.LocalizedLogger;

        protected override ILocalizationManager LocalizationManager => NexusServices.Get<ILocalizationManager>();

        protected override IConditionalWait ConditionalWait => NexusServices.ConditionalWait;

        protected override IImageComparator ImageComparator => NexusServices.Get<IImageComparator>();

        public void ClickAndWait()
        {
            Click();
            Browser.WaitForPageToLoad();
        }

        public void WaitAndClick()
        {
            State.WaitForClickable();
            Click();
        }

        public new void Click()
        {
            LogElementAction("loc.clicking");
            JsActions.HighlightElement();
            DoWithRetry(() => GetElement().Click());
        }

        public void Focus()
        {
            LogElementAction("loc.focusing");
            JsActions.SetFocus();
        }

        public string GetAttribute(string attr, HighlightState highlightState = HighlightState.Default)
        {
            LogElementAction("loc.el.getattr", attr);
            JsActions.HighlightElement(highlightState);
            var value = DoWithRetry(() => GetElement().GetAttribute(attr));
            LogElementAction("loc.el.attr.value", attr, value);
            return value;
        }

        public string GetCssValue(string propertyName, HighlightState highlightState = HighlightState.Default)
        {
            LogElementAction("loc.el.cssvalue", propertyName);
            JsActions.HighlightElement(highlightState);
            var value = DoWithRetry(() => GetElement().GetCssValue(propertyName));
            LogElementAction("loc.el.attr.value", propertyName, value);
            return value;
        }

        public string GetText(HighlightState highlightState = HighlightState.Default)
        {
            LogElementAction("loc.get.text");
            JsActions.HighlightElement(highlightState);
            var value = DoWithRetry(() => GetElement().Text);
            LogElementAction("loc.text.value", value);
            return value;
        }

        public void SetInnerHtml(string value)
        {
            Click();
            LogElementAction("loc.send.text", value);
            Browser.ExecuteScript(JavaScript.SetInnerHTML, GetElement(), value);
        }

        public void SendKey(Key key)
        {
            LogElementAction("loc.text.sending.key", key);
            var keysString = typeof(Keys)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(field => field.Name == key.ToString())?.GetValue(null).ToString();
            DoWithRetry(() => GetElement().SendKeys(keysString));
        }

        public ShadowRoot ExpandShadowRoot()
        {
            LogElementAction("loc.shadowroot.expand");
            var shadowRoot = (ShadowRoot)GetElement().GetShadowRoot();
            return shadowRoot;
        }

        public T FindElementInShadowRoot<T>(By locator, string name, ElementSupplier<T> supplier = null, ElementState state = ElementState.Displayed)
            where T : IElement
        {
            var shadowRootRelativeFinder = new RelativeElementFinder(LocalizedLogger, ConditionalWait, ExpandShadowRoot);
            var shadowRootFactory = new ElementFactory(ConditionalWait, shadowRootRelativeFinder, LocalizationManager);
            return shadowRootFactory.Get(locator, name, supplier, state);
        }
    }
}
