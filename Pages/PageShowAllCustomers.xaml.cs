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
    /// Interaction logic for PageShowAllCustomers.xaml
    /// </summary>
    public partial class PageShowAllCustomers : Page
    {
        public ObservableCollection<Customer> FullListOfCustomers
        {
            get => Customer.ListOfCustomers;
        }

        public Customer SelectedCustomer { get; set; }

        public PageShowAllCustomers()
        {
            DataContext = this;
            InitializeData();
            InitializeComponent();
        }

        private void InitializeData()
        {
            Customer.RefreshListOfCustomers();
        }
    }
}
