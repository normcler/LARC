using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LARC.Controllers
{
  public class PortfolioController : Controller
  {
    // GET: Portfolio
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult FundTable()
    {
      return RedirectToAction("FundTable", "index");
    }
  }
}