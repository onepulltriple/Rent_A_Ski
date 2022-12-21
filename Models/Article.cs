using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_A_Ski.Models
{
    public class Article
    {
        public int id { get; set; }

        public string Name { get; set; }

        public decimal PricePerDay { get; set; }

        public int Counter { get; set; }

        public int MaintenanceInterval { get; set; }

        public int Status_id { get; set; }

        public int Category_id { get; set; }


    }
}
