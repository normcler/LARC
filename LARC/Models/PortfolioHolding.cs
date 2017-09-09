using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LARC.Models
{
  /// TODO: Create a view that will display a list of objects of class PortfolioHolding.

  /// <summary>
  ///     A single holding (fund) in a portfolio.
  /// </summary>
  public class PortfolioHolding
  {
    //[Display(Name = "Symbol")]
    public string Symbol { get; set; }

    //[Display(Name = "Shares")]
    public decimal NumberOfShares { get; set; }

    public string Name { get; set; }

    public PortfolioHolding ()
    {
      // Note: We do not need the name. It will be available when we read
      // in the fund file.
      // Also, the primary key should be the symbol.
      this.Symbol = "";
      this.NumberOfShares = 0.0m;
    }
  }
}