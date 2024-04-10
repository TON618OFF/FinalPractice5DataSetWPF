using System;
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
                int AUTHORID = 0;
                int GENREID = 0;
                int PUBLISHHOUSE = 0;
                int COVER = 0;
                int QUANTITYPAGES = 0;
                if (!row.Row.IsNull("Author_ID"))
                {
                    AUTHORID = Convert.ToInt32(row.Row["Author_ID"]);
                }
                foreach (DataRowView item in pole4.Items)
                {
                    if (!item.Row.IsNull("ID_Author") && Convert.ToInt32(item.Row["ID_Author"]) == AUTHORID)
                    {
                        pole4.SelectedItem = item;
                        break;
                    }
                }
                if (!row.Row.IsNull("Genre_ID"))
                {
                    GENREID = Convert.ToInt32(row.Row["Genre_ID"]);
                }
                foreach (DataRowView item in pole5.Items)
                {
                    if (!item.Row.IsNull("ID_Genre") && Convert.ToInt32(item.Row["ID_Genre"]) == GENREID)
                    {
                        pole5.SelectedItem = item;
                        break;
                    }
                }
                if (!row.Row.IsNull("PublishHouse_ID"))
                {
                    PUBLISHHOUSE = Convert.ToInt32(row.Row["PublishHouse_ID"]);
                }
                foreach (DataRowView item in pole6.Items)
                {
                    if (!item.Row.IsNull("ID_PublishHouse") && Convert.ToInt32(item.Row["ID_PublishHouse"]) == PUBLISHHOUSE)
                    {
                        pole6.SelectedItem = item;
                        break;
                    }
                }
                if (!row.Row.IsNull("Cover_ID"))
                {
                    COVER = Convert.ToInt32(row.Row["Cover_ID"]);
                }
                foreach (DataRowView item in pole7.Items)
                {
                    if (!item.Row.IsNull("ID_Cover") && Convert.ToInt32(item.Row["ID_Cover"]) == COVER)
                    {
                        pole7.SelectedItem = item;
                        break;
                    }
                }
                if (!row.Row.IsNull("QuantityPages_ID"))
                {
                    QUANTITYPAGES = Convert.ToInt32(row.Row["QuantityPages_ID"]);
                }
                foreach (DataRowView item in pole8.Items)
                {
                    if (!item.Row.IsNull("ID_QuantityPages") && Convert.ToInt32(item.Row["ID_QuantityPages"]) == QUANTITYPAGES)
                    {
                        pole8.SelectedItem = item;
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
            try
            {

                if (pole4.SelectedItem is DataRowView selectedAuthor && pole5.SelectedItem is DataRowView selectedGenre && pole6.SelectedItem is DataRowView selectedPublishHouse && pole7.SelectedItem is DataRowView selectedCover && pole8.SelectedItem is DataRowView selectedQuantityPages)
                {
                    int selectedAuthorID = Convert.ToInt32(selectedAuthor["ID_Author"]);
                    int selectedGenreID = Convert.ToInt32(selectedGenre["ID_Genre"]);
                    int selectedPublishHouseID = Convert.ToInt32(selectedPublishHouse["ID_PublishHouse"]);
                    int selectedCoverID = Convert.ToInt32(selectedCover["ID_Cover"]);
                    int selectedQuantityPagesID = Convert.ToInt32(selectedQuantityPages["ID_QuantityPages"]);

                    books.InsertQuery(pole1.Text, pole2.Text, Convert.ToDecimal(pole3.Text), selectedAuthorID, selectedGenreID, selectedPublishHouseID, selectedCoverID, selectedQuantityPagesID);
                    dg_BD.ItemsSource = books.GetData();
                    dg_BD.Columns[0].Visibility = Visibility.Collapsed;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:" + ex.Message);
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }

    }
}
