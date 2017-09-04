using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARC.Models
{
  /// <summary>
  ///   A LARC client (customer)
  /// </summary>
  public class Client
  {
    public int ClientID { get; set; }
    public Account ClientAccount { get; set; }
    public Portfolio ClientPortfolio { get; set; }

    public Client()
    {
      // Note:  When a client is instantiated, his account will either be read
      //        in from the database or if this is a new account written out to
      //        the database.
      this.ClientAccount = new Account();
      this.ClientPortfolio = new Portfolio();
    }
  }
}