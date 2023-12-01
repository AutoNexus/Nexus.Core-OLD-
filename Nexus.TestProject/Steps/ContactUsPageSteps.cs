using Newtonsoft.Json;
using Nexus.Core.Utilities;
using Nexus.Selenium.Browsers;
using Nexus.Selenium.Configurations;
using Nexus.Template.CustomAttributes;
using Nexus.Template.Enums;
using Nexus.Template.Extensions;
using Nexus.Template.Forms_PageFolder_.Pages;
using Nexus.Template.Models;
using Nexus.TestProject.Constants;
using Nexus.TestProject.Extensions;
using System.Reflection;

namespace Nexus.TestProject.Steps
{
    public class ContactUsPageSteps
    {
        private readonly ContactUsPage contactUsPage = new ContactUsPage();
        private readonly ContactUsInfo contactUsInfo = JsonConvert.DeserializeObject<ContactUsInfo>(FileReader.GetTextFromEmbeddedResource(ResourceConstants.PathToContactUserWithInvalidEmail, Assembly.GetCallingAssembly()));

        [LogStep(StepType.Assertion)]
        public void ContactUsPageIsPresent()
        {
            contactUsPage.AssertIsPresent();
        }

        [LogStep(StepType.Assertion)]
        public void CheckThatTheContactUsFormElementsAreDisplayed()
        {
            Assert.Multiple(() =>
            {
                foreach (ContactUsTextFields name in Enum.GetValues(typeof(ContactUsTextFields)))
                {
                    Assert.IsTrue(contactUsPage.IsContactUsTextBoxPresent(name), $"Text field {name} should be displayed");
                }
                 Assert.IsTrue(contactUsPage.IsTermsCheckBoxExist, "Terms checkBox should be exist");
                 Assert.IsTrue(contactUsPage.IsTermsLabelPresent, "Terms label should be displayed");
                 Assert.IsTrue(contactUsPage.IsSendAMessageButtonPresent, "Send a message button should be displayed");
                 Assert.IsTrue(contactUsPage.IsTitleLabelPresent, "Title should be displayed");
            });
        }

        [LogStep(StepType.Assertion)]
        public void CheckThanContactUsTitleIsCorrect()
        {
             Assert.AreEqual(contactUsPage.TitleLabelTextValue, TitleConstants.TitleLabelText, "Title text should be same.");
        }

        [LogStep(StepType.Step)]
        public void ClickSendAMessageButton()
        {
            contactUsPage.ClickSend();
        }

        [LogStep(StepType.Step)]
        public void CheckTermCheckBox()
        {
            contactUsPage.CheckTermsCheckBox();
        }

        [LogStep(StepType.Step)]
        public void CheckTermCheckBoxIsCheckedOrNot(bool isChecked = false)
        {
            var expectedStatus = isChecked ? "checked" : "not checked";
            Assert.AreEqual(contactUsPage.IsTermsCheckBoxChecked, isChecked, $"Term CheckBox should be {expectedStatus}");
        }

        [LogStep(StepType.Step)]
        public void SetValueForTheTextField(ContactUsTextFields textField, string value)
        {
            contactUsPage.SetValueForContactUsTextBox(textField, value);
        }

        [LogStep(StepType.Step)]
        public void SetDataForTheAllTextFields()
        {
            SetValueForTheTextField(ContactUsTextFields.Name, contactUsInfo.Name);
            SetValueForTheTextField(ContactUsTextFields.Email, contactUsInfo.Email);
            SetValueForTheTextField(ContactUsTextFields.Company, contactUsInfo.Company);
            SetValueForTheTextField(ContactUsTextFields.ProjectDescription, contactUsInfo.Comment);
        }

        [LogStep(StepType.Assertion)]
        public void CheckThatWarningEmailMessageisPresentOrNot(bool isChecked = false)
        {
            var expectedStatus = isChecked ? "displayed" : "not displayed";
            NexusServices.ConditionalWait.WaitForTrue(() => contactUsPage.IsWarningEmailMessagePresent == isChecked,
            NexusServices.Get<ITimeoutConfiguration>().Script, NexusServices.Get<ITimeoutConfiguration>().PollingInterval,
                $"Warning email message should be {expectedStatus}.");
        }

        [LogStep(StepType.Step)]
        public void CheckThatWarningEmailMessageIsCorrect()
        {
             Assert.AreEqual(contactUsPage.WarningEmailMessageTextValue, ContactUsTextFields.Email.GetEnumDescription(), "Warning email message should be correct.");
        }
    }
}
