using System;
using static System.Console;

namespace Morningstar.Importer
{
    /// <summary>
    /// A class containing one mutual fund holding.
    /// </summary>
    /// <remarks>
    /// Programmer: N. S. Clerman
    /// 
    /// Note: All numeric and DateTime values are nullable. This is their
    ///       state if they are not present in the file.
    /// </remarks>
    public class Holding : IEquatable<Holding>
    {
        public string Name { get; set; }
        public decimal? Weighting { get; set; }
        public string Type { get; set; }
        public string Ticker { get; set; }
        public string Style { get; set; }
        public DateTime? FirstBought { get; set; }
        public int? SharesOwned { get; set; }
        public int? SharesChange { get; set; }
        public string Sector { get; set; }
        public int? MarketValue { get; set; }
        public decimal? Price { get; set; }
        // day change is a string containing the change in currency and in
        // percentage, separated by a vertical bar, |. The percentage includes
        // the percent symbol, %.
        public decimal? DayChangeValue { get; set; }
        public decimal? DayChangePercent { get; set; }
        // high low is a pair of floating point numbers separated by a hyphen
        // (dash), -.
        public decimal? HighDay { get; set; }
        public decimal? LowDay { get; set; }
        public int? Volume { get; set; }
        // same as highLowDay
        public decimal? High52week { get; set; }
        public decimal? Low52week { get; set; }
        public string Country { get; set; }
        public decimal? Return3month { get; set; }
        public decimal? Return1year { get; set; }
        public decimal? Return3year { get; set; }
        public decimal? Return5year { get; set; }
        public decimal? MarketCapMil { get; set; }
        public string Currency { get; set; }
        public int? RatingMorningstar { get; set; }
        public decimal? ReturnYTD { get; set; }
        public decimal? PriceToEarnings { get; set; }
        public DateTime? MaturityDate { get; set; }
        public decimal? CouponPercent { get; set; }
        public decimal? YieldToMaturity { get; set; }

        public void PrintHoldingData()
        {
            WriteLine($"Name: {this.Name}");
            WriteLine($"Ticker: {this.Ticker}");
            WriteLine($"Ticker: {this.Type}");
            WriteLine($"Weighting: {this.Weighting}");
            WriteLine($"First Bought: {this.FirstBought}");
            WriteLine($"Shares Owned: {this.SharesOwned}");
            WriteLine($"Day High: {this.HighDay}");
            WriteLine($"Day Low: {this.LowDay}");
            ReadLine();
        }

        /// <summary>
        ///     Purpose: Create operators == and !=
        /// </summary>
        /// <param name="h_1">
        ///     h1: the first holding
        /// </param>
        /// <param name="h_2">
        ///     h2: the second holding
        /// </param>
        /// <returns>
        ///     true if the holding Tickers are the identical.
        /// </returns>
        public static bool operator == (Holding h_1, Holding h_2)
        {
            if (System.Object.ReferenceEquals(h_1, h_2))
            {
                return true;
            }
            if ((object)h_1 == null || (object)h_2 == null)
            {
                return false;
            }
            return h_1.Ticker.ToUpper().Trim() == h_2.Ticker.ToUpper().Trim();
        }

        public static bool operator !=(Holding h_1, Holding h_2)
        {
            return !(h_1 == h_2);
        }

        /// <summary>
        ///     Purpose: The method required to make class Holding IEquatable
        /// </summary>
        /// <param name="targetHolding">
        ///     targetHolding: Comparison holding.
        /// </param>
        /// <returns>
        ///     result: true if the Ticker field for both are identical.
        /// </returns>
        /// <remarks>
        ///     Programmer: N. S. Clerman, 29-Jul-2017
        /// </remarks>

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Holding h = obj as Holding;
            if ((System.Object)h == null)
            {
                return false;
            }
            return HaveSameTicker(h);
        }

        public bool Equals(Holding targetHolding)
        {
            if ((object)targetHolding == null)
            {
                return false;
            }
            return HaveSameTicker(targetHolding);
        }

        public override int GetHashCode()
        {
            return this.Ticker.GetHashCode();
        }

        private bool HaveSameTicker(Holding targetHolding)
        {
            return this.Ticker.ToUpper().Trim() ==
                targetHolding.Ticker.ToUpper().Trim();
        }

        /// <summary>
        ///     Purpose: return a bool indicating if the type of the holding is
        ///     "EQUITY".
        /// </summary>
        /// <returns>
        ///     result: true if the holding type is "EQUITY"
        /// </returns>
        /// <remarks>
        ///     Programmer: N. S. Clerman, 29-Jul-2017
        /// </remarks>
        public bool IsEquityHolding()
        {
            bool result;
            result = this.Type.ToUpper() == "EQUITY";
            if (!result)
            {
                WriteLine($"Holding {this.Name} is not an equity holding.");
            }
            return result;
        }

        /// <summary>
        ///     Purpose: return a bool indicating if the Ticker property of a
        ///         Holding is "empty," where the string is a hyphen (dash), -.
        /// </summary>
        /// <returns></returns>
        public bool HasTicker()
        {
            bool result;
            return result = !(this.Ticker == "-");
            /*if (!result)
            {
                WriteLine(@"The following holding has an "" empty ticker.");
                this.PrintHoldingData();
            }*/
        }

        /// <summary>
        ///     Compute the overlap between two funds that have common holdings.
        /// </summary>
        /// <param name="targetHolding"></param>
        /// <returns>The overlap as a percentage</returns>
        public decimal ComputerOverlap( Holding targetHolding)
        {
            /*
             * Programmer: N. S. Clerman, 02-Aug-2017
             */
            // Verify the holdings are the same.
            if (this != targetHolding)
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
            return overlap = 0.5M *(decimal)(this.Weighting +
                targetHolding.Weighting);
        }
    }

}
