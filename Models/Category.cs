using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_A_Ski.Models
{
    public class Category
    {
        public int id { get; set; }

        public string Name { get; set; }

        public static ObservableCollection<Category> ListOfCategories
        {
            get { return new SQLController().GetCategories(); }
        }

    }
}
