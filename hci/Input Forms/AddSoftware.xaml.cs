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
    /// Interaction logic for AddSoftware.xaml
    /// </summary>
    public partial class AddSoftware : Window
    {

        public delegate void DataChangedEventHandler(object sender, EventArgs e);

        public event DataChangedEventHandler DataChanged;
        public ObservableCollection<int> Godine
        {
            get;
            set;
        }

        public AddSoftware()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.DataContext = this;
            this.Godine = new ObservableCollection<int>();
            for (int i = 2017; i > 1944; i--)
                this.Godine.Add(i);
            InitializeComponent();

        }


        private void SoftwareAdded_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DatabaseManager db = new DatabaseManager();

            string _id = id.Text;
            double _price = -1;
            string _name = name.Text;
            string _developer = Developer.Text;
            string _website = Website.Text;
            string _os = Os.Text;
            int _year = Int32.Parse(Year.Text);
            string _desc = Description.Text;

            bool ok = true;
            ObservableCollection<Software> softwares = db.GetCollectionSoftwares(new MySqlCommand("Select * from softwares;"));
            foreach (var software in softwares)
            {
                if (String.IsNullOrEmpty(_id) || String.IsNullOrEmpty(Price.Text) || String.IsNullOrEmpty(_name) || String.IsNullOrEmpty(_developer)
                    || String.IsNullOrEmpty(_website) || String.IsNullOrEmpty(_desc))
                {
                    MessageBox.Show("Greška u dodavanju! Popunite sva polja.");
                    ok = false;
                    break;
                }
                if (software.Id.Equals(_id))
                {
                    MessageBox.Show("Greška! Uneta oznaka softvera već postoji!");
                    ok = false;
                    break;
                }

                if (_id.Length > 14)
                {
                    MessageBox.Show("Greška u dodavanju! Oznaka softvera ne sme imati preko 15 karaktera.");
                    ok = false;
                    break;
                }


                if (_name.Length > 30)
                {
                    MessageBox.Show("Greška u dodavanju! Naziv softvera je predugačak.");
                    ok = false;
                    break;
                }

                if (_developer.Length > 50)
                {
                    MessageBox.Show("Greška u dodavanju! Naziv proizvođača je predugačak.");
                    ok = false;
                    break;
                }

                if (_website.Length > 30)
                {
                    MessageBox.Show("Greška u dodavanju! Naziv sajta je predugačak.");
                    ok = false;
                    break;
                }

                if (_desc.Length > 30)
                {
                    MessageBox.Show("Greška u dodavanju! Opis softvera je predugačak.");
                    ok = false;
                    break;
                }

                if (!(Double.TryParse(Price.Text, out _price)))
                {
                    MessageBox.Show("Greška prilikom unosa cene! Unesite validan broj.");
                    ok = false;
                    break;
                }
                else if (_price < 0)
                   {
                    MessageBox.Show("Greška! Cena ne može biti negativan broj.");
                    ok = false;
                    break;
                }
                   
            }

            if (ok)
            {
               

                Software s = new Software(_id, _name, _developer, _website, _desc, _os, _year, _price, false);


                MySqlCommand cmd = new MySqlCommand("insert into hci.softwares(softwareId,name,operatingSys,developer,site,year,price,description,deleted)"
                  + "values ('" + _id + "','" + _name + "','" + _os + "','" + _developer + "','" + _website + "'," + _year + "," + _price + ",'" + _desc + "'," + false + ");");

                db.ExecuteQuery(cmd);
                MessageBox.Show("Softver uspešno dodat!");

                DataChangedEventHandler handler = DataChanged;
                if (handler != null)
                {
                    handler(this, new EventArgs());
                }

                this.Close();
            }
        }

        private void SoftwareAdded_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SoftwareClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            this.Close();
        }

        private void SoftwareClose_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ApplicationHelpCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).ApplicationHelpCommand_Executed(sender, e);
        }
    }
}
