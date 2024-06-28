using CollegeLibraryDesktopApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
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
    /// Логика взаимодействия для AddEditPublishingPage.xaml
    /// </summary>
    public partial class AddEditPublishingPage : Page
    {
        public AddEditPublishingPage()
        {
            InitializeComponent();
            lsvPublishiing.ItemsSource = Model.GetContext().PublishingCompanies.OrderByDescending(x => x.Id).Take(3).ToList();
            tbxDateAdd.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PublishingCompany pc = new PublishingCompany() { Name = tbxName.Text, Description = tbxDescription.Text, Genre = tbxGener.Text };
                string filename = "UI/Images/brand.png";
                string path = System.IO.Path.GetFullPath("/UI/Images/Icons/brand.png");
                bool k = File.Exists("UI/Images/brand.png");
                Bitmap bitmap = new Bitmap(filename);
                System.Drawing.Image img = new System.Drawing.Bitmap(filename);
                pc.Logo = ImageToArray(bitmap);


                var image = new Bitmap(filename);
                string fl = System.IO.Path.GetFileName(filename);
                image.Save("\\" + fl);

                Model.GetContext().PublishingCompanies.Add(pc);
                Model.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены");
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
        private byte[] ImageToArray(System.Drawing.Image x)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(x, typeof(byte[]));
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

        private void lsvPublishiing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MessageBox.Show("Удаление", "Удалить издательство из списка", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    int id = Convert.ToInt16(lsvPublishiing.SelectedValue);
                    Model.GetContext().PublishingCompanies.Remove(Model.GetContext().PublishingCompanies.FirstOrDefault(p => p.Id == id));
                    Model.GetContext().SaveChanges();
                    MessageBox.Show("Издательство удалено из списка");
                }
                catch
                {
                    MessageBox.Show("Ошибка!");
                }

            }
        }
    }
}
