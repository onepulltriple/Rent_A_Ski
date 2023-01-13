using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_A_Ski.Models
{
    public class Employee
    {
        public int id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        private DateTime birthday;

        public DateTime Birthday
        {
            set { birthday = value; }
        }

        public string DateOfBirth
        {
            get { return birthday.ToString("yyyy-MM-dd"); }
        }

        public string Address { get; set; }

        public string City { get; set; }

        public string ZIP { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public static ObservableCollection<Employee> ListOfEmployees
        {
            get { return new SQLController().GetEmployees(); }
        }

    }
}
