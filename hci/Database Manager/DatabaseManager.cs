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

        public string get_id(MySqlCommand cmd)
        {
            string id = "";

            using (MySqlConnection conn = new MySqlConnection(this.connectionString))
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            id = reader.GetString(0);
                        }
                    }
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

            return id;
        }

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

        public List<Dictionary<string, string>> GetSoftwareList(MySqlCommand cmd)
        {
            var lista = new List<Dictionary<string, string>>();

            using (MySqlConnection conn = new MySqlConnection(this.connectionString))
            {
                try
                {
                    conn.Open();


                    MySqlDataReader reader;
                    cmd.Connection = conn;

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                Dictionary<string, string> dict = new Dictionary<string, string>();
                                dict["ID"] = reader.GetString(0);
                                dict["softwareId"] = reader.GetString(1);
                                dict["softwareName"] = reader.GetString(2);
                                dict["softwareOs"] = reader.GetString(3);
                                dict["softwareDeveloper"] = reader.GetString(4);
                                dict["softwareSite"] = reader.GetString(5);
                                dict["softwareYear"] = reader.GetString(6);
                                dict["softwarePrice"] = reader.GetString(7);
                                dict["softwareDescription"] = reader.GetString(8);
                                dict["deleted"] = reader.GetString(9);

                                lista.Add(dict);
                            }
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
                catch (System.Data.SqlTypes.SqlNullValueException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            return lista;
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
              

                    MySqlDataReader reader;
                    cmd.Connection = conn;

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if(reader.GetBoolean(5))
                                continue;

                            var course = new Course();

                            course.DbId = reader.GetString(0);
                            course.Id = reader.GetString(1);
                            course.Name = reader.GetString(2);
                            course.Date = reader.GetString(3);
                            course.Description = reader.GetString(4);
                            course.Deleted = reader.GetBoolean(5);

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

                    MySqlDataReader reader;
                    cmd.Connection = conn;

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if(reader.GetBoolean(9))
                                continue;

                            var software = new Software();

                            software.DbId = reader.GetString(0);
                            software.Id = reader.GetString(1);
                            software.Name = reader.GetString(2);
                            software.Os = reader.GetString(3);
                            software.Developer = reader.GetString(4);
                            software.Site = reader.GetString(5);
                            software.Year = reader.GetInt32(6);
                            software.Price = reader.GetDouble(7);
                            software.Description = reader.GetString(8);
                            software.Deleted = reader.GetBoolean(9);

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

        public ObservableCollection<Software> GetNeededSoftware(MySqlCommand cmd)
        {
            var collection = new ObservableCollection<Software>();

            //upisi u mysql bazu
            using (MySqlConnection conn = new MySqlConnection(this.connectionString))
            {
                try
                {
                    conn.Open();

                    MySqlDataReader reader;
                    cmd.Connection = conn;

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var software = new Software();

                            string softId = reader.GetString(0);

                            ObservableCollection<Software> softwares = GetCollectionSoftwares(new MySqlCommand("Select * from softwares;"));
                            foreach (var soft in softwares)
                            {
                                if (soft.Id.Equals(softId))
                                {
                                    software = soft;
                                }
                            }

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

        public ObservableCollection<Subject> GetCollectionSubjects(MySqlCommand cmd)
        {
            var collection = new ObservableCollection<Subject>();

            //upisi u mysql bazu
            using (MySqlConnection conn = new MySqlConnection(this.connectionString))
            {
                try
                {
                    conn.Open();

                    MySqlDataReader reader;
                    cmd.Connection = conn;

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if(reader.GetBoolean(12))
                                continue;

                            var subject = new Subject();

                            subject.DbId = reader.GetString(0);
                            subject.Id = reader.GetString(1);
                            subject.Name = reader.GetString(2);
                            subject.Description = reader.GetString(3);
                            subject.Size = reader.GetInt32(4);
                            subject.MinLength = reader.GetInt32(5);
                            subject.NoOfClasses = reader.GetInt32(6);
                            subject.NeedProjector = reader.GetBoolean(7);
                            subject.NeedBoard = reader.GetBoolean(8);
                            subject.NeedSmartBoard = reader.GetBoolean(9);
                            subject.Os = reader.GetString(10);
                            string smer = reader.GetString(11);
                            subject.Deleted = reader.GetBoolean(12);
                            
                            ObservableCollection<Course> courses = GetCollectionCourses(new MySqlCommand("Select * from courses;"));
                            foreach (var kurs in courses)
                            {
                                if (kurs.Id.Equals(smer))
                                {
                                    subject.Smer = kurs;
                                }
                            }

                            ObservableCollection<Software> softwares = GetNeededSoftware(new MySqlCommand("Select softwareId from softwareInSubject where subjectId = '" + subject.Id + "';"));
                            subject.Softwares = softwares;

                            collection.Add(subject);

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


        public ObservableCollection<Classroom> GetCollectionClassrooms(MySqlCommand cmd)
        {
            var collection = new ObservableCollection<Classroom>();

            //upisi u mysql bazu
            using (MySqlConnection conn = new MySqlConnection(this.connectionString))
            {
                try
                {
                    conn.Open();

                    MySqlDataReader reader;
                    cmd.Connection = conn;

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var classroom = new Classroom();

                            classroom.DbId = reader.GetString(0);
                            classroom.Id = reader.GetString(1);
                            classroom.Description = reader.GetString(2);
                            classroom.Size = reader.GetInt32(3);
                            classroom.HaveProjector = reader.GetBoolean(4);
                            classroom.HaveBoard = reader.GetBoolean(5);
                            classroom.HaveSmartBoard = reader.GetBoolean(6);
                            classroom.OperatingSys = reader.GetString(7);
                            classroom.Deleted = reader.GetBoolean(8);

                            if(classroom.Deleted)
                                continue;
                           
                            ObservableCollection<Software> softwares = GetNeededSoftware(new MySqlCommand("Select softwareId from softwareInClassroom where classroomId = '" + classroom.Id + "';"));
                            classroom.Softwares = softwares;

                            collection.Add(classroom);

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
