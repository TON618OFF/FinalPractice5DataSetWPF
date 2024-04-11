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
    /// Логика взаимодействия для AdminPageClients.xaml
    /// </summary>
    public partial class AdminPageClients : Page
    {
        ClientsTableAdapter clients = new ClientsTableAdapter();
        AuthTableAdapter auth = new AuthTableAdapter();
        public AdminPageClients()
        {
            InitializeComponent();
            pole1.PreviewTextInput += Pole1_PreviewTextInput;
            pole2.PreviewTextInput += Pole1_PreviewTextInput;
            pole3.PreviewTextInput += Pole1_PreviewTextInput;
            pole4.PreviewTextInput += Pole4_PreviewTextInput;
            pole1.PreviewKeyDown += TextBox_PreviewKeyDown;
            pole2.PreviewKeyDown += TextBox_PreviewKeyDown;
            pole3.PreviewKeyDown += TextBox_PreviewKeyDown;
            pole4.PreviewKeyDown += TextBox_PreviewKeyDown;
            pole6.ItemsSource = auth.GetData();
            pole6.DisplayMemberPath = "JustLogin";
            dg_BD.ItemsSource = clients.GetData();

        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
            {
                e.Handled = true;
            }
        }

        private void Pole4_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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
                    pole1.Text = row.Row["ClientSurname"].ToString();
                    pole2.Text = row.Row["ClientName"].ToString();
                    pole3.Text = row.Row["ClientMiddleName"].ToString();
                    pole4.Text = row.Row["ClientPhoneNumber"].ToString();
                    pole5.Text = row.Row["ClientEmail"].ToString();
                }
                if (dg_BD.SelectedItem is DataRowView selectedRow) 
                {
                    int authID = Convert.ToInt32(selectedRow["Auth_ID"]);
                    foreach (DataRowView item in pole6.Items)
                    {
                        pole6.SelectedItem = item;
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
                clients.DeleteQuery(Convert.ToInt32(id));
                dg_BD.ItemsSource = clients.GetData();
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
                clients.UpdateQuery(pole1.Text, pole2.Text, pole3.Text, pole4.Text, pole5.Text, Convert.ToInt32(pole6.Text), Convert.ToInt32(id));
                dg_BD.ItemsSource = clients.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;
            }
            catch
            {
                MessageBox.Show("Не трожь внешние ключи!");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (pole6.SelectedItem is DataRowView selectedauth)
            {
                int selectedauthID = Convert.ToInt32(selectedauth["ID_Auth"]);
                clients.InsertQuery(pole1.Text, pole2.Text, pole3.Text, pole4.Text, pole5.Text, selectedauthID);
                dg_BD.ItemsSource = clients.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
            dg_BD.Columns[6].Visibility = Visibility.Collapsed;
        }
    }
}
