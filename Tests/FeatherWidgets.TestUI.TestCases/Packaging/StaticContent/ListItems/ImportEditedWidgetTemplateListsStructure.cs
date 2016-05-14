﻿using System;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Framework.Utilities;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.Packaging.StaticContent
{
    /// <summary>
    /// Import edited widget template for lists structure
    /// </summary>
    [TestClass]
    public class ImportEditedWidgetTemplateListsStructure_ : FeatherTestCase
    {
        /// <summary>
        /// Import edited widget template for lists structure
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Packaging)]
        public void ImportEditedWidgetTemplateListsStructure()
        {
            BAT.Macros().NavigateTo().Design().WidgetTemplates();
            this.VerifyWidgetTemplates(this.widgetTemplatesNames, EditedWidgetTemplate, AreaName);
        }

        /// <summary>
        /// Forces calling initialize methods that will prepare test with data and resources. This method must be overridden if you want
        /// in your test case.
        /// </summary>
        protected override void ServerSetup()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(300000, () => BAT.Macros().User().EnsureAdminLoggedIn());
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Forces cleanup of the test data. This method is thrown if test setup fails. This method must be overridden in your test case.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        /// <summary>
        /// Verifies the widget templates.
        /// </summary>
        /// <param name="widgetTemplatesName">Name of the widget templates.</param>
        /// <param name="content">The content.</param>
        /// <param name="areaName">Name of the area.</param>
        private void VerifyWidgetTemplates(string[] widgetTemplatesName, string content, string areaName)
        {
            for (int i = 0; i < widgetTemplatesName.Length; i++)
            {
                BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().OpenWidgetTemplateByAreaAndName(widgetTemplatesName[i], areaName);
                BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().EditFrame.WaitForAsyncOperations();
                BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().VerifyWidgetTemplateContent(content);
                BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().ClickSaveChangesLink();
                Manager.Current.ActiveBrowser.WaitUntilReady();
                Manager.Current.ActiveBrowser.WaitForAsyncOperations();
            }
        }

        private const string EditedWidgetTemplate = "EDITED";
        private const string AreaName = @"Lists:";
        private string[] widgetTemplatesNames = new string[] 
                                                   { 
                                                        "Detail.DetailPageNewList", "List.AnchorListNew", "List.ExpandableListNew", 
                                                        "List.ExpandedListNew", "List.PagesListNew", "List.SimpleListNew"
                                                    };
    }
}
