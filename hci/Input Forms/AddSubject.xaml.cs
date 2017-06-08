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
