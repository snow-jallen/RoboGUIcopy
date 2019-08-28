using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Shared
{
    public class BasicCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public BasicCommand(Action execute, Func<bool> canExecute=null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canExecute();
        }

        public void Execute(object parameter)
        {
            execute();
        }
    }
}
