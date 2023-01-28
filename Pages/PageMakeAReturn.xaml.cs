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
    /// Interaction logic for PageMakeAReturn.xaml
    /// </summary>
    public partial class PageMakeAReturn : Page
    {
        public ObservableCollection<Customer> ListOfCustomersWithRentals { get; set; }

        public ObservableCollection<Article> ListOfCustomersRentedArticles { get; set; } = new();

        public ObservableCollection<Article> ListOfArticlesStagedForReturn { get; set; }

        public ObservableCollection<Article> ListOfArticlesStagedForRepairOrMaint { get; set; }

        public Customer SelectedCustomer { get; set; }

        public Article ArticleToReturn { get; set; }

        public Article SelectedStagedForReturnArticle { get; set; }

        public Article SelectedStagedForRepairOrMaintArticle { get; set; }

        public bool AreAnyArticlesStagedForReturn 
        {
            get
            {
                if (ListOfArticlesStagedForReturn.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        public bool AreAnyArticlesStagedForRepairOrMaint 
        {
            get
            {
                if (ListOfArticlesStagedForRepairOrMaint.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        public PageMakeAReturn()
        {
            DataContext = this;
            InitializeData();
            InitializeComponent();
        }

        private void InitializeData()
        {
            Rental.RefreshListOfRentals();

            ListOfCustomersWithRentals = new ObservableCollection<Customer>
                (
                    Rental.ListOfRentals.
                    Where(rental => rental.DateOfReturn == null).
                    Select(rental => rental.Customer).
                    Distinct()
                );

        }

        private void CustomerChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCustomersDisplayedArticles();
        }

        private void UpdateCustomersDisplayedArticles()
        {
            ListOfCustomersRentedArticles.Clear();

            var tempList = Rental.ListOfRentals.
                Where(rental => rental.Customer_id == SelectedCustomer.id).
                Select(rental => rental.Article);

            foreach (var item in tempList)
            {
                ListOfCustomersRentedArticles.Add(item);
            }

            NotificationLabel.Visibility = Visibility.Hidden;
        }

        private void CustomersRentedArticleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StageArticleForReturn(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveArticleFromStage(object sender, RoutedEventArgs e)
        {

        }

        private void MarkArticleForRepair(object sender, RoutedEventArgs e)
        {

        }

        private void ArticleStagedForReturnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StagedForRepairOrMaintArticleChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
