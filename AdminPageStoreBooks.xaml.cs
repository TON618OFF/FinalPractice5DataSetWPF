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
    /// Логика взаимодействия для AdminPageStoreBooks.xaml
    /// </summary>
    public partial class AdminPageStoreBooks : Page
    {
        StoreBooksTableAdapter storebooks = new StoreBooksTableAdapter();
        QueriesTableAdapter backups = new QueriesTableAdapter();
        BooksTableAdapter books = new BooksTableAdapter();
        StoreTableAdapter store = new StoreTableAdapter();
        public AdminPageStoreBooks()
        {
            InitializeComponent();
            pole2.ItemsSource = books.GetData();
            pole2.DisplayMemberPath = "Title";
            pole3.ItemsSource = store.GetData();
            pole3.DisplayMemberPath = "StoreName";
            dg_BD.ItemsSource = storebooks.GetData();

        }

        private void dg_BD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_BD.SelectedItem != null)
            {
                DataRowView row = dg_BD.SelectedItem as DataRowView;
                if (row != null)
                {
                    pole1.Text = row.Row["BooksAmount"].ToString();
                    pole2.Text = row.Row["Book_ID"].ToString();
                    pole3.Text = row.Row["Store_ID"].ToString();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                storebooks.DeleteQuery(Convert.ToInt32(id));
                dg_BD.ItemsSource = storebooks.GetData();
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
                storebooks.UpdateQuery(Convert.ToInt32(pole1.Text), Convert.ToInt32(pole2.Text), Convert.ToInt32(pole3.Text), Convert.ToInt32(id));
                dg_BD.ItemsSource = storebooks.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;

            }
            catch
            {
                MessageBox.Show("Не трожь внешние ключи!");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            storebooks.InsertQuery(Convert.ToInt32(pole1.Text), Convert.ToInt32(pole2.Text), Convert.ToInt32(pole3.Text));
            dg_BD.ItemsSource = storebooks.GetData();
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
            dg_BD.Columns[2].Visibility = Visibility.Collapsed;
            dg_BD.Columns[3].Visibility = Visibility.Collapsed;
        }

    }
}
