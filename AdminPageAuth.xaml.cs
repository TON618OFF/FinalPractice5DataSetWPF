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
    /// Логика взаимодействия для AdminPageAuth.xaml
    /// </summary>
    public partial class AdminPageAuth : Page
    {
        AuthTableAdapter auth = new AuthTableAdapter();
        QueriesTableAdapter backups = new QueriesTableAdapter();
        public AdminPageAuth()
        {
            InitializeComponent();
            dg_BD.ItemsSource = auth.GetData();
            pole1.PreviewKeyDown += TextBox_PreviewKeyDown;
            pole2.PreviewKeyDown += TextBox_PreviewKeyDown;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
            {
                e.Handled = true;
            }
        }

        private void dg_BD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_BD.SelectedItem != null)
            {
                DataRowView row = dg_BD.SelectedItem as DataRowView;
                if (row != null)
                {
                    pole1.Text = row.Row["JustLogin"].ToString();
                    pole2.Password = row.Row["JustPassword"].ToString();
                }
                else
                {
                    MessageBox.Show("Не выбрано ни одно поле");
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pole2.Password.Length < 5 && pole2.Password.Length > 15 != (IsValidInput(pole2.Password)) || !IsValidInput(pole2.Password))
                {
                    MessageBox.Show("Логин и пароль должны содержать от 5 до 15 букв и цифр!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                { 
                    if (string.IsNullOrEmpty(pole1.Text) || string.IsNullOrEmpty(pole2.Password))
                    {
                        MessageBox.Show("Поля не имеют никакого внутри значения! Введите в поля данные");
                    }
                    else
                    {
                        object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                        auth.DeleteQuery(Convert.ToInt32(id));
                        dg_BD.ItemsSource = auth.GetData();
                        dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                        dg_BD.Columns[2].Visibility = Visibility.Collapsed;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно удалить данные, так как они используются в другой таблице");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (pole2.Password.Length < 5 && pole2.Password.Length > 15 != (IsValidInput(pole2.Password)) || !IsValidInput(pole2.Password))
            {
                MessageBox.Show("Логин и пароль должны содержать от 5 до 15 букв и цифр!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(pole1.Text) || string.IsNullOrEmpty(pole2.Password))
                {
                    MessageBox.Show("Поля не имеют никакого внутри значения! Введите в поля данные");
                }
                else
                {
                    auth.InsertQuery(pole1.Text, pole2.Password);
                    dg_BD.ItemsSource = auth.GetData();
                    dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                    dg_BD.Columns[2].Visibility = Visibility.Collapsed;

                }
            }
        }

        private bool IsValidInput(string input)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            return regex.IsMatch(input);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pole2.Password.Length < 5 && pole2.Password.Length > 15 != (IsValidInput(pole2.Password)) || !IsValidInput(pole2.Password))
                {
                    MessageBox.Show("Логин и пароль должны содержать от 5 до 15 букв и цифр!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(pole1.Text) || string.IsNullOrEmpty(pole2.Password))
                    {
                        MessageBox.Show("Поля не имеют никакого внутри значения! Введите в поля данные");
                    }
                    else
                    {
                        object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                        auth.UpdateQuery(pole1.Text, pole2.Password, Convert.ToInt32(id));
                        dg_BD.ItemsSource = auth.GetData();
                        dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                        dg_BD.Columns[2].Visibility = Visibility.Collapsed;

                    }
                }
            }
            catch
            {
                MessageBox.Show("Не выбрано ни одно поле!");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
            dg_BD.Columns[2].Visibility = Visibility.Collapsed;

        }

    }
}
