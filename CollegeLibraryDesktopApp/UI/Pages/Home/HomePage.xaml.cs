using CollegeLibraryDesktopApp.Data.Model;
using CollegeLibraryDesktopApp.Services;
using CollegeLibraryDesktopApp.UI.Windows;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

namespace CollegeLibraryDesktopApp.UI.Pages.Home
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            try
            {
                lsvBooks.ItemsSource = new Model().Books.Where(p => p.Status == true).OrderByDescending(p => p.Id).Take(5).ToList();
                lsvOverdue.ItemsSource = Model.GetContext().ReservedBooks.Where(p => p.DateEnd == null).OrderByDescending(p => p.DateAdd).Take(1).ToList();
                stcUser.DataContext = HomeWindow.usr;
            }
            catch
            {
                Application.Current.Shutdown();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddEdit.Navigate(new UI.Pages.Books.BooksPage());
        }

        private void lsvBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddEdit.Navigate(new UI.Pages.Books.AddEditPages.AddEditBookPage(lsvBooks.SelectedItem as Book));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddEdit.Navigate(new UI.Pages.Settings.SettingsPage());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddEdit.Navigate(new UI.Pages.Books.AddEditPages.AddEditGenerPage());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                int countissuedbook = Model.GetContext().ReservedBooks.Where(p => p.DateEnd == null).Count();
                int allbook = Model.GetContext().Stocks.Count();
                
                MessageBox.Show(new Statistics().GetAllStatistics(allbook, countissuedbook));
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void lsvOverdue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReservedBook item = (lsvOverdue.SelectedItem as ReservedBook);
            AddEdit.Navigate(new UI.Pages.Readers.AddEdit.AddEditReaderPage(item.User));
        }
    }
}
