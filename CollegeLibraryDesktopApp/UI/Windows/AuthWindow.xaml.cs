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
using System.Windows.Shapes;

namespace CollegeLibraryDesktopApp.UI.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
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
        private void btnSignIn_Click(object sender, RoutedEventArgs e)
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
                if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(tbxLogin.Text))
                {
                    User user = Model.GetContext().Users.FirstOrDefault(p => p.Email == tbxLogin.Text);
                    if (user != null && user.HashPassword == CreateMD5("xSeYsUdEZM" + password))
                    {
                        Model.GetContext().Sessions.Add(new Session() {  UserId = user.Id, DateStart = DateTime.Now});
                        Model.GetContext().SaveChanges();
                        new HomeWindow(user).Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("xSeYsUdEZM" + password);
                        MessageBox.Show(CreateMD5("xSeYsUdEZM" + password));
                        MessageBox.Show("Указаны неверные пароль или логин");
                    }
                }
                else
                {
                    MessageBox.Show("Указаны не все данные");
                }

            }
            catch (Exception ex) { MessageBox.Show("Ошибка"); }
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
