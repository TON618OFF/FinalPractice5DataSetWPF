﻿using System;
using System.Collections.Generic;
using System.Data;
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
using MaterialDesignThemes.Wpf;
using Practice5.DataSet1TableAdapters;

namespace Practice5
{
    /// <summary>
    /// Логика взаимодействия для AdminPageBooks.xaml
    /// </summary>
    public partial class AdminPageBooks : Page
    {
        BooksTableAdapter books = new BooksTableAdapter();
        QueriesTableAdapter backups = new QueriesTableAdapter();
        AuthorsTableAdapter authors = new AuthorsTableAdapter();
        GenreTableAdapter genre = new GenreTableAdapter();
        PublishingHouseTableAdapter publishingHouse = new PublishingHouseTableAdapter();
        CoverTableAdapter cover = new CoverTableAdapter();
        QuantityPagesTableAdapter quantityPages = new QuantityPagesTableAdapter();  
        public AdminPageBooks()
        {
            InitializeComponent();
            pole4.ItemsSource = authors.GetData();
            pole4.DisplayMemberPath = "AuthorSurname";
            pole5.ItemsSource = genre.GetData();
            pole5.DisplayMemberPath = "Genre";
            pole6.ItemsSource = publishingHouse.GetData();
            pole6.DisplayMemberPath = "PublishHouse";
            pole7.ItemsSource = cover.GetData();
            pole7.DisplayMemberPath = "Cover";
            pole8.ItemsSource = quantityPages.GetData();
            pole8.DisplayMemberPath = "Pages";
            dg_BD.ItemsSource = books.GetData();
        }

        private void dg_BD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_BD.SelectedItem != null)
            {
                DataRowView row = dg_BD.SelectedItem as DataRowView;
                if (row != null)
                {
                    pole1.Text = row.Row["Title"].ToString();
                    pole2.Text = row.Row["PublishDate"].ToString();
                    pole3.Text = row.Row["Price"].ToString();
                }
                if (dg_BD.SelectedItem is DataRowView selectedRow)
                {
                    int authorID = Convert.ToInt32(selectedRow["Author_ID"]);
                    foreach (DataRowView item in pole4.Items)
                    {
                        if (Convert.ToInt32(item["ID_Author"]) == authorID)
                        {
                            pole4.SelectedItem = item; 
                            break;
                        }
                    }
                }
                if (dg_BD.SelectedItem is DataRowView selectedRow1)
                {
                    int genreID = Convert.ToInt32(selectedRow1["Genre_ID"]);
                    foreach (DataRowView item in pole5.Items)
                    {
                        if (Convert.ToInt32(item["ID_Genre"]) == genreID)
                        {
                            pole5.SelectedItem = item;
                            break;
                        }
                    }
                }
                if (dg_BD.SelectedItem is DataRowView selectedRow2)
                {
                    int publishhouseID = Convert.ToInt32(selectedRow2["PublishHouse_ID"]);
                    foreach (DataRowView item in pole6.Items)
                    {
                        if (Convert.ToInt32(item["ID_PublishHouse"]) == publishhouseID)
                        {
                            pole6.SelectedItem = item;
                            break;
                        }
                    }
                }
                if (dg_BD.SelectedItem is DataRowView selectedRow3)
                {
                    int coverID = Convert.ToInt32(selectedRow3["Cover_ID"]);
                    foreach (DataRowView item in pole7.Items)
                    {
                        if (Convert.ToInt32(item["ID_Cover"]) == coverID)
                        {
                            pole7.SelectedItem = item;
                            break;
                        }
                    }
                }
                if (dg_BD.SelectedItem is DataRowView selectedRow4)
                {
                    int quantitypagesID = Convert.ToInt32(selectedRow4["QuantityPages_ID"]);
                    foreach (DataRowView item in pole8.Items)
                    {
                        if (Convert.ToInt32(item["ID_QuantityPages"]) == quantitypagesID)
                        {
                            pole8.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                books.DeleteQuery(Convert.ToInt32(id));
                dg_BD.ItemsSource = books.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                books.UpdateQuery(pole1.Text, pole2.Text, Convert.ToDecimal(pole3.Text), Convert.ToInt32(pole4.Text), Convert.ToInt32(pole5.Text), Convert.ToInt32(pole6.Text), Convert.ToInt32(pole7.Text), Convert.ToInt32(pole8.Text), Convert.ToInt32(id));
                dg_BD.ItemsSource = books.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;

            }
            catch
            {
                MessageBox.Show("Не трожь внешние ключи!");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            books.InsertQuery(pole1.Text, pole2.Text, Convert.ToDecimal(pole3.Text), Convert.ToInt32(pole4.Text), Convert.ToInt32(pole5.Text), Convert.ToInt32(pole6.Text), Convert.ToInt32(pole7.Text), Convert.ToInt32(pole8.Text));
            dg_BD.ItemsSource = books.GetData();
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
            dg_BD.Columns[4].Visibility = Visibility.Collapsed;
            dg_BD.Columns[5].Visibility = Visibility.Collapsed;
            dg_BD.Columns[6].Visibility = Visibility.Collapsed;
            dg_BD.Columns[7].Visibility = Visibility.Collapsed;
            dg_BD.Columns[8].Visibility = Visibility.Collapsed;
        }

    }
}
