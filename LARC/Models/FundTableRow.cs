using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARC.Models
{
  /// <summary>
  ///   A class that holds the data for a row in the fund Table.Xs
  /// </summary>
  public class FundTableRow
  {
    public string Ticker { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal Weighting { get; set; }

    /// <summary>
    ///   Default constructor
    /// </summary>
    public FundTableRow()
    {
      this.Ticker = "";
      this.Name = "";
      this.Price = 0.0m;
      this.Weighting = 0.0m;
    }

    public FundTableRow(string ticker, string name, decimal price,
      decimal weighting)
    {
      this.Ticker = ticker;
      this.Name = name;
      this.Price = price;
      this.Weighting = weighting;
    }
  }
}