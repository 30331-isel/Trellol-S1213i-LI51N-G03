using EntitiesLogic.Entities;
using Logic;
using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Trellol.Infrastructure.Filters;
using Trellol.Models;

namespace Trellol.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (TrellolUserProvider.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                User user = new User() { 
                    Username = model.UserName, 
                    Password = model.Password, 
                    Email = model.Email,
                    isConfirmed = false
                };

                if (TrellolUserProvider.CreateUser(user))
                {
                    //Set cookie
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);

                    // Send confirmation email
                    String host = HttpContext.Request.Url.Host;
                    int port = HttpContext.Request.Url.Port;
                    Security.SecurityUtils.SendConfirmationEmail(host, port, user, "trellol.pi.1213@gmail.com");

                    // Add user to role
                    TrellolRoleProvider.AddUsersToRoles(new string[]{user.Username}, new string[]{TrellolRoleProvider.UnconfirmedUser});
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to create the user.");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authentication]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authentication]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;

                changePasswordSucceeded = TrellolUserProvider.ChangePassword(
                    User.Identity.Name, 
                    model.OldPassword, 
                    model.NewPassword);

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult Validate(String name, int passHash)
        {
            User user = TrellolUserProvider.GetUser(name);
            if (user == null || passHash != user.Password.GetHashCode())
                return View("Error");
            TrellolUserProvider.UnlockUser(name);
            return View();
        }

        //
        // GET: /Account/Delete/5

        public ActionResult Delete(string id)
        {
            return View(TrellolUserProvider.GetUser(id));
        }

        //
        // POST: /Account/Delete/5

        [HttpPost]
        public ActionResult Delete(string id, User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TrellolUserProvider.DeleteUser(user);

                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    return View(user);
                }
            }
            return View(user);

        }

    }
}
