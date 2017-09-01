using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARC.Models
{
  /// <summary>
  ///   A list of objects of class PortfolioHolding
  /// </summary>
  public class Portfolio
  {
    public List<PortfolioHolding> PortfolioList { get; set; }
  }
}