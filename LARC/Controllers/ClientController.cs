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
    // GET: Client
    public ActionResult Index()
    {
      Client testClient = CreateTestClient();
      return View(testClient.ClientPortfolio);
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