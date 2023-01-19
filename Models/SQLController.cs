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

        public int? AreCredentialsOK(string username, string password)
        {
            int? foundid = null; 
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
                    return null;
                }

                // If we reach this point, a username was found.
                while (reader.Read())
                {
                    foundid = (int)reader["id"];
                    foundPassword = (string)reader["Password"];
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                return null;
                //throw;
            }
            finally
            {
                connection.Close();
            }

            // Return the below resolved value directly.
            if (BCrypt.Net.BCrypt.Verify(password, foundPassword))
                return foundid;
            else
                return null;
        }

        public ObservableCollection<Article> GetArticles()
        {
            ObservableCollection<Article> tempListOfArticles = new();

            query = "SELECT * FROM TABLE_ARTICLES";

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
                        tempArticle.Status_id = (int)reader["TABLE_STATUS_ID"];
                        tempArticle.Category_id = (int)reader["TABLE_CATEGORY_ID"];

                        tempArticle.Status =
                            Status.ListOfStatuses.First(x => x.id == tempArticle.Status_id);

                        tempArticle.Category =
                            Category.ListOfCategories.First(x => x.id == tempArticle.Category_id);

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

        public ObservableCollection<Rental> GetRentals()
        {
            ObservableCollection<Rental> tempListOfRentals = new();

            query = "SELECT * FROM TABLE_RENTALS";

            command = new(query, connection);

            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Rental tempRental = new();
                        tempRental.id = (int)reader["id"];
                        tempRental.OutgoingDate = (DateTime)reader["OutgoingDate"];
                        tempRental.ReturnDate = (DateTime)reader["ReturnDate"];
                        tempRental.Article_id = (int)reader["TABLE_ARTICLES_ID"];
                        tempRental.Customer_id = (int)reader["TABLE_CUSTOMERS_ID"];
                        tempRental.Employee_id = (int)reader["TABLE_EMPLOYEES_ID"];

                        tempRental.Article =
                            Article.ListOfArticles.First(x => x.id == tempRental.Article_id);

                        tempRental.Customer =
                            Customer.ListOfCustomers.First(x => x.id == tempRental.Customer_id);

                        tempRental.Employee =
                            Employee.ListOfEmployees.First(x => x.id == tempRental.Employee_id);

                        tempListOfRentals.Add(tempRental);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                //throw;
            }

            connection.Close();

            return tempListOfRentals;
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
                        tempCustomer.Employee_id = (int)reader["TABLE_EMPLOYEES_ID"];

                        tempCustomer.Employee =
                            Employee.ListOfEmployees.First(x => x.id == tempCustomer.Employee_id);

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
                        tempEmployee.ZIP = (string)reader["ZIP"];
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

        public bool RentArticles(ObservableCollection<Article> articles_list, Customer customer)
        {
            // Add rentals to TABLE_RENTALS
            query = 
                "INSERT INTO TABLE_RENTALS " +
                "(OutgoingDate, TABLE_ARTICLES_ID, TABLE_CUSTOMERS_ID, TABLE_EMPLOYEES_ID) VALUES " +
                "(@outgoing_date, @article_id, @customer_id, @employee_id)";

            try
            {
                connection.Open();

                foreach (Article item in articles_list)
                {
                    command = new(query, connection);
                    command.Parameters.AddWithValue("@outgoing_date", DateTime.Now);
                    command.Parameters.AddWithValue("@article_id", item.id);
                    command.Parameters.AddWithValue("@customer_id", customer.id);
                    //command.Parameters.AddWithValue("@employee_id", LoginWindow.Credentials.id);
                    command.Parameters.AddWithValue("@employee_id", 1);

                    command.ExecuteNonQuery();
                }

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                //throw;
            }
            finally
            {
                connection.Close();
            }

            // Change status and increment counter of affected articles in TABLE_ARTICLES
            query = 
                "UPDATE TABLE_ARTICLES SET " +
                "TABLE_STATUS_ID = @status_id, " +
                "Counter = @counter " +
                "WHERE id = @article_id";

            try
            {
                connection.Open();

                foreach (Article item in articles_list)
                {
                    command = new(query, connection);
                    command.Parameters.AddWithValue("@status_id", 
                        Status.ListOfStatuses.First(x=>x.Description == "Reserved").id);
                    command.Parameters.AddWithValue("@counter", item.Counter++);
                    command.Parameters.AddWithValue("@article_id", item.id);

                    command.ExecuteNonQuery();
                }

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                //throw;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }
    }
}
