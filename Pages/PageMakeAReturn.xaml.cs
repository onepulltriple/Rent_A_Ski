using System;
using System.Collections.Generic;
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
        public PageMakeAReturn()
        {
            DataContext = this;
            InitializeData();
            InitializeComponent();
        }

        private void InitializeData()
        {

        }

        private void CustomerChanged(object sender, SelectionChangedEventArgs e)
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

        private void AvailableArticleChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ArticleStagedForReturnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
