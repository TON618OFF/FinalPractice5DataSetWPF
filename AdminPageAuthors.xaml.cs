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
using Practice5.DataSet1TableAdapters;

namespace Practice5
{
    /// <summary>
    /// Логика взаимодействия для AdminPageAuthors.xaml
    /// </summary>
    public partial class AdminPageAuthors : Page
    {
        AuthorsTableAdapter authors = new AuthorsTableAdapter();
        QueriesTableAdapter backups = new QueriesTableAdapter();
        public AdminPageAuthors()
        {
            InitializeComponent();
            pole1.PreviewTextInput += Pole1_PreviewTextInput;
            pole2.PreviewTextInput += Pole1_PreviewTextInput;
            pole3.PreviewTextInput += Pole1_PreviewTextInput;
            dg_BD.ItemsSource = authors.GetData();
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
                    pole1.Text = row.Row["AuthorSurname"].ToString();
                    pole2.Text = row.Row["AuthorName"].ToString();
                    pole3.Text = row.Row["AuthorMiddleName"].ToString();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                authors.DeleteQuery(Convert.ToInt32(id));
                dg_BD.ItemsSource = authors.GetData();
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
                authors.UpdateQuery(pole1.Text, pole2.Text, pole3.Text, Convert.ToInt32(id));
                dg_BD.ItemsSource = authors.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;

            }
            catch
            {
                MessageBox.Show("Не трожь внешние ключи!");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            


            authors.InsertQuery(pole1.Text, pole2.Text, pole3.Text);
            dg_BD.ItemsSource = authors.GetData();
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void ImportJson_Click(object sender, RoutedEventArgs e)
        {
            List<Authors> forImport = Deser.DeserializeObject<List<Authors>>();
            foreach (var item in forImport)
            {
                authors.InsertQuery(item.AuthorSurname, item.AuthorName, item.AuthorMiddleName);
            }
            dg_BD.ItemsSource = null;
            dg_BD.ItemsSource = authors.GetData();
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;

        }
    }
}
