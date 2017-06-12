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
        public delegate void DataChangedEventHandler(object sender, EventArgs e);

        public event DataChangedEventHandler DataChanged;

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
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.DataContext = this;
           // MainWindow.ConsoleAllocator.ShowConsoleWindow();
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


            DatabaseManager db = new DatabaseManager();

            string _id = Id.Text;
            string _name = name.Text;
            string _desc = Description.Text;
            int _size = Int32.Parse(Size.Text);
            string _os = os.Text;

            bool _projector;
            if (projector.Text.Equals("Da"))
                _projector = true;
            else _projector = false;

            bool _board;
            if (board.Text.Equals("Da"))
                _board = true;
            else _board = false;

            bool _smartBoard;
            if (smartboard.Text.Equals("Da"))
                _smartBoard = true;
            else _smartBoard = false;

            int minLength = Int32.Parse(MinNo.Text);
            int noOfClasses = Int32.Parse(NoOfClasses.Text);

            bool ok = true;
            ObservableCollection<Subject> subjects = db.GetCollectionSubjects(new MySqlCommand("Select * from subjects;"));
            foreach(var subject in subjects)
            {
                if (String.IsNullOrEmpty(_id) || String.IsNullOrEmpty(_desc) || String.IsNullOrEmpty(_name))
                {
                    MessageBox.Show("Greška u dodavanju! Popunite sva polja.");
                    ok = false;
                    break;
                }

                if (subject.Id.Equals(_id))
                {
                    MessageBox.Show("Greška! Uneta oznaka predmeta već postoji!");
                    ok = false;
                    break;
                }
            }

            if (ok)
            {
               
                Course c = new Course();
                foreach (var kurs in CourseCollection)
                {
                    if (kurs.Name.Equals(Course.Text))
                    {
                        c = kurs;
                    }
                }
                ObservableCollection<Software> _softwares = new ObservableCollection<Software>();

                var course_db_id = db.get_id(new MySqlCommand("Select ID from courses where courseId=\"" + c.Id + "\";"));
                MySqlCommand cmd = new MySqlCommand("insert into hci.subjects(subjectId,name,description,size,minLength,noOfClasses,needProjector,needBoard,needSmartBoard,needOperatingSys,courseId,deleted)"
                  + "values ('" + _id + "','" + _name + "','" + _desc + "'," + _size + "," + minLength + "," + noOfClasses + "," + _projector + "," + _board + "," + _smartBoard + ",'" 
                  + _os + "','" + course_db_id + "'," + false + ");");

                db.ExecuteQuery(cmd);

                foreach (SelectableObject<Software> sftwObject in SoftwaresSelectedCollection)
                    if (sftwObject.IsSelected)
                    {
                        _softwares.Add(sftwObject.ObjectData);
                        Console.WriteLine(sftwObject.ObjectData.ToString());
                        var software_db_id = db.get_id(new MySqlCommand("Select ID from softwares where softwareId=\"" + sftwObject.ObjectData.Id + "\";"));
                        var subject_db_id = db.get_id(new MySqlCommand("Select ID from subjects where subjectId=\"" + _id + "\";"));
                        MySqlCommand cmd2 = new MySqlCommand("insert into hci.softwareInSubject(subjectId, softwareId) values ('" + subject_db_id + "','" + software_db_id + "');");
                        db.ExecuteQuery(cmd2);

                    }


                Subject s = new Subject(_id, _name, _desc, _size, minLength, noOfClasses, _projector, _board, _smartBoard, _os, c, _softwares, false);

                MessageBox.Show("Predmet uspešno dodat!");

                DataChangedEventHandler handler = DataChanged;
                if (handler != null)
                {
                    handler(this, new EventArgs());
                }

                this.Close();
            }
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
