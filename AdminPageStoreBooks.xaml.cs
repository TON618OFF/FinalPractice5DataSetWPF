using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
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
using static Practice5.DataSet1;

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
                int BookID = 0;
                int StoreID = 0;
                DataRowView row = dg_BD.SelectedItem as DataRowView;
                if (row != null)
                {
                    pole1.Text = row.Row["BooksAmount"].ToString();
                    pole2.Text = row.Row["Book_ID"].ToString();
                    pole3.Text = row.Row["Store_ID"].ToString();
                }
                if (!row.Row.IsNull("Book_ID"))
                {
                    BookID = Convert.ToInt32(row.Row["Book_ID"]);
                }
                foreach (DataRowView item in pole2.Items)
                {
                    if (!item.Row.IsNull("ID_Book") && Convert.ToInt32(item.Row["ID_Book"]) == BookID)
                    {
                        pole2.SelectedItem = item;
                        break;
                    }
                }
                if (!row.Row.IsNull("Store_ID"))
                {
                    StoreID = Convert.ToInt32(row.Row["Store_ID"]);
                }
                foreach (DataRowView item in pole3.Items)
                {
                    if (!item.Row.IsNull("ID_Store") && Convert.ToInt32(item.Row["ID_Store"]) == StoreID)
                    {
                        pole3.SelectedItem = item;
                        break;
                    }
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
                MessageBox.Show("Данные используются в другой таблице");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (pole1.Text != null && pole2.Text != null && pole3.Text != null)
            {
                var existingBook = books.GetData().Any(x => x.ID_Book == Convert.ToInt32(pole1.Text));

                if (existingBook)
                {
                    MessageBox.Show("Данные о наличии этой книги уже находятся в таблице!");
                }
                else
                {
                    storebooks.InsertQuery(Convert.ToInt32(pole1.Text), Convert.ToInt32(pole2.Text), Convert.ToInt32(pole3.Text));
                    dg_BD.ItemsSource = storebooks.GetData();
                    dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBox.Show("Извините, но вы ничего не ввели");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }

    }
}
