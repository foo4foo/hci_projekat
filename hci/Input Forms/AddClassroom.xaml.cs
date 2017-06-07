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
        public ObservableCollection<Software> SoftwaresCollection
        {
            get;
            set;
        }

        public AddClassroom()
        {
            this.DataContext = this;
            // MainWindow.ConsoleAllocator.ShowConsoleWindow();
            DatabaseManager databaseManager = new DatabaseManager();
            MySqlCommand cmd = new MySqlCommand("Select * from softwares;");
            this.SoftwaresCollection = databaseManager.GetCollectionSoftwares(cmd);

            InitializeComponent();
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
