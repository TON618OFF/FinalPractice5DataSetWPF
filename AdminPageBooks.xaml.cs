using System;
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
        public AdminPageBooks()
        {
            InitializeComponent();
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
                    pole4.Text = row.Row["Author_ID"].ToString();
                    pole5.Text = row.Row["Genre_ID"].ToString();
                    pole6.Text = row.Row["PublishHouse_ID"].ToString();
                    pole7.Text = row.Row["Cover_ID"].ToString();
                    pole8.Text = row.Row["QuantityPages_ID"].ToString();
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
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }

    }
}
