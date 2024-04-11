using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для AdminPageOrderCheck.xaml
    /// </summary>
    public partial class AdminPageOrderCheck : Page
    {
        OrderCheckTableAdapter ordercheck = new OrderCheckTableAdapter();
        QueriesTableAdapter backups = new QueriesTableAdapter();
        ClientsTableAdapter clients = new ClientsTableAdapter();
        StaffTableAdapter staff = new StaffTableAdapter();
        StoreBooksTableAdapter storebook = new StoreBooksTableAdapter();
        public AdminPageOrderCheck()
        {
            InitializeComponent();
            pole1.PreviewKeyDown += TextBox_PreviewKeyDown;
            pole2.PreviewKeyDown += TextBox_PreviewKeyDown;
            pole3.PreviewKeyDown += TextBox_PreviewKeyDown;
            pole4.PreviewKeyDown += TextBox_PreviewKeyDown;
            pole5.PreviewKeyDown += TextBox_PreviewKeyDown;
            dg_BD.ItemsSource = ordercheck.GetData();
            pole3.ItemsSource = staff.GetData();
            pole3.DisplayMemberPath = "StaffName";
            pole4.ItemsSource = storebook.GetData();
            pole4.DisplayMemberPath = "BooksAmount";
            pole5.ItemsSource = clients.GetData();
            pole5.DisplayMemberPath = "ClientName";

        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
            {
                e.Handled = true;
            }
        }

        private void dg_BD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_BD.SelectedItem != null)
            {
                int StaffID = 0;
                int StoreBookID = 0;
                int ClientID = 0;
                DataRowView row = dg_BD.SelectedItem as DataRowView;
                if (row != null)
                {

                    pole1.Text = row.Row["OrderDate"].ToString();
                    pole2.Text = row.Row["OrderNumber"].ToString();

                    if (!row.Row.IsNull("Staff_ID"))
                    {
                        StaffID = Convert.ToInt32(row.Row["Staff_ID"]);
                    }
                    foreach (DataRowView item in pole3.Items)
                    {
                        if (!item.Row.IsNull("ID_Staff") && Convert.ToInt32(item.Row["ID_Staff"]) == StaffID)
                        {
                            pole3.SelectedItem = item;
                            break;
                        }
                    }
                    if (!row.Row.IsNull("StoreBook_ID"))
                    {
                        StoreBookID = Convert.ToInt32(row.Row["StoreBook_ID"]);
                    }
                    foreach (DataRowView item in pole4.Items)
                    {
                        if (!item.Row.IsNull("ID_StoreBook") && Convert.ToInt32(item.Row["ID_StoreBook"]) == StoreBookID)
                        {
                            pole4.SelectedItem = item;
                            break;
                        }
                    }
                    if (!row.Row.IsNull("Client_ID"))
                    {
                        ClientID = Convert.ToInt32(row.Row["Client_ID"]);
                    }
                    foreach (DataRowView item in pole5.Items)
                    {
                        if (!item.Row.IsNull("ID_Client") && Convert.ToInt32(item.Row["ID_Client"]) == ClientID)
                        {
                            pole5.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
        }

        List<string> Vnesnie = new List<string> { "Staff_ID", "StoreBook_ID", "Client_ID" }; 
        private void dg_BD_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            var selectedCell = grid.SelectedCells[0];
            var column = selectedCell.Column as DataGridComboBoxColumn; 


            if (column != null)
            {
                //var comboBox = new ComboBox();
                //comboBox.ItemsSource = Vnesnie; // список внешних ключей для выбранного столбца;
                //// comboBox.DisplayMemberPath = // имя отображаемого свойства;
                //// comboBox.SelectedValuePath = // имя свойства, используемого в качестве значения;

                //var binding = new Binding(column.SelectedValueBindingPath);
                //// если требуется валидация, можно добавить логику связывания и валидации
                //comboBox.SetBinding(ComboBox.SelectedValueProperty, binding);

                //var editingElement = (TextBox)e.EditingElement;
                //editingElement.Visibility = Visibility.Collapsed; // скрываем текстовое поле
                //comboBox.Visibility = Visibility.Visible; // показываем комбобокс

                //comboBox.ItemsSource = // заполните список внешних ключей в зависимости от выбранной ячейки
                //editingElement.Parent.AddChild(comboBox); // добавляем комбобокс в родительский контейнер
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                ordercheck.DeleteQuery(Convert.ToInt32(id));
                dg_BD.ItemsSource = ordercheck.GetData();
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
                ordercheck.UpdateQuery(pole1.Text, pole2.Text, Convert.ToInt32(pole3.Text), Convert.ToInt32(pole4.Text), Convert.ToInt32(pole5.Text), Convert.ToInt32(id));
                dg_BD.ItemsSource = ordercheck.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;

            }
            catch
            {
                MessageBox.Show("Не трожь внешние ключи!");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            ordercheck.InsertQuery(pole1.Text, pole2.Text, Convert.ToInt32(pole3.Text), Convert.ToInt32(pole4.Text), Convert.ToInt32(pole5.Text));
            dg_BD.ItemsSource = ordercheck.GetData();
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void CreateBackup_Click(object sender, RoutedEventArgs e)
        {
            backups.Backup_BookStore();
        }

        private void CreateCheck_Click(object sender, RoutedEventArgs e)
        {

            string fileName = "D:\\Vyacheslav\\Study\\Project\\PRACTICA\\ласт практика\\order_details.txt";
            string orderDetails = "\t\t\tИнформационна Система (ИС) Книжного Магазина" + "\n\t\t\tКассовый чек #" + pole2.Text + "\n\n\n\t\t\tДата заказа: " + pole1.Text + "\n\t\t\tОбслуживающий сотрудник: " + pole3.Text + "\n\t\t\tДанные о книге: " + pole4.Text + "\n\t\t\tКлиент оформивший заказ: " + pole5.Text;

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(orderDetails);
            }
        }
    }
}
