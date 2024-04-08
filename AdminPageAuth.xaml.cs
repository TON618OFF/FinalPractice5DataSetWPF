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
    /// Логика взаимодействия для AdminPageAuth.xaml
    /// </summary>
    public partial class AdminPageAuth : Page
    {
        AuthTableAdapter auth = new AuthTableAdapter();
        public AdminPageAuth()
        {
            InitializeComponent();
            dg_BD.ItemsSource = auth.GetData();
        }

        private void dg_BD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_BD.SelectedItem != null)
            {
                DataRowView row = dg_BD.SelectedItem as DataRowView;
                if (row != null)
                {
                    pole1.Text = row.Row["JustLogin"].ToString();
                    pole2.Text = row.Row["JustPassword"].ToString();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                auth.DeleteQuery(Convert.ToInt32(id));
                dg_BD.ItemsSource = auth.GetData();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            auth.InsertQuery(pole1.Text, pole2.Text);
            dg_BD.ItemsSource = auth.GetData();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                auth.UpdateQuery(pole1.Text, pole2.Text, Convert.ToInt32(id));
                dg_BD.ItemsSource = auth.GetData();
            }
            catch
            {
                MessageBox.Show("Не трожь внешние ключи!");
            }
        }
    }
}
