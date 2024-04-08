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
    /// Логика взаимодействия для AdminPageQuantityPages.xaml
    /// </summary>
    public partial class AdminPageQuantityPages : Page
    {
        QuantityPagesTableAdapter quantitypages = new QuantityPagesTableAdapter();
        public AdminPageQuantityPages()
        {
            InitializeComponent();
            dg_BD.ItemsSource = quantitypages.GetData();
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
                quantitypages.UpdateQuery(Convert.ToInt32(pole1.Text), Convert.ToInt32(id));
                dg_BD.ItemsSource = quantitypages.GetData();
            }
            catch
            {
                MessageBox.Show("Не трожь внешние ключи!");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            quantitypages.InsertQuery(Convert.ToInt32(pole1.Text));
            dg_BD.ItemsSource = quantitypages.GetData();
        }
    }
}
