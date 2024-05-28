using Npgsql;
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

namespace trbdApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }
        private bool isAuthenticated = false;
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Main());
        }

        private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string password = tbPassword.Text;
            var connString = "Host=localhost;Username=postgres;Password=Passw0rd;Database=ForTRBD";
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                Console.WriteLine("Соединение открыто");

                using (var cmd = new NpgsqlCommand($"SELECT user_id FROM public.\"Users\" where login='{login}' and parol = '{password}'", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("Запрос выполняется");
                        if (reader.Read())
                        {
                            isAuthenticated = true;
                            Console.WriteLine("Пользователь существует");
                        }
                    }
                }
                conn.Close();
            } 
            if (isAuthenticated)
            {
                MessageBox.Show("Такой пользователь уже существует, введите другие данные!");
            }
            else
            {
                try
                {
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand($"INSERT INTO public.\"Users\"(login, parol) VALUES ('{login}', '{password}');", conn))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                MessageBox.Show("Пользователь создан!");
                            }
                        }
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
            }
        }
    }
}
