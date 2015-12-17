﻿using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.SDK.DevelopersGuide.SitefinityEssentials.Modules.Forms;
using Telerik.WebTestRunner.Server.Attributes;

namespace FeatherWidgets.TestIntegration.Forms.Fields
{
    /// <summary>
    /// This class ensures Section header functionalities work correctly.
    /// </summary>
    [TestFixture]
    [Description("This class ensures Section header functionalities work correctly.")]
    public class SectionHeaderFieldTests
    {
        /// <summary>
        /// Ensures that when a Section header field widget is added to form the default value is presented in the page markup.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Ignore("Form creation via Server operations feather does not include passed widget.")] 
        [Description("Ensures that when a Section header field widget is added to form the default value is presented in the page markup.")]
        public void SectionHeader_MarkupIsCorrect()
        {
            var controller = new SectionHeaderController();
            controller.Model.Value = "Hello";
            var control = new MvcWidgetProxy();
            control.ControllerName = typeof(SectionHeaderController).FullName;
            control.Settings = new ControllerSettings(controller);

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "SectionHeaderFieldValueTest", "section-header-field-value-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);

                var pageContent = ServerOperationsFeather.Pages().GetPageContent(pageId);

                Assert.IsTrue(pageContent.Contains("Hello"), "Form did not render section header");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }
    }
}
