﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Practice5.DataSet1TableAdapters;

namespace Practice5
{
    /// <summary>
    /// Логика взаимодействия для AdminPagePublishHouse.xaml
    /// </summary>
    public partial class AdminPagePublishHouse : Page
    {
        PublishingHouseTableAdapter publishhouse = new PublishingHouseTableAdapter();
        public AdminPagePublishHouse()
        {
            InitializeComponent();
            pole1.PreviewTextInput += Pole1_PreviewTextInput;
            pole1.PreviewKeyDown += TextBox_PreviewKeyDown;
            dg_BD.ItemsSource = publishhouse.GetData();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
            {
                e.Handled = true;
            }
        }

        private void Pole1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Яa-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void dg_BD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_BD.SelectedItem != null)
            {
                DataRowView row = dg_BD.SelectedItem as DataRowView;
                if (row != null)
                {
                    pole1.Text = row.Row["PublishHouse"].ToString();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                publishhouse.DeleteQuery(Convert.ToInt32(id));
                dg_BD.ItemsSource = publishhouse.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;

            }
            catch
            {
                MessageBox.Show("Данные используются в другой таблице, удаление невозможно");
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                publishhouse.UpdateQuery(pole1.Text, Convert.ToInt32(id));
                dg_BD.ItemsSource = publishhouse.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;
            }
            catch
            {
                MessageBox.Show("Не трожь внешние ключи!");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var existingPosition = publishhouse.GetData().Any(x => x.PublishHouse == pole1.Text);

            if (existingPosition)
            {
                MessageBox.Show("Такое издательство уже существуеет, можно добавить какое-либо другое");
            }
            else
            {
                if (pole1.Text.Length < 3)
                {
                    MessageBox.Show("Увы, но название издательство должно иметь минимум 3 буквы!");
                }
                else
                {
                    publishhouse.InsertQuery(pole1.Text);
                    dg_BD.ItemsSource = publishhouse.GetData();
                    dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
