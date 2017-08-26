using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LARC.Models;

namespace LARC.Controllers
{
    public class FundsOverlapTableController : Controller
    {
        private static List<HoldingOverlap> testOverlapList
            = new List<HoldingOverlap>();
        private static FundsOverlapTable testTable = new FundsOverlapTable();




        [NonAction]
        public static void InitializeTestTable()
        {
            testOverlapList.
                Add(new HoldingOverlap
                {
                    HoldingTicker = "BMRN",
                    HoldingName = "Biomarin Pharmaceutical Inc",
                    Overlap = 0.595M
                });
            testOverlapList.
                Add(new HoldingOverlap
                {
                    HoldingTicker = "ALGN",
                    HoldingName = "Align Technology Inc",
                    Overlap = 0.290M
                });
            testOverlapList.
                Add(new HoldingOverlap
                {
                    HoldingTicker = "TFX",
                    HoldingName = "Teleflex Inc",
                    Overlap = 0.605M
                });
            testOverlapList.
                Add(new HoldingOverlap
                {
                    HoldingTicker = "ALKS",
                    HoldingName = "Alkermes PLC",
                    Overlap = 0.605M
                });
            testOverlapList.
                Add(new HoldingOverlap
                {
                    HoldingTicker = "WCG",
                    HoldingName = "WellCare Health Plans Inc",
                    Overlap = 0.333M
                });
            testOverlapList.
                Add(new HoldingOverlap
                {
                    HoldingTicker = "WST",
                    HoldingName = "West Pharmaceutical Services Inc",
                    Overlap = 0.435M
                });

            testTable.FundSymbol_1 ="FSEVX";
            testTable.FundSymbol_2 = "PRHSX";
            testTable.OverlapList = testOverlapList;
        }
        // GET: FundsOverlapTable
        public ActionResult Index()
        {
            //  By passing an argument to the view method, we are "binding" the
            //  testOverlapList object to the view.  In this way, the
            // testOverlapListobject will be accessible in the cshtml
            return View(testOverlapList);
        }
    }

        
    }