using School_library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace School_library.Commands
{
    public class EditBookInfoCommand : ICommand
    {
        private EditBookInfoViewModel editBookViewModel;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            editBookViewModel.updateBookInfo();
        }

        public EditBookInfoCommand(EditBookInfoViewModel editBookViewModel) => this.editBookViewModel = editBookViewModel;
    }
}
