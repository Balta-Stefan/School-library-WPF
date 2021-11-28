using School_library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace School_library.Commands
{
    public class DeleteBookCopyCommand : ICommand
    {
        private EditBookInfoViewModel editBookViewModel;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return editBookViewModel.copySelected;
        }

        public void Execute(object? parameter)
        {
            editBookViewModel.deleteCopy();
        }

        public void executeChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }


        public DeleteBookCopyCommand(EditBookInfoViewModel editBookViewModel) => this.editBookViewModel = editBookViewModel;
    }
}
