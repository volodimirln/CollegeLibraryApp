using CollegeLibraryDesktopApp.Data.Model;
using CollegeLibraryDesktopApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для AddReservedPage.xaml
    /// </summary>
    public partial class AddReservedPage : Page
    {
        private Book _context;
        public AddReservedPage(Book item)
        {
            InitializeComponent();
            _context = item;
            DataContext = new ReservedBook();
            tbxTitle.Content = "Выдать книгу: " + item.Title;
            IssuedBooks.ItemsSource = Model.GetContext().ReservedBooks.Where(p => p.Stock.Book.Id == item.Id && p.DateEnd == null).OrderByDescending(p => p.Id).ToList();
            tbBook.Text = item.Title;
            cmbReader.ItemsSource = Model.GetContext().Users.Where(p => p.RoleId == 2 && p.Status == true).ToList();
            List<ReservedBook> resrvedBooks = Model.GetContext().ReservedBooks.ToList();
            List<Stock> stocks = Model.GetContext().Stocks.Where(p => p.BookId == item.Id).ToList();
            stocks = stocks.Where(p => resrvedBooks.Where(x => x.StockId == p.Id && x.DateEnd != null) != null).ToList();
            List<Stock> stockupd = new List<Stock>();
            foreach (Stock s in stocks)
            {
                ReservedBook rs = resrvedBooks.LastOrDefault(p => p.StockId == s.Id);
                if (rs != null)
                {
                    if (rs.DateEnd != null)
                    {
                        stockupd.Add(s);
                    }
                }
            }
            cmbCopy.ItemsSource = stockupd;
            cmbReader.SelectedIndex = 0;
            cmbCopy.SelectedIndex = 0;
            DateAdd.Text = DateTime.Now.ToString("dd.MM.yyyy");
            DateEnd.Text = DateTime.Now.AddMonths(1).ToString("dd.MM.yyyy");
            int[] statistics = new Statistics().GetAllIssuedStatistics(Model.GetContext().Stocks.Where(p => p.BookId == item.Id).Count(), Model.GetContext().ReservedBooks.Where(p => p.Stock.Book.Id == item.Id && p.DateEnd == null).Count());
            int all = Model.GetContext().Stocks.Where(p => p.BookId == item.Id).Count();
            int issued = Model.GetContext().ReservedBooks.Where(p => p.Stock.Book.Id == item.Id && p.DateEnd == null).Count();
            lblAllCopies.Content = statistics[0].ToString() + " штук.";
            lblIssueCopies.Content = statistics[1].ToString() + " штук.";
            lblReserverCopies.Content = statistics[2].ToString() + " штук.";
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddEditBookPage(_context));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.GetContext().ReservedBooks.Add(new ReservedBook() { DateAdd = DateTime.Now, StockId = (cmbCopy.SelectedItem as Stock).Id, UserId = (cmbReader.SelectedItem as User).Id });
                Model.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены");
            }
            catch 
            {
                MessageBox.Show("Ошибка сохранения");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate (new AddEditGenerPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddAuthorPage(_context));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddEditPublishingPage());
        }
    }
}
