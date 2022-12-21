using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_A_Ski.Models
{
    public class Customer
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

        public string Address { get; set; }

        public string City { get; set; }    

        public string ZIP { get; set; }

        public int Employee_id { get; set; }

    }
}
