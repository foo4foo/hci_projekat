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
                                dict["constraintId"] = reader.GetString(0);
                                dict["softwareId"] = reader.GetString(1);
                                dict["softwareName"] = reader.GetString(2);
                                dict["softwareOs"] = reader.GetString(3);
                                dict["softwareDeveloper"] = reader.GetString(4);
                                dict["softwareSite"] = reader.GetString(5);
                                dict["softwareYear"] = reader.GetString(6);
                                dict["softwarePrice"] = reader.GetString(7);
                                dict["softwareDescription"] = reader.GetString(8);

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
                            var subject = new Subject();

                            subject.Id = reader.GetString(0);
                            subject.Name = reader.GetString(1);
                            subject.Description = reader.GetString(2);
                            subject.Size = reader.GetInt32(3);
                            subject.MinLength = reader.GetInt32(4);
                            subject.NoOfClasses = reader.GetInt32(5);
                            subject.NeedProjector = reader.GetBoolean(6);
                            subject.NeedBoard = reader.GetBoolean(7);
                            subject.NeedSmartBoard = reader.GetBoolean(8);
                            subject.Os = reader.GetString(9);
                            string smer = reader.GetString(10);
                            
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

                            classroom.Id = reader.GetString(0);
                            classroom.Description = reader.GetString(1);
                            classroom.Size = reader.GetInt32(2);
                            classroom.HaveProjector = reader.GetBoolean(3);
                            classroom.HaveBoard = reader.GetBoolean(4);
                            classroom.HaveSmartBoard = reader.GetBoolean(5);
                            classroom.OperatingSys = reader.GetString(6);
                           
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
