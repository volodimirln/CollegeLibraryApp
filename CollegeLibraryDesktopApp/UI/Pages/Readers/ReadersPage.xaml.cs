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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CollegeLibraryDesktopApp.UI.Pages.Readers
{
    /// <summary>
    /// Логика взаимодействия для ReadersPage.xaml
    /// </summary>
    public partial class ReadersPage : Page
    {
        public ReadersPage()
        {
            InitializeComponent();
            lsvReaders.ItemsSource = Model.GetContext().Users.OrderByDescending(u => u.Id).Where(p => p.Status == true && p.RoleId == 2).Take(7).ToList();
            lblCount.Content = "7 из " + Model.GetContext().Users.OrderByDescending(u => u.Id).Where(p => p.Status == true && p.RoleId == 2).Count();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddEdit.Navigate(new UI.Pages.Readers.AddEdit.AddEditReaderPage(null));
        }

        private void lsvBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddEdit.Navigate(new UI.Pages.Readers.AddEdit.AddEditReaderPage((lsvReaders.SelectedItem as User)));

        }
        public int take = 7;
        public int skip = 0;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            skip += 7;
            List<User> item = Model.GetContext().Users.OrderByDescending(u => u.Id).Where(p => p.Status == true && p.RoleId == 2).Skip(skip).Take(take).ToList();
            if (item.Count != 0)
            {
                lsvReaders.ItemsSource = item;
            }
            else
            {
                skip -= 7;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            skip -= 7;
            if (skip >= 0)
            {
                lsvReaders.ItemsSource = Model.GetContext().Users.OrderByDescending(u => u.Id).Where(p => p.Status == true && p.RoleId == 2).Skip(skip).Take(take).ToList();
            }
            else
            {
                skip = 0;
                lsvReaders.ItemsSource = Model.GetContext().Users.OrderByDescending(u => u.Id).Where(p => p.Status == true && p.RoleId == 2).Skip(skip).Take(take).ToList();
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddEdit.Navigate(new UI.Pages.Readers.AddEdit.AddEditReaderPage(null));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            lsvReaders.ItemsSource = Model.GetContext().Users.OrderByDescending(u => u.Id).Where(p => p.Status == false && p.RoleId == 2).Skip(skip).Take(take).ToList();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbxSearch.Text))
            {
                List<User> item = Model.GetContext().Users.OrderByDescending(u => u.Id).Where(p => p.Status == true && p.RoleId == 2 && p.Surname.Contains(tbxSearch.Text)).Skip(skip).Take(take).ToList();
                lsvReaders.ItemsSource = item;
            }
        }
    }
}
