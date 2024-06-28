using CollegeLibraryDesktopApp.Data.Model;
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

namespace CollegeLibraryDesktopApp.UI.Pages.Settings
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }
        public static string CreateMD5(string s)
        {
            using (var provider = System.Security.Cryptography.MD5.Create())
            {
                StringBuilder builder = new StringBuilder();

                foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(s)))
                    builder.Append(b.ToString("x2").ToLower());

                return builder.ToString();
            }

        }
        private void btnSavePassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string password = "";
                if (chShowPassword.IsChecked != null)
                {
                    if (chShowPassword.IsChecked == true)
                    {
                        password = tbxPassword.Text;
                    }
                    else
                    {
                        password = pswPassword.Password;
                    }
                }
                else
                {
                    password = pswPassword.Password;
                }
                if (password == tbxRepetPassword.Password)
                {
                    HomeWindow.usr.HashPassword = CreateMD5("xSeYsUdEZM" + password);
                    Model.GetContext().Users.AddOrUpdate(HomeWindow.usr);
                    Model.GetContext().SaveChanges();
                    MessageBox.Show("Пароль обновлен");
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void chShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (chShowPassword.IsChecked != null)
            {
                if (chShowPassword.IsChecked == true)
                {
                    tbxPassword.Text = pswPassword.Password;
                    pswPassword.Visibility = Visibility.Collapsed;
                }
                else
                {
                    pswPassword.Password = tbxPassword.Text;
                    pswPassword.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
