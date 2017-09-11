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
    private LARC_DBEntities db = new LARC_DBEntities();

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
  }
}