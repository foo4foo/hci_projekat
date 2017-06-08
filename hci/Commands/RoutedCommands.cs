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
                new KeyGesture(Key.C, ModifierKeys.Alt)
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

        public static readonly RoutedUICommand AddSoftwareCommand = new RoutedUICommand(
             "Add software",
             "Add software",
             typeof(RoutedCommands),
             new InputGestureCollection()
             {
                new KeyGesture(Key.S, ModifierKeys.Alt)
             }
         );

        public static readonly RoutedUICommand AddCourseCommand = new RoutedUICommand(
             "Add course",
             "Add course",
             typeof(RoutedCommands),
             new InputGestureCollection()
             {
                new KeyGesture(Key.R, ModifierKeys.Alt)
             }
         );
    }
}
