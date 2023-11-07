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

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Genre objGenre= new Genre();
            objGenre.Show();
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
    }
}
