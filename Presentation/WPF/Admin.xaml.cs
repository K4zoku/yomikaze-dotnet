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

namespace Yomikaze.WPF
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            FormComic objFormComic = new FormComic();
            objFormComic.Show();
            this.Close();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearchLoad_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Genre_Click(object sender, RoutedEventArgs e)
        {
            Admin2 objAdmin2= new Admin2();
            objAdmin2.Show();
            this.Close();
        }

        private void btn_User_Click(object sender, RoutedEventArgs e)
        {
            Admin1 objAdmin1 = new Admin1();
            objAdmin1.Show();
            this.Close();
        }

    }
}
