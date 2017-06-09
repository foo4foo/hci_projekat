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
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            ConsoleAllocator.ShowConsoleWindow();
            db = new DatabaseManager();
            InitializeComponent();
        }

        private void New_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not yet implemented");
        }

        // metode za komandu

        private void ViewClassroomsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.classrooms = db.GetCollectionClassrooms(new MySqlCommand("Select * from classrooms;"));

            var classroomSoftware = db.GetClassroomsSoftware(new MySqlCommand(
                "select sc.classroomId, s.softwareId, s.name, s.operatingSys, s.developer, s.site, s.year, s.price, "
                +
                "s.description from softwares as s left join softwareInClassroom as sc on sc.softwareId = s.softwareId;"
            ));

            foreach (var classroom in classrooms)
            {
                foreach (var cSoftware in classroomSoftware)
                {
                    if (classroom.Id.Equals(cSoftware["classroomId"]))
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

            dataGrid.ItemsSource = this.classrooms;
        }

        private void ViewClassrooms_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ViewSoftwareCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.software = db.GetCollectionSoftwares(new MySqlCommand("Select * from softwares;"));
            dataGrid.ItemsSource = this.software;
        }

        private void ViewSoftware_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ViewSubjectsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.subjects = db.GetCollectionSubjects(new MySqlCommand("Select * from subjects;"));
            dataGrid.ItemsSource = this.subjects;
        }

        private void ViewSubjects_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ViewCoursesCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.courses = db.GetCollectionCourses(new MySqlCommand("Select * from courses;"));
            dataGrid.ItemsSource = this.courses;
        }

        private void ViewCourses_CanExecute(object sender, CanExecuteRoutedEventArgs e)
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
            addClassroom.ShowDialog();

        }

        private void AddClassroomCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddSoftwareCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var addSoftware = new AddSoftware();
            addSoftware.ShowDialog();
        }

        private void AddSoftwareCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddCourseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var course = new AddCourse();
            course.ShowDialog();
        }

        private void AddCourseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddSubjectCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var subject = new AddSubject();
            subject.ShowDialog();
        }

        private void AddSubjectCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
