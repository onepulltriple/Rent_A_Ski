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
        public ObservableCollection<Customer> ListOfCustomersWithRentals { get; set; } = new();

        public ObservableCollection<Article> ListOfCustomersRentedArticles { get; set; } = new();

        public ObservableCollection<Article> ListOfArticlesStagedForReturn { get; set; } = new();

        public ObservableCollection<Article> ListOfArticlesStagedForRepairOrMaint { get; set; } = new();

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
            StageArticleForReturnButton.IsEnabled = false;
            RemoveArticleFromStageButton.IsEnabled = false;
            MarkArticleForRepairButton.IsEnabled = false;
            NotificationLabel.Visibility = Visibility.Hidden;
        }

        private void UpdateCustomersDisplayedArticles()
        {
            ListOfCustomersRentedArticles.Clear();

            var tempList = Rental.ListOfRentals.
                Where(rental => 
                    rental.Customer_id == SelectedCustomer.id &&
                    rental.DateOfReturn == null).
                Select(rental => rental.Article);

            foreach (var item in tempList)
            {
                ListOfCustomersRentedArticles.Add(item);
            }

            NotificationLabel.Visibility = Visibility.Hidden;
        }

        private void CustomersRentedArticleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StageArticleForReturnButton.IsEnabled = true;
            MarkArticleForRepairButton.IsEnabled = true;
            RemoveArticleFromStageButton.IsEnabled = false;
        }

        private void StageArticleForReturn(object sender, RoutedEventArgs e)
        {
            if (ArticleToReturn != null)
            {
                ListOfArticlesStagedForReturn.Add(ArticleToReturn);
                ListOfCustomersRentedArticles.Remove(ArticleToReturn);
            }

            StageArticleForReturnButton.IsEnabled = false;
            MarkArticleForRepairButton.IsEnabled = false;
            ReturnItemsToInventoryButton.IsEnabled = true;
        }

        private void MarkArticleForRepair(object sender, RoutedEventArgs e)
        {
            if (ArticleToReturn != null)
            {
                ListOfArticlesStagedForRepairOrMaint.Add(ArticleToReturn);
                ListOfCustomersRentedArticles.Remove(ArticleToReturn);
            }

            StageArticleForReturnButton.IsEnabled = false;
            MarkArticleForRepairButton.IsEnabled = false;
            SendItemsForRepairOrMaintenanceButton.IsEnabled = true;
        }

        private void RemoveArticleFromStage(object sender, RoutedEventArgs e)
        {
            if (SelectedStagedForReturnArticle != null)
            {
                ListOfCustomersRentedArticles.Add(SelectedStagedForReturnArticle);
                ListOfArticlesStagedForReturn.Remove(SelectedStagedForReturnArticle);

                if (!AreAnyArticlesStagedForReturn)
                    ReturnItemsToInventoryButton.IsEnabled = false;
            }

            if (SelectedStagedForRepairOrMaintArticle != null)
            {
                ListOfCustomersRentedArticles.Add(SelectedStagedForRepairOrMaintArticle);
                ListOfArticlesStagedForRepairOrMaint.Remove(SelectedStagedForRepairOrMaintArticle);

                if (!AreAnyArticlesStagedForRepairOrMaint)
                    SendItemsForRepairOrMaintenanceButton.IsEnabled = false;
            }

            RemoveArticleFromStageButton.IsEnabled = false;
        }

        private void ArticleStagedForReturnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveArticleFromStageButton.IsEnabled = true;
            DataGridForRepairOrMaint.SelectedItem = null;
        }

        private void ArticleStagedForRepairOrMaintChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveArticleFromStageButton.IsEnabled = true;
            DataGridForReturn.SelectedItem = null;
        }

        private void ReturnItemsToInventory(object sender, RoutedEventArgs e)
        {
            if (AreAnyArticlesStagedForReturn)
            {
                bool rentals_returned = new SQLController().
                    ReturnArticlesToInventory(ListOfArticlesStagedForReturn);

                if (rentals_returned)
                {
                    Rental.RefreshListOfRentals();
                    ListOfArticlesStagedForReturn.Clear();
                    UpdateCustomersDisplayedArticles();

                    // update list of customers, too

                    NotificationLabel.Content =
                        "Item(s) successfully returned by " +
                        $"{SelectedCustomer.FirstName} {SelectedCustomer.LastName}.";
                    NotificationLabel.Visibility = Visibility.Visible;

                    ReturnItemsToInventoryButton.IsEnabled = false;
                }
            }
        }

        private void SendItemsForRepairOrMaintenance(object sender, RoutedEventArgs e)
        {

        }
    }
}
