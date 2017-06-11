using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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

        private bool demo = false;

        private ObservableCollection<string> courses_names;

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

            DatabaseManager databaseManager = new DatabaseManager();
            MySqlCommand cmd = new MySqlCommand("Select * from softwares;");
            var sc = databaseManager.GetCollectionSoftwares(cmd);

            this.SoftwaresSelectedCollection = new ObservableCollection<SelectableObject<Software>>();

            for (int i = 0; i < sc.Count; i++)
            {
                this.SoftwaresSelectedCollection.Add(new SelectableObject<Software>(sc[i], false));
            }

            courses_names = new ObservableCollection<string>();

            MySqlCommand cmd2 = new MySqlCommand("Select * from courses;");
            var cc = databaseManager.GetCollectionCourses(cmd2);
            foreach (var el in cc)
            {
                courses_names.Add(el.Name);
                Console.WriteLine(el.Name);
            }

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            ConsoleAllocator.ShowConsoleWindow();
            Console.WriteLine(this.SoftwaresSelectedCollection[0].ObjectData.Name);
            db = new DatabaseManager();
            InitializeComponent();

            showClassroomTable();
            showCourseTable();
            showSoftwareTable();
            showSubjectTable();

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

        // metode za komandu

        private void showClassroomTable()
        {
            classrooms = db.GetCollectionClassrooms(new MySqlCommand("Select * from classrooms;"));

            var classroomSoftware = db.GetSoftwareList(new MySqlCommand(
                "select sc.classroomId, s.softwareId, s.name, s.operatingSys, s.developer, s.site, s.year, s.price, "
                +
                "s.description from softwares as s left join softwareInClassroom as sc on sc.softwareId = s.softwareId;"
            ));

            foreach (var classroom in classrooms)
            {
                foreach (var cSoftware in classroomSoftware)
                {
                    if (classroom.Id.Equals(cSoftware["constraintId"]))
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

            classroomTable.ItemsSource = this.classrooms;
            Raspored.ItemsSource = new ObservableCollection<Classroom>();
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
            "s.description from softwares as s left join softwareInSubject as ss on ss.softwareId = s.softwareId;"
        ));

            foreach (var subject in subjects)
            {
                foreach (var cSoftware in subjectSoftware)
                {
                    if (subject.Id.Equals(cSoftware["constraintId"]))
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
    }
}
