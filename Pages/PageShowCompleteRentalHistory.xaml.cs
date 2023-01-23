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
    /// Interaction logic for PageShowCompleteRentalHistory.xaml
    /// </summary>
    public partial class PageShowCompleteRentalHistory : Page
    {
        public ObservableCollection<Rental> FullListOfRentals { get; set; }

        public Rental SelectedRental { get; set; }

        public PageShowCompleteRentalHistory()
        {
            DataContext = this;
            InitializeData();
            InitializeComponent();
        }

        public void InitializeData()
        {
            Article.RefreshListOfArticles();
            Customer.RefreshListOfCustomers();
            Rental.RefreshListOfRentals();
            Employee.RefreshListOfEmployees();

            FullListOfRentals = Rental.ListOfRentals;
        }
    }
}
