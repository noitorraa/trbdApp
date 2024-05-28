using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
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
using NPOI.XWPF.UserModel;
using NPOI.SS.Formula;

namespace trbdApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для WorkArea.xaml
    /// </summary>
    public partial class WorkArea : Page
    {
        public WorkArea()
        {
            InitializeComponent();
        }
        private string rowData;
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Host=localhost;Username=postgres;Password=Passw0rd;Database=ForTRBD"; // Замените на строку подключения к вашей базе данных
            string query = "SELECT * FROM public.employees;"; // Замените на ваш SQL запрос

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                // Выполнение SQL запроса и получение данных
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {

                        XWPFDocument doc = new XWPFDocument();
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                // Преобразование различных типов данных в строку
                                string dataValue = reader.IsDBNull(i) ? "NULL" : reader.GetValue(i).ToString();
                                rowData += dataValue; // Добавление значения в строку данных
                            }

                            // Добавление строки данных в документ
                        }
                        XWPFParagraph paragraph = doc.CreateParagraph();
                        XWPFRun run = paragraph.CreateRun();
                        run.SetText(rowData);

                        // Сохранение документа в файл
                        string newFilePath = @"C:\Users\User\Desktop\myDoc.docx";
                        using (FileStream stream = new FileStream(newFilePath, FileMode.Create, FileAccess.Write))
                        {
                            doc.Write(stream);
                        }

                        Console.WriteLine("Данные успешно записаны в файл output.docx");

                    }
                }
            }
        }
    }
}

