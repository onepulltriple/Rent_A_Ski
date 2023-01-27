using Rent_A_Ski.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rent_A_Ski.Pages
{
    /// <summary>
    /// Interaction logic for PageShowOutstandingRentals.xaml
    /// </summary>
    public partial class PageShowOutstandingRentals : Page
    {
        public ObservableCollection<Rental> ListOfOutstandingRentals { get; set; } = new();

        public Rental SelectedOutstandingRental { get; set; }

        public PageShowOutstandingRentals()
        {
            DataContext = this;
            InitializeData();
            InitializeComponent();
        }

        public void InitializeData()
        {
            Rental.RefreshListOfRentals();

            var tempList = Rental.ListOfRentals.
                Where(rental => rental.Article.Status.Description == "Rented");

            foreach (var item in tempList)
            {
                ListOfOutstandingRentals.Add(item);
            }
        }
    }
}
