using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_A_Ski.Models
{
    public class Status
    {
        public int id { get; set; }

        public string Description { get; set; }

        public static ObservableCollection<Status> ListOfStatuses { get; set; }

        public static void RefreshListOfStatuses()
        {
            ListOfStatuses = new SQLController().GetStatuses();
        }
    }
}
