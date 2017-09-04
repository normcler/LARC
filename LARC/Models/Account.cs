using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARC.Models
{
    /// <summary>
    ///     class to hold client account information.
    ///     Each Client class contains an Account instance.
    /// </summary>
    public class Account
    {
      public int ID { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Email { get; set; }
      public string Password { get; set; }
      public DateTime DateCreated { get; set; }
      public DateTime ExpirationDate { get; set; }
      public DateTime LastDateModified { get; set; }

    public Account()
      {
          FirstName = "";
          LastName = "";
          Email = "";
          Password = "";
      }
    }
}