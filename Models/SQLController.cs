using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_A_Ski.Models
{
    public class SQLController
    {
        #region Connection info for Windows Authentication
        string connectionString =
            "Server=CREATOR-DESKTOP\\SQLEXPRESS; " +
            "Database=RENT_A_SKI; " +
            "Trusted_Connection=True;"; //Integrated Security = true; // also works
        #endregion

        SqlConnection connection;
        string query;
        SqlCommand command;
        SqlDataReader reader;

        public SQLController()
        {
            connection = new(connectionString);
        }

    }
}
