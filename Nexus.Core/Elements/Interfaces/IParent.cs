using OpenQA.Selenium;

namespace Nexus.Core.Elements.Interfaces
{
    /// <summary>
    /// Defines behavior of element with child elements.
    /// </summary>
    public interface IParent
    {
        //
        // Summary:
        //     Finds child element of current element by its locator.
        //
        // Parameters:
        //   childLocator:
        //     Locator of child element.
        //
        //   name:
        //     Child element name.
        //
        //   supplier:
        //     Delegate that defines constructor of child element in case of custom element.
        //
        //
        //   state:
        //     Child element state.
        //
        // Type parameters:
        //   T:
        //     Type of child element that has to implement IElement.
        //
        // Returns:
        //     Instance of child element.
        T FindChildElement<T>(By childLocator, string name = null, ElementSupplier<T> supplier = null, ElementState state = ElementState.Displayed) where T : IElement;

        //
        // Summary:
        //     Finds child elements of current element by its locator.
        //
        // Parameters:
        //   childLocator:
        //     Locator of child elements relative to their parent.
        //
        //   name:
        //     Child elements name.
        //
        //   supplier:
        //     Delegate that defines constructor of child element in case of custom element
        //     type.
        //
        //   expectedCount:
        //     Expected number of elements that have to be found (zero, more then zero, any).
        //
        //
        //   state:
        //     Child elements state.
        //
        // Type parameters:
        //   T:
        //     Type of child elements that has to implement IElement.
        //
        // Returns:
        //     List of child elements.
        IList<T> FindChildElements<T>(By childLocator, string name = null, ElementSupplier<T> supplier = null, ElementsCount expectedCount = ElementsCount.Any, ElementState state = ElementState.Displayed) where T : IElement;
    }
}
