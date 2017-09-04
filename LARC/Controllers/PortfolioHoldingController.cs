using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LARC.Models;

namespace LARC.Controllers
{
    public class PortfolioHoldingController : Controller
    {
        // GET: PortfolioHolding
        public ActionResult Index()
        {
            return View();
        }

    /// <summary>
    ///   Create a list of class PortfolioHolding for testing.
    /// </summary>
    //  Note: This list will be read in off the DB in Client. This is just for testing.

    [NonAction]
    public static List<PortfolioHolding> GetTestList()
    {
      int numOfFunds = 12;
      PortfolioHolding[] testArray = new PortfolioHolding[numOfFunds];
      testArray[0] = new PortfolioHolding { Symbol = "FPMAX", NumberOfShares = 3766.487M };
      testArray[1] = new PortfolioHolding { Symbol = "FSEVX", NumberOfShares = 862.661M };
      testArray[2] = new PortfolioHolding { Symbol = "PRASX", NumberOfShares = 5046.534M };
      testArray[3] = new PortfolioHolding { Symbol = "PRHSX", NumberOfShares = 728.24M };
      testArray[4] = new PortfolioHolding { Symbol = "PRITX", NumberOfShares = 3928.275M };
      testArray[5] = new PortfolioHolding { Symbol = "PRNHX", NumberOfShares = 686.556M };
      testArray[6] = new PortfolioHolding { Symbol = "VEMAX", NumberOfShares = 2515.0M };
      testArray[7] = new PortfolioHolding { Symbol = "VEUSX", NumberOfShares = 237.304M };
      testArray[8] = new PortfolioHolding { Symbol = "VFSVX", NumberOfShares = 830.336M };
      testArray[9] = new PortfolioHolding { Symbol = "VPADX", NumberOfShares = 186.273M };
      testArray[10] = new PortfolioHolding { Symbol = "VTMSX", NumberOfShares = 693.775M };
      testArray[11] = new PortfolioHolding { Symbol = "VTRIX", NumberOfShares = 560.45M };
      return testArray.ToList();
    }
  }
}
