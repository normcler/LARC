﻿using System;
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
        //        in the database
        ClientDB clientDB = db.ClientDBs.Find(1);
        int? portfolioID = clientDB.PortfolioID ?? 0;
        var dbRecord = db.PortfolioDBs.Find(portfolioID);
        model = new Portfolio(dbRecord.Name,
          dbRecord.PortfolioFunds.Select(x => new PortfolioHolding
          {
            Symbol = x.FundSymbol,
            NumberOfShares = x.NumberOfShares ?? 0
          }).ToList());
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