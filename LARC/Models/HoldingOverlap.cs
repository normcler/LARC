using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LARC.Models
{
  /// <summary>
  ///     The data for a holding that is held by two funds.
  ///     The data comprise the holding ticker (e.g., MSFT),
  ///     the holding name (e.g., Microsoft Corp.) and the overlap expressed
  ///     in percentage (e.g., 0.855).
  /// </summary>
  public class HoldingOverlap
  {
    public string HoldingTicker { get; set; }
    public string HoldingName { get; set; }
    public decimal Overlap { get; set; }

    public HoldingOverlap()
    {
      HoldingTicker = "";
      HoldingName = "";
      Overlap = 0.0M;
    }

    public HoldingOverlap(string ticker, string name, decimal overlap)
    {
      HoldingTicker = ticker;
      HoldingName = name;
      Overlap = overlap;
    }
  }
}
