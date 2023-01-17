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
    /// Interaction logic for PageMakeARental.xaml
    /// </summary>
    public partial class PageMakeARental : Page
    {
        public ObservableCollection<Article> ListOfAvailableArticles 
        {
            get
            {
                var tempList = Article.ListOfArticles.
                    Where(x => x.Status.Description == "Available").ToList();
                return new ObservableCollection<Article>(tempList);
            }
        }
        public ObservableCollection<Customer> FullListOfCustomers
        {
            get => Customer.ListOfCustomers;
        }

        public Article SelectedArticle { get; set; }
        public Customer SelectedCustomer { get; set; }

        public PageMakeARental()
        {
            DataContext = this;
            InitializeData();
            InitializeComponent();
        }

        private void InitializeData()
        {
            Article.RefreshListOfArticles();
            Customer.RefreshListOfCustomers();
        }

        private void CreateRental(object sender, RoutedEventArgs e)
        {

        }
    }
}
