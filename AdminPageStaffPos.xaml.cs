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
using static Practice5.DataSet1;

namespace Practice5
{
    /// <summary>
    /// Логика взаимодействия для AdminPageStaffPos.xaml
    /// </summary>
    public partial class AdminPageStaffPos : Page
    {
        StaffPositionTableAdapter staffpos = new StaffPositionTableAdapter();
        QueriesTableAdapter backups = new QueriesTableAdapter();
        public AdminPageStaffPos()
        {
            InitializeComponent();
            dg_BD.ItemsSource = staffpos.GetData();
        }

        private void dg_BD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_BD.SelectedItem != null)
            {
                DataRowView row = dg_BD.SelectedItem as DataRowView;
                if (row != null)
                {
                    pole1.Text = row.Row["Position"].ToString();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                staffpos.DeleteQuery(Convert.ToInt32(id));
                dg_BD.ItemsSource = staffpos.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Нельзя удалить должность, так как она используется в другой таблице");
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                staffpos.UpdateQuery(pole1.Text, Convert.ToInt32(id));
                dg_BD.ItemsSource = staffpos.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;

            }
            catch
            {
                MessageBox.Show("Не трожь внешние ключи!");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var existingPosition = staffpos.GetData().Any(x => x.Position == pole1.Text);

            if (existingPosition)
            {
                // Такая должность уже существует, не добавляем новую запись
                MessageBox.Show("Такая должность существует");
            }
            else
            {
                // Добавляем новую запись
                staffpos.InsertQuery(pole1.Text);
                dg_BD.ItemsSource = staffpos.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void ImportJson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<StaffPos> forImport = Deser.DeserializeObject<List<StaffPos>>();
                foreach (var item in forImport)
                {
                    staffpos.InsertQuery(item.Position);
                }
                dg_BD.ItemsSource = null;
                dg_BD.ItemsSource = staffpos.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;
            }
            catch
            {
                
            }

        }
    }
}
