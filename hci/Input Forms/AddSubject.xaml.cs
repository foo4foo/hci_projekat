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
    /// Interaction logic for AddSubject.xaml
    /// </summary>
    public partial class AddSubject : Window
    {

        public ObservableCollection<Software> SoftwaresCollection
        {
            get;
            set;
        }
        public ObservableCollection<Course> CourseCollection
        {
            get;
            set;
        }
        public ObservableCollection<int> Brojevi
        {
            get;
            set;
        }

        public ObservableCollection<SelectableObject<Software>> SoftwaresSelectedCollection
        {
            get;
            set;
        }

        private void OnSftwObjectsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            comboBox.SelectedItem = null;
        }


        public AddSubject()
        {
            this.DataContext = this;
            MainWindow.ConsoleAllocator.ShowConsoleWindow();
            DatabaseManager databaseManager = new DatabaseManager();
            MySqlCommand allSoftware = new MySqlCommand("Select * from softwares;");
            MySqlCommand allCourses = new MySqlCommand("Select * from courses;");
            this.SoftwaresCollection = databaseManager.GetCollectionSoftwares(allSoftware);
            this.CourseCollection = databaseManager.GetCollectionCourses(allCourses);

            this.SoftwaresSelectedCollection = new ObservableCollection<SelectableObject<Software>>();

            for (int i = 0; i < SoftwaresCollection.Count; i++)
            {
                this.SoftwaresSelectedCollection.Add(new SelectableObject<Software>(SoftwaresCollection[i], false));
            }
         
            this.Brojevi = new ObservableCollection<int>();
            for (int i = 1; i < 100; i++)
                this.Brojevi.Add(i);
            InitializeComponent();
        }

        private void SubjectAdded_Executed(object sender, ExecutedRoutedEventArgs e)
        {


            string _id = Id.Text;
            string _name = name.Text;
            string _desc = Description.Text;
            int _size = Int32.Parse(Size.Text);
            string _os = os.Text;

            bool _projector;
            if (projector.Text.Equals("Yes"))
                _projector = true;
            else _projector = false;

            bool _board;
            if (board.Text.Equals("Yes"))
                _board = true;
            else _board = false;

            bool _smartBoard;
            if (smartboard.Text.Equals("Yes"))
                _smartBoard = true;
            else _smartBoard = false;

            int minLength = Int32.Parse(MinNo.Text);
            int noOfClasses = Int32.Parse(NoOfClasses.Text);
            Course c = new Course();
            foreach(var kurs in CourseCollection)
            {
                if (kurs.Id.Equals(Course.Text))
                {
                    c = kurs;
                }
            }
            ObservableCollection<Software> _softwares = new ObservableCollection<Software>();

            Subject s = new Subject(_id, _name, _desc, _size, minLength, noOfClasses, _projector, _board, _smartBoard, _os, c, _softwares);

            MySqlCommand cmd = new MySqlCommand("insert into hci.subjects(subjectId,name,description,size,minLength,noOfClasses,needProjector,needBoard,needSmartBoard,needOperatingSys, courseId)"
              + "values ('" + _id + "','" + _name + "','" + _desc + "'," + _size + "," + minLength + "," + noOfClasses + "," + _projector + "," + _board + "," + _smartBoard + ",'" + _os +  "','" + Course.Text + "');");
            DatabaseManager db = new DatabaseManager();
            db.ExecuteQuery(cmd);

            foreach (SelectableObject<Software> sftwObject in SoftwaresSelectedCollection)
                if (sftwObject.IsSelected)
                {
                    MySqlCommand cmd2 = new MySqlCommand("insert into hci.softwareInSubject(subjectId, softwareId) values ('" + _id + "','" + sftwObject.ObjectData.Id + "');");
                    db.ExecuteQuery(cmd2);

                }


            MessageBox.Show("Subject successfully added!");
            this.Close();

        }

        private void SubjectAdded_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SubjectClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            this.Close();
        }

        private void SubjectClose_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

    }
}
