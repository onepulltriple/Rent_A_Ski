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
    /// Interaction logic for PageAddEditRemoveEmployees.xaml
    /// </summary>
    public partial class PageAddEditRemoveEmployees : Page
    {
        public ObservableCollection<Employee> FullListOfEmployees
        {
            get => Employee.ListOfEmployees;
        }

        public Employee SelectedEmployee { get; set; }

        public PageAddEditRemoveEmployees()
        {
            DataContext = this;
            InitializeData();
            InitializeComponent();
        }

        public void InitializeData()
        {
            Employee.RefreshListOfEmployees();
        }
    }
}
