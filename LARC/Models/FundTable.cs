using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Morningstar.Importer;

namespace LARC.Models
{
  /// <summary>
  ///   A class that contains a subset of the data for each fund holding.
  /// </summary>
  public class FundTable
  {

    public string Name { get; set; }
    public List<FundTableRow> TableRows { get; set; }

    /// <summary>
    ///   A class created on-the-fly to a subset of the data for each holding
    ///   in a fund.
    /// </summary>
    /// <param name=""></param>
    public FundTable(string fundName, List<Holding> fundHoldingList)
    {
      this.Name = fundName;
      int numOfHoldings = fundHoldingList.Count;
      List<FundTableRow> tableRows = new List<FundTableRow>();
      foreach (var holding in fundHoldingList)
      {
        this.TableRows.Add(new FundTableRow(holding.Ticker, holding.Name,
          (decimal)holding.Price, (decimal)holding.Weighting));
      }
    }

    public FundTable(string fundSymbol, LARC_DBEntities db)
    {
      this.Name = fundSymbol;
      List<FundEquity> fundEquityList =
        db.FundEquities.Where(f => f.FundSymbol == fundSymbol).ToList();
      List<FundTableRow> tableRows = new List<FundTableRow>();
      foreach (var item in fundEquityList)
      {
        Equity equity = db.Equities.First(x => x.Symbol == item.EquitySymbol);
        tableRows.Add(new FundTableRow(equity.Symbol, equity.Name,
          equity.Price, item.Weighting));
      }
      this.TableRows = tableRows;
    }
  }
}