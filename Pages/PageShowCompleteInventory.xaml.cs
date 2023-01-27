using Rent_A_Ski.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for PageShowCompleteInventory.xaml
    /// </summary>
    public partial class PageShowCompleteInventory : Page
    {
        public ObservableCollection<Article> ListOfCompleteInventory { get; set; }

        public Article SelectedArticle { get; set; }

        public PageShowCompleteInventory()
        {
            DataContext = this;
            InitializeData();
            InitializeComponent();
        }

        private void InitializeData()
        {
            Article.RefreshListOfArticles();
            ListOfCompleteInventory = Article.ListOfArticles;
        }
    }
}
