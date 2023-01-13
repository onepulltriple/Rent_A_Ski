using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
            $"Server={Environment.GetEnvironmentVariable("COMPUTERNAME")}\\SQLEXPRESS; " +  
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

        public ObservableCollection<Article> GetArticles()
        {
            ObservableCollection<Article> tempListOfArticles = new();

            //query = "SELECT * FROM TABLE_ARTICLES";
            query = 
                "SELECT TABLE_ARTICLES.*, TABLE_STATUS.Description AS status_id, TABLE_CATEGORY.Name AS category_id FROM TABLE_ARTICLES " +
                "INNER JOIN TABLE_STATUS ON TABLE_ARTICLES.TABLE_STATUS_ID = TABLE_STATUS.id " +
                "INNER JOIN TABLE_CATEGORY ON TABLE_ARTICLES.TABLE_CATEGORY_ID = TABLE_CATEGORY.id";

            command = new(query, connection);

            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Article tempArticle = new();
                        tempArticle.id = (int)reader["id"];
                        tempArticle.SerialNumber = (string)reader["SerialNumber"];
                        tempArticle.Name = (string)reader["Name"];
                        tempArticle.DateAdded = (DateTime)reader["DateAdded"];
                        tempArticle.PricePerDay = (decimal)reader["PricePerDay"];
                        tempArticle.Counter = (int)reader["Counter"];
                        tempArticle.MaintenanceInterval = (int)reader["MaintenanceInterval"];

                        tempArticle.Status = new Status();
                        tempArticle.Category = new Category();
                        tempArticle.Status.id = (int)reader["TABLE_STATUS_ID"];
                        tempArticle.Status.Description = (string)reader["status_id"];
                        tempArticle.Category.id = (int)reader["TABLE_CATEGORY_ID"];
                        tempArticle.Category.Name = (string)reader["category_id"];

                        tempListOfArticles.Add(tempArticle);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                //throw;
            }

            connection.Close();

            return tempListOfArticles;
        }

        public ObservableCollection<Status> GetStatuses()
        {
            ObservableCollection<Status> tempListOfStatuses = new();

            query = "SELECT * FROM TABLE_STATUS";

            command = new(query, connection);

            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Status tempStatus = new();
                        tempStatus.id = (int)reader["id"];
                        tempStatus.Description = (string)reader["Description"];

                        tempListOfStatuses.Add(tempStatus);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                //throw;
            }

            connection.Close();

            return tempListOfStatuses;
        }

        public ObservableCollection<Category> GetCategories()
        {
            ObservableCollection<Category> tempListOfCategories = new();

            query = "SELECT * FROM TABLE_CATEGORY";

            command = new(query, connection);

            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Category tempCategory = new();
                        tempCategory.id = (int)reader["id"];
                        tempCategory.Name = (string)reader["Name"];

                        tempListOfCategories.Add(tempCategory);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                //throw;
            }

            connection.Close();

            return tempListOfCategories;
        }

        public ObservableCollection<Customer> GetCustomers()
        {
            ObservableCollection<Customer> tempListOfCustomers = new();

            query = "SELECT * FROM TABLE_CUSTOMERS";

            command = new(query, connection);

            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Customer tempCustomer = new();
                        tempCustomer.id = (int)reader["id"];
                        tempCustomer.FirstName = (string)reader["FirstName"];
                        tempCustomer.LastName = (string)reader["LastName"];
                        tempCustomer.Email = (string)reader["Email"];
                        tempCustomer.Birthday = (DateTime)reader["Birthday"];
                        tempCustomer.Address = (string)reader["Address"];
                        tempCustomer.City = (string)reader["City"];
                        tempCustomer.ZIP = (string)reader["ZIP"];

                        tempCustomer.Employee = new Employee();
                        tempCustomer.Employee.id = (int)reader["TABLE_EMPLOYEES_ID"];

                        tempListOfCustomers.Add(tempCustomer);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                //throw;
            }

            connection.Close();

            return tempListOfCustomers;
        }

        public ObservableCollection<Employee> GetEmployees()
        {
            ObservableCollection<Employee> tempListOfEmployees = new();

            query = "SELECT * FROM TABLE_EMPLOYEES";

            command = new(query, connection);

            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Employee tempEmployee = new();
                        tempEmployee.id = (int)reader["id"];
                        tempEmployee.FirstName = (string)reader["FirstName"];
                        tempEmployee.LastName = (string)reader["LastName"];
                        tempEmployee.Email = (string)reader["Email"];
                        tempEmployee.Birthday = (DateTime)reader["Birthday"];
                        tempEmployee.Address = (string)reader["Address"];
                        tempEmployee.City = (string)reader["City"];
                        tempEmployee.Username = (string)reader["Username"];
                        tempEmployee.Password = (string)reader["Password"];

                        tempListOfEmployees.Add(tempEmployee);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                //throw;
            }

            connection.Close();

            return tempListOfEmployees;
        }
    }
}
