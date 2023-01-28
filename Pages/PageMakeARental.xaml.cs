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
using System.Windows.Shell;

namespace Rent_A_Ski.Pages
{
    /// <summary>
    /// Interaction logic for PageMakeARental.xaml
    /// </summary>
    public partial class PageMakeARental : Page
    {
        public ObservableCollection<Article> ListOfAvailableArticles { get; set; }

        public ObservableCollection<Customer> FullListOfCustomers { get; set; }

        public ObservableCollection<Article> StagedArticlesList { get; set; } = new();

        public ObservableCollection<Article> ListOfCustomersRentedArticles { get; set; } = new();

        public Article SelectedAvailableArticle { get; set; }

        public Article SelectedStagedArticle { get; set; }

        public Customer SelectedCustomer { get; set; }

        public bool AreAnyArticlesStaged 
        {
            get
            {
                if (StagedArticlesList.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        public bool IsACustomerSelected 
        {
            get
            {
                if (SelectedCustomer != null)
                    return true;
                else
                    return false;
            }
        }

        public PageMakeARental()
        {
            DataContext = this;
            InitializeData();
            InitializeComponent();
        }

        private void InitializeData()
        {
            Rental.RefreshListOfRentals();

            ListOfAvailableArticles = new ObservableCollection<Article>
                (
                    Article.ListOfArticles.
                    Where(article => article.Status.Description == "Available")
                );

            FullListOfCustomers = Customer.ListOfCustomers;
        }


        private void AddArticlesToStage(object sender, RoutedEventArgs e)
        {
            if (SelectedAvailableArticle != null)
            {
                StagedArticlesList.Add(SelectedAvailableArticle);
                ListOfAvailableArticles.Remove(SelectedAvailableArticle);
            }

            AddArticlesToStageButton.IsEnabled = false;
            if (IsACustomerSelected)
                CreateRentalButton.IsEnabled = true;
        }

        private void RemoveArticlesFromStage(object sender, RoutedEventArgs e)
        {
            if (SelectedStagedArticle != null)
            {
                ListOfAvailableArticles.Add(SelectedStagedArticle);
                StagedArticlesList.Remove(SelectedStagedArticle);
            }

            RemoveArticlesFromStageButton.IsEnabled = false;
            if (!AreAnyArticlesStaged)
                CreateRentalButton.IsEnabled = false;
        }

        private void CreateRental(object sender, RoutedEventArgs e)
        {
            if (AreAnyArticlesStaged && IsACustomerSelected)
            {
                bool rentals_logged = new SQLController().
                    RentArticles(StagedArticlesList, SelectedCustomer);

                if (rentals_logged)
                {
                    Rental.RefreshListOfRentals();
                    StagedArticlesList.Clear();
                    UpdateCustomersDisplayedArticles();

                    NotificationLabel.Content = 
                        "Item(s) successfully rented to " +
                        $"{SelectedCustomer.FirstName} {SelectedCustomer.LastName}.";
                    NotificationLabel.Visibility = Visibility.Visible;

                    CreateRentalButton.IsEnabled = false;
                    RemoveArticlesFromStageButton.IsEnabled = false;
                }
            }
        }

        private void AvailableArticleChanged(object sender, SelectionChangedEventArgs e)
        {
            AddArticlesToStageButton.IsEnabled = true;
            NotificationLabel.Visibility = Visibility.Hidden;
        }

        private void StagedArticleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveArticlesFromStageButton.IsEnabled = true;
        }

        private void CustomerChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCustomersDisplayedArticles();
            if (AreAnyArticlesStaged)
                CreateRentalButton.IsEnabled = true;
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
    }
}
