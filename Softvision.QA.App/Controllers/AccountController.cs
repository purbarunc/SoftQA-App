using Softvision.QA.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;

namespace Softvision.QA.App.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            using (HostingEnvironment.Impersonate())
            {
                if (ModelState.IsValid)
                {
                    // Check with AD
                    if (Membership.ValidateUser(model.UserName, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, true);
                        Session["username"] = model.UserName;
                        if (this.Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return this.Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }

                // If we got this far, something failed, redisplay form
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}