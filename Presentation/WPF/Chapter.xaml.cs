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
    /// Interaction logic for Chapter.xaml
    /// </summary>
    public partial class Chapter : Window
    {
        public Chapter()
        {
            InitializeComponent();
        }

        private void ColumnDefinition_DpiChanged(object sender, DpiChangedEventArgs e)
        {

        }
    }
}
