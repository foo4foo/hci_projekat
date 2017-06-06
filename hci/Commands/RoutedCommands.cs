﻿using System;
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

    }
}
