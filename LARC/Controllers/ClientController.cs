using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LARC.Models;

namespace LARC.Controllers
{
  public class ClientController : Controller
  {

    private LARC_DBEntities db = new LARC_DBEntities();

    // GET: Client
    public ActionResult Index(int? id)
    {
      Portfolio model = null;
      if(id == null)
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
          //  Note how this works: The portfolioID is the id of a portfolio in
          //  PortfolioDB table. We obtain the database record corresponding to
          //  this id. The PortfolioDB class has a name and a collection of
          //  objects of class PortfolioFund. This class was formed from a join
          //  table in the database that was created by the entity framework.
          //  The PortfolioFund class comprises the fund symbol and the number
          //  of shares.

          //  Also note that this is calling one of the constructors of the
          //  Portfolio class. This causes all the data to be imported.
          var dbRecord = db.PortfolioDBs.Find(portfolioID);
          model = new Portfolio(dbRecord.Name,
            dbRecord.PortfolioFunds.Select(x => new PortfolioHolding
            {
              Symbol = x.FundSymbol,
              NumberOfShares = x.NumberOfShares ?? 0,
              Name = x.Fund.Name
            }).ToList());
        }
      }
      else
      {
        var dbRecord = db.PortfolioDBs.Find(id);
        model = new Portfolio(dbRecord.Name, 
          dbRecord.PortfolioFunds.Select(x => new PortfolioHolding
          {
            Symbol = x.FundSymbol,
            NumberOfShares = x.NumberOfShares ?? 0
          }).ToList());
      }
      return View(model);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    /// <summary>
    ///   Create an object of class Client for testing.
    /// </summary>

    [NonAction]
    public Client CreateTestClient ()
    {
      Client testClient = new Client();
      testClient.ClientAccount = new Account();
      List<PortfolioHolding> portfolioHoldingList = 
        PortfolioHoldingController.GetTestList();
      testClient.ClientPortfolio =
        new Portfolio("Test Portfolio", portfolioHoldingList);
      return testClient;
    }
  }
}