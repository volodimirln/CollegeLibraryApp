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

namespace CollegeLibraryDesktopApp.UI.UserControllers.BookItem
{
    /// <summary>
    /// Логика взаимодействия для BookItemUC.xaml
    /// </summary>
    public partial class BookItemUC : UserControl
    {
        public static readonly DependencyProperty BookItem = DependencyProperty.Register("book", typeof(Book), typeof(BookItemUC));
        public  Book book { get { return (Book)GetValue(BookItem); } set { SetValue(BookItem, value); } }
        public BookItemUC()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            imgTest.Source = new BitmapImage(new Uri("/UI/Images/6189288048.jpg", UriKind.RelativeOrAbsolute));
        }
    }
}
