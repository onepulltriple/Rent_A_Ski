using System;
using System.Collections.Generic;
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

        private DateOnly birthday;

        public DateOnly Birthday
        {
            set { birthday = value; }
        }

        public string DateOfBirth
        {
            get { return birthday.ToString("dd.MM.yyyy"); }
        }

        public string Username { get; set; }

        public string Password { get; set; }


    }
}
