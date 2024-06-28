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

namespace CollegeLibraryDesktopApp.UI.Pages.Books.AddEditPages
{
    /// <summary>
    /// Логика взаимодействия для AddAuthorPage.xaml
    /// </summary>
    public partial class AddAuthorPage : Page
    {
        private Book _cuurentBook;
        public AddAuthorPage(Book item)
        {
            InitializeComponent();
            _cuurentBook = item;
            lblAuthor.Content = "Добавить автора книги " + item.Title;
            lsvAuthors.ItemsSource = Model.GetContext().AuthorsInBooks.Where(p => p.AuthorId == item.Id).ToList();
            tbxDateAdd.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.GetContext().Authors.Add(new Author() { Name = tbxName.Text, Description = tbxDescription.Text, Surname = tbxSurname.Text });
                Model.GetContext().SaveChanges();
                int lastid = Model.GetContext().Authors.OrderByDescending(p => p.Id).FirstOrDefault().Id;
                int lastId = Model.GetContext().Authors.OrderByDescending(p => p.Id).FirstOrDefault().Id;
                Model.GetContext().AuthorsInBooks.Add(new AuthorsInBook() { AuthorId = lastId, BookId = _cuurentBook.Id, DateAdd = DateTime.Now });
                Model.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены");
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddEditGenerPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddAuthorPage(_cuurentBook));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddEditPublishingPage());
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddEditBookPage(_cuurentBook));
        }
    }
}
