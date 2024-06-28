using CollegeLibraryDesktopApp.Data.Model;
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

namespace CollegeLibraryDesktopApp.UI.Windows
{
    /// <summary>
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public static User usr { set; get; }
        public HomeWindow(User user)
        {
            InitializeComponent();
            usr = user;
            MessageBox.Show($"Добро пожаловать, {usr.Name} {usr.Surname}");
            MainFrame.Navigate(new UI.Pages.Home.HomePage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UI.Pages.Home.HomePage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UI.Pages.Books.BooksPage());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UI.Pages.Readers.ReadersPage());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UI.Pages.Settings.SettingsPage());
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UI.Pages.Home.HomePage());
        }
    }
}
