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
using Rent_A_Ski.Models;
using Rent_A_Ski.Pages;

namespace Rent_A_Ski
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowCompleteInventory(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = new PageShowCompleteInventory();
        }

        private void ShowAllCustomers(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = new PageShowAllCustomers();
        }

        private void AddEditRemoveEmployees(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = new PageAddEditRemoveEmployees();
        }

        private void ShowCompleteRentalHistory(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = new PageShowCompleteRentalHistory();
        }

        private void MakeARental(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = new PageMakeARental();
        }

        private void ExitProgram(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
