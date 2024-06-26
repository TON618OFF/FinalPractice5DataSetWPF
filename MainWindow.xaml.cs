﻿using System;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AuthTableAdapter auth = new AuthTableAdapter();
        ClientsTableAdapter clients = new ClientsTableAdapter();
        StaffTableAdapter staff = new StaffTableAdapter();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var LogInData = auth.GetData().Rows;

                foreach (DataRow row in LogInData)
                {
                    if (row["JustLogin"].ToString() == Login.Text && row["JustPassword"].ToString() == Password.Password)
                    {
                        int authID = (int)row["ID_Auth"];

                        DataRow[] clientRow = clients.GetData().Select("Auth_ID = " + authID);
                        if (clientRow.Length > 0)
                        {
                            Admin admin = new Admin();
                            admin.Show();
                            Close();
                            return;
                        }

                        DataRow[] staffRow = staff.GetData().Select("Auth_ID = " + authID);
                        if (staffRow.Length > 0)
                        {
                            Client client = new Client();
                            client.Show();
                            Close();
                            return;
                        }

                        MessageBox.Show("Неверные логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                MessageBox.Show("Неверные логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

    }
}
