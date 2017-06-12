using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using hci.Database_Manager;
using hci.Models;
using MySql.Data.MySqlClient;

namespace hci.Input_Forms
{
    /// <summary>
    /// Interaction logic for AddClassroom.xaml
    /// </summary>
    public partial class AddClassroom : Window, INotifyPropertyChanged
    {

        public delegate void DataChangedEventHandler(object sender, EventArgs e);

        public event DataChangedEventHandler DataChanged;

        public ObservableCollection<Software> SoftwaresCollection
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

        private void OnSftwObjectCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            foreach (SelectableObject<Software> sftwObject in SoftwaresSelectedCollection)
                if (sftwObject.IsSelected)
                {
                    Console.WriteLine(sftwObject.ObjectData.Name);
                }
        }

        public AddClassroom()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.DataContext = this;
           // MainWindow.ConsoleAllocator.ShowConsoleWindow();
            DatabaseManager databaseManager = new DatabaseManager();
            MySqlCommand cmd = new MySqlCommand("Select * from softwares;");
            this.SoftwaresCollection = databaseManager.GetCollectionSoftwares(cmd);

            this.SoftwaresSelectedCollection = new ObservableCollection<SelectableObject<Software>>();

            for (int i = 0; i < SoftwaresCollection.Count; i++)
            {
                this.SoftwaresSelectedCollection.Add(new SelectableObject<Software>(SoftwaresCollection[i], false));
            }
            // _-------------_______________----------------___________________
            /* PRODJI PONOVO KROZ OVU kolekciju SoftwaresSelectedCollection i gdeje IsSelected true, upisi u bazu */
            // ok momak :D 
            // _-------------_______________----------------___________________
            this.Brojevi = new ObservableCollection<int>();
            for (int i = 1; i < 121; i++)
                this.Brojevi.Add(i);
      
            InitializeComponent();
        }

        private void ClassroomAdded_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
            DatabaseManager db = new DatabaseManager();

            string _id = classroomID.Text;
            string _desc = description.Text;
            int _size = Int32.Parse(size.Text);
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

            bool ok = true;
            bool deleted = false;
            ObservableCollection<Classroom> classrooms = db.GetCollectionClassrooms(new MySqlCommand("Select * from classrooms;"));
            foreach (var classroom in classrooms)
            {
                if (String.IsNullOrEmpty(_id) || String.IsNullOrEmpty(_desc))
                {
                    MessageBox.Show("Greška u dodavanju! Popunite sva polja.");
                    ok = false;
                    break;
                }

                if (classroom.Id.Equals(_id))
                {
                    MessageBox.Show("Greška! Uneta oznaka učionice već postoji!");
                    ok = false;
                    break;
                }
            }

            if (ok)
            {

                ObservableCollection<Software> _software = new ObservableCollection<Software>();


                MySqlCommand cmd = new MySqlCommand("insert into hci.classrooms(classroomId,description,size,haveProjector,haveBoard,haveSmartBoard,operatingSys,deleted)"
                  + "values ('" + _id + "','" + _desc + "'," + _size + "," + _projector + "," + _board + "," + _smartBoard + ",'" + _os + "'," + deleted + ");");

                db.ExecuteQuery(cmd);
                foreach (SelectableObject<Software> sftwObject in SoftwaresSelectedCollection)
                    if (sftwObject.IsSelected)
                    {
                        _software.Add(sftwObject.ObjectData);
                        var classroom_db_id = db.get_id(new MySqlCommand("Select ID from classrooms where classroomId=\"" + _id + "\";"));
                        var software_db_id = db.get_id(
                            new MySqlCommand("Select ID from softwares where softwareId=\"" + sftwObject.ObjectData.Id + "\";"));
                        MySqlCommand cmd2 = new MySqlCommand("insert into hci.softwareInClassroom(classroomId, softwareId) values ('" + 
                            classroom_db_id + "','" + software_db_id + "');");
                        db.ExecuteQuery(cmd2);
                    }


                Classroom c = new Classroom(_id, _desc, _size, _projector, _board, _smartBoard, _os, _software, false);


                MessageBox.Show("Classroom successfully added!");

                DataChangedEventHandler handler = DataChanged;
                if (handler != null)
                {
                    handler(this, new EventArgs());
                }
                this.Close();
            }
        }

        private void ClassroomAdded_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ClassroomClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            this.Close();
        }

        private void ClassroomClose_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
