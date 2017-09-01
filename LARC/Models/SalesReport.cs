using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARC.Models
{

  public class SalesReport
  {
    public List<string> states;
    public List<TopRevenueByDollar> TopRevenuesByDollar{ get; set; }
    public List<TopRevenueByQuantity> TopRevenuesByQuantity { get; set; }

    public SalesReport()
    {
      this.states = new List<string>();
      this.TopRevenuesByDollar = new List<TopRevenueByDollar>();
      this.TopRevenuesByQuantity = new List<TopRevenueByQuantity>();
    }
  }


  public class TopRevenueByDollar
  {
    public int Amount { get; set; }
    public string Name { get; set; }
    public decimal DollarAmount { get; set; }

    public TopRevenueByDollar(int amount, string name, decimal dollarAmount)
    {
      this.Amount = amount;
      this.Name = name;
      this.DollarAmount = dollarAmount;
    }
  }

  public class TopRevenueByQuantity
  {
    public int Amount { get; set; }
    public string Name { get; set; }
  }
}