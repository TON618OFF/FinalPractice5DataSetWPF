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
    /// Логика взаимодействия для AdminPageStaff.xaml
    /// </summary>
    public partial class AdminPageStaff : Page
    {

        StaffTableAdapter staff = new StaffTableAdapter();

        public AdminPageStaff()
        {
            InitializeComponent();
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
                    pole6.Text = row.Row["StaffPosition_ID"].ToString();
                }
            }
        }
    }
}
