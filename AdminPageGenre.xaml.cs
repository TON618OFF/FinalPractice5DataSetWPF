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
using static Practice5.DataSet1;

namespace Practice5
{
    /// <summary>
    /// Логика взаимодействия для AdminPageGenre.xaml
    /// </summary>
    public partial class AdminPageGenre : Page
    {
        GenreTableAdapter genre = new GenreTableAdapter();
        public AdminPageGenre()
        {
            InitializeComponent();
            pole1.PreviewTextInput += Pole1_PreviewTextInput;
            dg_BD.ItemsSource = genre.GetData();

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
                    pole1.Text = row.Row["Genre"].ToString();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                genre.DeleteQuery(Convert.ToInt32(id));
                dg_BD.ItemsSource = genre.GetData();
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
                genre.UpdateQuery(pole1.Text, Convert.ToInt32(id));
                dg_BD.ItemsSource = genre.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;

            }
            catch
            {
                MessageBox.Show("Не трожь внешние ключи!");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string genreName = pole1.Text;

            if (genreName.Length > 3)
            {
                genre.InsertQuery(pole1.Text);
                dg_BD.ItemsSource = genre.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Длина жанра должна быть больше 3 символов");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void ImportJson_Click(object sender, RoutedEventArgs e)
        {
            List<Genre> forImport = Deser.DeserializeObject<List<Genre>>();
            foreach (var item in forImport)
            {
                genre.InsertQuery(item.genre);
            }
            dg_BD.ItemsSource = null;
            dg_BD.ItemsSource = genre.GetData();
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
