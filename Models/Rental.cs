using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_A_Ski.Models
{
    public class Rental
    {
        public int id { get; set; }

        public DateTime OutgoingDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public int Article_id { get; set; }

        public int Customer_id { get; set; }

        public int Employee_id { get; set; }

    }
}
