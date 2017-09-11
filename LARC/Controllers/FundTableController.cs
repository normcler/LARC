using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LARC.Models;

namespace LARC.Controllers
{
  public class FundTableController : Controller
  {
    private LARC_DBEntities db = new LARC_DBEntities();

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
    // GET: FundTable
    public ActionResult Index(string id)
    {
      FundTable t = new FundTable(id, db);
      return View(t);
    }
  }
}