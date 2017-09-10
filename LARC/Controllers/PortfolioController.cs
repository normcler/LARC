using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LARC.Models;
using Morningstar.Importer;

namespace LARC.Controllers
{
  public class PortfolioController : Controller
  {
    private LARC_DBEntities db = new LARC_DBEntities();

    // GET: Portfolio
    public ActionResult Index(int? id)
    {
      Portfolio model = null;
      if (id == null)
      {
        // TODO:  The id here is canned to the value of 1.
        //        As soon as we have a login set up, we will look up the client
        //        in the database. The number here is 2 because we modified the
        //        database and the only client now has an id of 2.
        ClientDB clientDB = db.ClientDBs.Find(2);
        int? portfolioID = (clientDB.PortfolioID != null ?
          clientDB.PortfolioID : clientDB.PortfolioDBs.FirstOrDefault().ID);
        if (portfolioID != null)
        {
          model = BuildPortfolio(portfolioID);
        }
      }
      else
      {
        model = BuildPortfolio(id);
      }
      return View(model);
    }

    public Portfolio BuildPortfolio(int? id)
    {
      //  Note how this works: The portfolioID is the id of a portfolio in
      //  PortfolioDB table. We obtain the database record corresponding to
      //  this id. The PortfolioDB class has a name and a collection of
      //  objects of class PortfolioFund. This class was formed from a join
      //  table in the database that was created by the entity framework.
      //  The PortfolioFund class comprises the fund symbol and the number
      //  of shares.

      //  Also note that this is calling one of the constructors of the
      //  Portfolio class. This causes all the data to be imported.
      var dbRecord = db.PortfolioDBs.Find(id);
      List<PortfolioHolding> holdingList = 
        dbRecord.PortfolioFunds.Select(x => new PortfolioHolding
        {
          Symbol = x.FundSymbol,
          NumberOfShares = x.NumberOfShares ?? 0
        }).ToList();
      Portfolio model = new Portfolio(dbRecord.Name,
        holdingList);
        //dbRecord.PortfolioFunds.Select(x => new PortfolioHolding
        //{
        //  Symbol = x.FundSymbol,
        //  NumberOfShares = x.NumberOfShares ?? 0
        //}).ToList());
      Dictionary<string, List<Holding>> dictionary = model.PortfolioDictionary;
      foreach (string key in dictionary.Keys)
      {
        List<Holding> importEquities = dictionary[key];
        List<Holding> newEquities = new List<Holding>();

        // This did not work - why?
        //var newEquities = importEquities.Where(e => e.Ticker != "" &&
        //  !db.Equities.Select(y => y.Symbol).Contains(e.Ticker));
        var dbEquities = db.Equities;
        foreach (Holding equity in importEquities)
        {
          // Do not include this equity if it is already in the Equities
          // database or it has already been included in this list.
          // (An instance of the second occured in fund FPMAX: there were two 
          //  instances of equity SBER, a Russian bank called SBERBANK. The 
          //  equity names were very slightly different.)

          if (dbEquities.Any(e => e.Symbol == equity.Ticker) ||
             (newEquities.Any(n => n.Ticker == equity.Ticker)))
          {
            continue;
          }
          else
          {
            newEquities.Add(equity);
          }
        }
        List<FundEquity> fundEquityList = new List<FundEquity>();
        foreach (Holding equity in newEquities)
        {
          FundEquity fundEquity = new FundEquity
          {
            FundSymbol = key,
            EquitySymbol = equity.Ticker,
            Shares = equity.SharesOwned ?? 0,
            Weighting = equity.Weighting ?? 0,
            Equity = new Equity
            {
              Name = equity.Name,
              Price = equity.Price ?? 0,
              Symbol = equity.Ticker,
              Sector = equity.Sector,
              Currency = equity.Currency
            }
          };
          db.FundEquities.Add(fundEquity);
          fundEquityList.Add(fundEquity);
        }
        db.SaveChanges();
      }
      return model;
    }

    //[HttpPost]
    public ActionResult Edit ()
    {
      return View();
    }

    public ActionResult Create ()
    {
      return View();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    public ActionResult FundTable()
    {
      return RedirectToAction("FundTable", "index");
    }
  }
}