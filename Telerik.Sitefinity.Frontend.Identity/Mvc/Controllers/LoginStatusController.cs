﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Login Status widget.
    /// </summary>
    [Localization(typeof(LoginStatusResources))]
    [ControllerToolboxItem(Name = "LoginStatus_MVC", Title = "Login / Logout button", SectionName = "Login", CssClass = LoginStatusController.WidgetIconCssClass)]
    public class LoginStatusController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets the Login Status widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual ILoginStatusModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();

                return this.model;
            }
        }

        /// <summary>
        /// Gets or sets the name of the template that widget will be displayed.
        /// </summary>
        /// <value></value>
        public string TemplateName
        {
            get
            {
                return this.templateName;
            }

            set
            {
                this.templateName = value;
            }
        }

        /// <summary>
        /// Gets the is design mode.
        /// </summary>
        /// <value>The is design mode.</value>
        protected virtual bool IsDesignMode
        {
            get
            {
                return SystemManager.IsDesignMode;
            }
        }

        #endregion

        #region Actions

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="TemplateName" />
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        [RelativeRoute("")]
        public ActionResult Index()
        {
            var fullTemplateName = this.templateNamePrefix + this.TemplateName;
            var viewModel = this.Model.GetViewModel();

            this.ViewBag.IsDesignMode = SystemManager.IsDesignMode;

            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Sign out the user and redirect it to the login page.
        /// </summary>
        /// <returns>
        /// /// The <see cref="ActionResult" />.
        /// </returns>
        [RelativeRoute("SignOut")]
        public ActionResult SignOut()
        {            
            IOwinContext owimContext = SystemManager.CurrentHttpContext.Request.GetOwinContext();
            owimContext.Authentication.SignOut();
           
            return this.Redirect(this.GetCurrentPageUrl());
        }

        /// <summary>
        /// Returns JSON with the status of the user and his email, first and last names
        /// </summary>
        [Route("rest-api/login-status")]
        public JsonResult Status()
        {
            var response = this.Model.GetStatusViewModel();
            
            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <inheritDocs/>
        protected override void HandleUnknownAction(string actionName)
        {
            this.Index().ExecuteResult(this.ControllerContext);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="ILoginStatusModel"/>.
        /// </returns>
        private ILoginStatusModel InitializeModel()
        {
            var parameters = new Dictionary<string, object>() 
            {
                { "currentPageUrl", this.GetCurrentPageUrl() }
            };

            return ControllerModelFactory.GetModel<ILoginStatusModel>(this.GetType(), parameters);
        }

        #endregion

        #region Private fields and constants

        internal const string WidgetIconCssClass = "sfLoginNameIcn sfMvcIcn";

        private string templateName = "LoginButton";
        private ILoginStatusModel model;
        private string templateNamePrefix = "LoginStatus.";

        #endregion
    }
}
