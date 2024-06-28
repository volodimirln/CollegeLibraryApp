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

namespace CollegeLibraryDesktopApp.UI.Pages.Books
{
    /// <summary>
    /// Логика взаимодействия для BooksPage.xaml
    /// </summary>
    public partial class BooksPage : Page
    {
        public BooksPage()
        {
            InitializeComponent();
            lsvBooks.ItemsSource = Model.GetContext().Books.Where(p => p.Status == true).OrderByDescending(p => p.Id).Take(4).ToList();
            lblCount.Content = "4 из " + Model.GetContext().Books.Where(p => p.Status == true).Count();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddEdit.Navigate(new UI.Pages.Books.AddEditPages.AddEditBookPage(null));
        }

        private void lsvBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddEdit.Navigate(new UI.Pages.Books.AddEditPages.AddEditBookPage(lsvBooks.SelectedItem as Book));
        }
        public int take = 4;
        public int skip = 0;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

                skip += 4;
                List<Book> books = Model.GetContext().Books.Where(p => p.Status == true).OrderByDescending(p => p.Id).Skip(skip).Take(take).ToList();
            if (books.Count != 0)
            {
                lsvBooks.ItemsSource = books;
            }
            else
            {
                skip -= 4;
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (skip > 3)
            {
                skip -= 4;
                lsvBooks.ItemsSource = Model.GetContext().Books.Where(p => p.Status == true).OrderByDescending(p => p.Id).Skip(skip).Take(take).ToList();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddEdit.Navigate(new UI.Pages.Books.AddEditPages.AddEditBookPage(null));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            lsvBooks.ItemsSource = Model.GetContext().Books.Where(p => p.Status == false).OrderByDescending(p => p.Id).Skip(skip).Take(take).ToList();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbxSearch.Text))
            {
                List<Book> books = Model.GetContext().Books.Where(p => p.Status == true && p.Title.Contains(tbxSearch.Text)).OrderByDescending(p => p.Id).Skip(skip).Take(take).ToList();
                lsvBooks.ItemsSource = books;
            }
        }
    }
}
