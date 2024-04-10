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
    /// Логика взаимодействия для AdminPageQuantityPages.xaml
    /// </summary>
    public partial class AdminPageQuantityPages : Page
    {
        QuantityPagesTableAdapter quantitypages = new QuantityPagesTableAdapter();
        public AdminPageQuantityPages()
        {
            InitializeComponent();
            pole1.PreviewTextInput += Pole1_PreviewTextInput;
            dg_BD.ItemsSource = quantitypages.GetData();

        }

        private void Pole1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^\d+$");
            if (!string.IsNullOrEmpty(pole1.Text) && !pole1.Text.StartsWith("0") && regex.IsMatch(pole1.Text))
            {
                Regex regexoriginal = new Regex("[^0-9]+");
                e.Handled = regexoriginal.IsMatch(e.Text);
            }
            else
            {
                MessageBox.Show("Извините, но число либо начинается с нуля, либо поле пустое!");
            }
        }

        private void dg_BD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_BD.SelectedItem != null)
            {
                DataRowView row = dg_BD.SelectedItem as DataRowView;
                if (row != null)
                {
                    pole1.Text = row.Row["Pages"].ToString();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                quantitypages.DeleteQuery(Convert.ToInt32(id));
                dg_BD.ItemsSource = quantitypages.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Эти данные используются в другой таблице!");
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                quantitypages.UpdateQuery(Convert.ToInt32(pole1.Text), Convert.ToInt32(id));
                dg_BD.ItemsSource = quantitypages.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;

            }
            catch
            {
                MessageBox.Show("Не трожь внешние ключи!");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            var existingPosition = quantitypages.GetData().Any(x => x.Pages == Convert.ToInt32(pole1.Text));
            if (existingPosition)
            {
                MessageBox.Show("Такое количество страниц уже существует в этой таблице, добавьте другое.");
            }
            else
            {

                quantitypages.InsertQuery(Convert.ToInt32(pole1.Text));
                dg_BD.ItemsSource = quantitypages.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
