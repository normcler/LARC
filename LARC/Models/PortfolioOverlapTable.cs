using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARC.Models
{
  public class PortfolioOverlapTable
  {
    public List<PortfolioHolding> PorfolioList { get; set; }
    public decimal[,] OverlapMatrix { get; set; }

    public PortfolioOverlapTable(Portfolio portfolio)
    {
    }
  }
}