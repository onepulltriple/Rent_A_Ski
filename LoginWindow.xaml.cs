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
using System.Windows.Shapes;
using Rent_A_Ski.Models;

namespace Rent_A_Ski
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public Login Credentials { get; set; } = new();

        public LoginWindow()
        {
            InitializeComponent();
            DataContext = this;

            //string testpw = BCrypt.Net.BCrypt.HashPassword("skiski");
        }

        private void StartLogin(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Credentials.Username) ||
                String.IsNullOrEmpty(Credentials.Password))
                return;

            bool result = new SQLController().AreCredentialsOK(Credentials.Username, Credentials.Password);

            if (result)
            {
                MainWindow NewMainWindow = new();
                NewMainWindow.Show();
                this.Close();
            }
            else
            {
                LoginFailedMessage.Visibility = Visibility.Visible;
            }
        }
        

        #region Make "Credentials invalid." message disappear.
        private void UsernameTextBoxGetsFocus(object sender, RoutedEventArgs e)
        {
            LoginFailedMessage.Visibility = Visibility.Hidden;
        }

        private void PasswordTextBoxGetsFocus(object sender, RoutedEventArgs e)
        {
            LoginFailedMessage.Visibility = Visibility.Hidden;
        }
        #endregion

    }
}
