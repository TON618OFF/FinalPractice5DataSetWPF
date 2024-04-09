﻿using Practice5.DataSet1TableAdapters;
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

namespace Practice5
{
    /// <summary>
    /// Логика взаимодействия для ClientPageAuthors.xaml
    /// </summary>
    public partial class ClientPageAuthors : Page
    {
        AuthorsTableAdapter authors = new AuthorsTableAdapter();
        public ClientPageAuthors()
        {
            InitializeComponent();
            dg_BD.ItemsSource = authors.GetData();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
