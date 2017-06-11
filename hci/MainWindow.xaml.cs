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
           
           

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            ConsoleAllocator.ShowConsoleWindow();
         
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
                classroomTable.Visibility = Visibility.Visible;
                labelUcionice.Visibility = Visibility.Visible;

                softwareTable.Visibility = Visibility.Collapsed;
                labelSoftveri.Visibility = Visibility.Collapsed;
                subjectTable.Visibility = Visibility.Collapsed;
                labelPredmeti.Visibility = Visibility.Collapsed;
                courseTable.Visibility = Visibility.Collapsed;
                labelSmerovi.Visibility = Visibility.Collapsed;

                prikaziPredmete.IsChecked = false;
                prikaziSoftver.IsChecked = false;
                prikaziSmerove.IsChecked = false;
                // MenuItemDatoteka.Foreground = Brushes.White;
                // MenuItemDatoteka.Background = Brushes.Black;
            }
            else
            {
                classroomTable.Visibility = Visibility.Collapsed;
                labelUcionice.Visibility = Visibility.Collapsed;

                // ZA DEMO
                // ----- menjanje boje
                // SolidColorBrush sivaBoja = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                // MenuItemDatoteka.Background = sivaBoja;
                // ---- otvaranje submenua
               // MenuItemDatoteka.IsSubmenuOpen = true;

            }


        }



        private void ViewSoftwareCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            if (prikaziSoftver.IsChecked)
            {
                softwareTable.Visibility = Visibility.Visible;
                labelSoftveri.Visibility = Visibility.Visible;

                classroomTable.Visibility = Visibility.Collapsed;
                labelUcionice.Visibility = Visibility.Collapsed;
                subjectTable.Visibility = Visibility.Collapsed;
                labelPredmeti.Visibility = Visibility.Collapsed;
                courseTable.Visibility = Visibility.Collapsed;
                labelSmerovi.Visibility = Visibility.Collapsed;

                prikaziPredmete.IsChecked = false;
                prikaziUcionice.IsChecked = false;
                prikaziSmerove.IsChecked = false;
            }
            else
            {
                softwareTable.Visibility = Visibility.Collapsed;
                labelSoftveri.Visibility = Visibility.Collapsed;

            }


        }



        private void ViewSubjectsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            if (prikaziPredmete.IsChecked)
            {
                subjectTable.Visibility = Visibility.Visible;
                labelPredmeti.Visibility = Visibility.Visible;

                softwareTable.Visibility = Visibility.Collapsed;
                labelSoftveri.Visibility = Visibility.Collapsed;
                classroomTable.Visibility = Visibility.Collapsed;
                labelUcionice.Visibility = Visibility.Collapsed;
                courseTable.Visibility = Visibility.Collapsed;
                labelSmerovi.Visibility = Visibility.Collapsed;

                prikaziUcionice.IsChecked = false;
                prikaziSoftver.IsChecked = false;
                prikaziSmerove.IsChecked = false;
            }
            else
            {
                subjectTable.Visibility = Visibility.Collapsed;
                labelPredmeti.Visibility = Visibility.Collapsed;

            }


        }



        private void ViewCoursesCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (prikaziSmerove.IsChecked)
            {
                courseTable.Visibility = Visibility.Visible;
                labelSmerovi.Visibility = Visibility.Visible;

                softwareTable.Visibility = Visibility.Collapsed;
                labelSoftveri.Visibility = Visibility.Collapsed;
                subjectTable.Visibility = Visibility.Collapsed;
                labelPredmeti.Visibility = Visibility.Collapsed;
                classroomTable.Visibility = Visibility.Collapsed;
                labelUcionice.Visibility = Visibility.Collapsed;

                prikaziPredmete.IsChecked = false;
                prikaziSoftver.IsChecked = false;
                prikaziUcionice.IsChecked = false;
            }
            else
            {
                courseTable.Visibility = Visibility.Collapsed;
                labelSmerovi.Visibility = Visibility.Collapsed;

            }

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
    }
}
