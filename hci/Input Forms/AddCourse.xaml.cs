using hci.Database_Manager;
using hci.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace hci.Input_Forms
{
    /// <summary>
    /// Interaction logic for Course.xaml
    /// </summary>
    public partial class AddCourse : Window


    {

        public delegate void DataChangedEventHandler(object sender, EventArgs e);

        public event DataChangedEventHandler DataChanged;

        public AddCourse()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }


        private void CourseAdded_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DatabaseManager db = new DatabaseManager();

            string _id = Id.Text;
            string _name = name.Text;
            string _date = Date.Text;
            string _desc = Description.Text;

            bool ok = true;
            ObservableCollection<Course> courses = db.GetCollectionCourses(new MySqlCommand("Select * from courses;"));
            foreach (var course in courses)
            {
                if (String.IsNullOrEmpty(_id) || String.IsNullOrEmpty(_name) || String.IsNullOrEmpty(_date) || String.IsNullOrEmpty(_desc))
                {
                    MessageBox.Show("Greška u dodavanju! Popunite sva polja.");
                    ok = false;
                    break;
                }

                DateTime temp;
                if ((DateTime.TryParse(Date.Text, out temp)))
                {
                    if (temp > DateTime.Today)
                    {
                        MessageBox.Show("Greška! Datum uvođenja smera ne može biti u budućnosti.");
                        ok = false;
                        break;
                    }
                }
                if (course.Id.Equals(_id))
                {
                    MessageBox.Show("Greška! Uneta oznaka smera već postoji!");
                    ok = false;
                    break;
                }
                if (course.Name.Equals(_name))
                {
                    MessageBox.Show("Greška! Uneti naziv smera već postoji!");
                    ok = false;
                    break;
                }
            }

            if (ok)
            {
           
                Course c = new Course(_id, _name, _date, _desc, false);


                MySqlCommand cmd = new MySqlCommand("insert into hci.courses(courseId, name, date_, description, deleted)"
                  + "values ('" + _id + "','" + _name + "','" + _date + "','" + _desc + "', " + false + ");");

                db.ExecuteQuery(cmd);
                MessageBox.Show("Smer uspešno dodat!");

                DataChangedEventHandler handler = DataChanged;
                if (handler != null)
                {
                    handler(this, new EventArgs());
                }

                this.Close();
            }
        }

        private void CourseAdded_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CourseClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            this.Close();
        }

        private void CourseClose_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
