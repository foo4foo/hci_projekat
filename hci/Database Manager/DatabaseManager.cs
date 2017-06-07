using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using hci.Models;
using MySql.Data.MySqlClient;

namespace hci.Database_Manager
{
    public class DatabaseManager
    {
        private string connectionString = 
            "datasource=127.0.0.1;port=3306;username=root;password=gibanica;database=hci;";

        public DatabaseManager() { }

        public ObservableCollection<Software> GetCollectionSoftwares(MySqlCommand cmd)
        {
            var collection = new ObservableCollection<Software>();

            //upisi u mysql bazu
            using (MySqlConnection conn = new MySqlConnection(this.connectionString)) {
                try
                {
                    conn.Open();
                    //string query = "Insert into tabela (bleja) values (@bleja);";
                    //MySqlCommand commandDatabase = new MySqlCommand(query, conn);
                    //commandDatabase.Parameters.AddWithValue("@bleja", "ratko mladic");
                    //commandDatabase.ExecuteNonQuery();

                    MySqlDataReader reader;
                    cmd.Connection = conn;

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var software = new Software();

                            software.Id = reader.GetInt32(0);
                            software.Name = reader.GetString(1);
                            software.Size = reader.GetInt32(2);
                            software.Os = reader.GetString(3);
                            software.Developer = reader.GetString(4);
                            software.Site = reader.GetString(5);
                            software.Year = reader.GetInt32(6);
                            software.Price = reader.GetDouble(7);
                            software.Description = reader.GetString(8);

                            collection.Add(software);

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
            }
            return collection;
        }
    }
}
