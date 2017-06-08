using hci.Database_Manager;
using hci.Models;
using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace hci.Input_Forms
{
    /// <summary>
    /// Interaction logic for Course.xaml
    /// </summary>
    public partial class AddCourse : Window
    {
        public AddCourse()
        {
            
            InitializeComponent();
        }


        private void CourseAdded_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string _id = Id.Text;
            string _name = name.Text;
            string _date = Date.Text;
            string _desc = Description.Text;

            Course c = new Course(_id, _name, _date, _desc);


            MySqlCommand cmd = new MySqlCommand("insert into hci.courses(courseId, name, date_, description)"
              + "values ('" + _id + "','" + _name  + "','" + _date + "','" + _desc + "');");
            DatabaseManager db = new DatabaseManager();
            db.ExecuteQuery(cmd);
            MessageBox.Show("Course successfully added!");
            this.Close();
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
