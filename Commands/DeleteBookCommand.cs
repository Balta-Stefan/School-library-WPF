using School_library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace School_library.Commands
{
    public class DeleteBookCommand : ICommand
    {
        private BooksPanelViewModel viewModel;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (viewModel.SelectedBook == null)
                return false;
            else
                return true;
        }

        public void Execute(object? parameter)
        {
            viewModel.deleteBook();
        }

        public void changeSelection()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public DeleteBookCommand(BooksPanelViewModel viewModel) => this.viewModel = viewModel;
    }
}
