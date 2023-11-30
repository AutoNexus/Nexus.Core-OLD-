using OpenQA.Selenium;

namespace Nexus.Template.Forms_PageFolder_.Pages
{
    public class MainPage : BaseAppForm
    {
        public MainPage() : base(By.XPath("//section[contains(@class,'heroMain')]"), "Main page")
        {
        }
    }
}
