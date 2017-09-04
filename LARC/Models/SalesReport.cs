using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


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
    //[Display(Name = "Amount")]
    public int Amount { get; set; }
    //[Display(Name = "Name")]
    public string Name { get; set; }
    //[Display(Name = "Dollar Amount")]
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

    public TopRevenueByQuantity(int amount, string name)
    {
      //[Display(Name = "Quantity")]
      this.Amount = amount;
      //[Display(Name = "Amount")]
      this.Name = name;
    }
  }
}