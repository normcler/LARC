using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LARC.Models;

namespace LARC.Controllers
{
  public class SalesReportController : Controller
  {
    // GET: SalesReport
    public ActionResult Index(string id)
    {
      string connectionString = ConfigurationManager.
           ConnectionStrings["AdventureWorks"].ConnectionString;

      // The instantion of the class SalesReport that holds our
      // data. The class is defined in the model.
      SalesReport report = new SalesReport();
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();
        using (SqlCommand command = connection.CreateCommand())
        {
          command.CommandText = "sp_GetStates";
          command.CommandType = System.Data.CommandType.StoredProcedure;
          using (SqlDataReader reader = command.ExecuteReader())
          {
            // "StateProvince" is the column heading in the DB.
            int columnPosition = reader.GetOrdinal("StateProvince");
            while (reader.Read())
            {
              report.states.Add(reader.GetString(columnPosition));
            }
          }
        }
        if (!string.IsNullOrEmpty(id))
        {
          string stateProvince = id;
          using (SqlCommand command = connection.CreateCommand())
          {
            command.CommandText = "sp_GetTopSalesByDollar";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@stateProvince", stateProvince);

            using (SqlDataReader reader = command.ExecuteReader())
            {
              int amountColumn = reader.GetOrdinal("Amount");
              int nameColumn = reader.GetOrdinal("Name");
              int dollarAmountColumn = reader.GetOrdinal("DollarAmount");
              while (reader.Read())
              {
                int amount = reader.GetInt32(amountColumn);
                string name = reader.GetString(nameColumn);
                decimal dollarAmount = reader.GetDecimal(dollarAmountColumn);
                TopRevenueByDollar newItem = 
                  new TopRevenueByDollar(amount, name, dollarAmount);
                report.TopRevenuesByDollar.Add(newItem);
                
              }
            }
          }
        }
        connection.Close();
      }
      return View(report);
    }
  }
}