﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.EmailCampaigns
{
    /// <summary>
    /// DeleteSelectedMailListAndPageInSubscribeFormWidget test class.
    /// </summary>
    [TestClass]
    public class DeleteSelectedMailListAndPageInSubscribeFormWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DeleteSelectedMailListAndPageInSubscribeFormWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.EmailCampaigns),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void DeleteSelectedMailListAndPageInSubscribeFormWidget()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().EmailCampaigns().SubscribeFormWrapper().SelectItemsInFlatSelector(MailingList);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().EmailCampaigns().SubscribeFormWrapper().SelectExistingPage();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton(1);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(FirstPageName);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Arrange(this.TestName).ExecuteArrangement("DeleteMailingListAndPage");

            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().EmailCampaigns().SubscribeFormWrapper().VerifyErrorMessageForDeletedMailList();
            BATFeather.Wrappers().Backend().EmailCampaigns().SubscribeFormWrapper().VerifyErrorMessageForDeletedPage();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().EmailCampaigns().SubscribeFormWrapper().SelectItemsInFlatSelector(SecondMailingList);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().EmailCampaigns().SubscribeFormWrapper().SelectExistingPage();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton(1);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(SecondPageName);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture); 
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().VerifySubscribeMessageOnTheFrontend();
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().FillEmail(SubscriberEmail);
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().ClickSubscribeButton();
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(ContentBlockContent);
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

        private const string PageName = "SubscribeFormPage";
        private const string MailingList = "MailList";
        private const string SecondMailingList = "SecondMailList";
        private const string SubscriberEmail = "testNew@email.com";
        private const string WidgetName = "Subscribe form";
        private const string FirstPageName = "FirstPage";
        private const string SecondPageName = "SecondPage";
        private const string ContentBlockContent = "You are redirect to second page";
    }
}
