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
        List<string> nameTable = new List<string> { "Должность сотрудников", "Сотрудники", "Авторы", "Жанры", "Кол-во страниц книг", "Издательства", "Обложки", "Книги", "Магазин"};

        public Client()
        {
            InitializeComponent();
            cb_DB.ItemsSource = nameTable;
        }

        private void cb_DB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nameTable = cb_DB.SelectedItem.ToString();

            if (nameTable == "Должность сотрудников")
            {
                PageFrame.Content = new ClientPageStaffPos();
            }
            else if (nameTable == "Сотрудники")
            {
                PageFrame.Content = new ClientPageStaff();
            }
            else if (nameTable == "Авторы")
            {
                PageFrame.Content = new ClientPageAuthors();
            }
            else if (nameTable == "Жанры")
            {
                PageFrame.Content = new ClientPageGenre();
            }
            else if (nameTable == "Кол-во страниц книг")
            {
                PageFrame.Content = new ClientPageQuantityPages();
            }
            else if (nameTable == "Издательства")
            {
                PageFrame.Content = new ClientPagePublishHouse();
            }
            else if (nameTable == "Обложки")
            {
                PageFrame.Content = new ClientPageCover();
            }
            else if (nameTable == "Книги")
            {
                PageFrame.Content = new ClientPageBooks();
            }
            else if (nameTable == "Магазин")
            {
                PageFrame.Content = new ClientPageStore();
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
