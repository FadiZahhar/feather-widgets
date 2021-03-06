﻿using System;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.DynamicWidgets
{
    /// <summary>
    /// VerifyPagingOnFrontendPageForDynamicWidget_ test class.
    /// </summary>
    [TestClass]
    public class VerifyPagingOnFrontendPageForDynamicWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyPagingOnFrontendPageForDynamicWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.PagesAndContent), 
        TestCategory(FeatherTestCategories.DynamicWidgets)]
        public void VerifyPagingOnFrontendPageForDynamicWidget()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", true, null, new HtmlFindExpression("class=~sfMain")));
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.UsePaging);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ChangePagingOrLimitValue("2", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("2", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("20", "Limit");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.UsePaging);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("2", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("20", "Limit");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().PressCancelButton();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().CommonWrapper().AreTitlesPresentOnThePageFrontend(new string[] { DynamicTitle5, DynamicTitle4 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().CommonWrapper().AreTitlesPresentOnThePageFrontend(new string[] { DynamicTitle3, DynamicTitle2, DynamicTitle1 }));
            BATFeather.Wrappers().Frontend().CommonWrapper().NavigateToPageUsingPager("2", 3);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().CommonWrapper().AreTitlesPresentOnThePageFrontend(new string[] { DynamicTitle3, DynamicTitle2 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().CommonWrapper().AreTitlesPresentOnThePageFrontend(new string[] { DynamicTitle5, DynamicTitle4, DynamicTitle1 }));
            BATFeather.Wrappers().Frontend().CommonWrapper().NavigateToPageUsingPager("3", 3);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().CommonWrapper().AreTitlesPresentOnThePageFrontend(new string[] { DynamicTitle1 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().CommonWrapper().AreTitlesPresentOnThePageFrontend(new string[] { DynamicTitle5, DynamicTitle4, DynamicTitle3, DynamicTitle2 }));
            BATFeather.Wrappers().Frontend().CommonWrapper().NavigateToPageUsingPager("1", 3);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().CommonWrapper().AreTitlesPresentOnThePageFrontend(new string[] { DynamicTitle5, DynamicTitle4 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().CommonWrapper().AreTitlesPresentOnThePageFrontend(new string[] { DynamicTitle3, DynamicTitle2, DynamicTitle1 }));
            BAT.Macros().NavigateTo().Pages(this.Culture);
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

        private const string PageName = "TestPage";
        private const string WidgetName = "Press Articles MVC";
        private const string DynamicTitle1 = "Title1";
        private const string DynamicTitle2 = "Title2";
        private const string DynamicTitle3 = "Title3";
        private const string DynamicTitle4 = "Title4";
        private const string DynamicTitle5 = "Title5";
    }
}
