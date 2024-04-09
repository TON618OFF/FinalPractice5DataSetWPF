using Practice5.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
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

namespace Practice5
{
    /// <summary>
    /// Логика взаимодействия для ClientPagePublishHouse.xaml
    /// </summary>
    public partial class ClientPagePublishHouse : Page
    {
        PublishingHouseTableAdapter publishhouse = new PublishingHouseTableAdapter();
        public ClientPagePublishHouse()
        {
            InitializeComponent();
            dg_BD.ItemsSource = publishhouse.GetData();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
