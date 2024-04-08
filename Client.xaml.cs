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
using System.Windows.Shapes;

namespace Practice5
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        List<string> nameTable = new List<string> { "Должность сотрудников", "Сотрудники", "Authors", "Genre", "Quantity Pages", "Publishing House", "Cover", "Books", "Store", "StoreBooks", "OrderCheck" };

        public Client()
        {
            InitializeComponent();
            cb_DB.ItemsSource = nameTable;
        }

        private void cb_DB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nameTable = cb_DB.SelectedItem.ToString();

            if (nameTable == "Staff Position")
            {
                PageFrame.Content = new AdminPageStaffPos();
            }
            else if (nameTable == "Staff")
            {
                PageFrame.Content = new AdminPageStaff();
            }
            else if (nameTable == "Clients")
            {
                PageFrame.Content = new AdminPageClients();
            }
            else if (nameTable == "Auth")
            {
                PageFrame.Content = new AdminPageAuth();
            }
            else if (nameTable == "Authors")
            {
                PageFrame.Content = new AdminPageAuthors();
            }
            else if (nameTable == "Genre")
            {
                PageFrame.Content = new AdminPageGenre();
            }
            else if (nameTable == "Quantity Pages")
            {
                PageFrame.Content = new AdminPageQuantityPages();
            }
            else if (nameTable == "Publishing House")
            {
                PageFrame.Content = new AdminPagePublishHouse();
            }
            else if (nameTable == "Cover")
            {
                PageFrame.Content = new AdminPageCover();
            }
            else if (nameTable == "Books")
            {
                PageFrame.Content = new AdminPageBooks();
            }
            else if (nameTable == "Store")
            {
                PageFrame.Content = new AdminPageStore();
            }
            else if (nameTable == "StoreBooks")
            {
                PageFrame.Content = new AdminPageStoreBooks();
            }
            else if (nameTable == "OrderCheck")
            {
                PageFrame.Content = new AdminPageOrderCheck();
            }
        }
    }
}
