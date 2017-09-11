using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using LARC.Models;

namespace LARC.Controllers
{
  public class AccountController : Controller
  {
    // GET: Account
    public ActionResult Index()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult ForgotPassword(string email)
    {
      if (ModelState.IsValid)
      {
        var manager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
        IdentityUser user = manager.FindByEmail(email);
        string resetToken = manager.GeneratePasswordResetToken(user.Id);

        string sendGridApiKey = System.Configuration.ConfigurationManager
          .AppSettings["SendGrid.ApiKey"];

        SendGrid.SendGridClient client = new SendGrid.SendGridClient(sendGridApiKey);

        SendGrid.Helpers.Mail.SendGridMessage message =
          new SendGrid.Helpers.Mail.SendGridMessage();

        message.AddTo(email);
        message.Subject = "Reset Your AARC Password";
        message.SetFrom("no-reply@AARC.com", "AARC Administrator");
        string body = string.Format("<a href=\"{0}/account/resetpassword?email{1}&token={2}\">Reset your password</a>",
          Request.Url.GetLeftPart(UriPartial.Authority),
          email,  
          resetToken);
        message.AddContent("text/html", body);

        // this moves the templating out of code her to SendGrid.
        // The ID. is listed on the SendGrid template.
        message.SetTemplateId("bbaeb90c - ea12 - 4712 - b09b - db98ac12d6ea");

        // adding the .Result causes the async
        var response = client.SendEmailAsync(message).Result;
        // this allows you to look at the result if there are errors.
        var responseBody = response.Body.ReadAsStringAsync().Result;

        return RedirectToAction("ForgotPasswordSent");
      }
      return View();
    }

    // Get:

    public ActionResult SignIn()
    {
      return View();
    }

    // Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult SignIn(string username, string password,
      bool? rememberMe, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
        IdentityUser user = userManager.FindByName(username);

        if (user != null)
        {
          if (userManager.CheckPassword(user, password))
          {
            var userIdentity = userManager.
              CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties
            {
              IsPersistent = rememberMe ?? false,
              ExpiresUtc = DateTime.UtcNow.AddDays(7)
            },
            userIdentity);

            // This can be used to go back to a page where the user
            // lacked authorization, was redirected here. The system
            // sets up the returnUrl.
            if (!string.IsNullOrEmpty(returnUrl))
            {
              return Redirect(returnUrl);
            }
            else
            {
              return RedirectToAction("Index", "Home");
            }
          }
        }
      }

      return View();
    }

    public ActionResult SignOut()
    {
      var authenticationManager = HttpContext.GetOwinContext().Authentication;
      authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
      return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult ResetPassord (string email, string token, string password)
    {
      if (ModelState.IsValid)
      {
        var manager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
        IdentityUser user = manager.FindByEmail(email);
        IdentityResult result = manager.ResetPassword(user.Id, token, password);
        if (result.Succeeded)
        {
          TempData["PasswordReset"] = "Your password has been reset successfully";
          return RedirectToAction("SignIn");
        }
      }
      return View();
    }

    // GET: Register
    public ActionResult Register()
    {
      return View();
    }

    // POST: Register
    [HttpPost]
    public ActionResult Register(SubscribeViewModel model)
    {
      // Include these at the top:
      // using Microsoft.AspNet.Identity;
      // using Microsoft.AspNet.Identity.EntityFramework;
      // using Microsoft.AspNet.Identity.EntityFramework;

      var manager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();

      IdentityUser newuser = new IdentityUser(model.Email);
      newuser.Email = model.Email;
      //newuser.FirstName = model.FirstName;
      //newuser.LastName = model.LastName;
      // This can fail -
      // password might not be complex enough

     IdentityResult result = manager.Create(newuser, model.Password);
      if (!result.Succeeded)
      {
        ViewBag.Errors = result.Errors;
        return View();
      }

      //If the user creation succeeded, use the code below to set the sign-in cookie:
      //TODO: Most sites require you to confirm email before letting you sign-in.

      var authenticationManager = HttpContext.GetOwinContext().Authentication;

      var userIdentity = manager.CreateIdentity(newuser,
          DefaultAuthenticationTypes.ApplicationCookie);

      authenticationManager.SignIn(
          new Microsoft.Owin.Security.AuthenticationProperties(),
          userIdentity);

      return RedirectToAction("Index", "Home");
    }
  }
}