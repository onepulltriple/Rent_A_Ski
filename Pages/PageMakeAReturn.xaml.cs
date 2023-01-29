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

        public ObservableCollection<Article> ListOfArticlesStagedForMaintenance { get; set; } = new();

        public ObservableCollection<Article> ListOfArticlesStagedForRepair { get; set; } = new();

        public Customer SelectedCustomer { get; set; }

        public Article ArticleToReturn { get; set; }

        public Article SelectedStagedForReturnArticle { get; set; }

        public Article SelectedStagedForMaintenanceArticle { get; set; }

        public Article SelectedStagedForRepairArticle { get; set; }

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

        public bool AreAnyArticlesStagedForMaintenance
        {
            get
            {
                if (ListOfArticlesStagedForMaintenance.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        public bool AreAnyArticlesStagedForRepair 
        {
            get
            {
                if (ListOfArticlesStagedForRepair.Count > 0)
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
            MarkArticleForMaintenanceButton.IsEnabled = false;
            MarkArticleForRepairButton.IsEnabled = false;
            RemoveArticleFromStageButton.IsEnabled = false;
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
                if (!ListOfArticlesStagedForReturn.Contains(item) &&
                    !ListOfArticlesStagedForMaintenance.Contains(item) &&
                    !ListOfArticlesStagedForRepair.Contains(item))
                        ListOfCustomersRentedArticles.Add(item);
            }

            NotificationLabel.Visibility = Visibility.Hidden;
        }

        private void CustomersRentedArticleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StageArticleForReturnButton.IsEnabled = true;
            MarkArticleForMaintenanceButton.IsEnabled = true;
            MarkArticleForRepairButton.IsEnabled = true;
            RemoveArticleFromStageButton.IsEnabled = false;
        }

        private void StageArticleForReturn(object sender, RoutedEventArgs e)
        {
            if (ArticleToReturn != null)
            {
                if (ArticleToReturn.Counter >= ArticleToReturn.MaintenanceInterval)
                {
                    ListOfArticlesStagedForMaintenance.Add(ArticleToReturn);
                    ListOfCustomersRentedArticles.Remove(ArticleToReturn);
                }
                else
                {
                    ListOfArticlesStagedForReturn.Add(ArticleToReturn);
                    ListOfCustomersRentedArticles.Remove(ArticleToReturn);
                }
            }

            ButtonState01();
        }

        private void MarkArticleForMaintenance(object sender, RoutedEventArgs e)
        {
            if (ArticleToReturn != null)
            {
                ListOfArticlesStagedForMaintenance.Add(ArticleToReturn);
                ListOfCustomersRentedArticles.Remove(ArticleToReturn);
            }

            ButtonState01();
        }

        private void MarkArticleForRepair(object sender, RoutedEventArgs e)
        {
            if (ArticleToReturn != null)
            {
                ListOfArticlesStagedForRepair.Add(ArticleToReturn);
                ListOfCustomersRentedArticles.Remove(ArticleToReturn);
            }

            ButtonState01();
        }

        private void ButtonState01()
        {
            StageArticleForReturnButton.IsEnabled = false;
            MarkArticleForMaintenanceButton.IsEnabled = false;
            MarkArticleForRepairButton.IsEnabled = false;
            ReturnItemsToInventoryButton.IsEnabled = true;
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

            if (SelectedStagedForMaintenanceArticle != null)
            {
                ListOfCustomersRentedArticles.Add(SelectedStagedForMaintenanceArticle);
                ListOfArticlesStagedForMaintenance.Remove(SelectedStagedForMaintenanceArticle);

                if (!AreAnyArticlesStagedForMaintenance)
                    ReturnItemsToInventoryButton.IsEnabled = false;
            }

            if (SelectedStagedForRepairArticle != null)
            {
                ListOfCustomersRentedArticles.Add(SelectedStagedForRepairArticle);
                ListOfArticlesStagedForRepair.Remove(SelectedStagedForRepairArticle);

                if (!AreAnyArticlesStagedForRepair)
                    ReturnItemsToInventoryButton.IsEnabled = false;
            }

            RemoveArticleFromStageButton.IsEnabled = false;
        }

        private void ArticleStagedForReturnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveArticleFromStageButton.IsEnabled = true;
            DataGridForMaintenance.SelectedItem = null;
            DataGridForRepair.SelectedItem = null;
            NotificationLabel.Visibility = Visibility.Hidden; 
        }

        private void ArticleStagedForMaintenanceSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveArticleFromStageButton.IsEnabled = true;
            DataGridForReturn.SelectedItem = null;
            DataGridForRepair.SelectedItem = null;
            NotificationLabel.Visibility = Visibility.Hidden;
        }

        private void ArticleStagedForRepairChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveArticleFromStageButton.IsEnabled = true;
            DataGridForReturn.SelectedItem = null;
            DataGridForMaintenance.SelectedItem = null;
            NotificationLabel.Visibility = Visibility.Hidden;
        }

        private void ReturnItemsToInventory(object sender, RoutedEventArgs e)
        {
            int status_id = 0;
            bool rentals_returned = false;
            bool rentals_to_maintain = false;
            bool rentals_to_repair = false;

            if (AreAnyArticlesStagedForReturn)
            {
                status_id = Status.ListOfStatuses.
                    First(status => status.Description == "Available").id;

                rentals_returned = new SQLController().
                  ReturnArticlesToInventory(ListOfArticlesStagedForReturn, status_id);
            }

            if (AreAnyArticlesStagedForMaintenance)
            {
                status_id = Status.ListOfStatuses.
                    First(status => status.Description == "Maintenance required").id;

                rentals_to_maintain = new SQLController().
                    ReturnArticlesToInventory(ListOfArticlesStagedForMaintenance, status_id);
            }

            if (AreAnyArticlesStagedForRepair)
            {
                status_id = Status.ListOfStatuses.
                    First(status => status.Description == "Repair required").id;

                rentals_to_repair = new SQLController().
                    ReturnArticlesToInventory(ListOfArticlesStagedForRepair, status_id);
            }

            if (rentals_returned || rentals_to_maintain || rentals_to_repair)
            {
                Rental.RefreshListOfRentals();
                ListOfArticlesStagedForReturn.Clear();
                ListOfArticlesStagedForMaintenance.Clear();
                ListOfArticlesStagedForRepair.Clear();

                NotificationLabel.Content =
                    "Item(s) successfully returned to inventory.";
                NotificationLabel.Visibility = Visibility.Visible;

                ReturnItemsToInventoryButton.IsEnabled = false;
            }
        }
    }
}
