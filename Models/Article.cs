using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_A_Ski.Models
{
    public class Article
    {
        public int id { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        private DateTime dateadded;

        public DateTime DateAdded
        {
            set { dateadded = value; }
        }

        public string AddedToInventoryOn
        {
            get { return dateadded.ToString("yyyy-MM-dd"); }
        }

        public decimal PricePerDay { get; set; }

        public int Counter { get; set; }

        public int MaintenanceInterval { get; set; }

        public int Status_id { get; set; }

        public int Category_id { get; set; }

        public Status Status { get; set; }

        public Category Category { get; set; }

        public static ObservableCollection<Article> ListOfArticles { get; set; }

        public static void RefreshListOfArticles()
        {
            // Performed once at program start
                //Category.RefreshListOfCategories();
                //Status.RefreshListOfStatuses();

            ListOfArticles = new SQLController().GetArticles(); 
        }
    }
}
