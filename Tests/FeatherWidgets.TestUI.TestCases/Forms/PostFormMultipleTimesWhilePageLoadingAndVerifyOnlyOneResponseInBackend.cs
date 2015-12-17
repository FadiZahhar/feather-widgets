﻿using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Forms
{
    /// <summary>
    /// PostFormMultipleTimesWhilePageLoadingAndVerifyOnlyOneResponseInBackend test class.
    /// </summary>
    [TestClass]
    public class PostFormMultipleTimesWhilePageLoadingAndVerifyOnlyOneResponseInBackend_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ViewFormWithTextboxFieldOnPageAndVerifyResponseInBackend
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms), Telerik.TestUI.Core.Attributes.KnownIssue()]
        public void PostFormMultipleTimesWhilePageLoadingAndVerifyOnlyOneResponseInBackend()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
            BAT.Macros().NavigateTo().CustomPage("~/" + FeatherGlobals.BootstrapPageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyCheckboxesFieldLabelIsVisible(FeatherGlobals.SelectAChoiceLabelName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyTextboxFieldContainerIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SetTextboxContent(TextBoxContent);

            // Simulate multiple clicks
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().MultipleSubmitForm(5);

            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().ViewFormResponses(FeatherGlobals.FormName);

            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyNumberOfResponses(ExpectedResponsesCount);
            BAT.Wrappers().Backend().Forms().FormsResponseScreen().SelectResponse(ResponseNumber);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseAuthorUsername(ExpectedAuthorName);
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseSubmitDate();
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyResponseTextboxAnswer(TextBoxContent);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string TextBoxContent = "Textbox Field Text";
        private const int ExpectedResponsesCount = 1;
        private const int ResponseNumber = 1;
        private const string ExpectedAuthorName = "admin";
        public const string FormName = "Register";
    }
}
