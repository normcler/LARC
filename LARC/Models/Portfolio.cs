using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Morningstar.Importer;

namespace LARC.Models
{
  /// <summary>
  ///   A list of class PortfolioHolding. 
  /// </summary>
  public class Portfolio
  {
    public const string FILE_REPO = @"\Users\norm\Dropbox\" +
        @"windows-manitowoc\Source\Repos\fund-holdings\fund-holdings\" +
        @"file_repo\";

    // Name - user-assigned name
    // PortfolioList - a list of objects of class PortfolioHolding 
    //  (the fund symbol and the number of shares)
    // PortfolioDictionary - a dictionary whose keys are the fund symbol and
    //  values are a list of class Holding (each Holding entry comprises all the
    //  data read in off the fund file).
    public string Name;
    public List<PortfolioHolding> PortfolioList { get; set; }
    public Dictionary<string, List<Holding>> PortfolioDictionary { get; set; }

    /// <summary>
    ///   Default constructor
    /// </summary>
    public Portfolio ()
    {
      // Note:  The Portfolio list will come in from the database.
      //        The PortfolioDictionary will be created in the constructor
      //        by importing from the appropriate fund files. Eventually, the
      //        data should be obtained through an API, not through the 
      //        Morningstar file.
      this.Name = "";
      this.PortfolioList = new List<PortfolioHolding>();
      this.PortfolioDictionary = new Dictionary<string, List<Holding>>();
    }

    /// <summary>
    ///   Constructor, called on instantion of a client.
    /// </summary>
    /// <param name="holdingList">
    ///   The portfolio holding list read in from the DB. 
    /// </param>
    /// <param name="name">
    ///   The user-assigned portfolio name.
    /// </param>
    public Portfolio (string name, List<PortfolioHolding> portfolioSymbolList)
    {
      this.Name = name;
      this.PortfolioList = portfolioSymbolList;
      this.PortfolioDictionary = new Dictionary<string, List<Holding>>();
      /*
             *  Loop through all the funds in the list of fund tickers.
             *  Import the file. Filter out all holdings that are not equities
             *  or do not have a ticker symbol.
             */
      foreach (PortfolioHolding item in portfolioSymbolList)
      {
        /// TODO: Obtain this file from an API.
        // The filtering is done in the Morningstar importer, not here.
        List<Holding> equityList = Morningstar.Importer.FundFileImporter.
            Import(FILE_REPO, item.Symbol);
        //List<Holding> equityList = 
          //rawList.Where(x => (x.IsEquityHolding() && x.HasTicker())).ToList();
        this.PortfolioDictionary[item.Symbol] = equityList;
      }
    }

    /// <summary>
    ///   Compute the total overlap between two funds in a portfolio.
    /// </summary>
    /// <param name="fundSymbol_1">The symbol of the first fund</param>
    /// <param name="fund_Symbol_2">The symbol of the second fund</param>
    /// <returns></returns>
    public decimal ComputeTotalOverlap(string fundSymbol_1, string fundSymbol_2)
    {
      decimal fundsOverlap = 0.0M;
      if ((fundSymbol_1 != fundSymbol_2) &&
          (PortfolioDictionary.Count > 0) &&
          PortfolioDictionary.ContainsKey(fundSymbol_1) &&
          PortfolioDictionary.ContainsKey(fundSymbol_2))
      {
        List<Holding> hList_1 = PortfolioDictionary[fundSymbol_1];
        List<Holding> hList_2 = PortfolioDictionary[fundSymbol_2];
        List<Holding> commonHoldings = new List<Holding>();

        int kntCommon = 0;
        foreach (Holding h_1 in hList_1)
        {
          foreach (Holding h_2 in hList_2)
          {
            if (h_1 == h_2)
            {
              commonHoldings.Add(h_1);
              decimal currentOverlap = h_1.ComputerOverlap(h_2);
              kntCommon++;
              fundsOverlap += currentOverlap;
            }
          }
        }
      }
      return fundsOverlap;
    }
  }
}