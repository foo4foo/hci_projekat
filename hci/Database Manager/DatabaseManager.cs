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

        public void ExecuteQuery(MySqlCommand cmd)
        {
                //upisi u mysql bazu
                using (MySqlConnection conn = new MySqlConnection(this.connectionString))
                {
                    try
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();

                 

                        conn.Close();
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
        }


        public ObservableCollection<Course> GetCollectionCourses(MySqlCommand cmd)
        {
            var collection = new ObservableCollection<Course>();

            //upisi u mysql bazu
            using (MySqlConnection conn = new MySqlConnection(this.connectionString))
            {
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
                            var course = new Course();

                            course.Id = reader.GetString(0);
                            course.Name = reader.GetString(1);
                            course.Date = reader.GetString(2);
                            course.Description = reader.GetString(3);

                            collection.Add(course);

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

                            software.Id = reader.GetString(0);
                            software.Name = reader.GetString(1);
                            software.Os = reader.GetString(2);
                            software.Developer = reader.GetString(3);
                            software.Site = reader.GetString(4);
                            software.Year = reader.GetInt32(5);
                            software.Price = reader.GetDouble(6);
                            software.Description = reader.GetString(7);

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
