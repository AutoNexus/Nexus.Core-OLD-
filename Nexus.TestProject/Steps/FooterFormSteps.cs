﻿using Nexus.Template.CustomAttributes;
using Nexus.Template.Forms_PageFolder_;
using Nexus.TestProject.Extensions;

namespace Nexus.TestProject.Steps
{
    public class FooterFormSteps
    {
        private readonly FooterForm footerForm = new FooterForm();
        const float ComparisonThreshold = 0.1f;

        [LogStep(StepType.Assertion)]
        public void FooterFormIsPresent()
        {
            footerForm.AssertIsPresent();
        }

        [LogStep(StepType.Assertion)]
        public void CheckVisualElementsPresent()
        {
            Assert.Multiple(() =>
            {
                  Assert.IsTrue(footerForm.IsLogoPresent, "Logo should be displayed");
                  Assert.IsTrue(footerForm.IsContactsPresent, "Contacts section should be displayed");
                  Assert.IsTrue(footerForm.IsSubscribePresent, "Subscribe section should be displayed");
            });
        }

        [LogStep(StepType.Step)]
        public void SaveDump()
        {
            footerForm.Dump.Save();
        }

        [LogStep(StepType.Assertion)]
        public void CheckThatTheVisualElementsAreCorrect()
        {
            var compareVisualElements = footerForm.Dump.Compare();
            Assert.LessOrEqual(compareVisualElements, ComparisonThreshold, "The footer form should contain the correct visual elements");
        }
    }
}
