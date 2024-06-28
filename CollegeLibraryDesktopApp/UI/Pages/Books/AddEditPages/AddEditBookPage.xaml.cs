using System;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CollegeLibraryDesktopApp.Data.Model;
using CollegeLibraryDesktopApp.UI.Windows;
using Microsoft.Win32;

namespace CollegeLibraryDesktopApp.UI.Pages.Books.AddEditPages
{
    /// <summary>
    /// Логика взаимодействия для AddEditBookPage.xaml
    /// </summary>
    public partial class AddEditBookPage : Page
    {
        private Book _currentItem = new Book();

        public AddEditBookPage(Book item)
        {
            InitializeComponent();
            if (item != null)
            {
                _currentItem = item;
            }
            else
            {
                _currentItem.Title = "Впишите название";
                System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#787976");
                SolidColorBrush brush = new SolidColorBrush(color);
                tbxTitle.Foreground = brush;
                _currentItem.DateAdd = DateTime.Now;
                _currentItem.GenreId = 1;
                _currentItem.PublishingId = 1;
                _currentItem.Status = true;
                _currentItem.ISBNCode = "001";
            }
            DataContext = _currentItem;
            cmbGeners.ItemsSource = Model.GetContext().Genres.ToList();
            cmbPublishing.ItemsSource = Model.GetContext().PublishingCompanies.ToList();

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Home.HomePage());
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.GetContext().Books.AddOrUpdate(_currentItem);
                Model.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены");
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения данных");
            }
        }

        private void textChange_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnSave.Visibility = Visibility.Visible;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.Visibility = Visibility.Hidden;

        }

        private void btnChangeCoverPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                string filename = dlg.FileName;
                try
                {
                    Bitmap bitmap = new Bitmap(filename);
                    System.Drawing.Image img = new System.Drawing.Bitmap(filename);
                    _currentItem.CoverImage = ImageToArray(bitmap);
                    var image = new Bitmap(filename);
                    string fl = System.IO.Path.GetFileName(filename);
                    image.Save("\\" + fl);
                    MessageBox.Show("Ваш файл: " + filename);
                    btnSave.Visibility = Visibility.Visible;
                }
                catch
                {
                    MessageBox.Show("Неверный формат книги");
                }
            }
        }
        private byte[] ImageToArray(System.Drawing.Image x)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(x, typeof(byte[]));
        }

        private void cmbGeners_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSave.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_currentItem.Id != 0)
            {
                MainFrame.Navigate(new AddReservedPage(_currentItem));
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MessageBox.Show("Получить книгу", "Получить книгу", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    int id = Convert.ToInt16(lsvRider.SelectedValue);
                    ReservedBook book = Model.GetContext().ReservedBooks.FirstOrDefault(p => p.Id == id);
                    book.DateEnd = DateTime.Now;
                    Model.GetContext().ReservedBooks.AddOrUpdate(book);
                    Model.GetContext().SaveChanges();
                    MessageBox.Show("Книга получена");
                    lsvRider.SelectedValue = null;
                    lsvRider.ItemsSource = Model.GetContext().ReservedBooks.Where(p => p.Stock.Book.Id == _currentItem.Id && p.DateEnd == null).OrderByDescending(p => p.Id).ToList();
                    lsvRider.SelectedValue = null;
                }
                catch
                {
                    MessageBox.Show("Ошибка внесения данных");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_currentItem.Id != 0)
            {
                try
                {
                    Model.GetContext().Stocks.Add(new Stock() { BookId = _currentItem.Id, Comment = "Экземпляр книги в хорошем состоянии" });
                    Model.GetContext().SaveChanges();
                    int lastId = Model.GetContext().Stocks.OrderByDescending(p => p.Id).FirstOrDefault().Id;
                    Model.GetContext().ReservedBooks.Add(new ReservedBook() { StockId = lastId, DateAdd = DateTime.Now, DateEnd = DateTime.Now, UserId = HomeWindow.usr.Id});
                    Model.GetContext().SaveChanges();
                    MessageBox.Show("Добавлен новый экземпляр - "+ lastId);
                }
                catch
                {
                    MessageBox.Show("Ошибка!");
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddEditGenerPage());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddEditPublishingPage());

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddAuthorPage(_currentItem));
        }
    }
}
