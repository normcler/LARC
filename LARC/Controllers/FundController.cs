using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LARC.Models;

namespace LARC.Controllers
{
    public class FundController : Controller
  {
    // this is in lieu of a constructor.
    LARC_DBEntities db = new LARC_DBEntities();

    // Type override and hit tab
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    // GET: Fund
    public ActionResult Index()
    {
      return View(db.Funds);
    }

    // GET: Details
    public ActionResult Details(string id)
    {
      return View(db.Funds.Find(id));
    }
  }
}