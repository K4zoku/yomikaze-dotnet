﻿using System;
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
    /// Interaction logic for Genre.xaml
    /// </summary>
    public partial class Genre : Window
    {
        public Genre()
        {
            InitializeComponent();
        }

        private void btnSave(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save Successfully!");
        }
    }
}