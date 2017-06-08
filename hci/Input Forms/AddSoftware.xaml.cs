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

        public ObservableCollection<int> Godine
        {
            get;
            set;
        }

        public AddSoftware()
        {
            this.DataContext = this;
            this.Godine = new ObservableCollection<int>();
            for (int i = 2017; i > 1944; i--)
                this.Godine.Add(i);
            InitializeComponent();

        }


        private void SoftwareAdded_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string _id = id.Text;
            string _name = name.Text;
            string _developer = Developer.Text;
            string _website = Website.Text;
            string _os = Os.Text;
            int _year = Int32.Parse(Year.Text);
            double _price = Convert.ToDouble(Price.Text);
            string _desc = Description.Text;

            Software s = new Software(_id, _name, _developer, _website, _desc, _os, _year, _price);


            MySqlCommand cmd = new MySqlCommand("insert into hci.softwares(softwareId,name,operatingSys,developer,site,year,price,description)"
              + "values ('" + _id + "','" + _name + "','" + _os + "','" + _developer + "','" + _website + "'," + _year + "," + _price + ",'" + _desc + "');");
            DatabaseManager db = new DatabaseManager();
            db.ExecuteQuery(cmd);
            MessageBox.Show("Software successfully added!");
            this.Close();
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

    }
}
