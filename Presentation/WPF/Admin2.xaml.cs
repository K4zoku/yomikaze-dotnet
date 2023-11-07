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
    /// Interaction logic for Admin2.xaml
    /// </summary>
    public partial class Admin2 : Window
    {
        public Admin2()
        {
            InitializeComponent();
        }

        private void btn_Create(object sender, RoutedEventArgs e)
        {
            Genre objGenre = new Genre();
            objGenre.Show();
        }

        private void btn_LogOut(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You Log Out!!!");
        }

        private void bntDelete(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You Delete!!!");
        }
    }
}
