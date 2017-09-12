using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARC.Models
{
  /// <summary>
  ///   A class that is used to build the portfolio overlap table.
  /// </summary>
  public class PortfolioOverlapTable
  {

    public string Name;
    public List<PortfolioOverlapRow> TableRows { get; set; }

    /// <summary>
    ///   Constructor to form an overlap table for a portfolio
    /// </summary>
    /// <param name="clientID">Client ID number</param>
    /// <param name="db">Database</param>
    public PortfolioOverlapTable(int clientID, LARC_DBEntities db)
    {
      //ClientDB clientDB = db.ClientDBs.Find(clientID);
      ClientDB clientDB = db.ClientDBs.Find(2);
      int? portfolioId = clientDB.PortfolioID;

      var portfolioRecords = db.PortfolioFunds.Where(r => r.PortfolioID == portfolioId);
      List<string> fundList = new List<string>();
      foreach (var record in portfolioRecords)
      {
        fundList.Add(record.FundSymbol);
      }

      // The data I need is in FundEquities.
      int numberOfFunds = fundList.Count;
      decimal[,] overlapMatrix = new decimal[numberOfFunds, numberOfFunds];
      for (int kntOuter = 0; kntOuter < numberOfFunds; kntOuter++)
      {
        int jDiagonal = kntOuter;
        for (int iRow = 0; iRow < jDiagonal; iRow++)
        {
          // write the overlaps up to the diagonal; they are the mirror
          // of overlaps already computed.
        }

        // Inner loop over funds not yet compared.
        for (int kntInner = kntOuter + 1; kntInner < numberOfFunds;
            kntInner++)
        {
          // Compute the overlap.
          overlapMatrix[kntOuter, kntInner] =
            ComputeTotalOverlap(fundList[kntOuter], fundList[kntInner], db);
        }
      }
    }

    /// <summary>
    ///     Purpose: Find the common holdings in two funds in the portfolio
    ///         and return the overlap between the two funds.
    /// </summary>
    /// <param name="fundSymbol_1">
    ///     fundSymbol_1: The ticker for the first funds
    /// </param>
    /// <param name="fundSymbol_2">
    ///     fundSymbol_2: The ticker for the second fund
    /// </param>
    /// <returns>
    ///     fundsOverlap: the overlap in holdings between the funds.
    /// </returns>
    public static decimal ComputeTotalOverlap(string fundSymbol_1,
        string fundSymbol_2, LARC_DBEntities db)
    {
      /*
       * Programmer: N. S. Clerman, 29-Jul-2017
       * 
       * Revisions
       * =========
       *  1) N.S. Clerman, 02-Aug-2017: Change the commonHoldings List to
       *      a List of class Equity. Write a text file to create an table.
       */
      decimal fundsOverlap = 0.0M;
      if (fundSymbol_1 != fundSymbol_2)
      {
        List<FundEquity> fundEquityList_1 =
          db.FundEquities.Where(f => f.FundSymbol == fundSymbol_1).ToList();
        List<FundEquity> fundEquityList_2 =
          db.FundEquities.Where(f => f.FundSymbol == fundSymbol_2).ToList();

        List<Equity> commonEquities = new List<Equity>();
        Dictionary<string, decimal> localOverlapList =
            new Dictionary<string, decimal>();

        /*
         * A nested loop over the all the holdings in all the funds.
         * my guess is that there's a Linq function that will
         * accomplish this in one statement.

         */
        //for (int knt_1 = 0; knt_1 < fundEquityList_1.Count; knt_1++)
        // I cannot use this. I need to collect some data for each
        // individual overlap. Also, I need the loop to sum the individual
        // overlaps. I could use Intersect, but them I would need other,
        // different loops to compute what I need.

        //commonHoldings = (fundEquityList_1.Intersect(fundEquityList_2)).ToList<Equity>();
        //WriteLine($"trialIntersect has {commonHoldings.Count} elements");

        /*
         * A nested loop over the all the holdings in all the funds.
                        */
        //for (int knt_1 = 0; knt_1 < fundEquityList_1.Count; knt_1++)
        int kntCommon = 0;
        foreach (var fundEquityPair_1 in fundEquityList_1)
        {
          foreach (var fundEquityPair_2 in fundEquityList_2)
          {
            if (fundEquityPair_1.EquitySymbol == fundEquityPair_2.EquitySymbol)
            {
              Equity equity_1 = fundEquityPair_1.Equity;
              Equity equity_2 = fundEquityPair_2.Equity;
              commonEquities.Add(equity_1);
              decimal currentOverlap = ComputerOverlap(fundEquityPair_1, fundEquityPair_2);
              kntCommon++;
              localOverlapList[fundEquityPair_1.EquitySymbol] = currentOverlap;
              fundsOverlap += currentOverlap;
            }
          }
        }
        localOverlapList.Clear();
      }
      return fundsOverlap;
    }

    public static decimal ComputerOverlap(FundEquity targetFundEquity_1,
      FundEquity targetFundEquity_2)
    {
      /*
       * Programmer: N. S. Clerman, 02-Aug-2017
       */
      // Verify the holdings are the same.
      if (targetFundEquity_1.EquitySymbol != targetFundEquity_2.EquitySymbol)
      {
        return 0.0M;
      }

      /*
       * These holdings are in two different funds. The total value of
       * each fund in the portfolio are assumed identical (imagine each
       * as $100) when computing the overlap.
       * 
       * Therefore, the computation in this case is half the sum of the
       * two percentages.
       */
      decimal overlap;
      return overlap = 0.5M * (decimal)(targetFundEquity_1.Weighting +
          targetFundEquity_2.Weighting);
    }

  }
}