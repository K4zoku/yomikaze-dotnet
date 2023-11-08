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

        private void btn_Genre(object sender, RoutedEventArgs e)
        {
            Admin2 objAdmin2 = new Admin2();
            objAdmin2.Show();
            this.Close();
        }

        private void btn_User(object sender, RoutedEventArgs e)
        {
            Admin1 objAdmin1 = new Admin1();
            objAdmin1.Show();
            this.Close();
        }

        private void bntLogOut(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You Log Out!");
        }

        private void BtnDelete(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You Delete!!!");
        }


        private void btn_Create(object sender, RoutedEventArgs e)
        {
            FormComic objFormComic = new FormComic();
            objFormComic.Show();
            this.Close();
        }
    }
    
}
