using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public bool AreCredentialsOK(string username, string password)
        {
            string foundPassword = "";
            query = "SELECT * FROM TABLE_EMPLOYEES WHERE Username = @UsernamePlaceholder";

            command = new(query, connection);
            command.Parameters.AddWithValue("@UsernamePlaceholder", username);

            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.HasRows == false)
                {
                    return false;
                }

                // If we reach this point, a username was found.
                while (reader.Read())
                {
                    foundPassword = (string)reader["Password"];
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                return false;
                //throw;
            }
            finally
            {
                connection.Close();
            }

            // Return the below resolved value directly.
            return BCrypt.Net.BCrypt.Verify(password, foundPassword);

        }
    }
}
