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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AuthTableAdapter auth = new AuthTableAdapter();
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
                        int? clientID = row.IsNull("Client_ID") ? null : (int?)row["Client_ID"];
                        int? staffID = row.IsNull("Staff_ID") ? null : (int?)row["Staff_ID"];
                        
                        if (staffID != null)
                        {
                            Admin admin = new Admin();
                            admin.Show();
                            Close();
                        }
                        else if (clientID != null)
                        {
                            Client client = new Client();
                            client.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Неверные данные входа!");
                        }
                        return;
                    }
                    
                }
                MessageBox.Show("Неверные данные входа!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }

            
        }

        private bool IsAdmin(string username, string password)
        {
            return true;
        }

        private bool IsClient(string username, string password)
        {
            return false;
        }
    }
}
