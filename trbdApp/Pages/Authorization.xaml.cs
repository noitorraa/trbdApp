using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using trbdApp;

namespace trbdApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        private bool isAuthenticated = false;
        public Authorization()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string password = tbPassword.Text;
            //string hash = hasher.Hashpasswords(password);

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
                            Console.WriteLine("Пользователь существует "+reader);
                        }
                    }
                }
                conn.Close();
            }
            if (isAuthenticated)
            {
                this.NavigationService.Navigate(new WorkArea());
            }
            else
            {
                MessageBox.Show("Ошибка при входе в аккаунт! Попробуйте снова!");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Main());
        }
    }
}
