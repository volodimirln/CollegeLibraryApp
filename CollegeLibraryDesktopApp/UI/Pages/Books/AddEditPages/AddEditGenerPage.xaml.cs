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
    /// Логика взаимодействия для AddEditGenerPage.xaml
    /// </summary>
    public partial class AddEditGenerPage : Page
    {

        public AddEditGenerPage()
        {
            InitializeComponent();
            lsvGansres.ItemsSource = Model.GetContext().Genres.OrderByDescending(p => p.Id).Take(3).ToList();
            tbxDateAdd.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.GetContext().Genres.Add(new Genre() { Title = tbxName.Text, ColorHEX = tbxColor.Text, DateAdd = DateTime.Now });
                Model.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены");
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Home.HomePage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddEditGenerPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddEditPublishingPage());
        }

        private void lsvGansres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MessageBox.Show("Удаление", "Удалить издательство из списка", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    int id = Convert.ToInt16(lsvGansres.SelectedValue);
                    Model.GetContext().Genres.Remove(Model.GetContext().Genres.FirstOrDefault(p => p.Id == id));
                    Model.GetContext().SaveChanges();
                    MessageBox.Show("Жанр удален из списка");
                }
                catch
                {
                    MessageBox.Show("Ошибка!");
                }

            }
        }
    }
}
