using System;
using System.Windows.Input;

namespace UI.Core
{
	public class RelayComamnd : ICommand
	{
		private Action<object> execute;
		private Func<object, bool> canExecute;

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public RelayComamnd(Action<object> _execute, Func<object, bool> _canExecute = null)
		{
			execute = _execute;
			canExecute = _canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return canExecute == null || canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			canExecute(parameter);
		}
	}
}
