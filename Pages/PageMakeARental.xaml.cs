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
        public ObservableCollection<Article> ListOfAvailableArticles { get; set; }

        public ObservableCollection<Customer> FullListOfCustomers
        {
            get => Customer.ListOfCustomers;
        }

        public ObservableCollection<Article> StagedArticlesList { get; set; } = new();

        public Article SelectedAvailableArticle { get; set; }

        public Article SelectedStagedArticle { get; set; }
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

            var tempList = Article.ListOfArticles.
                Where(x => x.Status.Description == "Available").ToList();
            ListOfAvailableArticles = new ObservableCollection<Article>(tempList);
        }

        private void CreateRental(object sender, RoutedEventArgs e)
        {
            
        }


        private void AddArticlesToStage(object sender, RoutedEventArgs e)
        {
            if (SelectedAvailableArticle != null)
            {
                StagedArticlesList.Add(SelectedAvailableArticle);
                ListOfAvailableArticles.Remove(SelectedAvailableArticle);
            }
        }

        private void RemoveArticlesFromStage(object sender, RoutedEventArgs e)
        {
            if (SelectedStagedArticle != null)
            {
                ListOfAvailableArticles.Add(SelectedStagedArticle);
                StagedArticlesList.Remove(SelectedStagedArticle);
            }
        }

        private void CustomerChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
