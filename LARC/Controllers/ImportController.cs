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
    public ActionResult Index(HttpPostedFileBase file, string symbol)
    {
      string fileName = System.IO.Path.GetFileName(file.FileName);
      System.IO.Directory.CreateDirectory(Server.MapPath("~/Temp"));
      file.SaveAs(Server.MapPath("~/Temp/" + fileName));

      // This brings in a list of equities from the .csv file. 
      // Each contains many properties as described in the Holding class in the
      // Morningstar Importer.
      var equities = Morningstar.Importer.FundFileImporter.Import(Server.MapPath("~/Temp/" + fileName), symbol);

      // We are adding an object of class Fund.
      // It contains the following properties:
      // string Symbol - the fund symbol.
      // ICollection<FundEquity> - collection of class FundEquity.
      db.Funds.Add(new Fund
      {
        Symbol = symbol, 

        FundEquities = equities.Where(x=> x.Name != "-" && 
        !db.Equities.Select(y => y.Name).Contains(x.Name)).Select(x => new FundEquity
        {
          // Shares - shares owned by the mutual fund, not the number of 
          //          shares in the portfolio.
          // Weighting - the weighting of the equity in the fund.
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