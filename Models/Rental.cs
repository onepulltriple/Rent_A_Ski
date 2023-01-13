using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_A_Ski.Models
{
    public class Rental
    {
        public int id { get; set; }

        private DateTime outgoingdate;

        public DateTime OutgoingDate
        {
            set { outgoingdate = value; }
        }

        public string DateOfRental
        {
            get { return outgoingdate.ToString("yyyy-MM-dd"); }
        }

        private DateTime incomingdate;

        public DateTime IncomingDate
        {
            set { incomingdate = value; }
        }

        public string DateOfReturn
        {
            get { return incomingdate.ToString("yyyy-MM-dd"); }
        }

        public DateTime ReturnDate { get; set; }

        public int Article_id { get; set; }

        public int Customer_id { get; set; }

        public int Employee_id { get; set; }

        public Article Article { get; set; }

        public Customer Customer { get; set; }

        public Employee Employee { get; set; }

        public static ObservableCollection<Rental> ListOfRentals
        {
            get { return new SQLController().GetRentals(); }
        }

    }
}
