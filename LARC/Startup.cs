using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using LARC;

using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LARC.Models;

//This is an assembly decorator - it adds some metadata
//to the DLL to indicate what the startup class is called.
[assembly: OwinStartup(typeof(Startup))]

namespace LARC
{
  public class Startup
  {

    // My startup class contains a configuration method
    // which will "set-up" authentication for my app.
    public void Configuration(Owin.IAppBuilder app)
    {
      app.UseCookieAuthentication(new CookieAuthenticationOptions

      {
        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        LoginPath = new Microsoft.Owin.PathString("/Account/LogOn")
      });

      app.CreatePerOwinContext(() =>
      {
        UserStore<IdentityUser> store = new UserStore<IdentityUser>();
        UserManager<IdentityUser> manager = new UserManager<IdentityUser>(store);
        manager.UserTokenProvider = new EmailTokenProvider<IdentityUser>();
        manager.PasswordValidator = new PasswordValidator
        {
          RequiredLength = 4,
          RequireDigit = false,
          RequireUppercase = false,
          RequireLowercase = false,
          RequireNonLetterOrDigit = false
        };
        return manager;
      });
    }
  }
}