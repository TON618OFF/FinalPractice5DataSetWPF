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
using System.Windows.Shapes;

namespace Practice5
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        List<string> nameTable = new List<string> { "Должность сотрудников", "Сотрудники", "Клиенты", "Авторизация", "Авторы", "Жанры", "Кол-во страниц", "Издательство", "Обложка", "Книги", "Магазин", "МагазинКниги", "Чек Заказа" };
        public Admin()
        {
            InitializeComponent();
            cb_DB.ItemsSource = nameTable;
        }

        private void cb_DB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nameTable = cb_DB.SelectedItem.ToString();
            
            if(nameTable == "Должность сотрудников")
            {
                PageFrame.Content = new AdminPageStaffPos();
            }
            else if(nameTable == "Сотрудники")
            {
                PageFrame.Content = new AdminPageStaff();
            }
            else if (nameTable == "Клиенты")
            {
                PageFrame.Content = new AdminPageClients();
            }
            else if (nameTable == "Авторизация")
            {
                PageFrame.Content = new AdminPageAuth();
            }
            else if (nameTable == "Авторы")
            {
                PageFrame.Content = new AdminPageAuthors();
            }
            else if (nameTable == "Жанры")
            {
                PageFrame.Content = new AdminPageGenre();
            }
            else if (nameTable == "Кол-во страниц")
            {
                PageFrame.Content = new AdminPageQuantityPages();
            }
            else if (nameTable == "Издательство")
            {
                PageFrame.Content = new AdminPagePublishHouse();
            }
            else if (nameTable == "Обложка")
            {
                PageFrame.Content = new AdminPageCover();
            }
            else if (nameTable == "Книги")
            {
                PageFrame.Content = new AdminPageBooks();
            }
            else if (nameTable == "Магазин")
            {
                PageFrame.Content = new AdminPageStore();
            }
            else if (nameTable == "МагазинКниги")
            {
                PageFrame.Content = new AdminPageStoreBooks();
            }
            else if (nameTable == "Чек Заказа")
            {
                PageFrame.Content = new AdminPageOrderCheck();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
