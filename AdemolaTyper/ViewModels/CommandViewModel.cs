using System;
using System.Windows.Input;
using MVVMLib;

namespace AdemolaTyper.ViewModels
{
    /// <summary>
    /// Represents an actionable item displayed by a View.
    /// </summary>
    public class CommandViewModel : ViewModelBase
    {
        public CommandViewModel(string displayName, ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            base.DisplayName = displayName;
            Command = command;
        }

        public ICommand Command { get; private set; }
    }
}