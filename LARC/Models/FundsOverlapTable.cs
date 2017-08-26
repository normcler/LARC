using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LARC.Models
{
    /// <summary>
    ///     A class containing the symbols of two funds and a List of class
    ///     HoldingOverlap. These are the holdings of the two funds that are
    ///     held by each.
    /// </summary>
    public class FundsOverlapTable
    {
        public string FundSymbol_1;
        public string FundSymbol_2;
        public List<HoldingOverlap> OverlapList { get; set; }

        public static List<FundsOverlapTable> PortFundsOverlaps =
            new List<FundsOverlapTable>();

        public FundsOverlapTable()
        {
            FundSymbol_1 = "";
            FundSymbol_2 = "";
            OverlapList = new List<HoldingOverlap>();
        }

        public FundsOverlapTable(string symbol_1 = "", string symbol_2 = "")
        {
            FundSymbol_1 = symbol_1;
            FundSymbol_2 = symbol_2;
            OverlapList = new List<HoldingOverlap>();
        }

        public void PrintTable()
        {
            WriteLine($" Overlap Table for funds {this.FundSymbol_1}" +
                $" and {this.FundSymbol_2}");
            WriteLine("TICKER NAME                 OVERLAP");

            foreach (HoldingOverlap h in this.OverlapList)
            {
                WriteLine($"{h.HoldingTicker}    {h.HoldingName}     " +
                    $"{h.Overlap}");
            }
        }
    }
}
