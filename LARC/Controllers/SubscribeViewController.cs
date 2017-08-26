using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LARC.Models;

namespace LARC.Controllers
{
    /// <summary>
    ///     class for the subscribe page.
    /// </summary>
    public class SubscribeViewController : Controller
    {
        // GET: SubscribeView
        public ActionResult Subscribe() => View();

        // POST:
        /// <summary>
        ///     Add a new subscriber.
        /// </summary>
        /// <returns></returns>
        /*[HttpPost]
        public ActionResult AddAccount()
        {
            Account newAccount = new Account();            
        }*/
    }

}