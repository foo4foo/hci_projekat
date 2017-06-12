using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using hci.Database_Manager;
using hci.Input_Forms;
using hci.Models;
using MySql.Data.MySqlClient;
using System.Timers;

namespace hci
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Course> courses;
      

        private ObservableCollection<Subject> subjects;

        private ObservableCollection<Classroom> classrooms;

        private ObservableCollection<Software> software;
    
        private DatabaseManager db;

        public ObservableCollection<int> Brojevi
        {
            get;
            set;
        }

        public ObservableCollection<int> Godine
        {
            get;
            set;
        }

        private bool demo = false;


        public ObservableCollection<SelectableObject<Software>> SoftwaresSelectedCollection
        {
            get;
            set;
        }

        internal static class ConsoleAllocator
        {
            [DllImport(@"kernel32.dll", SetLastError = true)]
            static extern bool AllocConsole();

            [DllImport(@"kernel32.dll")]
            static extern IntPtr GetConsoleWindow();

            [DllImport(@"user32.dll")]
            static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            const int SwHide = 0;
            const int SwShow = 5;



            public static void ShowConsoleWindow()
            {
                var handle = GetConsoleWindow();

                if (handle == IntPtr.Zero)
                {
                    AllocConsole();
                }
                else
                {
                    ShowWindow(handle, SwShow);
                }
            }

            public static void HideConsoleWindow()
            {
                var handle = GetConsoleWindow();

                ShowWindow(handle, SwHide);
            }
        }

        public MainWindow()
        {
            this.DataContext = this;

            db = new DatabaseManager();
            ConsoleAllocator.ShowConsoleWindow();
            MySqlCommand cmd = new MySqlCommand("Select * from softwares;");
            var sc = db.GetCollectionSoftwares(cmd);

            this.SoftwaresSelectedCollection = new ObservableCollection<SelectableObject<Software>>();
           
            for (int i = 0; i < sc.Count; i++)
            {
                this.SoftwaresSelectedCollection.Add(new SelectableObject<Software>(sc[i], false));
                
            }

            this.Brojevi = new ObservableCollection<int>();
            for (int i = 1; i < 121; i++)
                this.Brojevi.Add(i);

            this.Godine = new ObservableCollection<int>();
            for (int i = 2017; i > 1944; i--)
                this.Godine.Add(i);

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
          
        
            InitializeComponent();

            /*____________-----------------------___________________*/
            // Ovde su dodavani podaci za comboboxove (velicine itd) jer
            // iz xaml-a nece da radi
            cb1Sub.ItemsSource = this.Brojevi; // for subject
            cb2Sub.ItemsSource = this.Brojevi; // --||--
            cb3Sub.ItemsSource = this.Brojevi; // --||--

            cbSoftware.ItemsSource = this.Godine; // for software

            cbClassroom.ItemsSource = this.Brojevi; // for classroom

            /*____________-----------------------___________________*/
            showClassroomTable();
            showCourseTable();
            showSoftwareTable();
            showSubjectTable();

            MakeRaspored(); //prepare table

            string s = TableValue(0, 0, classrooms.Count + 1);  //read string from coordinates
                                                                //Console.Write(s);

            
            editSmerovi.ItemsSource = courses;
            editSoftverZaPredmet.ItemsSource = SoftwaresSelectedCollection;
            editSoftverZaUcionicu.ItemsSource = SoftwaresSelectedCollection;
            softwareTable.Visibility = Visibility.Collapsed;
            labelSoftveri.Visibility = Visibility.Collapsed;
            subjectTable.Visibility = Visibility.Collapsed;
            labelPredmeti.Visibility = Visibility.Collapsed;
            courseTable.Visibility = Visibility.Collapsed;
            labelSmerovi.Visibility = Visibility.Collapsed;
            editSoftware.Visibility = Visibility.Collapsed;
            editCourses.Visibility = Visibility.Collapsed;
            editSubjects.Visibility = Visibility.Collapsed;

        }

        private void New_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not yet implemented");
        }
   
        public void MakeRaspored()
        {
            for (int p = 0; p < subjects.Count + 1; p++)
                PredmetiZaDrop.ColumnDefinitions.Add(new ColumnDefinition());
            for (int z = 0; z < 2; z++)
            {
                RowDefinition r = new RowDefinition();
                if (z == 0)
                    r.Height = new GridLength(2, GridUnitType.Star);
                else r.Height = new GridLength(5, GridUnitType.Star);
                PredmetiZaDrop.RowDefinitions.Add(r);
            }


            for (int x = 1; x < subjects.Count + 1; x++)
            {
                for (int z = 1; z < 2; z++)
                {
                    TextBox tb = new TextBox();
                   // tb.AllowDrop = true;
                   

                   // tb.PreviewDragEnter += TextBox_DragEnter;
                  //  tb.PreviewDrop += TextBox_Drop;

                     tb.IsReadOnly = true;
                    if (z == 1)
                    {
                        tb.Text = subjects[x - 1].Name;
                    }
                    else
                    {
                        //tb.Text = "xxx";
                       
                    }
                    Grid.SetColumn(tb, x);
                    Grid.SetRow(tb, z);
                  
                    string name = "cell_" + x + "_" + z;
                    tb.Name = name;
                    PredmetiZaDrop.Children.Add(tb);
                }
            }



            for (int x = 0; x < classrooms.Count + 1; x++)
                MyGrid.ColumnDefinitions.Add(new ColumnDefinition());
            for (int z = 0; z < 63; z++)
            {
                RowDefinition r = new RowDefinition();
                if (z == 0)
                    r.Height = new GridLength(2, GridUnitType.Star);
                else r.Height = new GridLength(5, GridUnitType.Star);
                MyGrid.RowDefinitions.Add(r);
            }


            // -------------- kolona za vreme ------------------
            int j = 0;
            int y = 0;

            for (double i = 7.0; i <= 22;)
            {
                TextBox tb = new TextBox();
                tb.FontWeight = FontWeights.Bold;

                tb.IsReadOnly = true;
                
              
                if (y == 0) { tb.Text = "Vreme / Učionice"; Grid.SetRow(tb, 3); }


                else
                {
                    double d = i + 0.15 * j;
                    if (j == 0)
                    {
                        tb.Text = d + ".00 h";
                    }
                    else if (j == 2)
                    {
                        tb.Text = d + "0 h";
                    }
                    else
                        tb.Text = d + " h";

                    j++;

                }
                y++;
                if (i == 22) { tb.Text = i + ".00 h"; Grid.SetRow(tb, y);  MyGrid.Children.Add(tb); break; }
                if (j == 4)
                {
                    j = 0;
                    i += 1;
                }
                Grid.SetColumn(tb, 0);
              
                Grid.SetRow(tb, y);
                MyGrid.Children.Add(tb);
            }

            // --- kraj kolone za vreme ----



            for (int x = 1; x < classrooms.Count + 1; x++)
            {
                for (int z = 1; z < 63; z++)
                {
                    TextBox tb = new TextBox();
                    tb.FontWeight = FontWeights.Bold;
                  //  tb.IsReadOnly = true;
                    if (z == 1) {
                        tb.Text = classrooms[x - 1].Id;
                        tb.IsReadOnly = true;
                    }
                    else
                    {
                        tb.AllowDrop = true;
                        tb.PreviewDragEnter += TextBox_DragEnter;
                        tb.PreviewDrop += TextBox_Drop;
                        tb.PreviewDragLeave += TextBox_DragLeave;
                        //tb.Text = "";
                    }
                    Grid.SetColumn(tb, x);
                    Grid.SetRow(tb, z);
                    string name = "cell_" + x + "_" + z;
                    tb.Name = name;
                    MyGrid.RegisterName(tb.Name, tb);
                   
                    MyGrid.Children.Add(tb);
                }
            }
         
         

        }

        public string TableValue(int column, int row, int rows)
        {
            int i = row + column * rows;
            return ((TextBox)MyGrid.Children[i]).Text;
        }


        // metode za komandu

        private void showClassroomTable()
        {
            classrooms = db.GetCollectionClassrooms(new MySqlCommand("Select * from classrooms;"));

            var classroomSoftware = db.GetSoftwareList(new MySqlCommand(
                "select sc.classroomId, s.softwareId, s.name, s.operatingSys, s.developer, s.site, s.year, s.price, "
                +
                "s.description, s.deleted from softwares as s left join softwareInClassroom as sc on sc.softwareId = s.ID;"
            ));

            foreach (var classroom in classrooms)
            {
                if (!classroom.Deleted)
                {
                    foreach (var cSoftware in classroomSoftware)
                    {
                        if (cSoftware["deleted"].Equals("True"))
                            continue;
                        if (classroom.DbId.Equals(cSoftware["ID"]))
                        {
                            var software = new Software();
                            software.Id = cSoftware["softwareId"];
                            software.Name = cSoftware["softwareName"];
                            software.Description = cSoftware["softwareDescription"];
                            software.Developer = cSoftware["softwareDeveloper"];
                            software.Os = cSoftware["softwareOs"];
                            software.Year = Convert.ToInt32(cSoftware["softwareYear"]);
                            software.Site = cSoftware["softwareSite"];
                            software.Price = Convert.ToDouble(cSoftware["softwarePrice"]);

                            classroom.TestS.Add(software.Name);


                        }
                    }
                }
            }

            classroomTable.ItemsSource = this.classrooms;
           
        }

        private void TextBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            var tb = e.Source as TextBox;
         
        }


        private void TextBox_DragLeave(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            var tb = e.Source as TextBox;
            int f = Grid.GetRowSpan(tb);
            string ff = tb.Text;
            if (f == 3 && ff.Equals("")) Grid.SetRowSpan(tb, 1);
        }

        private void TextBox_Drop(object sender, DragEventArgs e)
        {

            var tb = e.Source as TextBox;
            string reg_name = tb.Name;
            string[] tokens = reg_name.Split('_');
            int row = Int32.Parse(tokens[2]);
                       
            Grid.SetRow(tb, row-2);
            Grid.SetRowSpan(tb, 3);
          /*  int row2 = row + 2;
            Console.Write(tokens[0] + "_" + tokens[1] + "_" + row2);
             TextBox tb2 = (TextBox)this.MyGrid.FindName(tokens[0] + "_" + tokens[1] + "_" + row2);
            Grid.SetRow(tb2, row2 - 2);
            Grid.SetRowSpan(tb2, 3); */
        }

        private void showCourseTable()
        {
  
            this.courses = db.GetCollectionCourses(new MySqlCommand("Select * from courses;"));
            courseTable.ItemsSource = this.courses;
        }

        private void showSoftwareTable()
        {
            this.software = db.GetCollectionSoftwares(new MySqlCommand("Select * from softwares;"));
            softwareTable.ItemsSource = this.software;
        }

        private void showSubjectTable()
        {
            this.subjects = db.GetCollectionSubjects(new MySqlCommand("Select * from subjects;"));

            var subjectSoftware = db.GetSoftwareList(new MySqlCommand(
            "select ss.subjectId, s.softwareId, s.name, s.operatingSys, s.developer, s.site, s.year, s.price, "
            +
            "s.description, s.deleted from softwares as s left join softwareInSubject as ss on ss.softwareId = s.ID;"
        ));

            foreach (var subject in subjects)
            {
                foreach (var cSoftware in subjectSoftware)
                {
                    if (cSoftware["deleted"].Equals("True"))
                        continue;
                    if (subject.DbId.Equals(cSoftware["ID"]))
                    {
                        var software = new Software();
                        software.Id = cSoftware["softwareId"];
                        software.Name = cSoftware["softwareName"];
                        software.Description = cSoftware["softwareDescription"];
                        software.Developer = cSoftware["softwareDeveloper"];
                        software.Os = cSoftware["softwareOs"];
                        software.Year = Convert.ToInt32(cSoftware["softwareYear"]);
                        software.Site = cSoftware["softwareSite"];
                        software.Price = Convert.ToDouble(cSoftware["softwarePrice"]);

                        subject.TestS.Add(software.Name);


                    }
                }
            }


            subjectTable.ItemsSource = this.subjects;
        }

        private void ViewClassroomsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (prikaziUcionice.IsChecked)
            {

                visibleClassroomsTable();
            }
            else
            {
                classroomTable.Visibility = Visibility.Collapsed;
                labelUcionice.Visibility = Visibility.Collapsed;


            }


        }

        private void visibleClassroomsTable()
        {
            classroomTable.Visibility = Visibility.Visible;
            labelUcionice.Visibility = Visibility.Visible;
            editClassrooms.Visibility = Visibility.Visible;

            softwareTable.Visibility = Visibility.Collapsed;
            labelSoftveri.Visibility = Visibility.Collapsed;
            subjectTable.Visibility = Visibility.Collapsed;
            labelPredmeti.Visibility = Visibility.Collapsed;
            courseTable.Visibility = Visibility.Collapsed;
            labelSmerovi.Visibility = Visibility.Collapsed;
            editSoftware.Visibility = Visibility.Collapsed;
            editCourses.Visibility = Visibility.Collapsed;
            editSubjects.Visibility = Visibility.Collapsed;

            prikaziPredmete.IsChecked = false;
            prikaziSoftver.IsChecked = false;
            prikaziSmerove.IsChecked = false;
        }



        private void ViewSoftwareCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            if (prikaziSoftver.IsChecked)
            {
                visibleSoftwareTable();
            }
            else
            {
                softwareTable.Visibility = Visibility.Collapsed;
                labelSoftveri.Visibility = Visibility.Collapsed;

            }


        }

        private void visibleSoftwareTable()
        {
            softwareTable.Visibility = Visibility.Visible;
            labelSoftveri.Visibility = Visibility.Visible;
            editSoftware.Visibility = Visibility.Visible;

            classroomTable.Visibility = Visibility.Collapsed;
            labelUcionice.Visibility = Visibility.Collapsed;
            subjectTable.Visibility = Visibility.Collapsed;
            labelPredmeti.Visibility = Visibility.Collapsed;
            courseTable.Visibility = Visibility.Collapsed;
            labelSmerovi.Visibility = Visibility.Collapsed;
            editClassrooms.Visibility = Visibility.Collapsed;
            editCourses.Visibility = Visibility.Collapsed;
            editSubjects.Visibility = Visibility.Collapsed;

            prikaziPredmete.IsChecked = false;
            prikaziUcionice.IsChecked = false;
            prikaziSmerove.IsChecked = false;
        }



        private void ViewSubjectsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            if (prikaziPredmete.IsChecked)
            {
                visibleSubjectsTable();
            }
            else
            {
                subjectTable.Visibility = Visibility.Collapsed;
                labelPredmeti.Visibility = Visibility.Collapsed;

            }


        }

        private void visibleSubjectsTable()
        {
            subjectTable.Visibility = Visibility.Visible;
            labelPredmeti.Visibility = Visibility.Visible;
            editSubjects.Visibility = Visibility.Visible;

            softwareTable.Visibility = Visibility.Collapsed;
            labelSoftveri.Visibility = Visibility.Collapsed;
            classroomTable.Visibility = Visibility.Collapsed;
            labelUcionice.Visibility = Visibility.Collapsed;
            courseTable.Visibility = Visibility.Collapsed;
            labelSmerovi.Visibility = Visibility.Collapsed;
            editSoftware.Visibility = Visibility.Collapsed;
            editClassrooms.Visibility = Visibility.Collapsed;
            editCourses.Visibility = Visibility.Collapsed;

            prikaziUcionice.IsChecked = false;
            prikaziSoftver.IsChecked = false;
            prikaziSmerove.IsChecked = false;
        }



        private void ViewCoursesCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (prikaziSmerove.IsChecked)
            {
                visibleCoursesTable();

            }
            else
            {
                courseTable.Visibility = Visibility.Collapsed;
                labelSmerovi.Visibility = Visibility.Collapsed;

            }

        }

        private void visibleCoursesTable()
        {
            courseTable.Visibility = Visibility.Visible;
            labelSmerovi.Visibility = Visibility.Visible;
            editCourses.Visibility = Visibility.Visible;

            softwareTable.Visibility = Visibility.Collapsed;
            labelSoftveri.Visibility = Visibility.Collapsed;
            subjectTable.Visibility = Visibility.Collapsed;
            labelPredmeti.Visibility = Visibility.Collapsed;
            classroomTable.Visibility = Visibility.Collapsed;
            labelUcionice.Visibility = Visibility.Collapsed;
            editSoftware.Visibility = Visibility.Collapsed;
            editSubjects.Visibility = Visibility.Collapsed;
            editClassrooms.Visibility = Visibility.Collapsed;

            prikaziPredmete.IsChecked = false;
            prikaziSoftver.IsChecked = false;
            prikaziUcionice.IsChecked = false;
        }

        private void ViewSoftware_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ViewSubjects_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ViewCourses_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ViewClassrooms_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddClassroomCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var addClassroom = new AddClassroom();
            addClassroom.DataChanged += DataChanged;
            addClassroom.ShowDialog();

        }
        private void DataChanged(object sender, EventArgs e)
        {
            //System.Windows.MessageBox.Show("Something has happened", "Parent");
            showClassroomTable();
            showCourseTable();
            showSoftwareTable();
            showSubjectTable();

        }

        private void SaveAllCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SaveAllCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // prodji kroz sve 4 kolekcije i uradi update u bazi
            var db = new DatabaseManager();

            try
            {
                foreach (var software in this.software)
                {
                    string stmt = "Update softwares set softwareId=\"" + software.Id + "\",name=\"" + software.Name +
                                  "\",operatingSys=\""
                                  + software.Os + "\",developer=\"" + software.Developer + "\",site=\"" +
                                  software.Site + "\",year=" + software.Year
                                  + ",price=" + software.Price + ",description=\"" + software.Description +
                                  "\",deleted=" + software.Deleted
                                  + " where ID=" + software.DbId + ";";
                    Console.WriteLine(stmt);
                    MySqlCommand cmd = new MySqlCommand(stmt);
                    db.ExecuteQuery(cmd);
                }

                foreach (var classroom in this.classrooms)
                {
                    string stmt = "Update classrooms set classroomId=\"" + classroom.Id + "\",description=\"" +
                                  classroom.Description + "\",size=\""
                                  + classroom.Size + "\",haveProjector=" + classroom.HaveProjector + ",haveBoard=" +
                                  classroom.HaveBoard
                                  + ",haveSmartBoard=" + classroom.HaveSmartBoard
                                  + ",operatingSys=\"" + classroom.OperatingSys + "\",deleted=" + classroom.Deleted
                                  + " where ID=" + classroom.DbId + ";";
                    Console.WriteLine(stmt);
                    MySqlCommand cmd = new MySqlCommand(stmt);
                    db.ExecuteQuery(cmd);
                }

                foreach (var subject in this.subjects)
                {
                    string stmt = "Update subjects set subjectId=\"" + subject.Id + "\",description=\"" +
                                  subject.Description + "\",size=\""
                                  + subject.Size + "\",needProjector=" + subject.NeedProjector + ",needBoard=" +
                                  subject.NeedBoard
                                  + ",needSmartBoard=" + subject.NeedSmartBoard + ",name=\"" + subject.Name +
                                  "\",minLength=\"" + subject.MinLength + "\""
                                  + ",needOperatingSys=\"" + subject.Os + "\",deleted=" + subject.Deleted +
                                  ",noOfClasses=\"" + subject.NoOfClasses + "\""
                                  + " where ID=" + subject.DbId + ";";
                    Console.WriteLine(stmt);
                    MySqlCommand cmd = new MySqlCommand(stmt);
                    db.ExecuteQuery(cmd);
                }

                foreach (var course in this.courses)
                {
                    string stmt = "Update courses set courseId=\"" + course.Id + "\",description=\"" +
                                  course.Description
                                  + "\",name=\"" + course.Name + "\",deleted=" + course.Deleted + ",date_=\"" +
                                  course.Date + "\""
                                  + " where ID=" + course.DbId + ";";
                    Console.WriteLine(stmt);
                    MySqlCommand cmd = new MySqlCommand(stmt);
                    db.ExecuteQuery(cmd);
                }

                showClassroomTable();
                showCourseTable();
                showSoftwareTable();
                showSubjectTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AddClassroomCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddSoftwareCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var addSoftware = new AddSoftware();
            addSoftware.DataChanged += DataChanged;
            addSoftware.ShowDialog();
        }

        private void AddSoftwareCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddCourseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var course = new AddCourse();
            course.DataChanged += DataChanged;
            course.ShowDialog();
        }

        private void AddCourseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddSubjectCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var subject = new AddSubject();
            subject.DataChanged += DataChanged;
            subject.ShowDialog();
        }

        private void AddSubjectCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private async void DemoMode_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!demo)
            {
                demo = true;
                DemoLabel.Visibility = Visibility.Visible;
                DemoButton.Visibility = Visibility.Visible;
                while (demo)
                {
                    var addClassroom = new AddClassroom();

                    await Task.Delay(1300);

                    blinkMenuItem(MenuItemDatoteka, 3);
                    if (!demo) break;
                    await Task.Delay(1800);
                    MenuItemDatoteka.IsSubmenuOpen = true;
                    blinkMenuItem(MenuItemDodaj, 2);
                    if (!demo) break;
                    await Task.Delay(1200);
                    MenuItemDodaj.IsSubmenuOpen = true;
                    blinkMenuItem(MenuItemDodajUcionicu, 3);
                    if (!demo) break;
                    await Task.Delay(1800);
                    MenuItemDatoteka.IsSubmenuOpen = false;


                    addClassroom.Show();
                    if (!demo) { addClassroom.Close(); break; }
                    await Task.Delay(700);
                    addClassroom.classroomID.Text = "OZN1";
                    await Task.Delay(700);
                    if (!demo) { addClassroom.Close(); break; }
                    addClassroom.description.Text = "Opis za demonstraciju";
                    await Task.Delay(700);
                    if (!demo) { addClassroom.Close(); break; }
                    addClassroom.softwares.IsDropDownOpen = true;
                    await Task.Delay(1000);
                    if (!demo) { addClassroom.Close(); break; }
                    addClassroom.softwares.IsDropDownOpen = false;
                    await Task.Delay(1300);
                    addClassroom.Close();

                    blinkMenuItem(Prikaz, 3);
                    if (!demo) break;
                    await Task.Delay(1800);
                    Prikaz.IsSubmenuOpen = true;
                    blinkMenuItem(prikaziPredmete, 2);
                    if (!demo) break;
                    await Task.Delay(1200);
                    prikaziPredmete.IsChecked = true;
                    visibleSubjectsTable();
                    if (!demo) break;
                    await Task.Delay(1300);
                    prikaziSoftver.IsChecked = true;
                    visibleSoftwareTable();
                    if (!demo) break;
                    await Task.Delay(1300);
                    prikaziUcionice.IsChecked = true;
                    visibleClassroomsTable();
                    if (!demo) break;
                    await Task.Delay(1000);
                    Prikaz.IsSubmenuOpen = false;
                    await Task.Delay(1000);
                    MenuItemDatoteka.IsSubmenuOpen = true;
                    blinkMenuItem(SacuvajMenuItem, 3);
                    for (int i = 0; i < 3; i++)
                    {
                        SacuvajButton.Background = Brushes.Black;
                        SacuvajButton.Foreground = Brushes.White;
                        await Task.Delay(300);
                        SacuvajButton.Foreground = Brushes.Black;
                        SacuvajButton.Background = Brushes.White;
                        await Task.Delay(300);
                    }
                    if (!demo) break;
                    await Task.Delay(1800);
                    if (!demo) break;
                    MenuItemDatoteka.IsSubmenuOpen = false;
                    for (int i = 0; i < 5; i++)
                    {
                        prevuciteLabel.FontWeight = FontWeights.Bold;
                        prevuciteLabel.FontSize = 14;
                        prevuciteLabel.Visibility = Visibility.Collapsed;
                        await Task.Delay(300);
                        prevuciteLabel.Visibility = Visibility.Visible;
                        await Task.Delay(300);
                    }
                    prevuciteLabel.FontSize = 12;
                    prevuciteLabel.FontWeight = FontWeights.Normal;
                    if (!demo) break;

                }
            }
        }

        private void DemoMode_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        private async void blinkMenuItem(MenuItem mi, int blinks)
        {
            SolidColorBrush sivaBoja = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            for (int i = 0; i < blinks; i++)
            {
                mi.Background = Brushes.Black;
                mi.Foreground = Brushes.White;
                await Task.Delay(300);
                mi.Foreground = Brushes.Black;
                mi.Background = sivaBoja;
                await Task.Delay(300);
            }
        }

        private void DemoButton_Click(object sender, RoutedEventArgs e)
        {
            demo = false;
            DemoLabel.Visibility = Visibility.Hidden;
            DemoButton.Visibility = Visibility.Hidden;
        }

        private bool KeyEnteredIsValid(string key)
        {
            Regex regex;
            regex = new Regex("[^0-9]+\\.?[0-9]+"); //regex that matches disallowed text
            return regex.IsMatch(key);
        }

        private void Price_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = KeyEnteredIsValid(e.Text);
        }
    }
}
