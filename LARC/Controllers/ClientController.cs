﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LARC.Models;
using Morningstar.Importer;


namespace LARC.Controllers
{
  public class ClientController : Controller
  {

    private LARC_DBEntities db = new LARC_DBEntities();

    // GET: Client
    public ActionResult Index(int? id)
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