﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.EmailCampaigns
{
    /// <summary>
    /// This is the entry point class for subscribe form on the frontend.
    /// </summary>
    public class SubscibeFormWrapper : BaseWrapper
    {
        /// <summary>
        /// Verify subscribe message for mvc pure
        /// </summary>
        public void VerifySubscribeMessageOnTheFrontend()
        {
            HtmlForm subscribeForm = this.EM.EmailCampaigns.SubscribeFormFrontend.SubscribeFormPureMVC.AssertIsPresent("Subscribe form");
            bool isPresentSubscribe = subscribeForm.InnerText.Contains("Subscribe");
            Assert.IsTrue(isPresentSubscribe);

            bool isPresentMessage = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent().InnerText.Contains("Subscribe to our email newsletter to receive updates");
            Assert.IsTrue(isPresentMessage);
        }

        /// <summary>
        ///  Verify successfully subscribed message for mvc pure
        /// </summary>
        /// <param name="email">Email</param>
        public void VerifySuccessfullySubscribeMessageOnTheFrontend(string email)
        {
            HtmlForm subscribeForm = this.EM.EmailCampaigns.SubscribeFormFrontend.SubscribeFormPureMVC.AssertIsPresent("Subscribe form");
            bool isPresentSubscribe = subscribeForm.InnerText.Contains("Thank you. You have successfully subscribed to our newsletter (" + email + ")");
            Assert.IsTrue(isPresentSubscribe);
        }

        /// <summary>
        /// Verify subscribe message for hybrid page
        /// </summary>
        public void VerifySubscribeMessageOnTheFrontendHybrid()
        {
            HtmlDiv subscribeForm = this.EM.EmailCampaigns.SubscribeFormFrontend.SubscribeForm.AssertIsPresent("Subscribe form");
            bool isPresentSubscribe = subscribeForm.InnerText.Contains("Subscribe");
            Assert.IsTrue(isPresentSubscribe);

            bool isPresentMessage = subscribeForm.InnerText.Contains("Subscribe to our email newsletter to receive updates");
            Assert.IsTrue(isPresentMessage);
        }

        /// <summary>
        ///  Verify successfully subscribed message for hybrid page
        /// </summary>
        /// <param name="email">Email</param>
        public void VerifySuccessfullySubscribeMessageOnTheFrontendHybrid(string email)
        {
            HtmlDiv subscribeForm = this.EM.EmailCampaigns.SubscribeFormFrontend.SubscribeForm.AssertIsPresent("Subscribe form");
            bool isPresentSubscribe = subscribeForm.InnerText.Contains("Thank you. You have successfully subscribed to our newsletter (" + email + ")");

            Assert.IsTrue(isPresentSubscribe);
        }

        /// <summary>
        /// Fill user email
        /// </summary>
        /// <param name="firstName">Email address</param>
        public void FillEmail(string email)
        {
            HtmlInputText emailInput = EM.EmailCampaigns.SubscribeFormFrontend.EmailAddressField
                .AssertIsPresent("Email field");

            emailInput.Text = string.Empty;
            emailInput.Text = email;
        }

        /// <summary>
        /// Press subscribe button
        /// </summary>
        public void ClickSubscribeButton()
        {
            HtmlButton subscribeButton = EM.EmailCampaigns.SubscribeFormFrontend.SubscribeButton
            .AssertIsPresent("Subscribe button");
            subscribeButton.MouseClick();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }

        /// <summary>
        /// Verify subscribe message not styled page
        /// </summary>
        public void VerifySubscribeMessageOnTheFrontendNotStyledPage()
        {
            HtmlDiv subscribeDiv = this.EM.EmailCampaigns.SubscribeFormFrontend.SubscribeDiv.AssertIsPresent("Subscribe form");
            bool isPresentSubscribe = subscribeDiv.InnerText.Contains("Subscribe");
            Assert.IsTrue(isPresentSubscribe);

            bool isPresentMessage = subscribeDiv.InnerText.Contains("Subscribe to our email newsletter to receive updates");
            Assert.IsTrue(isPresentMessage);
        }

        /// <summary>
        ///  Verify successfully subscribed message not styled page
        /// </summary>
        /// <param name="email">Email</param>
        public void VerifySuccessfullySubscribeMessageOnTheFrontendNotStyledPage(string email)
        {
            HtmlDiv subscribeDiv = this.EM.EmailCampaigns.SubscribeFormFrontend.SubscribeDiv.AssertIsPresent("Subscribe form");
            bool isPresentSubscribe = subscribeDiv.InnerText.Contains("Thank you. You have successfully subscribed to our newsletter (" + email + ")");
            Assert.IsTrue(isPresentSubscribe);
        }

        /// <summary>
        /// Fill user email not styled page
        /// </summary>
        /// <param name="firstName">Email address</param>
        public void FillEmailNotStyledPage(string email)
        {
            HtmlInputText emailInput = EM.EmailCampaigns.SubscribeFormFrontend.EmailAddressTextField
                .AssertIsPresent("Email field");

            emailInput.Text = string.Empty;
            emailInput.Text = email;
        }

        /// <summary>
        /// Press subscribe button not styled page
        /// </summary>
        public void ClickSubscribeButtonNotStyledPage()
        {
            HtmlInputSubmit subscribeButton = EM.EmailCampaigns.SubscribeFormFrontend.SubscribeInputButton
            .AssertIsPresent("Subscribe button");
            subscribeButton.MouseClick();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }
    }
}
