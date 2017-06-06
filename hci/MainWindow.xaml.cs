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
using hci.Database_Manager;
using MySql.Data.MySqlClient;

namespace hci
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static class ConsoleAllocator
        {
            [DllImport(@"kernel32.dll", SetLastError = true)]
            static extern bool AllocConsole();

            [DllImport(@"kernel32.dll")]
            static extern IntPtr GetConsoleWindow();

            [DllImport(@"user32.dll")]
            static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            const int SwHide = 0;
            const int SwShow = 5;


            public static void ShowConsoleWindow()
            {
                var handle = GetConsoleWindow();

                if (handle == IntPtr.Zero)
                {
                    AllocConsole();
                }
                else
                {
                    ShowWindow(handle, SwShow);
                }
            }

            public static void HideConsoleWindow()
            {
                var handle = GetConsoleWindow();

                ShowWindow(handle, SwHide);
            }
        }

        public MainWindow()
        {
            //ConsoleAllocator.ShowConsoleWindow();
            //upisi u mysql bazu
            /*var conn_string = "datasource=127.0.0.1;port=3306;username=root;password=gibanica;database=hci;";
            using (MySqlConnection conn = new MySqlConnection(conn_string)) {
                try
                {
                    conn.Open();
                    //string query = "Insert into tabela (bleja) values (@bleja);";
                    //MySqlCommand commandDatabase = new MySqlCommand(query, conn);
                    //commandDatabase.Parameters.AddWithValue("@bleja", "ratko mladic");
                    //commandDatabase.ExecuteNonQuery();

                    MySqlDataReader reader;
                    string query2 = "Select * from tabela;";
                    MySqlCommand command = new MySqlCommand(query2, conn);

                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.GetInt32(0) + " - " + reader.GetString(1));
                        }
                    }

                    reader.Close();

                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                catch (System.InvalidOperationException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }*/

            InitializeComponent();
        }

        private void New_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not yet implemented");
        }

        // metode za komandu

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DatabaseManagerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //var databaseManager = new DatabaseManager();
            //databaseManager.Show();
        }

        private void DatabaseManagerCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
