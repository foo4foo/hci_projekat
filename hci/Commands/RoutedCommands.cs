using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace hci.Commands
{
    public static class RoutedCommands
    {
        public static readonly RoutedUICommand SaveAllCommand = new RoutedUICommand(
            "Save",
            "Save",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.S, ModifierKeys.Control)
            }            
            
        );

        public static readonly RoutedUICommand ExitCommand = new RoutedUICommand(
            "Exit",
            "Exit",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.Q, ModifierKeys.Control)
            }
        );

        public static readonly RoutedUICommand AddClassroomCommand = new RoutedUICommand(
            "Add classroom",
            "Add classroom",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.C, ModifierKeys.Alt | ModifierKeys.Control)
            }
        );


        public static readonly RoutedUICommand AddSoftwareCommand = new RoutedUICommand(
             "Add software",
             "Add software",
             typeof(RoutedCommands),
             new InputGestureCollection()
             {
                new KeyGesture(Key.S, ModifierKeys.Alt | ModifierKeys.Control)
             }
         );


        public static readonly RoutedUICommand AddCourseCommand = new RoutedUICommand(
             "Add course",
             "Add course",
             typeof(RoutedCommands),
             new InputGestureCollection()
             {
                new KeyGesture(Key.R, ModifierKeys.Alt | ModifierKeys.Control)
             }
         );

        public static readonly RoutedUICommand AddSubjectCommand = new RoutedUICommand(
           "Add subject",
           "Add subject",
           typeof(RoutedCommands),
           new InputGestureCollection()
           {
                new KeyGesture(Key.A, ModifierKeys.Alt | ModifierKeys.Control)
           }
       );

        public static readonly RoutedUICommand ClassroomAdded = new RoutedUICommand(
           "Add classroom",
           "Add classroom",
           typeof(RoutedCommands)
       );

        public static readonly RoutedUICommand ClassroomClose = new RoutedUICommand(
        "Close add-classroom window",
        "Close add-classroom window",
        typeof(RoutedCommands)
       );


        public static readonly RoutedUICommand SoftwareClose = new RoutedUICommand(
            "Close add-software window",
             "Close add-software window",
            typeof(RoutedCommands)
            );

        public static readonly RoutedUICommand SoftwareAdded = new RoutedUICommand(
         "Add software",
         "Add software",
         typeof(RoutedCommands)
     );

        public static readonly RoutedUICommand CourseClose = new RoutedUICommand(
       "Close add-Course window",
        "Close add-Course window",
       typeof(RoutedCommands)
       );

        public static readonly RoutedUICommand CourseAdded = new RoutedUICommand(
         "Add Course",
         "Add Course",
         typeof(RoutedCommands)
     );


        public static readonly RoutedUICommand SubjectClose = new RoutedUICommand(
       "Close add-Subject window",
        "Close add-Subject window",
       typeof(RoutedCommands)
       );

        public static readonly RoutedUICommand SubjectAdded = new RoutedUICommand(
         "Add Subject",
         "Add Subject",
         typeof(RoutedCommands)
     );

        public static readonly RoutedUICommand ViewSoftwareCommand = new RoutedUICommand(
                "View software",
                "View software",
                typeof(RoutedCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.S, ModifierKeys.Alt)
                }
        );

        public static readonly RoutedUICommand ViewCoursesCommand = new RoutedUICommand(
                "View courses",
                "View courses",
                typeof(RoutedCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.R, ModifierKeys.Alt)
                }
        );

        public static readonly RoutedUICommand ViewSubjectsCommand = new RoutedUICommand(
                "View subjects",
                "View subjects",
                typeof(RoutedCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.A, ModifierKeys.Alt)
                }
        );

        public static readonly RoutedUICommand ViewClassroomsCommand = new RoutedUICommand(
                "View classrooms",
                "View classrooms",
                typeof(RoutedCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.C, ModifierKeys.Alt)
                }
        );

        public static readonly RoutedUICommand DemoModeCommand = new RoutedUICommand(
         "Start demo mode",
         "Start demo mode",
         typeof(RoutedCommands),
         new InputGestureCollection()
             {
                    new KeyGesture(Key.A, ModifierKeys.Control)
             }
         );

        public static readonly RoutedUICommand HelpIndexCommand = new RoutedUICommand(
            "Help index",
            "Help index",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F1, ModifierKeys.Control)
            }
        );
    }
}
