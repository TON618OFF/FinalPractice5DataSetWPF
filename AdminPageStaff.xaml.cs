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
    /// Логика взаимодействия для AdminPageStaff.xaml
    /// </summary>
    public partial class AdminPageStaff : Page
    {

        StaffTableAdapter staff = new StaffTableAdapter();
        StaffPositionTableAdapter staffpos = new StaffPositionTableAdapter();
        AuthTableAdapter auth = new AuthTableAdapter();

        public AdminPageStaff()
        {

            InitializeComponent();
            pole6.ItemsSource = staffpos.GetData();
            pole6.DisplayMemberPath = "Position";
            pole7.ItemsSource = auth.GetData();
            pole7.DisplayMemberPath = "JustLogin";
            dg_BD.ItemsSource = staff.GetData();

        }

        private void dg_BD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_BD.SelectedItem != null)
            {
                DataRowView row = dg_BD.SelectedItem as DataRowView;
                if (row != null)
                {
                    pole1.Text = row.Row["StaffSurname"].ToString();
                    pole2.Text = row.Row["StaffName"].ToString();
                    pole3.Text = row.Row["StaffMiddleName"].ToString();
                    pole4.Text = row.Row["StaffPhoneNumber"].ToString();
                    pole5.Text = row.Row["StaffEmail"].ToString();
                }
                if(dg_BD.SelectedItem is DataRowView selectedRow)
                {
                    int positionID = Convert.ToInt32(selectedRow["StaffPosition_ID"]);
                    foreach (DataRowView item in pole6.Items)
                    {
                        if (Convert.ToInt32(item["ID_StaffPosition"]) == positionID)
                        {
                            pole6.SelectedItem = item;
                            break;
                        }
                    }
                }
                if (dg_BD.SelectedItem != null && dg_BD.SelectedItem is DataRowView selectedRow3)
                {
                    if (selectedRow3.Row != null && selectedRow3.Row["Auth_ID"] != DBNull.Value)
                    {
                        int authID = Convert.ToInt32(selectedRow3.Row["Auth_ID"]);
                        foreach (DataRowView item in pole7.Items)
                        {
                            if (item.Row != null && item.Row["ID_Auth"] != DBNull.Value && Convert.ToInt32(item.Row["ID_Auth"]) == authID)
                            {
                                pole7.SelectedItem = item;
                                break;
                            }
                        }
                    }
                else
                    {
                        pole7.SelectedItem = null;
                    }
                }

                else
                {
                    pole7.SelectedItem = null;
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            string phoneNumber = pole4.Text;

            if (IsValidRussianPhoneNumber(phoneNumber))
            {
                string email = pole5.Text;
                if (IsValidEmailFormat(email))
                {
                    object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                    staff.DeleteQuery(Convert.ToInt32(id));
                    dg_BD.ItemsSource = staff.GetData();
                    dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Ошибка в почте. Проверьте корректность");
                }
            }
            else
            {
                MessageBox.Show("Ошибка в длине номера телефона");
            }

        }

        private bool IsValidRussianPhoneNumber(string phoneNumber)
        {
            // Регулярное выражение для проверки номера телефона в российском формате
            Regex regex = new Regex(@"^\+(?:[0-9] ?){6,14}[0-9]$");

            return regex.IsMatch(phoneNumber);
        }

        private bool IsValidEmailFormat(string email)
        {
            // Проверка наличия @ и точки, а также формата до @
            if (email.Contains("@") && email.Contains(".") && email.IndexOf("@") < email.LastIndexOf("."))
            {
                return true;
            }
            return false;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            string phoneNumber = new string(pole4.Text.Where(char.IsDigit).ToArray());

            if (phoneNumber.Length == 12)
            {
                string email = pole5.Text;
                if (IsValidEmailFormat(email))
                {
                    object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                    staff.UpdateQuery(pole1.Text, pole2.Text, pole3.Text, pole4.Text, pole5.Text, Convert.ToInt32(pole6.Text), Convert.ToInt32(pole7.Text), Convert.ToInt32(id));
                    dg_BD.ItemsSource = staff.GetData();
                    dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Ошибка в почте. Проверьте корректность");
                }
            }
            else
            {
                MessageBox.Show("Ошибка в длине номера телефона");
            }

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string phoneNumber = pole4.Text;
            phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            if (phoneNumber.Length == 11)
            {
                string email = pole5.Text;
                if (IsValidEmailFormat(email))
                {
                    if (pole6.SelectedItem is DataRowView selectedauth)
                    {
                        int selectedauthID = (Convert.ToInt32(selectedauth["Auth_ID"]));
                        staff.InsertQuery(pole1.Text, pole2.Text, pole3.Text, phoneNumber, email, Convert.ToInt32(pole6.Text), Convert.ToInt32(pole7.Text));
                        dg_BD.ItemsSource = staff.GetData();
                        dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка в почте. Проверьте корректность");
                }
            }
            else
            {
                MessageBox.Show("Ошибка в длине номера телефона");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
