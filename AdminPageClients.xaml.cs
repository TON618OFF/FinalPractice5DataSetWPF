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
    /// Логика взаимодействия для AdminPageClients.xaml
    /// </summary>
    public partial class AdminPageClients : Page
    {
        ClientsTableAdapter clients = new ClientsTableAdapter();
        public AdminPageClients()
        {
            InitializeComponent();
            dg_BD.ItemsSource = clients.GetData();
        }

        private void dg_BD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_BD.SelectedItem != null)
            {
                DataRowView row = dg_BD.SelectedItem as DataRowView;
                if (row != null)
                {
                    pole1.Text = row.Row["ClientSurname"].ToString();
                    pole2.Text = row.Row["ClientName"].ToString();
                    pole3.Text = row.Row["ClientMiddleName"].ToString();
                    pole4.Text = row.Row["ClientPhoneNumber"].ToString();
                    pole5.Text = row.Row["ClientEmail"].ToString();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                clients.DeleteQuery(Convert.ToInt32(id));
                dg_BD.ItemsSource = clients.GetData();
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
                clients.UpdateQuery(pole1.Text, pole2.Text, pole3.Text, pole4.Text, pole5.Text, Convert.ToInt32(id));
                dg_BD.ItemsSource = clients.GetData();
            }
            catch
            {
                MessageBox.Show("Не трожь внешние ключи!");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            clients.InsertQuery(pole1.Text, pole2.Text, pole3.Text, pole4.Text, pole5.Text);
            dg_BD.ItemsSource = clients.GetData();
        }
    }
}
