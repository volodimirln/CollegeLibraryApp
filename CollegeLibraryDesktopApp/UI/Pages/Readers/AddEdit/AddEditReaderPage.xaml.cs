using CollegeLibraryDesktopApp.Data.Model;
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

namespace CollegeLibraryDesktopApp.UI.Pages.Readers.AddEdit
{
    /// <summary>
    /// Логика взаимодействия для AddEditReaderPage.xaml
    /// </summary>
    public partial class AddEditReaderPage : Page
    {
        private User _currentItem = new User();
        public AddEditReaderPage(User item)
        {
            InitializeComponent();
            if (item != null)
            {
                _currentItem = item;
            }
            DataContext = _currentItem;
            lsvBooks.ItemsSource = Model.GetContext().ReservedBooks.Where(p => p.UserId == _currentItem.Id && p.DateEnd == null).OrderByDescending(p => p.Id). ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_currentItem.Id == 0)
                {
                    _currentItem.DateRegistrseion = DateTime.Now;
                    _currentItem.RoleId = 2;
                }
                _currentItem.DateChange = DateTime.Now;

                Model.GetContext().Users.AddOrUpdate(_currentItem);
                Model.GetContext().SaveChanges();
                MessageBox.Show("Изменения сохранены");
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UI.Pages.Books.BooksPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UI.Pages.Home.HomePage());
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UI.Pages.Readers.ReadersPage());
        }

        private void lsvBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MessageBox.Show("Получить книгу", "Получить книгу", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    int id = Convert.ToInt16(lsvBooks.SelectedValue);
                    ReservedBook book = Model.GetContext().ReservedBooks.FirstOrDefault(p => p.Id == id);
                    book.DateEnd = DateTime.Now;
                    Model.GetContext().ReservedBooks.AddOrUpdate(book);
                    Model.GetContext().SaveChanges();
                    MessageBox.Show("Книга получена");
                }
                catch
                {
                    MessageBox.Show("Ошибка внесения данных");
                }
            }
        }
    }
}
