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
    /// Логика взаимодействия для AdminPageOrderCheck.xaml
    /// </summary>
    public partial class AdminPageOrderCheck : Page
    {
        OrderCheckTableAdapter ordercheck = new OrderCheckTableAdapter();
        public AdminPageOrderCheck()
        {
            InitializeComponent();
            dg_BD.ItemsSource = ordercheck.GetData();
        }

        private void dg_BD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_BD.SelectedItem != null)
            {
                DataRowView row = dg_BD.SelectedItem as DataRowView;
                if (row != null)
                {
                    pole1.Text = row.Row["OrderDate"].ToString();
                    pole2.Text = row.Row["OrderNumber"].ToString();
                    pole3.Text = row.Row["Staff_ID"].ToString();
                    pole4.Text = row.Row["StoreBook_ID"].ToString();
                    pole5.Text = row.Row["Client_ID"].ToString();
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
    }
}
