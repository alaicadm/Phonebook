using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PhoneBook.ViewModel
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        //readonly Predicate<object> _canExecute;
        readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value;}
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if(execute == null) { throw new ArgumentNullException("execute"); }
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute) : this(execute, null) 
        {

        }

       

        public bool CanExecute(object parameter) 
        { 
            //return _canExecute == null ? true : _canExecute(parameter); 
            return _canExecute == null || this._canExecute(parameter);
        }

        public void Execute(object parameter) { _execute.Invoke(parameter); }
    }
}
