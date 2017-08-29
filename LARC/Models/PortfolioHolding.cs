using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARC.Models
{
  /// <summary>
  ///     A single holding (fund) in a portfolio.
  /// </summary>
  public class PortfolioHolding
  {
      public string Symbol { get; set; }
      public int NumberOfShares { get; set; }
  }
}