using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;


namespace LARC.Controllers
{
  public class AccountController : Controller
  {
    // GET: Account
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult SignOut()
    {
      var authenticationManager = HttpContext.GetOwinContext().Authentication;
      authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
      return RedirectToAction("Index", "Home");
    }

    // GET: Register
    public ActionResult Register()
    {
      return View();
    }

    // POST: Register
    [HttpPost]
    public ActionResult Register(string username, string password)
    {
      // Include these at the top:
      // using Microsoft.AspNet.Identity;
      // using Microsoft.AspNet.Identity.EntityFramework;
      // using Microsoft.AspNet.Identity.EntityFramework;

      var manager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();

      IdentityUser newuser = new IdentityUser(username);
      // This can fail -
      // password might not be complex enough

     IdentityResult result = manager.Create(newuser, password);
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