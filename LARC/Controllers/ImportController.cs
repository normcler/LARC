using LARC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LARC.Controllers
{
  public class ImportController : Controller
  {
    // GET: Import
    public ActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Index(HttpPostedFileBase file, string ticker)
    {
      string fileName = System.IO.Path.GetFileName(file.FileName);
      System.IO.Directory.CreateDirectory(Server.MapPath("~/Temp"));
      file.SaveAs(Server.MapPath("~/Temp/" + fileName));

      var equities = Morningstar.Importer.FundFileImporter.Import(Server.MapPath("~/Temp/" + fileName), ticker);
      db.Funds.Add(new Fund
      {
        Symbol = ticker, 

        FundEquities = equities.Where(x=> x.Name != "-" && !db.Equities.Select(y => y.Name).Contains(x.Name)).Select(x => new FundEquity
        {
          Shares = x.SharesOwned ?? 0,
          Weighting = x.Weighting ?? 0,
          Equity = new Equity
          {
            Name = x.Name,
            Price = x.Price ?? 0,
            Symbol = x.Ticker
          }
        }).ToArray()
      });
      db.SaveChanges();


      //TODO: Save this fund to the database!
      return RedirectToAction("Index", "Home");

    }

    private LARC_DBEntities db = new LARC_DBEntities();


    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}